namespace Application.DTOs.ReservationDTOs
{
    public class UpdateReservationDTOs
    {
        public string CustomerId { get; set; }
        public string RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
