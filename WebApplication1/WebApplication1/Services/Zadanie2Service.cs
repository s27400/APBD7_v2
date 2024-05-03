using WebApplication1.Models_DTOs;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class Zadanie2Service : IZadanie2Service
{
    private IZadanie2Repo _zadanie2Repo;

    public Zadanie2Service(IZadanie2Repo repo)
    {
        _zadanie2Repo = repo;
    }

    public async Task<int> InsertByTransaction(WarehouseProductDTO dto)
    {
        return await _zadanie2Repo.InsertByTransaction(dto);
    }
}