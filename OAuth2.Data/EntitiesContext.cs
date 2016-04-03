using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using OAuth2.Data.Models;

namespace OAuth2.Data
{
    public class EntitiesContext : OpenAccessContext, IUnitOfWork
    {
        static MetadataContainer metadataContainer = new AuthMetadataSource().GetModel();
        static BackendConfiguration backend = new BackendConfiguration()
        {
            Backend = "mssql"
        };
        private const string connectionStringName = @"server=.\sqlexpress;database=AuthTest;Integrated Security=True;";
        public EntitiesContext()
            : base(connectionStringName, backend, metadataContainer)
        {
        }

        public EntitiesContext(string connection)
            : base(connection, backend, metadataContainer)
        { }

        public EntitiesContext(BackendConfiguration backendConfiguration)
            : base(connectionStringName, backendConfiguration, metadataContainer)
        { }

        public EntitiesContext(string connection, MetadataSource metadataSource)
            : base(connection, backend, metadataSource)
        { }

        public EntitiesContext(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
            : base(connection, backendConfiguration, metadataSource)
        { }

        public IQueryable<User> Users
        {
            get
            {
                return this.GetAll<User>();
            }
        }

        public IQueryable<Client> Clients
        {
            get
            {
                return this.GetAll<Client>();
            }
        }
        public IQueryable<RefreshToken> RefreshTokens
        {
            get
            {
                return this.GetAll<RefreshToken>();
            }
        }
    }

    public interface IEntitiesContextUnitOfWork: IUnitOfWork
    {
        IQueryable<User> Users
        {
            get;
        }

        IQueryable<Client> Clients
        {
            get;
        }
        IQueryable<RefreshToken> RefreshTokens
        {
            get;
        }
    }
}
