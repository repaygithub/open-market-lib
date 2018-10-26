using System;
using System.IO;
using System.Net;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

using Leedom.Common;

namespace OpenMarket.Api {

    public class MediaRequest {

        /// <summary>The string of characters to use as the mime boundary in the open market paylod.</summary>
        public readonly string MIME_BOUNDARY = String.Format("--", Guid.NewGuid());

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

        /// <summary>A handler for the media request meta data.</summary>
        private MediaMetaRequest meta;

        /// <summary>A handler for base64 encoded media.</summary>
        private MediaDataRequest data;

        /// <summary>A handler for textual media.</summary>
        private MediaTextRequest text;

        /// <summary>A handler for web accessible media.</summary>
        private MediaUrlRequest url;

        public MediaRequest(string to, string from, string filePath, string b64Filedata, string description) {
            string fileName = null;
            if (!String.IsNullOrEmpty(filePath)) {
                fileName = Path.GetFileName(filePath);
            }

            meta = new MediaMetaRequest(to, from, description);
            meta.OnHttpResponse += this.ApplyResponse;
            meta.OnPostEntry += this.PostEntry;        

            data = new MediaDataRequest(fileName, b64Filedata);
            data.OnHttpResponse += this.ApplyResponse;
            data.OnPostEntry += this.PostEntry;

            text = new MediaTextRequest(fileName, description);
            text.OnHttpResponse += this.ApplyResponse;
            text.OnPostEntry += this.PostEntry;

            url = new MediaUrlRequest(filePath);
            url.OnHttpResponse += this.ApplyResponse;
            url.OnPostEntry += this.PostEntry;
        }

        /// <summary>
        /// Check to see if we have enough information to build the 
        /// open market MMS payload.
        /// </summary>
        /// <returns>Indicates there is not enough information (true) or we can proceed (false).</returns>
        public bool IsNotValid() {
            if (meta.IsNotValid()) {
                return true;
            }

            if ((!url.IsAvailable()) && (!text.IsAvailable()) && (!data.IsAvailable())) {
                this.ApplyResponse(HttpStatusCode.BadRequest, "At least one media type is required.");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Create the open market payload from the available information.
        /// </summary>
        /// <returns>A string representation of the various media request types and meta data.</returns>
        public String Serialize() {
            StringBuilder sb = new StringBuilder(this.MIME_BOUNDARY);
            sb.Append(meta.Serialize());
            sb.Append(this.ToJSON(typeof(MediaMetaRequest), meta));

            if (url.IsAvailable()) {
                sb.Append(this.MIME_BOUNDARY);
                sb.AppendLine(url.Serialize());
            }

            if (text.IsAvailable()) {
                sb.Append(this.MIME_BOUNDARY);
                sb.AppendLine(text.Serialize());
            }

            if (data.IsAvailable()) {
                sb.Append(this.MIME_BOUNDARY);
                sb.AppendLine(data.Serialize());
            }

            sb.AppendLine(this.MIME_BOUNDARY);

            return sb.ToString();
        }

        /// <summary>
        /// Convert the request object class into a JSON or
        /// XML request body. 
        /// </summary>
        /// <param name="requestType">The type of request being sent.</param>
        /// <param name="requestObj">The actual request object to serialize.</param>
        /// <returns>A byte array of the serialized content or null if none available.</returns>
        private string ToJSON(Type requestType, object requestObj) {
            if (requestObj == null) {
                return null;
            }

            if (requestType == typeof(String)) {
                return String.Format("{0}", requestObj);
            }

            MemoryStream stream = new MemoryStream();

            try {
                XmlObjectSerializer metaData = new DataContractJsonSerializer(requestType);
                metaData.WriteObject(stream, requestObj);
            }
            catch (Exception) {
                return null;
            }

            if (stream == null) {
                return null;
            }

            string jsonContent = Encoding.UTF8.GetString(stream.ToArray());
            return jsonContent;
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
