using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entities.Products;

namespace Talabat.Dashboard.Controllers
{
    public class BrandController(IUnitOfWork _unitOfWork) : Controller
    {
        public async Task< IActionResult >Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            return View(brands);
        }

        public async Task<IActionResult> Create(ProductBrand brand)
        {
            try
            {
                await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(brand);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", "please enter your Name");
                return View("Index", await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync()); ;
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetAsync(id);

            _unitOfWork.GetRepository<ProductBrand, int>().Delete(brand);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction("Index");

        }

    }
}
