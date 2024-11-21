namespace Garage301.Models
{
    public class Receipt
    {
        public string RegistrationNumber { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime CheckoutTime { get; set; }
        public string ParkedDuration => $"{(int)(CheckoutTime - ArrivalTime).TotalHours} houes, {(CheckoutTime - ArrivalTime).Minutes} minutes";

        public decimal ParkingCost { get; set; }
    }
}
