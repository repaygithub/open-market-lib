using System;
using System.Net;
using System.Text;

using Leedom.Common;

namespace OpenMarket.Api {

    class MediaDataRequest {

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

        /// <summary>The local or remote file name of the data.</summary>
        private string fileName;

        /// <summary>A base64 representation of the file byte data.</summary>
        private string b64Filedata;

        /// <summary>The mime type of the given file.</summary>
        private string mimeType;

        /// <summary>
        /// Instantiate a media data request for use in the open market payload.
        /// </summary>
        /// <param name="fileName">The local or remote file name of the data.</param>
        /// <param name="b64Filedata">A base64 representation of the file byte data.</param>
        public MediaDataRequest(string fileName, string b64Filedata) {
            this.fileName = fileName;
            this.b64Filedata = b64Filedata;
        }

        /// <summary>
        /// Check to see if enough valid information is available to include
        /// this in the open market payload.
        /// </summary>
        /// <returns>Indicates whether or not this media type can be included in the payload.</returns>
        public bool IsAvailable() {
            if (String.IsNullOrEmpty(this.fileName)) {
                return false;
            }

            if (String.IsNullOrEmpty(this.b64Filedata)) {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Create a string representaion of this media type
        /// that can be used in the open market payload.
        /// </summary>
        /// <returns>A string representation of thie media type in the required open market format.</returns>
        public string Serialize() {
            StringBuilder buffer = new StringBuilder();
            buffer.AppendLine(String.Format("Content-Type: {0}", this.mimeType));
            buffer.AppendLine(String.Format("Content-Id: {0}", this.fileName));
            buffer.AppendLine("Content-Transfer-Encoding: BASE64");
            buffer.AppendLine();
            buffer.AppendLine(this.b64Filedata);

            return buffer.ToString();
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