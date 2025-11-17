using System;

namespace SM.FMA.Components.Pages.ContractComponents
{
    public class ContractDto
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public string ScanUri { get; set; } = string.Empty;
        public Guid FacultyMemberId { get; set; }
        // Optionally add more fields if needed
    }
}
