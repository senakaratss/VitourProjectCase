using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VitourProjectCase.Entities
{
    public class TourPlan
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TourPlanId { get; set; }
        public int DayNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TourId { get; set; }
    }
}
