using MongoDB.Driver;
using VitourProjectCase.Dtos.ReportDtos;
using VitourProjectCase.Entities;
using VitourProjectCase.Settings;

namespace VitourProjectCase.Services.ReportServices
{
    public class ReportService : IReportService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly IMongoCollection<Review> _reviewCollection;
        private readonly IMongoCollection<Tour> _tourCollection;
        public ReportService(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _reservationCollection = database.GetCollection<Reservation>(_databaseSettings.ReservationCollectionName);
            _reviewCollection = database.GetCollection<Review>(_databaseSettings.ReviewCollectionName);
            _tourCollection = database.GetCollection<Tour>(_databaseSettings.TourCollectionName);
        }
        public async Task<List<KpiCardDto>> GetKpiCardsAsync()
        {
            var now = DateTime.UtcNow;
            var currentMonth = now.Month;
            var currentYear = now.Year;

            var prevDate = now.AddMonths(-1);
            var prevMonth = prevDate.Month;
            var prevYear = prevDate.Year;

            var currentFilter = Builders<Reservation>.Filter.Where(x => x.ReservationDate.Year == currentYear
                                                                    && x.ReservationDate.Month == currentMonth);
            var currentReviewFilter = Builders<Review>.Filter.Where(x => x.ReviewDate.Year == currentYear
                                                                    && x.ReviewDate.Month == currentMonth);
            var prevFilter = Builders<Reservation>.Filter.Where(x => x.ReservationDate.Year == prevYear
                                                                    && x.ReservationDate.Month == prevMonth);
            var prevReviewFilter = Builders<Review>.Filter.Where(x => x.ReviewDate.Year == prevYear
                                                                    && x.ReviewDate.Month == prevMonth);

            var currentReservations = await _reservationCollection.Find(currentFilter).ToListAsync();
            var prevReservations = await _reservationCollection.Find(prevFilter).ToListAsync();

            var currentReviews = await _reviewCollection.Find(currentReviewFilter).ToListAsync();
            var prevReviews = await _reviewCollection.Find(prevReviewFilter).ToListAsync();

            var currentRevenue = currentReservations.Sum(x => x.TotalPrice);
            var prevRevenue = prevReservations.Sum(x => x.TotalPrice);

            var currentReservationCount = currentReservations.Count;
            var prevReservationCount = prevReservations.Count;

            var currentCustomerCount = currentReservations.Sum(x => x.PersonCount);
            var prevCustomerCount = prevReservations.Sum(x => x.PersonCount);

            var currentAvgReviewScore = currentReviews.Any() ? currentReviews.Average(x => x.Score) : 0;
            var prevAvgReviewScore = prevReviews.Any() ? prevReviews.Average(x => x.Score) : 0;

            double CalculateChange(decimal current, decimal prev)
            {
                if (prev == 0)
                {
                    if (current == 0) return 0;
                    return 100;
                }
                return (double)((current - prev) / prev) * 100;
            }
            double CalculateChangeInt(int current, int prev)
            {
                if (prev == 0)
                {
                    if (current == 0) return 0;
                    return 100;
                }
                return ((double)(current - prev) / prev) * 100;
            }

            return new List<KpiCardDto>
            {
                new KpiCardDto
                {
                     Title = "Toplam Gelir",
                     Value = $"₺{currentRevenue:N0}",
                     PreviousValue = $"₺{prevRevenue:N0}",
                     PercentageChange = Math.Round(CalculateChange(currentRevenue, prevRevenue),1),
                     IsIncrease = currentRevenue >= prevRevenue,
                },
                new KpiCardDto
                {
                     Title = "Toplam Rezervasyon",
                     Value = currentReservationCount.ToString(),
                     PreviousValue = prevReservationCount.ToString(),
                     PercentageChange = Math.Round(CalculateChangeInt(currentReservationCount, prevReservationCount),1),
                     IsIncrease = currentReservationCount >= prevReservationCount,
                },
                new KpiCardDto
                {
                     Title = "Toplam Kişi",
                     Value = currentCustomerCount.ToString(),
                     PreviousValue = prevCustomerCount.ToString(),
                     PercentageChange = Math.Round(CalculateChangeInt(currentCustomerCount, prevCustomerCount),1),
                     IsIncrease = currentCustomerCount >= prevCustomerCount,
                },
                new KpiCardDto
                {
                     Title = "Ortalama Puan",
                     Value = currentAvgReviewScore.ToString("0.0"),
                     PreviousValue = prevAvgReviewScore.ToString("0.0"),
                     PercentageChange = Math.Round(CalculateChange((decimal)currentAvgReviewScore, (decimal)prevAvgReviewScore),1),
                     IsIncrease = currentAvgReviewScore >= prevAvgReviewScore,
                }

            };
        }

