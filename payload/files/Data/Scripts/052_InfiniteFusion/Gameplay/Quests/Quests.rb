def define_quest(quest_id,quest_type,quest_name,quest_description,quest_location,npc_sprite)
  case quest_type
  when :HOTEL_QUEST
    text_color = HotelQuestColor
  when :FIELD_QUEST
    text_color = FieldQuestColor
  when :LEGENDARY_QUEST
    text_color = LegendaryQuestColor
  when :ROCKET_QUEST
    text_color = TRQuestColor
  end
  new_quest = Quest.new(quest_id, quest_name, quest_description, npc_sprite, quest_location, quest_location, text_color)
  QUESTS[quest_id] = new_quest
end

QUESTS = {
    #Pokemart
    "pokemart_johto" => Quest.new("pokemart_johto", _INTL("Pokémon de Johto"), _INTL("Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Johto."), "traveler_johto", _INTL("Cerulean City"), HotelQuestColor),
    "pokemart_hoenn" => Quest.new("pokemart_hoenn", _INTL("Pokémon de Hoenn"), _INTL("Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Hoenn."), "traveler_hoenn", _INTL("Vermillion City"), HotelQuestColor),
    "pokemart_sinnoh" => Quest.new("pokemart_sinnoh", _INTL("Pokémon de Sinnoh"), _INTL("Um viajante no Department Center quer que você mostre a ele um Pokémon nativo da região de Sinnoh."), "traveler_sinnoh", _INTL("Celadon City"), HotelQuestColor),
    "pokemart_unova" => Quest.new( "pokemart_unova", _INTL("Pokémon de Unova"), _INTL("Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Unova."), "traveler_unova", _INTL("Fuchsia City"), HotelQuestColor),
    "pokemart_kalos" => Quest.new("pokemart_kalos", _INTL("Pokémon de Kalos"), _INTL("Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Kalos."), "traveler_kalos", _INTL("Saffron City"), HotelQuestColor),
    "pokemart_alola" => Quest.new("pokemart_alola", _INTL("Pokémon de Alola"), _INTL("Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Alola."), "traveler_alola", _INTL("Cinnabar Island"), HotelQuestColor),


    #Pewter hotel
    "pewter_1" => Quest.new("pewter_1", _INTL("Coleta de cogumelos"), _INTL("Uma senhora em Pewter City quer que você traga 3 TinyMushroom de Viridian Forest para fazer um ensopado."), "BW (74)", _INTL("Pewter City"), HotelQuestColor),
    "pewter_2" =>Quest.new("pewter_2", _INTL("Remédio perdido"), _INTL("Um Youngster em Pewter City precisa da sua ajuda para encontrar um Revive perdido. Ele perdeu o item ao se sentar em algum banco de Pewter City."), "BW (19)", _INTL("Pewter City"), HotelQuestColor),
    "pewter_3" =>Quest.new("pewter_3", _INTL("Evolução Bug "), _INTL("Um Bug Catcher em Pewter City quer que você mostre a ele um Pokémon Bug totalmente evoluído."), "BWBugCatcher_male", _INTL("Pewter City"), HotelQuestColor),
    "pewter_field_1" => Quest.new("pewter_field_1", _INTL("Jardim de néctar"), _INTL("Um senhor quer que você traga flores de cores diferentes para o jardim da cidade."),  "BW (039)", _INTL("Pewter City"), FieldQuestColor),
    "pewter_field_2" => Quest.new("pewter_field_2", _INTL("Eu escolho você!"), _INTL("Um Pikachu no PokéMart perdeu seu Pokémon League Hat oficial. Encontre um e entregue ao Pikachu!"),  "YOUNGSTER_LeagueHat", _INTL("Pewter City"), FieldQuestColor),
    "pewter_field_3" => Quest.new("pewter_field_3", _INTL("Âmbar pré-histórico!"), _INTL("Encontre um cientista em Viridian Forest para procurar âmbar pré-histórico."),  "BW (82)", _INTL("Pewter City"), FieldQuestColor),

    #Cerulean hotel
    "cerulean_1" => Quest.new("cerulean_1", _INTL("Cupido em ação"), _INTL("Um garoto em Cerulean City quer que você leve uma carta de amor para uma Pokémon Breeder chamada Maude. Ela provavelmente está em alguma das rotas perto de Cerulean City."), "BW (18)", _INTL("Cerulean City"), HotelQuestColor),
    "cerulean_2" => Quest.new("cerulean_2", _INTL("Especialistas de Tipo"), _INTL("Derrote todos os Type Experts espalhados pela região de Kanto ({1}/{2})",pbGet(VAR_TYPE_EXPERTS_BEATEN),TOTAL_NB_TYPE_EXPERTS), "expert-normal", _INTL("Cerulean City"), HotelQuestColor),

    #Route 24
    "cerulean_field_1" => Quest.new("cerulean_field_1", _INTL("Pesquisa de campo (Parte 1)"), _INTL("O assistente do Professor Oak quer que você capture um Abra."),  "BW (82)", _INTL("Route 24"), FieldQuestColor),
    "cerulean_field_2" => Quest.new("cerulean_field_2", _INTL("Pesquisa de campo (Parte 2)"), _INTL("O assistente do Professor Oak quer que você encontre todos os Pokémon da Route 24."),  "BW (82)", _INTL("Route 24"), FieldQuestColor),
    "cerulean_field_3" => Quest.new("cerulean_field_3", _INTL("Pesquisa de campo (Parte 3)"), _INTL("O assistente do Professor Oak quer que você capture um Buneary usando o Pokéradar."),  "BW (82)", _INTL("Route 24"), FieldQuestColor),

    #Vermillion City
    "vermillion_2" => Quest.new("vermillion_2", _INTL("Pescando uma sola"), _INTL("Um pescador quer que você pesque uma bota velha. Fisgue uma com a Old Rod em qualquer corpo d'água."), "BW (71)", _INTL("Cerulean City"), HotelQuestColor),
    "vermillion_1" => Quest.new("vermillion_1", _INTL("Tipos incomuns 1"), _INTL("Uma mulher no hotel quer que você mostre a ela um Pokémon tipo Water/Fire."), "BW (58)", _INTL("Vermillion City"), HotelQuestColor),
    "vermillion_3" => Quest.new("vermillion_3", _INTL("Coquetel de frutos do mar "), _INTL("Pegue algumas pernas de Krabby no vapor na cozinha do S.S. Anne e leve-as de volta ao hotel antes que esfriem."), "BW (36)", _INTL("Vermillion City"), HotelQuestColor),
    "vermillion_field_1" => Quest.new("vermillion_field_1", _INTL("Materiais de construção "), _INTL("Pegue algumas wooden planks em Viridian City e alguns Bricks em Pewter City."),  "BW (36)", _INTL("Vermillion City"), FieldQuestColor),
    "vermillion_field_2" => Quest.new("vermillion_field_2", _INTL("Garçom sobre as águas"), _INTL("O garçom do S.S. Anne quer que você anote os pedidos do restaurante enquanto ele vai buscar um bolo reserva."),  "BW (53)", _INTL("S.S. Anne"), FieldQuestColor),

    #Celadon City
    "celadon_1" => Quest.new("celadon_1", _INTL("Sol ou Lua"), _INTL("Mostre o Pokémon em que Eevee evolui ao ser exposto a uma Moon Stone ou Sun Stone para ajudar a cientista em sua pesquisa."), "BW (82)", _INTL("Celadon City"), HotelQuestColor),
    "celadon_2" => Quest.new("celadon_2", _INTL("Por quem os sinos dobram"), _INTL("Toque o sino de Lavender Town na hora certa para revelar seu segredo."), "BW (40)", _INTL("Lavender Town"), HotelQuestColor),
    "celadon_3" => Quest.new("celadon_3", _INTL("Ovo cozido"), _INTL("Uma mulher quer que você dê a ela um ovo para fazer uma omelete."), "BW (24)", _INTL("Celadon City"), HotelQuestColor),
    "celadon_field_1" => Quest.new("celadon_field_1", _INTL("Um passeio com Eevee!"), _INTL("Passeie com Eevee por um tempo até ele ficar cansado."),  "BW (37)", _INTL("Celadon City"), FieldQuestColor),

    #Fuchsia City
    "fuchsia_1" => Quest.new("fuchsia_1", _INTL("Corrida de bicicleta!"), _INTL("Encontre a Cyclist no fim da Route 17 e bata o tempo dela subindo a Cycling Road!"), "BW032", _INTL("Cycling Road"), HotelQuestColor),
    "fuchsia_2" => Quest.new("fuchsia_2", _INTL("Pokémon perdido!"), _INTL("Encontre o treinador da Chansey perdida!"), "113", _INTL("Fuchsia City"), HotelQuestColor),
    "fuchsia_3" => Quest.new("fuchsia_3", _INTL("Limpando a Cycling Road"), _INTL("Livre-se de todos os Pokémon que estão sujando a Cycling Road."), "BW (77)", _INTL("Fuchsia City"), HotelQuestColor),
    "fuchsia_4" => Quest.new("fuchsia_4", _INTL("Pokémon mordedor"), _INTL("Um pescador quer saber qual é o Pokémon de dentes afiados que o mordeu no lago da Safari Zone."), "BW (71)", _INTL("Fuchsia City"), HotelQuestColor),

    #Crimson City
    "crimson_1" => Quest.new("crimson_1", _INTL("Resgate dos Shellder"), _INTL("Coloque todos os Shellder encalhados de volta na água na rota para Crimson City."), "BW (48)", _INTL("Crimson City"), HotelQuestColor),
    "crimson_2" => Quest.new("crimson_2", _INTL("Batalha da quarta rodada"), _INTL("Derrote Jeanette e seu Bellsprout de nível alto em uma Pokémon Battle."), "BW024", _INTL("Crimson City"), HotelQuestColor),
    "crimson_3" => Quest.new("crimson_3", _INTL("Tipos incomuns 2"), _INTL("Uma mulher no hotel quer que você mostre a ela um Pokémon tipo Normal/Ghost."), "BW (58)", _INTL("Crimson City"), HotelQuestColor),
    "crimson_4" => Quest.new("crimson_4", _INTL("O topo da cachoeira"), _INTL("Alguém quer que você investigue o topo de uma cachoeira perto de Crimson City."), "BW (28)", _INTL("Crimson City"), HotelQuestColor),

    #Saffron City
    "saffron_1" => Quest.new("saffron_1", _INTL("Filhotes perdidos"), _INTL("Encontre todos os Growlithe desaparecidos nas rotas ao redor de Saffron City."), "BW (73)", _INTL("Saffron City"), HotelQuestColor),
    "saffron_2" => Quest.new("saffron_2", _INTL("Pokémon invisível"), _INTL("Encontre um Pokémon invisível na parte leste de Saffron City."), "BW (57)", _INTL("Saffron City"), HotelQuestColor),
    "saffron_3" => Quest.new("saffron_3", _INTL("Osso duro de roer!"), _INTL("Encontre um Rare Bone usando Rock Smash."), "BW (72)", _INTL("Saffron City"), HotelQuestColor),
    "saffron_field_1" => Quest.new("saffron_field_1", _INTL("Rainha da dança!"), _INTL("Dance com a Copycat Girl!"),  "BW (24)", _INTL("Saffron City (nightclub)"), FieldQuestColor),

    #Cinnabar Island
    "cinnabar_1" => Quest.new("cinnabar_1", _INTL("O Pokémon transformação"), _INTL("O cientista quer que você encontre Quick Powder, que às vezes pode ser encontrado com Ditto selvagens no porão da mansão."), "BW (82)", _INTL("Cinnabar Island"), HotelQuestColor),
    "cinnabar_2" => Quest.new("cinnabar_2", _INTL("Diamantes e pérolas"), _INTL("Encontre um Diamond Necklace para salvar o casamento do homem."), "BW (71)", _INTL("Cinnabar Island"), HotelQuestColor),
    "cinnabar_3" => Quest.new("cinnabar_3", _INTL("Artefato roubado"), _INTL("Recupere um vaso roubado de um ladrão na Pokémon Mansion."), "BW (21)", _INTL("Cinnabar Island"), HotelQuestColor),

    #Goldenrod City
    "goldenrod_1" => Quest.new( "goldenrod_1", _INTL("Lembrança da Safari!"), _INTL("Traga uma lembrança da Safari Zone de Fuchsia City."), "BW (28)", _INTL("Goldenrod City"), HotelQuestColor),
    "goldenrod_2" => Quest.new("goldenrod_2", _INTL("A floresta amaldiçoada"), _INTL("Uma criança quer que você encontre um tronco flutuante em Ilex Forest. Do que será que ela está falando?"), "BW109", _INTL("Goldenrod City"), HotelQuestColor),

    "goldenrod_police_1" => Quest.new("goldenrod_police_1", _INTL("Trabalho policial disfarçado!"), _INTL("Procure a polícia em Goldenrod City para ajudá-los em uma operação policial importante."),  "BW (80)", _INTL("Goldenrod City"), FieldQuestColor),
    "pinkan_police" => Quest.new("pinkan_police", _INTL("Pinkan Island!"), _INTL("A Team Rocket está planejando um assalto em Pinkan Island. Você uniu forças com a polícia para detê-los!"),  "BW (80)", _INTL("Goldenrod City"), FieldQuestColor),

    #Violet City
    "violet_1" => Quest.new("violet_1", _INTL("Desarme as pinhas!"), _INTL("Livre-se de todos os Pineco na Route 31 e na Route 30."), "BW (64)", _INTL("Violet City"), HotelQuestColor),
    "violet_2" => Quest.new("violet_2", _INTL("Encontre a SlowpokeTail!"), _INTL("Encontre uma SlowpokeTail em algumas flores, em algum lugar por Violet City!"), "BW (19)", _INTL("Violet City"), HotelQuestColor),

    #Blackthorn City
    "blackthorn_1" => Quest.new( "blackthorn_1", _INTL("Evolução Dragon"), _INTL("Uma Dragon Tamer em Blackthorn City quer que você mostre a ela um Pokémon Dragon totalmente evoluído."), "BW014", _INTL("Blackthorn City"), HotelQuestColor),
    "blackthorn_2" => Quest.new("blackthorn_2", _INTL("Tesouro afundado!"), _INTL("Encontre uma memorabilia antiga em um navio afundado perto de Cinnabar Island."), "BW (28)", _INTL("Blackthorn City"), HotelQuestColor),
    "blackthorn_3" => Quest.new("blackthorn_3", _INTL("A maior carpa"), _INTL("Um pescador quer que você pesque um Magikarp de nível excepcionalmente alto em Dragon's Den."), "BW (71)", _INTL("Blackthorn City"), HotelQuestColor),

    #Ecruteak City
    "ecruteak_1" => Quest.new("ecruteak_1", _INTL("Evolução Ghost"), _INTL("Uma garota em Ecruteak City quer que você mostre a ela um Pokémon Ghost totalmente evoluído."), "BW014", _INTL("Ecruteak City"), HotelQuestColor),

    #Kin Island
    "kin_1" => Quest.new("kin_1", _INTL("Banana Slamma!"), _INTL("Colete 30 bananas."), "BW059", _INTL("Kin Island"), HotelQuestColor),
    "kin_2" => Quest.new("kin_2", _INTL("Meteoro caído"), _INTL("Investigue uma cratera perto de Bond Bridge."), "BW009", _INTL("Kin Island"), HotelQuestColor),
    "kin_field_1" => Quest.new("kin_field_1", _INTL("O peixe mais raro"), _INTL("Um pescador quer que você mostre a ele um Feebas. Pelo visto, eles podem ser pescados pelas Sevii Islands quando chove."),  "BW056", _INTL("Kin Island"), FieldQuestColor),

    "legendary_deoxys_1" => Quest.new("legendary_deoxys_1", _INTL("Primeiro contato"), _INTL("Encontre as peças desaparecidas de uma nave alienígena caída."), "BW (92)", _INTL("Bond Bridge"), LegendaryQuestColor),
    "legendary_deoxys_2" => Quest.new("legendary_deoxys_2", _INTL("Primeiro contato (Parte 2)"), _INTL("Peça ao marinheiro no porto de Cinnabar Island para levar você até a ilha inexplorada onde a nave pode estar."), "BW (92)", _INTL("Bond Bridge"), LegendaryQuestColor),

    #Necrozma quest
    "legendary_necrozma_1" => Quest.new("legendary_necrozma_1", _INTL("Prismas misteriosos"), _INTL("Você encontrou um pedestal com um prisma misterioso. Parece haver espaço para mais prismas."), "BW_Sabrina", _INTL("Pokémon Tower"), LegendaryQuestColor),
    "legendary_necrozma_2" => Quest.new("legendary_necrozma_2", _INTL("A longa noite (Parte 1)"), _INTL("Uma escuridão misteriosa envolveu parte da região. Encontre Sabrina do lado de fora do portão oeste de Saffron City para investigar."), "BW_Sabrina", _INTL("Lavender Town"), LegendaryQuestColor),
    "legendary_necrozma_3" => Quest.new("legendary_necrozma_1", _INTL("A longa noite (Parte 2)"), _INTL("A escuridão misteriosa se expandiu. Encontre Sabrina no topo do Dept. Store de Celadon City para descobrir a origem da escuridão."), "BW_Sabrina", _INTL("Route 7"), LegendaryQuestColor),
    "legendary_necrozma_4" => Quest.new("legendary_necrozma_4", _INTL("A longa noite (Parte 3)"), _INTL("Fuchsia City parece não ter sido afetada pela escuridão. Investigue para ver se consegue descobrir mais informações."), "BW_Sabrina", _INTL("Celadon City"), LegendaryQuestColor),
    "legendary_necrozma_5" => Quest.new("legendary_necrozma_5", _INTL("A longa noite (Parte 4)"), _INTL("A escuridão misteriosa se expandiu de novo e plantas estranhas apareceram. Siga as plantas para ver aonde elas levam."), "BW_koga", _INTL("Fuchsia City"), LegendaryQuestColor),
    "legendary_necrozma_6" => Quest.new("legendary_necrozma_6", _INTL("A longa noite (Parte 5)"), _INTL("Você encontrou uma fruta estranha que parece estar ligada à escuridão misteriosa. Procure o Professor Oak para analisá-la."), "BW029", _INTL("Safari Zone"), LegendaryQuestColor),
    "legendary_necrozma_7" => Quest.new("legendary_necrozma_7", _INTL("A longa noite (Parte 6)"), _INTL("A planta estranha que você encontrou parece brilhar na escuridão misteriosa que agora cobre toda a região. Tente seguir o brilho para encontrar a origem da perturbação."), "BW-oak", _INTL("Pallet Town"), LegendaryQuestColor),


    "legendary_meloetta_1" => Quest.new("legendary_meloetta_1", _INTL("Uma banda lendária (Parte 1)"), _INTL("O vocalista de uma banda em Saffron City quer que você ajude a recrutar um baterista. Eles acham que ouviram uma bateria por Crimson City..."), "BW107", _INTL("Saffron City"), LegendaryQuestColor),
    "legendary_meloetta_2" => Quest.new("legendary_meloetta_2", _INTL("Uma banda lendária (Parte 2)"), _INTL("O baterista de uma Pokéband lendária quer que você encontre os antigos integrantes. O empresário da banda falou sobre dois ex-guitarristas..."), "band_drummer", _INTL("Saffron City"), LegendaryQuestColor),
    "legendary_meloetta_3" => Quest.new("legendary_meloetta_3", _INTL("Uma banda lendária (Parte 3)"), _INTL("O baterista de uma Pokéband lendária quer que você encontre os antigos integrantes. Há rumores sobre uma música estranha ouvida pela região."), "band_drummer", _INTL("Saffron City"), LegendaryQuestColor),
    "legendary_meloetta_4" => Quest.new("legendary_meloetta_4", _INTL("Uma banda lendária (Parte 4)"), _INTL("Você reuniu a banda inteira! Venha assistir ao show no sábado à noite."), "BW117", _INTL("Saffron City"), LegendaryQuestColor),

    "legendary_cresselia_1" => Quest.new(61, _INTL("Lunar Feathers misteriosas"), _INTL("Uma entidade misteriosa pediu que você coletasse Lunar Feathers para ela. Ela disse que virá à noite para dizer onde procurar. Seja lá quem for..."), "lunarFeather", _INTL("Lavender Town"), LegendaryQuestColor),
    #removed
    #11 => Quest.new(11, "Powering the Lighthouse", "Catch some Voltorb to power up the lighthouse", QuestBranchHotels, "BW (43)", "Vermillion City", HotelQuestColor),
}

