using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models_DTOs;

public class WarehouseProductDTO
{
    [Required]
    public int IdProduct { get; set; }
    [Required]
    public int IdWarehouse { get; set; }
    [Required]
    [Range(1, Int32.MaxValue)]
    public int Amount { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
}