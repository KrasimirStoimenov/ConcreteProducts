﻿namespace ConcreteProducts.Web.Models.Products
{
    using System;
    using System.Collections.Generic;

    public class ProductListViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; init; }

        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public int PreviousPageNumber => this.PageNumber - 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ProductsCount / this.ItemsPerPage);

        public int ProductsCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}