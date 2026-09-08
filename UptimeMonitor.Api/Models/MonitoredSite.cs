namespace UptimeMonitor.Api.Models
{
    public class MonitoredSite
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool? IsOnline { get; set; }
        public DateTime? LastChecked { get; set; }
    }
}
