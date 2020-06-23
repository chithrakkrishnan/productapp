using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Models.Queries;
using SuperMarket.API.Domain.Services;
using SuperMarket.API.Extensions;
using SuperMarket.API.Resources;

namespace SuperMarket.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<QueryResultResource<ProductResource>> List([FromQuery] ProductsQueryResource query)

        {
            var productsQuery = _mapper.Map<ProductsQueryResource, ProductsQuery>(query);
            var queryResult = await _productService.List(productsQuery);

            var resource = _mapper.Map<QueryResult<Product>, QueryResultResource<ProductResource>>(queryResult);
            return resource;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]
            SaveProductResource resource)
        {
          
            var category = _mapper.Map<SaveProductResource, Product>(resource);
            var result = await _productService.Save(category);

            if (!result.Success)
                return BadRequest(result.Message);

            var productResource = _mapper.Map<Product, ProductResource>(result.Resource);
            return Ok(productResource);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var result = await _productService.GetValue(id);
            var resources = _mapper.Map<Product, ProductResource>(result.Resource);

            return Ok(resources);
        }

    }
}