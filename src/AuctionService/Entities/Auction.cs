using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionService.Entities;

[Table("Auction")]
public class Auction
{
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int ReservePrice { get; set; }
    [MaxLength(100)]
    public required string Seller { get; set; }
    public string? Buyer { get; set; }
    [MaxLength(100)]
    public string? Winner { get; set; }
    public int? SoldAmount { get; set; }
    public int? CurrentHighBid { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime AuctionEndDate { get; set; }
    public Status Status { get; set; }
    public required Item Item { get; set; }

}