using System;

namespace Open.OAuth2
{
    public class OAuth2Exception : Exception
    {
        public OAuth2Exception(string message)
            : base(message)
        {

        }

        public OAuth2Exception(OAuth2Error error)
            : base(error.Error)
        {

        }

        public OAuth2Exception(Exception exc)
            : base(exc.Message, exc)
        {

        }
    }
}