        public async Task<List<MonthlyGoalDto>> GetMonthlyGoalsAsync()
        {
            var now = DateTime.UtcNow;
            var month = now.Month;
            var year = now.Year;

            // 1. Gelir
            var revenueFilter = Builders<Reservation>.Filter.Where(x => x.ReservationDate.Year == year && x.ReservationDate.Month == month);
            var reservations = await _reservationCollection.Find(revenueFilter).ToListAsync();
            var totalRevenue = reservations.Sum(x => x.TotalPrice);
            var revenueGoal = 500000M;
            // 2. Rezervasyon sayısı
            var totalReservationCount = reservations.Count;
            var reservationGoal = 150;
            // 3. Yeni Müşteri sayısı
            var totalCustomers = reservations.Sum(x => x.PersonCount);
            var customerGoal = 200;
            // 4. Yorum sayısı
            var reviewFilter = Builders<Review>.Filter.Where(x => x.ReviewDate.Year == year && x.ReviewDate.Month == month);
            var reviews = await _reviewCollection.Find(reviewFilter).ToListAsync();
            var totalReviews = reviews.Count;
            var reviewGoal = 50;
            // 5. Ortalama puan
            var avgScore = reviews.Any() ? reviews.Average(x => x.Score) : 0;
            var scoreGoal = 5.0M;
            // 6. Tur sayısı
            var activeTours = await _tourCollection.CountDocumentsAsync(t => t.Status);
            var tourGoal = 10;

            return new List<MonthlyGoalDto>
                    {
                        new MonthlyGoalDto { Name="Gelir Hedefi", CurrentValue=totalRevenue, TargetValue=revenueGoal, Unit="₺"},
                        new MonthlyGoalDto { Name="Rezervasyon", CurrentValue=totalReservationCount, TargetValue=reservationGoal, Unit="rezervasyon"},
                        new MonthlyGoalDto { Name="Yeni Müşteri", CurrentValue=totalCustomers, TargetValue=customerGoal, Unit="müşteri" },
                        new MonthlyGoalDto { Name="Yorum Sayısı", CurrentValue=totalReviews, TargetValue=reviewGoal, Unit="yorum"},
                        new MonthlyGoalDto { Name="Ortalama Puan", CurrentValue=(decimal)avgScore, TargetValue=scoreGoal, Unit="puan" },
                        new MonthlyGoalDto { Name="Tur Sayısı", CurrentValue=activeTours, TargetValue=tourGoal, Unit="aktif tur" },
                    };

        }

        public async Task<List<MonthlyRevenueDto>> GetMonthlyRevenueChartAsync()
        {
            var now = DateTime.UtcNow;
            var result = new List<MonthlyRevenueDto>();

            for (int i = 7; i >= 0; i--)
            {
                var date = now.AddMonths(-i);
                var month = date.Month;
                var year = date.Year;

                var filter = Builders<Reservation>.Filter.Where(x => x.ReservationDate.Year == year
                                                                    && x.ReservationDate.Month == month);

                var resevations = await _reservationCollection.Find(filter).ToListAsync();
                var revenue = resevations.Sum(x => x.TotalPrice);

                result.Add(new MonthlyRevenueDto
                {
                    Month = date.ToString("MMM", new System.Globalization.CultureInfo("tr-TR")),
                    Revenue = revenue
                });
            }
            return result;
        }

        public async Task<List<ResultTopTourDto>> GetTopTourListAsync()
        {
            var reservations = await _reservationCollection.Find(x => true).ToListAsync();
            var tours = await _tourCollection.Find(x => true).ToListAsync();

            var grouped = reservations.GroupBy(r => r.TourId).Select(x => new
            {
                TourId = x.Key,
                TotalReservations = x.Count()
            }).OrderByDescending(y => y.TotalReservations).Take(5).ToList();

            var result = grouped.Select(g =>
            {
                var tour = tours.FirstOrDefault(t => t.TourId == g.TourId);
                return new ResultTopTourDto
                {
                    TourId = g.TourId,
                    Title = tour?.Title ?? "Bilinmiyor",
                    TotalReservations = g.TotalReservations,
                };
            }).ToList();

            return result;
        }
    }
}
