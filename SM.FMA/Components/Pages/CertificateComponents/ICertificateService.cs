namespace SM.FMA.Components.Pages.CertificateComponents;

public interface ICertificateService
{
    Task<List<CertificateDto>> GetCertificatesAsync(Guid facultyMemberId);
    Task<CertificateDto> GetCertificateByIdAsync(Guid id);
    Task<CertificateDto> UpsertCertificateAsync(CertificateDto certificate);
    Task DeleteCertificateAsync(Guid id);
} 