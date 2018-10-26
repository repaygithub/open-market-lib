using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// Object that contains information about the mobile operator.
    /// </summary>
    [DataContract(Name = "mobileOperator")]
    class PreviewMobileOperator {

        /// <summary>An OpenMarket-specific number that identifies the mobile operator.</summary>
        [DataMember(Name = "mobileOperatorId")]
        public int id = 0;

        /// <summary>The name of the mobile operator.</summary>
        [DataMember(Name = "mobileOperatorName")]
        public string name = null;

        /// <summary>The maximum number of GSM characters that you can send in a single part message to this operator.</summary>
        [DataMember(Name = "textLength")]
        public int messageLength = 0;

        /// <summary>The maximum number of bytes that you can send in a single part message to this operator.</summary>
        [DataMember(Name = "binaryLength")]
        public int binaryLength = 0;

        /// <summary>The maximum number of UCS-2 characters that you can send in a single part message to this operator.</summary>
        [DataMember(Name = "unicodeLength")]
        public int unicodeLength = 0;

        /// <summary>Whether the mobile operator supports WAP Push messaging.</summary>
        [DataMember(Name = "wapPush")]
        public bool wapPush = false;

    }

}
