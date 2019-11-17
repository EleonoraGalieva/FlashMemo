using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlashMemo.Models;


namespace FlashMemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OldCards : ContentPage
    {

        public OldCards()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await UpdateFileList();
        }

        async Task UpdateFileList()
        {
            //getting all the files
            cards.ItemsSource = await DependencyService.Get<IFileWorker>().GetFilesAsync();
            cards.SelectedItem = null;
        }

        FlashCard GettingTheCollection(string line, FlashCard card)
        {
            string key = null;
            string value = null;
            bool flag = false; //flag of ' '

            foreach (char sign in line)
            {
                if (sign == '\n')
                {
                    flag = false;
                    card.Add(key, value);
                    key = null;
                    value = null;
                    continue;
                }

                if (sign == ' ')
                {
                    flag = true;
                    continue;
                }

                if (!flag)
                {
                    key += sign;
                }

                if (flag)
                {
                    value += sign;
                }
            }

            return card;
        }

        private async void Cards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            FlashCard card = new FlashCard();
            //getting the name of the file
            string file_name = (string)e.SelectedItem;
            //getting all the text from the file
            string my_cards = await DependencyService.Get<IFileWorker>().LoadTextAsync(file_name);
            card = GettingTheCollection(my_cards, card);
            await Navigation.PushAsync(new TheTest(card));
        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            //getting the name of the file
            string file_name = (string)((MenuItem)sender).BindingContext;
            //delete
            await DependencyService.Get<IFileWorker>().DeleteAsync(file_name);
            //update the list of files
            await UpdateFileList();
        }
    }
}