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
        //se realiza el anclaje entre la vista y el VM que le da la funcionalidad
        //
        UserViewModel viewModel;
        public AppLoginPage()
        {
            InitializeComponent();

            //esto vincula la v con el vm y ademas crea la instancia del obj
   
            this.BindingContext = viewModel = new UserViewModel();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            
            //validacion del ingreso del usuario al app

           if(TxtUserName.Text !=null && !string.IsNullOrEmpty(TxtUserName.Text.Trim()) &&
                TxtPassword.Text !=null && !string.IsNullOrEmpty(TxtPassword.Text.Trim()) )

            {

                //si hay info en los cuadros de texto de email y pass se procede

                try
                {

                    UserDialogs.Instance.ShowLoading("Checking User Access...");
                    await Task.Delay(2000); 

                    string username = TxtUserName.Text.Trim();
                    string password = TxtPassword.Text.Trim();  

                    bool R = await viewModel.UserAccessValidation(username, password);

                    if (R) 
                    {
                        //si la validacion es correcta se permite el ingreso al sistema
                        //igual qu el progra 5 vamos a tener un usuario global

                        //TODO: crear el objeto de usuario global

                        await Navigation.PushAsync(new Startpage());
                        return;
                    }
                    else
                    {
                        //algo salio mal
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
                    //apagamos la animacion 
                    UserDialogs.Instance.HideLoading();

                }


            }
           else
            {
                //si no digito datos indicarle al usuario del requerimiento

                await DisplayAlert("Data Required", "Username and Password are required...", "OK");
                return;
            }
              
        }

        private async void BtnSignUp_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserSignUpPage());
        }
    }
}