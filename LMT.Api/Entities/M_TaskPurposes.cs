using System.ComponentModel.DataAnnotations;

namespace LMT.Api.Entities
{
    public class M_TaskPurposes
    {
        [Key]
        public int Task_Purpose_Id { get; set; }
        public string Task_Purpose_Name { get; set; } = string.Empty;
    }
}
