
namespace PlannerApp.Database.Models;

public class Taskes
{
    public int TaskId { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string TaskDescription { get; set; } = string.Empty;
    public DateTime? TaskStartDate { get; set; }
    public DateTime? TaskEndDate { get; set; }
    public int TaskProjectId { get; set; }
    public long TaskOwnerId { get; set; }
    public string TaskState { get; set; } = "Pendiente";
}