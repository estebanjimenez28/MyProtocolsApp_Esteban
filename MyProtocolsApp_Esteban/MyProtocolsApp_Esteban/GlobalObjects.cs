﻿using MyProtocolsApp_Esteban.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyProtocolsApp_Esteban
{
    public static class GlobalObjects
    {
        //definimos las propiedades de codificacion de los json
        //que usaremos en los modelos

        public static string MimeType = "application/json";
        public static string ContentType = "Content-Type";

        public static UserDTO MyLocalUser = new UserDTO();  
    }
}
