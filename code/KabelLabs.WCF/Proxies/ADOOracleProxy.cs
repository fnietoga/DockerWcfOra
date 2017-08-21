using System.Collections.Generic;
using System.Data;
using System.Collections.Specialized;
using System;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

//using ONO.ServicesPlatform.Common.Libraries.Settings;
//using ONO.ServicesPlatform.Common.Libraries.ErrorCodes;

namespace KabelLabs.WCF.Proxies
{
    public interface IADOOracleProxy {

        OracleParameter CreateInt32Parameter(string name, ParameterDirection direction, object value);
        OracleParameter CreateDecimalParameter(string name, ParameterDirection direction, object value);
        OracleParameter CreateArrayParameter(string name, ParameterDirection direction, string udtTypeName, object value);
        OracleParameter CreateObjectParameter(string name, ParameterDirection direction, string udtTypeName, object value);
        OracleParameter CreateDateParameter(string name, ParameterDirection direction, object value);
        OracleParameter CreateCharParameter(string name, ParameterDirection direction, int? size, object value);
        OracleParameter CreateVarchar2Parameter(string name, ParameterDirection direction, int? size, object value);
        OracleParameter CreateClobParameter(string name, ParameterDirection direction, object value); 
        OracleParameter CreateCursorParameter(string name, ParameterDirection direction, object value); 
        void LoadParameters(params OracleParameter[] parameters);
        T[] DeserializeFromNameValueCollection<T>(List<NameValueCollection> lnameValueCollection) where T : new();
        IDictionary<string, object> ExecuteProcedure();
        T ExecuteProcedure<T>(string paramName);
        void ExecuteNonQuery();
        T ExecuteScalar<T>();
        object ExecuteScalar();

    }


    public class ADOOracleProxy : IADOOracleProxy
    {
        #region Private members
        private OracleConnection conn;
        private OracleCommand cmd;
        #endregion

        #region Constructors	
        public ADOOracleProxy(string connectionName, string commandText)
        {
            conn = new OracleConnection(getConnectionString(connectionName));

            cmd = new OracleCommand()
            {
                Connection = conn,
                CommandText = commandText,
                CommandType = CommandType.StoredProcedure
            };
        }

        public ADOOracleProxy(string connectionName, string commandText, CommandType commandType)
        {
            conn = new OracleConnection(getConnectionString(connectionName));
            cmd = new OracleCommand()
            {
                Connection = conn,
                CommandText = commandText,
                CommandType = commandType
            };
        }

        public ADOOracleProxy(string connectionName, string commandText, CommandType commandType, bool bindByName)
        {
            conn = new OracleConnection(getConnectionString(connectionName));
            cmd = new OracleCommand()
            {
                Connection = conn,
                CommandText = commandText,
                CommandType = commandType,
                BindByName = bindByName
            };
        }

        private string getConnectionString(string connectionName)
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings[connectionName];
            return connection?.ConnectionString;
        }

        #endregion

		#region Parameters Creation

        public OracleParameter CreateInt32Parameter(string name, ParameterDirection direction, object value)
        {
            return CreateParameter(OracleDbType.Int32, name, direction, null, null, value);
        }

        public OracleParameter CreateDecimalParameter(string name, ParameterDirection direction, object value)
        {
            return CreateParameter(OracleDbType.Decimal, name, direction, null, null, value);
        }

        public OracleParameter CreateArrayParameter(string name, ParameterDirection direction, string udtTypeName, object value)
        {
            return CreateParameter(OracleDbType.Array, name, direction, udtTypeName, null, value);
        }

        public OracleParameter CreateObjectParameter(string name, ParameterDirection direction, string udtTypeName, object value)
        {
            return CreateParameter(OracleDbType.Object, name, direction, udtTypeName, null, value);
        }

        public OracleParameter CreateDateParameter(string name, ParameterDirection direction, object value)
        {
            return CreateParameter(OracleDbType.Date, name, direction, null, null, value);
        }

        public OracleParameter CreateCharParameter(string name, ParameterDirection direction, int? size, object value)
        {
            return CreateParameter(OracleDbType.Char, name, direction, null, size, value);
        }

        public OracleParameter CreateVarchar2Parameter(string name, ParameterDirection direction, int? size, object value)
        {
            return CreateParameter(OracleDbType.Varchar2, name, direction, null, size, value);
        }

        public OracleParameter CreateClobParameter(string name, ParameterDirection direction, object value)
        {
            return CreateParameter(OracleDbType.Clob, name, direction, null, null, value);
        }

        public OracleParameter CreateCursorParameter(string name, ParameterDirection direction, object value)
        {
            return CreateParameter(OracleDbType.RefCursor, name, direction, null, null, value);
        }

        public OracleParameter CreateOutputCursorParameter(string name)
        {
            OracleParameter parameter = new OracleParameter();
            parameter.OracleDbType = OracleDbType.RefCursor;
            parameter.ParameterName = name;
            parameter.Direction = ParameterDirection.Output;

            return parameter;
        }

        private OracleParameter CreateParameter(OracleDbType type,
            string name, ParameterDirection direction, string udtTypeName, int? size, object value)
        {
            OracleParameter oraParam = new OracleParameter();
            oraParam.OracleDbType = type;
            oraParam.ParameterName = name;
            oraParam.Direction = direction;
            if (!string.IsNullOrEmpty(udtTypeName))
                oraParam.UdtTypeName = udtTypeName;
            if (size.HasValue)
                oraParam.Size = size.Value;
            if (value != null)
                oraParam.Value = value;
            return oraParam;
        }

        public void LoadParameters(params OracleParameter[] parameters)
        {
            if (cmd != null)
                foreach (OracleParameter parameter in parameters)
                    cmd.Parameters.Add(parameter);
        }

