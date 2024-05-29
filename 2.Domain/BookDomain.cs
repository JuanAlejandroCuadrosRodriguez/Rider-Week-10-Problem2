using _3.Data;
using _3.Data.Models;

namespace _2.Domain;

public class BookDomain : IBookDomain
{
    private IBookData _bookData;
    
    public BookDomain(IBookData bookData)
    {
        _bookData = bookData;
    }
    
    public async Task<int> SaveAsync(Book data)
    {
        return await _bookData.SaveAsync(data);
    }

    public async Task<Boolean> UpdateAsync(Book data, int id)
    {
        var existingBook = await _bookData.GetByIdAsync(id);

        return await _bookData.UpdateAsync(data, id);
    }

    public async Task<Boolean> DeleteAsync(int id)
    { 
        return await _bookData.DeleteAsync(id);
    }
}