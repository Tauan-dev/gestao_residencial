using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Category;
using ApiGastosResidenciais.Application.DTOs.Person;
using ApiGastosResidenciais.Application.DTOs.Transaction;
using ApiGastosResidenciais.Domain.Entities;
using AutoMapper;

namespace ApiGastosResidenciais.Application.Mapping
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<CreatePersonDto, Person>();
            CreateMap<UpdatePersonDto, Person>();


            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();


            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<CreateTransactionDto, Transaction>();
            CreateMap<UpdateTransactionDto, Transaction>();
        }
    }
}