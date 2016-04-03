using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Data.Authentication.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLiftTime { get; set; }
        public string AllowedOrigin { get; set; }
    }

    public enum ApplicationTypes
    {
        JavaScript = 0,
        Native = 1
    }
}
