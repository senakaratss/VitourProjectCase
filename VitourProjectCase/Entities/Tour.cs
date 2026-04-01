using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VitourProjectCase.Entities
{
    public class Tour
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TourId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImageUrl { get; set; }
        public string Badge { get; set; }
        public int DayCount { get; set; }
        public int Capacity { get; set; }
        public decimal Price { get; set; }
        public bool Status { get; set; }
        public string CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }

        public string MapLocationImageUrl { get; set; }
        public string MapLocationTitle { get; set; }
        public string MapLocationDescription { get; set; }
    }
}
