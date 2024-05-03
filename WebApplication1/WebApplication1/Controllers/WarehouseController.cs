using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models_DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private IWarehouseService _warehouseService;

    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public async Task<IActionResult> AddProductWarehouse(WarehouseProductDTO toAdd)
    {
        bool productExist = await _warehouseService.ProductExist(toAdd.IdProduct);

        if (!productExist)
        {
            return NotFound($"Brak produktu o id: {toAdd.IdProduct}");
        }

        bool warehouseExist = await _warehouseService.WarehouseExist(toAdd.IdWarehouse);

        if (!warehouseExist)
        {
            return NotFound($"Brak magazynu o id: {toAdd.IdWarehouse}");
        }

        int orderId = await _warehouseService.VerificationInOrder(toAdd.IdProduct, toAdd.Amount, toAdd.CreatedAt);

        if (orderId == -999)
        {
            return NotFound("Nie znaleziono odpowiedniego zamówienia");
        }

        bool notInCompleted = await _warehouseService.NotInCompleted(orderId);

        if (!notInCompleted)
        {
            return NotFound($"Zamówienie o id: {orderId} zostało skompletowane");
        }

        await _warehouseService.UpdateFulfilledAt(orderId);

        await _warehouseService.InsertToProductWarehouse(toAdd, orderId);

        int pk = await _warehouseService.GetPK(toAdd, orderId);

        if (pk == 0)
        {
            return NotFound("Nie mogę odszukać dodanego zamówienia");
        }

        return Ok($"Klucz główny dodanego zamówienia: {pk}");
    }

}