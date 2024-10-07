namespace LMT.Api.DTOs
{
    public class GetTaskWorkersMapDTO
    {
        public string Worker_Reg_Id { get; set; }
        public string Worker_Name { get; set; } = string.Empty;
        public string Worker_Contact_No { get; set; } = string.Empty;
        public string Job_Role_Name { get; set; } = string.Empty;
        public string Estd_Name { get; set; } = string.Empty;
        public DateTime Worker_Reg_Valid_Upto { get; set; }
        public bool IsMapped { get; set; }
    }
}
