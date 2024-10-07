using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMT.Api.DTOs
{
    public class GetT_EstablishmentDTO
    {
        public int Estd_Id { get; set; }
        public string Estd_Name { get; set; } = string.Empty;
        public string Estd_Owner_Name { get; set; } = string.Empty;
        public string Estd_Contact_No { get; set; } = string.Empty;
        public DateTime Estd_Reg_Valid_Upto { get; set; }
    }
}
