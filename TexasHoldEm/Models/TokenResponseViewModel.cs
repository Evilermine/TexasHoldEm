using Newtonsoft.Json;

namespace TexasHoldEm.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenResponseViewModel  {

        public TokenResponseViewModel() { }
  
        public string token { get; set; }
        public int expiration { get; set; }
    }
}