using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KabelLabs.WCF.Entities
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://KabelLabs/Doker/1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://KabelLabs/Doker/1.0", IsNullable = false)]
    public class ExecutePLRequest
    {
        public int EMPLOYEE_ID { get; set; }

        public bool EMPLOYEE_IDSpecified { get; set; }

        public string COUNTRY_NAME { get; set; }
        public string DEPARTMENT_NAME { get; set; }

    }
}