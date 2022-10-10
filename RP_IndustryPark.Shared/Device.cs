namespace RP_IndustriPark.Shared
{
    public class Device
    {
        
        public string Id { get; set; } = Guid.NewGuid().ToString("n");

        public string? Location { get; set; }

        public DateTime Date { get; set; }

        public string? Type { get; set; }

        public bool Status { get; set; }
    }
}
