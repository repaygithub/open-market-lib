using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// 
    /// </summary>
    [DataContract(Name = "error")]
    class ErrorResponse {

        /// <summary>An Open Market error code to further identify the issue.</summary>
        [DataMember(Name = "code")]
        public string code = null;

        /// <summary>A short description of the request issue encountered.</summary>
        [DataMember(Name = "description")]
        public string description = null;

    }
}
