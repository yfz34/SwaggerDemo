using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerDemo.Dtos;
using Swashbuckle.AspNetCore.Annotations;

namespace SwaggerDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [SwaggerTag("產品增刪改查")]
    // [ApiExplorerSettings(IgnoreApi = true)]
    public class ProductController : ControllerBase
    {
        private static readonly List<ProductDto> products = new()
        {
            new ProductDto { Id = Guid.NewGuid(), Name = "Mouse", Description = "This is a mouse.", Price = 500, CreatedDate = DateTimeOffset.UtcNow, UpdatedDate = DateTimeOffset.UtcNow },
            new ProductDto { Id = Guid.NewGuid(), Name = "Keyboard", Description = "This is a keyboard.", Price = 1000, CreatedDate = DateTimeOffset.UtcNow, UpdatedDate = DateTimeOffset.UtcNow },
            new ProductDto { Id = Guid.NewGuid(), Name = "Screen", Description = "This is a screen.", Price = 10000, CreatedDate = DateTimeOffset.UtcNow, UpdatedDate = DateTimeOffset.UtcNow },
        };

        /// <summary>
        /// 取得產品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [SwaggerOperation(Tags = new[] { "產品相關", "Product" })]
        public IEnumerable<ProductDto> Get()
        {
            return products;
        }

        /// <summary>
        /// 透過產品id取得產品資料
        /// </summary>
        /// <param name="id">產品id</param>
        /// <returns></returns>
        /// <remarks>備註描述</remarks>
        /// <response code="200">取得產品資料</response>
        /// <response code="404">沒有資料</response>
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "透過產品id取得產品資料",
            Description = "備註描述",
            OperationId = "GetProductById",
            Tags = new[] { "產品相關", "Product" }
        )]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ProductDto> GetById(Guid id)
        {
            var product = products.Where(product => product.Id == id).SingleOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        /// <summary>
        /// 新增產品
        /// </summary>
        /// <param name="createProductDto">新增產品物件</param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /product
        ///     {
        ///        "name": "Computer",
        ///        "description": "This is a computer.",
        ///        "price": 30000
        ///     }
        ///
        /// </remarks>
        /// <response code="201">回傳新的產品資料</response>
        /// <response code="400">資料欄位有誤</response>
        [HttpPost]
        [SwaggerOperation(Tags = new[] { "產品相關", "Product" })]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(400, "資料欄位有誤")]
        public ActionResult<ProductDto> Post(CreateProductDto createProductDto)
        {
            var product = new ProductDto
            {
                Id = Guid.NewGuid(),
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                Price = createProductDto.Price,
                CreatedDate = DateTimeOffset.UtcNow,
                UpdatedDate = DateTimeOffset.UtcNow
            };

            products.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        /// <summary>
        /// 編輯產品資料
        /// </summary>
        /// <param name="id">產品id</param>
        /// <param name="updateProductDto">產品資訊</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [SwaggerOperation(Tags = new[] { "產品相關", "Product" })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(Guid id, UpdateProductDto updateProductDto)
        {
            var existingProduct = products.Where(product => product.Id == id).SingleOrDefault();

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = updateProductDto.Name;
            existingProduct.Description = updateProductDto.Description;
            existingProduct.Price = updateProductDto.Price;
            existingProduct.UpdatedDate = DateTimeOffset.UtcNow;

            return NoContent();
        }

        /// <summary>
        /// 使用產品id刪除資料
        /// </summary>
        /// <param name="id">產品id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Tags = new[] { "產品相關", "Product" })]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(Guid id)
        {
            var index = products.FindIndex(product => product.Id == id);

            if (index < 0)
            {
                return NotFound();
            }

            products.RemoveAt(index);

            return NoContent();
        }
    }
}