		 #endregion

        #region Execute
		public T[] DeserializeFromNameValueCollection<T>(List<NameValueCollection> lnameValueCollection) where T : new()
        {
            T[] lresult = null;


            if (lnameValueCollection.Count > 0)
                lresult = new T[lnameValueCollection.Count];

            Type type = typeof(T);
            //obtenemos los tipos de los campos:

            var properties = type.GetProperties();
            int i = 0;
            foreach (NameValueCollection nameValueCollection in lnameValueCollection)
            {

                T result = new T();
                foreach (var property in properties)
                {
                    string key = property.Name;
                    string stringValue = nameValueCollection[key];

                    object value = null;

                    var baseType = Nullable.GetUnderlyingType(property.PropertyType);
                    if (baseType != null)
                    {
                        if (stringValue != null)
                            value = Convert.ChangeType(stringValue, baseType);
                        else
                            value = null;
                    }
                    else
                    {
                        try
                        {
                            value = Convert.ChangeType(stringValue, property.PropertyType);
                        }
                        catch (Exception) { }
                    }

                    try
                    {
                        property.SetValue(result, value, null);
                    }
                    catch (Exception) { }

                }

                lresult[i] = result;

                i++;
            }
            return lresult;
        }
        

        public IDictionary<string, object> ExecuteProcedure()
        {
            IDictionary<string, object> result = new Dictionary<string, object>();

            using (conn)
            {
                try
                {
                    conn.Open();

                    cmd.ExecuteNonQuery();

                    foreach (OracleParameter param in cmd.Parameters)
                    {
                        if (param.Direction != ParameterDirection.Input)
                        {
                            if (param.OracleDbType == OracleDbType.RefCursor)
                            {
                                OracleRefCursor cursorRef = param.Value as OracleRefCursor;
                                if (cursorRef != null && !cursorRef.IsNull)
                                {
                                    using (cursorRef)
                                    {
                                        List<NameValueCollection> rows = new List<NameValueCollection>();

                                        using (OracleDataReader reader = cursorRef.GetDataReader())
                                        {
                                            while (reader.Read())
                                            {
                                                NameValueCollection row = new NameValueCollection();

                                                for (int i = 0; i < reader.FieldCount; i++)
                                                    row.Add(reader.GetName(i), reader.IsDBNull(i) ? null : Convert.ToString(reader.GetValue(i)));

                                                rows.Add(row);
                                            }

                                            reader.Close();
                                        }

                                        result.Add(param.ParameterName, rows);
                                    }
                                }
                            }
                            else if (param.DbType == DbType.String || param.DbType == DbType.StringFixedLength
                                || param.DbType == DbType.AnsiString || param.DbType == DbType.AnsiStringFixedLength)
                            {
                                if (!((OracleString)param.Value).IsNull)
                                    result.Add(param.ParameterName, ((OracleString)param.Value).Value);
                            }
                            else if (param.OracleDbType == OracleDbType.Clob)
                            {
                                //Si Clob, es necesario utilizar la propiedad Value para recuperar el string antes de cerrar la conexión.
                                //Obs: El Value de param devuelve el objeto Clob, por eso el cast y el uso del Value del objeto Clob.
                                if (!((OracleClob)param.Value).IsNull)
                                    result.Add(param.ParameterName, ((OracleClob)param.Value).Value);
                            }
                            else
                                result.Add(param.ParameterName, param.Value);
                        }
                    }

                    conn.Close();
                }
                catch (OracleException ex)
                {
                    //throw ErrorCodeMng.GetOracleException(ex.Number, ex.Message);
                    throw ex;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    if (conn != null && conn.State != ConnectionState.Closed)
                    {
                        try
                        {
                            conn.Close();
                        }
                        catch (Exception) { }
                    }
                }
            }

            return result;
        }

        public T ExecuteProcedure<T>(string paramName)
        {
            ExecuteNonQuery();

            return (T)cmd.Parameters[paramName].Value;
        }

        public void ExecuteNonQuery()
        {
            using (conn)
            {
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception)
                {
                    if (conn != null && conn.State != ConnectionState.Closed)
                    {
                        try
                        {
                            conn.Close();
                        }
                        catch (Exception) { }
                    }

                    throw;
                }
            }
        }

        //public List<List<object>> ExecuteReader()
        //{
        //    List<List<object>> results = null;

        //    using (conn)
        //    {
        //        try
        //        {
        //            conn.Open();

        //            using (var reader = cmd.ExecuteReader())
        //            {
        //                results = new List<List<object>>();

        //                while (reader.Read())
        //                {
        //                    List<object> row = new List<object>();

        //                    for (int column = 0; column < reader.FieldCount; column++)
        //                    {
        //                        row.Add(reader.GetValue(column));
        //                    }

        //                    results.Add(row);
        //                }
        //            }
        //            conn.Close();
        //        }                 
        //        finally
        //        {
        //            if (conn != null && conn.State != ConnectionState.Closed)
        //            {
        //                try
        //                {
        //                    conn.Close();
        //                }
        //                catch (Exception) { }
        //            }
        //        }
        //    }

        //    return results;
        //}
        public T ExecuteScalar<T>()
        {
            object dbResult = ExecuteScalar();

            return (dbResult == null) ? default(T) : (T)dbResult;
        }

        public object ExecuteScalar()
        {
            object retVal = null;

            using (conn)
            {
                try
                {
                    conn.Open();
                    retVal = cmd.ExecuteScalar();
                    conn.Close();
}
                catch (Exception)
                {
                    if (conn != null && conn.State != ConnectionState.Closed)
                    {
                        try
                        {
                            conn.Close();
                        }
                        catch (Exception) { }
                    }

                    throw;
                }
            }

            return retVal;
        }

		#endregion
    }
}
