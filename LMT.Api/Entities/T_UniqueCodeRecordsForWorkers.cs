using System.ComponentModel.DataAnnotations;

namespace LMT.Api.Entities
{
    public class T_UniqueCodeRecordsForWorkers
    {
        [Key]
        public int Id { get; set; }
        public int LastUniqueNumber { get; set; }
    }
}
