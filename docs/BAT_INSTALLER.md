# BAT / PowerShell Installation Method

## English

The recommended download is the single-file graphical `.exe`. The `.bat` method is provided separately for people who prefer transparent scripts or want to inspect every command.

The BAT package does not include the graphical `.exe` installer. The main `.exe` download does not include `.bat` files.

Download the BAT package from the Releases page:

`PTBR-Translation-BAT-v1.1.5.zip`

### Manual BAT Tutorial

1. Download `PTBR-Translation-BAT-v1.1.5.zip` from the Releases page.
2. Extract the ZIP into a folder you trust.
3. Open the extracted folder.
4. Run `installer/Install-PTBR.bat`.
5. Select your Pokemon Infinite Fusion game folder.
6. The script creates a required backup inside the selected game folder.
7. Wait for the install to finish.
8. Open the game.
9. For a new save, choose `Português` when the game asks for a language.
10. For an existing English save, open the title/load screen, choose `Idioma`, and select `Português`.

To restore, run `installer/Restore-Latest-Backup.bat` and select the same game folder. For the safest uninstall flow, use the graphical `.exe` and click `Desinstalar tradução`, because the `.exe` also backs up saves and resets the saved language to English.

### After Official Game Updates

If you update Pokemon Infinite Fusion with the official updater `.bat`, run the PT-BR installer again before opening your save. For the BAT method, run `installer/Install-PTBR.bat` again from the extracted package or use the one-line PowerShell command below from the game folder.

Known crash if the translation is missing after an update:

`Script 'MultiSaves.rb' line 786: NoMethodError occurred. undefined method '[]' for nil:NilClass`

If this happens, close the game and reinstall/update the PT-BR translation. For extra safety before official updates, manually copy `%APPDATA%\infinitefusion`.

### One-Line PowerShell Install

Advanced users can open PowerShell inside the game folder and run:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -Command "irm https://raw.githubusercontent.com/ExtremestoneGG/infinite-fusion-ptbr/main/scripts/install-from-github.ps1 | iex"
```

This downloads the BAT/PowerShell package from this GitHub release, installs it into the current folder, and creates a required backup. It does not download the game.

Only run this command if the current folder is your Pokemon Infinite Fusion folder. The folder should contain `Game.exe` and `Data`.

## Português

O download recomendado é o instalador gráfico `.exe` em arquivo único. O método `.bat` é oferecido separado para quem prefere scripts transparentes ou quer inspecionar cada comando.

O pacote BAT não inclui o instalador gráfico `.exe`. O download principal `.exe` não inclui arquivos `.bat`.

Baixe o pacote BAT pela página de Releases:

`PTBR-Translation-BAT-v1.1.5.zip`

### Tutorial Manual Pelo BAT

1. Baixe `PTBR-Translation-BAT-v1.1.5.zip` pela página de Releases.
2. Extraia o ZIP em uma pasta confiável.
3. Abra a pasta extraída.
4. Execute `installer/Install-PTBR.bat`.
5. Selecione a pasta do Pokemon Infinite Fusion.
6. O script cria um backup obrigatório dentro da pasta escolhida do jogo.
7. Aguarde a instalação terminar.
8. Abra o jogo.
9. Em um save novo, escolha `Português` quando o jogo pedir o idioma.
10. Em um save existente em inglês, abra a tela inicial/de carregamento, escolha `Idioma` e selecione `Português`.

Para restaurar, execute `installer/Restore-Latest-Backup.bat` e selecione a mesma pasta do jogo. Para a desinstalação mais segura, use o `.exe` gráfico e clique em `Desinstalar tradução`, porque o `.exe` também cria backup dos saves e volta o idioma salvo para inglês.

### Depois De Updates Oficiais

Se você atualizar Pokemon Infinite Fusion pelo updater oficial `.bat`, rode o instalador PT-BR de novo antes de abrir seu save. No método BAT, execute `installer/Install-PTBR.bat` de novo a partir do pacote extraído ou use o comando PowerShell abaixo dentro da pasta do jogo.

Crash conhecido quando a tradução está ausente depois de um update:

`Script 'MultiSaves.rb' line 786: NoMethodError occurred. undefined method '[]' for nil:NilClass`

Se isso acontecer, feche o jogo e reinstale/atualize a tradução PT-BR. Por segurança, antes de updates oficiais, copie manualmente `%APPDATA%\infinitefusion`.

### Instalação Por Uma Linha No PowerShell

Usuários avançados podem abrir o PowerShell dentro da pasta do jogo e rodar:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -Command "irm https://raw.githubusercontent.com/ExtremestoneGG/infinite-fusion-ptbr/main/scripts/install-from-github.ps1 | iex"
```

Isso baixa o pacote BAT/PowerShell desta release no GitHub, instala na pasta atual e cria backup obrigatório. Ele não baixa o jogo.

Rode esse comando somente se a pasta atual for a pasta do Pokemon Infinite Fusion. A pasta deve conter `Game.exe` e `Data`.
