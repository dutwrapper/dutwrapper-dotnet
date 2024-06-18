using System;
using System.Collections.Generic;
using System.Text;

namespace DutWrapper
{
    public static partial class Account
    {
        public class Session
        {
            public string? SessionId { get; private set; }

            public string? ViewState { get; private set; }

            public string? ViewStateGenerator { get; private set; }

            public Session(string? sessionId = null, string? viewState = null, string? viewStateGenerator = null)
            {
                this.SessionId = sessionId;
                this.ViewState = viewState;
                this.ViewStateGenerator = viewStateGenerator;
            }

            public void EnsureValidSession()
            {
                if (SessionId == null)
                {
                    throw new Exception("Session ID is not valid!");
                }
            }

            public void EnsureValidViewState()
            {
                if (ViewState == null || ViewStateGenerator == null)
                {
                    throw new Exception("ViewState variables is not valid!");
                }
            }
        }
    }
}
