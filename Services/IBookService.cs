using HttpRequest.Models;

namespace HttpRequest.Services
{
    public interface IBookService
    {
        Task<bool> Find(int id);
        Task<IEnumerable<Book>> Get();
        Task<Book> GetById(int id);
        Task Add(Book book);
        Task<Book> Update(Book book);
        Task<bool> Delete(int id);
    }
}
