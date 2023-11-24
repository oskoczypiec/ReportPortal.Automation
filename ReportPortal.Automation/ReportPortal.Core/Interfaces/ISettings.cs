namespace ReportPortal.Core.Interfaces
{
    public interface ISettings
    {
        string URL { get; set; }
        string Browser {  get; set; }
        string User {  get; set; }
        string Pass { get; set; }
        string BearerKey { get; set; }
    }
}
