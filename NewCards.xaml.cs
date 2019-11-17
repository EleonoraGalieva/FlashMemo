using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Specialized;
using FlashMemo.Models;
using System.Collections;
using Rg.Plugins.Popup.Services;
using Google.Cloud.Translation.V2;
using Google.Apis.Auth.OAuth2;
using Plugin.Settings;

namespace FlashMemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCards : ContentPage
    {
        FlashCard flashCard;
        static SettingsForYandex settings = new SettingsForYandex
        {
            Culture = "ru",
            OriginalLang = new KeyValuePair<string, string>("ru", "Русский"),
            TranslateLang = new KeyValuePair<string, string>("en", "Английский")
        };

        Translator translator = new Translator(settings.Culture);
        public NewCards()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            flashCard = new FlashCard();

        }

        public async void Save(string filename)
        {
            string text = null;
            if (String.IsNullOrEmpty(filename)) return;
            //if file exists
            if (await DependencyService.Get<IFileWorker>().ExistsAsync(filename))
            {
                bool isRewrited = await DisplayAlert("Подверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
                if (isRewrited == false) return;
            }
            //overwriting the file
            foreach (DictionaryEntry dictionary in flashCard)
            {
                text += (string)dictionary.Key + " " + (string)dictionary.Value + "\n";
            }
            await DependencyService.Get<IFileWorker>().SaveTextAsync(filename, text);
        }

        private async void ButtonForAdding_Clicked(object sender, EventArgs e)
        {
            if (NativeWord.Text == null || ForeignWord == null)
                await DisplayAlert(null, "Упс... Вы ничего не ввели", "Исправить!");
            flashCard.Add(NativeWord.Text, ForeignWord.Text);
            NativeWord.Text = null;
            ForeignWord.Text = null;
        }

        private async void Start_Clicked(object sender, EventArgs e)
        {
            if (flashCard.IsEmpty())
                await DisplayAlert(null, "Упс... Вы ничего не ввели", "Исправить!");
            if (NativeWord.Text == null || ForeignWord.Text == null)
                await DisplayAlert(null, "Упс... Вы ничего не ввели", "Исправить!");
            else
            {
                bool result = await DisplayAlert("Новая партия карточек!", "Желаете сохранить?", "Да", "Нет");
                if (result)
                {
                    await PopupNavigation.Instance.PushAsync(new MyPopUp(this));
                }
                await Navigation.PushAsync(new TheTest(flashCard));
            }
        }

        private async void Translate(string word, string foreign_lang)
        {
            Translation temp = await translator.TranslateTextAsync(word, "ru", foreign_lang);
            ForeignWord.Text = temp.text;
        }

        private void NativeWord_Completed(object sender, EventArgs e)
        {
            string cross = CrossSettings.Current.GetValueOrDefault("trans_lang", "en");
            Translate(NativeWord.Text,cross);
        }
    }
}