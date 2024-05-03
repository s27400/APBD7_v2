using System.Data.SqlClient;
using WebApplication1.Models_DTOs;

namespace WebApplication1.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task<bool> ProductExist(int productId)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;

        cmd.CommandText = "SELECT * FROM Product WHERE IdProduct = @productId";
        cmd.Parameters.AddWithValue("@productId", productId);

        if (await cmd.ExecuteScalarAsync() is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> WarehouseExist(int warehouseId)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Warehouse WHERE IdWarehouse = @warehouseId";
        cmd.Parameters.AddWithValue("@warehouseId", warehouseId);

        if (await cmd.ExecuteScalarAsync() is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<int> VerificationInOrder(int productId, int amount, DateTime dt)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            "SELECT IdOrder FROM [Order] WHERE IdProduct = @productId AND Amount = @amount AND CreatedAt < @dt AND FulfilledAt IS NULL";
        cmd.Parameters.AddWithValue("@productId", productId);
        cmd.Parameters.AddWithValue("@amount", amount);
        cmd.Parameters.AddWithValue("@dt", dt);

        var res = await cmd.ExecuteScalarAsync();
        if (res is not null)
        {
            return (int)res;
        }

        return -999;
    }

    public async Task<bool> NotInCompleted(int orderId)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT * FROM Product_Warehouse WHERE IdOrder = @orderId";
        cmd.Parameters.AddWithValue("@orderId", orderId);

        if (await cmd.ExecuteScalarAsync() is not null)
        {
            return false;
        }

        return true;
    }

    public async Task<int> UpdateFulfilledAt(int orderId)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "UPDATE [Order] SET FulfilledAt = @dt WHERE IdOrder = @orderId";
        cmd.Parameters.AddWithValue("@dt", DateTime.Now.AddHours(2));
        cmd.Parameters.AddWithValue("@orderId", orderId);

        return await cmd.ExecuteNonQueryAsync();
    }

    public async Task<int> InsertToProductWarehouse(WarehouseProductDTO toAdd, int idOrder)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        decimal price = await GetPrice(toAdd.IdProduct);
        decimal finalPrice = price * toAdd.Amount;
        cmd.Connection = con;
        cmd.CommandText =
            "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES(@IdW, @IdP, @IdO, @amo, @price, @createdAt)";
        cmd.Parameters.AddWithValue("@IdW", toAdd.IdWarehouse);
        cmd.Parameters.AddWithValue("@IdP", toAdd.IdProduct);
        cmd.Parameters.AddWithValue("@IdO", idOrder);
        cmd.Parameters.AddWithValue("@amo", toAdd.Amount);
        cmd.Parameters.AddWithValue("@price", finalPrice);
        cmd.Parameters.AddWithValue("@createdAt", toAdd.CreatedAt);


        return await cmd.ExecuteNonQueryAsync();

    }

    public async Task<decimal> GetPrice(int productId)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText = "SELECT Price FROM Product WHERE IdProduct = @prodId";
        cmd.Parameters.AddWithValue("@prodId", productId);

        var res = await cmd.ExecuteScalarAsync();

        if (res is not null)
        {
            return (decimal)res;
        }

        return 0;
    }

    public async Task<int> GetPK(WarehouseProductDTO toAdd, int orderId)
    {
        await using var con = new SqlConnection(_configuration.GetConnectionString("Default"));
        await con.OpenAsync();

        await using var cmd = new SqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            "SELECT IdProductWarehouse FROM Product_Warehouse WHERE IdWarehouse = @IdW AND IdProduct = @IdP AND IdOrder = @IdO";
        cmd.Parameters.AddWithValue("@IdW", toAdd.IdWarehouse);
        cmd.Parameters.AddWithValue("@IdP", toAdd.IdProduct);
        cmd.Parameters.AddWithValue("@IdO", orderId);

        var res = await cmd.ExecuteScalarAsync();

        if (res is not null)
        {
            return (int)res;
        }

        return 0;
    }
}