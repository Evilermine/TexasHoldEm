using Newtonsoft.Json;

namespace TexasHoldEm.Models
{
    [JsonObject(MemberSerialization.OptOut)]
    public class UserCredentialsViewModel
    {
        public UserCredentialsViewModel() { }
        public string username { get; set; }
        public string password { get; set; }
    }
}
