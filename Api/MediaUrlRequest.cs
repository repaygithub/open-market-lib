using System;
using System.IO;
using System.Net;
using System.Text;

using Leedom.Common;
using Leedom.Common.WebAppFramework.Validate;

namespace OpenMarket.Api {

    class MediaUrlRequest {

        /// <summary>The web accessible media URL.</summary>
        private string url = null;

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
        /// Instantiate a new media url request with the given URL.
        /// </summary>
        /// <param name="url"></param>
        public MediaUrlRequest(string url) {
            this.url = url;
        }

        /// <summary>
        /// Check to see if enough valid information is available to include
        /// this in the open market payload.
        /// </summary>
        /// <returns>Indicates whether or not this media type can be included in the payload.</returns>
        public bool IsAvailable() {
            if(string.IsNullOrEmpty(this.url)) {
                return false;            
            }

            IValidate validator = ValidatorFactory.DefaultInstance().AcquireValidator("expression");
            if (!validator.IsValid("VRE_URL", this.url)) {
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
            buffer.AppendLine("Content-Type: application/vnd.openmarket.remoteContent");
            buffer.AppendLine(String.Format("Content-Id: {0}", Path.GetFileName(this.url)));
            buffer.AppendLine();
            buffer.AppendLine(this.url);

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
	