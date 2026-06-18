class HairShopPresenter < PokemonMartScreen
  def pbChooseBuyItem

  end

  def initialize(scene, stock, adapter = nil, versions=false)
    super(scene,stock,adapter)
    @use_versions = versions
  end


  def pbBuyScreen
    @scene.pbStartBuyScene(@stock, @adapter)
    item = nil
    loop do
      item = @scene.pbChooseBuyItem
      break if !item

      if !@adapter.isShop?
        if pbConfirm(_INTL("Gostaria de comprar {1}?", item.name))
          @adapter.putOnOutfit(item)
          @scene.pbEndBuyScene
          return
        end
        next

      end

      itemname = @adapter.getDisplayName(item)
      price = @adapter.getPrice(item)

      echoln price
      if !price.is_a?(Integer)
        #@adapter.switchVersion(item,1)
        pbDisplayPaused("Este é seu penteado atual!")
        next
      end
      if @adapter.getMoney < price
        pbDisplayPaused("Você não tem dinheiro suficiente.")
        next
      end

      if !pbConfirm(_INTL("Certo. Você quer {1}. Vai custar ${2}. OK?",
                          itemname, price.to_s_formatted))
        next
      end
      quantity = 1

      if @adapter.getMoney < price
        pbDisplayPaused("Você não tem dinheiro suficiente.")
        next
      end
      added = 0

      @adapter.setMoney(@adapter.getMoney - price)
      @stock.compact!
      pbDisplayPaused("Aqui está! Obrigado!") { pbSEPlay("Mart buy item") }
      @adapter.addItem(item)
      #break
    end
    @scene.pbEndBuyScene
  end

  def isWornItem?(item)
    super
  end

end
