# Pokemon Infinite Fusion PT-BR Fan Translation

![Pokemon Infinite Fusion PT-BR Mod](assets/infinite-fusion-ptbr-mod-logo-github-bg.png)

**Version:** 1.0.0  
**Language:** Brazilian Portuguese  
**Status:** fan-made release candidate for public testing and feedback
**Repository:** https://github.com/ExtremestoneGG/infinite-fusion-ptbr  
**Downloads:** https://github.com/ExtremestoneGG/infinite-fusion-ptbr/releases

> **Important:** this is an unofficial, fan-made translation project. It is not affiliated with, endorsed by, or presented as official by the Pokemon Infinite Fusion developers, Nintendo, Game Freak, The Pokemon Company, or any related rights holder.

> **Safety notice:** this repository does **not** include the game. Download Pokemon Infinite Fusion only from the official Pokemon Infinite Fusion Discord maintained by the game developers. Do not download the game from random websites, mirrors, reuploads, or "easy installer" pages, because fake downloads and malware have been reported in the community.

## What This Is

This is a Brazilian Portuguese fan translation patch for **Pokemon Infinite Fusion**. The goal is to make the game's story, quests, dialogue, menus, descriptions, and gameplay text comfortable for Brazilian players while preserving the names that Brazilian Pokemon fans are already used to reading in English.

The project was coordinated by a designer and AI enthusiast, not by a professional programmer. AI was used as a tool to help extract text, maintain consistency, draft translations, and run validation passes. The project is open to feedback, corrections, human review, and improvements from the community.

This is made **by fans, for fans**. It does not try to replace the official project or pretend to be official. If the Pokemon Infinite Fusion team ever finds this useful as a reference for an official localization workflow, that would be amazing, but the current release should be treated as a community testing build.

## What Was Translated

The translation focuses on readable Portuguese while preserving the Pokemon vocabulary most Brazilian players already use:

| Translated | Preserved in English |
| --- | --- |
| Story dialogue | Pokemon names |
| Quest text | Move names |
| NPC dialogue, including shops and services | Item names |
| Gameplay menus and UI text | Ability names |
| Pokedex descriptions | NPC names |
| Item descriptions | Location names |
| Move descriptions | Organization names |
| Ability descriptions/effects | Type/element names |
| Nature names | Drawn or bitmap text |
| Outfit, hat, and hairstyle descriptions | Original credits and sprite author names |

Example: an item name like **Pokeball** or **Rare Candy** remains in English, but the explanation of what it does can be translated into Portuguese.

## What Changed

This release contains:

- `Data/portuguese.dat`, a new Brazilian Portuguese language data file.
- 53 script files with translated gameplay UI strings and language support adjustments.
- A text patch for Pokedex entries and outfit descriptions that changes only text fields.
- A graphical Windows `.exe` installer with folder selection, backup, install, validation, and restore options.
- A separate BAT/PowerShell package for users who prefer transparent scripts.
- Validation reports documenting what was checked before release.

This release does **not** include:

- The game executable.
- Full game downloads.
- Sprites, music, graphics packs, or updater downloads.
- Random files from a local game installation.
- The unofficial sprite cache or downloaded sprite updates.

## Installation

### Quick Tutorial

