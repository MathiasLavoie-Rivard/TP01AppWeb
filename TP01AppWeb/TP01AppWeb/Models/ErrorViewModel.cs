using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TP01AppWeb.Models
{
    public class ErrorViewModel : ReadMe
    {
        public string Error { get; set; }
        
        public ErrorViewModel(string p_error)
        {
            Error = p_error;
        }
    }
}
