

namespace PlannerApp.Components.Models;


public class CardComponent
{

    public int IdTask { get; set; } 
    public string Title { get; set; }   = string.Empty;

    public List<TagComponent> Tags { get; set; } = new List<TagComponent>();
    public string Description  { get; set; } = string.Empty;
}
