[T4Scaffolding.Scaffolder(Description = "Create Repository")][CmdletBinding()]
param(        
    [parameter(Position = 0, Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
	[string]$ModelType,
    [string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $false
)
	# Ensure you've referenced System.Data.Entity
	(Get-Project $Project).Object.References.Add("System.Data.Entity") | Out-Null
	
	$foundModelType = Get-ProjectType $ModelType -Project $Project
	if (!$foundModelType) { return }
	
	$primaryKey = Get-PrimaryKey $foundModelType.FullName -Project $Project -ErrorIfNotFound
	if (!$primaryKey) { return }

	$outputPath = ($foundModelType.Name + "Repository")
	# $outputPath = Join-Path Repository ($foundModelType.Name + "Repository")

	

# $outputPath = "ExampleOutput"  # The filename extension will be added based on the template's <#@ Output Extension="..." #> directive
# $namespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value

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