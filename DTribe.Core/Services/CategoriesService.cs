using AutoMapper;
using DTribe.Core.DTO;
using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using DTribe.Core.ResponseObjects;
using DTribe.Core.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.Services
{
    public interface ICategoriesService
    {
        //Task<StandardResponse<IEnumerable<UserCategoriesDTO>>> GetCategoriesSearchAsync(string searchString, string UserID, int distance, string distanceType, string sectionID, double userLatitude, double userLongitude, string city);
        Task<StandardResponse<IEnumerable<UserCategoriesSearchBySPDTO>>> GetCategoriesSearchBySPAsync(string searchString, string UserID, double userLatitude, double userLongitude, string distanceType, string city, string sectionID);
    }
    public class CategoriesService : ICategoriesService
    {
        private static ICategoriesRepository _catRepository { get; set; }
        private readonly IMapper _mapper;
        public CategoriesService(ICategoriesRepository categoryRepository, IMapper mapper)
        {
            _catRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<StandardResponse<IEnumerable<UserCategoriesSearchBySPDTO>>> GetCategoriesSearchBySPAsync(string searchString, string UserID, double userLatitude, double userLongitude, string distanceType, string city, string sectionID)
        {
            var response = new StandardResponse<IEnumerable<UserCategoriesSearchBySPDTO>>();

            IEnumerable<UserCategoriesSearchResult>? category = await _catRepository.GetCategoriesSearchBySPAsync(searchString, UserID, userLatitude, userLongitude, distanceType, city, sectionID);
            
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
    }
}
