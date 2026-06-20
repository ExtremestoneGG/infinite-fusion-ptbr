# Installer

## English

The recommended PT-BR installer is a small native Windows `.exe`. It lets the player choose `Game.exe` directly or scan for likely Pokemon Infinite Fusion folders, creates a required backup inside the selected game folder, installs or updates the translated files, and can uninstall the translation by restoring the original pre-translation backup.

The main `.exe` release asset is a single file with the PT-BR payload embedded inside it. It does not include `.bat` files. A separate BAT/PowerShell package is available for users who prefer readable scripts or want a one-line PowerShell install.

## Installation Tutorial

1. Download the game only from the official Pokemon Infinite Fusion Discord.
2. Download `InfiniteFusionPTBRInstaller-v1.1.4.exe` from this repository's Releases page.
3. Run `InfiniteFusionPTBRInstaller-v1.1.4.exe`.
4. Click `Choose Game.exe` and select the game's `Game.exe`, or use `Scan` to search common folders automatically.
5. Confirm the install prompt.
6. Wait for the copy and patch steps to finish.
7. Open the game and select `Português` in the language menu.

The selected game folder should contain `Game.exe` and `Data`.

## After Updating The Game

If you run the official Pokemon Infinite Fusion updater `.bat`, run the PT-BR installer again before opening your save. Choose the same `Game.exe` and click `Install / Update PT-BR`.

This matters because the official update can restore original English script files. A save that was last used while the PT-BR translation was installed can crash if it is opened while those translated files are missing.

It performs four actions:

1. Validates that the selected folder contains `Data` and `Data/Scripts`.
2. Creates a required timestamped backup under `PTBR_BACKUPS` inside the selected game folder.
3. Copies the translated language/script files from `payload/files`.
4. Applies `payload/patches/text_patches.json` to text-only JSON fields for outfit descriptions, with Pokedex species/fusion mappings excluded and planned as future work.

The `.exe` installer does not download files, does not contact the internet, does not include the game, and does not require administrator permissions.

The BAT/PowerShell method is shipped separately as `PTBR-Translation-BAT-v1.1.4.zip`. See [BAT_INSTALLER.md](BAT_INSTALLER.md) for that tutorial.

### Files

- `InfiniteFusionPTBRInstaller-v1.1.4.exe`: recommended single-file graphical installer.
- `installer/InfiniteFusionPTBRInstaller.exe`: same graphical installer inside the repository source tree.
- `installer-src/ModernInstaller.cs`: source code for the modern WPF graphical installer.
- `installer-src/InfiniteFusionPtbrInstaller.cs`: shared installer core and legacy WinForms UI source.
- `payload/files`: translated language data and script-level UI files.
- `payload/patches/text_patches.json`: outfit description text mappings with Pokedex species/fusion mappings excluded from the v1.1.4 active payload.

## Restore Tutorial

1. Open `InfiniteFusionPTBRInstaller-v1.1.4.exe`.
2. Select the same game folder.
3. Click `Desinstalar tradução`.
4. The installer backs up your save files under `PTBR_BACKUPS/save_backups` and resets the save language back to English.
5. The installer prefers the original pre-translation backup in `PTBR_BACKUPS`, so the game files return to English instead of staying partially translated.
6. After uninstalling, run the official game update `.bat` once so the official updater can verify and refresh the original game files before you play.

Fallback option: if you installed through the separate BAT package, run `installer/Restore-Latest-Backup.bat`.

Use this if the game update overwrites files, if you want to test another version, or if anything feels wrong after installation.

## Known Issue: Save Crash After Official Update

Known crash message:

`Script 'MultiSaves.rb' line 786: NoMethodError occurred. undefined method '[]' for nil:NilClass`

Workaround:

1. Close the game.
2. Open `InfiniteFusionPTBRInstaller-v1.1.4.exe`.
3. Select the same `Game.exe`.
4. Click `Install / Update PT-BR`.
5. Open the game again.

For extra safety before official game updates, manually copy `%APPDATA%\infinitefusion` somewhere safe. In the graphical uninstall flow, v1.1.4 automatically backs up save files and resets the saved language to English.

## Português

O instalador PT-BR recomendado é um pequeno `.exe` nativo para Windows. Ele permite escolher o `Game.exe` diretamente ou escanear possíveis pastas do Pokemon Infinite Fusion, cria backup obrigatório dentro da pasta escolhida do jogo, instala ou atualiza os arquivos traduzidos e consegue desinstalar a tradução restaurando o backup original anterior à tradução.

