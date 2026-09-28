namespace AuctionService.Dto;

public class UpdateAuctionRequestDto
{
    public string? Make { get; set; }
    public string? Model { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public int? Year { get; set; }
    public int? Mileage { get; set; }
}