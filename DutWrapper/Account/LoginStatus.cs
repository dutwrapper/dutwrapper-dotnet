using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper
{
    public static partial class Account
    {
        /// <summary>
        /// Account status while logging in.
        /// </summary>
        public enum LoginStatus
        {
            /// <summary>
            /// Unknown status. This might be code exception.
            /// </summary>
            Unknown = -2,
            /// <summary>
            /// No internet connection.
            /// </summary>
            NoInternet = -1,
            /// <summary>
            /// This acconut has logged in.
            /// </summary>
            LoggedIn = 0,
            /// <summary>
            /// Logged out or not logged in yet.
            /// </summary>
            LoggedOut = 1,
            /// <summary>
            /// This account has been locked.
            /// </summary>
            AccountLocked = 2
        }
    }
}
