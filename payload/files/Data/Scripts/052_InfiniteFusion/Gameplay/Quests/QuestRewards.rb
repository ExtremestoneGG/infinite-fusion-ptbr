#
# Rewards given by hotel questman after a certain nb. of completed quests
#
QUEST_REWARDS = [
  QuestReward.new(1, :HM08, 1, _INTL("Este HM vai permitir iluminar cavernas escuras e deve ajudar você a avançar na sua jornada!")),
  QuestReward.new(5, :AMULETCOIN, 1, _INTL("Este item permite ganhar o dobro de dinheiro em uma batalha se o Pokémon que estiver segurando ele participar!")),
  QuestReward.new(10, :LANTERN, 1, _INTL("Isto vai permitir iluminar cavernas sem precisar usar um HM! Prático, não é?")),
  QuestReward.new(15, :LINKINGCORD, 3, _INTL("Este cabo estranho ativa a evolução de Pokémon que normalmente evoluem por troca. Sei que você vai usar bem!")),
  QuestReward.new(20, :SLEEPINGBAG, 1, _INTL("Este item prático vai permitir que você durma onde quiser. Você nem vai precisar mais de hotéis!")),
  QuestReward.new(30, :MISTSTONE, 1, _INTL("Esta pedra rara pode evoluir qualquer Pokémon, independentemente do nível ou método de evolução. Use com sabedoria!"), true),
  QuestReward.new(50, :GSBALL, 1, _INTL("Dizem que esta bola misteriosa é a chave para invocar o protetor de Ilex Forest. É uma relíquia preciosa.")),
  QuestReward.new(60, :MASTERBALL, 1, _INTL("Esta bola rara pode capturar qualquer Pokémon. Não desperdice!"), true),
]