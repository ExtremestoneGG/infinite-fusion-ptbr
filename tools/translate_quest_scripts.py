from __future__ import annotations

import re
import shutil
from pathlib import Path


GAME_ROOT = Path(r"D:\InfiniteFusion - Copia")
REPO_ROOT = GAME_ROOT / "ptbr-release"
SOURCE_ROOT = GAME_ROOT / "PTBR_BACKUPS" / "quest_scripts_pre_ptbr_20260619_065651"
PAYLOAD_ROOT = REPO_ROOT / "payload" / "files" / "Data" / "Scripts" / "052_InfiniteFusion" / "Gameplay" / "Quests"
LIVE_ROOT = GAME_ROOT / "Data" / "Scripts" / "052_InfiniteFusion" / "Gameplay" / "Quests"


FIXES: list[tuple[str, str]] = [
    # Quest log UI and reward flow.
    (r"Hotel Quests", "Quests de hotel"),
    (r"Field Quests", "Quests de campo"),
    (r"Team Rocket Quests", "Quests da Team Rocket"),
    (r"Legendary Quests", "Quests lendárias"),
    (r"\\C\[6\]NEW QUEST: ", r"\\C[6]NOVA QUEST: "),
    (r"\\qp\\C\[6\]Quest completed!", r"\\qp\\C[6]Quest concluída!"),
    (r"Quest Log", "Diário de quests"),
    (r"Ongoing: ", "Ativas: "),
    (r"Completed: ", "Concluídas: "),
    (r"Not Completed", "Pendente"),
    (r"Completed", "Concluída"),
    (r"Quest received in:", "Recebida em:"),
    (r"Quest received at:", "Recebida às:"),
    (r"Quest received from:", "Recebida de:"),
    (r"From \{1\}", "De {1}"),
    (r"From ", "De "),
    (r"Ongoing Quests", "Quests ativas"),
    (r"Concluída Quests", "Quests concluídas"),
    (r"Completed Quests", "Quests concluídas"),
    (r"No ongoing quests", "Nenhuma quest ativa"),
    (r"No completed quests", "Nenhuma quest concluída"),
    (r"Accepted quests: \\C\[1\]\{1\}", r"Quests aceitas: \\C[1]{1}"),
    (r"Completed quests: \\C\[1\]\{1\}", r"Quests concluídas: \\C[1]{1}"),
    (r"In-progress: \\C\[1\]\{1\}", r"Em andamento: \\C[1]{1}"),
    (r"Also, there's one more thing\.\.\.", "Ah, e tem mais uma coisa..."),
    (
        r"As a gift for having helped so many people, I want to give you this\.",
        "Como presente por ter ajudado tanta gente, quero te dar isto.",
    ),
    (
        r"I have no more rewards to give you! Thanks for helping all these people!",
        "Não tenho mais recompensas para te dar! Obrigado por ajudar tanta gente!",
    ),
    (r"Help \{1\} more person and I'll give you something good!", "Ajude mais {1} pessoa e eu te darei algo bom!"),
    (r"Help \{1\} more people and I'll give you something good!", "Ajude mais {1} pessoas e eu te darei algo bom!"),

    # Quest reward descriptions.
    (
        r"This HM will allow you to illuminate dark caves and should help you to progress in your journey!",
        "Este HM vai permitir iluminar cavernas escuras e deve ajudar você a avançar na sua jornada!",
    ),
    (
        r"This item will allows you to get twice the money in a battle if the Pokémon holding it took part in it!",
        "Este item permite ganhar o dobro de dinheiro em uma batalha se o Pokémon que estiver segurando ele participar!",
    ),
    (
        r"This will allow you to illuminate caves without having to use a HM! Practical, isn't it\?",
        "Isto vai permitir iluminar cavernas sem precisar usar um HM! Prático, não é?",
    ),
    (
        r"This strange cable triggers the evolution of Pokémon that typically evolve via trade\. I know you'll put it to good use!",
        "Este cabo estranho ativa a evolução de Pokémon que normalmente evoluem por troca. Sei que você vai usar bem!",
    ),
    (
        r"This handy item will allow you to sleep anywhere you want\. You won't even need hotels anymore!",
        "Este item prático vai permitir que você durma onde quiser. Você nem vai precisar mais de hotéis!",
    ),
    (
        r"This rare stone can evolve any Pokémon, regardless of their level or evolution method\. Use it wisely!",
        "Esta pedra rara pode evoluir qualquer Pokémon, independentemente do nível ou método de evolução. Use com sabedoria!",
    ),
    (
        r"This mysterious ball is rumored to be the key to call upon the protector of Ilex Forest\.  It's a precious relic\.",
        "Dizem que esta bola misteriosa é a chave para invocar o protetor de Ilex Forest. É uma relíquia preciosa.",
    ),
    (r"This rare ball can catch any Pokémon\. Don't waste it!", "Esta bola rara pode capturar qualquer Pokémon. Não desperdice!"),

    # Quest definitions.
    (r"Johto Pokémon", "Pokémon de Johto"),
    (
        r"A traveler in the PokéMart wants you to show him a Pokémon native to the Johto region\.",
        "Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Johto.",
    ),
    (r"Hoenn Pokémon", "Pokémon de Hoenn"),
    (
        r"A traveler in the PokéMart you to show him a Pokémon native to the Hoenn region\.",
        "Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Hoenn.",
    ),
    (r"Sinnoh Pokémon", "Pokémon de Sinnoh"),
    (
        r"A traveler in the Department Center wants you to show him a Pokémon native to the Sinnoh region\.",
        "Um viajante no Department Center quer que você mostre a ele um Pokémon nativo da região de Sinnoh.",
    ),
    (r"Unova Pokémon", "Pokémon de Unova"),
    (
        r"A traveler in the PokéMart wants you to show him a Pokémon native to the Unova region\.",
        "Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Unova.",
    ),
    (r"Kalos Pokémon", "Pokémon de Kalos"),
    (
        r"A traveler in the PokéMart wants you to show him a Pokémon native to the Kalos region\.",
        "Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Kalos.",
    ),
    (r"Alola Pokémon", "Pokémon de Alola"),
    (
        r"A traveler in the PokéMart wants you to show him a Pokémon native to the Alola region\.",
        "Um viajante no PokéMart quer que você mostre a ele um Pokémon nativo da região de Alola.",
    ),
    (r"Mushroom Gathering", "Coleta de cogumelos"),
    (
        r"A lady in Pewter City wants you to bring her 3 TinyMushroom from Viridian Forest to make a stew\.",
        "Uma senhora em Pewter City quer que você traga 3 TinyMushroom de Viridian Forest para fazer um ensopado.",
    ),
    (r"Lost Medicine", "Remédio perdido"),
    (
        r"A youngster in Pewter City needs your help to find a lost Revive\. He lost it by sitting on a bench somewhere in Pewter City\.",
        "Um Youngster em Pewter City precisa da sua ajuda para encontrar um Revive perdido. Ele perdeu o item ao se sentar em algum banco de Pewter City.",
    ),
    (r"Bug Evolution ", "Evolução Bug "),
    (
        r"A Bug Catcher in Pewter City wants you to show him a fully-evolved Bug Pokémon\.",
        "Um Bug Catcher em Pewter City quer que você mostre a ele um Pokémon Bug totalmente evoluído.",
    ),
    (r"Nectar garden", "Jardim de néctar"),
    (r"An old man wants you to bring differently colored flowers for the city's garden\.", "Um senhor quer que você traga flores de cores diferentes para o jardim da cidade."),
    (r"I Choose You!", "Eu escolho você!"),
    (
        r"A Pikachu in the PokéMart has lost its official Pokémon League Hat\. Find one and give it to the Pikachu!",
        "Um Pikachu no PokéMart perdeu seu Pokémon League Hat oficial. Encontre um e entregue ao Pikachu!",
    ),
    (r"Prehistoric Amber!", "Âmbar pré-histórico!"),
    (r"Meetup with a scientist in Viridian Forest to look for prehistoric amber\.", "Encontre um cientista em Viridian Forest para procurar âmbar pré-histórico."),
    (r"Playing Cupid", "Cupido em ação"),
    (
        r"A boy in Cerulean City wants you bring a love letter to a Pokémon Breeder named Maude\. She's probably somewhere in one of the routes near Cerulean City",
        "Um garoto em Cerulean City quer que você leve uma carta de amor para uma Pokémon Breeder chamada Maude. Ela provavelmente está em alguma das rotas perto de Cerulean City.",
    ),
    (r"Type Experts", "Especialistas de Tipo"),
    (r"Defeat all of the Especialistas de Tipo scattered around the Kanto region \(\{1\}/\{2\}\)", "Derrote todos os Type Experts espalhados pela região de Kanto ({1}/{2})"),
    (r"Defeat all of the Type Experts scattered around the Kanto region \(\{1\}/\{2\}\)", "Derrote todos os Type Experts espalhados pela região de Kanto ({1}/{2})"),
    (r"Field Research \(Part 1\)", "Pesquisa de campo (Parte 1)"),
    (r"Professor Oak's aide wants you to catch an Abra\.", "O assistente do Professor Oak quer que você capture um Abra."),
    (r"Field Research \(Part 2\)", "Pesquisa de campo (Parte 2)"),
    (r"Professor Oak's aide wants you to encounter every Pokémon on Route 24\.", "O assistente do Professor Oak quer que você encontre todos os Pokémon da Route 24."),
    (r"Field Research \(Part 3\)", "Pesquisa de campo (Parte 3)"),
    (r"Professor Oak's aide wants you to catch a Buneary using the Pokéradar\.", "O assistente do Professor Oak quer que você capture um Buneary usando o Pokéradar."),
    (r"Fishing for Sole", "Pescando uma sola"),
    (r"A fisherman wants you to fish up an old boot\. Hook it up with the old rod in any body of water\.", "Um pescador quer que você pesque uma bota velha. Fisgue uma com a Old Rod em qualquer corpo d'água."),
    (r"Unusual Types 1", "Tipos incomuns 1"),
    (r"A woman at the hotel wants you to show her a Water/Fire-type Pokémon", "Uma mulher no hotel quer que você mostre a ela um Pokémon tipo Water/Fire."),
    (r"Seafood Cocktail ", "Coquetel de frutos do mar "),
    (
        r"Get some steamed Krabby legs from the S\.S\. Anne's kitchen and bring them back to the hotel before they get cold",
        "Pegue algumas pernas de Krabby no vapor na cozinha do S.S. Anne e leve-as de volta ao hotel antes que esfriem.",
    ),
    (r"Building Materials ", "Materiais de construção "),
    (r"Get some wooden planks from Viridian City and some Bricks from Pewter City\.", "Pegue algumas wooden planks em Viridian City e alguns Bricks em Pewter City."),
    (r"Waiter on the Water", "Garçom sobre as águas"),
    (r"The S\.S\. Anne waiter wants you to take restaurant orders while he went to get a replacement cake\.", "O garçom do S.S. Anne quer que você anote os pedidos do restaurante enquanto ele vai buscar um bolo reserva."),
    (r"Sun or Moon", "Sol ou Lua"),
    (
        r"Show the Pokémon that Eevee evolves when exposed to a Moon or Sun stone to help the scientist with her research\.",
        "Mostre o Pokémon em que Eevee evolui ao ser exposto a uma Moon Stone ou Sun Stone para ajudar a cientista em sua pesquisa.",
    ),
    (r"For Whom the Bell Tolls", "Por quem os sinos dobram"),
    (r"Ring Lavender Town's bell when the time is right to reveal its secret\.", "Toque o sino de Lavender Town na hora certa para revelar seu segredo."),
    (r"Hardboiled", "Ovo cozido"),
    (r"A lady wants you to give her an egg to make an omelette\.", "Uma mulher quer que você dê a ela um ovo para fazer uma omelete."),
    (r"A stroll with Eevee!", "Um passeio com Eevee!"),
    (r"Walk Eevee around for a while until it gets tired\.", "Passeie com Eevee por um tempo até ele ficar cansado."),
    (r"Bicycle Race!", "Corrida de bicicleta!"),
    (r"Go meet the Cyclist at the bottom of Route 17 and beat her time up the Cycling Road!", "Encontre a Cyclist no fim da Route 17 e bata o tempo dela subindo a Cycling Road!"),
    (r"Lost Pokémon!", "Pokémon perdido!"),
    (r"Find the lost Chansey's trainer!", "Encontre o treinador da Chansey perdida!"),
    (r"Cleaning up the Cycling Road", "Limpando a Cycling Road"),
    (r"Get rid of all the Pokémon dirtying up the Cycling Road\.", "Livre-se de todos os Pokémon que estão sujando a Cycling Road."),
    (r"Bitey Pokémon", "Pokémon mordedor"),
    (
        r"A fisherman wants to know what is the sharp-toothed Pokémon that bit him in the Safari Zone's lake\.",
        "Um pescador quer saber qual é o Pokémon de dentes afiados que o mordeu no lago da Safari Zone.",
    ),
    (r"Shellfish Rescue", "Resgate dos Shellder"),
    (r"Put all the stranded Shellders back in the water on the route to Crimson City\.", "Coloque todos os Shellder encalhados de volta na água na rota para Crimson City."),
    (r"Fourth Round Rumble", "Batalha da quarta rodada"),
    (r"Defeat Jeanette and her high-level Bellsprout in a Pokémon Battle", "Derrote Jeanette e seu Bellsprout de nível alto em uma Pokémon Battle."),
    (r"Unusual Types 2", "Tipos incomuns 2"),
    (r"A woman at the hotel wants you to show her a Normal/Ghost-type Pokémon", "Uma mulher no hotel quer que você mostre a ela um Pokémon tipo Normal/Ghost."),
    (r"The Top of the Waterfall", "O topo da cachoeira"),
    (r"Someone wants you to go investigate the top of a waterfall near Crimson City", "Alguém quer que você investigue o topo de uma cachoeira perto de Crimson City."),
    (r"Lost Puppies", "Filhotes perdidos"),
    (r"Find all of the missing Growlithe in the routes around Saffron City\.", "Encontre todos os Growlithe desaparecidos nas rotas ao redor de Saffron City."),
    (r"Invisible Pokémon", "Pokémon invisível"),
    (r"Find an invisible Pokémon in the eastern part of Saffron City\.", "Encontre um Pokémon invisível na parte leste de Saffron City."),
    (r"Bad to the Bone!", "Osso duro de roer!"),
    (r"Find a Rare Bone using Rock Smash\.", "Encontre um Rare Bone usando Rock Smash."),
    (r"Dancing Queen!", "Rainha da dança!"),
    (r"Dance with the Copycat Girl!", "Dance com a Copycat Girl!"),
    (r"The transformation Pokémon", "O Pokémon transformação"),
    (
        r"The scientist wants you to find some Quick Powder that can sometimes be found with wild Ditto in the mansion's basement\.",
        "O cientista quer que você encontre Quick Powder, que às vezes pode ser encontrado com Ditto selvagens no porão da mansão.",
    ),
    (r"Diamonds and Pearls", "Diamantes e pérolas"),
    (r"Find a Diamond Necklace to save the man's marriage\.", "Encontre um Diamond Necklace para salvar o casamento do homem."),
    (r"Stolen artifact", "Artefato roubado"),
    (r"Recover a stolen vase from a burglar in the Pokémon Mansion", "Recupere um vaso roubado de um ladrão na Pokémon Mansion."),
    (r"Safari Souvenir!", "Lembrança da Safari!"),
    (r"Bring back a souvenir from the Fuchsia City Safari Zone", "Traga uma lembrança da Safari Zone de Fuchsia City."),
    (r"The Cursed Forest", "A floresta amaldiçoada"),
    (r"A child wants you to find a floating tree stump in Ilex Forest\. What could she be talking about\?", "Uma criança quer que você encontre um tronco flutuante em Ilex Forest. Do que será que ela está falando?"),
    (r"Undercover police work!", "Trabalho policial disfarçado!"),
    (r"Go see the police in Goldenrod City to help them with an important police operation\.", "Procure a polícia em Goldenrod City para ajudá-los em uma operação policial importante."),
    (r"Team Rocket is planning a heist on Pinkan Island\. You joined forces with the police to stop them!", "A Team Rocket está planejando um assalto em Pinkan Island. Você uniu forças com a polícia para detê-los!"),
    (r"Defuse the Pinecones!", "Desarme as pinhas!"),
    (r"Get rid of all the Pineco on Route 31 and Route 30", "Livre-se de todos os Pineco na Route 31 e na Route 30."),
    (r"Find Slowpoke's Tail!", "Encontre a SlowpokeTail!"),
    (r"Find a SlowpokeTail in some flowers, somewhere around Violet City!", "Encontre uma SlowpokeTail em algumas flores, em algum lugar por Violet City!"),
    (r"Dragon Evolution", "Evolução Dragon"),
    (r"A Dragon Tamer in Blackthorn City wants you to show her a fully-evolved Dragon Pokémon\.", "Uma Dragon Tamer em Blackthorn City quer que você mostre a ela um Pokémon Dragon totalmente evoluído."),
    (r"Sunken Treasure!", "Tesouro afundado!"),
    (r"Find an old memorabilia on a sunken ship near Cinnabar Island\.", "Encontre uma memorabilia antiga em um navio afundado perto de Cinnabar Island."),
    (r"The Largest Carp", "A maior carpa"),
    (r"A fisherman wants you to fish up a Magikarp that's exceptionally high-level at Dragon's Den\.", "Um pescador quer que você pesque um Magikarp de nível excepcionalmente alto em Dragon's Den."),
    (r"Ghost Evolution", "Evolução Ghost"),
    (r"A girl in Ecruteak City wants you to show her a fully-evolved Ghost Pokémon\.", "Uma garota em Ecruteak City quer que você mostre a ela um Pokémon Ghost totalmente evoluído."),
    (r"Collect 30 bananas", "Colete 30 bananas."),
    (r"Fallen Meteor", "Meteoro caído"),
    (r"Investigate a crater near Bond Bridge\.", "Investigue uma cratera perto de Bond Bridge."),
    (r"The rarest fish", "O peixe mais raro"),
    (r"A fisherman wants you to show him a Feebas\. Apparently they can be fished around the Sevii Islands when it rains\.", "Um pescador quer que você mostre a ele um Feebas. Pelo visto, eles podem ser pescados pelas Sevii Islands quando chove."),
    (r"First Contact", "Primeiro contato"),
    (r"First Contact \(Part 2\)", "Primeiro contato (Parte 2)"),
    (r"Find the missing pieces of a fallen alien spaceship", "Encontre as peças desaparecidas de uma nave alienígena caída."),
    (r"Ask the sailor at Cinnabar Island's harbour to take you to the uncharted island where the spaceship might be located", "Peça ao marinheiro no porto de Cinnabar Island para levar você até a ilha inexplorada onde a nave pode estar."),
    (r"Mysterious prisms", "Prismas misteriosos"),
    (r"You found a pedestal with a mysterious prism on it\. There seems to be room for more prisms\.", "Você encontrou um pedestal com um prisma misterioso. Parece haver espaço para mais prismas."),
    (r"The long night \(Part 1\)", "A longa noite (Parte 1)"),
    (r"The long night \(Part 2\)", "A longa noite (Parte 2)"),
    (r"The long night \(Part 3\)", "A longa noite (Parte 3)"),
    (r"The long night \(Part 4\)", "A longa noite (Parte 4)"),
    (r"The long night \(Part 5\)", "A longa noite (Parte 5)"),
    (r"The long night \(Part 6\)", "A longa noite (Parte 6)"),
    (r"A mysterious darkness has shrouded some of the region\. Meet Sabrina outside of Saffron City's western gate to investigate\.", "Uma escuridão misteriosa envolveu parte da região. Encontre Sabrina do lado de fora do portão oeste de Saffron City para investigar."),
    (r"The mysterious darkness has expended\. Meet Sabrina on top of Celadon City's Dept\. Store to figure out the source of the darkness\.", "A escuridão misteriosa se expandiu. Encontre Sabrina no topo do Dept. Store de Celadon City para descobrir a origem da escuridão."),
    (r"Fuchsia City appears to be unaffected by the darkness\. Go investigate to see if you can find out more information\.", "Fuchsia City parece não ter sido afetada pela escuridão. Investigue para ver se consegue descobrir mais informações."),
    (r"The mysterious darkness has expended yet again and strange plants have appeared\. Follow the plants to see where they lead\.", "A escuridão misteriosa se expandiu de novo e plantas estranhas apareceram. Siga as plantas para ver aonde elas levam."),
    (r"You found a strange fruit that appears to be related to the mysterious darkness\. Go see professor Oak to have it analyzed\.", "Você encontrou uma fruta estranha que parece estar ligada à escuridão misteriosa. Procure o Professor Oak para analisá-la."),
    (r"The strange plant you found appears to glow in the mysterious darkness that now covers the entire region\. Try to follow the glow to find out the source of the disturbance\.", "A planta estranha que você encontrou parece brilhar na escuridão misteriosa que agora cobre toda a região. Tente seguir o brilho para encontrar a origem da perturbação."),
    (r"A legendary band \(Part 1\)", "Uma banda lendária (Parte 1)"),
    (r"A legendary band \(Part 2\)", "Uma banda lendária (Parte 2)"),
    (r"A legendary band \(Part 3\)", "Uma banda lendária (Parte 3)"),
    (r"A legendary band \(Part 4\)", "Uma banda lendária (Parte 4)"),
    (r"The singer of a band in Saffron City wants you to help them recruit a drummer\. They think they've heard some drumming around Crimson City\.\.\.", "O vocalista de uma banda em Saffron City quer que você ajude a recrutar um baterista. Eles acham que ouviram uma bateria por Crimson City..."),
    (r"The drummer from a legendary Pokéband wants you to find its former bandmates\. The band manager talked about two former guitarists\.\.\.", "O baterista de uma Pokéband lendária quer que você encontre os antigos integrantes. O empresário da banda falou sobre dois ex-guitarristas..."),
    (r"The drummer from a legendary Pokéband wants you to find its former bandmates\. There are rumors about strange music that was heard around the region\.", "O baterista de uma Pokéband lendária quer que você encontre os antigos integrantes. Há rumores sobre uma música estranha ouvida pela região."),
    (r"You assembled the full band! Come watch the show on Saturday night\.", "Você reuniu a banda inteira! Venha assistir ao show no sábado à noite."),
    (r"Mysterious Lunar feathers", "Lunar Feathers misteriosas"),
    (r"A mysterious entity asked you to collect Lunar Feathers for them\. It said that they will come at night to tell you where to look\. Whoever that may be\.\.\.", "Uma entidade misteriosa pediu que você coletasse Lunar Feathers para ela. Ela disse que virá à noite para dizer onde procurar. Seja lá quem for..."),
    (r"Trainer Rematches", "Revanche de Trainers"),
    (r"A lass you battled wants to switch up her team and rematch you!", "Uma Lass que batalhou com você quer mudar o time e pedir revanche!"),
    (r"Lost glasses", "Óculos perdidos"),
    (r"A trainer has lost their glasses, help him find them!", "Um Trainer perdeu os óculos. Ajude-o a encontrá-los!"),
    (r"Weather Watch", "Observação do clima"),
    (r"Help your rival with fieldwork and find a Pokémon that only appears when it's windy!", "Ajude seu rival com trabalho de campo e encontre um Pokémon que só aparece quando está ventando!"),
    (r"Spores Harvest", "Colheita de esporos"),
    (r"A scientist has tasked you to collect 4 spore samples from the large mushrooms that can be found in the woods!", "Um cientista encarregou você de coletar 4 amostras de esporos dos cogumelos grandes encontrados na floresta!"),
    (r"Special Flowery Grass", "Grama florida especial"),
    (r"Find an Oricorio in the flowery grass behind the flower shop\.", "Encontre um Oricorio na grama florida atrás da floricultura."),
    (r"Nectar Flowers", "Flores de néctar"),
    (r"Find all 4 types of nectar flowers to transform Oricorio\.", "Encontre todos os 4 tipos de flores de néctar para transformar Oricorio."),
    (r"Your Very Own Secret Base!", "Sua própria Secret Base!"),
    (r"Talk to Aarune near his secret base to learn how to make your own\.", "Fale com Aarune perto da secret base dele para aprender a criar a sua."),
    (r"Volume Booster!", "Aumentador de volume!"),
    (r"Find a Wingull to fuse with a Whismur to make it louder\.", "Encontre um Wingull para fundir com um Whismur e deixá-lo mais alto."),

    # Darkrai / Cresselia clues.
    (r"Find the first feather in the northernmost dwelling in the port of exquisite sunsets\.\.\.", "Encontre a primeira pena na moradia mais ao norte do porto dos pores do sol requintados..."),
    (r"Amidst a nursery for Pokémon youngsters, the second feather hides, surrounded by innocence\.", "Em meio a um berçário para jovens Pokémon, a segunda pena se esconde, cercada de inocência."),
    (r"Find the next one in the inn where water meets rest", "Encontre a próxima na hospedaria onde a água encontra o descanso."),
    (r"Find the next one inside the lone house in the city at the edge of civilization\.", "Encontre a próxima dentro da casa solitária na cidade à beira da civilização."),
    (r"The final feather lies back in the refuge for orphaned Pokémon\.\.\.", "A pena final está de volta no refúgio dos Pokémon órfãos..."),
    (r"Lie in the bed\.\.\. Bring me the feathers\.\.\.", "Deite-se na cama... Traga-me as penas..."),
    (r"Eevee is getting tired\. You should head back soon!", "Eevee está ficando cansado. É melhor voltar logo!"),
    (r"\{1\}'s \{2\}", "{2} de {1}"),

    # Team Rocket quests and related dialogue.
    (r"\\C\[\{1\}\]NEW MISSION: ", r"\\C[{1}]NOVA MISSÃO: "),
    (r"Mission completed!", "Missão concluída!"),
    (r"Mission Failed\.\.\.", "Missão falhou..."),
    (r"Creepy Crawlies", "Insetos rastejantes"),
    (r"The Team Rocket Captain has tasked you with clearing the bug infestation in the temporary Rocket HQ in Cerulean City", "O Capitão da Team Rocket encarregou você de acabar com a infestação de insetos no Rocket HQ temporário em Cerulean City."),
    (r"No Fishing Zone", "Zona sem pesca"),
    (r"Intimidate the fishermen at Nugget Bridge until they leave the area\.", "Intimide os pescadores na Nugget Bridge até eles irem embora da área."),
    (r"Disobedient Pokémon", "Pokémon desobediente"),
    (r"Bring back the Pokémon given by the Team Rocket Captain fainted to teach it a lesson\.", "Traga de volta desmaiado o Pokémon dado pelo Capitão da Team Rocket para ensinar uma lição a ele."),
    (r"Follow Petrel and go steal a rare Pokémon from a young girl\.", "Siga Petrel e roube um Pokémon raro de uma garotinha."),
    (r"Supplying the new grunts", "Abastecendo os novos grunts"),
    (r"Catch 4 Pokémon with Rocket Balls in the outskirts of Celadon City\.", "Capture 4 Pokémon com Rocket Balls nos arredores de Celadon City."),
    (r"Interception!", "Interceptação!"),
    (r"Intercept the TMs shipment to the Celadon Store and pose as the delivery person to deliver fake TMs\.", "Intercepte o carregamento de TMs para a Celadon Store e finja ser a pessoa da entrega para entregar TMs falsas."),
    (r"Pokémon Collector", "Colecionador de Pokémon"),
    (r"Go meet a Pokémon collector on Route 22, near Viridian City and get his rare Pokémon\.", "Encontre um colecionador de Pokémon na Route 22, perto de Viridian City, e pegue o Pokémon raro dele."),
    (r"Operation Shutdown", "Operação Shutdown"),
    (r"The Team Rocket HQ is being raided! Regroup with the rest of the grunts in Goldenrod Tunnel!", "O Team Rocket HQ está sendo invadido! Reagrupe-se com o resto dos grunts em Goldenrod Tunnel!"),
    (r"Help Team Rocket with a heist on a Pokémon nature preserve!", "Ajude a Team Rocket em um assalto a uma reserva natural de Pokémon!"),
    (r"First off what does the legendary Pokémon look like\?", "Primeiro: como é a aparência do Pokémon lendário?"),
    (r"A flying creature", "Uma criatura voadora"),
    (r"A large beast", "Uma fera grande"),
    (r"An aquatic creature", "Uma criatura aquática"),
    (r"I don't know\.\.\.", "Eu não sei..."),
    (r"I don't know", "Eu não sei"),
    (r"You don't know\? Have you even seen that Pokémon\?", "Você não sabe? Você pelo menos viu esse Pokémon?"),
    (r"Hmm\.\.\. You better have some more information\.", "Hmm... É melhor você ter mais alguma informação."),
    (r"\{1\} that's also a legendary Pokémon\? That sounds incredible! You have my attention\.", "{1} que também é um Pokémon lendário? Isso parece incrível! Você tem minha atenção."),
    (r"Okay\.\.\. What about its type\?", "Certo... E quanto ao tipo dele?"),
    (r"Electric-type", "tipo Electric"),
    (r"Fire-type", "tipo Fire"),
    (r"Water-Type", "tipo Water"),
    (r"Ice-type", "tipo Ice"),
    (r"So you don't know its type\.\.\. Hmm\.\.\.", "Então você não sabe o tipo dele... Hmm..."),
    (r"Hmm\.\.\. So it's an unknown creature that's \{1\}\.\.\.", "Hmm... Então é uma criatura desconhecida que é {1}..."),
    (r"Hmm\.\.\. \{1\} that's \{2\}\.", "Hmm... {1} que é {2}."),
    (r"That sounds pretty exciting!", "Isso parece bem interessante!"),
    (r"I've never heard of such a creature, but keep going\.", "Nunca ouvi falar de uma criatura assim, mas continue."),
    (r"So\.\.\. Where was this legendary Pokémon sighted\?", "Então... Onde esse Pokémon lendário foi avistado?"),
    (r"Near Viridian City", "Perto de Viridian City"),
    (r"Near Lavender Town", "Perto de Lavender Town"),
    (r"Near Cerulean City", "Perto de Cerulean City"),
    (r"Near Cinnabar Island", "Perto de Cinnabar Island"),
    (r"Do you even know anything\? This has been such a waste of time!", "Você sabe alguma coisa? Isso foi uma perda de tempo enorme!"),
    (r"How can you not know where it was sighted\? Do you know how unhelpful this is to me\?", "Como você não sabe onde ele foi avistado? Você sabe o quanto isso não me ajuda?"),
    (r"\{1\}, huh\? Ah yes, that would make a lot of sense\.\.\. How did I not think of this before\?", "{1}, hein? Ah, sim, isso faria muito sentido... Como eu não pensei nisso antes?"),
    (r"Hmmm\.\.\. \{1\}, really\? That sounds pretty surprising to me\.", "Hmmm... {1}, sério? Isso me parece bem surpreendente."),
    (r"that unknown location", "aquele local desconhecido"),
    (r"And at what time of the day was that legendary Pokémon seen near \{1\} exactly\?", "E em que horário do dia esse Pokémon lendário foi visto perto de {1}, exatamente?"),
    (r"At dawn", "Ao amanhecer"),
    (r"At noon", "Ao meio-dia"),
    (r"In the afternoon", "À tarde"),
    (r"At sunset", "Ao pôr do sol"),
    (r"At night", "À noite"),
    (r"So it was seen near \{1\} \{2\}\.\.\.", "Então ele foi visto perto de {1} {2}..."),
    (r"\.\.\. Wait, I can't take your only Pokémon!", "... Espera, eu não posso pegar seu único Pokémon!"),
    (r"Oh, this is the Pokémon you got from the collector, right\?", "Ah, esse é o Pokémon que você conseguiu com o colecionador, certo?"),
    (r"Yeah, I can't take that one\. The collector blabbed to the police so it's too risky\.", "É, não posso pegar esse. O colecionador abriu o bico para a polícia, então é arriscado demais."),
    (r"You wanna sell me this \{1\}, is that right\?", "Você quer me vender este {1}, é isso?"),
    (r"Hmm\.\.\. Let's see\.\.\.", "Hmm... Vamos ver..."),
    (r"\\GI could give you \$\{1\} for it\. Do we have a deal\?", r"\\GPosso te dar ${1} por ele. Temos um acordo?"),
    (r"\\GExcellent\. And of course, 30% goes to Team Rocket\. So you get \$\{1\}\.", r"\\GExcelente. E, claro, 30% vai para a Team Rocket. Então você fica com ${1}."),
    (r"\\GPleasure doing business with you\.", r"\\GPrazer em fazer negócios com você."),
    (r"Stop wasting my time!", "Pare de desperdiçar meu tempo!"),
]


