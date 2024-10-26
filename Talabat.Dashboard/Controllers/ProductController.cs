using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Controllers
{
    public class ProductController(IUnitOfWork _unitOfWork , IMapper _mapper) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            var mapperProduct = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            return View(mapperProduct);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
