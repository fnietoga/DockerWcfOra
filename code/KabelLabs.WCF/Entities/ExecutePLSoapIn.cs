using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;

namespace KabelLabs.WCF.Entities
{
    public class ExecutePLSoapIn
    {
        [MessageBodyMemberAttribute(Namespace = "http://KabelLabs/Doker/1.0", Order = 0)]
        public ExecutePLRequest ExecutePLRequest;

    }
}