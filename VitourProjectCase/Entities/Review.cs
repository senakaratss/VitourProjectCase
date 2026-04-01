using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VitourProjectCase.Entities
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReviewId { get; set; }
        public string NameSurname { get; set; }
        public string Comment { get; set; }
        public double Score { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool Status { get; set; }
        public string TourId { get; set; }

        public int GuideScore { get; set; }
        public int TransportationScore { get; set; }
        public int AccommodationScore { get; set; }
        public int ValueForMoneyScore { get; set; }

    }
}
