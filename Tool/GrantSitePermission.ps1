Import-Module PnP.PowerShell

$siteurl = "https://2h8d1h.sharepoint.com/sites/DevSite1"

Connect-PnPOnline -Url $siteurl -DeviceLogin -LaunchBrowser

Grant-PnPAzureADAppSitePermission -AppId 'f79ac065-abc1-4afc-8ff5-2bae75711396' -DisplayName 'SharePoint1' -Site $siteurl -Permissions Write

Get-PnPAzureADAppSitePermission

#change permissione to app
Set-PnPAzureADAppSitePermission -PermissionId 'aTowaS50fG1zLnNwLmV4dHxmNzlhYzA2NS1hYmMxLTRhZmMtOGZmNS0yYmFlNzU3MTEzOTZAM2Y4ZGJmMzUtZmU4ZS00YjVkLTlhNzYtMTlkMzI1MDZiNzI5' -Permissions FullControl