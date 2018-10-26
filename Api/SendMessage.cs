using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// Object that contains details about the message you are sending.
    /// </summary>
    [DataContract(Name = "message")]
    class SendMessage {

        /// <summary>
        /// Identifies what type of message you are sending 
        /// (text, WAP Push, or binary). For text messages, 
        /// it also identifies whether you are sending the 
        /// message contents to us as plain text or 
        /// hex-encoded text.</summary>
        [DataMember(Name = "type")]
        public string type = null;

        /// <summary>
        /// The message sent to the end user's phone. 
        /// Alphanumeric string that is between 1 to 2680 
        /// bytes in size. If the content is longer than 
        /// one SMS message, then it may be rejected, 
        /// truncated, or segmented.The value of mlc sets 
        /// this behavior.</summary>
        [DataMember(Name = "content")]
        public string content = null;

        /// <summary>
        /// Required if the message type is set to 
        /// hexEncodedText. Identifies the character set 
        /// or encoding that you have used for the text.
        /// This is valid only for hex-encoded text 
        /// (otherwise the value is ignored). 
        /// </summary>
        [DataMember(Name = "charset", EmitDefaultValue = false)]
        public string charset = null;

        /// <summary>
        /// Specifies the period (in seconds) that 
        /// OpenMarket and mobile operators will attempt 
        /// to deliver the message. You can specify a 
        /// number between 0 and 259200.
        /// </summary>
        [DataMember(Name = "validityPeriod", EmitDefaultValue = false)]
        public int? validityPeriod = null;

        /// <summary>
        /// Required if the message type is set to wapPush. 
        /// The URL that you want sent in the WAP Push.</summary>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string url = null;

        /// <summary>
        /// Specifies what action to take if the message content 
        /// is larger than a single part SMS.
        /// </summary>
        [DataMember(Name = "mlc", EmitDefaultValue = false)]
        public string mlc = null;

        /// <summary>
        /// Indicates whether a user data header (UDH) is encoded 
        /// in the message. This value is valid when type is 
        /// either hexEncodedText or binary.</summary>
        [DataMember(Name = "udh", EmitDefaultValue = false)]
        public bool? header = null;

    }
}
