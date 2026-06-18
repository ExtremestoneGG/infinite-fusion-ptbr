class FusionSelectOptionsScene < PokemonOption_Scene
  attr_accessor :selectedAbility
  attr_accessor :selectedNature
  attr_accessor :hasNickname
  attr_accessor :nickname


  def initialize(abilityList,natureList, pokemon1, pokemon2)
    @abilityList = abilityList
    @natureList = natureList
    @selectedAbility=nil
    @selectedNature=nil
    @selBaseColor = Color.new(48,96,216)
    @selShadowColor = Color.new(32,32,32)
    @show_frame=false
    @hasNickname = false
    @nickname = nil

    @pokemon1=pokemon1
    @pokemon2=pokemon2
  end


  def initUIElements
    @sprites["title"] = Window_UnformattedTextPokemon.newWithSize(
      _INTL("Escolha a habilidade e a Nature do seu Pokémon"), 0, 0, Graphics.width, 64, @viewport)
    @sprites["textbox"] = pbCreateMessageWindow
    @sprites["textbox"].letterbyletter = false
    pbSetSystemFont(@sprites["textbox"].contents)
    @sprites["title"].opacity=0
  end

  def pbStartScene(inloadscreen = nil)
    super
    @sprites["option"].opacity=0
  end


  def getAbilityName(ability)
    return GameData::Ability.get(ability.id).real_name
  end

  def getAbilityDescription(ability)
    return GameData::Ability.get(ability.id).description
  end

  def getNatureName(nature)
    return GameData::Nature.get(nature.id).name
  end

  def getNatureDescription(nature)
    change= GameData::Nature.get(nature.id).stat_changes
    return _INTL("Nature neutra") if change.empty?
    positiveChange = change[0]
    negativeChange = change[1]
    stat_names = {
      :ATTACK => _INTL("Ataque"),
      :DEFENSE => _INTL("Defesa"),
      :SPECIAL_ATTACK => _INTL("Ataque Especial"),
      :SPECIAL_DEFENSE => _INTL("Defesa Especial"),
      :SPEED => _INTL("Velocidade")
    }
    return _INTL("+ {1}\n- {2}", stat_names[positiveChange[0]] || GameData::Stat.get(positiveChange[0]).name,
                                      stat_names[negativeChange[0]] || GameData::Stat.get(negativeChange[0]).name)
  end

  def shouldSelectNickname
    if @pokemon1.nicknamed? && @pokemon2.nicknamed?
      @hasNickname=true
      return true
    end
    if @pokemon1.nicknamed? && !@pokemon2.nicknamed?
      @hasNickname=true
      @nickname = @pokemon1.name
      return false
    end
    if !@pokemon1.nicknamed? && @pokemon2.nicknamed?
      @hasNickname=true
      @nickname = @pokemon2.name
      return false
    end
    @hasNickname=false
    return false
  end

  def pbGetOptions(inloadscreen = false)

    options = []
    if shouldSelectNickname
      options << EnumOption.new(_INTL("Apelido"), [_INTL(@pokemon1.name), _INTL(@pokemon2.name)],
                                proc { 0 },
                                proc { |value|
                                  if value ==0
                                    @nickname = @pokemon1.name
                                  else
                                    @nickname = @pokemon2.name
                                  end
                                }, _INTL("Escolha o apelido do Pokémon"))
    end

    options << EnumOption.new(_INTL("Habilidade"), [_INTL(getAbilityName(@abilityList[0])), _INTL(getAbilityName(@abilityList[1]))],
                     proc { 0 },
                     proc { |value|
                       @selectedAbility=@abilityList[value]
                     }, [getAbilityDescription(@abilityList[0]), getAbilityDescription(@abilityList[1])]
      )

    options << EnumOption.new(_INTL("Nature"), [_INTL(getNatureName(@natureList[0])), _INTL(getNatureName(@natureList[1]))],
                     proc { 0 },
                     proc { |value|
                       @selectedNature=@natureList[value]
                     }, [getNatureDescription(@natureList[0]), getNatureDescription(@natureList[1])]
      )
    return options
  end


  def isConfirmedOnKeyPress
    return true
  end

end

