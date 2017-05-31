using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.Employees.Operations;


namespace _9x.WebServices.Employees
{
    public static class EmployeeExamples
    {


        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {
            return Read.RetrieveMultiple(service);
        }

        //public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        //{
        //    return Read.RetrieveByQuery(service);
        //}

        public static void CRUDExample(CorrigoService service, bool isCreateUpdateDelete = false)
        {
            if (service == null) return;

            Employee user = isCreateUpdateDelete? Create.Execute(service) : null;
            
            if (user != null)
            {
                    Read.Retrieve(service, user.Id);

                    Update.Execute(service, user);

                    Read.Retrieve(service, user.Id);

                    Delete.Execute(service, user.Id);

                    Update.Restore(service, user);
            }

            Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);
        }
    }

}