def apply_fixes(text: str) -> str:
    for pattern, replacement in FIXES:
        text = re.sub(pattern, lambda match, value=replacement: value, text)

    text = text.replace("Quest completed!", "Quest conclu\u00edda!")
    text = text.replace("Conclu\u00edda quests", "Quests conclu\u00eddas")
    text = text.replace("Conclu\u00edda Quests", "Quests conclu\u00eddas")
    text = text.replace("Accepted quests:", "Quests aceitas:")
    text = text.replace("In-progress:", "Em andamento:")

    # These words are code identifiers and must never be translated.
    text = text.replace("pbConcluídaQuest?", "pbCompletedQuest?")
    text = text.replace("pbConcluidaQuest?", "pbCompletedQuest?")
    return text


def rebuild_target(target_root: Path) -> list[Path]:
    changed: list[Path] = []
    for source in sorted(SOURCE_ROOT.rglob("*.rb")):
        relative = source.relative_to(SOURCE_ROOT)
        target = target_root / relative
        target.parent.mkdir(parents=True, exist_ok=True)
        original = source.read_text(encoding="utf-8-sig")
        translated = apply_fixes(original)
        if not target.exists() or target.read_text(encoding="utf-8-sig", errors="replace") != translated:
            target.write_text(translated, encoding="utf-8", newline="")
            changed.append(target)
    return changed


def main() -> None:
    if not SOURCE_ROOT.exists():
        raise SystemExit(f"Missing clean quest-script backup: {SOURCE_ROOT}")

    changed = rebuild_target(PAYLOAD_ROOT)
    changed += rebuild_target(LIVE_ROOT)

    print(f"Rebuilt {len(changed)} quest script files.")
    for path in changed:
        print(path)


if __name__ == "__main__":
    main()
