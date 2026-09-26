using AuctionService.Dto;
using AuctionService.Entities;
using Mapster;

namespace AuctionService.RequestHelpers;

public class MappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Auction, AuctionDto>()
            .Map(dest => dest.Make, src => src.Item.Make)
            .Map(dest => dest.Model, src => src.Item.Model)
            .Map(dest => dest.Year, src => src.Item.Year)
            .Map(dest => dest.Color, src => src.Item.Color)
            .Map(dest => dest.Mileage, src => src.Item.Mileage)
            .Map(dest => dest.ImageUrl, src => src.Item.ImageUrl)
            .Map(dest => dest.Description, src => src.Item.Description);

        // We already have matching fields, just need to include the item's fields
        // Meaning, for the properties on Auction.Item, use the fields in the source object - Make, Color, Mileage, ect.
        config.NewConfig<CreateAuctionRequestDto, Auction>()
            .Map(dest => dest.Item, src => src);
    }
}