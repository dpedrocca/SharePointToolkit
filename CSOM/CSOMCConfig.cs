using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharePointToolkit.CSOM
{
    class CSOMCConfig
    {
        public string Site { get; set; }
        public string SPOTenantName { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string CertificateThumbprint { get; set; }
        public string CertificatePassword { get; set; }
        public string TenantId { get; set; }
        public string ListCreationInformation { get; set; }
        
    }
}
