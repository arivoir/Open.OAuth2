using System.Runtime.Serialization;

namespace Open.OAuth2
{
    [DataContract]
    public class OAuth2Token
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
        [DataMember(Name = "refresh_token", IsRequired = false)]
        public string RefreshToken { get; set; }
        [DataMember(Name = "token_type")]
        public string Type { get; set; }
        [DataMember(Name = "expires_in")]
        public long ExpiresIn { get; set; }
        //[DataMember(Name = "scope")]
        //public string Scope { get; set; }
    }
}
