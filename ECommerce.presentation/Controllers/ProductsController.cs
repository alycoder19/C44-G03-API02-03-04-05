using ECommerce.presentation.Attribute;
using ECommerce.Service.Abstraction;
using ECommerce.Shared;
using ECommerce.Shared.DTOS.ProductDTOS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        #region GetAllProduct


        [HttpGet]
        [RedisCache]
        //[Cache]
        public async Task<ActionResult<PaginatedResult<ProductDTO>>> GetAllProduct([FromQuery] ProductQueryParams queryParms)
        {

            var Products = await _productService.GetAllProductsAsync(queryParms);
            return Ok(Products);


        }



        #endregion


        #region GetProduct By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>>GetProductById(int id)
        {

            throw new Exception();
                var Product = await _productService.GetProductByIdAsync(id);
             //   if (Product is null) return NotFound($"No Product With Id : {id} Found!");
                return Ok(Product);
            
            



        }


        #endregion


        #region Get All Brands

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDTO>>> GetAllBrands()
        {

            var Brands=await _productService.GetAllBrandsAsync();
            return Ok(Brands);

        }

        #endregion


        #region Get All Types

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDTO>>>GetAllTypes()
        {

            var Types=await _productService.GetAllTypesAsync();
            return Ok(Types);

        }


        #endregion

    }
}
