using WebApplication1.Models_DTOs;

namespace WebApplication1.Repositories;

public interface IZadanie2Repo
{
    Task<int> InsertByTransaction(WarehouseProductDTO dto);
}