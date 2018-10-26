using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// The response sent back from open market 
    /// from the send request.
    /// </summary>
    [DataContract(Name = "sendResponse")]
    class SendResponse {

        /// <summary>An error object populated for failed requests.</summary>
        [DataMember(Name = "error")]
        public ErrorResponse error = null;

    }
}
