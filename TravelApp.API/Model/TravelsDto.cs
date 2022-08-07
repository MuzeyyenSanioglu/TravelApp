namespace TravelApp.API.Model
{
    public class TravelsDto
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string StartingCity { get; set; }
        public string EndCity { get; set; }
        public string Description { get; set; }
        public int NumberOfSeat { get; set; }
        public int NumberOfAvailebleSeat { get; set; }
        public DateTime TravelDate { get; set; }
        public bool IsPublication { get; set; }
    }
}
