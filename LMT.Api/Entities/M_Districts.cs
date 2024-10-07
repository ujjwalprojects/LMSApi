using System.ComponentModel.DataAnnotations;

namespace LMT.Api.Entities
{
    public class M_Districts
    {
        [Key]
        public int District_Id { get; set; }
        public string District_Name { get; set; } = string.Empty;
        public int District_State_Id { get; set; }
    }
}
