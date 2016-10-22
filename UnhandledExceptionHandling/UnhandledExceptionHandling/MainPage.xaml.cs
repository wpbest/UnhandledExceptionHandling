using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace UnhandledExceptionHandling
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            throw new Exception("The button labeled '" + button.Text + "' has thrown an exception");
        }
    }
}
