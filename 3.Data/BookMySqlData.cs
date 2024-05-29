using _3.Data.Context;
using _3.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace _3.Data;

public class BookMySqlData : IBookData
{
    private BooksDBContext _booksDbContext;
    
    public BookMySqlData(BooksDBContext booksDbContext)
    {
        _booksDbContext = booksDbContext;
    }
    
    public async Task<int> SaveAsync(Book data)
    {
        data.IsActive = true;
        
        using (var transaction = await _booksDbContext.Database.BeginTransactionAsync())
        {
            try
            {
                _booksDbContext.Books.Add(data);
                await _booksDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        return data.Id;
    }

    public async Task<Boolean> UpdateAsync(Book data, int id)
    {
        using (var transaction = await _booksDbContext.Database.BeginTransactionAsync())
        {
            var bookToUpdate = _booksDbContext.Books.Where(t => t.Id == id).FirstOrDefault();
            bookToUpdate.Title = data.Title;
            bookToUpdate.Author = data.Author;
            bookToUpdate.Year = data.Year;
            
            _booksDbContext.Books.Update(bookToUpdate);
            await _booksDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        return true;
    }

    public async Task<Boolean> DeleteAsync(int id)
    {
        using (var transaction = await _booksDbContext.Database.BeginTransactionAsync())
        {
            var bookToDelete = _booksDbContext.Books.Where(t => t.Id == id).FirstOrDefault();
            bookToDelete.IsActive = false;
            
            await _booksDbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        return true;
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _booksDbContext.Books.Where(t => t.IsActive)
            .ToListAsync();
    }

    public async Task<List<Book>> getSearchedAsync(string title, string author, int? year)
    {
        return await _booksDbContext.Books
            .Where(t => t.IsActive && t.Title.Contains(title) && t.Author.Contains(author) && t.Year >= year)
            .ToListAsync();
    }

    public async Task<Book> GetByIdAsync(int Id)
    {
        return await _booksDbContext.Books.Where(t => t.Id == Id)
            .FirstOrDefaultAsync();
    }
}