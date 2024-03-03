using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SharePointToolkit.Helpers
{
    class X509CertificateUtility
    {
        internal static X509Certificate2 LoadCertificate(StoreName storeName, StoreLocation storeLocation, string thumbprint)
        {
            // The following code gets the cert from the keystore
            using (X509Store store = new X509Store(storeName, storeLocation))
            {
                store.Open(OpenFlags.ReadOnly);

                X509Certificate2Collection certCollection =
                        store.Certificates.Find(X509FindType.FindByThumbprint,
                        thumbprint, false);

                X509Certificate2Enumerator enumerator = certCollection.GetEnumerator();

                X509Certificate2 cert = null;

                while (enumerator.MoveNext())
                {
                    cert = enumerator.Current;
                }

                return cert;
            }
        }
    }
}
