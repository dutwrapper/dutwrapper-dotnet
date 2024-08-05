using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper.WinUI.Accounts
{
    public class AuthInfo
    {
        public string? Username { get; private set; }

        public string? Password { get; private set; }

        public AuthInfo(string? username = null, string? password = null)
        {
            this.Username = username;
            this.Password = password;
        }

        public void EnsureValidAuth()
        {
            if (Username == null || Password == null)
            {
                throw new Exception("Username or password is empty!");
            }
            if (Username.Length < 6 || Password.Length < 6)
            {
                throw new Exception("Username or password length is less than 6 characters!");
            }
        }
    }
}
