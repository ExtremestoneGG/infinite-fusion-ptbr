# Pokemon Infinite Fusion PT-BR Fan Translation

<!-- Banner placeholder: add your project image here later, for example: -->
<!-- ![Pokemon Infinite Fusion PT-BR Fan Translation](assets/banner.png) -->

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
- A small PowerShell installer with backup and restore support.
- Validation reports documenting what was checked before release.

This release does **not** include:

- The game executable.
- Full game downloads.
- Sprites, music, graphics packs, or updater downloads.
- Random files from a local game installation.
- The unofficial sprite cache or downloaded sprite updates.

## Installation

1. Download and install Pokemon Infinite Fusion from the **official Pokemon Infinite Fusion Discord only**.
2. Download this translation release from GitHub.
3. Extract the release ZIP anywhere you trust.
4. Run `installer/Install-PTBR.bat`.
5. Select your Pokemon Infinite Fusion folder.
6. Leave backup enabled unless you know exactly what you are doing.
7. Start the game and select `Português` in the language menu.

To undo the patch, run `installer/Restore-Latest-Backup.bat` and select the same game folder.

## Installer Notes

The installer is intentionally simple and transparent:

- It is a plain PowerShell script, not a compiled executable.
- It does not need administrator access.
- It does not connect to the internet.
- It does not download the game.
- It validates that the selected folder looks like a Pokemon Infinite Fusion installation.
- It creates a timestamped backup in `PTBR_BACKUPS` before changing files.
- It can restore the latest backup.

See [docs/INSTALLER.md](docs/INSTALLER.md) for details.

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
- Um instalador simples em PowerShell com backup e restauração.
- Relatórios de validação documentando o que foi checado antes da release.

Esta release **não** contém:

- Executável do jogo.
- Download completo do jogo.
- Sprites, músicas, pacotes gráficos ou updater.
- Arquivos aleatórios de uma instalação local.
- Cache de sprites ou sprites baixados por atualizador.

## Instalação

1. Baixe e instale Pokemon Infinite Fusion **somente pelo Discord oficial de Pokemon Infinite Fusion**.
2. Baixe a release desta tradução pelo GitHub.
3. Extraia o ZIP da tradução em uma pasta confiável.
4. Execute `installer/Install-PTBR.bat`.
5. Selecione a pasta do Pokemon Infinite Fusion.
6. Deixe o backup ativado, a menos que você saiba exatamente o que está fazendo.
7. Abra o jogo e selecione `Português` no menu de idioma.

Para desfazer a instalação, execute `installer/Restore-Latest-Backup.bat` e selecione a mesma pasta do jogo.

## Sobre O Instalador

O instalador foi feito para ser simples e transparente:

- É um script PowerShell aberto, não um `.exe` compilado.
- Não precisa de administrador.
- Não acessa a internet.
- Não baixa o jogo.
- Verifica se a pasta selecionada parece uma instalação do Pokemon Infinite Fusion.
- Cria um backup com data e hora em `PTBR_BACKUPS` antes de alterar arquivos.
- Consegue restaurar o backup mais recente.

Veja [docs/INSTALLER.md](docs/INSTALLER.md) para detalhes.

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
