namespace ConcreteProducts.Web.Infrastructure
{
    using System.Linq;
    using AutoMapper;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Services.Categories.Models;
    using ConcreteProducts.Web.Services.Colors.Models;
    using ConcreteProducts.Web.Services.Products.Models;
    using ConcreteProducts.Web.Services.Shapes.Models;
    using ConcreteProducts.Web.Services.Warehouses.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Product, ProductBaseServiceModel>();
            this.CreateMap<Product, ProductListingServiceModel>()
                .ForMember(p => p.InPallet, cfg => cfg.MapFrom(p => $"{p.QuantityInPalletInPieces} pieces / {p.QuantityInPalletInUnitOfMeasurement}{p.UnitOfMeasurement}"));
            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name))
                .ForMember(p => p.UnitOfMeasurement, cfg => cfg.MapFrom(p => p.UnitOfMeasurement.ToString()))
                .ForMember(p => p.AvailableColorsName, cfg => cfg.MapFrom(c => c.ProductColors.Select(pc => pc.Color.Name).ToList()));

            this.CreateMap<Category, CategoryServiceModel>();
            this.CreateMap<Category, CategoryWithProducts>()
                .ForMember(c => c.ProductsCount, cfg => cfg.MapFrom(c => c.Products.Count));

            this.CreateMap<Color, ColorServiceModel>();
            this.CreateMap<Color, ColorDeleteServiceModel>()
                .ForMember(c => c.ProductsRelatedToColor, cfg => cfg.MapFrom(c => c.ProductColors.Count));

            this.CreateMap<ProductColor, ColorServiceModel>()
                .ForMember(c => c.Id, cfg => cfg.MapFrom(pc => pc.ColorId))
                .ForMember(c => c.Name, cfg => cfg.MapFrom(pc => pc.Color.Name));

            this.CreateMap<Shape, ShapeServiceModel>();
            this.CreateMap<Shape, ShapeDetailsServiceModel>();
            this.CreateMap<Shape, ShapeAndWarehouseServiceModel>()
                .ForMember(s => s.WarehouseName, cfg => cfg.MapFrom(s => s.Warehouse.Name));

            this.CreateMap<Warehouse, WarehouseServiceModel>();
            this.CreateMap<Warehouse, WarehouseWithProductsAndShapesCount>()
                .ForMember(w => w.TotalProductsCount, cfg => cfg.MapFrom(p => p.Products.Count))
                .ForMember(w => w.TotalShapesCount, cfg => cfg.MapFrom(s => s.Shapes.Count));
        }
    }
}
