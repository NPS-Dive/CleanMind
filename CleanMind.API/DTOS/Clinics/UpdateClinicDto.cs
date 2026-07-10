using System.ComponentModel.DataAnnotations;

namespace CleanMind.API.DTOS.Clinics;

public class UpdateClinicDto
{
    [Required]
    [StringLength(150)]
    public required string Name { get; set; }
    }