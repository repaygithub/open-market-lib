using System;
using System.Net;
using System.Text;

using Leedom.Common;
using Leedom.Common.Utils;

namespace OpenMarket.Api {

    public class ShortMessageService : IProvideSMS {

        /// <summary>
        /// Called to record a user or system action for
        /// accountability and debugging purposes.
        /// </summary>
        public event PostEntry OnPostEntry;

        /// <summary>
        /// Called to report a probelm as an HTTP response status
        /// and message.
        /// </summary>
        public event ApplyHttpResponse OnHttpResponse;

        /// <summary>
        /// The authorization header value to use when making Open Market reqeusts.
        /// </summary>
        private string auth;

        /// <summary>
        /// Indicates the message was successfully sent where applicable.
        /// </summary>
        private bool sent = false;

        /// <summary>
        /// Indicates the phone number was confirmed as a cell.
        /// </summary>
        private bool confirmed = false;

        /// <summary>
        /// Clean up any resources.
        /// </summary>
        public void Dispose() {
        }

        /// <summary>
        /// Send a messsage to a cell recipient.
        /// </summary>
        /// <param name="user">The user identity (api key) to authenticate with the provider.</param>
        /// <param name="password">The user password (secondary key) to authenticate with the provider.</param>
        /// <param name="to">The cell phone number to send the message to.</param>
        /// <param name="from">The long or short code to send the message from.</param>
        /// <param name="message">The SMS message content.</param>
        /// <param name="reference">An optional reference to associate and keep with the message.</param>
        /// <param name="note">An optional note to associate and keep with the message.</param> 
        /// <returns>Indicates the success or failure of the send.</returns>
        public bool Send(string user, string password, string to, string from, string message, string reference, string note) {
            if (String.IsNullOrEmpty(user)) {
                this.ApplyResponse(HttpStatusCode.InternalServerError, "Api user name is missing or empty.");
                return false;
            }

            if (String.IsNullOrEmpty(password)) {
                this.ApplyResponse(HttpStatusCode.InternalServerError, "Api user passsword is missing or empty.");
                return false;
            }

            this.auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", user, password)));
            this.sent = false;

            SendRequest request = new SendRequest();
            request.payload.destination.address = to;
            request.payload.source.address = from;
            request.payload.source.type = 1; // 1 = long code, 3 = short code
            request.payload.message.content = message;
            request.payload.message.type = "text";

            if ((!String.IsNullOrEmpty(reference)) || (!String.IsNullOrEmpty(note))) {
                request.payload.options = new SendOptions();
                request.payload.options.note1 = reference;
                request.payload.options.note2 = note;
            }

            HttpTransceiver adapter = new HttpTransceiver();
            adapter.OnRequestInitialized += this.RequestInitialized;
            adapter.OnRequestComplete += this.SendRequestComplete;
            adapter.OnRequestError += this.SendRequestError;

            string url = String.Format("{0}{1}", om.Default.api_base_url, om.Default.send_method_name);
            adapter.Post(url, typeof(SendRequest), request, typeof(SendResponse));

            return this.sent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="filePath"></param>
        /// <param name="b64Filedata"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public bool Media(string user, string password, string to, string from, string filePath, string b64Filedata, string description)
        {
            if (String.IsNullOrEmpty(user))
            {
                this.ApplyResponse(HttpStatusCode.InternalServerError, "Api user name is missing or empty.");
                return false;
            }

            if (String.IsNullOrEmpty(password))
            {
                this.ApplyResponse(HttpStatusCode.InternalServerError, "Api user passsword is missing or empty.");
                return false;
            }

            this.auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", user, password)));
            this.sent = false;

            MediaRequest request = new MediaRequest(to, from, filePath, b64Filedata, description);
            request.OnHttpResponse += this.ApplyResponse;
            request.OnPostEntry += this.PostEntry;

            if (request.IsNotValid()) {
                return false;
            }
            
            HttpTransceiver adapter = new HttpTransceiver();
            adapter.OnRequestInitialized += this.RequestInitialized;
            adapter.OnRequestComplete += this.SendRequestComplete;
            adapter.OnRequestError += this.SendRequestError;

            string url = String.Format("{0}{1}", om.Default.api_base_url, om.Default.mms_method_name);
            adapter.Post(url, typeof(string), request.Serialize(), typeof(MediaResponse));

            return this.sent;

        }

        /// <summary>
        /// Check to see if the given phone digits are
        /// indeed a cell phone. 
        /// </summary>
        /// <param name="user">The user identity (api key) to authenticate with the provider.</param>
        /// <param name="password">The user password (secondary key) to authenticate with the provider.</param>
        /// <param name="digits">The phone number digits to check for a cell.</param>
        /// <returns>Indicates whether or not the digits are a cell.</returns>
        public bool IsACell(string user, string password, string digits) {
            if (String.IsNullOrEmpty(user)) {
                this.ApplyResponse(HttpStatusCode.InternalServerError, "Api user name is missing or empty.");
                return false;
            }

            if (String.IsNullOrEmpty(password)) {
                this.ApplyResponse(HttpStatusCode.InternalServerError, "Api user passsword is missing or empty.");
                return false;
            }

            this.auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(String.Format("{0}:{1}", user, password)));
            this.confirmed = false;

            HttpTransceiver adapter = new HttpTransceiver();
            adapter.OnRequestInitialized += this.RequestInitialized;
            adapter.OnRequestComplete += this.PreviewRequestComplete;
            adapter.OnRequestError += this.PreviewRequestError;

            string url = String.Format("{0}{1}{2}", om.Default.api_base_url, om.Default.confirm_method_name, digits);
            adapter.Get(url, typeof(PreviewResponse));

            return this.confirmed;
        }

