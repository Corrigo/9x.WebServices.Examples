using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.Tasks.Operations
{
	internal static class Create
	{

        public static Task Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating Task");
            Console.WriteLine("Creating Task");

            #region Get Ids for Task required fields: latest existing Model, Priority, Speciality, GLAccount
            var modelList = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Model,
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                    Count = 1
                });

            if (modelList == null || modelList.Length == 0)
            {
                Debug.Print("Creation of new Task failed - no existing Model is found");
                Console.WriteLine("Creation of new Task failed - no existing Model is found");
                return null;
            }

            var priorityList = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.WoPriority,
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                    Count = 1
                });

            if (priorityList == null || priorityList.Length == 0)
            {
                Debug.Print("Creation of new Task failed - no existing priority is found");
                Console.WriteLine("Creation of new Task failed - no existing priority is found");
                return null;
            }

            var specialtyList = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Specialty,
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                    Count = 1
                });

            if (specialtyList == null || specialtyList.Length == 0)
            {
                Debug.Print("Creation of new Task failed - no existing specialty is found");
                Console.WriteLine("Creation of new Task failed - no existing specialty is found");
                return null;
            }

            var accountList = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.GLAccount,
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Ascending,
                            PropertyName = "Id"
                        }
                    },
                    Count = 1
                });

            //if (accountList == null || accountList.Length == 0)
            //{
            //    Debug.Print("Creation of new Task failed - no existing GLaccount is found");
            //    Console.WriteLine("Creation of new Task failed - no existing GLaccount is found");
            //    return null;
            //}


            #endregion

            string timeStamp = $".ByWSDK.{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";

            var toCreate = new Task
            {
                //All fields below are required
                ModelId = modelList[0].Id,
                Priority = new WoPriority { Id = priorityList[0].Id },
                Specialty = new Specialty { Id = specialtyList[0].Id },
                //GlAccount = ((GLAccount)accountList[0]).DisplayAs,
                Preventive = true, //Type is required
                DisplayAs = "Test Task" + timeStamp,
                Instructions = "-",
                SelfHelpContent = "-",
                Number = "TN"+timeStamp
            };

            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new Task failed");
                Console.WriteLine("Creation of new Task failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new Task with Id={id.ToString()}");
                Console.WriteLine($"Created new Task with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new Task failed");
            Console.WriteLine("Creation of new Task failed");
            Console.WriteLine();
            if (resultData.ErrorInfo != null && !string.IsNullOrEmpty(resultData.ErrorInfo.Description))
            {
                Debug.Print(resultData.ErrorInfo.Description);
                Console.WriteLine(resultData.ErrorInfo.Description);
            }


            return null;
        }
    }
}
