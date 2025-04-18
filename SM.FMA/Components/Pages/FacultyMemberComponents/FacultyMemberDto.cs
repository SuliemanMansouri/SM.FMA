namespace SM.FMA.Components.Pages.FacultyMemberComponents
{
    public class FacultyMemberDto
    {
        public FacultyMemberDto()
        {
            Sex= 'M';
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
        public string EmployeeId { get; set; } //رقم الموظف
        public string RegistrationNumber { get; set; }  //رقم القيد
        public string FinancialNumber { get; set; }
        public string IBAN { get; set; }
        public string SSN { get; set; }                 //رقم الضمان الاجتماعي
        public string BranchName { get; set; }
        public string BankName { get; set; }
        public int PublicationsCount { get; set; }
        public int PapersCount { get; set; }
        public int BooksCount { get; set; }


    }
}
