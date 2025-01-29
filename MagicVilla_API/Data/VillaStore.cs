using MagicVilla_API.Models.Dto;

namespace MagicVilla_API.Data;

public static class VillaStore
{
    public static List<VillaDto> villaList = new List<VillaDto>
    {
        new VillaDto{Id = 1, Name="Pool View", Occupancy = 1, Sqft = 300},
        new VillaDto{Id = 2, Name="Milk View", Occupancy = 5, Sqft = 100},
    };
}