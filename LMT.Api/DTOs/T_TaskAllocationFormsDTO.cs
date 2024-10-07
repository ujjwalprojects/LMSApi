namespace LMT.Api.DTOs
{
    public class T_TaskAllocationFormsDTO
    {
        public string Task_Id { get; set; }
        public string Task_Name { get; set; } = string.Empty;
        public int Task_Task_Purpose_Id { get; set; }
        public string Task_Description { get; set; } = string.Empty;
        public DateTime Task_Creation_Date { get; set; }
        public string Tasks_Created_By_Id { get; set; } = string.Empty;
        public string Task_Status { get; set; } = string.Empty;
        public int? Task_Estd_Id { get; set; }
        public string Task_Assigned_To_Id { get; set; } = string.Empty;
        public string Task_Assigned_To_Name { get; set; } = string.Empty;
        public DateTime Task_Assigned_Date { get; set; }
        public string? Task_Completion_Remarks { get; set; } = string.Empty;
        public DateTime? Task_Completion_Date { get; set; }
        public string? Task_Site_Image { get; set; } = string.Empty;
    }
}
