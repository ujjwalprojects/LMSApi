using System.ComponentModel.DataAnnotations;

namespace LMT.Api.Entities
{
    public class M_BlockMunicipals
    {
        [Key]
        public int Block_Municipal_Id { get; set; }
        public string Block_Municipal_Name { get; set; } = string.Empty;
        public int Block_Municipal_Dist_Id { get; set; }
    }
}
