using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using Dapper;
using OAuth2.Common;

namespace OAuth2.API.Repositories
{
    public class DBAuthentication
    {
        private string ConnectionString;
        public DBAuthentication()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["AuthConnection"].ConnectionString;
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string strSql, object sqlParms = null, SqlTransaction sqlTrans = null)
        {
            using (var sqlConn = (sqlTrans == null) ? new SqlConnection(ConnectionString) : sqlTrans.Connection)
            {
                try
                {
                    if (sqlConn.State != ConnectionState.Open)
                        await sqlConn.OpenAsync();
                    return await sqlConn.QueryAsync<T>(strSql, sqlParms, sqlTrans);
                }
                finally
                {
                    if (sqlTrans != null)
                        sqlConn.Close();
                }
            }
        }

        public async Task<bool> ExecuteAsync(string strSql, object sqlParms = null, SqlTransaction sqlTrans = null)
        {
            using (var sqlConn = (sqlTrans == null) ? new SqlConnection(ConnectionString) : sqlTrans.Connection)
            {
                try
                {
                    if (sqlConn.State != ConnectionState.Open)
                        await sqlConn.OpenAsync();
                    return await AsyncHelper.RunAsynchronously<bool>(() =>
                    {
                        var affectedRows = sqlConn.Execute(strSql, sqlParms, sqlTrans);
                        return affectedRows > 0;
                    });
                }
                finally
                {
                    if (sqlTrans != null)
                        sqlConn.Close();
                }
            }
        }
    }
}
