# Validation

## English

Release 1.1.0 passed the following checks:

- `Data/portuguese.dat` was generated with `20,581` translated language entries.
- Direct payload files were synced against the translated game folder: `106`.
- Packaged script files were synced against the translated game folder: `105`.
- Translation error list is empty.
- Post-build text fixes applied: `8`.
- Pokedex species/fusion descriptions are deferred to a future project and are not counted as complete in v1.1.0.
- Outfit JSON validation confirmed `0` outfit name changes; only descriptions were translated.
- v1.1.0 fixes `18/18` battle text strings from the current screenshot report set.
- README showcase includes `8` in-game PT-BR screenshots.
- Known official updater/save compatibility crash documented with the reinstall workaround.
- The installer payload was statically validated: embedded payload, manifest, no active Pokedex payload, outfit patch presence, and compiled resources. Direct GUI invocation was blocked by the local sandbox.
- The modern WPF `.exe` installer source code is included in `installer-src/ModernInstaller.cs`.
- Shared installer core code is included in `installer-src/InfiniteFusionPtbrInstaller.cs`.
- The recommended release asset is a single `.exe`; BAT/PowerShell installation is packaged separately.

The machine-readable report is in `docs/reports/validation_report.json`.

## Portugues

A versao 1.1.0 passou pelas seguintes checagens:

- `Data/portuguese.dat` foi gerado com `20.581` entradas traduzidas.
- Arquivos diretos do payload foram sincronizados com a pasta traduzida do jogo: `106`.
- Arquivos de script empacotados foram sincronizados com a pasta traduzida do jogo: `105`.
- A lista de erros de traducao esta vazia.
- Ajustes de texto pos-build aplicados: `8`.
- As descricoes de especies/fusoes da Pokedex ficam para um projeto futuro e nao contam como concluidas na v1.1.0.
- A validacao dos JSONs de roupas confirmou `0` alteracoes em nomes; apenas descricoes foram traduzidas.
- A v1.1.0 corrige `18/18` textos de batalha do conjunto atual de prints reportados.
- O README inclui `8` imagens de demonstracao da traducao PT-BR dentro do jogo.
- Crash conhecido de compatibilidade entre updater oficial/save documentado com a solucao de reinstalar a traducao.
- O payload do instalador foi validado estaticamente: payload embutido, manifesto, nenhuma Pok?dex ativa, patch de roupas e recursos compilados. A execu??o direta da GUI foi bloqueada pelo sandbox local.
- O codigo-fonte do instalador WPF moderno esta em `installer-src/ModernInstaller.cs`.
- O nucleo compartilhado do instalador esta em `installer-src/InfiniteFusionPtbrInstaller.cs`.
- O arquivo recomendado da release e um `.exe` unico; a instalacao BAT/PowerShell fica em um pacote separado.

O relatorio em formato de maquina esta em `docs/reports/validation_report.json`.
