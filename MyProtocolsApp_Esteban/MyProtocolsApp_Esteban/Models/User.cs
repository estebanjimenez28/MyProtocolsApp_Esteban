﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;


namespace MyProtocolsApp_Esteban.Models
{
   public class User


    {

        public RestRequest Request { get; set; }    
        //en este ejemplo usare los mismos atributos que en el modelo del API
        //posteriormente en otra clase usare el DTP del usario para simplificar
        //el json que se envia y recibe desde el API


        public int UserId { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string BackUpEmail { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? Address { get; set; }
        public bool? Active { get; set; }
        public bool? IsBlocked { get; set; }
        public int UserRoleId { get; set; }

        public virtual UserRole? UserRole { get; set; }

        public User()
        {

        }
        //Funciones especificas de llamada a end points del API
        //funcion que permite validar que los datos digitados en la pagian de
        //applogin sean correctos o no.

        public async Task<bool> ValidateUserLogin()
        {
            try
            {

                //usaremos el prefijo de la ruta URL del API que se indica en
                //services\APIConnection para agregar el sufijo y lograr la ruta
                //completa de consumo del end point que se quiere usar.

                string RouteSufix = String.Format("Users/ValidateLogin?username={0}&password={1}", this.Email, this.Password);

                //armamos la rura completa al endpoint en el API
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;   

                RestClient client = new RestClient(URL);  
                
                Request = new RestRequest(URL, Method.Get);

                //agregamos mecanismo de seguridad, en este caso API key

                Request.AddHeader(Services.APIConnection.ApiKeyName,Services.APIConnection.ApiKeyValue);
                
                //ejecutar la llamada al API

                RestResponse response = await client.ExecuteAsync(Request); 

                //saber si las cosas salieron bien

                HttpStatusCode statusCode = response.StatusCode;    

                if(statusCode == HttpStatusCode.OK) 
                {
                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            { string message = ex.Message;  

                throw;
            }
        }
    }

   
}
