using System.ComponentModel.DataAnnotations;

namespace LMT.Api.Entities
{
    public class T_TaskWorkerMappers
    {
        [Key]
        public int TaskWorkerMapId { get; set; }
        public string TaskID { get; set; }
        public string Worker_Reg_Id { get; set; }
    }
}
