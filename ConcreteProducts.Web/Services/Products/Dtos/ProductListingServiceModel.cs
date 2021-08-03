﻿namespace ConcreteProducts.Web.Services.Products.Dtos
{
    public class ProductListingServiceModel : ProductBaseServiceModel
    {
        public string Dimensions { get; init; }

        public string InPallet { get; init; }

        public string ImageUrl { get; init; }

        public string CategoryName { get; init; }
    }
}