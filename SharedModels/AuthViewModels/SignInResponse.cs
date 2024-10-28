using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.AuthViewModels
{
    public class SignInResponse
    {
        public bool IsSuccess { get; set; }
        public IList<string> Errors { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime JwtExpires { get; set; }
    }
}
