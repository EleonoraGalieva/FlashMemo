using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
	public partial class MyPopUp : PopupPage
	{
        NewCards card;
		public MyPopUp (NewCards card)
		{
			InitializeComponent ();
            this.card = card;
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            string filename = FileName.Text;
            card.Save(filename);
            PopupNavigation.Instance.PopAsync(true);
        }
    }
}