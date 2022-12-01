using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace BuildFlow.Services
{
    public interface ISavePDF
    {
        Task SaveAndView(string filename, string contentType, MemoryStream stream);
    }
}
