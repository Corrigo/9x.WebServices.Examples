using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Security.Cryptography;

namespace _9x.WebServices.WorkZones.Operations
{
    internal static class Create
    {
        public static WorkZone Execute(CorrigoService service)
        {
            Console.WriteLine();
            Debug.Print("Creating WorkZone");
            Console.WriteLine("Creating WorkZone");

            //
            // Get Asset Template Id by its name.
            // Template name can be spotted by navigation Url: https://v91.qa.corrigo.com/corpnet/assets/templatemanager.aspx
            //
            string assetTemplateName = "SK_WrokZoneTemplate";

            var assetTemplates = service.RetrieveMultiple(
                new QueryByProperty
                {
                    Count = 1,
                    EntityType = EntityType.AssetTemplate,
                    PropertySet = new PropertySet
                    {
                        Properties = new string[]
                        {
                            "Id",
                        }
                    },
                    Conditions = new[]
                    {
                        new PropertyValuePair {PropertyName = "DisplayAs", Value = assetTemplateName},
                    },
                });


            WorkZone toCreate = new WorkZone
            {
                DisplayAs = $"Test {DateTime.Now}",
                Number = $"Test {DateTime.Now}",
                WoNumberPrefix = Guid.NewGuid().ToString().Split('-')[0],
                TimeZone = 4, //(GMT-08:00) Pacific Time (US & Canada) 
                AutoAssignEnabled = true,
                DefaultAccess = PTEType.Unknown
            };


            var resultData = service.Execute(new WorkZoneCreateCommand
            {
                AssetTemplateId = assetTemplates[0].Id,
                WorkZone = toCreate,
                SkipDefaultSettings = false
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new WorkZone failed");
                Console.WriteLine("Creation of new WorkZone failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as WorkZoneCommandResponse;

            int? id = commandResponse?.WorkZone?.Id;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new WorkZone with Id={id}");
                Console.WriteLine($"Created new WorkZone with Id={id}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new WorkZone failed");
            Console.WriteLine("Creation of new WorkZone failed");
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