###################
# HOENN QUESTS   ##
# ################

#route 102
define_quest("route_102_rematch",:FIELD_QUEST,_INTL("Revanche de Trainers"), _INTL("Uma Lass que batalhou com você quer mudar o time e pedir revanche!"),_INTL("Route 102"),"NPC_Hoenn_Lass")

    #Route 116
define_quest("route116_glasses",:FIELD_QUEST,_INTL("Óculos perdidos"), _INTL("Um Trainer perdeu os óculos. Ajude-o a encontrá-los!"),_INTL("Route 116"),"NPC_Hoenn_BugManiac")

#Route 104 (South)
define_quest("route104_rivalWeather",:FIELD_QUEST,_INTL("Observação do clima"), _INTL("Ajude seu rival com trabalho de campo e encontre um Pokémon que só aparece quando está ventando!"),_INTL("Route 104"),"rival")

#Petalburg woods
define_quest("petalburgwoods_spores",:FIELD_QUEST,_INTL("Colheita de esporos"), _INTL("Um cientista encarregou você de coletar 4 amostras de esporos dos cogumelos grandes encontrados na floresta!"),_INTL("Petalburg Woods"),"NPC_Hoenn_Scientist")

#Route 104 (North)
define_quest("route104_oricorio",:FIELD_QUEST,_INTL("Grama florida especial"), _INTL("Encontre um Oricorio na grama florida atrás da floricultura."),_INTL("Route 104"),"NPC_Hoenn_AromaLady")
define_quest("route104_oricorio_forms",:FIELD_QUEST,_INTL("Flores de néctar"), _INTL("Encontre todos os 4 tipos de flores de néctar para transformar Oricorio."),_INTL("Route 104"),"NPC_Hoenn_AromaLady")

#Route 115
define_quest("route115_secretBase",:FIELD_QUEST,_INTL("Sua própria Secret Base!"), _INTL("Fale com Aarune perto da secret base dele para aprender a criar a sua."),_INTL("Route 115"),"NPC_Hoenn_AromaLady")

#Rustboro
define_quest("rustboro_whismur",:FIELD_QUEST,_INTL("Aumentador de volume!"), _INTL("Encontre um Wingull para fundir com um Whismur e deixá-lo mais alto."),_INTL("Rustboro City"),"NPC_schoolgirl")
