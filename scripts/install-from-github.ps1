param(
    [string]$GameDir = (Get-Location).Path,
    [string]$Version = "v1.1.3"
)

$ErrorActionPreference = "Stop"

$repo = "ExtremestoneGG/infinite-fusion-ptbr"
$assetName = "PTBR-Translation-BAT-v1.1.3.zip"
$downloadUrl = "https://github.com/$repo/releases/download/$Version/$assetName"

function Assert-GameDir {
    param([string]$Path)
    $full = [System.IO.Path]::GetFullPath($Path)
    if (-not (Test-Path -LiteralPath $full -PathType Container)) {
        throw "Game folder does not exist: $full"
    }
    if (-not (Test-Path -LiteralPath (Join-Path $full "Game.exe") -PathType Leaf)) {
        throw "This does not look like the Pokemon Infinite Fusion folder: missing Game.exe."
    }
    if (-not (Test-Path -LiteralPath (Join-Path $full "Data") -PathType Container)) {
        throw "This does not look like the Pokemon Infinite Fusion folder: missing Data folder."
    }
    if (-not (Test-Path -LiteralPath (Join-Path $full "Data\Scripts") -PathType Container)) {
        throw "This does not look like the Pokemon Infinite Fusion folder: missing Data\Scripts folder."
    }
    return $full
}

$gameRoot = Assert-GameDir $GameDir
$workRoot = Join-Path $gameRoot "PTBR_INSTALLER_TEMP"
$zipPath = Join-Path $workRoot $assetName
$extractPath = Join-Path $workRoot "extracted"

Write-Host "Pokemon Infinite Fusion PT-BR Mod installer"
Write-Host "Game folder: $gameRoot"
Write-Host "Downloading from: $downloadUrl"
Write-Host ""
Write-Host "Reminder: this script downloads only the fan translation from GitHub."
Write-Host "Download the game itself only from the official Pokemon Infinite Fusion Discord."
Write-Host ""

if (Test-Path -LiteralPath $workRoot) {
    Remove-Item -LiteralPath $workRoot -Recurse -Force
}
New-Item -ItemType Directory -Force -Path $extractPath | Out-Null

try {
    Invoke-WebRequest -Uri $downloadUrl -OutFile $zipPath -UseBasicParsing
    Expand-Archive -LiteralPath $zipPath -DestinationPath $extractPath -Force

    $installer = Join-Path $extractPath "installer\Install-PTBR.ps1"
    if (-not (Test-Path -LiteralPath $installer -PathType Leaf)) {
        throw "Downloaded package did not contain installer\Install-PTBR.ps1"
    }

    $args = @("-NoProfile", "-ExecutionPolicy", "Bypass", "-File", $installer, "-GameDir", $gameRoot, "-Quiet")

    Write-Host "Installing PT-BR translation..."
    $process = Start-Process -FilePath "powershell.exe" -ArgumentList $args -Wait -PassThru -NoNewWindow
    if ($process.ExitCode -ne 0) {
        throw "Installer failed with exit code $($process.ExitCode)."
    }
    Write-Host ""
    Write-Host "Done. Backup was created in the game folder."
} finally {
    if (Test-Path -LiteralPath $workRoot) {
        Remove-Item -LiteralPath $workRoot -Recurse -Force -ErrorAction SilentlyContinue
    }
}
