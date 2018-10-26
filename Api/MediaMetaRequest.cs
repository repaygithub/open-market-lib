using System;
using System.Net;
using System.Runtime.Serialization;

using Leedom.Common;

namespace OpenMarket.Api {

    ///<summary>
    ///Object that contains details of the message you are sending
    ///</summary>
    [DataContract(Name = "mms")]
    class MediaMetaRequest {

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


        ///<summary>
        /// Identifies what type of message you are sending 
        /// (MT).</summary>
        [DataMember(Name = "type")]
        public string type = "MT";

        ///<summary>
        /// Identifies the sender of the message i.e your contact number as seen by the end user
        /// In the US, this must be an MMS-specific short code. In the UK, this can be either a 
        /// short code or a virtual mobile number. If supplying a virtual mobile number, the number 
        /// must be in international format (e.g. 447700900750) and without a leading "+" character.
        /// </summary>
        [DataMember(Name = "source")]
        public string source = null;

        ///<summary>
        /// The mobile number to which you are sending the MMS 
        /// message. The number must be in international format
        /// (eg. 44700900765) and without a leading "+" character.
        /// </summary>
        [DataMember(Name = "destination")]
        public string destination = null;


        ///<summary>
        /// The MT configuration is specified in this block
        /// </summary>
        [DataMember(Name = "mtConfig", EmitDefaultValue = false)]
        public string mtConfig = "default";

        ///<summary>
        /// The subject line for MMS message. This displays on the end user handset
        /// often either above the content or along with the source of the MMS-specific
        /// </summary>
        [DataMember(Name = "subject", EmitDefaultValue = false)]
        public string subject = null;

        ///<summary>
        /// This displays the numberic ID representing the mobile operator as known by
        /// OpenMarket 
        /// </summary>
        [DataMember(Name = " mobileOperatorId", EmitDefaultValue = false)]
        public int mobileOperatorId = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="description"></param>
        public MediaMetaRequest(string to, string from, string description) {
            this.destination = to;
            this.source = from;
            this.subject = description;
        }

        /// <summary>
        /// Check to see if enough valid meta information is available to build
        /// the open market payload.
        /// </summary>
        /// <returns>Indicates there is not enough information (true) or the payload can proceed construction (false).</returns>
        public bool IsNotValid() {
            return false;
        }

        /// <summary>
        /// Create a string representaion of this media type
        /// that can be used in the open market payload.
        /// </summary>
        /// <returns>A string representation of thie media type in the required open market format.</returns>
        public string Serialize() {
            return String.Empty;
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