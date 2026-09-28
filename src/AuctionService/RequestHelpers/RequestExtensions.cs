using AuctionService.Dto;
using AuctionService.Entities;

namespace AuctionService.RequestHelpers;

public static class RequestExtensions
{
    extension(UpdateAuctionRequestDto dto)
    {
        public bool AllValuesNull()
        {
            return dto is { Color: null, Description: null, Make: null, Mileage: null, Model: null, Year: null };
        }

        public bool AllValuesMatch(Auction auction)
        {
            // Match if the dto property is null OR property already matches the DB object's property
            return (dto.Color == null || dto.Color == auction.Item.Color)
                    && (dto.Description == null || dto.Description == auction.Item.Description)
                    && (dto.Make == null || dto.Make == auction.Item.Make)
                    && (dto.Mileage == null || dto.Mileage == auction.Item.Mileage)
                    && (dto.Model == null || dto.Model == auction.Item.Model)
                    && (dto.Year == null || dto.Year == auction.Item.Year);
        }
    }
}