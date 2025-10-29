using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class CredentialsDtoRequest
    {
        public string usuario { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}