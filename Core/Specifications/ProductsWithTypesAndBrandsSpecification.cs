using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInlude(x => x.ProductType);
            AddInlude(x => x.ProductBrand);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInlude(x => x.ProductType);
            AddInlude(x => x.ProductBrand);
        }
    }
}