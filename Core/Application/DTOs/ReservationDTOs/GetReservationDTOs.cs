namespace Application.DTOs.ReservationDTOs
{
    public class GetReservationDTOs
    {
        public string CustomerId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
