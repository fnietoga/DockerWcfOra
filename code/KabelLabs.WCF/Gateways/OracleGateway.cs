using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Web;

namespace KabelLabs.WCF.Gateways{
   
    public interface IOracleGateway
    {
        Entities.EMPLOYEEINFOTYPE[] LISTEMPLOYEES(int? i_EMPLOYEE_ID, string i_COUNTRY_NAME, string i_DEPARTMENT_NAME);
    }

    public class OracleGateway: IOracleGateway
    {
        public const string ORACLE_CONNECTIONSTRING_NAME = "OracleHRConnString";

        public OracleGateway() { }

        public Entities.EMPLOYEEINFOTYPE[] LISTEMPLOYEES(int? strEMPLOYEE_ID, string strCOUNTRY_NAME, string strDEPARTMENT_NAME) {

            Entities.EMPLOYEEINFOTYPE[] retVal = null;
            try
            {
                Proxies.ADOOracleProxy cliente = new Proxies.ADOOracleProxy(ORACLE_CONNECTIONSTRING_NAME, "PKG_HR_EMPLOYEES.P_LISTEMPLOYEES");
                cliente.LoadParameters(
                      cliente.CreateVarchar2Parameter("i_EMPLOYEE_ID", ParameterDirection.Input, 250, strEMPLOYEE_ID),
                      cliente.CreateVarchar2Parameter("i_COUNTRY_NAME", ParameterDirection.Input, 250, strCOUNTRY_NAME),
                      cliente.CreateVarchar2Parameter("i_DEPARTMENT_NAME", ParameterDirection.Input, 250, strDEPARTMENT_NAME),
                      cliente.CreateCursorParameter("o_EMPLOYEES", ParameterDirection.Output, null)
                      );

                IDictionary<string, object> result = cliente.ExecuteProcedure();
                if (result != null)
                {
                    //Extrae el contenido del cursor a un array por reflection
                    List<NameValueCollection> linfoEmployees = result.ContainsKey("o_EMPLOYEES") ? (List<NameValueCollection>)result["o_EMPLOYEES"] : null;
                    retVal =  cliente.DeserializeFromNameValueCollection<Entities.EMPLOYEEINFOTYPE>(linfoEmployees);
                }
            }
            catch (Exception ex)//OracleException ex
            {
                //throw ErrorCodeMng.GetOracleException(ex.Number, ex.Message);
                throw ex;
            }

            return retVal;
        }
    }
}