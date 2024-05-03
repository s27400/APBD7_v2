using WebApplication1.Models_DTOs;

namespace WebApplication1.Services;

public interface IWarehouseService
{
    public Task<bool> ProductExist(int productId);
    public Task<bool> WarehouseExist(int warehouseId);
    public Task<int> VerificationInOrder(int productId, int amount, DateTime dt);
    public Task<bool> NotInCompleted(int orderId);
    public Task<int> UpdateFulfilledAt(int orderId);
    public Task<int> InsertToProductWarehouse(WarehouseProductDTO toAdd, int idOrder);
    
    public Task<int> GetPK(WarehouseProductDTO toAdd, int orderId);

}