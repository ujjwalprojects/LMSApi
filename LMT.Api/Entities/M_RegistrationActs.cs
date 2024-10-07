using System.ComponentModel.DataAnnotations;

namespace LMT.Api.Entities
{
    public class M_RegistrationActs
    {
        [Key]
        public int Reg_Act_Id { get; set; }
        public string Reg_Act_Name { get; set; } = string.Empty;
    }
}
