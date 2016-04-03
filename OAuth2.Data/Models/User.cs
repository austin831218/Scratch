using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;

namespace OAuth2.Data.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        //public RefreshToken RefreshToken { get; set; }

        public static MappingConfiguration GetMapping()
        {
            var mapping = new MappingConfiguration<User>();
            mapping.MapType(user => new
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Password = user.Password
            }).ToTable("User");

            mapping.HasProperty(user => user.UserId).IsIdentity(KeyGenerator.Autoinc);
            mapping.HasProperty(user => user.UserName).HasLength(50);
            mapping.HasProperty(user => user.Password).HasLength(50);

            return mapping;
        }
    }
}
