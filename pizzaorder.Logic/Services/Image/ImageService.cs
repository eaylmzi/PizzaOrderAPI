using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using pizzaorder.Data.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizzaorder.Data.Services.Image
{
    public class ImageService : IImageService
    {      
        public byte[] ConvertImageToString(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                 file.CopyToAsync(memoryStream);
                 return memoryStream.ToArray();
            }
        }

        public string DisplayImage(byte[] photo)
        {
            if (photo != null)
            {
                string str = Encoding.UTF8.GetString(photo);


                return str;
                //var imageData = Convert.FromBase64String(str);
                //return File(imageData, "image/jpg"); // veya "image/png" veya "image/gif" gibi uygun MIME türünü belirtebilirsiniz

            }
            else
            {
                return string.Empty;
            }
        }
    }
}
