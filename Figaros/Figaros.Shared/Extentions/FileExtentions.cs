using Microsoft.AspNetCore.Http;

namespace Figaros.Shared.Extentions
{
    public static class FileExtentions
    {
        public static bool IsFileContent(this IFormFile formFile)
        {
            if (formFile.ContentType == "application/pdf" || formFile.ContentType == "application/msword" || 
                formFile.ContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" ||
                formFile.ContentType.Contains("image"))
            {
                return true;
            }
            return false;
        }

        public static bool IsValidFileLength(this IFormFile formFile)
        {
            if (formFile.Length < 4 * 1024 * 1024)
            {
                return true;
            }
            return false;
        }

        public static string SaveFile(this IFormFile formFile, string root, string file)
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

        public static bool DeleteFile(this string fileURL, string root, string file)
        {
            string path = Path.Combine(root, file, fileURL);

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
