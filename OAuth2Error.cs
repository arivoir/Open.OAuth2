using System.Runtime.Serialization;

namespace Open.OAuth2
{
    [DataContract]
    public class OAuth2Error
    {
        [DataMember(Name = "error")]
        public string Error { get; set; }
        [DataMember(Name = "error_description")]
        public string ErrorDescription { get; set; }
    }
}
