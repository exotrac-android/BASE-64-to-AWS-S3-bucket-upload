using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;

namespace ExportDataToCSV
{

    class Program
    {
        static void Main(string[] args)
        {
            // Replace these with your actual values
            //data:image/png;base64,

            string inputimagestring = "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";



            string base64Image = inputimagestring;
            string fileName = "testkamin.jpg"; 

            var imageBytes = Convert.FromBase64String(base64Image);
            var uploader = new iamresponse();


            //  https://s3.dal.us.cloud-object-storage.appdomain.cloud/chrobinson/testkamin.jpg
            // string bucketUrl = "https://s3.dal.us.cloud-object-storage.appdomain.cloud/chrobinson/";


            string bucketUrl = "https://s3.dal.us.cloud-object-storage.appdomain.cloud/kamin/";



           // string bucketUrl = "s3.private.dal.us.cloud-object-storage.appdomain.cloud/kamin/";
           //// Error uploading file: Access to the path 'C:\Users\Harshit\Music\GITHUB\Kamin_consoleapplication\Final ConsoleApp to upload base64
           ////server\ConsoleApp1\bin\Debug\s3.private.dal.us.cloud-object-storage.appdomain.cloud\kamin\testkamin.jpg' is denied.



            string token = uploader.GenerateIamToken(fileName, ref bucketUrl);

            if (!string.IsNullOrEmpty(token))
            {
                var uploadResult = uploader.UploadStaticFile(imageBytes, token, bucketUrl);
                Console.WriteLine($"Upload Result: {uploadResult}");
            }
            else
            {
                Console.WriteLine("Failed to generate IAM token.");
            }
        }
    }

    public class iamresponse
    {
        private string token { get; set; }
        private static string tokenUrl = "https://iam.cloud.ibm.com/identity/token";

        public string GenerateIamToken(string FileName, ref string bucketUrl)
        {
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                string data = "grant_type=urn:ibm:params:oauth:grant-type:apikey&apikey=V5wGvlIlw5SiHr4wRPdfYHDXP_lOn2_mgFqVQZh081U1"; 
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    var response = client.UploadString(tokenUrl, data);
                    var iam_response = JsonConvert.DeserializeObject<dynamic>(response);
                    token = iam_response.access_token;
                    bucketUrl += FileName;
                    return token;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating IAM token: {ex.Message}");
                return null;
            }
        }

        public string UploadStaticFile(byte[] stream, string token, string bucketUrl)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;
                    client.Headers.Add("x-amz-acl", "public-read");
                    client.Headers[HttpRequestHeader.ContentType] = "image/jpeg";
                    var response = client.UploadData(bucketUrl, "PUT", stream);
                    return "Upload successful";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading file: {ex.Message}");
                return "Upload failed";
            }
        }
    }
}