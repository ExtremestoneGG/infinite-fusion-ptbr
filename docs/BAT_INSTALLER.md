# BAT / PowerShell Installation Method

## English

The recommended download is the single-file graphical `.exe`. The `.bat` method is provided separately for people who prefer transparent scripts or want to inspect every command.

The BAT package does not include the graphical `.exe` installer. The main `.exe` download does not include `.bat` files.

Download the BAT package from the Releases page:

`PTBR-Translation-BAT-v1.0.0.zip`

### Manual BAT Tutorial

1. Download `PTBR-Translation-BAT-v1.0.0.zip` from the Releases page.
2. Extract the ZIP into a folder you trust.
3. Open the extracted folder.
4. Run `installer/Install-PTBR.bat`.
5. Select your Pokemon Infinite Fusion game folder.
6. The script creates a required backup inside the selected game folder.
7. Wait for the install to finish.
8. Open the game and select `Português`.

To restore, run `installer/Restore-Latest-Backup.bat` and select the same game folder.

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

`PTBR-Translation-BAT-v1.0.0.zip`

### Tutorial Manual Pelo BAT

1. Baixe `PTBR-Translation-BAT-v1.0.0.zip` pela página de Releases.
2. Extraia o ZIP em uma pasta confiável.
3. Abra a pasta extraída.
4. Execute `installer/Install-PTBR.bat`.
5. Selecione a pasta do Pokemon Infinite Fusion.
6. O script cria um backup obrigatório dentro da pasta escolhida do jogo.
7. Aguarde a instalação terminar.
8. Abra o jogo e selecione `Português`.

Para restaurar, execute `installer/Restore-Latest-Backup.bat` e selecione a mesma pasta do jogo.

### Instalação Por Uma Linha No PowerShell

Usuários avançados podem abrir o PowerShell dentro da pasta do jogo e rodar:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -Command "irm https://raw.githubusercontent.com/ExtremestoneGG/infinite-fusion-ptbr/main/scripts/install-from-github.ps1 | iex"
```

Isso baixa o pacote BAT/PowerShell desta release no GitHub, instala na pasta atual e cria backup obrigatório. Ele não baixa o jogo.

Rode esse comando somente se a pasta atual for a pasta do Pokemon Infinite Fusion. A pasta deve conter `Game.exe` e `Data`.
