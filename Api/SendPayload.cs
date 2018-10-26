using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    [DataContract(Name = "mobileTerminate")]
    class SendPayload {

        /// <summary>Whether this is a global one-way or two-way 
        /// message. You do not need to specify interaction if 
        /// you are messaging in the US or Canada (the value 
        /// is ignored).</summary>
        [DataMember(Name = "interaction", EmitDefaultValue = false)]
        public string interaction = null;

        /// <summary>Required for India messaging only. This 
        /// identifies whether the message is transactional or 
        /// promotional. For any non-India messaging, this field 
        /// is ignored. </summary>
        [DataMember(Name = "promotional", EmitDefaultValue = false)]
        public string promotional = null;

        /// <summary>Object that contains a variety of optional 
        /// message sending options.</summary>
        [DataMember(Name = "options", EmitDefaultValue = false)]
        public SendOptions options = null;

        /// <summary>Object that contains details about the 
        /// message recipient (the end user's details).</summary>
        [DataMember(Name = "destination", EmitDefaultValue = false)]
        public SendDestination destination = null;

        /// <summary>Object that contains details about the 
        /// message sender (your details).</summary>
        [DataMember(Name = "source")]
        public SendSource source = null;

        /// <summary>Object that contains details about the 
        /// message you are sending.</summary>
        [DataMember(Name = "message")]
        public SendMessage message = null;

        /// <summary>Object that identifies that you want to 
        /// receive delivery receipts.</summary>
        [DataMember(Name = "delivery", EmitDefaultValue = false)]
        public SendDelivery delivery = null;

        /// <summary>
        /// Instantiate a new send method payload.
        /// </summary>
        public SendPayload() {
            this.destination = new SendDestination();
            this.source = new SendSource();
            this.message = new SendMessage();
        }

    }

}
