using _9x.WebServices.WoActionLogs;
using CorrigoServiceWebReference.CorrigoGA;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace _9x.WebServices.Documents.Operations
{
    internal static class Create
    {

        public static Document Execute(CorrigoService service, DocumentCreateParams createParams)
        {
            if (service == null || createParams == null) return null;

            Console.WriteLine();
            Debug.Print("Creating Document");
            Console.WriteLine("Creating Document");

            //Get latest WO
            CorrigoEntity[] latestWO = WoActionLogExamples.GetLatestWOs(service, 1);
            if (latestWO.Length != 1)
            {
                Console.WriteLine("Can not create Document because no existing Work Order was found");
                return null;
            }

            //Get document types
            var documentTypes = service.RetrieveMultiple(
                new QueryExpression
                {
                    EntityType = EntityType.DocumentType,
                    PropertySet = new AllProperties(),
                });

            var docType = documentTypes.Where(t => ((DocumentType)t).DisplayAs == createParams.DocType).SingleOrDefault();

            var toCreate = new Document
            {
                ActorId = latestWO[0].Id,
                ActorTypeId = ActorType.WO,
                Description = createParams.Description,
                Title = createParams.Title,
                DocType = new DocumentType {Id = docType.Id},
                MimeType = createParams.MimeType,
                StorageTypeId = createParams.IsLink? DocumentStorageType.URL : DocumentStorageType.Local 
            };

            toCreate.DocUrl = createParams.IsLink ? toCreate.Description : toCreate.Title;

            if (!string.IsNullOrEmpty(createParams.FileName))
            {
                toCreate.Blob = new Blob {
                    FileName = createParams.FileName,
                    Body = createParams.Body
                };
            }

            var resultData = service.Execute(new CreateCommand
            {
                Entity = toCreate
            });

            if (resultData == null)
            {
                Debug.Print("Creation of new Document failed");
                Console.WriteLine("Creation of new Document failed");
                Console.WriteLine();
                return null;
            }


            var commandResponse = resultData as OperationCommandResponse;

            int? id = (commandResponse != null && commandResponse.EntitySpecifier != null) ? commandResponse.EntitySpecifier.Id : null;

            if (id.HasValue && resultData.ErrorInfo == null)
            {
                toCreate.Id = id.Value;
                Debug.Print($"Created new Document with Id={id.ToString()}");
                Console.WriteLine($"Created new Document with Id={id.ToString()}");
                Console.WriteLine();
                return toCreate;
            }


            Debug.Print("Creation of new Document failed");
            Console.WriteLine("Creation of new Document failed");
            Console.WriteLine();
            if (resultData.ErrorInfo != null && !string.IsNullOrEmpty(resultData.ErrorInfo.Description))
            {
                Debug.Print(resultData.ErrorInfo.Description);
                Console.WriteLine(resultData.ErrorInfo.Description);
            }


            return null;
        }
    }

    internal class DocumentCreateParams
    {
        public string Description {get; set;}
        public string Title { get; set; }
        public string MimeType { get; set; }
        public string DocType { get; set; }
        public bool IsLink { get; set; }
        public string FileName { get; set; }
        public byte[] Body { get; set; }

    }
}
