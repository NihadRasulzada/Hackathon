namespace Application.DTOs.ReservationDTOs
{
    public class GetReservationDTOs
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
