param (
    [Parameter(Mandatory=$true)]
    [string]$jsonFilePath
)

# Read the content of the json file
$jsonContent = Get-Content -Path $jsonFilePath -Raw | ConvertFrom-Json

foreach ($key in $jsonContent.PSObject.Properties.Name) {
    $jsonContent.$key.href = $jsonContent.$key.href.ToLower()
}

# Convert the modified object back to JSON and save it to the file
$jsonContent | ConvertTo-Json -Depth 100 | Set-Content -Path $jsonFilePath
