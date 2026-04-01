using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VitourProjectCase.Entities
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReservationId { get; set; }
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public int PersonCount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime ReservationDate { get; set; }
        public string ReservationCode { get; set; }
        public string TourId { get; set; }
    }
}
