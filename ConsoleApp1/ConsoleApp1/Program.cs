using System;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ConsoleApp1
{
    internal class Program
    {

        //static void Main(string[] args)
        //{
        //string file = "fe4ba9ee-7061-4406-b8b9-256ccae28184/uvp/aea1c57b-aeaf-45f4-96e6-8d3558ca9ff4";
        //     download_FromBlob(file, "uvpcontainer");
        // }

        public static void download_FromBlob(string filetoDownload, string azure_ContainerName)
        {
            Console.WriteLine("Inside downloadfromBlob()");

            string storageAccount_connectionString = "DefaultEndpointsProtocol=https;AccountName=anupstorage;AccountKey=3sdoBXltauO0QFWrKmKpN74heus1iziHLh+mCzetjLqeUmKn9bSEuARqQuBVRGO715cm7P7lilbCV1p2kvq4RA==;EndpointSuffix=core.windows.net";

            CloudStorageAccount mycloudStorageAccount = CloudStorageAccount.Parse(storageAccount_connectionString);
            CloudBlobClient blobClient = mycloudStorageAccount.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference(azure_ContainerName);
            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(filetoDownload);

            // provide the file download location below            
            Stream file = File.OpenWrite("C:\\Users\\anukumar\\Downloads\\test.vsix");

            cloudBlockBlob.DownloadToStream(file);

            Console.WriteLine("Download completed!");

        }
    }
}
