using Microsoft.VisualStudio.TestTools.UnitTesting;
using KabelLabs.WCF.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KabelLabs.WCF.Gateways.Tests
{
    [TestClass()]
    public class OracleGatewayTests
    {
        [TestMethod()]
        public void LISTEMPLOYEES_null_Test()
        {
            Libraries.Gateways.OracleGateway gateway = new Libraries.Gateways.OracleGateway();
            Libraries.Entities.EMPLOYEEINFOTYPE[] result = gateway.LISTEMPLOYEES(null, null, null);


            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].CITY));
            Assert.IsTrue(result[0].COMMISSION_PCT > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].COUNTRY_ID));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].COUNTRY_NAME));
            Assert.IsTrue(result[0].DEPARTMENT_ID > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].DEPARTMENT_NAME));
            Assert.IsTrue(result[0].EMPLOYEE_ID > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].FIRST_NAME));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].JOB_ID));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].JOB_TITLE));
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].LAST_NAME));
            Assert.IsTrue(result[0].LOCATION_ID > 0);
            Assert.IsTrue(result[0].MANAGER_ID > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].REGION_NAME));
            Assert.IsTrue(result[0].SALARY > 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(result[0].STATE_PROVINCE));
            Assert.Fail();
        }
    }
}