using Microsoft.AspNetCore.Http;

namespace Figaros.Shared.Extentions
{
    public static class ImageExtentions
    {
        public static bool IsImageContent(this IFormFile formFile)
        {
            if (formFile.ContentType.Contains("image"))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidImageLength(this IFormFile formFile)
        {
            if (formFile.Length < 3 * 1024 * 1024)
            {
                return true;
            }
            return false;
        }

        public static string SaveImage(this IFormFile formFile, string root, string file)
        {
            string fileName = Guid.NewGuid().ToString() + formFile.FileName;

            if (fileName.Length > 100)
            {
                fileName = Guid.NewGuid().ToString() + formFile.FileName.Substring(formFile.FileName.Length - 64, 64);
            }

            string path = Path.Combine(root, file, fileName);

            using (FileStream filestream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(filestream);
            }

            return fileName;
        }

        public static bool DeleteImage(this string imageURL, string root, string file)
        {
            string path = Path.Combine(root, file, imageURL);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