        /// <summary>
        /// Check to see if the given short code
        /// keyword is available. Typically used 
        /// for shared short codes since Textmaxx
        /// knows all keywords for owned short 
        /// codes.
        /// </summary>
        /// <param name="user">The user identity (api key) to authenticate with the provider.</param>
        /// <param name="password">The user password (secondary key) to authenticate with the provider.</param>
        /// <param name="did">The short code DID to check if the given keyword already exists.</param>
        /// <param name="keyword">The keyword to for availability.</param>
        /// <returns>Indicates whether or not the keyword is available.</returns>
        public bool IsKeywordAvailable(string user, string password, string did, string keyword) {
            return true;  // not implemented
        }

        /// <summary>
        /// Called after the HTTP request object is setup.
        /// Right before it is consumed.
        /// </summary>
        /// <param name="request">The request object about to be consumed. Make any adjustments or additions as necessary.</param>
        private void RequestInitialized(HttpWebRequest request) {
            request.Headers[HttpRequestHeader.Authorization] = String.Format("Basic {0}", this.auth);
        }

        /// <summary>
        /// Called when any error occurs in the transceiver. Typically
        /// this will be a resposne error but can also point to something
        /// more simple like a missing or invalid URI.
        /// </summary>
        /// <param name="request">The request object if it has been initalized. Null otherwise.</param>
        /// <param name="status">An HTTP status code that most represents the problem that occured.</param>
        /// <param name="error">A more descriptive error of what happened.</param>
        /// <param name="resposne">The HTTP response object if one is available. Null otherwise.</param>
        private void SendRequestError(HttpWebRequest request, HttpStatusCode status, string error, object response) {
            this.sent = false;

            string errorCode = status.ToString();
            string errorText = error;
            
            if (response != null) {
                SendResponse responseObj = (SendResponse)response;
                errorCode = responseObj.error.code;
                errorText = responseObj.error.description;
            }

            this.ApplyResponse(status, String.Format("{0}: {1}", errorCode, errorText));
        }

        /// <summary>
        /// Called when any error occurs in the transceiver. Typically
        /// this will be a resposne error but can also point to something
        /// more simple like a missing or invalid URI.
        /// </summary>
        /// <param name="request">The request object if it has been initalized. Null otherwise.</param>
        /// <param name="status">An HTTP status code that most represents the problem that occured.</param>
        /// <param name="error">A more descriptive error of what happened.</param>
        /// <param name="resposne">The HTTP response object if one is available. Null otherwise.</param>
        private void PreviewRequestError(HttpWebRequest request, HttpStatusCode status, string error, object response) {
            this.confirmed = false;

            string errorCode = status.ToString();
            string errorText = error;

            if (response != null) {
                PreviewResponse responseObj = (PreviewResponse)response;
                errorCode = responseObj.error.code;
                errorText = responseObj.error.description;
            }

            this.ApplyResponse(status, String.Format("{0}: {1}", errorCode, errorText));
        }

        /// <summary>
        /// Called upon the successful completion of the send request and
        /// response.
        /// </summary>
        /// <param name="request">The request object used.</param>
        /// <param name="response">The response object that can be type casted to your expected response type.</param>
        private void SendRequestComplete(HttpWebRequest request, object response) {
            this.sent = true;
        }

        /// <summary>
        /// Called upon the successful completion of the preview (cell confirmation) request and
        /// response.
        /// </summary>
        /// <param name="request">The request object used.</param>
        /// <param name="response">The response object that can be type casted to your expected response type.</param>
        private void PreviewRequestComplete(HttpWebRequest request, object response) {
            if (response == null) {
                this.ApplyResponse(HttpStatusCode.InternalServerError, "Confirmation response is missing or empty.");
                return;
            }

            PreviewResponse responseObj = (PreviewResponse)response;

            this.confirmed = responseObj.payload.destination.wireless;
        }

        /// <summary>
		/// Record a user or system action for later retrival.
		/// </summary>
		/// <param name="action">The user or system action performed.</param>
		/// <param name="message">A succinct description of what happened.</param>
		private void PostEntry(string action, string message) {
            this.OnPostEntry?.Invoke(action, message);
        }

        /// <summary>
        /// Update the HTTP response status to report an issue to the 
        /// API user.
        /// </summary>
        /// <param name="statusCode">The new HTTP status code.</param>
        /// <param name="message">A succinct description of the issue encountered.</param>
        private void ApplyResponse(HttpStatusCode statusCode, string message) {
            this.OnHttpResponse?.Invoke(statusCode, message);
        }

    }

}
