using HttpRequest.Models;
using System.Text.Json;

namespace HttpRequest.Services
{
    public class BookService : IBookService
    {
        private readonly string filePath = @"./Data/Books.json";

        public async Task<bool> Find(int id)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                bool found = false;
                var books = await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(stream);
                var book = books.FirstOrDefault(x => x.Id == id);
                if (book != null) found = true;
                return found;
            };
        }

        public async Task<IEnumerable<Book>> Get()
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var books = await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(stream);
                return books;
            };
        }

        public async Task<Book> GetById(int id)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                var books = await JsonSerializer.DeserializeAsync<IEnumerable<Book>>(stream);
                return books.FirstOrDefault(x => x.Id == id);
            };
        }

        public async Task Add(Book book)
        {
            var books = new List<Book>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                books = await JsonSerializer.DeserializeAsync<List<Book>>(stream);

                book.Id = books.Last().Id + 1;
                books.Add(book);
            };

            await WriteFile(books);
        }

        public async Task<Book> Update(Book book)
        {
            var books = new List<Book>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                books = await JsonSerializer.DeserializeAsync<List<Book>>(stream);

                int bookIndex = books.FindIndex(x => x.Id == book.Id);
                books.RemoveAt(bookIndex);
                books.Add(book);
                books.Sort((x, y) => x.Id - y.Id);
            };

            await WriteFile(books);

            return book;
        }

        public async Task<bool> Delete(int id)
        {
            bool result = false;
            var books = new List<Book>();

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                books = await JsonSerializer.DeserializeAsync<List<Book>>(stream);

                int bookIndex = books.FindIndex(x => x.Id == id);
                books.RemoveAt(bookIndex);
            };

            await WriteFile(books);

            return result;
        }

        private async Task WriteFile(List<Book> books)
        {
            using (var stream = new FileStream(filePath, FileMode.Truncate, FileAccess.Write))
            {
                await JsonSerializer.SerializeAsync(stream, books);
            };
        }
    }
}
