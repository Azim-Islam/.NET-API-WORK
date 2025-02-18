using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Models.Dto;

public class VillaDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public double Rate { get; set; }
    public int Sqft { get; set; }
    public int Occupancy { get; set; }
    public string Image { get; set; }
}