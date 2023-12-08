using AutoMapper;
using DTribe.Core.DTO;
using DTribe.Core.Entities;
using DTribe.Core.IRepositories;
using DTribe.Core.ResponseObjects;
using DTribe.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTribe.Core.Services
{
    public interface ICategoryService
    {
        Task<StandardResponse<List<CategoriesDTO>>> GetUserCategoryList(string UserID);
        Task<StandardResponse<List<CategoryItemDTO>>> GetUserCategoryItemList(string UserID, string categoryId);
        Task<StandardResponse<List<GlobalCategoriesDTO>>> GetGlobalCategoryList();
        Task<StandardResponse<List<GlobalCategoryItemDTO>>> GetGlobalCategoryItemList(string categoryId);
        Task<StandardResponse<object>> InsertCategoryItem(string userID, List<CategoryItemDTO> category);
        Task<StandardResponse<object>> UpdateCategoryItem(string UuerID, List<CategoryItemDTO> category);
        Task<StandardResponse<object>> DeleteCategoryItem(string userID, List<CategoryItemDTO> category);

        Task<StandardResponse<object>> InsertCategory(string userID, List<CategoriesDTO> category);
        Task<StandardResponse<object>> UpdateCategory(string userID, List<CategoriesDTO> category);
        Task<StandardResponse<object>> DeleteCategory(string userID, List<CategoriesDTO> category);
    }
    public class CategoryService : ICategoryService
    {
        private static ICategoriesRepository _categoryRepository { get; set; }
        private readonly IMapper _mapper;
        public CategoryService(ICategoriesRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }



        public async Task<StandardResponse<List<CategoriesDTO>>> GetUserCategoryList(string UserID)
        {
            var response = new StandardResponse<List<CategoriesDTO>>();

            List<Categories>? category = await _categoryRepository.GetUserCategoriesListAsync(UserID);
            List<CategoriesDTO>? categoriesdto = _mapper.Map<List<CategoriesDTO>>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, UserID);
                return response;
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }

        public async Task<StandardResponse<List<CategoryItemDTO>>> GetUserCategoryItemList(string UserID, string categoryId)
        {
            var response = new StandardResponse<List<CategoryItemDTO>>();

            List<CategoryItem>? category = await _categoryRepository.GetUserCategoriesItemListAsync(UserID, categoryId);
            List<CategoryItemDTO>? categoriesdto = _mapper.Map<List<CategoryItemDTO>>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, UserID);
                return response;
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }

        public async Task<StandardResponse<List<GlobalCategoriesDTO>>> GetGlobalCategoryList()
        {
            var response = new StandardResponse<List<GlobalCategoriesDTO>>();

            List<GlobalCategories>? category = await _categoryRepository.GetGlobalCategoriesListAsync();
            List<GlobalCategoriesDTO>? categoriesdto = _mapper.Map<List<GlobalCategoriesDTO>>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, null);
                return response;
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }

        public async Task<StandardResponse<List<GlobalCategoryItemDTO>>> GetGlobalCategoryItemList(string categoryId)
        {
            var response = new StandardResponse<List<GlobalCategoryItemDTO>>();

            List<GlobalCategoryItems>? category = await _categoryRepository.GetGlobalCategoriesItemsListAsync(categoryId);
            List<GlobalCategoryItemDTO>? categoriesdto = _mapper.Map<List<GlobalCategoryItemDTO>>(category);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, null);
                return response;
            }
            response.Status = ResponseStatus.Success;
            response.Data = categoriesdto;
            return response;
        }


        public async Task<StandardResponse<object>> InsertCategory(string UserID, List<CategoriesDTO> category)
        {
            List<Categories> categorynew = new List<Categories>();
            var response = new StandardResponse<object>();
            
            try
            {
                //TODO: validations here
                foreach (var categorydetails in category)
                {
                    categorynew.Add(new Categories
                    {
                        IDX = Guid.NewGuid(),
                        CategoryID = categorydetails.CategoryID,
                        CategoryName = categorydetails.CategoryName,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    });
                }

                await _categoryRepository.InsertCategory(UserID, categorynew);

                response.Status = ResponseStatus.Success;
                response.Message = $"Insert successful.)";
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
        public async Task<StandardResponse<object>> UpdateCategory(string UserID, List<CategoriesDTO> category)
        {
            List<Categories> categorynew = new List<Categories>();
            var response = new StandardResponse<object>();

            try
            {
                //TODO: validations here
                foreach (var categorydetails in category)
                {
                    categorynew.Add(new Categories
                    {
                        CategoryID = categorydetails.CategoryID,
                        CategoryName = categorydetails.CategoryName,
                        UpdatedDate = DateTime.Now,
                    });
                }

                await _categoryRepository.UpdateCategory(UserID, categorynew);

                response.Status = ResponseStatus.Success;
                response.Message = $"Insert successful.)";
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

        public async Task<StandardResponse<object>> DeleteCategory(string UserID, List<CategoriesDTO> category)
        {
            throw new NotImplementedException();
        }

        //Category Item
        public async Task<StandardResponse<object>> InsertCategoryItem(string userID, List<CategoryItemDTO> category)
        {
            List<CategoryItem> categorynew = new List<CategoryItem>();
            var response = new StandardResponse<object>();

            try
            {
                //TODO: validations here
                foreach (var categorydetails in category)
                {
                    categorynew.Add(new CategoryItem
                    {
                        IDX = Guid.NewGuid(),
                        UserID = userID,
                        CategoryID = categorydetails.CategoryID,
                        CategoryName = categorydetails.CategoryName,
                        CategoryItemID = categorydetails.CategoryItemID,
                        CategoryItemName = categorydetails.CategoryItemName,
                        CreatedDate = DateTime.Now,
                        UpdatedDate = DateTime.Now,
                    });
                }

                await _categoryRepository.InsertCategorItem(userID, categorynew);

                response.Status = ResponseStatus.Success;
                response.Message = $"Insert successful.)";
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

        public async Task<StandardResponse<object>> UpdateCategoryItem(string userID, List<CategoryItemDTO> category)
        {
            List<CategoryItem> categoryupdate = new List<CategoryItem>();
            var response = new StandardResponse<object>();

            try
            {
                //TODO: validations here
                foreach (var categorydetails in category)
                {
                    categoryupdate.Add(new CategoryItem
                    {
                        UserID = userID,
                        CategoryID = categorydetails.CategoryID,
                        CategoryName = categorydetails.CategoryName,
                        CategoryItemID = categorydetails.CategoryItemID,
                        CategoryItemName = categorydetails.CategoryItemName,
                        UpdatedDate = DateTime.Now,
                    });
                }

                await _categoryRepository.UpdateCategorItem(userID, categoryupdate);

                response.Status = ResponseStatus.Success;
                response.Message = $"Update successful.)";
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

        public async Task<StandardResponse<object>> DeleteCategoryItem(string userID, List<CategoryItemDTO> category)
        {
            List<CategoryItem>? categoryitem = _mapper.Map<List<CategoryItem>>(category);
            var response = new StandardResponse<object>();

            try
            {

                await _categoryRepository.DeleteCategorItem(userID, categoryitem);

                response.Status = ResponseStatus.Success;
                response.Message = $"Delete successful.)";
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

    }
}
