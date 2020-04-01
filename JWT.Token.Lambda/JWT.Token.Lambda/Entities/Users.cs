using System;
using System.Collections.Generic;

namespace JWT.Token.Lambda.Entities
{
    public partial class Users
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
