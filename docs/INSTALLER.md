# Installer

## English

The PT-BR installer is a plain PowerShell patcher. It was intentionally kept readable and uncompiled so players can inspect what it does before running it.

## Installation Tutorial

1. Download the game only from the official Pokemon Infinite Fusion Discord.
2. Download the translation ZIP from this repository's Releases page.
3. Extract the ZIP.
4. Run `installer/InfiniteFusionPTBRInstaller.exe`.
5. Select the main Pokemon Infinite Fusion folder.
6. Confirm the install prompt.
7. Wait for the copy and patch steps to finish.
8. Open the game and select `Português` in the language menu.

The selected game folder should contain `Game.exe` and `Data`.

It performs four actions:

1. Validates that the selected folder contains `Data` and `Data/Scripts`.
2. Creates a timestamped backup under `PTBR_BACKUPS`.
3. Copies the translated language/script files from `payload/files`.
4. Applies `payload/patches/text_patches.json` to JSON text fields only.

The installer does not download files, does not contact the internet, does not include the game, and does not require administrator permissions.

The recommended installer is the `.exe` graphical installer. The `.bat` and `.ps1` files remain available as a transparent fallback.

### Files

- `installer/InfiniteFusionPTBRInstaller.exe`: recommended graphical installer.
- `installer-src/InfiniteFusionPtbrInstaller.cs`: source code for the graphical installer.
- `installer/Install-PTBR.bat`: easiest install entry point.
- `installer/Install-PTBR.ps1`: readable installer source.
- `installer/Restore-Latest-Backup.bat`: restores the newest backup created by the installer.
- `payload/files`: translated language data and script-level UI files.
- `payload/patches/text_patches.json`: Pokedex and outfit description text mappings.

### Command Line

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File installer\Install-PTBR.ps1 -GameDir "D:\Games\InfiniteFusion"
powershell -NoProfile -ExecutionPolicy Bypass -File installer\Install-PTBR.ps1 -GameDir "D:\Games\InfiniteFusion" -RestoreLatest
powershell -NoProfile -ExecutionPolicy Bypass -File installer\Install-PTBR.ps1 -GameDir "D:\Games\InfiniteFusion" -ValidateOnly
```

## Restore Tutorial

1. Open `installer/InfiniteFusionPTBRInstaller.exe`.
2. Select the same game folder.
3. Click `Restore Latest Backup`.
4. The newest backup in `PTBR_BACKUPS` is restored.

Fallback option: run `installer/Restore-Latest-Backup.bat`.

Use this if the game update overwrites files, if you want to test another version, or if anything feels wrong after installation.

## Português

O instalador PT-BR é um patcher simples em PowerShell. Ele foi mantido aberto e sem compilar para que qualquer pessoa possa abrir o arquivo e conferir o que ele faz antes de executar.

## Tutorial De Instalação

1. Baixe o jogo somente pelo Discord oficial de Pokemon Infinite Fusion.
2. Baixe o ZIP da tradução pela página de Releases deste repositório.
3. Extraia o ZIP.
4. Execute `installer/InfiniteFusionPTBRInstaller.exe`.
5. Selecione a pasta principal do Pokemon Infinite Fusion.
6. Confirme a janela de instalação.
7. Aguarde a cópia e os patches terminarem.
8. Abra o jogo e selecione `Português` no menu de idioma.

A pasta escolhida deve conter `Game.exe` e `Data`.

Ele faz quatro coisas:

1. Verifica se a pasta escolhida contém `Data` e `Data/Scripts`.
2. Cria um backup com data e hora dentro de `PTBR_BACKUPS`.
3. Copia os arquivos traduzidos de idioma/scripts a partir de `payload/files`.
4. Aplica `payload/patches/text_patches.json` apenas em campos de texto dos JSONs.

O instalador não baixa arquivos, não acessa a internet, não inclui o jogo e não precisa de permissões de administrador.

O instalador recomendado é o `.exe` com interface gráfica. Os arquivos `.bat` e `.ps1` continuam disponíveis como alternativa transparente.

### Arquivos

- `installer/InfiniteFusionPTBRInstaller.exe`: instalador gráfico recomendado.
- `installer-src/InfiniteFusionPtbrInstaller.cs`: código-fonte do instalador gráfico.
- `installer/Install-PTBR.bat`: forma mais simples de instalar.
- `installer/Install-PTBR.ps1`: código aberto do instalador.
- `installer/Restore-Latest-Backup.bat`: restaura o backup mais recente criado pelo instalador.
- `payload/files`: dados de idioma e scripts de interface traduzidos.
- `payload/patches/text_patches.json`: mapeamentos de texto da Pokédex e descrições de roupas.

## Tutorial De Restauração

1. Abra `installer/InfiniteFusionPTBRInstaller.exe`.
2. Selecione a mesma pasta do jogo.
3. Clique em `Restore Latest Backup`.
4. O backup mais recente dentro de `PTBR_BACKUPS` será restaurado.

Alternativa: execute `installer/Restore-Latest-Backup.bat`.

Use isso se uma atualização do jogo sobrescrever arquivos, se você quiser testar outra versão ou se algo parecer errado depois da instalação.
