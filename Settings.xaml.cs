using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlashMemo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashMemo
{
    public class Option
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }

	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
        public List<Option> Options { get; set; }
        Translator translator;

		public Settings ()
		{
			InitializeComponent ();
            translator = new Translator("ru");
            Options = new List<Option>
            {
                new Option {Name ="Автоперевод",ImagePath="https://cdn.iconscout.com/icon/free/png-256/translate-2-404700.png"},
                new Option {Name="Выбор языка",ImagePath="https://cdn0.iconfinder.com/data/icons/kameleon-free-pack-rounded/110/Settings-5-512.png"},
                new Option {Name = "Тема",ImagePath="https://storage.googleapis.com/multi-static-content/previews/artage-io-thumb-9c00ba8456d78c742f50f68ba41be448.png"},
            };
            this.BindingContext = this;
		}

        private async void Set_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            int index = e.SelectedItemIndex;
            if (index==0)
            {
                var langs = await translator.GetAvailableLanguages("ru");
                IDictionary<string, string> langs_ = langs.Langs;
                await Navigation.PushAsync(new Languages(langs_));
            }
        }
    }
}