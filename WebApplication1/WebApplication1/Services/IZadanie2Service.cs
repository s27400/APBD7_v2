using WebApplication1.Models_DTOs;

namespace WebApplication1.Services;

public interface IZadanie2Service
{
    Task<int> InsertByTransaction(WarehouseProductDTO dto);
}