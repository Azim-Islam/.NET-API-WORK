﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_API.Models.Dto;

public class VillaNumberCreateDto
{
    [Required]
    public int VillaNo { get; set; }
    public string SpecialDetails { get; set; }
    [ForeignKey("Villa")]
    public int VillaID { get; set; }

}