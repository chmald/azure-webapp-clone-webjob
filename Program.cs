using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WPClone
{
    class Program
    {
        static void Main(string[] args)
        {
            int _timeOut = 10; // Timeout in X amount of minutes.
            int _timeOutMilli = _timeOut * 60000;
            CloneApp().Wait(_timeOutMilli);
        }

        static async Task CloneApp()
        {
            // Web Deployment Settings
            string deployment_user = ConfigurationSettings.AppSettings["userPass"];
            string web_app_clone = ConfigurationSettings.AppSettings["webApp"];

            // Compress contents of wwwroot directory
            string _wwwrootPath = @"D:\home\site\wwwroot";
            string _zipPath = @".\webappCopy.zip";
            string _scmURL = @"https://" + web_app_clone + ".scm.azurewebsites.net/api/zipdeploy";

            // Zip up the wwwroot file
            Console.WriteLine("Zipping up: " + _wwwrootPath + "...");
            ZipFile.CreateFromDirectory(_wwwrootPath, _zipPath);
            Console.WriteLine("Zipping complete!");

            // Request for Zip Deployment to backup Web App
            Console.WriteLine("Creating HttpClient...");
            var _client = new HttpClient();
            var _credByteArray = Encoding.ASCII.GetBytes(deployment_user);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(_credByteArray));

            // Open ZipFile and stream into request
            var _zip = File.OpenRead(_zipPath);
            var _content = new StreamContent(_zip);
            var _req = new HttpRequestMessage(new HttpMethod("POST"), _scmURL);
            _req.Content = _content;

            Console.WriteLine("Sending Zip Deploy...");
            var result = await _client.PostAsync(_scmURL, _content);
            Console.WriteLine(result);

            // Clean up
            if(File.Exists(_zipPath))
            {
                File.Delete(_zipPath);
            }

            Console.WriteLine("End of WebJob!");
        }
    }
}
