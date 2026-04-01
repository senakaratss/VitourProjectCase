using AutoMapper;
using VitourProjectCase.Dtos.CategoryDtos;
using VitourProjectCase.Dtos.GalleryDtos;
using VitourProjectCase.Dtos.ReservationDtos;
using VitourProjectCase.Dtos.ReviewDtos;
using VitourProjectCase.Dtos.TourDtos;
using VitourProjectCase.Dtos.TourPlanDtos;
using VitourProjectCase.Entities;

namespace VitourProjectCase.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

            CreateMap<Tour, CreateTourDto>().ReverseMap();
            CreateMap<Tour, ResultTourDto>().ReverseMap();
            CreateMap<Tour, UpdateTourDto>().ReverseMap();
            CreateMap<Tour, GetTourByIdDto>().ReverseMap();
            CreateMap<Tour, TourCardDto>().ReverseMap();
            CreateMap<Tour, TourDetailDto>().ReverseMap();

            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, ResultReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            CreateMap<Review, GetReviewByIdDto>().ReverseMap();
            CreateMap<Review, ResultReviewByTourIdDto>().ReverseMap();

            CreateMap<TourPlan, CreateTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, ResultTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, UpdateTourPlanDto>().ReverseMap();
            CreateMap<TourPlan, GetTourPlanByIdDto>().ReverseMap();
            CreateMap<TourPlan, ResultTourPlanByTourIdDto>().ReverseMap();

            CreateMap<Gallery, CreateGalleryDto>().ReverseMap();
            CreateMap<Gallery, ResultGalleryDto>().ReverseMap();
            CreateMap<Gallery, UpdateGalleryDto>().ReverseMap();
            CreateMap<Gallery, GetGalleryByIdDto>().ReverseMap();
            CreateMap<Gallery, ResultGalleryByTourIdDto>().ReverseMap();

            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, ResultReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();
            CreateMap<Reservation, GetReservationByIdDto>().ReverseMap();
            CreateMap<Reservation, ResultReservationByTourIdDto>().ReverseMap();


        }
    }
}
