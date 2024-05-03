using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using WebApplication1.Models_DTOs;

namespace WebApplication1.Repositories;

public class Zadanie2Repo : IZadanie2Repo
{
    private IConfiguration _configuration;

    public Zadanie2Repo(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<int> InsertByTransaction(WarehouseProductDTO dto)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await con.OpenAsync();

        DbTransaction trans = await con.BeginTransactionAsync();

        await using var cmd = con.CreateCommand();
        cmd.Connection = con;
        cmd.Transaction = (SqlTransaction)trans;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "AddProductToWarehouse";
        cmd.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
        cmd.Parameters.AddWithValue("@IdWarehouse", dto.IdWarehouse);
        cmd.Parameters.AddWithValue("@Amount", dto.Amount);
        cmd.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);
        try
        {
            var temp = await cmd.ExecuteScalarAsync();
            trans.CommitAsync();
            return Convert.ToInt32(temp);
        }
        catch (SqlException e)
        {
            await trans.RollbackAsync();
            return 0;
        }
        catch (Exception e)
        {
            await trans.RollbackAsync();
            return 0;
        }

        

    }
}