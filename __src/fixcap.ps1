param (
    [Parameter(Mandatory=$true)]
    [string]$htmlFilePath
)

# Modify the input file path to have the '.out' extension
# $outHtmlFilePath = $htmlFilePath -replace '\.html$', '.out'

# Read the content of the HTML file
$htmlContent = Get-Content -Path $htmlFilePath -Raw

# Perform the case-insensitive replacement of href attributes with their lowercase values
$modifiedHtmlContent = $htmlContent -replace '(?i)href="([^"]+)"', { 'href="{0}"' -f $_.Groups[1].Value.ToLower() }

# Save the modified HTML back to the file
$modifiedHtmlContent | Set-Content -Path $htmlFilePath -Force
