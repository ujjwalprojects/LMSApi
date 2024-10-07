﻿namespace LMT.Api.DTOs
{
    public class T_EstablishmentRegistrationsDTO
    {
        public int Estd_Id { get; set; }
        public string Estd_Name { get; set; } = string.Empty;
        public string Estd_Owner_Name { get; set; } = string.Empty;
        public string Estd_Contact_No { get; set; } = string.Empty;
        public string? Estd_Reg_No { get; set; }
        public string? Estd_TradeLicense_No { get; set; }
        public int? Estd_Reg_Act_Id { get; set; }
        public DateTime? Estd_Reg_Valid_From { get; set; }
        public DateTime? Estd_Reg_Valid_Upto { get; set; }
        public DateTime? Estd_Rec_Last_Updated { get; set; }
        public string? Estd_Remarks { get; set; }
        public string? Estd_Description { get; set; }
        public bool Estd_IsRegistered { get; set; }
    }
}
