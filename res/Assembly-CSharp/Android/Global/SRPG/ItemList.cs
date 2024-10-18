// Decompiled with JetBrains decompiler
// Type: SRPG.ItemList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(112, "攻撃アイテム表示", FlowNode.PinTypes.Input, 12)]
  [AddComponentMenu("SRPG/UI/アイテムリスト")]
  [FlowNode.Pin(100, "アイテムリセット", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "アイテム決定", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(110, "全アイテム表示", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(111, "回復アイテム表示", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(150, "装備スロット解除", FlowNode.PinTypes.Input, 150)]
  public class ItemList : SRPG_ListBase, IFlowInterface
  {
    private ItemData[] mInventoryCache = new ItemData[5];
    public static ItemData SelectedItem;
    public GameObject ItemTemplate;
    public ItemList.ItemFilters Filter;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null) || !this.ItemTemplate.activeInHierarchy)
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
      if ((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        for (int index = 0; index < player.Items.Count; ++index)
        {
          if (player.Items[index].Num > 0)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
            DataSource.Bind<ItemData>(gameObject, player.Items[index]);
            ListItemEvents component = gameObject.GetComponent<ListItemEvents>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            {
              component.OnSelect = new ListItemEvents.ListItemEvent(this.OnSelect);
              component.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnOpenDetail);
              component.OnCloseDetail = new ListItemEvents.ListItemEvent(this.OnCloseDetail);
            }
            gameObject.transform.SetParent(this.transform, false);
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
        listItemEvents.gameObject.SetActive(false);
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(listItemEvents.gameObject, (ItemData) null);
        if (dataOfClass != null && dataOfClass.ItemType == EItemType.Used)
        {
          if (dataOfClass.Skill == null)
            Debug.LogError((object) "消費アイテムに対してスキル効果が設定されていない");
          else if (this.Filter == ItemList.ItemFilters.All)
            listItemEvents.gameObject.SetActive(true);
          else if (this.Filter == ItemList.ItemFilters.Potions)
          {
            SkillEffectTypes effectType = dataOfClass.Skill.EffectType;
            switch (effectType)
            {
              case SkillEffectTypes.Heal:
              case SkillEffectTypes.Buff:
              case SkillEffectTypes.Revive:
label_10:
                listItemEvents.gameObject.SetActive(true);
                continue;
              default:
                switch (effectType - 12)
                {
                  case SkillEffectTypes.None:
                  case SkillEffectTypes.Defend:
                    goto label_10;
                  default:
                    if (effectType != SkillEffectTypes.RateHeal)
                    {
                      listItemEvents.gameObject.SetActive(false);
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
                listItemEvents.gameObject.SetActive(true);
                continue;
              default:
                listItemEvents.gameObject.SetActive(false);
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
          if (!((UnityEngine.Object) InventorySlot.Active != (UnityEngine.Object) null))
            break;
          MonoSingleton<GameManager>.Instance.Player.SetInventory(InventorySlot.Active.Index, (ItemData) null);
          this.InventoryChanged();
          break;
      }
    }

    private void OnSelect(GameObject go)
    {
      ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(go, (ItemData) null);
      if (dataOfClass == null || !((UnityEngine.Object) InventorySlot.Active != (UnityEngine.Object) null))
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
