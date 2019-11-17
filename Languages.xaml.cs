using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlashMemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Languages : ContentPage
    {
        IDictionary<string, string> _langs;
        public Languages(IDictionary<string,string> Langs)
        {
            InitializeComponent();
            _langs = Langs;
            langs.ItemsSource = _langs.Values;
            langs.SelectedItem = null;
        }

        private void Langs_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null) return;
            int lang_index = e.SelectedItemIndex;
            KeyValuePair<string, string> keyValues = _langs.ElementAt(lang_index);
            CrossSettings.Current.AddOrUpdateValue("trans_lang", keyValues.Key);
        }
    }
}