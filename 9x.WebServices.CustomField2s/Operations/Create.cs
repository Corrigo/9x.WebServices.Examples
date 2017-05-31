
using CorrigoServiceWebReference.CorrigoGA;



namespace _9x.WebServices.CustomField2s.Operations
{
    internal static class Create
    {
        public static CustomField2 Execute(CorrigoService service, CorrigoEntity entity,
            CustomFieldDescriptor customFieldDescriptor)
        {
            string label = "IFS-1119";
            string href = "https://jira.qa.corrigo.com:55445/browse/IFS-1119";
            string urlValue = $"<a href=\"{href}\">{label}</a>";

            var customField = new CustomField2
            {
                Descriptor = new CustomFieldDescriptor {Id = customFieldDescriptor.Id},
                ObjectId = entity.Id,
                ObjectTypeId = customFieldDescriptor.ActorTypeId,
                Value = urlValue
            };
            var command = new CreateCommand {Entity = customField};
            var response = service.Execute(command) as OperationCommandResponse;

            customField.Id = response?.EntitySpecifier?.Id ?? 0;

            return customField;
        }
    }
}
