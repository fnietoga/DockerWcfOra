using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KabelLabs.WCF.Entities
{
    public class EMPLOYEEINFOTYPE
    {
        int EMPLOYEE_ID { get; set; }   
		string JOB_ID { get; set; }
        int MANAGER_ID { get; set; }
        int DEPARTMENT_ID { get; set; }
        int LOCATION_ID { get; set; }
        string COUNTRY_ID { get; set; }
        string FIRST_NAME { get; set; }
        string LAST_NAME { get; set; }
        int SALARY { get; set; }
        decimal COMMISSION_PCT { get; set; }
        string DEPARTMENT_NAME { get; set; }
        string JOB_TITLE { get; set; }
        string CITY { get; set; }
        string STATE_PROVINCE { get; set; }
        string COUNTRY_NAME { get; set; }
        string REGION_NAME { get; set; }
    }
}