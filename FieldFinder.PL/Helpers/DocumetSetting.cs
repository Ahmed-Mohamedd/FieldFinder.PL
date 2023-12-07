namespace FieldFinder.PL.Helpers
{
    public static class DocumetSetting
    {
        public static string UploadDocument(IFormFile file, string FolderName)
        {
            //1- Get Located Folder Path to save in.

            //string FolderPath = "E:\\1-(EraaSoft)\\.NetCore Mvc  (.net6)\\BulkyBook\\BulkyBook.PL\\wwwroot\\Files\\Images\\";
            //string FolderPath = Directory.GetCurrentDirectory()+"\\wwwroot\\Files\\"+FolderName;
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            // 2- get file name and make it unique
            string FileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3- get File path
            string FilePath = Path.Combine(FolderPath, FileName);

            // 4- Save file As Stream (Stream => is A data per time)
            using var fs = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(fs);

            return FileName;

        }


        public static void DeleteFile(string fileName, string FolderNAme)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", FolderNAme, fileName);
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        //public static string ImagePath(string fileName, string FolderNAme)
        //{
        //    return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", FolderNAme, fileName);
        //}
    }
}
