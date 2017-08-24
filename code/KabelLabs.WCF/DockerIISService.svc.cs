using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace KabelLabs.WCF
{
     public class DockerIISService : IDockerIISService
    {
        private Libraries.Gateways.OracleGateway _gateway;

        public DockerIISService() : this(new Libraries.Gateways.OracleGateway()) { }         
     
         public DockerIISService(Libraries.Gateways.OracleGateway gateway) {
            this._gateway = gateway;
        }


        public Entities.ExecutePLSoapOut ExecutePL(Entities.ExecutePLSoapIn request) 
        {
            Entities.ExecutePLSoapOut response = new Entities.ExecutePLSoapOut();

            if (request == null)
                throw new ArgumentNullException("request");
            if (request.ExecutePLRequest ==null )
                throw new ArgumentNullException("request.ExecutePLRequest");

            if (!request.ExecutePLRequest.EMPLOYEE_IDSpecified
                && string.IsNullOrWhiteSpace(request.ExecutePLRequest.COUNTRY_NAME)
                && string.IsNullOrWhiteSpace(request.ExecutePLRequest.DEPARTMENT_NAME))
            {
                throw new ArgumentException("Debe especificar al menos un valor de filtrado");
            }

            response.ExecutePLResponse = new Libraries.Entities.ExecutePLResponse();
            response.ExecutePLResponse.Employees = _gateway.LISTEMPLOYEES(
                request.ExecutePLRequest.EMPLOYEE_IDSpecified ? request.ExecutePLRequest.EMPLOYEE_ID : null as int?,
                request.ExecutePLRequest.COUNTRY_NAME,
                request.ExecutePLRequest.DEPARTMENT_NAME
                ); 

            return response;
        }

       
    }
}
