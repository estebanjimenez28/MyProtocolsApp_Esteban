using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyProtocolsApp_Esteban.ViewModels;
using Acr.UserDialogs;

namespace MyProtocolsApp_Esteban.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppLoginPage : ContentPage
      
    {
        //se realiza el anclaje entre esta vista y el VM que le da la 
        //funcionalidad 

        UserViewModel viewModel;

        public AppLoginPage()
        {
            InitializeComponent();

            //esto vincula la v con el vm y además crea la instancia del obj 
            this.BindingContext = viewModel = new UserViewModel();

        }

        private void SwShowPassword_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwShowPassword.IsToggled)
            {
                TxtPassword.IsPassword = false;
            }
            else
            {
                TxtPassword.IsPassword = true;
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            //validación del ingreso del usuario a la app 

            if (TxtUserName.Text != null && !string.IsNullOrEmpty(TxtUserName.Text.Trim()) &&
                TxtPassword.Text != null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()))
            {
                //si hay info en los cuadros de texto de email y pass se procede 
                try
                {
                    //hacemos una animación de espera 
                    UserDialogs.Instance.ShowLoading("Checking User Access...");
                    await Task.Delay(2000);

                    string username = TxtUserName.Text.Trim();
                    string password = TxtPassword.Text.Trim();

                    bool R = await viewModel.UserAccessValidation(username, password);

                    if (R)
                    {
                        //si la validación es correcta se permite el ingreso al sistema 
                        //igual que el progra 5 vamos a tener un usuario global 

                        //TODO: crear el objeto de usuario global 

                        await Navigation.PushAsync(new Startpage());
                        return;
                    }
                    else
                    {
                        //algo salió mal 

                        await DisplayAlert("User Access Denied", "Username or Password are incorrect", "OK");
                        return;
                    }


                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    //apagamos la animación de carga 
                    UserDialogs.Instance.HideLoading();
                }


            }
            else
            {
                //si no digito datos indicarle al usuario del requerimiento 

                await DisplayAlert("Data required", "Username and Password are required...", "OK");
                return;
            }






        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSignUpPage());
        }
    }
}
