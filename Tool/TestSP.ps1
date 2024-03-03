#requires -modules "Microsoft.Graph"

<#
This Sample Code is provided for the purpose of illustration only and is not intended to be used in a production environment.  
THIS SAMPLE CODE AND ANY RELATED INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.  
We grant You a nonexclusive, royalty-free right to use and modify the Sample Code and to reproduce and distribute the object code form of the Sample Code, provided that You agree: (i) to not use Our name, logo, or trademarks to market 
Your software product in which the Sample Code is embedded; (ii) to include a valid copyright notice on Your software product in which the Sample Code is embedded; and (iii) to indemnify, hold harmless, 
and defend Us and Our suppliers from and against any claims or lawsuits, including attorneys’ fees, that arise or result from the use or distribution of the Sample Code.
## Please note: None of the conditions outlined in the disclaimer above will supersede the terms and conditions contained within the Premier Customer Services Description.
.Description
	- Adds Sites.Selected permission to a site for an App ID
  
.Parameters
	
.Author
	- Carl Grzywacz
.Modified
	- 2023-05-23 010:00 AM
  
 .Permissions Required
  - Application - Sites.FullControl.All
.EXAMPLE
  .\Add-VivaLearningListMetadata.ps1 -SiteUrl "https://tenant.sharepoint.com" -LibraryName "Viva Learning" -TermSetPath "Company|Departments"
#>

$role = "write";
$tenantName = "2h8d1h.sharepoint.com"
$serverRelativeSiteUrl = "/sites/DevTest1";
$site = Get-MgSite -SiteId "$($tenantName):$serverRelativeSiteUrl";

# Root Site example
#$site = Get-MgSite -SiteId $tenantName; 

$appIdForSitesDotSelected = "f79ac065-abc1-4afc-8ff5-2bae75711396";
$displayNameForSitesDotSelected = "Test Sharepoint Dav1"

Connect-MgGraph -Scopes "Sites.ReadWrite.All"

Get-MgSitePermission  -SiteId $site.Id

Import-Module Microsoft.Graph.Sites
$params = @{
	Roles = @(
		$role
	)
	GrantedToIdentities = @(
		@{
			Application = @{
				Id = $appIdForSitesDotSelected
				DisplayName = $displayNameForSitesDotSelected
			}
		}
	)
}
New-MgSitePermission -SiteId $site.Id -BodyParameter $params