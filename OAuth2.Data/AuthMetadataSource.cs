using System.Collections.Generic;
using Telerik.OpenAccess.Metadata.Fluent;

namespace OAuth2.Data
{
    public class AuthMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            return new List<MappingConfiguration>();
        }
    }
}
