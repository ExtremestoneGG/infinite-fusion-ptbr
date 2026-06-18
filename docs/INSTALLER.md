# Installer

## English

The recommended PT-BR installer is a small graphical Windows `.exe`. It asks for the Pokemon Infinite Fusion folder, creates a backup by default, installs the translated files, and can restore the latest backup if the player runs it again.

The `.exe` package does not include `.bat` files. A separate BAT/PowerShell package is available for users who prefer readable scripts or want a one-line PowerShell install.

## Installation Tutorial

1. Download the game only from the official Pokemon Infinite Fusion Discord.
2. Download `PTBR-Translation-v1.0.0.zip` from this repository's Releases page.
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

The `.exe` installer does not download files, does not contact the internet, does not include the game, and does not require administrator permissions.

The BAT/PowerShell method is shipped separately as `PTBR-Translation-BAT-v1.0.0.zip`. See [BAT_INSTALLER.md](BAT_INSTALLER.md) for that tutorial.

### Files

- `installer/InfiniteFusionPTBRInstaller.exe`: recommended graphical installer.
- `installer-src/InfiniteFusionPtbrInstaller.cs`: source code for the graphical installer.
- `payload/files`: translated language data and script-level UI files.
- `payload/patches/text_patches.json`: Pokedex and outfit description text mappings.

## Restore Tutorial

1. Open `installer/InfiniteFusionPTBRInstaller.exe`.
2. Select the same game folder.
3. Click `Restore Latest Backup`.
4. The newest backup in `PTBR_BACKUPS` is restored.

Fallback option: if you installed through the separate BAT package, run `installer/Restore-Latest-Backup.bat`.

Use this if the game update overwrites files, if you want to test another version, or if anything feels wrong after installation.

## Português

O instalador PT-BR recomendado é um pequeno `.exe` gráfico para Windows. Ele pede a pasta do Pokemon Infinite Fusion, cria backup por padrão, instala os arquivos traduzidos e consegue restaurar o backup mais recente se a pessoa executar de novo.

O pacote `.exe` não inclui arquivos `.bat`. Um pacote separado em BAT/PowerShell está disponível para quem prefere scripts legíveis ou quer instalar por uma linha no PowerShell.

## Tutorial De Instalação

1. Baixe o jogo somente pelo Discord oficial de Pokemon Infinite Fusion.
2. Baixe `PTBR-Translation-v1.0.0.zip` pela página de Releases deste repositório.
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

O instalador `.exe` não baixa arquivos, não acessa a internet, não inclui o jogo e não precisa de permissões de administrador.

O método BAT/PowerShell é distribuído separadamente como `PTBR-Translation-BAT-v1.0.0.zip`. Veja [BAT_INSTALLER.md](BAT_INSTALLER.md) para esse tutorial.

### Arquivos

- `installer/InfiniteFusionPTBRInstaller.exe`: instalador gráfico recomendado.
- `installer-src/InfiniteFusionPtbrInstaller.cs`: código-fonte do instalador gráfico.
- `payload/files`: dados de idioma e scripts de interface traduzidos.
- `payload/patches/text_patches.json`: mapeamentos de texto da Pokédex e descrições de roupas.

## Tutorial De Restauração

1. Abra `installer/InfiniteFusionPTBRInstaller.exe`.
2. Selecione a mesma pasta do jogo.
3. Clique em `Restore Latest Backup`.
4. O backup mais recente dentro de `PTBR_BACKUPS` será restaurado.

Alternativa: se você instalou pelo pacote BAT separado, execute `installer/Restore-Latest-Backup.bat`.

Use isso se uma atualização do jogo sobrescrever arquivos, se você quiser testar outra versão ou se algo parecer errado depois da instalação.
