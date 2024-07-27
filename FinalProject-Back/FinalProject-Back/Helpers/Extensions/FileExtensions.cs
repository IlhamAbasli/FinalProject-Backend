namespace FinalProject_Back.Helpers.Extensions
{
    public static class FileExtensions
    {
        public static async Task SaveFileToLocalAsync(this IFormFile file, string filePath)
        {
            using FileStream stream = new(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        public static void DeleteFileFromLocal(this string path)
        {
            if (File.Exists(path))
                File.Delete(path);
        }
    }
}
