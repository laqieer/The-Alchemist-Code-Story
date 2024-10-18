// Decompiled with JetBrains decompiler
// Type: SRPG.UnitCompositeWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class UnitCompositeWindow : MonoBehaviour, IFlowInterface
  {
    private List<GameObject> mConsumeObjects = new List<GameObject>(10);
    public RectTransform ItemLayoutParent;
    public RectTransform CommonItemLayoutParent;
    public GameObject ItemTemplate;
    public GameObject CommonItemTemplate;
    private ItemParam mItemParam;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null) || !this.ItemTemplate.activeInHierarchy)
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
      this.Refresh();
    }

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
        using (Dictionary<string, int>.Enumerator enumerator = consumes.GetEnumerator())
        {
          while (enumerator.MoveNext())
          {
            KeyValuePair<string, int> current = enumerator.Current;
            if (index >= this.mConsumeObjects.Count)
            {
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
              gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
              this.mConsumeObjects.Add(gameObject);
            }
            GameObject mConsumeObject = this.mConsumeObjects[index];
            DataSource.Bind<ConsumeItemData>(mConsumeObject, new ConsumeItemData()
            {
              param = MonoSingleton<GameManager>.Instance.GetItemParam(current.Key),
              num = current.Value
            });
            mConsumeObject.SetActive(true);
            ++index;
          }
        }
      }
      using (Dictionary<byte, NeedEquipItemDictionary>.KeyCollection.Enumerator enumerator = item_list.CommonNeedNum.Keys.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          byte current = enumerator.Current;
          NeedEquipItemDictionary equipItemDictionary = item_list.CommonNeedNum[current];
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
                  GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
                  gameObject.gameObject.SetActive(true);
                  gameObject.transform.SetParent((Transform) this.ItemLayoutParent, false);
                  ItemData itemData = this.CreateItemData(itemParam.iname, 1);
                  DataSource.Bind<ItemData>(gameObject, itemData);
                }
                else
                {
                  GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.CommonItemTemplate);
                  gameObject.gameObject.SetActive(true);
                  gameObject.transform.SetParent((Transform) this.CommonItemLayoutParent, false);
                  ItemData data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(itemParam.iname) ?? this.CreateItemData(itemParam.iname, 0);
                  ItemData cmmon_data = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(commonItemParam.iname) ?? this.CreateItemData(commonItemParam.iname, 0);
                  gameObject.GetComponent<CommonConvertItem>().Bind(data, cmmon_data, equipItemDictionary.list[index].NeedPiece);
                }
              }
            }
          }
        }
      }
      DataSource.Bind<ItemParam>(this.gameObject, this.mItemParam);
      GameParameter.UpdateAll(this.gameObject);
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
