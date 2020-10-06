using System;
using System.Collections.Generic;
using System.Linq;
using Minio.Exceptions;
using System.Text;
using System.Threading.Tasks;
using Minio;
using Minio.DataModel;

namespace FileUploader
{
    class FileUpload
    {
        static void Main(string[] args)
        {
            //set tdscminio to MinIO server IPAddress(94.232.173.54  , 94.232.173.80) in  local dns 
            var endpoint = "tdscminio";
            var accessKey = "tdscminioadmin";
            var secretKey = "TebyanSmartPasswd1399";

            
            try
            {

                Console.Write("1 :Put Object to MinIO ");
                Console.Write("\n");
                Console.Write("2 :Get Object from MinIO ");
                Console.Write("\n");
                Console.Write("Your Choisee is : ");
                var i = Console.ReadLine();

                var minio = new MinioClient(endpoint, accessKey, secretKey);
                if (i=="1")
                {
                    FileUpload.PutObjectToStorage(minio).Wait();
                }
                else if (i=="2")
                {
                    FileUpload.GetObjectFromStorage(minio).Wait();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }

        private async static Task PutObjectToStorage(MinioClient minio)
        {
            var bucketName = "music2";
            var location = "us-east-1";
            var objectName = "14.mp3";
            var filePath = "C:\\Users\\Yousef\\Music\\tavalod\\14.mp3";
            var contentType = "application/zip";
        
            try
            {

                // Make a bucket on the server, if not already present.
                bool found = await minio.BucketExistsAsync(bucketName);
                if (!found)
                {
                    await minio.MakeBucketAsync(bucketName, location);
                }
                // Upload a file to bucket.
                await minio.PutObjectAsync(bucketName, objectName, filePath);
                Console.WriteLine("Successfully uploaded " + objectName);
            }
            catch (MinioException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);
            }
        }
        private async static Task GetObjectFromStorage(MinioClient minio)
        {

            var mybucket = "mybucket";
            var myobject = "14.mp3";
            
            
            try
            {
                // Check whether the object exists using statObject().
                // If the object is not found, statObject() throws an exception,
                // else it means that the object exists.
                // Execution is successful.
                await minio.StatObjectAsync(mybucket, myobject);

                // Get input stream to have content of 'my-objectname' from 'my-bucketname'
                await minio.GetObjectAsync(mybucket, myobject,"ttttt.mp3" );

                
            }
            catch (MinioException e)
            {
                Console.WriteLine("Error occurred: " + e);
            }
        }
    }
}
