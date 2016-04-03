using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2.API.Models
{
    public class AuthenticatedUser
    {
        public long UserID { get; set; }
        public string UserName { get; set; }
        
        //Probably need to add user role and permission here
    }
}