1. Download and install Pokemon Infinite Fusion from the **official Pokemon Infinite Fusion Discord only**.
2. Open the [Releases page](https://github.com/ExtremestoneGG/infinite-fusion-ptbr/releases).
3. Download `PTBR-Translation-v1.0.0.zip`, the recommended package with the graphical `.exe` installer. Do not use this project as a game download.
4. Extract the ZIP into any folder you trust, for example your Downloads folder or a folder on your desktop.
5. Open the extracted folder.
6. Run `installer/InfiniteFusionPTBRInstaller.exe`.
7. When the folder picker opens, select the main Pokemon Infinite Fusion folder, the one that contains `Game.exe` and the `Data` folder.
8. Confirm the install message. Backup is enabled by default and should stay enabled.
9. Wait while the installer copies the translated files and applies the Pokedex/outfit text patches. Older machines may take a few minutes.
10. Start the game.
11. Open the language menu and select `Português`.

To undo the patch, open `installer/InfiniteFusionPTBRInstaller.exe` again and click `Restore Latest Backup`.

If Windows SmartScreen warns about the file, it is because this is a small unsigned fan tool. The installer source code is included in `installer-src/InfiniteFusionPtbrInstaller.cs`.

If you prefer the script-based method, download the separate `PTBR-Translation-BAT-v1.0.0.zip` package and follow [docs/BAT_INSTALLER.md](docs/BAT_INSTALLER.md).

### How The Installer Works

The recommended installer is `installer/InfiniteFusionPTBRInstaller.exe`. It is a small Windows Forms app that uses the files inside the extracted `PTBR-Translation-v1.0.0.zip` package.

The `.bat` installer is not included inside the recommended `.exe` ZIP. It is shipped separately as `PTBR-Translation-BAT-v1.0.0.zip`, which includes `installer/Install-PTBR.bat`, `installer/Install-PTBR.ps1`, and `installer/Restore-Latest-Backup.bat`.

The installer:

- Checks if the selected folder looks like Pokemon Infinite Fusion.
- Creates a backup in `PTBR_BACKUPS`.
- Copies the Portuguese language file and translated script files.
- Applies text-only patches to Pokedex entries and outfit descriptions.
- Keeps the game's executable, sprites, music, saves, and unrelated files untouched.

### Restore Tutorial

1. Open `installer/InfiniteFusionPTBRInstaller.exe`.
2. Select the same Pokemon Infinite Fusion folder used during installation.
3. Click `Restore Latest Backup`.
4. The installer restores the newest backup from `PTBR_BACKUPS`.
5. Files created by the translation installer are removed, and overwritten files are restored.

Fallback option: if you installed through the BAT package, run `installer/Restore-Latest-Backup.bat` and select the same game folder.

If you want to be extra careful, copy your save folder before testing any mod or fan patch.

## Installer Notes

The installer is intentionally simple and transparent:

- The recommended package uses a small unsigned `.exe`, and its source code is included.
- The separate BAT package uses readable PowerShell scripts.
- It does not need administrator access.
- The downloaded installer packages do not connect to the internet.
- It does not download the game.
- It validates that the selected folder looks like a Pokemon Infinite Fusion installation.
- It creates a timestamped backup in `PTBR_BACKUPS` before changing files.
- It can restore the latest backup.

See [docs/INSTALLER.md](docs/INSTALLER.md) for the `.exe` method and [docs/BAT_INSTALLER.md](docs/BAT_INSTALLER.md) for the BAT/PowerShell method.

### One-Line PowerShell Method

Advanced users can also open PowerShell inside the Pokemon Infinite Fusion folder and run this command:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -Command "irm https://raw.githubusercontent.com/ExtremestoneGG/infinite-fusion-ptbr/main/scripts/install-from-github.ps1 | iex"
```

That command downloads the script package from this GitHub release, creates a backup by default, installs the PT-BR files, and cleans up its temporary installer folder. Only run it from the folder that contains `Game.exe` and `Data`.

## Validation Summary

Release 1.0.0 was checked with a final validation sweep:

- Base translated language entries: `20,577`
- Fusion Pokedex translation mappings: `61,726`
- Outfit description mappings: `314`
- Pokedex JSON files loaded successfully.
- Fusion Pokedex translation errors: `0`
- Active Pokedex English sentence flags after final polish: `0`
- Active `Pokébola`/`Pokebola` flags after final polish: `0`
- Outfit names were preserved; only descriptions were translated.

The full machine-readable validation report is available at [docs/reports/validation_report.json](docs/reports/validation_report.json).

## Feedback

This first public release is meant to be tested. Reports are welcome for:

- Awkward context or tone.
- Dialogue that feels out of order.
- UI text that clips or does not fit.
- Any untranslated dialogue that is not intentionally preserved.
- Any Pokemon, move, item, ability, NPC, or location name that was accidentally translated.
- Bugs caused by installation or restore.

Please include screenshots, the map/location, and what language option was active when reporting issues.

## Credits And Respect

All credit for Pokemon Infinite Fusion belongs to its creators and contributors. This repository is only a fan translation patch and exists because Brazilian players love the game and want to enjoy its story in Portuguese.

If you are part of the Pokemon Infinite Fusion team and want this project changed, removed, restructured, or adapted into a cleaner localization format, please reach out through the repository issues.

---

# Tradução PT-BR Fanmade de Pokemon Infinite Fusion

**Versão:** 1.0.0  
**Idioma:** Português do Brasil  
**Status:** versão fanmade para testes públicos e feedback
**Repositório:** https://github.com/ExtremestoneGG/infinite-fusion-ptbr  
**Downloads:** https://github.com/ExtremestoneGG/infinite-fusion-ptbr/releases

> **Importante:** este é um projeto de tradução não oficial, feito por fã. Ele não é afiliado, aprovado, endossado nem apresentado como oficial pelos desenvolvedores de Pokemon Infinite Fusion, Nintendo, Game Freak, The Pokemon Company ou qualquer detentor relacionado.

> **Aviso de segurança:** este repositório **não** inclui o jogo. Baixe Pokemon Infinite Fusion somente pelo Discord oficial de Pokemon Infinite Fusion mantido pelos desenvolvedores. Não baixe o jogo por sites aleatórios, mirrors, reuploads ou páginas prometendo instalador fácil, porque downloads falsos e malware já foram relatados pela comunidade.

## O Que É Este Projeto

Esta é uma tradução fanmade para Português do Brasil de **Pokemon Infinite Fusion**. O objetivo é deixar história, quests, diálogos, menus, descrições e textos de gameplay confortáveis para jogadores brasileiros, mantendo em inglês os nomes que a comunidade BR de Pokemon já está acostumada a usar.

O projeto foi coordenado por um designer e entusiasta de IA, não por um programador profissional. IA foi usada como ferramenta para ajudar na extração de textos, consistência, rascunhos de tradução e validações. O projeto está aberto a feedback, correções, revisão humana e melhorias da comunidade.

Isto foi feito **de fã para fã**. A tradução não tenta substituir o projeto oficial nem fingir que é oficial. Se um dia a equipe de Pokemon Infinite Fusion achar este trabalho útil como referência para uma localização oficial, seria incrível, mas esta versão deve ser tratada como uma build comunitária de testes.

## O Que Foi Traduzido

A tradução tenta soar natural em português sem mexer no vocabulário de Pokemon que jogadores brasileiros já costumam usar em inglês:

| Traduzido | Mantido em inglês |
| --- | --- |
| Diálogos da história | Nomes de Pokemon |
| Textos de quests | Nomes de golpes |
| Falas de NPCs, incluindo lojas e serviços | Nomes de itens |
| Menus e textos de interface | Nomes de habilidades |
| Descrições da Pokédex | Nomes de NPCs |
| Descrições de itens | Nomes de lugares |
| Descrições de golpes | Nomes de organizações |
| Descrições/efeitos de habilidades | Tipos/elementos |
| Nomes das Natures | Textos desenhados/bitmap |
| Descrições de roupas, chapéus e cabelos | Créditos e autores de sprites |

Exemplo: um item como **Pokeball** ou **Rare Candy** continua com o nome em inglês, mas a explicação do que ele faz pode aparecer em português.

## O Que Foi Alterado

Esta release contém:

- `Data/portuguese.dat`, um novo arquivo de idioma em Português do Brasil.
- 53 arquivos de script com textos de interface traduzidos e ajustes de suporte ao idioma.
- Um patch de texto para entradas da Pokédex e descrições de roupas, alterando apenas campos de texto.
- Um instalador gráfico `.exe` para Windows com seleção de pasta, backup, instalação, validação e restauração.
- Um pacote separado em BAT/PowerShell para quem prefere scripts transparentes.
- Relatórios de validação documentando o que foi checado antes da release.

Esta release **não** contém:

- Executável do jogo.
- Download completo do jogo.
- Sprites, músicas, pacotes gráficos ou updater.
- Arquivos aleatórios de uma instalação local.
- Cache de sprites ou sprites baixados por atualizador.

## Instalação

### Tutorial Rápido

1. Baixe e instale Pokemon Infinite Fusion **somente pelo Discord oficial de Pokemon Infinite Fusion**.
2. Abra a [página de Releases](https://github.com/ExtremestoneGG/infinite-fusion-ptbr/releases).
3. Baixe `PTBR-Translation-v1.0.0.zip`, o pacote recomendado com instalador gráfico `.exe`. Não use este projeto como download do jogo.
4. Extraia o ZIP em uma pasta confiável, como Downloads ou uma pasta na área de trabalho.
5. Abra a pasta extraída.
6. Execute `installer/InfiniteFusionPTBRInstaller.exe`.
7. Quando a janela de seleção abrir, escolha a pasta principal do Pokemon Infinite Fusion, aquela que contém `Game.exe` e a pasta `Data`.
8. Confirme a mensagem de instalação. O backup fica ativado por padrão e é recomendado deixar assim.
9. Aguarde enquanto o instalador copia os arquivos traduzidos e aplica os patches de texto da Pokédex/roupas. Em computadores mais antigos isso pode levar alguns minutos.
10. Abra o jogo.
11. Entre no menu de idioma e selecione `Português`.

Para desfazer a instalação, abra `installer/InfiniteFusionPTBRInstaller.exe` de novo e clique em `Restore Latest Backup`.

Se o Windows SmartScreen avisar sobre o arquivo, é porque ele é uma ferramenta pequena de fã e não possui assinatura digital. O código-fonte do instalador está incluído em `installer-src/InfiniteFusionPtbrInstaller.cs`.

Se você preferir o método por script, baixe o pacote separado `PTBR-Translation-BAT-v1.0.0.zip` e siga [docs/BAT_INSTALLER.md](docs/BAT_INSTALLER.md).

### Como O Instalador Funciona

O instalador recomendado é `installer/InfiniteFusionPTBRInstaller.exe`. Ele é um pequeno app Windows Forms que usa os arquivos extraídos do pacote `PTBR-Translation-v1.0.0.zip`.

O instalador `.bat` não fica dentro do ZIP recomendado com `.exe`. Ele é distribuído separadamente como `PTBR-Translation-BAT-v1.0.0.zip`, que inclui `installer/Install-PTBR.bat`, `installer/Install-PTBR.ps1` e `installer/Restore-Latest-Backup.bat`.

O instalador:

- Verifica se a pasta escolhida parece ser o Pokemon Infinite Fusion.
- Cria um backup em `PTBR_BACKUPS`.
- Copia o arquivo de idioma português e os scripts traduzidos.
- Aplica patches somente de texto nas entradas da Pokédex e descrições de roupas.
- Mantém executável, sprites, músicas, saves e arquivos não relacionados do jogo sem mexer.

### Tutorial De Restauração

1. Abra `installer/InfiniteFusionPTBRInstaller.exe`.
2. Selecione a mesma pasta do Pokemon Infinite Fusion usada na instalação.
3. Clique em `Restore Latest Backup`.
4. O instalador restaura o backup mais recente dentro de `PTBR_BACKUPS`.
5. Arquivos criados pelo instalador da tradução são removidos, e arquivos sobrescritos são restaurados.

Alternativa: se você instalou pelo pacote BAT, execute `installer/Restore-Latest-Backup.bat` e selecione a mesma pasta do jogo.

Se quiser ter cuidado extra, copie sua pasta de saves antes de testar qualquer mod ou patch fanmade.

## Sobre O Instalador

O instalador foi feito para ser simples e transparente:

- O pacote recomendado usa um `.exe` pequeno e sem assinatura digital, com código-fonte incluído.
- O pacote BAT separado usa scripts PowerShell legíveis.
- Não precisa de administrador.
- Os pacotes baixados não acessam a internet.
- Não baixa o jogo.
- Verifica se a pasta selecionada parece uma instalação do Pokemon Infinite Fusion.
- Cria um backup com data e hora em `PTBR_BACKUPS` antes de alterar arquivos.
- Consegue restaurar o backup mais recente.

Veja [docs/INSTALLER.md](docs/INSTALLER.md) para o método `.exe` e [docs/BAT_INSTALLER.md](docs/BAT_INSTALLER.md) para o método BAT/PowerShell.

### Método PowerShell Em Uma Linha

Usuários avançados também podem abrir o PowerShell dentro da pasta do Pokemon Infinite Fusion e rodar:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -Command "irm https://raw.githubusercontent.com/ExtremestoneGG/infinite-fusion-ptbr/main/scripts/install-from-github.ps1 | iex"
```

Esse comando baixa o pacote de scripts desta release no GitHub, cria backup por padrão, instala os arquivos PT-BR e limpa a pasta temporária do instalador. Rode somente dentro da pasta que contém `Game.exe` e `Data`.

## Resumo Da Validação

A versão 1.0.0 passou por uma varredura final:

- Entradas traduzidas do arquivo base de idioma: `20.577`
- Mapeamentos de tradução da Pokédex de fusões: `61.726`
- Mapeamentos de descrições de roupas/chapéus/cabelos: `314`
- Arquivos JSON da Pokédex carregaram corretamente.
- Erros de tradução da Pokédex de fusões: `0`
- Frases em inglês detectadas na Pokédex ativa após polimento final: `0`
- Ocorrências de `Pokébola`/`Pokebola` na Pokédex ativa após polimento final: `0`
- Nomes de roupas foram preservados; apenas descrições foram traduzidas.

O relatório completo em formato de máquina está em [docs/reports/validation_report.json](docs/reports/validation_report.json).

## Feedback

Esta primeira release pública foi feita para ser testada. Feedbacks são bem-vindos sobre:

- Contexto ou tom estranho.
- Diálogos que pareçam fora de ordem.
- Textos de interface cortados ou apertados.
- Qualquer fala não traduzida que não faça parte das exceções.
- Qualquer nome de Pokemon, golpe, item, habilidade, NPC ou local que tenha sido traduzido por acidente.
- Bugs na instalação ou restauração.

Ao reportar, mande prints, o local/mapa, e qual idioma estava selecionado no jogo.

## Créditos E Respeito

Todo o crédito por Pokemon Infinite Fusion pertence aos criadores e contribuidores do jogo. Este repositório é apenas um patch de tradução fanmade e existe porque jogadores brasileiros amam o jogo e querem aproveitar a história em português.

Se você faz parte da equipe de Pokemon Infinite Fusion e quer que este projeto seja alterado, removido, reestruturado ou adaptado para um formato melhor de localização, abra uma issue no repositório.
