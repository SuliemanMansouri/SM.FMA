using Microsoft.EntityFrameworkCore;
using SM.FMA.Data;
using SM.FMA.Data.Entities;

namespace SM.FMA.Components.Pages.CertificateComponents;

public class CertificateService : ICertificateService
{
    private readonly ApplicationDbContext _context;

    public CertificateService(ApplicationDbContext context)
    {
        _context = context;
    }

    //public async Task<List<CertificateDto>> GetCertificatesAsync(Guid facultyMemberId)
    //{
    //    return await _context.Certificates
    //        .Where(c => c.FacultyMemberId == facultyMemberId)
    //        .Select(c => new CertificateDto
    //        {
    //            Id = c.Id,
    //            FacultyMemberId = c.FacultyMemberId,
    //            Degree = c.Degree,
    //            Institution = c.Institution,
    //            DateAwarded = c.DateAwarded
    //        })
    //        .ToListAsync();
    //}

    //public async Task<CertificateDto> GetCertificateByIdAsync(Guid id)
    //{
    //    var certificate = await _context.Certificates.FindAsync(id)
    //        ?? throw new KeyNotFoundException("Certificate not found");

    //    return new CertificateDto
    //    {
    //        Id = certificate.Id,
    //        FacultyMemberId = certificate.FacultyMemberId,
    //        Degree = certificate.Degree,
    //        Institution = certificate.Institution,
    //        DateAwarded = certificate.DateAwarded
    //    };
    //}

    //public async Task<CertificateDto> UpsertCertificateAsync(CertificateDto certificateDto)
    //{
    //    var certificate = await _context.Certificates.FindAsync(certificateDto.Id);

    //    if (certificate == null)
    //    {
    //        certificate = new Certificate
    //        {
    //            Id = Guid.NewGuid(),
    //            FacultyMemberId = certificateDto.FacultyMemberId
    //        };
    //        _context.Certificates.Add(certificate);
    //    }

    //    certificate.Degree = certificateDto.Degree;
    //    certificate.Institution = certificateDto.Institution;
    //    certificate.DateAwarded = certificateDto.DateAwarded;

    //    await _context.SaveChangesAsync();

    //    certificateDto.Id = certificate.Id;
    //    return certificateDto;
    //}

    //public async Task DeleteCertificateAsync(Guid id)
    //{
    //    var certificate = await _context.Certificates.FindAsync(id)
    //        ?? throw new KeyNotFoundException("Certificate not found");

    //    _context.Certificates.Remove(certificate);
    //    await _context.SaveChangesAsync();
    //}
} 