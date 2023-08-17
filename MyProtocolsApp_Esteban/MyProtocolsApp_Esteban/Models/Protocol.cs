﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyProtocolsApp_Esteban.Models
{
    public class Protocol
    {
        public Protocol()
        {
            //ProtocolStepProtocolSteps = new HashSet<ProtocolStep>();
        }

        [JsonIgnore]
        public RestRequest Request { get; set; }

        public int ProtocolId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreationDate { get; set; }
        public TimeSpan? AlarmHour { get; set; }
        public bool? AlarmActive { get; set; }
        public bool? AlarmJustInWeekDays { get; set; }
        public bool? Active { get; set; }
        public int UserId { get; set; }
        public int ProtocolCategory { get; set; }

       // public virtual ProtocolCategory? ProtocolCategoryNavigation { get; set; } = null!;
      //  public virtual User? User { get; set; } = null!;

        public virtual ICollection<ProtocolStep>? ProtocolStepProtocolSteps { get; set; }

        //Funciones del modelo
        public async Task<ObservableCollection< Protocol>> GetProtocolListByUserID()
        {
            try
            {

                string RouteSufix = String.Format("Protocols/GetProtocolListByUser?id={0}", this.UserId);

                //armamos la rura completa al endpoint en el API
                string URL = Services.APIConnection.ProductionPrefixURL + RouteSufix;

                RestClient client = new RestClient(URL);

                Request = new RestRequest(URL, Method.Get);

                //agregamos mecanismo de seguridad, en este caso API key

                Request.AddHeader(Services.APIConnection.ApiKeyName, Services.APIConnection.ApiKeyValue);
                Request.AddHeader(GlobalObjects.ContentType, GlobalObjects.MimeType);

                //ejecutar la llamada al API

                RestResponse response = await client.ExecuteAsync(Request);

                //saber si las cosas salieron bien

                HttpStatusCode statusCode = response.StatusCode;

                if (statusCode == HttpStatusCode.OK)
                {
                    var list = JsonConvert.DeserializeObject<ObservableCollection<Protocol>>(response.Content);
                
                    return list;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                string message = ex.Message;

                throw;
            }

        }
    }
}
