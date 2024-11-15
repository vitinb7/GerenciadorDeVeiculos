using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceApiCarro
{
    public class User
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public static string Token { get; set; }
    }
    public class LoginResposta
    {
        public string Token { get; set; }
    }
}
