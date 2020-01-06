using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreSwaggerSample.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace AspNetCoreSwaggerSample.Formatter
{
    public class VcardInputFormatter : TextInputFormatter
    {
        public VcardInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            if (type == typeof(Contact))
            {
                return base.CanReadType(type);
            }

            return false;
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context,
            Encoding encoding)
        {
            if (context == null)
            {
                throw new ArgumentException(nameof(context));
            }

            if (encoding == null)
            {
                throw new ArgumentException(nameof(encoding));
            }

            var request = context.HttpContext.Request;

            using (var reader = new StreamReader(request.Body, encoding))
            {
                try
                {
                    await ReadLineSync("BEGIN:VCARD", reader, context);
                    await ReadLineSync("VERSION:2.1", reader, context);

                    var nameLine = await ReadLineSync("N:", reader, context);
                    var split = nameLine.Split(";".ToCharArray());
                    var contact = new Contact() {LastName = split[0].Substring(2), FirstName = split[1]};
                    await ReadLineSync("FN:", reader, context);

                    var idLine = await ReadLineSync("UID:", reader, context);
                    contact.ID = idLine.Substring(4);

                    await ReadLineSync("END:VCARD", reader, context);
                    return await InputFormatterResult.SuccessAsync(contact);
                }
                catch
                {
                    return await InputFormatterResult.FailureAsync();
                }
            }
        }

        private async Task<string> ReadLineSync(string expectedText, StreamReader reader, InputFormatterContext context)
        {
            var line = await reader.ReadLineAsync();
            if (!line.StartsWith(expectedText))
            {
                var errorMessage = $"Looked for {expectedText} and got {line}";
                context.ModelState.TryAddModelError(context.ModelName, errorMessage);
                throw new Exception(errorMessage);
            }

            return line;
        }
    }
}