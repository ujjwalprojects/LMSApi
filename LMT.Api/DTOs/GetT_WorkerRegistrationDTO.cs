using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMT.Api.DTOs
{
    public class GetT_WorkerRegistrationDTO
    {
        public string Worker_Reg_Id { get; set; }
        public string Worker_Name { get; set; } = string.Empty;
        public string Worker_Contact_No { get; set; } = string.Empty;
        public string Job_Role_Name { get; set; }
        public string Estd_Name { get; set; }
        public DateTime Worker_Reg_Valid_Upto { get; set; }
    }
}
