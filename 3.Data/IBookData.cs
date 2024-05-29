using _3.Data.Models;

namespace _3.Data;

public interface IBookData
{
    Task<int> SaveAsync(Book data);
    
    Task<Boolean> UpdateAsync(Book data, int id);
    
    Task<Boolean> DeleteAsync(int id);
    
    Task<List<Book>> GetAllAsync();
    
    Task<List<Book>> getSearchedAsync(string title, string author, int? year);
    
    Task<Book> GetByIdAsync(int Id);
}