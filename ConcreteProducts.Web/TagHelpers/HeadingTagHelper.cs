namespace ConcreteProducts.Web.TagHelpers
{
    using Microsoft.AspNetCore.Razor.TagHelpers;

    [HtmlTargetElement("h2", Attributes = "heading-tag")]
    public class HeadingTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.RemoveAll("heading-tag");
            output.Attributes.Add("class", "headingMargin text-center offset-md-3 col-md-6 col-sm-12");
        }
    }
}
