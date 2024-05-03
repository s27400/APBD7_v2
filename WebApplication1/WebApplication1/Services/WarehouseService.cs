using WebApplication1.Models_DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class WarehouseService : IWarehouseService
{
    private IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<bool> ProductExist(int productId)
    {
        return await _warehouseRepository.ProductExist(productId);
    }

    public async Task<bool> WarehouseExist(int warehouseId)
    {
        return await _warehouseRepository.WarehouseExist(warehouseId);
    }

    public async Task<int> VerificationInOrder(int productId, int amount, DateTime dt)
    {
        return await _warehouseRepository.VerificationInOrder(productId, amount, dt);
    }

    public async Task<bool> NotInCompleted(int orderId)
    {
        return await _warehouseRepository.NotInCompleted(orderId);
    }

    public async Task<int> UpdateFulfilledAt(int orderId)
    {
        return await _warehouseRepository.UpdateFulfilledAt(orderId);
    }

    public async Task<int> InsertToProductWarehouse(WarehouseProductDTO toAdd, int idOrder)
    {
        return await _warehouseRepository.InsertToProductWarehouse(toAdd, idOrder);
    }

    public async Task<int> GetPK(WarehouseProductDTO toAdd, int orderId)
    {
        return await _warehouseRepository.GetPK(toAdd, orderId);
    }




}