# Changelog

## 1.1.2

### English

- Redesigned the graphical Windows installer with a native borderless dark window, custom title bar, improved button styling, installer icon usage, and clearer action hierarchy.
- Added an explicit `Desinstalar tradução` button.
- During graphical uninstall, the installer now backs up save files under `PTBR_BACKUPS/save_backups` and resets the saved `PokemonSystem.language` value back to English to avoid the known post-uninstall save crash.
- Kept the scan cancel flow and `Game.exe` validation flow.
- Kept Pokedex species/fusion descriptions deferred as future work and outside the active payload.

### Portugues

- Redesenhado o instalador grafico para Windows com janela nativa sem borda, barra superior escura propria, botoes melhores, uso do icone do instalador e hierarquia de acoes mais clara.
- Adicionado o botao explicito `Desinstalar tradução`.
- Durante a desinstalacao pelo `.exe`, o instalador agora cria backup dos saves em `PTBR_BACKUPS/save_backups` e volta o valor salvo de `PokemonSystem.language` para ingles, evitando o crash conhecido apos remover a traducao.
- Mantido o fluxo de cancelar escaneamento e validacao por `Game.exe`.
- Descricoes de especies/fusoes da Pokedex continuam como trabalho futuro e fora do payload ativo.

## 1.1.0

### English

- Rebuilt the recommended Windows installer with a cleaner WPF interface, GitHub-dark styling, project artwork, and a custom installer icon.
- Added `Game.exe` selection, automatic scan for likely game folders, update flow for existing PT-BR installs, scan cancellation, and a shortcut to open the backup folder.
- Kept backup mandatory before every install/update.
- Fixed remaining English battle intro messages such as wild encounters, trainer challenges, send-out messages, Safari encounters, weather, and terrain announcements.
- Added README showcase screenshots from the in-game PT-BR translation.
- Added a coverage snapshot by translation category for public testing.
- Documented the official updater/save compatibility crash and the reinstall workaround.
- Kept Pokedex species/fusion descriptions out of the active v1.1.0 payload; they remain future work.
- Hotfix: fixed invalid Ruby escape sequences in quest notification color codes that could crash on startup.

### Português

- Refeito o instalador recomendado para Windows com interface WPF mais limpa, estilo escuro do GitHub, arte do projeto e ícone personalizado.
- Adicionada seleção por `Game.exe`, busca automática por possíveis pastas do jogo, fluxo de atualização para instalações PT-BR existentes, cancelamento de busca e atalho para abrir a pasta de backups.
- Backup mantido como obrigatório antes de toda instalação/atualização.
- Corrigidas mensagens de batalha que ainda apareciam em inglês, incluindo encontros selvagens, desafios de treinadores, envio de Pokémon, Safari, clima e terreno.
- Adicionadas imagens de demonstração da tradução PT-BR no README.
- Adicionado resumo de cobertura por categoria para a fase de testes públicos.
- Documentado o crash conhecido de compatibilidade entre updater oficial/save e a solução de reinstalar a tradução.
- Descrições de espécies/fusões da Pokédex ficaram fora do payload ativo da v1.1.0; seguem como trabalho futuro.
- Hotfix: corrigidas sequências inválidas de escape Ruby nos códigos de cor das notificações de quests que podiam causar crash ao iniciar.

## 1.0.0

### English

- Added Brazilian Portuguese language file.
- Added Portuguese as a selectable language.
- Translated story, quest, dialogue, gameplay UI, shop/service text, item/move/ability descriptions, and outfit descriptions within the agreed scope. Pokedex species/fusion descriptions are tracked as future work.
- Preserved Pokemon, move, item, ability, NPC, location, organization, and type names.
- Added a single-file graphical Windows `.exe` installer with folder selection, install, required backup, validation, and restore options.
- Added a separate BAT/PowerShell installer package with backup, restore, and one-line PowerShell install support.
- Added a PT-BR Mod README logo/banner.
- Added documentation for process, scope, validation, and installation.
- Added a final validation report.

### Português

- Adicionado arquivo de idioma em Português do Brasil.
- Adicionado Português como idioma selecionável.
- Traduzidos textos de história, quests, diálogos, interface, lojas/serviços, descrições de itens/golpes/habilidades e descrições de roupas dentro do escopo combinado. Descrições de espécies/fusões da Pokédex ficam registradas como trabalho futuro.
- Preservados nomes de Pokemon, golpes, itens, habilidades, NPCs, locais, organizações e tipos.
- Adicionado instalador gráfico `.exe` em arquivo único para Windows com seleção de pasta, instalação, backup obrigatório, validação e restauração.
- Adicionado pacote separado de instalação BAT/PowerShell com backup, restauração e suporte a instalação por uma linha no PowerShell.
- Adicionada logo/banner PT-BR Mod no README.
- Adicionada documentação de processo, escopo, validação e instalação.
- Adicionado relatório final de validação.
