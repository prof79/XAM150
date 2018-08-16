// BookManager.cs

namespace BookClient.Data
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class BookManager
    {
        #region Fields

        private const string Url =
            "http://xam150.azurewebsites.net/api/books/";

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
                new MediaTypeWithQualityHeaderValue("application/json");

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

        public Task<Book> Add(string title, string author, string genre)
        {
            // TODO: use POST to add a book
            throw new NotImplementedException();
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
