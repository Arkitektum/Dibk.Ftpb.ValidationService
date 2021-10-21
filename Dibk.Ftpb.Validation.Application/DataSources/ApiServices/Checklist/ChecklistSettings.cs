namespace Dibk.Ftpb.Validation.Application.DataSources.ApiServices.Checklist
{
    public class ChecklistSettings
    {
        public string BaseAddress { get; set; }
        public string ChecklistUrl { get; set; }
    }
    public interface IChecklistSettings
    {
        string BaseAddress { get; set; }
        string ChecklistUrl { get; set; }
    }
}
