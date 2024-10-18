// Decompiled with JetBrains decompiler
// Type: SRPG.ItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("SRPG/UI/アイテムリスト")]
  [FlowNode.Pin(100, "アイテムリセット", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "アイテム決定", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(110, "全アイテム表示", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(111, "回復アイテム表示", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(112, "攻撃アイテム表示", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(150, "装備スロット解除", FlowNode.PinTypes.Input, 150)]
  public class ItemList : SRPG_ListBase, IFlowInterface
  {
    private ItemData[] mInventoryCache = new ItemData[5];
    public static ItemData SelectedItem;
    public GameObject ItemTemplate;
    public ItemList.ItemFilters Filter;

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void InventoryChanged()
    {
      GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.INVENTORY_ITEMICON);
      GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.INVENTORY_ITEMNAME);
      GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.INVENTORY_ITEMAMOUNT);
      GameParameter.UpdateValuesOfType(GameParameter.ParameterTypes.INVENTORY_FRAME);
    }

    private void ResetInventory()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < this.mInventoryCache.Length; ++index)
        player.SetInventory(index, this.mInventoryCache[index]);
      this.InventoryChanged();
    }

    private void CacheInventory()
    {
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      for (int index = 0; index < this.mInventoryCache.Length; ++index)
        this.mInventoryCache[index] = player.Inventory[index];
    }

    protected override void Start()
    {
      this.CacheInventory();
      if (Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
      {
        List<ItemData> items = MonoSingleton<GameManager>.Instance.Player.Items;
        for (int index = 0; index < items.Count; ++index)
        {
          if (items[index].Num > 0)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
            DataSource.Bind<ItemData>(gameObject, items[index]);
            ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
            if (Object.op_Inequality((Object) component, (Object) null))
            {
              component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
              component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenDetail);
              component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseDetail);
            }
            gameObject.transform.SetParent(((Component) this).transform, false);
            gameObject.SetActive(true);
            this.AddItem(component);
          }
        }
      }
      base.Start();
      this.RefreshItems();
    }

    private void RefreshItems()
    {
      ListItemEvents[] items = this.Items;
      for (int index = items.Length - 1; index >= 0; --index)
      {
        ListItemEvents listItemEvents = items[index];
        ((Component) listItemEvents).gameObject.SetActive(false);
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) listItemEvents).gameObject, (ItemData) null);
        if (dataOfClass != null && dataOfClass.ItemType == EItemType.Used)
        {
          if (dataOfClass.Skill == null)
            Debug.LogError((object) "消費アイテムに対してスキル効果が設定されていない");
          else if (this.Filter == ItemList.ItemFilters.All)
            ((Component) listItemEvents).gameObject.SetActive(true);
          else if (this.Filter == ItemList.ItemFilters.Potions)
          {
            SkillEffectTypes effectType = dataOfClass.Skill.EffectType;
            switch (effectType)
            {
              case SkillEffectTypes.Heal:
              case SkillEffectTypes.Buff:
              case SkillEffectTypes.Revive:
label_10:
                ((Component) listItemEvents).gameObject.SetActive(true);
                continue;
              default:
                switch (effectType)
                {
                  case SkillEffectTypes.CureCondition:
                  case SkillEffectTypes.GemsIncDec:
                    goto label_10;
                  default:
                    if (effectType != SkillEffectTypes.RateHeal)
                    {
                      ((Component) listItemEvents).gameObject.SetActive(false);
                      continue;
                    }
                    goto label_10;
                }
            }
          }
          else if (this.Filter == ItemList.ItemFilters.Offensive)
          {
            switch (dataOfClass.Skill.EffectType)
            {
              case SkillEffectTypes.Attack:
              case SkillEffectTypes.Debuff:
              case SkillEffectTypes.FailCondition:
              case SkillEffectTypes.RateDamage:
              case SkillEffectTypes.RateDamageCurrent:
                ((Component) listItemEvents).gameObject.SetActive(true);
                continue;
              default:
                ((Component) listItemEvents).gameObject.SetActive(false);
                continue;
            }
          }
        }
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 100:
          this.ResetInventory();
          break;
        case 101:
          this.CacheInventory();
          break;
        case 110:
          this.Filter = ItemList.ItemFilters.All;
          this.RefreshItems();
          break;
        case 111:
          this.Filter = ItemList.ItemFilters.Potions;
          this.RefreshItems();
          break;
        case 112:
          this.Filter = ItemList.ItemFilters.Offensive;
          this.RefreshItems();
          break;
        case 150:
          if (!Object.op_Inequality((Object) InventorySlot.Active, (Object) null))
            break;
          MonoSingleton<GameManager>.Instance.Player.SetInventory(InventorySlot.Active.Index, (ItemData) null);
          this.InventoryChanged();
          break;
      }
    }

    private void OnSelect(GameObject go)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null || !Object.op_Inequality((Object) InventorySlot.Active, (Object) null))
        return;
      for (int index = 0; index < 5; ++index)
      {
        if (MonoSingleton<GameManager>.Instance.Player.Inventory[index] == dataOfClass)
          MonoSingleton<GameManager>.Instance.Player.SetInventory(index, (ItemData) null);
      }
      MonoSingleton<GameManager>.Instance.Player.SetInventory(InventorySlot.Active.Index, dataOfClass);
      this.InventoryChanged();
    }

    private void OnOpenDetail(GameObject go)
    {
    }

    private void OnCloseDetail(GameObject go)
    {
    }

    public enum ItemFilters
    {
      All,
      Potions,
      Offensive,
    }
  }
}
