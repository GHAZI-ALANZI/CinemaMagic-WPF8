using System.IO;

namespace CinemaMagic.ViewModels.ProductManagement
{
    public class MotSoPhuongThucBoTro
    {
        public static string pathProject()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;

            string kq = System.IO.Path.Combine(basePath, @"..\..\..\");

            string fullPathToProject = System.IO.Path.GetFullPath(kq);
            return fullPathToProject;
        }


        public static void copyFile(string source, string destination)
        {
            if (File.Exists(source))
            {

                string destinationFilePath = Path.Combine(destination, Path.GetFileName(source));

                // Copy file
                File.Copy(source, destinationFilePath);
            }
        }



        public static string RandomFileName()
        {
            Random random = new Random();
            int nameLength = random.Next(10, 20);
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var name = new char[nameLength];

            for (int i = 0; i < nameLength; i++)
            {
                name[i] = chars[random.Next(chars.Length)];
            }
            return new string(name);
        }
    }
}
