using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Application.Abstraction.Models.Products;

namespace Talabat.Core.Application.Abstraction.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductToReturnDto>> GetProductsAsync(string? sort);
        Task<ProductToReturnDto> GetProductAsync(int Id);

        Task<IEnumerable<CategoryDto>> GetCategoriesAsync();
        Task<IEnumerable<BrandDto>> GetBrandsAsync();

    }
}
