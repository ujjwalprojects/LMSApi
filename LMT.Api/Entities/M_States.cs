using System.ComponentModel.DataAnnotations;

namespace LMT.Api.Entities
{
    public class M_States
    {
        [Key]
        public int State_Id { get; set; }
        public string State_Name { get; set; }=string.Empty;
    }
}
