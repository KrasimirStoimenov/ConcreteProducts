namespace ConcreteProducts.Web.Infrastructure
{
    using AutoMapper;
    using ConcreteProducts.Web.Data.Models;
    using ConcreteProducts.Web.Services.Categories.Dtos;
    using ConcreteProducts.Web.Services.Colors.Dtos;
    using ConcreteProducts.Web.Services.Shapes.Dtos;
    using ConcreteProducts.Web.Services.Warehouses.Dtos;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Category, CategoryServiceModel>();
            this.CreateMap<Category, CategoryWithProducts>()
                .ForMember(c => c.ProductsCount, cfg => cfg.MapFrom(c => c.Products.Count));

            this.CreateMap<Color, ColorServiceModel>();
            this.CreateMap<Color, ColorDeleteServiceModel>()
                .ForMember(c => c.ProductsRelatedToColor, cfg => cfg.MapFrom(c => c.ProductColors.Count));

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
