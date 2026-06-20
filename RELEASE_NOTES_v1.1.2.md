# Pokemon Infinite Fusion PT-BR Fan Translation v1.1.2

Installer-focused hotfix for the public fan-made Brazilian Portuguese translation release.

This release includes:

- A redesigned native Windows graphical installer with a borderless GitHub-dark window, custom title bar, clearer buttons, and the project installer icon.
- A visible `Desinstalar tradução` action.
- Save safety during graphical uninstall: the installer backs up save files to `PTBR_BACKUPS/save_backups` and resets the saved `PokemonSystem.language` value back to English before restoring game files.
- The previous quest color-code startup crash fix remains included.
- Pokedex species/fusion descriptions remain deferred as future work and are not included in the active payload.

Known update/save issue:

If the official game updater restores original English scripts while a save was last used with the PT-BR translation, the game can crash at `MultiSaves.rb line 786`. Reinstall the PT-BR translation before opening the save, or use `Desinstalar tradução` when intentionally removing the translation.

Release files:

- `InfiniteFusionPTBRInstaller-v1.1.2.exe`: recommended single-file graphical installer.
- `PTBR-Translation-BAT-v1.1.2.zip`: separate BAT/PowerShell package.

## Português

Hotfix focado no instalador da tradução fan-made em Português do Brasil.

Esta release inclui:

- Um instalador gráfico nativo para Windows redesenhado, com janela sem borda, tema escuro estilo GitHub, barra superior própria, botões mais claros e ícone do projeto.
- Ação visível `Desinstalar tradução`.
- Segurança de save durante a desinstalação pelo `.exe`: o instalador cria backup dos saves em `PTBR_BACKUPS/save_backups` e volta o valor salvo de `PokemonSystem.language` para inglês antes de restaurar os arquivos do jogo.
- A correção anterior do crash ao iniciar por escape inválido em quests continua incluída.
- Descrições de espécies/fusões da Pokédex continuam como trabalho futuro e não entram no payload ativo.

Erro conhecido de update/save:

Se o updater oficial do jogo restaurar os scripts originais em inglês enquanto o save foi usado por último com a tradução PT-BR, o jogo pode crashar em `MultiSaves.rb line 786`. Reinstale a tradução PT-BR antes de abrir o save, ou use `Desinstalar tradução` se a intenção for remover a tradução.

Arquivos da release:

- `InfiniteFusionPTBRInstaller-v1.1.2.exe`: instalador gráfico recomendado em arquivo único.
- `PTBR-Translation-BAT-v1.1.2.zip`: pacote BAT/PowerShell separado.
