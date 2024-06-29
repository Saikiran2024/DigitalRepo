using AutoMapper;
using DTribe.Core.DTO;
using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using DTribe.Core.ResponseObjects;
using DTribe.Core.Utilities;

namespace DTribe.Core.Services
{
    public interface IUserCategoryService
    {
        Task<StandardResponse<UserCategoriesDTO>> GetCategoryDetailsByIDX(string Uscid);
        Task<StandardResponse<List<UserCategoriesDTO>>> GetUserCategoriesListAsync(string UserID, string sectionID);
        Task<StandardResponse<List<UserCategoriesDTO>>> CategoryWiseSearch(string UserID, string searchString, string sectionID);
        Task<StandardResponse<List<UserCategoriesDTO>>> NearLocationWiseSearch(string UserID, string location);
        Task<StandardResponse<object>> Insert(string userID, UserCategoriesDTO category);
        Task<StandardResponse<object>> Update(string userID, UserCategoriesDTO category);
        Task<StandardResponse<object>> Delete(string userID, string USCID);
    }
    public class UserCategoryService : IUserCategoryService
    {
        private static IUserCategoriesRepository _categoryRepository { get; set; }
        private readonly IMapper _mapper;
        private readonly IUserInfoService _userInfoService;
        public UserCategoryService(IUserCategoriesRepository categoryRepository, IMapper mapper,IUserInfoService userInfoService)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _userInfoService.GetUserId();
        }

