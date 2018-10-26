using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    [DataContract(Name = "error")]
    public class MediaError {

        /// <summary>
        /// Code defining the reason for the outcome of the 
        /// request. For a list of the codes, see Response 
        /// error codes: https://www.openmarket.com/docs/Content/apis/mms-http/mms-send.htm#Response
        /// </summary>
        [DataMember(Name = "code")]
        public string code;

        /// <summary>
        /// Natural language description of the error.
        /// </summary>
        [DataMember(Name = "description")]
        public string description;

    }

}
