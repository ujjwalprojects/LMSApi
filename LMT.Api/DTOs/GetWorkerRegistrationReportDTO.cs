namespace LMT.Api.DTOs
{
    public class GetWorkerRegistrationReportDTO
    {
        public string Worker_Reg_Id { get; set; }
        public string Worker_Name { get; set; }
        public string Worker_Aadhaar_No { get; set; }
        public string Worker_VoterCard_No { get; set; }
        public string Worker_eShram_No { get; set; }
        public DateTime Worker_DOB { get; set; }
        public string Worker_Gender { get; set; }
        public string Worker_Contact_No { get; set; }
        public string Present_StateName { get; set; }
        public string Present_DistrictName { get; set; }
        public string Worker_Persent_Address { get; set; }
        public string Permanent_StateName { get; set; }
        public string Worker_Permanent_Address { get; set; }
        public string WorkerType_Name { get; set; }
        public string Estd_Name { get; set; } // Nullable value handled in SQL using ISNULL
        public string Job_Role_Name { get; set; }
        public DateTime Worker_Reg_Valid_From { get; set; }
        public DateTime Worker_Reg_Valid_Upto { get; set; }
        public DateTime Worker_Rec_Last_Updated { get; set; }
        public string Worker_FatherOrHusband_Name { get; set; }
        public string Worker_Religion { get; set; }
        public DateTime Worker_DateOfEntry_InSikkim { get; set; }
        public string Worker_IsSelfEmployed { get; set; } // "Yes" or "No" based on the case statement
        public string Worker_NameOf_NextOf_Kin { get; set; }
        public string Worker_AddressOf_NextOf_Kin { get; set; }
        public string Worker_Health_Status { get; set; }
        public string Worker_Registration_Type { get; set; }
        public string Worker_Employment_Type { get; set; }
    }
}
