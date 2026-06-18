module GameData
  class Nature
    attr_reader :id
    attr_reader :id_number
    attr_reader :real_name
    attr_reader :stat_changes

    DATA = {}

    extend ClassMethods
    include InstanceMethods

    def self.load; end
    def self.save; end

    def initialize(hash)
      @id           = hash[:id]
      @id_number    = hash[:id_number]    || -1
      @real_name    = hash[:name]         || "Unnamed"
      @stat_changes = hash[:stat_changes] || []
    end

    # @return [String] the translated name of this nature
    def name
      return _INTL(@real_name)
    end
  end
end

#===============================================================================

GameData::Nature.register({
  :id           => :HARDY,
  :id_number    => 0,
  :name         => _INTL("Resistente")
})

GameData::Nature.register({
  :id           => :LONELY,
  :id_number    => 1,
  :name         => _INTL("Solitária"),
  :stat_changes => [[:ATTACK, 10], [:DEFENSE, -10]]
})

GameData::Nature.register({
  :id           => :BRAVE,
  :id_number    => 2,
  :name         => _INTL("Audaz"),
  :stat_changes => [[:ATTACK, 10], [:SPEED, -10]]
})

GameData::Nature.register({
  :id           => :ADAMANT,
  :id_number    => 3,
  :name         => _INTL("Firme"),
  :stat_changes => [[:ATTACK, 10], [:SPECIAL_ATTACK, -10]]
})

GameData::Nature.register({
  :id           => :NAUGHTY,
  :id_number    => 4,
  :name         => _INTL("Travessa"),
  :stat_changes => [[:ATTACK, 10], [:SPECIAL_DEFENSE, -10]]
})

GameData::Nature.register({
  :id           => :BOLD,
  :id_number    => 5,
  :name         => _INTL("Ousada"),
  :stat_changes => [[:DEFENSE, 10], [:ATTACK, -10]]
})

GameData::Nature.register({
  :id           => :DOCILE,
  :id_number    => 6,
  :name         => _INTL("Dócil")
})

GameData::Nature.register({
  :id           => :RELAXED,
  :id_number    => 7,
  :name         => _INTL("Relaxada"),
  :stat_changes => [[:DEFENSE, 10], [:SPEED, -10]]
})

GameData::Nature.register({
  :id           => :IMPISH,
  :id_number    => 8,
  :name         => _INTL("Arteira"),
  :stat_changes => [[:DEFENSE, 10], [:SPECIAL_ATTACK, -10]]
})

GameData::Nature.register({
  :id           => :LAX,
  :id_number    => 9,
  :name         => _INTL("Descontraída"),
  :stat_changes => [[:DEFENSE, 10], [:SPECIAL_DEFENSE, -10]]
})

GameData::Nature.register({
  :id           => :TIMID,
  :id_number    => 10,
  :name         => _INTL("Tímida"),
  :stat_changes => [[:SPEED, 10], [:ATTACK, -10]]
})

GameData::Nature.register({
  :id           => :HASTY,
  :id_number    => 11,
  :name         => _INTL("Apressada"),
  :stat_changes => [[:SPEED, 10], [:DEFENSE, -10]]
})

GameData::Nature.register({
  :id           => :SERIOUS,
  :id_number    => 12,
  :name         => _INTL("Séria")
})

GameData::Nature.register({
  :id           => :JOLLY,
  :id_number    => 13,
  :name         => _INTL("Alegre"),
  :stat_changes => [[:SPEED, 10], [:SPECIAL_ATTACK, -10]]
})

GameData::Nature.register({
  :id           => :NAIVE,
  :id_number    => 14,
  :name         => _INTL("Ingênua"),
  :stat_changes => [[:SPEED, 10], [:SPECIAL_DEFENSE, -10]]
})

GameData::Nature.register({
  :id           => :MODEST,
  :id_number    => 15,
  :name         => _INTL("Modesta"),
  :stat_changes => [[:SPECIAL_ATTACK, 10], [:ATTACK, -10]]
})

GameData::Nature.register({
  :id           => :MILD,
  :id_number    => 16,
  :name         => _INTL("Meiga"),
  :stat_changes => [[:SPECIAL_ATTACK, 10], [:DEFENSE, -10]]
})

GameData::Nature.register({
  :id           => :QUIET,
  :id_number    => 17,
  :name         => _INTL("Quieta"),
  :stat_changes => [[:SPECIAL_ATTACK, 10], [:SPEED, -10]]
})

GameData::Nature.register({
  :id           => :BASHFUL,
  :id_number    => 18,
  :name         => _INTL("Envergonhada")
})

GameData::Nature.register({
  :id           => :RASH,
  :id_number    => 19,
  :name         => _INTL("Precipitada"),
  :stat_changes => [[:SPECIAL_ATTACK, 10], [:SPECIAL_DEFENSE, -10]]
})

GameData::Nature.register({
  :id           => :CALM,
  :id_number    => 20,
  :name         => _INTL("Calma"),
  :stat_changes => [[:SPECIAL_DEFENSE, 10], [:ATTACK, -10]]
})

GameData::Nature.register({
  :id           => :GENTLE,
  :id_number    => 21,
  :name         => _INTL("Gentil"),
  :stat_changes => [[:SPECIAL_DEFENSE, 10], [:DEFENSE, -10]]
})

GameData::Nature.register({
  :id           => :SASSY,
  :id_number    => 22,
  :name         => _INTL("Atrevida"),
  :stat_changes => [[:SPECIAL_DEFENSE, 10], [:SPEED, -10]]
})

GameData::Nature.register({
  :id           => :CAREFUL,
  :id_number    => 23,
  :name         => _INTL("Cuidadosa"),
  :stat_changes => [[:SPECIAL_DEFENSE, 10], [:SPECIAL_ATTACK, -10]]
})

GameData::Nature.register({
  :id           => :QUIRKY,
  :id_number    => 24,
  :name         => _INTL("Peculiar")
})
