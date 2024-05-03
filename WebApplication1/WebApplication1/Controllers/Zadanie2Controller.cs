using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models_DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/Zadanie2")]
[ApiController]
public class Zadanie2Controller : ControllerBase
{
    private IZadanie2Service _zadanie2Service;

    public Zadanie2Controller(IZadanie2Service zadanie2Service)
    {
        _zadanie2Service = zadanie2Service;
    }

    [HttpPost]
    public async Task<IActionResult> InsertByTransaction(WarehouseProductDTO dto)
    {
        int res = await _zadanie2Service.InsertByTransaction(dto);

        if (res == 0)
        {
            return NotFound("Błąd");
        }

        return Ok($"Dodano rekord {res}");
    }
}