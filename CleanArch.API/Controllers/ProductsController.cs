using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var produtos = await _productService.GetProducts();

            if(produtos == null) return NotFound("Products not found");

            return Ok(produtos);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var product = await _productService.GetById(id);
            if(product == null)
            {
                return NotFound("Category not found");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ProductDTO productDTO)
        {
            if(productDTO == null) return BadRequest("Data Invalid");

            await _productService.Add(productDTO);
            return new CreatedAtRouteResult("GetProduct", new {id = productDTO.Id}, productDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ProductDTO productDTO)
        {
            if(id != productDTO.Id)
            {
                return BadRequest("Data invalid");
            }

            if(productDTO == null) return BadRequest("Data invalid");

            await _productService.Update(productDTO);
            return Ok(productDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var productDTO = await _productService.GetById(id);

            if(productDTO == null)
            {
                return NotFound("Produto not found");
            }

            await _productService.Remove(id);
            return Ok(productDTO);
        }
    }
}