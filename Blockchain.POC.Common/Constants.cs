using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain.POC.Common
{
    public class ApplicationPropertiesConstants
    {
        /// <summary>
        ///  The port used by the application
        /// </summary>
        public const string Port = "Port";

        /// <summary>
        /// A dictionnary of each url as key and the websocket that the current client is connected to as value
        /// </summary>
        public const string PortUrlWebSockets = "urlWebSockets";

        /// <summary>
        /// The current user address
        /// </summary>
        public const string UserAddress = "User";


    }
}
