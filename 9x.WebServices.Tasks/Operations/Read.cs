using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Linq;

namespace _9x.WebServices.Tasks.Operations
{
	internal static class Read
	{

        public static Task Retrieve(CorrigoService service, int id)
        {
            Console.WriteLine();
            Console.WriteLine($"Retrieve Task with id={id}");
            CorrigoEntity result = null;

            try
            {
                result = service.Retrieve(
                    new EntitySpecifier
                    {
                        EntityType = EntityType.Task,
                        Id = id
                    },
                    new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ModelId",
                            "DisplayAs",
                            "Preventive",
                            "Routine",
                            "Corrective",
                            "Default",
                            "Symptom",
                            "CompletionTime",
                            "Specialty.*",
                            "Priority.*",
                            "SelfHelpType",
                            "Instructions",
                            "SelfHelpContent",
                            "PeopleRequired",
                            "SkillLevel",
                            "GlAccount",
                            "Number",
                            "Currencies.*"
                        }
                    }
                //new AllProperties()
                );
            }
            catch (Exception e)
            {
                if (!string.IsNullOrEmpty(e.Message)) Console.WriteLine(e.Message);
            }

            if (result == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            Task toReturn = result as Task;

            if (toReturn == null)
            {
                Console.WriteLine("Retrieve failed");
                return null;
            }

            int padRightNumber = 35;

            Console.WriteLine(string.Concat("Task.Id=".PadRight(padRightNumber), toReturn.Id.ToString()));
            Console.WriteLine(string.Concat("Task.ModelId=".PadRight(padRightNumber), toReturn.ModelId.ToString()));

            var model = service.Retrieve(new EntitySpecifier { EntityType = EntityType.Model, Id = toReturn.ModelId }, new AllProperties()) as Model;
            if (model != null) Console.WriteLine(string.Concat("Model.Name= ".PadRight(padRightNumber), $"'{model.DisplayAs}'"));

            Console.WriteLine(string.Concat("Task.DisplayAs=".PadRight(padRightNumber), toReturn.DisplayAs ?? ""));
            Console.WriteLine(string.Concat("Task.Preventive=".PadRight(padRightNumber), toReturn.Preventive.ToString()));
            Console.WriteLine(string.Concat("Task.Routine=".PadRight(padRightNumber), toReturn.Routine.ToString()));
            Console.WriteLine(string.Concat("Task.Corrective=".PadRight(padRightNumber), toReturn.Corrective.ToString()));
            Console.WriteLine(string.Concat("Task.Default=".PadRight(padRightNumber), toReturn.Default.ToString()));
            Console.WriteLine(string.Concat("Task.Symptom=".PadRight(padRightNumber), toReturn.Symptom.ToString()));
            Console.WriteLine(string.Concat("Task.CompletionTime=".PadRight(padRightNumber), toReturn.CompletionTime.ToString()));

            if (toReturn.Specialty != null)
            {
                Console.WriteLine(string.Concat("Task.Specialty.Id=".PadRight(padRightNumber), toReturn.Specialty.Id.ToString()));
                Console.WriteLine(string.Concat("Task.Specialty.DisplayAs=".PadRight(padRightNumber), toReturn.Specialty.DisplayAs ?? ""));
                Console.WriteLine(string.Concat("Task.Specialty.Instructions=".PadRight(padRightNumber), toReturn.Specialty.Instructions));
            }

            if (toReturn.Priority != null)
            {
                Console.WriteLine(string.Concat("Task.Priority.Id=".PadRight(padRightNumber), toReturn.Priority.Id.ToString()));
                Console.WriteLine(string.Concat("Task.Priority.DisplayAs=".PadRight(padRightNumber), toReturn.Priority.DisplayAs ?? ""));
            }

            if (toReturn.SelfHelpType != null)
            {
                Console.WriteLine(string.Concat("Task.SelfHelpType=".PadRight(padRightNumber), toReturn.SelfHelpType.ToString()));                
            }

            Console.WriteLine(string.Concat("Task.Instructions=".PadRight(padRightNumber), toReturn.Instructions ?? ""));
            Console.WriteLine(string.Concat("Task.SelfHelpContent=".PadRight(padRightNumber), toReturn.SelfHelpContent ?? ""));
            Console.WriteLine(string.Concat("Task.PeopleRequired=".PadRight(padRightNumber), toReturn.PeopleRequired.ToString()));
            Console.WriteLine(string.Concat("Task.SkillLevel=".PadRight(padRightNumber), toReturn.SkillLevel.ToString()));
           
            Console.WriteLine(string.Concat("Task.GlAccount=".PadRight(padRightNumber), toReturn.GlAccount ?? ""));
            Console.WriteLine(string.Concat("Task.Number=".PadRight(padRightNumber), toReturn.Number ?? ""));

            if (toReturn.Currencies != null)
            {
                for (int i = 0; i < toReturn.Currencies.Length; i++)
                {
                    Console.WriteLine(string.Concat($"Task.Currencies[{i}].Nte=".PadRight(padRightNumber), toReturn.Currencies[i].Nte?.ToString() ?? ""));
                }
            }

            Console.WriteLine();

            return toReturn;
        }

