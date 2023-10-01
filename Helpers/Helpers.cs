namespace LuvFinder_API.Helpers
{
    public class Helpers
    {
        public static int CalculateAge(DateTime birthdate)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;
            // Go back to the year in which the person was born in case of a leap year
            if (birthdate.Date > today.AddYears(-age)) age--;
            return age;

        }

        public static bool IsImageExtension(string ext)
        {
            string[] _validExtensions = { "jpg", "bmp", "gif", "png" }; //  etc
            return _validExtensions.Contains(ext.ToLower());
        }

        public static string  GetProfilePicPath(string path, string filename)
        {
            return path + $"\\ClientApp\\public\\assets\\images\\userprofileimages\\{filename}";
        }
        //get images from file structure
        public static string GetProfilePicUrl(string filename)
        {
            return 
                string.IsNullOrEmpty(filename) ?
                $"assets/images/userprofileimages/no-image-available.png" :
                $"assets/images/userprofileimages/{filename}";
        }
        //get images from db 
        public static string GetProfilePicDB(byte[] ImageData)
        {
            if (ImageData == null)
                return $"assets/images/userprofileimages/no-image-available.png";
            
            string imageBase64Data = Convert.ToBase64String(ImageData);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
            return imageDataURL;
        }
    }
}
