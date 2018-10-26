using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    [DataContract(Name = "preview")]
    class PreviewPayload {

        /// <summary>Object that contains information about the phone number.</summary>
        [DataMember(Name = "destination")]
        public PreviewDestination destination = null;

        /// <summary>Object that contains information about the mobile operator.</summary>
        [DataMember(Name = "mobileOperator")]
        public PreviewMobileOperator mobileOperator = null;

    }

}
