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
    public interface ISectionService
    {
        Task<StandardResponse<List<SectionDTO>>> GetSectionList();
        Task<StandardResponse<List<GlobalSectionCategoriesDTO>>> GetSectionCategoryList(string SectionID);
    }
    public class SectionService : ISectionService
    {
        private static ISectionRepository _sectionRepository { get; set; }
        private readonly IMapper _mapper;
        public SectionService(ISectionRepository sectionRepository, IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }
        public async Task<StandardResponse<List<SectionDTO>>> GetSectionList()
        {
            var response = new StandardResponse<List<SectionDTO>>();

            List<Section>? section = await _sectionRepository.GetSectionList();
            List<SectionDTO>? sections = _mapper.Map<List<SectionDTO>>(section);

            response.Status = ResponseStatus.Success;
            response.Data = sections;
            return response;
        }

        public async Task<StandardResponse<List<GlobalSectionCategoriesDTO>>> GetSectionCategoryList(string SectionID)
        {
            var response = new StandardResponse<List<GlobalSectionCategoriesDTO>>();

            List<GlobalSectionCategories>? section = await _sectionRepository.GetSectionCategoryList(SectionID);
            List<GlobalSectionCategoriesDTO>? sections = _mapper.Map<List<GlobalSectionCategoriesDTO>>(section);

            response.Status = ResponseStatus.Success;
            response.Data = sections;
            return response;
        }
    }
}
