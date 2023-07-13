using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyProtocolsApp_Esteban.Models;

namespace MyProtocolsApp_Esteban.ViewModels
{
    public class UserViewModel :BaseViewModel
    {
        //el VM funciona como puente entre el modelo y la vista
        //en sentido teorico el vm "siente" los cambios de la vista 
        //y los pasa al odelo de forma automatica, o viceversa
        //segun se use en uno o dos sentidos.

        //tambien se puede usar (como en este caso particular,
        //simplemente como mediador de procesos. Mas adelante se usara
        //commands y bingins en dos sentidos

        //primero en formato de funciones clasicas
        public User MyUser { get; set; }

        public UserViewModel() 
        { 
            MyUser = new User();
        }  

        //funciones
        //funcion para validar el ingreso del usuario al app por medio del login

        public async Task<bool> UserAccessValidation(string pEmail, string pPassword)
        {
            //debemos poder controlar que no se ejecute la operacion mas de una vez
            //en este caso hay una funcionalidad pensada para eso en baseViewModel que
            //fue heredada al definir esta clase.
            //se usara una propiedad llamada "IsBusy" para indicar que esta en proceso de ejecucuion
            //mientras su valor sea verdadero

            //control de bloqueo de funcionalidad

            if(IsBusy) return false; 
           IsBusy = true;
            try
            {
                MyUser.Email = pEmail;
                MyUser.Password = pPassword;

                bool R = await MyUser.ValidateUserLogin();
                return R;

            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;

                throw;
            }
            finally
            {
                IsBusy = false;
            }
           
        }

    }
}
