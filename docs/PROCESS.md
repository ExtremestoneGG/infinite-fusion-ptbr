# Translation Process

## English

This project started from the existing language structure in Pokemon Infinite Fusion. The game already had English and French language files, so the translation workflow focused on adding a Portuguese language file and patching remaining script-level text that was not fully covered by the language database.

The process was:

1. Inspect the game's language files and script structure.
2. Build `Data/portuguese.dat` from the extracted translatable strings.
3. Translate story, quests, dialogue, NPC lines, menus, and descriptions with context in mind.
4. Preserve names that Brazilian players usually keep in English: Pokemon names, item names, move names, ability names, NPC names, location names, organization names, and type names.
5. Add `Português` to the language list in `Data/Scripts/001_Settings.rb`.
6. Patch script-level UI strings that do not reliably change through the language file alone.
7. Translate Pokedex entries and outfit descriptions through text-only JSON mappings.
8. Run validation scans for JSON validity, known exception rules, and obvious English leftovers.
9. Package only the translation patch, not the full game.

The release uses a text patch for large JSON files so installer users keep their local metadata, sprite data, and game file structure as much as possible.

## Português

Este projeto começou a partir da estrutura de idiomas que já existia no Pokemon Infinite Fusion. O jogo já tinha arquivos de inglês e francês, então o fluxo foi adicionar um arquivo de português e ajustar textos de script que não eram totalmente cobertos pelo banco de idioma.

O processo foi:

1. Analisar os arquivos de idioma e a estrutura de scripts do jogo.
2. Criar `Data/portuguese.dat` a partir dos textos extraídos.
3. Traduzir história, quests, diálogos, falas de NPCs, menus e descrições pensando no contexto.
4. Preservar nomes que jogadores brasileiros normalmente mantêm em inglês: nomes de Pokemon, itens, golpes, habilidades, NPCs, lugares, organizações e tipos.
5. Adicionar `Português` à lista de idiomas em `Data/Scripts/001_Settings.rb`.
6. Ajustar textos de interface em scripts quando eles não mudavam de forma confiável apenas pelo arquivo de idioma.
7. Traduzir entradas da Pokédex e descrições de roupas usando mapeamentos apenas de texto.
8. Rodar varreduras de validação para JSON, regras de exceção e restos óbvios em inglês.
9. Empacotar somente o patch de tradução, não o jogo completo.

A release usa patch de texto para arquivos JSON grandes para que o usuário mantenha metadados locais, dados de sprites e a estrutura da instalação sempre que possível.
