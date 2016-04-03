using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Data
{
    public class DBUtility
    {
        public static void Migration(string connectionName)
        {
            using (EntitiesContext dbContext = new EntitiesContext(connectionName))
            {
                Telerik.OpenAccess.ISchemaHandler schemaHandler = dbContext.GetSchemaHandler();
                string script = null;
                if (schemaHandler.DatabaseExists())
                {
                    script = schemaHandler.CreateUpdateDDLScript(null);
                }
                else
                {
                    schemaHandler.CreateDatabase();
                    script = schemaHandler.CreateDDLScript();
                }
                if (!string.IsNullOrEmpty(script))
                {
                    schemaHandler.ExecuteDDLScript(script);
                }
            }
        }
    }
}
