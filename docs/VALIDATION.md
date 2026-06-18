# Validation

## English

Release 1.1.0 passed the following checks:

- `Data/portuguese.dat` was generated with `20,577` translated language entries.
- `Data/pokedex/all_entries.json` was generated with `475` translated base Pokedex entries.
- Fusion Pokedex cache contains `61,726` translation mappings.
- Translation error list is empty.
- `Data/pokedex/dex.json` and `Data/dex.json` load as valid JSON.
- The active Pokedex files have `0` obvious English sentence flags after final polish.
- The active Pokedex files have `0` `Pokebola` flags after final polish, keeping the item/object term as `Pokeball`.
- Outfit JSON validation confirmed `0` outfit name changes; only descriptions were translated.
- v1.1.0 fixes `18/18` battle text strings from the current screenshot report set.
- README showcase includes `9` in-game PT-BR screenshots.
- The installer was tested on a temporary sandbox folder for install, required backup, JSON patching, and restore.
- The modern WPF `.exe` installer source code is included in `installer-src/ModernInstaller.cs`.
- Shared installer core code is included in `installer-src/InfiniteFusionPtbrInstaller.cs`.
- The recommended release asset is a single `.exe`; BAT/PowerShell installation is packaged separately.

The machine-readable report is in `docs/reports/validation_report.json`.

## Portugues

A versao 1.1.0 passou pelas seguintes checagens:

- `Data/portuguese.dat` foi gerado com `20.577` entradas traduzidas.
- `Data/pokedex/all_entries.json` foi gerado com `475` entradas base traduzidas da Pokedex.
- O cache da Pokedex de fusoes contem `61.726` mapeamentos de traducao.
- A lista de erros de traducao esta vazia.
- `Data/pokedex/dex.json` e `Data/dex.json` carregam como JSON valido.
- Os arquivos ativos da Pokedex tem `0` frases obvias em ingles apos o polimento final.
- Os arquivos ativos da Pokedex tem `0` ocorrencias de `Pokebola` apos o polimento final, mantendo o termo como `Pokeball`.
- A validacao dos JSONs de roupas confirmou `0` alteracoes em nomes; apenas descricoes foram traduzidas.
- A v1.1.0 corrige `18/18` textos de batalha do conjunto atual de prints reportados.
- O README inclui `9` imagens de demonstracao da traducao PT-BR dentro do jogo.
- O instalador foi testado em uma pasta temporaria para instalar, criar backup obrigatorio, aplicar patch JSON e restaurar.
- O codigo-fonte do instalador WPF moderno esta em `installer-src/ModernInstaller.cs`.
- O nucleo compartilhado do instalador esta em `installer-src/InfiniteFusionPtbrInstaller.cs`.
- O arquivo recomendado da release e um `.exe` unico; a instalacao BAT/PowerShell fica em um pacote separado.

O relatorio em formato de maquina esta em `docs/reports/validation_report.json`.
