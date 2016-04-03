using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace OAuth2.Data.Models
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }
        public string ProtectedTicket { get; set; }

        public static MappingConfiguration GetMapping()
        {
            var mapping = new MappingConfiguration<RefreshToken>();
            mapping.MapType(token => new
            {
                Id = token.Id,
                //UserId = token.UserId,
                //ClientId = token.ClientId,
                IssuedUtc = token.IssuedUtc,
                ExpiresUtc = token.ExpiresUtc,
                ProtectedTicket = token.ProtectedTicket
            }).ToTable("RefreshToken");

            mapping.HasAssociation(token => token.User).ToColumn("UserId");
            mapping.HasAssociation(token => token.Client).ToColumn("ClientId");

            return mapping;
        }
    }
}
