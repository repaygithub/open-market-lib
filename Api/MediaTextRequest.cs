using System;
using System.Net;
using System.Text;

using Leedom.Common;
using Leedom.Common.Utils;

namespace OpenMarket.Api {
    
    class MediaTextRequest {

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

        /// <summary>A semi-unique identity for the media. This is often the file name.</summary>
        private string id;

        /// <summary>A short description of the media for those unable to view it.</summary>
        private string message;

        /// <summary>
        /// Instantiate a new media text request for use in the open market payload.
        /// </summary>
        /// <param name="fileName">An optional semi-unique identity for the media. Most often the file name.</param>
        /// <param name="message">A short description of the media for those unable to view it.</param>
        public MediaTextRequest(string fileName, string message) {
            this.id = fileName;
            this.message = message;
        }

        /// <summary>
        /// Check to see if enough valid information is available to include
        /// this in the open market payload.
        /// </summary>
        /// <returns>Indicates whether or not this media type can be included in the payload.</returns>
        public bool IsAvailable() {
            if (String.IsNullOrEmpty(this.message)) {
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
            if (String.IsNullOrEmpty(this.id)) {
                this.id = Utilities.GenerateHash(this.message);
            }

            StringBuilder buffer = new StringBuilder();
            buffer.AppendLine("Content-Type: text/plain; charset=utf-8");
            buffer.AppendLine(String.Format("Content-Id: {0}", this.id));
            buffer.AppendLine();
            buffer.AppendLine(this.message);

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