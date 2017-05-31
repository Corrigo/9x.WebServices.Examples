using CorrigoServiceWebReference.CorrigoGA;
using System;

namespace _9x.WebServices.CustomField2s.Operations
{
    internal static class Update
    {
        public static void Execute(CorrigoService service, CustomField2 customField)
        {
            string label = "upd-IFS-1119 ";
            string href = "https://jira.qa.corrigo.com:55445/browse/IFS-1119";
            string urlValue = $"<a href=\"{href}\">{label}</a>";

            customField.Value = urlValue;

            var command = new UpdateCommand
            {
                Entity = customField,
                PropertySet = new PropertySet {Properties = new[] {"Value"}}
            };
            var response = service.Execute(command) as OperationCommandResponse;
        }

        public static void Restore(CorrigoService service, CustomField2 toRestore)
        {
            if (toRestore == null || service == null) return;
            Console.WriteLine();
            Console.WriteLine($"Restoring CustomField2 with id={toRestore.Id.ToString()}");

            var restoreResult = service.Execute(new RestoreCommand
            {
                EntitySpecifier = new EntitySpecifier {Id = toRestore.Id, EntityType = EntityType.CustomField2}
            });

            if (restoreResult == null)
            {
                Console.WriteLine("Update of CustomField2 failed");
                return;
            }

            if (restoreResult.ErrorInfo != null && !string.IsNullOrEmpty(restoreResult.ErrorInfo.Description))
            {
                Console.WriteLine(restoreResult.ErrorInfo.Description);
                Console.WriteLine("Restore of CustomField2 failed");
                return;
            }

            Console.WriteLine("CustomField2 is restored");
        }
    }
}
