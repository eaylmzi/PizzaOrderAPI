using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.Services.Image
{
    public interface IImageService
    {
        public byte[] ConvertImageToString(IFormFile file);
        public string DisplayImage(byte[] photo);
    }
}
