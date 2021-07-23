namespace ConcreteProducts.Web.Services.Colors.Dtos
{
    public class ColorDeleteServiceModel : ColorServiceModel
    {
        public int ProductsRelatedToColor { get; init; }
    }
}
