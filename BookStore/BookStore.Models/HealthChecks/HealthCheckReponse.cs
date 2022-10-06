namespace BookStore.Models.HealthChecks
{
    public class HealthCheckReponse
    {
        public string Status { get; set; } = string.Empty;
        public IEnumerable<IndividualHealthCheckResponse> HealthChecks { get; set; } = Enumerable.Empty<IndividualHealthCheckResponse>();
        public TimeSpan HealthCheckDuration { get; set; }
    }
}
