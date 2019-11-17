using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FlashMemo.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FlashMemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TheTest : ContentPage, INotifyPropertyChanged
    {
        private FlashCard flash_card = new FlashCard();
        private int number_of_incorrect_cards;
        private FlashCard incorrect_flash_card=new FlashCard();
        private int index = 0;

        public TheTest(FlashCard flash_card)
        {
            InitializeComponent();
            this.flash_card = flash_card;
            NativeWord.Text = (string)flash_card[0]; //returns key
        }

        public void Testing(string line)
        {
            index++;
            if (index >= flash_card.Count)
            {
                flash_card = flash_card - incorrect_flash_card;
                return;
            }
            if ((string)flash_card[NativeWord.Text] == line)
            {
                NativeWord.Text = (string)flash_card[index];
                ForeignWord.Text = "\0";
            }
            else
            {
                number_of_incorrect_cards++;
                incorrect_flash_card.Add(NativeWord.Text, (string)flash_card[NativeWord.Text]);
                NativeWord.Text = (string)flash_card[index];
                ForeignWord.Text = "\0";
            }
            return;
        }

        private void ForeignWord_Completed(object sender, EventArgs e)
        {
            Testing(((Entry)sender).Text);
        }
    }
}