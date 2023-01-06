using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Monsta_backend.Models
{
    public class AuthResponseDto
    {
        public bool IsAuthSuccessful { get; set; }
        public string? ErrorMessage { get; set; }
        public string? Token { get; set; }
    }
}
