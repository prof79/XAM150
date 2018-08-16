// BookManager.cs

namespace BookClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class BookManager
    {
        #region Fields

        private const string Url =
            "http://xam150.azurewebsites.net/api/books/";

        private const string JsonMime =
            "application/json";

        private string _authorizationKey;

        #endregion

        #region Book REST API

        private async Task<HttpClient> GetClientAsync()
        {
            var client = new HttpClient();

            // First time - no authorization key
            if (String.IsNullOrWhiteSpace(_authorizationKey))
            {
                var loginUri = $"{Url}login";

                // TODO: Error handling
                var jsonString =
                    await client.GetStringAsync(loginUri);

                _authorizationKey =
                    JsonConvert.DeserializeObject<string>(jsonString);
            }

            var mediaType =
                new MediaTypeWithQualityHeaderValue(JsonMime);

            client
                .DefaultRequestHeaders
                .Accept
                .Add(mediaType);

            client
                .DefaultRequestHeaders
                .Add("Authorization", _authorizationKey);

            return client;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var client = await GetClientAsync();

            // TODO: Error handling
            var booksJson = await client.GetStringAsync(Url);

            return
                JsonConvert
                    .DeserializeObject<IEnumerable<Book>>(booksJson);
        }

        public async Task<Book> AddAsync(string title, string author, string genre)
        {
            var book = new Book
            {
                ISBN = String.Empty,
                Title = title,
                Authors = new List<string> { { author } },
                Genre = genre,
                PublishDate = DateTime.Now
            };

            var jsonBook =
                JsonConvert.SerializeObject(book);

            var client = await GetClientAsync();

            var content =
                new StringContent(jsonBook, Encoding.UTF8, JsonMime);

            // TODO: Error handling
            var postResponse =
                await client.PostAsync(Url, content);

            // TODO: Error handling
            var bookResponseJson = await
                postResponse.Content.ReadAsStringAsync();

            return
                JsonConvert.DeserializeObject<Book>(bookResponseJson);
        }

        public Task Update(Book book)
        {
            // TODO: use PUT to update a book
            throw new NotImplementedException();
        }

        public Task Delete(string isbn)
        {
            // TODO: use DELETE to delete a book
            throw new NotImplementedException();
        }

        #endregion
    }
}