        public async Task<StandardResponse<UserCategoriesDTO>> GetCategoryDetailsByIDX(string Uscid)
        {
            var response = new StandardResponse<UserCategoriesDTO>();
            string userId = _userInfoService.GetUserId();

            UserCategories? category = await _categoryRepository.GetCategoryDetailsByIDX(userId, Uscid);
            UserCategoriesDTO? categoriesdto = _mapper.Map<UserCategoriesDTO>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, null);
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }
        public async Task<StandardResponse<List<UserCategoriesDTO>>> GetUserCategoriesListAsync(string UserID, string sectionID)
        {
            var response = new StandardResponse<List<UserCategoriesDTO>>();

            IEnumerable<UserCategories>? category = await _categoryRepository.GetUserCategoriesListAsync(UserID, sectionID);
            List<UserCategoriesDTO>? categoriesdto = _mapper.Map<List<UserCategoriesDTO>>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, null);
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }

        public async Task<StandardResponse<List<UserCategoriesDTO>>> CategoryWiseSearch(string UserID, string searchString, string sectionID)
        {
            var response = new StandardResponse<List<UserCategoriesDTO>>();

            IEnumerable<UserCategories>? category = await _categoryRepository.CategoryWiseSearch(UserID, searchString, sectionID);
            List<UserCategoriesDTO>? categoriesdto = _mapper.Map<List<UserCategoriesDTO>>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, null);
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }

        public Task<StandardResponse<List<UserCategoriesDTO>>> NearLocationWiseSearch(string UserID, string location)
        {
            throw new NotImplementedException();
        }

        public async Task<StandardResponse<object>> Insert(string userID, UserCategoriesDTO category)
        {
            var response = new StandardResponse<object>();
            try
            {
                string uscidSting= RandomStringGenerator.GenerateRandomString(5);
                //TODO: validations here
                var categoryNew = new UserCategories
                {
                    //IDX = Guid.NewGuid(),
                    UserCategoryID= userID+ category.SectionID+ category.CategoryID+ uscidSting,
                    CategoryID = category.CategoryID,
                    CategoryName = category.CategoryName,
                    UserID = userID,
                    Title = category.Title,
                    SectionID = category.SectionID,
                    CityLocationID = category.CityLocationID,
                    Description = category.Description,
                    DistanceLocation = category.DistanceLocation,
                    Latitude = category.Latitude,
                    Longitude = category.Longitude,
                    Price = category.Price,
                    Rating = category.Rating,
                    ImageID = category.ImageID,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                };
                await _categoryRepository.Insert(userID, categoryNew);

                response.Status = ResponseStatus.Success;
                response.Message = $""+category.CategoryName+" Category Insert successful.";
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Fatal;
                response.AddError(FrequentErrors.InternalServerError);
                response.AddError(ex.Message);
                //response.AddError($"Error occurred while inserting data for Tenant: {tenantID}, User: {userID}");
                response.AddError(category.ToString());

                //_logger.LogError(ex, "Error occurred while inserting data for Tenant: {TenantID}, User: {UserID}", tenantID, userID);
                throw;
            }

            return response;
        }

        public async Task<StandardResponse<object>> Update(string userID, UserCategoriesDTO category)
        {
            var response = new StandardResponse<object>();

            try
            {
                // TODO: Perform any necessary validations here

                // Retrieve the existing entity from the repository
                var existingCategory = await _categoryRepository.GetCategoryDetailsByIDX(userID, category.UserCategoryID);

                if (existingCategory == null)
                {
                    // Handle the case where the entity to be updated is not found
                    response.Status = ResponseStatus.Error;
                    response.AddError("Category not found.");
                    return response;
                }

                // Update the properties of the existing entity
                existingCategory.CategoryID = category.CategoryID;
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.Title = category.Title;
                existingCategory.SectionID = category.SectionID;
                existingCategory.CityLocationID = category.CityLocationID;
                existingCategory.Description = category.Description;
                existingCategory.DistanceLocation = category.DistanceLocation;
                existingCategory.Latitude = category.Latitude;
                existingCategory.Longitude = category.Longitude;
                existingCategory.Price = category.Price;
                existingCategory.Rating = category.Rating;
                existingCategory.ImageID = category.ImageID;
                existingCategory.UpdatedDate = DateTime.Now;

                // Save the changes to the repository
                await _categoryRepository.Update(userID, existingCategory);

                response.Status = ResponseStatus.Success;
                response.Message = ""+category.CategoryName+" Category Update successful.";
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Fatal;
                response.AddError(FrequentErrors.InternalServerError);
                response.AddError(ex.Message);
                response.AddError(category.ToString());

                // Add any additional error handling or logging as needed
                throw;
            }

            return response;
        }

        public async Task<StandardResponse<object>> Delete(string userID, string USCID)
        {
            var response = new StandardResponse<object>();

            try
            {
                // Retrieve the existing entity from the repository
                
                var existingCategory = await _categoryRepository.GetCategoryDetailsByIDX(userID, USCID);

                if (existingCategory == null)
                {
                    // Handle the case where the entity to be deleted is not found
                    response.Status = ResponseStatus.Error;
                    response.AddError("Category not found.");
                    return response;
                }
                //UserCategories? categoriesdto = _mapper.Map<UserCategories>(category);
                // Remove the entity from the repository
                await _categoryRepository.Delete(userID, existingCategory);

                response.Status = ResponseStatus.Success;
                response.Message = " Category Delete successful.";
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Fatal;
                response.AddError(FrequentErrors.InternalServerError);
                response.AddError(ex.Message);

                // Add any additional error handling or logging as needed
                throw;
            }

            return response;
        }

       

        /// <summary>
        /// //////////////
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>


        //public async Task<StandardResponse<List<CategoriesDTO>>> GetUserCategoryList(string UserID)
        //{
        //    var response = new StandardResponse<List<CategoriesDTO>>();

        //    List<Categories>? category = await _categoryRepository.GetUserCategoriesListAsync(UserID);
        //    List<CategoriesDTO>? categoriesdto = _mapper.Map<List<CategoriesDTO>>(category);
        //    if (category == null)
        //    {
        //        response.Status = ResponseStatus.Error;
        //        response.AddError(FrequentErrors.UserNotFound, UserID);
        //        return response;
        //    }
        //    response.Status = ResponseStatus.Success;
        //    response.Data = categoriesdto;
        //    return response;
        //}

        //public async Task<StandardResponse<List<CategoryItemDTO>>> GetUserCategoryItemList(string UserID, string categoryId)
        //{
        //    var response = new StandardResponse<List<CategoryItemDTO>>();

        //    List<CategoryItem>? category = await _categoryRepository.GetUserCategoriesItemListAsync(UserID, categoryId);
        //    List<CategoryItemDTO>? categoriesdto = _mapper.Map<List<CategoryItemDTO>>(category);
        //    if (category == null)
        //    {
        //        response.Status = ResponseStatus.Error;
        //        response.AddError(FrequentErrors.UserNotFound, UserID);
        //        return response;
        //    }
        //    response.Status = ResponseStatus.Success;
        //    response.Data = categoriesdto;
        //    return response;
        //}

        //public async Task<StandardResponse<List<GlobalCategoriesDTO>>> GetGlobalCategoryList()
        //{
        //    var response = new StandardResponse<List<GlobalCategoriesDTO>>();

        //    List<GlobalCategories>? category = await _categoryRepository.GetGlobalCategoriesListAsync();
        //    List<GlobalCategoriesDTO>? categoriesdto = _mapper.Map<List<GlobalCategoriesDTO>>(category);
        //    if (category == null)
        //    {
        //        response.Status = ResponseStatus.Error;
        //        response.AddError(FrequentErrors.UserNotFound, null);
        //        return response;
        //    }
        //    response.Status = ResponseStatus.Success;
        //    response.Data = categoriesdto;
        //    return response;
        //}




        //public async Task<StandardResponse<object>> InsertCategory(string UserID, List<CategoriesDTO> category)
        //{
        //    List<Categories> categorynew = new List<Categories>();
        //    var response = new StandardResponse<object>();

        //    try
        //    {
        //        //TODO: validations here
        //        foreach (var categorydetails in category)
        //        {
        //            categorynew.Add(new Categories
        //            {
        //                IDX = Guid.NewGuid(),
        //                CategoryID = categorydetails.CategoryID,
        //                CategoryName = categorydetails.CategoryName,
        //                CreatedDate = DateTime.Now,
        //                UpdatedDate = DateTime.Now,
        //            });
        //        }

        //        await _categoryRepository.InsertCategory(UserID, categorynew);

        //        response.Status = ResponseStatus.Success;
        //        response.Message = $"Insert successful.)";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = ResponseStatus.Fatal;
        //        response.AddError(FrequentErrors.InternalServerError);
        //        response.AddError(ex.Message);
        //        //response.AddError($"Error occurred while inserting data for Tenant: {tenantID}, User: {userID}");
        //        response.AddError(category.ToString());

        //        //_logger.LogError(ex, "Error occurred while inserting data for Tenant: {TenantID}, User: {UserID}", tenantID, userID);
        //        throw;
        //    }

        //    return response;
        //}
        //public async Task<StandardResponse<object>> UpdateCategory(string UserID, List<CategoriesDTO> category)
        //{
        //    List<Categories> categorynew = new List<Categories>();
        //    var response = new StandardResponse<object>();

        //    try
        //    {
        //        //TODO: validations here
        //        foreach (var categorydetails in category)
        //        {
        //            categorynew.Add(new Categories
        //            {
        //                CategoryID = categorydetails.CategoryID,
        //                CategoryName = categorydetails.CategoryName,
        //                UpdatedDate = DateTime.Now,
        //            });
        //        }

        //        await _categoryRepository.UpdateCategory(UserID, categorynew);

        //        response.Status = ResponseStatus.Success;
        //        response.Message = $"Insert successful.)";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Status = ResponseStatus.Fatal;
        //        response.AddError(FrequentErrors.InternalServerError);
        //        response.AddError(ex.Message);
        //        //response.AddError($"Error occurred while inserting data for Tenant: {tenantID}, User: {userID}");
        //        response.AddError(category.ToString());

        //        //_logger.LogError(ex, "Error occurred while inserting data for Tenant: {TenantID}, User: {UserID}", tenantID, userID);
        //        throw;
        //    }

        //    return response;
        //}

        //public async Task<StandardResponse<object>> DeleteCategory(string UserID, List<CategoriesDTO> category)
        //{
        //    throw new NotImplementedException();
        //}



    }
}
