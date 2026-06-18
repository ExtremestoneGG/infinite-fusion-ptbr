# Installer

## English

The PT-BR installer is a plain PowerShell patcher. It was intentionally kept readable and uncompiled so players can inspect what it does before running it.

It performs four actions:

1. Validates that the selected folder contains `Data` and `Data/Scripts`.
2. Creates a timestamped backup under `PTBR_BACKUPS`.
3. Copies the translated language/script files from `payload/files`.
4. Applies `payload/patches/text_patches.json` to JSON text fields only.

The installer does not download files, does not contact the internet, does not include the game, and does not require administrator permissions.

### Files

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

## Português

O instalador PT-BR é um patcher simples em PowerShell. Ele foi mantido aberto e sem compilar para que qualquer pessoa possa abrir o arquivo e conferir o que ele faz antes de executar.

Ele faz quatro coisas:

1. Verifica se a pasta escolhida contém `Data` e `Data/Scripts`.
2. Cria um backup com data e hora dentro de `PTBR_BACKUPS`.
3. Copia os arquivos traduzidos de idioma/scripts a partir de `payload/files`.
4. Aplica `payload/patches/text_patches.json` apenas em campos de texto dos JSONs.

O instalador não baixa arquivos, não acessa a internet, não inclui o jogo e não precisa de permissões de administrador.

### Arquivos

- `installer/Install-PTBR.bat`: forma mais simples de instalar.
- `installer/Install-PTBR.ps1`: código aberto do instalador.
- `installer/Restore-Latest-Backup.bat`: restaura o backup mais recente criado pelo instalador.
- `payload/files`: dados de idioma e scripts de interface traduzidos.
- `payload/patches/text_patches.json`: mapeamentos de texto da Pokédex e descrições de roupas.
