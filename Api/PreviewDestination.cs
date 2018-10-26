using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// Object that contains information about the phone number.
    /// </summary>
    [DataContract(Name = "destination")]
    class PreviewDestination {

        /// <summary>The country dial in code for the phone number. </summary>
        [DataMember(Name = "countryCode")]
        public string countryCode = null;

        /// <summary>The phone number of the end user, including the country code. </summary>
        [DataMember(Name = "phoneNumber")]
        public string cell = null;

        /// <summary>Identifies the country of origin for the phone number. This is a two-letter country code, as defined in ISO 3166-1.</summary>
        [DataMember(Name = "alpha2Code")]
        public string alpha2Code = null;

        /// <summary>Whether the phone number is enabled to send SMS.</summary>
        [DataMember(Name = "wireless")]
        public bool wireless = false;

    }

}
