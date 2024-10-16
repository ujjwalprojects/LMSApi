using System.ComponentModel.DataAnnotations;

namespace LMT.Api.Entities
{
    public class T_WorkerRegistrations
    {
        [Key]
        public string Worker_Reg_Id { get; set; }
        public string Worker_Name { get; set; } = string.Empty;
        public string? Worker_Aadhaar_No { get; set; } 
        public string? Worker_VoterCard_No { get; set; } 
        public string? Worker_eShram_No { get; set; }
        public DateTime Worker_DOB { get; set; }
        public string Worker_Gender { get; set; } = string.Empty;
        public string Worker_Contact_No { get; set; } = string.Empty;
        public int Worker_Present_State_Id { get; set; }
        public int Worker_Present_District_Id { get; set; }
        public string Worker_Persent_Address { get; set; } = string.Empty;
        public int Worker_Permanent_State_Id { get; set; }
        public string Worker_Permanent_Address { get; set; } = string.Empty;
        public int Worker_WorkerType_Id { get; set; }
        public int? Worker_Estd_Id { get; set; }
        public int Worker_Job_Role_Id { get; set; }
        public DateTime? Worker_Reg_Valid_From { get; set; }
        public DateTime? Worker_Reg_Valid_Upto { get; set; }
        public DateTime Worker_Rec_Last_Updated { get; set; }
        public string? Worker_Image { get; set; }
        public string? Worker_IdProof_First { get; set; }
        public string? Worker_IdProof_Second { get; set; }
        public string? Worker_FatherOrHusband_Name { get; set; }
        public string? Worker_Religion { get; set; }
        public DateTime? Worker_DateOfEntry_InSikkim { get; set; }
        public bool Worker_IsSelfEmployed { get; set; }
        public string? Worker_NameOf_NextOf_Kin { get; set; }
        public string? Worker_AddressOf_NextOf_Kin { get; set; }
        public string? Worker_Health_Status { get; set; }
        public string? Worker_Registration_Type { get; set; }
        public string? Worker_Employment_Type { get; set; }
    }
}
