using Newtonsoft.Json;

namespace TexasHoldEm.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenRequestViewModel
    {
        public TokenRequestViewModel() { }
        public string username { get; set; }
        public string password { get; set; }
    }
}
