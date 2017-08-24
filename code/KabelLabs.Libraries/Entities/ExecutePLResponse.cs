using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KabelLabs.Libraries.Entities
{
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://KabelLabs/Doker/1.0")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://KabelLabs/Doker/1.0", IsNullable = false)]
    public class ExecutePLResponse
    {
        public EMPLOYEEINFOTYPE[] Employees { get; set; }

    }
}