using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KabelLabs.Libraries.Entities
{
    public class EMPLOYEEINFOTYPE
    {
        public int EMPLOYEE_ID { get; set; }
        public string JOB_ID { get; set; }
        public int MANAGER_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int LOCATION_ID { get; set; }
        public string COUNTRY_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public int SALARY { get; set; }
        public decimal COMMISSION_PCT { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string JOB_TITLE { get; set; }
        public string CITY { get; set; }
        public string STATE_PROVINCE { get; set; }
        public string COUNTRY_NAME { get; set; }
        public string REGION_NAME { get; set; }
    }
}