﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyProtocolsApp_Esteban.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Startpage : ContentPage
	{
		public Startpage()
		{
			InitializeComponent();
			LoadUserName();

		}
		private void LoadUserName() 
		{ LblUserName.Text = GlobalObjects.MyLocalUser.Nombre.ToUpper(); 

		}

        private async void BtnUserManagment_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync (new UserManagmentPage());
        }

        private async void BtnPasswordManagment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PasswordManagmentPage());
        }
    }
}