class PokemonGameOption_Scene < PokemonOption_Scene
  def pbGetOptions(inloadscreen = false)
    @current_game_mode = getTrainersDataMode
    options = []
    options << SliderOption.new(_INTL("Volume da Música"), 0, 100, 5,
                                proc { $PokemonSystem.bgmvolume },
                                proc { |value|
                                  if $PokemonSystem.bgmvolume != value
                                    $PokemonSystem.bgmvolume = value
                                    if $game_system.playing_bgm != nil && !inloadscreen
                                      playingBGM = $game_system.getPlayingBGM
                                      $game_system.bgm_pause
                                      $game_system.bgm_resume(playingBGM)
                                    end
                                  end
                                }, _INTL("Define o volume da música de fundo")
    )

    options << SliderOption.new(_INTL("Volume dos Efeitos"), 0, 100, 5,
                                proc { $PokemonSystem.sevolume },
                                proc { |value|
                                  if $PokemonSystem.sevolume != value
                                    $PokemonSystem.sevolume = value
                                    if $game_system.playing_bgs != nil
                                      $game_system.playing_bgs.volume = value
                                      playingBGS = $game_system.getPlayingBGS
                                      $game_system.bgs_pause
                                      $game_system.bgs_resume(playingBGS)
                                    end
                                    pbPlayCursorSE
                                  end
                                }, _INTL("Define o volume dos efeitos sonoros")
    )

    options << EnumOption.new(_INTL("Movimento Padrão"), [_INTL("Andando"), _INTL("Correndo")],
                              proc { $PokemonSystem.runstyle },
                              proc { |value| $PokemonSystem.runstyle = value },
                              [_INTL("Anda por padrão quando a tecla de correr não está pressionada"),
                               _INTL("Corre por padrão quando a tecla de correr não está pressionada")]
    )

    options << EnumOption.new(_INTL("Velocidade do Texto"), [_INTL("Normal"), _INTL("Rápida")],
                              proc { $PokemonSystem.textspeed },
                              proc { |value|
                                $PokemonSystem.textspeed = value
                                MessageConfig.pbSetTextSpeed(MessageConfig.pbSettingToTextSpeed(value))
                              }, _INTL("Define a velocidade em que o texto é exibido")
    )
    if $game_switches
      options << EnumOption.new(_INTL("Dificuldade"), [_INTL("Fácil"), _INTL("Normal"), _INTL("Difícil")],
                                proc { $Trainer.selected_difficulty },
                                proc { |value|
                                  setDifficulty(value)
                                  @manually_changed_difficulty = true
                                }, [_INTL("Todos os Pokémon da equipe ganham experiência. No resto, igual à dificuldade Normal."),
                                    _INTL("A experiência padrão. Os níveis são parecidos com os jogos oficiais."),
                                    _INTL("Níveis mais altos e IA mais esperta. Todos os treinadores têm acesso a itens de cura.")]
      )
    end

    if $game_switches
      options <<
        EnumOption.new(_INTL("Salvar automático"), [_INTL("Ligado"), _INTL("Desligado")],
                       proc { $game_switches[AUTOSAVE_ENABLED_SWITCH] ? 0 : 1 },
                       proc { |value|
                         if !$game_switches[AUTOSAVE_ENABLED_SWITCH] && value == 0
                           @autosave_menu = true
                           openAutosaveMenu()
                         end
                         $game_switches[AUTOSAVE_ENABLED_SWITCH] = value == 0
                       },
                       _INTL("Salva automaticamente ao curar em Pokémon Centers")
        )
    end

    options << EnumOption.new(_INTL("Tipo de aceleração"), [_INTL("Segurar"), _INTL("Alternar")],
                              proc { $PokemonSystem.speedup },
                              proc { |value|
                                $PokemonSystem.speedup = value
                              }, _INTL("Escolha como a aceleração será ativada")
    )

    options << SliderOption.new(_INTL("Vel. da aceleração"), 1, 10, 1,
                                proc { $PokemonSystem.speedup_speed },
                                proc { |value|
                                  $PokemonSystem.speedup_speed = value
                                }, _INTL("Define o quanto o jogo acelera ao usar o botão de aceleração. Padrão: 3x")
    )
    # if $game_switches && ($game_switches[SWITCH_NEW_GAME_PLUS] || $game_switches[SWITCH_BEAT_THE_LEAGUE]) #beat the league
    #   options << EnumOption.new("Text Speed", ["Normal", "Fast", "Instant"],
    #                             proc { $PokemonSystem.textspeed },
    #                             proc { |value|
    #                               $PokemonSystem.textspeed = value
    #                               MessageConfig.pbSetTextSpeed(MessageConfig.pbSettingToTextSpeed(value))
    #                             }, "Sets the speed at which the text is displayed"
    #   )
    # else
    #   options << EnumOption.new("Text Speed", ["Normal", "Fast"],
    #                             proc { $PokemonSystem.textspeed },
    #                             proc { |value|
    #                               $PokemonSystem.textspeed = value
    #                               MessageConfig.pbSetTextSpeed(MessageConfig.pbSettingToTextSpeed(value))
    #                             }, "Sets the speed at which the text is displayed"
    #   )
    # end
    options <<
      EnumOption.new(_INTL("Baixar dados"), [_INTL("Ligado"), _INTL("Desligado")],
                     proc { $PokemonSystem.download_sprites },
                     proc { |value|
                       $PokemonSystem.download_sprites = value
                     },
                     _INTL("Baixa automaticamente sprites personalizados e entradas da Pokédex que estiverem faltando")
      )
    #
    generated_entries_option_selected = $PokemonSystem.use_generated_dex_entries ? 1 : 0
    options << EnumOption.new(_INTL("Entradas autogeradas"), [_INTL("Desligado"), _INTL("Ligado")],
                              proc { generated_entries_option_selected },
                              proc { |value|
                                $PokemonSystem.use_generated_dex_entries = value == 1
                              },
                              [
                                _INTL("Fusões sem entrada personalizada na Pokédex não exibem nada."),
                                _INTL("Fusões sem entrada personalizada na Pokédex exibem um texto autogerado.")

                              ]
    )

    generated_entries_option_selected = $PokemonSystem.include_alt_sprites_in_random ? 1 : 0
    options << EnumOption.new(_INTL("Categorias de sprite"), [_INTL("Normal"), _INTL("Qualquer")],
                              proc { generated_entries_option_selected },
                              proc { |value|
                                $PokemonSystem.include_alt_sprites_in_random = value == 1
                              },
                              [
                                _INTL("Sprites escolhidos automaticamente seguem as regras padrão de sprites de Pokémon."),
                                _INTL("Sprites escolhidos automaticamente podem ser qualquer coisa, incluindo referências, memes e piadas.")
                              ]
    ) ? 1 : 0

    custom_eggs_option_selected = $PokemonSystem.hide_custom_eggs ? 1 : 0
    options << EnumOption.new(_INTL("Eggs personalizados"), [_INTL("Ligado"), _INTL("Desligado")],
                              proc { custom_eggs_option_selected },
                              proc { |value|
                                $PokemonSystem.hide_custom_eggs = value == 1
                              },
                              [_INTL("Eggs têm sprites diferentes para cada Pokémon."),
                               _INTL("Todos os Eggs usam o mesmo sprite.")]
    )

    if $game_switches && ($game_switches[SWITCH_NEW_GAME_PLUS] || $game_switches[SWITCH_BEAT_THE_LEAGUE]) # beat the league
      options <<
        EnumOption.new(_INTL("Tipo de batalha"), [_INTL("1v1"), _INTL("2v2"), _INTL("3v3")],
                       proc { $PokemonSystem.battle_type },
                       proc { |value|
                         if value == 0
                           $game_variables[VAR_DEFAULT_BATTLE_TYPE] = [1, 1]
                         elsif value == 1
                           $game_variables[VAR_DEFAULT_BATTLE_TYPE] = [2, 2]
                         elsif value == 2
                           $game_variables[VAR_DEFAULT_BATTLE_TYPE] = [3, 3]
                         else
                           $game_variables[VAR_DEFAULT_BATTLE_TYPE] = [1, 1]
                         end
                         $PokemonSystem.battle_type = value
                       }, _INTL("Define o número de Pokémon enviados nas batalhas, quando possível")
        )
    end

    options << EnumOption.new(_INTL("Efeitos de batalha"), [_INTL("Ligado"), _INTL("Desligado")],
                              proc { $PokemonSystem.battlescene },
                              proc { |value| $PokemonSystem.battlescene = value },
                              _INTL("Exibe animações dos golpes nas batalhas")
    )

    options << EnumOption.new(_INTL("Estilo de batalha"), [_INTL("Trocar"), _INTL("Fixo")],
                              proc { $PokemonSystem.battlestyle },
                              proc { |value| $PokemonSystem.battlestyle = value },
                              [_INTL("Pergunta se quer trocar de Pokémon antes de o oponente enviar o próximo"),
                               _INTL("Não pergunta se quer trocar de Pokémon antes do próximo oponente")]
    )

    options << NumberOption.new(_INTL("Moldura de fala"), 1, Settings::SPEECH_WINDOWSKINS.length,
                                proc { $PokemonSystem.textskin },
                                proc { |value|
                                  $PokemonSystem.textskin = value
                                  MessageConfig.pbSetSpeechFrame("Graphics/Windowskins/" + Settings::SPEECH_WINDOWSKINS[value])
                                }
    )
    # NumberOption.new("Menu Frame",1,Settings::MENU_WINDOWSKINS.length,
    #   proc { $PokemonSystem.frame },
    #   proc { |value|
    #     $PokemonSystem.frame = value
    #     MessageConfig.pbSetSystemFrame("Graphics/Windowskins/" + Settings::MENU_WINDOWSKINS[value])
    #   }
    # ),
    options << EnumOption.new(_INTL("Entrada de texto"), [_INTL("Cursor"), _INTL("Teclado")],
                              proc { $PokemonSystem.textinput },
                              proc { |value| $PokemonSystem.textinput = value },
                              [_INTL("Digite o texto selecionando letras na tela"),
                               _INTL("Digite o texto pelo teclado")]
    )
    if $game_variables
      options << EnumOption.new(_INTL("Ícones de Fusão"), [_INTL("Combinado"), _INTL("DNA")],
                                proc { $game_variables[VAR_FUSION_ICON_STYLE] },
                                proc { |value| $game_variables[VAR_FUSION_ICON_STYLE] = value },
                                [_INTL("Combina os ícones dos dois Pokémon na party"),
                                 _INTL("Usa o mesmo ícone de party para todas as fusões")]
      )
      battle_type_icon_option_selected = $PokemonSystem.type_icons ? 1 : 0
      options << EnumOption.new(_INTL("Ícones de tipo em batalha"), [_INTL("Desligado"), _INTL("Ligado")],
                                proc { battle_type_icon_option_selected },
                                proc { |value| $PokemonSystem.type_icons = value == 1 },
                                _INTL("Mostra o tipo do Pokémon inimigo nas batalhas.")
      )

    end
    options << EnumOption.new(_INTL("Tamanho da tela"), [_INTL("S"), _INTL("M"), _INTL("L"), _INTL("XL"), _INTL("Cheia")],
                              proc { [$PokemonSystem.screensize, 4].min },
                              proc { |value|
                                if $PokemonSystem.screensize != value
                                  $PokemonSystem.screensize = value
                                  pbSetResizeFactor($PokemonSystem.screensize)
                                  echoln $PokemonSystem.screensize
                                end
                              }, _INTL("Define o tamanho da tela")
    )
    options << EnumOption.new(_INTL("Surf rápido"), [_INTL("Desligado"), _INTL("Ligado")],
                              proc { $PokemonSystem.quicksurf },
                              proc { |value| $PokemonSystem.quicksurf = value },
                              _INTL("Começa a surfar automaticamente ao interagir com água")
    )

    options << EnumOption.new(_INTL("Limite de nível"), [_INTL("Desligado"), _INTL("Ligado")],
                              proc { $PokemonSystem.level_caps },
                              proc { |value| $PokemonSystem.level_caps = value },
                              _INTL("Impede subir acima do nível mais alto do próximo Gym Leader")
    )

    device_option_selected = $PokemonSystem.on_mobile ? 1 : 0
    options << EnumOption.new(_INTL("Dispositivo"), [_INTL("PC"), _INTL("Celular")],
                              proc { device_option_selected },
                              proc { |value| $PokemonSystem.on_mobile = value == 1 },
                              [_INTL("O dispositivo pretendido para jogar."),
                               _INTL("Desativa algumas opções sem suporte ao jogar no celular.")]
    )

    if $game_switches && $game_switches[SWITCH_LEGENDARY_MODE]
      selected_game_mode = $game_switches[SWITCH_MODERN_MODE] ? 1 : 0
      options << EnumOption.new(_INTL("Trainers"), [_INTL("Classic"), _INTL("Remix")],
                                proc { selected_game_mode },
                                proc { |value|
                                  $game_switches[SWITCH_MODERN_MODE] = value == 1
                                  @manually_changed_gamemode = true
                                },
                                [_INTL("Usa trainers do Classic Mode no Legendary Mode"),
                                 _INTL("Usa trainers do Remix Mode no Legendary Mode")]
      )
    end
    return options
  end

  def pbEndScene
    echoln "Selected Difficulty: #{$Trainer.selected_difficulty}, lowest difficutly: #{$Trainer.lowest_difficulty}" if $Trainer
    if $Trainer && $Trainer.selected_difficulty < $Trainer.lowest_difficulty
      $Trainer.lowest_difficulty = $Trainer.selected_difficulty
      echoln "lowered difficulty (#{$Trainer.selected_difficulty})"
      if @manually_changed_difficulty
        pbMessage(_INTL("The savefile's lowest selected difficulty was changed to #{getDisplayDifficulty()}."))
        @manually_changed_difficulty = false
      end
    end

    if getTrainersDataMode != @current_game_mode
      pbMessage(_INTL("The game was mode changed - Reshuffling trainers."))
      Kernel.pbShuffleTrainers
      @manually_changed_gamemode = false
    end

    super
  end
end

