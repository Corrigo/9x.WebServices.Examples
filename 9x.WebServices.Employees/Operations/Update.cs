using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.Employees.Operations
{
    internal static class Update
    {
        public static void Execute(CorrigoService service, Employee toUpdate)
        {
            if (toUpdate == null || service == null) return;



            toUpdate.Instructions = string.Concat("Instructions updated by WSDK at ",
                $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}");


            var specialties = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Specialty,
                    PropertySet = new AllProperties(),
                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "Id",
                                Operator = ConditionOperator.GreaterThan,
                                Values = new object[] {0}
                            }
                        },
                        FilterOperator = LogicalOperator.Or
                    },
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                });

            Specialty specialty = (specialties != null && specialties.Length > 0) ? specialties[0] as Specialty : null;

            if (specialty != null)
            {
                LinkUserAndSpecialty userAndSpecialty = new LinkUserAndSpecialty
                {
                    SpecialtyId = specialty.Id,
                    UserId = toUpdate.Id
                };

                toUpdate.Specialties = new LinkUserAndSpecialty[] { userAndSpecialty };
            }

            Console.WriteLine($"Updating Employee with id={toUpdate.Id.ToString()}");

            var resultUpdate = service.Execute(new UpdateCommand
            {
                Entity = toUpdate,
                PropertySet = new PropertySet { Properties = new[] { "Instructions", "Specialties.*" } }
            });

            if (resultUpdate == null)
            {
                Console.WriteLine("Update of Employee failed");
                return;
            }

            if (resultUpdate.ErrorInfo != null && !string.IsNullOrEmpty(resultUpdate.ErrorInfo.Description))
            {
                Console.WriteLine(resultUpdate.ErrorInfo.Description);
                Console.WriteLine("Update of Employee failed");
                return;
            }

            Console.WriteLine("Employee is updated");

        }

        public static void Restore(CorrigoService service, Employee toRestore)
        {
            if (toRestore == null || service == null) return;

            Console.WriteLine($"Restoring Employee with id={toRestore.Id.ToString()}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier { Id = toRestore.Id, EntityType = EntityType.Employee }
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of Employee failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of Employee failed");
                return;
            }

            Console.WriteLine("Employee is restored");

        }
    }
}
