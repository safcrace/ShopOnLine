using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
            : base (x => 
                        (string.IsNullOrWhiteSpace(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) && 
                        (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
                    )
        {
            AddInlude(x => x.ProductType);
            AddInlude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyingPaging(productParams.PageIndex * (productParams.PageIndex -1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInlude(x => x.ProductType);
            AddInlude(x => x.ProductBrand);
        }
    }
}