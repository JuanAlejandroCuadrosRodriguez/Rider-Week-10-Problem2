using _3.Data.Models;

namespace _2.Domain;

public interface IBookDomain
{
    Task<int> SaveAsync(Book data);
    
    Task<Boolean> UpdateAsync(Book data, int id);
    
    Task<Boolean> DeleteAsync(int id);
}