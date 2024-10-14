[T4Scaffolding.Scaffolder(Description = "Create Repository")][CmdletBinding()]
param(        
    [parameter(Position = 0, Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
	[string]$ProjectName,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[string]$Area,
	[string]$ViewScaffolder = "View",
	[alias("MasterPage")]$Layout,
 	[alias("ContentPlaceholderIDs")][string[]]$SectionNames,
	[alias("PrimaryContentPlaceholderID")][string]$PrimarySectionName,
	[switch]$ReferenceScriptLibraries = $false,
	[switch]$Repository = $false,
	[switch]$NoChildItems = $false,
	[switch]$Force = $false,
	[string]$ForceMode
)

	$excluded = @("*.cs")
	[array] $result = @(get-childitem -Name $ProjectName -include $excluded)
	Foreach ($Model in $result)
	{	
	
		$pos = $Model.IndexOf('.')
		$ModelName = $Model.Substring(0, $pos)
		$ModelType = $ProjectName + '.' + $ModelName

		$foundModelType = Get-ProjectType $ModelType -Project $Project
		if (!$foundModelType) { continue }



		$primaryKey = Get-PrimaryKey $foundModelType.FullName -Project $Project -ErrorIfNotFound
		if (!$primaryKey) { continue }

		$outputPath = ($foundModelType.Name + "Repository")

		$modelTypePluralized = Get-PluralizedWord $foundModelType.Name
		$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
		$repositoryNamespace = [T4Scaffolding.Namespaces]::Normalize($defaultNamespace + "." + [System.IO.Path]::GetDirectoryName($outputPath).Replace([System.IO.Path]::DirectorySeparatorChar, "."))
		$modelTypeNamespace = [T4Scaffolding.Namespaces]::GetNamespace($foundModelType.FullName)

		Add-ProjectItemViaTemplate $outputPath -Template ZcfRepository -Model @{
			ModelType = [MarshalByRefObject]$foundModelType; 
			PrimaryKey = [string]$primaryKey; 
			DefaultNamespace = $defaultNamespace; 
			RepositoryNamespace = $repositoryNamespace; 
			ModelTypeNamespace = $modelTypeNamespace; 
			ModelTypePluralized = [string]$modelTypePluralized; 
		} -SuccessMessage "Added Repository '{0}'" -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force


		$outputPath = ("I" + $foundModelType.Name + "Repository")

		Add-ProjectItemViaTemplate $outputPath -Template ZcfIRepository -Model @{
			ModelType = [MarshalByRefObject]$foundModelType; 
			PrimaryKey = [string]$primaryKey; 
			DefaultNamespace = $defaultNamespace; 
			RepositoryNamespace = $repositoryNamespace; 
			ModelTypeNamespace = $modelTypeNamespace; 
			ModelTypePluralized = [string]$modelTypePluralized; 
		} -SuccessMessage "Added IRepository '{0}'" -TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force

	
				$defaultNamespaceService = $defaultNamespace -replace ".Data", ".Services"
				$repositoryNamespaceService = $repositoryNamespace -replace ".Data", ".Services"
				$ProjectService = $Project -replace ".Data", ".Services"
				(Get-Project $ProjectService).Object.References.Add("System.Data.Entity") | Out-Null

				#add Service
				$outputPathService = ($foundModelType.Name + "Service")
				Add-ProjectItemViaTemplate $outputPathService -Template ZcfService -Model @{
					ModelType = [MarshalByRefObject]$foundModelType; 
					PrimaryKey = [string]$primaryKey; 
					DefaultNamespace = $defaultNamespaceService; 
					RepositoryNamespace = $repositoryNamespaceService; 
					ModelTypeNamespace = $modelTypeNamespace; 
					ModelTypePluralized = [string]$modelTypePluralized; 
				} -SuccessMessage "Added Service '{0}'" -TemplateFolders $TemplateFolders -Project $ProjectService -CodeLanguage $CodeLanguage -Force:$Force

				$outputPathService = ("I" + $foundModelType.Name + "Service")
				Add-ProjectItemViaTemplate $outputPathService -Template ZcfIService -Model @{
					ModelType = [MarshalByRefObject]$foundModelType; 
					PrimaryKey = [string]$primaryKey; 
					DefaultNamespace = $defaultNamespaceService; 
					RepositoryNamespace = $repositoryNamespaceService; 
					ModelTypeNamespace = $modelTypeNamespace; 
					ModelTypePluralized = [string]$modelTypePluralized; 
				} -SuccessMessage "Added IService '{0}'" -TemplateFolders $TemplateFolders -Project $ProjectService -CodeLanguage $CodeLanguage -Force:$Force



# Interpret the "Force" and "ForceMode" options
$overwriteController = $Force -and ((!$ForceMode) -or ($ForceMode -eq "ControllerOnly"))
$overwriteFilesExceptController = $Force -and ((!$ForceMode) -or ($ForceMode -eq "PreserveController"))

# If you haven't specified a model type, we'll guess from the controller name
if (!$ModelType) 
{
	if ($ControllerName.EndsWith("Controller", [StringComparison]::OrdinalIgnoreCase)) 
	{
			# If you've given "PeopleController" as the full controller name, we're looking for a model called People or Customer
			$ModelType = [System.Text.RegularExpressions.Regex]::Replace($ControllerName, "Controller$", "", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
			$foundModelType = Get-ProjectType $ModelType -Project $Project -ErrorAction SilentlyContinue
			if (!$foundModelType) {
				$ModelType = [string](Get-SingularizedWord $ModelType)
				$foundModelType = Get-ProjectType $ModelType -Project $Project -ErrorAction SilentlyContinue
			}
	} 
	else 
	{
		# If you've given "people" as the controller name, we're looking for a model called People or Customer, and the controller will be PeopleController
		$ModelType = $ControllerName
		$foundModelType = Get-ProjectType $ModelType -Project $Project -ErrorAction SilentlyContinue
		if (!$foundModelType) 
		{
			$ModelType = [string](Get-SingularizedWord $ModelType)
			$foundModelType = Get-ProjectType $ModelType -Project $Project -ErrorAction SilentlyContinue
		}
		if ($foundModelType) 
		{
			$ControllerName = [string]($foundModelType.Name) + "Controller"
			# $ControllerName = [string](Get-PluralizedWord $foundModelType.Name) + "Controller"
		}
	}
	if (!$foundModelType) { throw "Cannot find a model type corresponding to a controller called '$ControllerName'. Try supplying a -ModelType parameter value." }
	} 
	else 
	{
	# If you have specified a model type
	$foundModelType = Get-ProjectType $ModelType -Project $Project
	if (!$foundModelType) { return }
	if (!$ControllerName.EndsWith("Controller", [StringComparison]::OrdinalIgnoreCase)) {
		$ControllerName = $ControllerName + "Controller"
	}
}
Write-Host "Scaffolding $ControllerName..."

$ControllerName = $foundModelType.Name + "Controller"

$primaryKey = Get-PrimaryKey $foundModelType.FullName -Project $Project -ErrorIfNotFound
if (!$primaryKey) { return }

$outputPath = Join-Path Controllers $ControllerName
# We don't create areas here, so just ensure that if you specify one, it already exists
if ($Area) {
	$areaPath = Join-Path Areas $Area
	if (-not (Get-ProjectItem $areaPath -Project $Project)) {
		Write-Error "Cannot find area '$Area'. Make sure it exists already."
		return
	}
	$outputPath = Join-Path $areaPath $outputPath
}

# Prepare all the parameter values to pass to the template, then invoke the template with those values
$repositoryName = $foundModelType.Name + "Repository"
$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value
$modelTypeNamespace = [T4Scaffolding.Namespaces]::GetNamespace($foundModelType.FullName)
$controllerNamespace = [T4Scaffolding.Namespaces]::Normalize($defaultNamespace + "." + [System.IO.Path]::GetDirectoryName($outputPath).Replace([System.IO.Path]::DirectorySeparatorChar, "."))
$areaNamespace = if ($Area) { [T4Scaffolding.Namespaces]::Normalize($defaultNamespace + ".Areas.$Area") } else { $defaultNamespace }
$dbContextNamespace = $foundDbContextType.Namespace.FullName
$repositoriesNamespace = [T4Scaffolding.Namespaces]::Normalize($areaNamespace + ".Models")
$modelTypePluralized = Get-PluralizedWord $foundModelType.Name
$relatedEntities = [Array](Get-RelatedEntities $foundModelType.FullName -Project $project)
if (!$relatedEntities) { $relatedEntities = @() }

$defaultNamespaceController = $defaultNamespace -replace ".Data", ".Web.UI"
$repositoryNamespaceController = $repositoryNamespace -replace ".Data", ".Web.UI"
$ProjectController = $Project -replace ".Data", ".Web.UI"
$controllerNamespace = $defaultNamespaceController

$templateName = if($Repository) { "ZcfController" } else { "ZcfController" }
Add-ProjectItemViaTemplate $outputPath -Template $templateName -Model @{
	ControllerName = $ControllerName;
	ModelType = [MarshalByRefObject]$foundModelType; 
	PrimaryKey = [string]$primaryKey; 
	DefaultNamespace = $defaultNamespaceController; 
	AreaNamespace = $areaNamespace; 
	RepositoriesNamespace = $repositoriesNamespace;
	ModelTypeNamespace = $modelTypeNamespace; 
	ControllerNamespace = $controllerNamespace; 
	Repository = $repositoryName; 
	ModelTypePluralized = [string]$modelTypePluralized; 
	RelatedEntities = $relatedEntities;
} -SuccessMessage "Added controller {0}" -TemplateFolders $TemplateFolders -Project $ProjectController -CodeLanguage $CodeLanguage -Force:$overwriteController

if (!$NoChildItems) {
	$controllerNameWithoutSuffix = [System.Text.RegularExpressions.Regex]::Replace($ControllerName, "Controller$", "", [System.Text.RegularExpressions.RegexOptions]::IgnoreCase)
	if ($ViewScaffolder) {
		Scaffold Views -ViewScaffolder $ViewScaffolder -Controller $controllerNameWithoutSuffix -ModelType $foundModelType.FullName -Area $Area -Layout $Layout -SectionNames $SectionNames -PrimarySectionName $PrimarySectionName -ReferenceScriptLibraries:$ReferenceScriptLibraries -Project $ProjectController -CodeLanguage $CodeLanguage -Force:$overwriteFilesExceptController
	}
}

}
	
