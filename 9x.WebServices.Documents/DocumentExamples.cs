using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorrigoServiceWebReference;
using CorrigoServiceWebReference.CorrigoGA;
using _9x.WebServices.Documents.Operations;
using System.Drawing;

namespace _9x.WebServices.Documents
{
    public static class DocumentExamples
    {

        public static void CRUDExample(CorrigoService service, bool isCreateUpdateDelete = false)
        {
            if (service == null) return;


            Document documentLink = !isCreateUpdateDelete ? null :
                Create.Execute(service,
                new DocumentCreateParams
                {
                    IsLink = true,
                    DocType = "Link",
                    MimeType = "url",
                    Title = "CorrigoService.asmx",
                    Description = "https://v91.qa.corrigo.com/wsdk/CorrigoService.asmx"
                });

            Document documentTxtFile = !isCreateUpdateDelete ? null :
                Create.Execute(service,
                new DocumentCreateParams
                {
                    DocType = "Document",
                    MimeType = "text/plain",
                    Title = "Hey test",
                    FileName = "Hey test.txt",
                    Description = "Addition of txt document via CorrigoService",
                    Body = Encoding.ASCII.GetBytes("Hey ya !!!")
                });

            Document documentExcelFile = !isCreateUpdateDelete ? null :
                Create.Execute(service,
                new DocumentCreateParams
                {
                    DocType = "Document",
                    MimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    Title = "MSExcelTest",
                    FileName = "MSExcelTest.xlsx",
                    Description = "Addition of Excel document via CorrigoService",
                    Body = _9x.WebServices.Documents.Properties.Resources.MSExcelTest
                });

            Document documentPDFFile = !isCreateUpdateDelete ? null :
                Create.Execute(service,
                new DocumentCreateParams
                {
                    DocType = "Document",
                    MimeType = "application/pdf",
                    Title = "OutputTest",
                    FileName = "OutputTest.pdf",
                    Description = "Addition of PDF document via CorrigoService",
                    Body = _9x.WebServices.Documents.Properties.Resources.OutputTest
                });

            Document documentWordFile = !isCreateUpdateDelete ? null :
                Create.Execute(service,
                new DocumentCreateParams
                {
                    DocType = "Document",
                    MimeType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                    Title = "WordTest",
                    FileName = "WordTest.docx",
                    Description = "Addition of Word document via CorrigoService",
                    Body = _9x.WebServices.Documents.Properties.Resources.WordTest
                });

            Document documentBmpFile = !isCreateUpdateDelete ? null :
                Create.Execute(service,
                new DocumentCreateParams
                {
                    DocType = "Picture",
                    MimeType = "image/bmp",
                    Title = "Smiley11",
                    FileName = "Smiley11.bmp",
                    Description = "Addition of BMP document via CorrigoService",
                    Body = (byte[]) (new ImageConverter()).ConvertTo(
                                            _9x.WebServices.Documents.Properties.Resources.Smiley11,
                                            typeof(byte[]))
                });


            if (documentBmpFile != null)
            {
                Document document = Read.Retrieve(service, documentBmpFile.Id);

                Update.Execute(service, document);

                Read.Retrieve(service, document.Id);

                Update.Restore(service, document); // Document is not restorable.

                Delete.Execute(service, document.Id); // Attention : delete will exactly delete Document from DB.                
            }

            Read.RetrieveMultiple(service);

            Read.RetrieveByQuery(service);

        }
    }

}
