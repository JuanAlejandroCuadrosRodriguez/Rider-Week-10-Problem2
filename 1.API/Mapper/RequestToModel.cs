using _1.API.Request;
using _3.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace _1.API.Mapper;

public class RequestToModel : Profile
{
    public RequestToModel()
    {
        CreateMap<BookRequest, Book>();
    }
}