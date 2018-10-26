using System;
using System.Runtime.Serialization;

namespace OpenMarket.Api {

    /// <summary>
    /// Object that contains a variety of optional message sending options.
    /// </summary>
    [DataContract(Name = "options")]
    class SendOptions {

        /// <summary>Use this to add data to the request that you may want 
        /// available in reports, such as individual identifiers (e.g. your 
        /// own transaction, ticket, or system IDs). It has no effect on 
        /// the message or its delivery. The value is free-form text that 
        /// is 0 to 200 characters in length.</summary>
        [DataMember(Name = "note1", EmitDefaultValue = false)]
        public string note1 = null;

        /// <summary>Use this to add data to the request that you may want 
        /// available in reports, such as individual identifiers (e.g. 
        /// your own transaction, ticket, or system IDs). It has no effect 
        /// on the message or its delivery. The value is free-form text 
        /// that is 0 to 200 characters in length.</summary>
        [DataMember(Name = "note2", EmitDefaultValue = false)]
        public string note2 = null;

        /// <summary>Required for US messaging when using a short code. 
        /// The value is ignored for all other messaging. This identifies 
        /// a pre-provisioned program linked to the short code messaging 
        /// service you are providing.OpenMarket will provide you with the 
        /// value, which will be between 1 to 50 characters, and is not 
        /// case-sensitive.</summary>
        [DataMember(Name = "programId", EmitDefaultValue = false)]
        public string programId = null;

        /// <summary>Determines whether the message is sent as a flash 
        /// message. Flash messages are shown immediately on an end 
        /// user's mobile phone without the user taking any action, and 
        /// are often not saved to the phone.</summary>
        [DataMember(Name = "flash", EmitDefaultValue = false)]
        public string flash = null;

    }

}
