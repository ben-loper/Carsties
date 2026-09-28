using AuctionService.Dto;
using AuctionService.Entities;

namespace AuctionService.RequestHelpers;

public static class AuctionMap
{
    public static void MapUpdateValues(Auction auction, UpdateAuctionRequestDto dto)
    {
        auction.Item.Make = dto.Make ?? auction.Item.Make;
        auction.Item.Model = dto.Model ?? auction.Item.Model;
        auction.Item.Description = dto.Description ?? auction.Item.Description;
        auction.Item.Year = dto.Year ?? auction.Item.Year;
        auction.Item.Color = dto.Color ?? auction.Item.Color;
        auction.Item.Mileage = dto.Mileage ?? auction.Item.Mileage;
    }
}