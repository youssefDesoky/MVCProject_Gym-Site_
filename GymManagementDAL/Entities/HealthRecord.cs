using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementDAL.Entities;

[Table("Members")] // Make It To Be a Part From Table "Members" 
public class HealthRecord : BaseEntity
{
    public decimal Height { get; set; } // in centimeters
    public decimal Weight { get; set; } // in kilograms
    public string BloodType { get; set; } = null!;
    public string? Note { get; set; }
}
