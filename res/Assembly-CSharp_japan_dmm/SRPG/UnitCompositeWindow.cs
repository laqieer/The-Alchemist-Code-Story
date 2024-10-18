// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCompositeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class UnitCompositeWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform ItemLayoutParent;
    public RectTransform CommonItemLayoutParent;
    public GameObject ItemTemplate;
    public GameObject CommonItemTemplate;
    private ItemParam mItemParam;
    private List<GameObject> mConsumeObjects = new List<GameObject>(10);

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start() => this.Refresh();

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    private void Refresh()
    {
      this.mItemParam = MonoSingleton<GameManager>.Instance.GetItemParam(GlobalVars.SelectedCreateItemID);
      int cost = 0;
      bool is_ikkatsu = false;
      Dictionary<string, int> consumes = (Dictionary<string, int>) null;
      NeedEquipItemList item_list = new NeedEquipItemList();
      MonoSingleton<GameManager>.Instance.Player.CheckEnableCreateItem(this.mItemParam, ref is_ikkatsu, ref cost, ref consumes, item_list);
      for (int index = 0; index < this.mConsumeObjects.Count; ++index)
        this.mConsumeObjects[index].gameObject.SetActive(false);
      if (consumes != null)
      {
        int index = 0;
        foreach (KeyValuePair<string, int> keyValuePair in consumes)
        {
          if (index >= this.mConsumeObjects.Count)
          {
            GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
            gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
            this.mConsumeObjects.Add(gameObject);
          }
          GameObject mConsumeObject = this.mConsumeObjects[index];
          DataSource.Bind<ConsumeItemData>(mConsumeObject, new ConsumeItemData()
          {
            param = MonoSingleton<GameManager>.Instance.GetItemParam(keyValuePair.Key),
            num = keyValuePair.Value
          });
          mConsumeObject.SetActive(true);
          ++index;
        }
      }
      foreach (byte key in item_list.CommonNeedNum.Keys)
      {
        NeedEquipItemDictionary equipItemDictionary = item_list.CommonNeedNum[key];
        ItemParam commonItemParam = equipItemDictionary.CommonItemParam;
        if (commonItemParam != null)
        {
          for (int index = 0; index < equipItemDictionary.list.Count; ++index)
          {
            ItemParam itemParam = equipItemDictionary.list[index].Param;
            if (itemParam != null)
            {
              if ((int) itemParam.cmn_type - 1 == 2)
              {
                GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
                gameObject.gameObject.SetActive(true);
                gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
                ItemData itemData = this.CreateItemData(itemParam.iname, 1);
                DataSource.Bind<ItemData>(gameObject, itemData);
              }
              else
              {
                GameObject gameObject = Object.Instantiate<GameObject>(this.CommonItemTemplate);
                gameObject.gameObject.SetActive(true);
                gameObject.transform.SetParent((Transform) this.CommonItemLayoutParent, false);
                ItemData data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(itemParam.iname) ?? this.CreateItemData(commonItemParam.iname, 0);
                ItemData cmmon_data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(commonItemParam.iname) ?? this.CreateItemData(commonItemParam.iname, 0);
                gameObject.GetComponent<CommonConvertItem>().Bind(data, cmmon_data, equipItemDictionary.list[index].NeedPiece);
              }
            }
          }
        }
      }
      DataSource.Bind<ItemParam>(((Component) this).gameObject, this.mItemParam);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    public ItemData CreateItemData(string iname, int num)
    {
      Json_Item json = new Json_Item();
      json.iname = iname;
      json.num = num;
      ItemData itemData = new ItemData();
      itemData.Deserialize(json);
      return itemData;
    }
  }
}
