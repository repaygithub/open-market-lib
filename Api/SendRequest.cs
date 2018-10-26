using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    [DataContract(Name = "sendRequest")]
    class SendRequest {

        /// <summary>The JSON body has a top-level object that contains the one member.</summary>
        [DataMember(Name = "mobileTerminate")]
        public SendPayload payload = null;

        /// <summary>
        /// Instantiate a new send request.
        /// </summary>
        public SendRequest() {
            this.payload = new SendPayload();
        }

    }

}
