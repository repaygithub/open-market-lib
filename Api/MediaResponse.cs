using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    [DataContract(Name = "mms")]
    public class MediaResponse {

        /// <summary>
        /// A unique OpenMarket ID for the MMS you 
        /// have sent. Delivery reports use this ID 
        /// to identify the MMS message they are 
        /// reporting on.
        /// </summary>
        [DataMember(Name = "mtId")]
        public ulong id;

        /// <summary>
        /// If your request is rejected then the 
        /// body of the response will contain a 
        /// code that identifies the error.
        /// </summary>
        [DataMember(Name = "error")]
        public MediaError error;

        /// <summary>
        /// X-Request-Id: A unique ID that identifies the 
        /// specific request in OpenMarket's systems.This 
        /// ID is logged in our systems and is useful if 
        /// you need to query the request with OpenMarket 
        /// Support at a later date.
        /// </summary>
        public string requestId;

    }
}

