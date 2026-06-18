# Installer

## English

The recommended PT-BR installer is a small graphical Windows `.exe`. It asks for the Pokemon Infinite Fusion folder, creates a required backup inside the selected game folder, installs the translated files, and can restore the latest backup if the player runs it again.

The main `.exe` release asset is a single file with the PT-BR payload embedded inside it. It does not include `.bat` files. A separate BAT/PowerShell package is available for users who prefer readable scripts or want a one-line PowerShell install.

## Installation Tutorial

1. Download the game only from the official Pokemon Infinite Fusion Discord.
2. Download `InfiniteFusionPTBRInstaller-v1.0.0.exe` from this repository's Releases page.
3. Run `InfiniteFusionPTBRInstaller-v1.0.0.exe`.
4. Select the main Pokemon Infinite Fusion folder.
5. Confirm the install prompt.
6. Wait for the copy and patch steps to finish.
7. Open the game and select `Português` in the language menu.

The selected game folder should contain `Game.exe` and `Data`.

It performs four actions:

1. Validates that the selected folder contains `Data` and `Data/Scripts`.
2. Creates a required timestamped backup under `PTBR_BACKUPS` inside the selected game folder.
3. Copies the translated language/script files from `payload/files`.
4. Applies `payload/patches/text_patches.json` to JSON text fields only.

The `.exe` installer does not download files, does not contact the internet, does not include the game, and does not require administrator permissions.

The BAT/PowerShell method is shipped separately as `PTBR-Translation-BAT-v1.0.0.zip`. See [BAT_INSTALLER.md](BAT_INSTALLER.md) for that tutorial.

### Files

- `InfiniteFusionPTBRInstaller-v1.0.0.exe`: recommended single-file graphical installer.
- `installer/InfiniteFusionPTBRInstaller.exe`: same graphical installer inside the repository source tree.
- `installer-src/InfiniteFusionPtbrInstaller.cs`: source code for the graphical installer.
- `payload/files`: translated language data and script-level UI files.
- `payload/patches/text_patches.json`: Pokedex and outfit description text mappings.

## Restore Tutorial

1. Open `InfiniteFusionPTBRInstaller-v1.0.0.exe`.
2. Select the same game folder.
3. Click `Restore Latest Backup`.
4. The newest backup in `PTBR_BACKUPS` is restored.

Fallback option: if you installed through the separate BAT package, run `installer/Restore-Latest-Backup.bat`.

Use this if the game update overwrites files, if you want to test another version, or if anything feels wrong after installation.

## Português

O instalador PT-BR recomendado é um pequeno `.exe` gráfico para Windows. Ele pede a pasta do Pokemon Infinite Fusion, cria backup obrigatório dentro da pasta escolhida do jogo, instala os arquivos traduzidos e consegue restaurar o backup mais recente se a pessoa executar de novo.

O arquivo principal `.exe` da release é um arquivo único com o pacote PT-BR embutido dentro dele. Ele não inclui arquivos `.bat`. Um pacote separado em BAT/PowerShell está disponível para quem prefere scripts legíveis ou quer instalar por uma linha no PowerShell.

## Tutorial De Instalação

1. Baixe o jogo somente pelo Discord oficial de Pokemon Infinite Fusion.
2. Baixe `InfiniteFusionPTBRInstaller-v1.0.0.exe` pela página de Releases deste repositório.
3. Execute `InfiniteFusionPTBRInstaller-v1.0.0.exe`.
4. Selecione a pasta principal do Pokemon Infinite Fusion.
5. Confirme a janela de instalação.
6. Aguarde a cópia e os patches terminarem.
7. Abra o jogo e selecione `Português` no menu de idioma.

A pasta escolhida deve conter `Game.exe` e `Data`.

Ele faz quatro coisas:

1. Verifica se a pasta escolhida contém `Data` e `Data/Scripts`.
2. Cria um backup obrigatório com data e hora dentro de `PTBR_BACKUPS`, na pasta escolhida do jogo.
3. Copia os arquivos traduzidos de idioma/scripts a partir de `payload/files`.
4. Aplica `payload/patches/text_patches.json` apenas em campos de texto dos JSONs.

O instalador `.exe` não baixa arquivos, não acessa a internet, não inclui o jogo e não precisa de permissões de administrador.

O método BAT/PowerShell é distribuído separadamente como `PTBR-Translation-BAT-v1.0.0.zip`. Veja [BAT_INSTALLER.md](BAT_INSTALLER.md) para esse tutorial.

### Arquivos

- `InfiniteFusionPTBRInstaller-v1.0.0.exe`: instalador gráfico recomendado em arquivo único.
- `installer/InfiniteFusionPTBRInstaller.exe`: o mesmo instalador gráfico dentro da árvore de código do repositório.
- `installer-src/InfiniteFusionPtbrInstaller.cs`: código-fonte do instalador gráfico.
- `payload/files`: dados de idioma e scripts de interface traduzidos.
- `payload/patches/text_patches.json`: mapeamentos de texto da Pokédex e descrições de roupas.

## Tutorial De Restauração

1. Abra `InfiniteFusionPTBRInstaller-v1.0.0.exe`.
2. Selecione a mesma pasta do jogo.
3. Clique em `Restore Latest Backup`.
4. O backup mais recente dentro de `PTBR_BACKUPS` será restaurado.

Alternativa: se você instalou pelo pacote BAT separado, execute `installer/Restore-Latest-Backup.bat`.

Use isso se uma atualização do jogo sobrescrever arquivos, se você quiser testar outra versão ou se algo parecer errado depois da instalação.
