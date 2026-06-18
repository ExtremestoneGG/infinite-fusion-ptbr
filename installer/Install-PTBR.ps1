param(
    [string]$GameDir = "",
    [switch]$NoBackup,
    [switch]$RestoreLatest,
    [switch]$ValidateOnly,
    [switch]$Quiet
)

$ErrorActionPreference = "Stop"

function Get-PackageRoot {
    $scriptDir = Split-Path -Parent $PSCommandPath
    return Split-Path -Parent $scriptDir
}

function Write-Utf8NoBom {
    param([string]$Path, [string]$Text)
    $encoding = New-Object System.Text.UTF8Encoding($false)
    [System.IO.File]::WriteAllText($Path, $Text, $encoding)
}

function Read-JsonFile {
    param([string]$Path)
    return [System.IO.File]::ReadAllText($Path, [System.Text.Encoding]::UTF8) | ConvertFrom-Json
}

function Convert-PairsToDictionary {
    param([object]$Pairs)
    $dictionary = New-Object "System.Collections.Generic.Dictionary[String,String]" ([System.StringComparer]::Ordinal)
    if ($null -eq $Pairs) { return $dictionary }
    foreach ($pair in $Pairs) {
        $source = [string]$pair.source
        $target = [string]$pair.target
        if (-not $dictionary.ContainsKey($source)) {
            $dictionary.Add($source, $target)
        }
    }
    return $dictionary
}

function Show-Info {
    param([string]$Message)
    Write-Host $Message
}

function Show-Message {
    param([string]$Message, [string]$Title = "Infinite Fusion PT-BR")
    if ($Quiet) { return }
    try {
        Add-Type -AssemblyName System.Windows.Forms -ErrorAction SilentlyContinue
        [System.Windows.Forms.MessageBox]::Show($Message, $Title, "OK", "Information") | Out-Null
    } catch {
        Write-Host $Message
    }
}

function Select-GameFolder {
    if ($Quiet) { throw "GameDir is required when running in quiet mode." }
    Add-Type -AssemblyName System.Windows.Forms
    $dialog = New-Object System.Windows.Forms.FolderBrowserDialog
    $dialog.Description = "Select your Pokemon Infinite Fusion game folder. The game itself should only be downloaded from the official Pokemon Infinite Fusion Discord."
    $dialog.ShowNewFolderButton = $false
    if ($dialog.ShowDialog() -ne [System.Windows.Forms.DialogResult]::OK) {
        throw "Installation cancelled."
    }
    return $dialog.SelectedPath
}

function Confirm-Install {
    param([string]$SelectedGameDir, [bool]$BackupEnabled)
    if ($Quiet) { return }
    Add-Type -AssemblyName System.Windows.Forms
    $backupText = if ($BackupEnabled) { "A backup will be created before changes are applied." } else { "Backup is disabled." }
    $message = @"
Pokemon Infinite Fusion PT-BR Fan Translation v1.0.0

This is an unofficial fan-made translation patch. It does not include the game and does not download anything.

Game folder:
$SelectedGameDir

$backupText

Only install Pokemon Infinite Fusion from the official Pokemon Infinite Fusion Discord maintained by the game developers. Do not download the game from random websites or mirrors.

Continue?
"@
    $result = [System.Windows.Forms.MessageBox]::Show($message, "Install PT-BR Translation", "YesNo", "Warning")
    if ($result -ne [System.Windows.Forms.DialogResult]::Yes) {
        throw "Installation cancelled."
    }
}

function Assert-GameDir {
    param([string]$Path)
    if ([string]::IsNullOrWhiteSpace($Path)) { throw "Game folder was not selected." }
    $full = [System.IO.Path]::GetFullPath($Path)
    if (-not (Test-Path -LiteralPath $full -PathType Container)) {
        throw "Game folder does not exist: $full"
    }
    if (-not (Test-Path -LiteralPath (Join-Path $full "Data") -PathType Container)) {
        throw "This does not look like the game folder: missing Data folder."
    }
    if (-not (Test-Path -LiteralPath (Join-Path $full "Data\Scripts") -PathType Container)) {
        throw "This does not look like the game folder: missing Data\Scripts folder."
    }
    $exeFound = (Test-Path -LiteralPath (Join-Path $full "Game.exe")) -or (Test-Path -LiteralPath (Join-Path $full "infinitefusion.exe")) -or (Test-Path -LiteralPath (Join-Path $full "InfiniteFusion.exe"))
    if (-not $exeFound) {
        Show-Info "Warning: no common game executable was found, but Data and Data\Scripts exist. Continuing."
    }
    return $full
}

