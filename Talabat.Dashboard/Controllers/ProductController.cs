﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Domain.Contracts.Persistence;
using Talabat.Core.Domain.Entities.Products;
using Talabat.Dashboard.Helper;
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

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.Image != null)
                {
                    productViewModel.PictureUrl = PictureSetting.UploadFile(productViewModel.Image, "Products");
                }
                else
                {
                    productViewModel.PictureUrl = "images/products/glazed-donuts.png";
                }

                var mappedProduct = _mapper.Map<ProductViewModel, Product>(productViewModel);
                string userName = User.Identity?.Name ?? "aya.ali";
                mappedProduct.CreatedBy = userName;
                mappedProduct.CreatedOn = DateTime.UtcNow;
                mappedProduct.LastModifiedBy = userName;
                mappedProduct.LastModifiedOn = DateTime.UtcNow;


                mappedProduct.NormalizedName = productViewModel.Name?.ToUpperInvariant();

                await _unitOfWork.GetRepository<Product, int>().AddAsync(mappedProduct);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");
            }

            return View(productViewModel);

        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (productViewModel.Image != null)
                {
                    if (productViewModel.PictureUrl != null) { 

                        PictureSetting.DeleteFile(productViewModel.PictureUrl, "products");
                    productViewModel.PictureUrl = PictureSetting.UploadFile(productViewModel.Image, "products");
                }
                }
                else
                {
                    productViewModel.PictureUrl = PictureSetting.UploadFile(productViewModel.Image, "products");
                }
                var mappedProduct = _mapper.Map<ProductViewModel,  Product>(productViewModel);
                string userName = User.Identity?.Name ?? "aya.ali";
                mappedProduct.CreatedBy = userName;
                mappedProduct.CreatedOn = DateTime.UtcNow;
                mappedProduct.LastModifiedBy = userName;
                mappedProduct.LastModifiedOn = DateTime.UtcNow;


                mappedProduct.NormalizedName = productViewModel.Name?.ToUpperInvariant();
                _unitOfWork.GetRepository<Product, int>().Update(mappedProduct);
                var result = await _unitOfWork.CompleteAsync();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(productViewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            var mappedProduct = _mapper.Map<Product, ProductViewModel>(product);
            return View(mappedProduct);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id)
            {
                return NotFound();
            }
            try
            {
                var product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
                if (product.PictureUrl != null)
                {
                    PictureSetting.DeleteFile(product.PictureUrl, "products");
                }
                _unitOfWork.GetRepository<Product, int>().Delete(product);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {
                return
         View(productViewModel);
            }
        }
    }
}