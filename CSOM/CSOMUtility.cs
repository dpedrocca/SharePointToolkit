using Microsoft.SharePoint.Client;
using PnP.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharePointToolkit;
using CSOMLib = Microsoft.SharePoint.Client;

namespace SharePointToolkit.CSOM
{
    class CSOMUtility
    {
        public async Task Test(CSOMCConfig cfg, CancellationToken cancellationToke = default)
        {
            var siteRelativeUri = cfg.Site;

            var spoTenant = cfg.SPOTenantName;

            // Connect to the target SPO site via CSOM
            using (var clientContext = await AuthenticationManager.CreateWithCertificate(
                cfg.ClientId,
                System.Security.Cryptography.X509Certificates.StoreName.My,
                System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser,
                cfg.CertificateThumbprint,
                cfg.TenantId)
                .GetContextAsync($"https://{spoTenant}{siteRelativeUri}"))
            {
                // Let's see if the current user is site admin
                var currentUser = clientContext.Web.CurrentUser;
                clientContext.Load(currentUser, u => u.IsSiteAdmin);
                await clientContext.ExecuteQueryAsync();

                //this.logger.LogInformation($"Current user is site admin? {currentUser.IsSiteAdmin}");

                // Define a new generic list
                var newList = new CSOMLib.ListCreationInformation
                {
                    Title = $"Generated via CSOM - {Guid.NewGuid()}",
                    TemplateType = (int)CSOMLib.ListTemplateType.GenericList
                };

                clientContext.Load(clientContext.Web.Lists);
                clientContext.ExecuteQuery();
                foreach (List list2 in clientContext.Web.Lists)
                {
                    try
                    {
                        if (list2.BaseType.ToString() == "DocumentLibrary")
                        {
                            // here u get all document library
                        }
                    }
                    catch
                    { }
                };

                var fileCreationInfo = new FileCreationInformation
                {
                    Content = System.IO.File.ReadAllBytes(@"c:\test\LogoSturni365Full.png"),
                    Overwrite = true,
                    Url = Path.GetFileName(@"c:\test\LogoSturni365Full.png")
                };
                var targetFolder = clientContext.Web.GetFolderByServerRelativeUrl(@"https://2h8d1h.sharepoint.com/sites/devsite1/test1");
                var uploadFile = targetFolder.Files.Add(fileCreationInfo);
                clientContext.Load(uploadFile);
                clientContext.ExecuteQuery();

                Site site = clientContext.Site;
                Web web = clientContext.Web;
                List list = web.Lists.GetByTitle("Test1");
                Folder newFolder = list.RootFolder.Folders.Add("F5");
                clientContext.ExecuteQuery();
                newFolder.ListItemAllFields.BreakRoleInheritance(false, true);
                var role = new RoleDefinitionBindingCollection(clientContext);
                role.Add(web.RoleDefinitions.GetByType(RoleType.Contributor));
                User user = web.EnsureUser("GradyA@2h8d1h.onmicrosoft.com");
                newFolder.ListItemAllFields.RoleAssignments.Add(user, role);
                newFolder.Update();
                clientContext.ExecuteQuery();

                var role2 = new RoleDefinitionBindingCollection(clientContext);
                role2.Add(web.RoleDefinitions.GetByType(RoleType.Reader));
                User user2 = web.EnsureUser("AdeleV@2h8d1h.onmicrosoft.com");
                newFolder.ListItemAllFields.RoleAssignments.Add(user2, role2);
                newFolder.Update();
                clientContext.ExecuteQuery();


                // Add the list to the site
                clientContext.Web.Lists.Add(newList);
                await clientContext.ExecuteQueryAsync();
            }

        }
        public async Task GetDocuments(CSOMCConfig cfg, string folder = "", CancellationToken cancellationToke = default)
        {
            var siteRelativeUri = cfg.Site;

            var spoTenant = cfg.SPOTenantName;

            // Connect to the target SPO site via CSOM
            using (var clientContext = await AuthenticationManager.CreateWithCertificate(
                cfg.ClientId,
                System.Security.Cryptography.X509Certificates.StoreName.My,
                System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser,
                cfg.CertificateThumbprint,
                cfg.TenantId)
                .GetContextAsync($"https://{spoTenant}{siteRelativeUri}"))
            {
                // Let's see if the current user is site admin
                var currentUser = clientContext.Web.CurrentUser;
                clientContext.Load(currentUser, u => u.IsSiteAdmin);
                await clientContext.ExecuteQueryAsync();

                Site site = clientContext.Site;
                Web web = clientContext.Web;
                List list = web.Lists.GetByTitle("Documents");
                clientContext.Load(list);
                clientContext.ExecuteQuery();

                //tutti i file escluse le folder, ricorsivo
                //non sembra funzionare
                //<Where><Eq><FieldRef Name='FSObjType' /><Value Type='Integer'>0</Value></Eq></Where><QueryOptions><ViewAttributes Scope='RecursiveAll' /></QueryOptions>

                //simple query: contiene file e folders
                //<View><RowLimit>1000</RowLimit></View>

                //<View Scope=\"RecursiveAll\"></View>"

                //<Query><Where><Eq><FieldRef Name='Title'/><Value Type='Text'>F4</Value></Eq></Where></Query>

                //filter by folder OK !!!
                //camlQuery.ViewXml = "<View Scope='FilesOnly' />";
                //camlQuery.FolderServerRelativeUrl = "/sites/DevSite1/Shared%20Documents/F4";

                folder = "F4";

                CamlQuery camlQuery = new CamlQuery();
                if (string.IsNullOrEmpty(folder))
                {
                    camlQuery.ViewXml = "<View Scope=\"RecursiveAll\"></View>\"";
                }
                else
                {
                    //non funziona
                    //camlQuery.ViewXml = "<View Scope='RecursiveAll'><Query><Where><Eq><FieldRef Name='FileDirRef'/><Value Type='Text'>/F4/</Value></Eq></Where></Query></View>";
                    //camlQuery.ViewXml = "<View Scope=\"RecursiveAll\"></View>";

                    camlQuery.ViewXml = "<View Scope='RecursiveAll' />";
                    camlQuery.FolderServerRelativeUrl = "/sites/DevSite1/Shared%20Documents/F4";
                }

                ListItemCollection collListItem = list.GetItems(camlQuery);

                clientContext.Load(collListItem,
                         items => items.Include(
                            item => item.Id,
                            item => item.DisplayName,
                            item => item.HasUniqueRoleAssignments,
                            items => items.FileSystemObjectType,
                            items => items.ContentType
                            ));

                clientContext.ExecuteQuery();

                foreach (ListItem oListItem in collListItem)
                {
                    if (oListItem.FileSystemObjectType == FileSystemObjectType.File)
                    {
                        // This is a File
                    }
                    else if (oListItem.FileSystemObjectType == FileSystemObjectType.Folder)
                    {
                        // This is a  Folder
                    }

                    System.Diagnostics.Trace.WriteLine(string.Format("ID: {0} \nDisplay name: {1} \nUnique role assignments: {2}",
                        oListItem.Id, oListItem.DisplayName, oListItem.HasUniqueRoleAssignments));
                }

                
            }

        }
        public async Task TestConnection(CSOMCConfig cfg, CancellationToken cancellationToke = default)
        {
            var siteRelativeUri = cfg.Site;

            var spoTenant = cfg.SPOTenantName;

            // Connect to the target SPO site via CSOM
            using (var clientContext = await AuthenticationManager.CreateWithCertificate(
                cfg.ClientId,
                System.Security.Cryptography.X509Certificates.StoreName.My,
                System.Security.Cryptography.X509Certificates.StoreLocation.CurrentUser,
                cfg.CertificateThumbprint,
                cfg .TenantId)
                .GetContextAsync($"https://{spoTenant}{siteRelativeUri}"))
            {
                var res = clientContext.GetContextSettings();
            }

        }
    }
}