function Join-UnderRoot {
    param([string]$Root, [string]$RelativePath)
    $combined = [System.IO.Path]::GetFullPath((Join-Path $Root $RelativePath))
    $rootFull = [System.IO.Path]::GetFullPath($Root).TrimEnd("\", "/") + [System.IO.Path]::DirectorySeparatorChar
    if (-not $combined.StartsWith($rootFull, [System.StringComparison]::OrdinalIgnoreCase)) {
        throw "Unsafe path outside game folder: $RelativePath"
    }
    return $combined
}

$script:BackupEntries = @()
$script:BackedUp = @{}

function Backup-RelativeFile {
    param([string]$GameRoot, [string]$BackupRoot, [string]$RelativePath, [bool]$BackupEnabled)
    if (-not $BackupEnabled) { return }
    if ($script:BackedUp.ContainsKey($RelativePath)) { return }

    $target = Join-UnderRoot $GameRoot $RelativePath
    $backupTarget = Join-UnderRoot $BackupRoot $RelativePath
    $backupParent = Split-Path -Parent $backupTarget
    New-Item -ItemType Directory -Force -Path $backupParent | Out-Null

    if (Test-Path -LiteralPath $target -PathType Leaf) {
        Copy-Item -LiteralPath $target -Destination $backupTarget -Force
        $script:BackupEntries += [pscustomobject]@{ path = $RelativePath; action = "restore_file" }
    } else {
        $script:BackupEntries += [pscustomobject]@{ path = $RelativePath; action = "delete_created" }
    }
    $script:BackedUp[$RelativePath] = $true
}

function Copy-DirectFiles {
    param([string]$PackageRoot, [string]$GameRoot, [string]$BackupRoot, [bool]$BackupEnabled)
    $payloadRoot = Join-Path $PackageRoot "payload\files"
    $payloadFull = [System.IO.Path]::GetFullPath($payloadRoot).TrimEnd("\", "/")
    $copied = 0
    foreach ($file in Get-ChildItem -LiteralPath $payloadRoot -Recurse -File) {
        $relative = $file.FullName.Substring($payloadFull.Length).TrimStart("\", "/")
        Backup-RelativeFile $GameRoot $BackupRoot $relative $BackupEnabled
        $target = Join-UnderRoot $GameRoot $relative
        New-Item -ItemType Directory -Force -Path (Split-Path -Parent $target) | Out-Null
        Copy-Item -LiteralPath $file.FullName -Destination $target -Force
        $copied++
    }
    return $copied
}

function Apply-EntryPatch {
    param([string]$GameRoot, [string]$BackupRoot, [hashtable]$Map, [string]$RelativePath, [bool]$BackupEnabled)
    $target = Join-UnderRoot $GameRoot $RelativePath
    if (-not (Test-Path -LiteralPath $target -PathType Leaf)) {
        Show-Info "Skipping missing JSON file: $RelativePath"
        return 0
    }
    Backup-RelativeFile $GameRoot $BackupRoot $RelativePath $BackupEnabled
    $data = Read-JsonFile $target
    $changed = 0
    foreach ($row in $data) {
        if ($null -ne $row.entry) {
            $old = [string]$row.entry
            if ($Map.ContainsKey($old)) {
                $row.entry = $Map[$old]
                $changed++
            }
        }
    }
    if ($changed -gt 0) {
        $json = ConvertTo-Json -InputObject @($data) -Depth 80
        Write-Utf8NoBom $target ($json + [Environment]::NewLine)
    }
    return $changed
}

function Apply-DescriptionPatch {
    param([string]$GameRoot, [string]$BackupRoot, [hashtable]$Map, [string]$RelativePath, [bool]$BackupEnabled)
    $target = Join-UnderRoot $GameRoot $RelativePath
    if (-not (Test-Path -LiteralPath $target -PathType Leaf)) {
        Show-Info "Skipping missing JSON file: $RelativePath"
        return 0
    }
    Backup-RelativeFile $GameRoot $BackupRoot $RelativePath $BackupEnabled
    $data = Read-JsonFile $target
    $changed = 0
    foreach ($row in $data) {
        if ($null -ne $row.description) {
            $old = [string]$row.description
            if ($Map.ContainsKey($old)) {
                $row.description = $Map[$old]
                $changed++
            }
        }
    }
    if ($changed -gt 0) {
        $json = ConvertTo-Json -InputObject @($data) -Depth 80
        Write-Utf8NoBom $target ($json + [Environment]::NewLine)
    }
    return $changed
}

function Install-Translation {
    param([string]$GameRoot, [bool]$BackupEnabled)
    $packageRoot = Get-PackageRoot
    $manifestPath = Join-Path $packageRoot "manifest.json"
    $patchPath = Join-Path $packageRoot "payload\patches\text_patches.json"
    if (-not (Test-Path -LiteralPath $manifestPath -PathType Leaf)) { throw "Missing manifest.json." }
    if (-not (Test-Path -LiteralPath $patchPath -PathType Leaf)) { throw "Missing payload\patches\text_patches.json." }

    $timestamp = Get-Date -Format "yyyyMMdd_HHmmss"
    $backupRoot = Join-Path $GameRoot "PTBR_BACKUPS\ptbr_$timestamp"
    if ($BackupEnabled) {
        New-Item -ItemType Directory -Force -Path $backupRoot | Out-Null
    }

    $copied = 0
    $dexChanged = 0
    $baseDexChanged = 0
    $descriptionChanged = 0
    try {
        Show-Info "Copying translated language and script files..."
        $copied = Copy-DirectFiles $packageRoot $GameRoot $backupRoot $BackupEnabled

        Show-Info "Applying JSON text patches. This can take a few minutes on older machines..."
        $patch = Read-JsonFile $patchPath
        $entryMap = Convert-PairsToDictionary $patch.entry_translations
        $dexChanged = Apply-EntryPatch $GameRoot $backupRoot $entryMap "Data\pokedex\dex.json" $BackupEnabled
        $baseDexChanged = Apply-EntryPatch $GameRoot $backupRoot $entryMap "Data\dex.json" $BackupEnabled

        foreach ($section in $patch.outfit_description_translations) {
            $relative = ([string]$section.path) -replace "/", "\"
            $map = Convert-PairsToDictionary $section.translations
            $descriptionChanged += Apply-DescriptionPatch $GameRoot $backupRoot $map $relative $BackupEnabled
        }
    } finally {
        if ($BackupEnabled -and $script:BackupEntries.Count -gt 0) {
            $backupManifest = [pscustomobject]@{
                version = "1.0.0"
                created_on = (Get-Date).ToString("s")
                game_dir = $GameRoot
                entries = $script:BackupEntries
            }
            $backupJson = $backupManifest | ConvertTo-Json -Depth 8
            Write-Utf8NoBom (Join-Path $backupRoot "backup_manifest.json") ($backupJson + [Environment]::NewLine)
        }
    }

    $summary = @"
PT-BR translation installed.

Files copied: $copied
Pokedex entries patched: $dexChanged
Base dex entries patched: $baseDexChanged
Outfit descriptions patched: $descriptionChanged
Backup: $(if ($BackupEnabled) { $backupRoot } else { "disabled" })
"@
    Show-Info $summary
    Show-Message $summary
}

function Restore-LatestBackup {
    param([string]$GameRoot)
    $backupBase = Join-Path $GameRoot "PTBR_BACKUPS"
    if (-not (Test-Path -LiteralPath $backupBase -PathType Container)) {
        throw "No PTBR_BACKUPS folder found."
    }
    $latest = Get-ChildItem -LiteralPath $backupBase -Directory | Sort-Object LastWriteTime -Descending | Select-Object -First 1
    if ($null -eq $latest) { throw "No backups found." }
    $manifestPath = Join-Path $latest.FullName "backup_manifest.json"
    if (-not (Test-Path -LiteralPath $manifestPath -PathType Leaf)) {
        throw "Backup manifest not found: $manifestPath"
    }
    $manifest = Read-JsonFile $manifestPath
    $restored = 0
    $deleted = 0
    foreach ($entry in $manifest.entries) {
        $relative = [string]$entry.path
        $target = Join-UnderRoot $GameRoot $relative
        $backupFile = Join-UnderRoot $latest.FullName $relative
        if ($entry.action -eq "restore_file") {
            if (Test-Path -LiteralPath $backupFile -PathType Leaf) {
                New-Item -ItemType Directory -Force -Path (Split-Path -Parent $target) | Out-Null
                Copy-Item -LiteralPath $backupFile -Destination $target -Force
                $restored++
            }
        } elseif ($entry.action -eq "delete_created") {
            if (Test-Path -LiteralPath $target -PathType Leaf) {
                Remove-Item -LiteralPath $target -Force
                $deleted++
            }
        }
    }
    $summary = "Restored latest PT-BR backup: $($latest.FullName)`nFiles restored: $restored`nCreated files removed: $deleted"
    Show-Info $summary
    Show-Message $summary
}

try {
    if ([string]::IsNullOrWhiteSpace($GameDir)) {
        $GameDir = Select-GameFolder
    }
    $GameDir = Assert-GameDir $GameDir

    if ($ValidateOnly) {
        Show-Message "Folder validated successfully:`n$GameDir"
        exit 0
    }

    if ($RestoreLatest) {
        Restore-LatestBackup $GameDir
        exit 0
    }

    $backupEnabled = -not $NoBackup
    Confirm-Install $GameDir $backupEnabled
    Install-Translation $GameDir $backupEnabled
} catch {
    $msg = "PT-BR installer stopped: $($_.Exception.Message)"
    Write-Host $msg
    Show-Message $msg "Infinite Fusion PT-BR - Error"
    exit 1
}
