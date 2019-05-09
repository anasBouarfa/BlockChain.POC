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

    class EncrytionConstants
    {
        /// <summary>
        ///  The secret encryption key used in the application
        /// </summary>
        public static byte[] EncryptionKey = new byte[8] { 106, 48, 123, 23, 49, 86, 3, 114 };

        /// <summary>
        ///  The initialization vector used in the ecnryption
        /// </summary>
        public static byte[] InitializationVector = new byte[8] { 10, 29, 151, 94, 222, 123, 117, 8 };

    }
}
