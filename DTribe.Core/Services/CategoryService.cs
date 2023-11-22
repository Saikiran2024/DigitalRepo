using AutoMapper;
using DTribe.Core.DTO;
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
        Task<StandardResponse<Categories>> GetCategoryList(string CategotyID);
        Task<StandardResponse<object>> InsertCategory(string UserID, CategoriesDTO category);
    }
    public class CategoryService : ICategoryService
    {
        private static ICategoriesRepository _categoryRepository { get; set; }
        //private readonly IMapper _mapper;
        public CategoryService(ICategoriesRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            //_mapper = mapper;
        }

        public async Task<StandardResponse<Categories>> GetCategoryList(string CategotyID)
        {
            var response = new StandardResponse<Categories>();

            Categories? category = await _categoryRepository.GetCategoriesAsync(CategotyID);
            if (category == null)
            {
                response.Status = ResponseStatus.Error;
                response.AddError(FrequentErrors.UserNotFound, CategotyID);
                return response;
            }
            response.Status = ResponseStatus.Success;
            response.Data = category;
            return response;
        }

        public async Task<StandardResponse<object>> InsertCategory(string UserID, CategoriesDTO category)
        {
            Categories categorynew = new Categories();
            var response = new StandardResponse<object>();
            //Categories categoriesobj = _mapper.Map<Categories>(category);
            try
            {
                //TODO: validations here

                categorynew.IDX = Guid.NewGuid();
                categorynew.CategoryID = category.CategoryID;
                categorynew.CategoryName = category.CategoryName;
                categorynew.CreatedDate = DateTime.Now;
                categorynew.UpdatedDate = DateTime.Now;

                await _categoryRepository.Insert(UserID, categorynew);

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
    }
}
