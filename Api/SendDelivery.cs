using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// Object that identifies that you want to receive delivery receipts.
    /// </summary>
    [DataContract(Name = "delivery")]
    class SendDelivery {

        /// <summary>
        /// Indicates that you want to receive a delivery receipt 
        /// for the message. OpenMarket will send the delivery 
        /// receipt when the mobile operator sends us a final 
        /// delivery state.</summary>
        [DataMember(Name = "receiptRequested", EmitDefaultValue = false)]
        public string receiptRequested = null;

        /// <summary>
        /// The URL to which you want us to send delivery receipts 
        /// for this message. The value must be between 12 to 2048 
        /// characters.The URL must begin with the protocol, 
        /// e.g.: https:// or http://.</summary>
        [DataMember(Name = "url", EmitDefaultValue = false)]
        public string url = null;

    }

}
