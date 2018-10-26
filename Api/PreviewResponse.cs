using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// The response received from Open Market from 
    /// the cell confirmation (preview) request.
    /// </summary>
    [DataContract(Name = "previewResponse")]
    class PreviewResponse {

        /// <summary>The main response payload containing meta data.</summary>
        [DataMember(Name = "preview")]
        public PreviewPayload payload = null;

        /// <summary>An error object populated for failed requests.</summary>
        [DataMember(Name = "error")]
        public ErrorResponse error = null;        

    }
}
