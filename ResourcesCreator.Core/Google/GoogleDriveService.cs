using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResourcesCreator.Core.Google
{
    public class GoogleDriveService
    {
        private const string CLIENT_ID = "";
        private const string CLIENT_SECRET = "";

        private readonly ClientSecrets clientSecrets;
        private readonly string api_key;

        private DriveService service;

        private DriveService Service
        {
            get
            {
                if (service == null)
                {
                    string[] scopes = new[] { DriveService.Scope.DriveFile, DriveService.Scope.Drive };

                    if (String.IsNullOrEmpty(api_key))
                    {
                        UserCredential credential = GoogleWebAuthorizationBroker.AuthorizeAsync(clientSecrets, scopes, "user", CancellationToken.None).Result;

                        service = new DriveService(new BaseClientService.Initializer()
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = "Resources Creator"
                        });
                    }
                    else
                    {
                        service = new DriveService(new BaseClientService.Initializer()
                        {
                            ApiKey = api_key,
                            ApplicationName = "Resources Creator"
                        });
                    }

                    
                }
                return service;
            }
        }



        public GoogleDriveService(string clientId, string clientSecret, string apiKey)
        {
            this.api_key = apiKey;

            if (String.IsNullOrEmpty(clientId) || String.IsNullOrEmpty(clientSecret))
            {
                clientId = CLIENT_ID;
                clientSecret = CLIENT_SECRET;
            }


            this.clientSecrets = new ClientSecrets() { ClientId = clientId, ClientSecret = clientSecret };
        }


        public async Task<IList<string>> GetFileList(string query)
        {
            var list = Service.Files.List();
            list.Q = query;
            FileList files = await list.ExecuteAsync();

            return files.Items.Select(f => f.Title).ToList();
        }

        public async Task<Stream> DownloadFile(string url, Action<string> callback = null)
        {
            var downloader = new MediaDownloader(Service) { ChunkSize = 1024 * 1024 };

            var ms = new MemoryStream();
            var progress = await downloader.DownloadAsync(url, ms);
            if (progress.Status != DownloadStatus.Completed)
            {
                if (callback != null)
                {
                    callback("Can not to authorise");
                    callback("We try download raw file.");
                }
                TryUrlRawDownload(url, ms);
            }

            ms.Position = 0;
            return ms;
        }

        private void TryUrlRawDownload(string url, Stream ms)
        {
            var request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            ms = response.GetResponseStream();
        }
    }
}
