// MainPage.xaml.cs

namespace BookClient
{
    using BookClient.Data;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage
    {
        #region Fields

        private readonly IList<Book> books = new ObservableCollection<Book>();
        private readonly BookManager manager = new BookManager();

        private bool _myIsBusy = true;

        #endregion

        #region Properties

        public bool MyIsBusy
        {
            get => _myIsBusy;

            set
            {
                if (value != _myIsBusy)
                {
                    _myIsBusy = value;

                    activityIndicator.IsEnabled
                        = activityIndicator.IsRunning
                        = activityIndicator.IsVisible
                        = _myIsBusy;
                    //IsBusy = value;
                }
            }
        }

        #endregion

        #region Constructor

        public MainPage()
        {
            BindingContext = books;
            InitializeComponent();
        }

        #endregion

        #region Event Handlers

        private async void OnRefresh(object sender, EventArgs e)
        {
            this.MyIsBusy = true;

            var bookCollection = await manager.GetAllAsync();

            foreach (Book book in bookCollection)
            {
                if (books.All(b => b.ISBN != book.ISBN))
                    books.Add(book);
            }

            this.MyIsBusy = false;
        }

        private async void OnAddNewBook(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEditBookPage(manager, books));
        }

        private async void OnEditBook(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEditBookPage(manager, books, (Book)e.Item));
        }

        private async void OnDeleteBook(object sender, EventArgs e)
        {
            var item = (MenuItem) sender;

            var book = item.CommandParameter as Book;

            if (book != null)
            {
                if (await this.DisplayAlert("Delete Book?",
                    "Are you sure you want to delete the book '"
                        + book.Title + "'?", "Yes", "Cancel") == true)
                {
                    this.MyIsBusy = true;

                    await manager.DeleteAsync(book.ISBN);

                    books.Remove(book);

                    this.MyIsBusy = false;
                }
            }
        }

        #endregion
    }
}