O arquivo principal `.exe` da release é um arquivo único com o pacote PT-BR embutido dentro dele. Ele não inclui arquivos `.bat`. Um pacote separado em BAT/PowerShell está disponível para quem prefere scripts legíveis ou quer instalar por uma linha no PowerShell.

## Tutorial De Instalação

1. Baixe o jogo somente pelo Discord oficial de Pokemon Infinite Fusion.
2. Baixe `InfiniteFusionPTBRInstaller-v1.1.4.exe` pela página de Releases deste repositório.
3. Execute `InfiniteFusionPTBRInstaller-v1.1.4.exe`.
4. Clique em `Escolher Game.exe` e selecione o `Game.exe` do jogo, ou use `Escanear` para procurar pastas comuns automaticamente.
5. Confirme a janela de instalação.
6. Aguarde a cópia e os patches terminarem.
7. Abra o jogo e selecione `Português` no menu de idioma.

A pasta escolhida deve conter `Game.exe` e `Data`.

## Depois De Atualizar O Jogo

Se você rodar o updater oficial `.bat` do Pokemon Infinite Fusion, rode o instalador PT-BR de novo antes de abrir seu save. Escolha o mesmo `Game.exe` e clique em `Instalar / Atualizar PT-BR`.

Isso importa porque o update oficial pode restaurar scripts originais em inglês. Um save usado pela última vez com a tradução PT-BR instalada pode crashar se for aberto quando esses arquivos traduzidos estiverem ausentes.

Ele faz quatro coisas:

1. Verifica se a pasta escolhida contém `Data` e `Data/Scripts`.
2. Cria um backup obrigatório com data e hora dentro de `PTBR_BACKUPS`, na pasta escolhida do jogo.
3. Copia os arquivos traduzidos de idioma/scripts a partir de `payload/files`.
4. Aplica `payload/patches/text_patches.json` apenas em campos de texto dos JSONs para descrições de roupas, com descrições da Pokédex documentadas como trabalho futuro.

O instalador `.exe` não baixa arquivos, não acessa a internet, não inclui o jogo e não precisa de permissões de administrador.

O método BAT/PowerShell é distribuído separadamente como `PTBR-Translation-BAT-v1.1.4.zip`. Veja [BAT_INSTALLER.md](BAT_INSTALLER.md) para esse tutorial.

### Arquivos

- `InfiniteFusionPTBRInstaller-v1.1.4.exe`: instalador gráfico recomendado em arquivo único.
- `installer/InfiniteFusionPTBRInstaller.exe`: o mesmo instalador gráfico dentro da árvore de código do repositório.
- `installer-src/ModernInstaller.cs`: código-fonte do instalador gráfico WPF moderno.
- `installer-src/InfiniteFusionPtbrInstaller.cs`: núcleo compartilhado do instalador e código da interface WinForms legada.
- `payload/files`: dados de idioma e scripts de interface traduzidos.
- `payload/patches/text_patches.json`: mapeamentos de descrições de roupas. As descrições da Pokédex não contam como concluídas na v1.1.4.

## Tutorial De Restauração

1. Abra `InfiniteFusionPTBRInstaller-v1.1.4.exe`.
2. Selecione a mesma pasta do jogo.
3. Clique em `Desinstalar tradução`.
4. O instalador cria backup dos saves em `PTBR_BACKUPS/save_backups` e volta o idioma salvo para inglês.
5. O instalador prefere o backup original anterior à tradução dentro de `PTBR_BACKUPS`, então os arquivos do jogo voltam para inglês em vez de ficarem parcialmente traduzidos.
6. Depois de desinstalar, rode uma vez o `.bat` oficial de atualização do jogo para o updater oficial verificar e refrescar os arquivos originais antes de jogar.

Alternativa: se você instalou pelo pacote BAT separado, execute `installer/Restore-Latest-Backup.bat`.

Use isso se uma atualização do jogo sobrescrever arquivos, se você quiser testar outra versão ou se algo parecer errado depois da instalação.

## Erro Conhecido: Crash De Save Depois De Update Oficial

Mensagem conhecida do crash:

`Script 'MultiSaves.rb' line 786: NoMethodError occurred. undefined method '[]' for nil:NilClass`

Como resolver:

1. Feche o jogo.
2. Abra `InfiniteFusionPTBRInstaller-v1.1.4.exe`.
3. Selecione o mesmo `Game.exe`.
4. Clique em `Instalar / Atualizar PT-BR`.
5. Abra o jogo novamente.

Por segurança, antes de updates oficiais do jogo, copie manualmente `%APPDATA%\infinitefusion` para outro lugar. Na desinstalação pelo `.exe`, a v1.1.4 cria backup dos saves e volta o idioma salvo para inglês automaticamente.
