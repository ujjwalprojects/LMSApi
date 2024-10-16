namespace LMT.Api.DTOs
{
    public class GetEstablishmentRegistrationReportDTO
    {
        public int Estd_Id { get; set; }
        public string Estd_Name { get; set; }
        public string Estd_Owner_Name { get; set; }
        public string Estd_Contact_No { get; set; }
        public string Estd_Reg_No { get; set; }
        public string Estd_TradeLicense_No { get; set; }
        public string Reg_Act_Name { get; set; }
        public DateTime Estd_Reg_Valid_From { get; set; }
        public DateTime Estd_Reg_Valid_Upto { get; set; }
        public string Estd_Remarks { get; set; }
        public string Estd_Description { get; set; }
        public string Estd_IsRegistered { get; set; }
    }
}
