namespace Application.DTOs.ReservationDTOs
{
    public class GetReservationDTOs
    {
        public string CustomerId { get; set; }
        public string RoomId { get; set; }
        public bool IsPayed { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
