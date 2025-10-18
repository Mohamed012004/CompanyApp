namespace Company.Route.PL.Helpers
{
    public class DocumentSetting
    {
        // 1. Upload
        // Return Image Name
        public static string UploadFile(IFormFile file, string FolderName)
        {
            // To Get Path Of File ==> FolderPath + FileName
            //1. Get Folder Path
            //var FolderPath = "E:\\route\\MVC\\MVC Project\\Company.Route\\Company.Route.PL\\wwwroot\\Images\\" + FolderName;   // Static


            //var FolderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\Images\\" + FolderName;


            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", FolderName);


            // 2. Get File Name + Make it Unique
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            // Get FilePath
            var filePath = Path.Combine(FolderPath, fileName);

            var FileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(FileStream);

            return fileName;

        }


        // 2. Delete
        public static void Delete(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName, fileName);

            if (File.Exists(filePath))
            {
                try
                {
                    // نحاول نحذف الملف
                    File.Delete(filePath);
                }
                catch (IOException)
                {
                    // الملف لسه مقفول، نستنى شويه ونجرب تاني
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    Thread.Sleep(300);
                    File.Delete(filePath);
                }
            }
        }


    }
}
