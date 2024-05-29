using _1.API.Response;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace _1.API.Mapper;

public class ModelToResponse : Profile
{
    public ModelToResponse()
    {
        CreateMap<Book, BookResponse>();
    }
}