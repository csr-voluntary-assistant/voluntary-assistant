$files = $(git diff HEAD HEAD~ --name-only)
$temp = $files -split ' '
$count = $temp.Length

echo "Total files changed: $count"

For ($i=0; $i -lt $temp.Length; $i++)
{
    $name=$temp[$i]
    echo "File changed: $name"

    if ($name -like "src/*")
    {
        Write-Host "##vso[task.setvariable variable=SOURCE_CODE_CHANGED]True"
    }
}