using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace OAuth2.Data.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLiftTime { get; set; }
        public string AllowOrigin { get; set; }

        public static MappingConfiguration GetMapping()
        {
            var mapping = new MappingConfiguration<Client>();

            mapping.MapType(client => new
            {
                Id = client.Id,
                Secret = client.Secret,
                Name = client.Name,
                ApplicationType = client.ApplicationType,
                Active = client.Active,
                RefreshTokenLiftTime = client.RefreshTokenLiftTime,
                AllowOrigin = client.AllowOrigin
            }).ToTable("Client");
            
            mapping.HasProperty(client => client.Id).HasLength(50);
            mapping.HasProperty(client => client.Name).HasLength(50);


            return mapping;
        }
    }

    public enum ApplicationTypes
    {
        JavaScript = 0,
        Native = 1
    }
}
