using Microsoft.AspNetCore.Mvc.Formatters;

namespace WebAPP
{
	public class TextPlainInputFormatter : InputFormatter
	{
		const string ContentType = "text/plain";

		public TextPlainInputFormatter()
		{
			SupportedMediaTypes.Add(ContentType);
		}

		public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
		{
			using StreamReader reader = new StreamReader(context.HttpContext.Request.Body);
			return await InputFormatterResult.SuccessAsync(await reader.ReadToEndAsync());
		}

		public override bool CanRead(InputFormatterContext context)
		{
			return context.HttpContext.Request.ContentType?.StartsWith(ContentType) == true;
		}
	}
}
