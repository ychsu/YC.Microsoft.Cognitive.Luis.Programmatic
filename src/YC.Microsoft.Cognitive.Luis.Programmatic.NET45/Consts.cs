using System;
using System.Collections.Generic;
using System.Text;

namespace YC.Microsoft.Cognitive.Luis.Programmatic
{
    internal class Consts
    {
        /// <summary>
        /// endpoint
        /// </summary>
        /// <remarks>
        /// {0} 用region取代
        /// {1} appId
        /// {2} versionId
        /// </remarks>
        public const string Endpoint = "https://{0}.api.cognitive.microsoft.com/luis/api/v2.0/apps/{1}/versions/{2}/";

        public const string SubscriptionHeaderKey = "Ocp-Apim-Subscription-Key";
    }
}
