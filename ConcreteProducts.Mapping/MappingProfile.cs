namespace ConcreteProducts.Web.Infrastructure
{
    using System;
    using System.Linq;

    using AutoMapper;

    using ConcreteProducts.Data.Models;
    using ConcreteProducts.Services.Colors.Models;
    using ConcreteProducts.Services.Shapes.Models;
    using ConcreteProducts.Services.Products.Models;
    using ConcreteProducts.Services.Categories.Models;
    using ConcreteProducts.Services.Warehouses.Models;
    using ConcreteProducts.Services.ProductColors.Model;
    using ConcreteProducts.Services.WarehouseProducts.Models;
    using ConcreteProducts.Services.Chats.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Product, ProductBaseServiceModel>();
            this.CreateMap<Product, ProductListingServiceModel>()
                .ForMember(p => p.InPallet, cfg => cfg.MapFrom(p => $"{p.QuantityInPalletInPieces:F2} pieces / {p.QuantityInPalletInUnitOfMeasurement:F2}{p.UnitOfMeasurement}"));
            this.CreateMap<Product, ProductDetailsServiceModel>()
                .ForMember(p => p.CategoryName, cfg => cfg.MapFrom(c => c.Category.Name))
                .ForMember(p => p.UnitOfMeasurement, cfg => cfg.MapFrom(p => p.UnitOfMeasurement.ToString()))
                .ForMember(p => p.AvailableColorsName, cfg => cfg.MapFrom(c => c.ProductColors.Select(pc => pc.Color.Name).ToList()));

            this.CreateMap<Category, CategoryBaseServiceModel>();
            this.CreateMap<Category, CategoryWithProducts>()
                .ForMember(c => c.ProductsCount, cfg => cfg.MapFrom(c => c.Products.Count));

            this.CreateMap<Color, ColorBaseServiceModel>();
            this.CreateMap<Color, ColorDeleteServiceModel>()
                .ForMember(c => c.ProductsRelatedToColor, cfg => cfg.MapFrom(c => c.ProductColors.Count));

            this.CreateMap<ProductColor, ColorBaseServiceModel>()
                .ForMember(c => c.Id, cfg => cfg.MapFrom(pc => pc.ColorId))
                .ForMember(c => c.Name, cfg => cfg.MapFrom(pc => pc.Color.Name));
            this.CreateMap<ProductColor, ProductColorBaseServiceModel>()
                .ForMember(c => c.Name, cfg => cfg.MapFrom(pc => $"{pc.Product.Name} - {pc.Color.Name}"));

            this.CreateMap<Shape, ShapeBaseServiceModel>();
            this.CreateMap<Shape, ShapeDetailsServiceModel>();
            this.CreateMap<Shape, ShapeAndWarehouseServiceModel>()
                .ForMember(s => s.WarehouseName, cfg => cfg.MapFrom(s => s.Warehouse.Name));

            this.CreateMap<Warehouse, WarehouseBaseServiceModel>();
            this.CreateMap<Warehouse, WarehouseWithProductsAndShapesCount>()
                .ForMember(w => w.TotalProductsCount, cfg => cfg.MapFrom(pw => pw.WarehouseProducts.Count))
                .ForMember(w => w.TotalShapesCount, cfg => cfg.MapFrom(s => s.Shapes.Count));

            this.CreateMap<WarehouseProductColors, WarehouseProductsServiceModel>()
                .ForMember(wp => wp.ProductColorName, cfg => cfg.MapFrom(wp => $"{wp.ProductColor.Product.Name} - {wp.ProductColor.Color.Name}"))
                .ForMember(wp => wp.TotalUnitOfMeasurement, cfg => cfg.MapFrom(wp => $"{wp.Count / wp.ProductColor.Product.CountInUnitOfMeasurement:F2} {wp.ProductColor.Product.UnitOfMeasurement.ToString()}"))
                .ForMember(wp => wp.Pallets, cfg => cfg.MapFrom(wp => (int)Math.Ceiling(wp.Count / wp.ProductColor.Product.QuantityInPalletInPieces)));

            this.CreateMap<ChatMessage, MessageServiceModel>();
        }
    }
}
