namespace Application.DTOs.ReservationDTOs
{
    public class CreateReservationDTOs
    {        
        public string CustomerId { get; set; }        
        public string RoomId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
