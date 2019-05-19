using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using WebSite.Model.ViewModel;

namespace WebSite.Infrastructure
{
	[HtmlTargetElement("div", Attributes = "page-model")]
	public class PageLinkTagHelper : TagHelper
	{
		private readonly IUrlHelperFactory urlHelperFactory;

		public PageLinkTagHelper(IUrlHelperFactory helperFactory)
		{
			urlHelperFactory = helperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public PagingInfo PageModel { get; set; }
		public string PageAction { get; set; }
		public bool PageClassesEnabled { get; set; } = false;
		public string PageClass { get; set; }
		public string PageClassNormal { get; set; }
		public string PageClassSelected { get; set; }
		public string CompanyName { get; set; }
		public string CompanyStatus { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			TagBuilder result = new TagBuilder("div");
			for (int i = 1; i <= PageModel.TotalPages && i <= 10; i++)
			{
				TagBuilder tag = new TagBuilder("a");
				tag.Attributes["href"] =
					urlHelper.Action(PageAction, new {page = i, status = CompanyStatus, name = CompanyName});

				if (PageClassesEnabled)
				{
					tag.AddCssClass(PageClass);
					tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
				}

				tag.InnerHtml.Append(i.ToString());
				result.InnerHtml.AppendHtml(tag);
			}

			output.Content.AppendHtml(result.InnerHtml);
		}
	}
}