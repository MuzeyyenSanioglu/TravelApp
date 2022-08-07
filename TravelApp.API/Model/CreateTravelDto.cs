namespace TravelApp.API.Model
{
    public class CreateTravelDto
    {
     
        public string StartingCity { get; set; }
        public string EndCity { get; set; }
        public string Description { get; set; }
        public int NumberOfSeat { get; set; }
        public DateTime TravelDate { get; set; }
        public bool IsPublication { get; set; }
    }
}
