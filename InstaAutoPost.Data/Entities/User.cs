using InstaAutoPost.Data.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstaAutoPost.Data.Entities
{
    public class User:EntityBase,IEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
