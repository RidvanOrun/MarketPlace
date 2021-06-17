using AutoMapper;
using MarketPlace.API.Model.DTOs;
using MarketPlace.API.Services.Interface;
using MarketPlace.DomainLayer.Entities.Concrete;
using MarketPlace.DomainLayer.Enums;
using MarketPlace.DomainLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketPlace.API.Services.Concrete
{
    public class CategoryService:ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<Category>> CategoryList()
        {
            return await _unitOfWork.CategoryRepository.Get(x => x.Status != Status.Passive);
        }

        public async Task Create(CategoryDTO categoryDTO)
        {
            Category category = _mapper.Map<CategoryDTO, Category>(categoryDTO);

            await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.Commit();
        }

        public async Task Delete(CategoryDTO categoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.FirstOrDefault(x => x.id == categoryDTO.id);
            if (category != null)
            {
                _unitOfWork.CategoryRepository.Delete(category);
                await _unitOfWork.Commit();
            }
        }

        public async Task Update(CategoryDTO categoryDTO)
        {
            var category = await _unitOfWork.CategoryRepository.FirstOrDefault(x => x.id == categoryDTO.id);

            if (category != null)
            {
                category.id = categoryDTO.id;
                category.CategoryName = categoryDTO.CategoryName;
                category.Description = categoryDTO.Description;
            }

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.Commit();
        }

        public async Task<CategoryDTO> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.FirstOrDefault(x=>x.id==id);

            return _mapper.Map<CategoryDTO>(category);
        }       

    }
}
