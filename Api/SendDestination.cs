using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// Object that contains details about the message recipient (the end user's details).
    /// </summary>
    [DataContract(Name = "destination")]
    class SendDestination {

        /// <summary>
        /// The phone number to which you are sending an SMS message. 
        /// This must be a phone number that includes the country 
        /// code. Do not include a "+" character.</summary>
        [DataMember(Name = "address")]
        public string address = null;

        /// <summary>
        /// An OpenMarket-specific number that identifies the mobile 
        /// operator to which OpenMarket should route the message.</summary>
        [DataMember(Name = "mobileOperatorId", EmitDefaultValue = false)]
        public int? mobileOperatorId = null;

    }
}
