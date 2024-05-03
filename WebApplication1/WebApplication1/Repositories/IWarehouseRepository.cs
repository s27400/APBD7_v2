using System.Runtime.CompilerServices;
using WebApplication1.Models_DTOs;

namespace WebApplication1.Repositories;

public interface IWarehouseRepository
{
    public Task<bool> ProductExist(int productId);
    public Task<bool> WarehouseExist(int warehouseId);
    public Task<int> VerificationInOrder(int productId, int amount, DateTime dt);
    public Task<bool> NotInCompleted(int orderId);
    public Task<int> UpdateFulfilledAt(int orderId);
    public Task<int> InsertToProductWarehouse(WarehouseProductDTO toAdd, int idOrder);
    public Task<decimal> GetPrice(int productId);
    public Task<int> GetPK(WarehouseProductDTO toAdd, int orderId);
}