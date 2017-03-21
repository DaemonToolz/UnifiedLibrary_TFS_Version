using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Google.Apis.Util.Store;
using System.Threading;
using Google.Apis.Services;

using static Google.Apis.Sheets.v4.SpreadsheetsResource;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace UnifiedLibraryV1.Google.Sheet{
    public class SheetClient{
        private static string[] Scopes = {
            SheetsService.Scope.SpreadsheetsReadonly,
            SheetsService.Scope.Spreadsheets
        };

        private UserCredential credentials;
        private String credentialPath;
        private String credentialkeygen;
        private SheetsService service;

        private ValuesResource sheets;
        private BatchGetRequest selectedSheet;

        private String ApplicationName { get; set; }
        public String CredentialPath {
            get { return credentialPath; }
            set {
                if (value == null || value.Trim().Count() == 0)
                    credentialPath = "secret_client.json"; 
                else {
                    if (value.EndsWith("secret_client.json"))
                        credentialPath = value;
                    else if (!value.EndsWith("/"))
                        credentialPath = value + "/secret_client.json";
                    else
                        credentialPath = value + "secret_client.json";
                } 
            }
        }
        
        public SheetClient(){

        }

        public void ReadCredential(String path){

        }
        
        public void AddCredential(String path){

        }

        public bool CheckCredential(){
            bool isValid = false;
            try {
                using (var stream = new FileStream(credentialPath, FileMode.Open, FileAccess.Read)) {
                    credentialkeygen = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".credentials/" + ApplicationName + ".json");
                    credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                       GoogleClientSecrets.Load(stream).Secrets,
                       Scopes,
                       "user",
                       CancellationToken.None,
                       new FileDataStore(credentialkeygen, true)).Result;
                }

                service = new SheetsService(new BaseClientService.Initializer() {
                    HttpClientInitializer = credentials,
                    ApplicationName = ApplicationName,
                });

                isValid = true;
            }
            catch{
                isValid = false;
            }
            
            return isValid;
        }

        public void RetrieveSheets(String Id) {
            sheets = service.Spreadsheets.Values;
            if (Id == null || Id.Trim().Count() == 0) return;
            try { selectedSheet = service.Spreadsheets.Values.BatchGet(Id); } catch {}
        }

        public void SelectRange() { }
        public void UpdateRange() { }
    }
}
