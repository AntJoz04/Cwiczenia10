using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.DTOs;
using WebApplication5.Entities;

namespace WebApplication5.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PcsController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    public PcsController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pcs = await _dbContext.Pcs.Select(p => new PcListDto
        {
            Id = p.Id,
            Name = p.Name,
            Weight = p.Weight,
            Warranty = p.Warranty,
            CreatedAt = p.CreatedAt,
            Stock = p.Stock
        }).ToListAsync();
        
        
        return Ok(pcs);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var pc = await _dbContext.Pcs.Where(p => p.Id == id).Select(p => new PcDetailDto
        {
            Id = p.Id,
            Name = p.Name,
            Weight = p.Weight,
            Warranty = p.Warranty,
            CreatedAt = p.CreatedAt,
            Stock = p.Stock,
            ComponentList = p.PcComponents.Select(pcc => new ComponentListDto
            {
                Ammount = pcc.Ammount,
                Component = new ComponentDto
                {
                    Code = pcc.ComponentCode,
                    Name = pcc.Component.Name,
                    Description = pcc.Component.Description,
                    Manufacturer = new ManufacturerDto
                    {
                        Id = pcc.Component.ComponentManufacturer.Id,
                        Abbrevation = pcc.Component.ComponentManufacturer.Abbreviation,
                        Fullname = pcc.Component.ComponentManufacturer.FullName,
                        FoundationDate = pcc.Component.ComponentManufacturer.FoundationDate
                    }
                },
                ComponentTypes = new ComponentTypeDto
                {
                    Id = pcc.Component.ComponentType.Id,
                    Abbrevation = pcc.Component.ComponentType.Abbreviation,
                    Name = pcc.Component.ComponentType.Name
                }
            }).ToList()
        }).FirstOrDefaultAsync();
        if (pc == null)
        {
            return NotFound();
        }
        return Ok(pc);

    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PcCreateDto dto)
    {
        var pc = new Pcs
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };
        await _dbContext.Pcs.AddAsync(pc);
        await _dbContext.SaveChangesAsync();
        var result = new PcListDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock

        };
        return Created("api/pcs", result);


    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PcCreateDto dto)
    {
        var pc = await _dbContext.Pcs.FindAsync(id);
        if (pc == null)
        {
            return NotFound();
        }
        pc.Name=dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }
}