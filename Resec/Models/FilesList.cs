using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resec.Models
{
    public class FilesList
    {
        public string[] list { get; set; }

        public FilesList(string[] filesName)
        {
            list = filesName;
        }
    }
}
