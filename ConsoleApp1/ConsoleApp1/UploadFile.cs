using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class UploadFile
    {
        private static void Main(string[] args)
        {
            string filename = @"D:\format-toggle-3.0.0.vsix";
            FileStream fs = File.OpenRead(filename);
            fs.Seek(0, SeekOrigin.Begin);
            _ = UploadArtifactFileAsync(fs);
            fs.Position = 0;
            //Re-use the same stream.
        }

        public static async Task UploadArtifactFileAsync(Stream ArtifactFileStream)
        {
            if (ArtifactFileStream == null)
            {
                throw new ArgumentNullException(nameof(ArtifactFileStream));
            }
            try
            {
                // PMP config
                const string targetHost = "pmp-dev-wcus-webapi.azurewebsites.net";
                const string registryName = "vscode";
                const string artifactFileTypeMoniker = "vscodevsix";

                // Construct Request
                string requestUri = $"https://{targetHost}/api/upload?" +
                    $"RegistryName={registryName}&ArtifactFileTypeMoniker={artifactFileTypeMoniker}";
                HttpClient httpClient = new HttpClient();
                StreamContent streamContent = new StreamContent(ArtifactFileStream);
                MultipartFormDataContent content = new MultipartFormDataContent
                {
                        { streamContent, "artifactFile", "artifactFile" }
                };

                // Perform Request
                HttpResponseMessage response = httpClient.PostAsync(requestUri, content).Result;

                // Process Response
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}