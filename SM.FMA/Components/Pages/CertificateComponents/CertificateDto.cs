using System.ComponentModel.DataAnnotations;

namespace SM.FMA.Components.Pages.CertificateComponents;

public class CertificateDto
{
    public Guid Id { get; set; }
    public Guid FacultyMemberId { get; set; }
    
    [Required]
    public string Degree { get; set; } = string.Empty;
    
    [Required]
    public string Institution { get; set; } = string.Empty;
    
    [Required]
    public DateOnly DateAwarded { get; set; }
} 