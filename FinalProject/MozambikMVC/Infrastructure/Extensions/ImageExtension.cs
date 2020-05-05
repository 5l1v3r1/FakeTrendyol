using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace MozambikMVC.Infrastructure.Extensions
{
    public static class ImageExtension
    {
        public static string CreateImage(this IFormFile Logo, IHostEnvironment _environment)
        {
            string uniqueName = null;
            if (Logo != null)
            {
                string uploadFolders = Path.Combine(_environment.ContentRootPath, "Images");
                uniqueName = Guid.NewGuid().ToString() + "_" + Logo.FileName;
                string filePath = Path.Combine(uploadFolders, uniqueName);
                FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
               
                Logo.CopyTo(stream);
                stream.Close();
            }
            return uniqueName;
        }

        public static void DeleteImage(string fileName,IHostEnvironment env)
        {
            string filePath = Path.Combine(env.ContentRootPath, "Images", fileName);
            File.Delete(filePath);
        }
        public static bool IsImage(this IFormFile file)
        {
            if (file == null)
                return false;

            switch (file.ContentType)
            {
                case "image/jpg":
                case "image/gif":
                case "image/jpeg":
                case "image/png":
                    return true;
                default:
                    return false;
            }
        }

        public static bool LessThan(this IFormFile file, byte mb)
        {
            if (file == null)
                return false;

            return file.ContentType.Length / Math.Pow(1024, 2) <= mb;
        }

    }
}
