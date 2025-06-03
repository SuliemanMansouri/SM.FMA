using MudBlazor.Extensions;
using SM.FMA.Data.Enums;

namespace SM.FMA.Data.Entities
{
    public class FacultyMember
    {
        public FacultyMember()
        {
            Id = Guid.NewGuid();
            NameAr = string.Empty;
            NameEn = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }
        public FacultyMember(Guid id, string nameAr, string nameEn, string email, string phoneNumber)
        {
            Id = id;
            NameAr = nameAr;
            NameEn = nameEn;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public Guid Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public char Sex { get; set; }
        public DateTime DOB { get; set; }
        public string POB { get; set; }
        public string NID { get; set; }
        public ContractualDesignation ContractualDesignation { get; set; } = ContractualDesignation.None; //التعيين
        public string EmployeeId { get; set; } //رقم الموظف
        public string AcademicId { get; set; } //رقم الأكاديمي
        public string RegistrationNumber { get; set; }  //رقم القيد
        public string FinancialNumber { get; set; }
        public string IBAN { get; set; }
        public string SSN { get; set; }                 //رقم الضمان الاجتماعي
        public string BranchName { get; set; }
        public string BankName { get; set; }

        public DateTime? DateOfJoining { get; set; }
        public List<Certificate> Certificates { get; set; } = [];   //certificates history
        public List<FacultyRank> FacultyRanks { get; set; } = [];   //promotion history الترقيات العلمية
        public List<Publication> Publications { get; set; } = [];   //publications history المنشورات العلمية
        public List<FacultyPosition> FacultyPositions { get; set; } = [];  //positions history المناصب العلمية


    }
}
