using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductsController(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _repo.GetAllAsync();
        var result = _mapper.Map<IEnumerable<ProductDto>>(products);

        return Ok(new ReturnResult<IEnumerable<ProductDto>>
        {
            IsSuccess = true,
            Result = result
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _repo.GetAsync(id);

        if (product == null)
        {
            return NotFound(new ReturnResult<ProductDto>
            {
                IsSuccess = false,
                ErrorMessage = new List<string> { "товар не найден" }
            });
        }

        return Ok(new ReturnResult<ProductDto>
        {
            IsSuccess = true,
            Result = _mapper.Map<ProductDto>(product)
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        await _repo.CreateAsync(product);

        var result = _mapper.Map<ProductDto>(product);

        return CreatedAtAction(
            nameof(Get),
            new { id = product.Id },
            new ReturnResult<ProductDto>
            {
                IsSuccess = true,
                Result = result
            });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProductCreateDto dto)
    {
        var product = await _repo.GetAsync(id);

        if (product == null)
        {
            return NotFound(new ReturnResult<ProductDto>
            {
                IsSuccess = false,
                ErrorMessage = new List<string> { "товар не найден" }
            });
        }

        product.Name = dto.Name;
        product.Price = dto.Price;
        product.Category = dto.Category;

        await _repo.UpdateAsync(product);

        return Ok(new ReturnResult<ProductDto>
        {
            IsSuccess = true,
            Result = _mapper.Map<ProductDto>(product)
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _repo.GetAsync(id);

        if (product == null)
        {
            return NotFound(new ReturnResult<object>
            {
                IsSuccess = false,
                ErrorMessage = new List<string> { "товар не найден" }
            });
        }

        await _repo.DeleteAsync(id);

        return Ok(new ReturnResult<object>
        {
            IsSuccess = true,
            Result = new { message = "товар успешно удален" }
        });
    }
}