        public static CorrigoEntity[] RetrieveMultiple(CorrigoService service)
        {

            var models = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.Model,
                    PropertySet = new AllProperties(),
                    Conditions = new PropertyValuePair[0]
                });


            var list = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.Task,
                    PropertySet = new AllProperties(),
                    Conditions = new PropertyValuePair[0],
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Ascending,
                            PropertyName = "ModelId"
                        },
                        new OrderExpression
                        {
                            OrderType = OrderType.Ascending,
                            PropertyName = "Id"
                        }
                    },
                });
            Console.WriteLine("Tasks: Retrieve Multiple");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(6), "|", "Id".PadLeft(6), "|", "ModelId".PadLeft(6), "|", "Model Name".PadRight(30), "|", "DisplayAs".PadRight(50), "|", "Instructions".PadRight(25), "|", "GlAccount".PadRight(15), "|", "Number".PadRight(10)));

            int i = 0;
            foreach (Task item in list)
            {
                var model = models.Where(m => m.Id == item.ModelId).ToArray();
                string modelName = (model.Length == 1) ? ((Model) model[0]).DisplayAs : "";

                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(6), "|", item.Id.ToString().PadLeft(6), "|", item.ModelId.ToString().PadLeft(6), "|", modelName.PadRight(30), "|", item.DisplayAs.PadRight(50), "|", item.Instructions.PadLeft(25), "|", item.GlAccount.PadLeft(15), "|", item.Number.ToString().PadLeft(10)));
            }

            Console.WriteLine();
            return list;
        }
        
        public static CorrigoEntity[] RetrieveByQuery(CorrigoService service)
        {

            Console.WriteLine();
            

            var models = service.RetrieveMultiple(
                new QueryByProperty
                {
                    EntityType = EntityType.Model,
                    PropertySet = new AllProperties(),
                    Conditions = new PropertyValuePair[0],
                    Count = 1,
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Descending,
                            PropertyName = "Id"
                        }
                    },
                });

            if (models == null || models.Length == 0) return null;

            Model model = (Model) models[0];
            Console.WriteLine($"Retrieving Tasks for Model '{model.DisplayAs}' with Id={model.Id}");

            var list = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.Task,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                            "ModelId",
                            "DisplayAs",
                            "Preventive",
                            "Routine",
                            "Corrective",
                            "Default",
                            "Symptom",
                            "CompletionTime",
                            "Specialty.*",
                            "Priority.*",
                            "SelfHelpType",
                            "Instructions",
                            "SelfHelpContent",
                            "PeopleRequired",
                            "SkillLevel",
                            "GlAccount",
                            "Number",
                            "Currencies.*"
                        }
                    },
                    //PropertySet = new AllProperties(),

                    Criteria = new FilterExpression
                    {
                        Conditions = new ConditionExpression[]
                        {
                            new ConditionExpression
                            {
                                PropertyName = "ModelId",
                                Operator = ConditionOperator.In,
                                Values = new Object[]{ model.Id }
                            }
                        },
                        FilterOperator = LogicalOperator.Or
                    },
                    Orders = new[]
                    {
                        new OrderExpression
                        {
                            OrderType = OrderType.Ascending,
                            PropertyName = "Id"
                        }
                    },
                });
            Console.WriteLine();
            Console.WriteLine($"Tasks: Retrieve by query");

            if (list == null || list.Length == 0) return list;

            Console.WriteLine(string.Concat("#".PadLeft(6), "|", "Id".PadLeft(6), "|", "ModelId".PadLeft(6), "|", "Model Name".PadRight(30), "|", "DisplayAs".PadRight(50), "|", "Instructions".PadRight(25), "|", "GlAccount".PadRight(15), "|", "Number".PadRight(10)));

            int i = 0;
            foreach (Task item in list)
            {                                
                i++;
                Console.WriteLine(string.Concat(i.ToString().PadLeft(6), "|", item.Id.ToString().PadLeft(6), "|", item.ModelId.ToString().PadLeft(6), "|", model.DisplayAs.PadRight(30), "|", item.DisplayAs.PadRight(50), "|", item.Instructions.PadLeft(25), "|", item.GlAccount.PadLeft(15), "|", item.Number.ToString().PadLeft(10)));
            }

            Console.WriteLine();
            return list;
        }

    }
}
