using System.ComponentModel.DataAnnotations;

namespace LMT.Api.Entities
{
    public class M_JobRoles
    {
        [Key]
        public int Job_Role_Id { get; set; }
        public string Job_Role_Name { get; set; } = string.Empty;
    }
}
