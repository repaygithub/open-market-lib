using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// Object that contains details about the message sender (your details).
    /// </summary>
    [DataContract(Name = "source")]
    class SendSource {

        /// <summary>
        /// The message originator from which you want to send this 
        /// message.</summary>
        [DataMember(Name = "address")]
        public string address = null;

        /// <summary>
        /// Indicates what type of number is set in address. OpenMarket 
        /// will validate the source address based on the ton value.
        /// </summary>
        [DataMember(Name = "ton", EmitDefaultValue = false)]
        public int? type = null;

    }
}
