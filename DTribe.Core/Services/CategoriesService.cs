using AutoMapper;
using DTribe.Core.DTO;
using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using DTribe.Core.ResponseObjects;
using DTribe.DB.Entities;

namespace DTribe.Core.Services
{
    public interface ICategoriesService
    {
        //Task<StandardResponse<IEnumerable<UserCategoriesSearchResult>>> GetPostedList();
        //Task<StandardResponse<IEnumerable<UserCategoriesDTO>>> GetCategoriesSearchAsync(string searchString, string UserID, int distance, string distanceType, string sectionID, double userLatitude, double userLongitude, string city);
        Task<StandardResponse<GlobalCategoriesDTO>> GetUserCategoriesAsync(string categoryID);
        Task<StandardResponse<IEnumerable<UserCategoriesSearchBySPDTO>>> GetPostedList();
        Task<StandardResponse<IEnumerable<UserCategoriesSearchBySPDTO>>> GetPostedListBySearch(string searchString, string distanceType, string sectionID);
    }
    public class CategoriesService : ICategoriesService
    {
        private static ICategoriesRepository _catRepository { get; set; }
        private static IUserInfoService _userinfoService { get; set; }
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IMapper _mapper;
        public CategoriesService(ICategoriesRepository categoryRepository,IUserInfoService userInfoService,IUserInfoRepository userInfoRepository, IMapper mapper)
        {
            _userInfoRepository = userInfoRepository;
            _userinfoService= userInfoService;
            _catRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<StandardResponse<IEnumerable<UserCategoriesSearchBySPDTO>>> GetPostedListBySearch(string searchString, string distanceType, string sectionID)
        {
            var response = new StandardResponse<IEnumerable<UserCategoriesSearchBySPDTO>>();
            string userId= _userinfoService.GetUserId();
            UserInfo user = await _userInfoRepository.GetUserInfoAsync(userId);
            IEnumerable<UserCategoriesSearchResult>? category = await _catRepository.GetPostedListBySearch(searchString, userId, user.Latitude, user.Longitude, distanceType, user.CityLocationID, sectionID);
            
            IEnumerable<UserCategoriesSearchBySPDTO> ? categoriesdto = _mapper.Map<IEnumerable<UserCategoriesSearchBySPDTO>>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, null);
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }
        public async Task<StandardResponse<IEnumerable<UserCategoriesSearchBySPDTO>>> GetPostedList()
        {
            var response = new StandardResponse<IEnumerable<UserCategoriesSearchBySPDTO>>();
            string userId = _userinfoService.GetUserId();
            UserInfo user = await _userInfoRepository.GetUserInfoAsync(userId);
            IEnumerable<UserCategoriesSearchResult>? category = await _catRepository.GetPostedList(userId, user.Latitude, user.Longitude);

            IEnumerable<UserCategoriesSearchBySPDTO>? categoriesdto = _mapper.Map<IEnumerable<UserCategoriesSearchBySPDTO>>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, null);
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }

        public async Task<StandardResponse<GlobalCategoriesDTO>> GetUserCategoriesAsync(string categoryID)
        {
            var response = new StandardResponse<GlobalCategoriesDTO>();

            GlobalCategories? category = await _catRepository.GetUserCategoriesAsync(categoryID);
            GlobalCategoriesDTO? categoriesdto = _mapper.Map<GlobalCategoriesDTO>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, null);
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }

        //private string CalculatePostedTime(DateTime createdDate)
        //{
        //    TimeSpan timeSpan = DateTime.UtcNow - createdDate;

        //    if (timeSpan.TotalHours >= 24)
        //    {
        //        return createdDate.ToString("yyyy-MM-dd");
        //    }
        //    else
        //    {
        //        int hours = (int)timeSpan.TotalHours;
        //        int minutes = timeSpan.Minutes;
        //        return $"{hours}h {minutes}m ago";
        //    }
        //}
    }


}
