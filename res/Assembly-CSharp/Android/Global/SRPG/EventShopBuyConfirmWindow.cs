// Decompiled with JetBrains decompiler
// Type: SRPG.EventShopBuyConfirmWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class EventShopBuyConfirmWindow : MonoBehaviour, IFlowInterface
  {
    public RectTransform UnitLayoutParent;
    public GameObject UnitTemplate;
    public GameObject EnableEquipUnitWindow;
    public GameObject limited_item;
    public GameObject no_limited_item;
    public Text SoldNum;
    private List<GameObject> mUnits;

    private void Awake()
    {
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.UnitTemplate != (UnityEngine.Object) null && this.UnitTemplate.activeInHierarchy)
        this.UnitTemplate.SetActive(false);
      this.mUnits = new List<GameObject>(MonoSingleton<GameManager>.Instance.Player.Units.Count);
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
      if ((UnityEngine.Object) this.UnitTemplate == (UnityEngine.Object) null)
        return;
      List<UnitData> units = MonoSingleton<GameManager>.Instance.Player.Units;
      for (int index = 0; index < this.mUnits.Count; ++index)
        this.mUnits[index].gameObject.SetActive(false);
      EventShopItem data1 = MonoSingleton<GameManager>.Instance.Player.GetEventShopData().items[GlobalVars.ShopBuyIndex];
      if ((UnityEngine.Object) this.limited_item != (UnityEngine.Object) null)
        this.limited_item.SetActive(!data1.IsNotLimited);
      if ((UnityEngine.Object) this.no_limited_item != (UnityEngine.Object) null)
        this.no_limited_item.SetActive(data1.IsNotLimited);
      if ((UnityEngine.Object) this.SoldNum != (UnityEngine.Object) null)
        this.SoldNum.text = data1.remaining_num.ToString();
      ItemData itemDataByItemId = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(data1.iname);
      if ((UnityEngine.Object) this.EnableEquipUnitWindow != (UnityEngine.Object) null)
      {
        this.EnableEquipUnitWindow.gameObject.SetActive(false);
        int index1 = 0;
        for (int index2 = 0; index2 < units.Count; ++index2)
        {
          UnitData data2 = units[index2];
          bool flag = false;
          for (int index3 = 0; index3 < data2.Jobs.Length; ++index3)
          {
            JobData job = data2.Jobs[index3];
            if (job.IsActivated)
            {
              int equipSlotByItemId = job.FindEquipSlotByItemID(data1.iname);
              if (equipSlotByItemId != -1 && job.CheckEnableEquipSlot(equipSlotByItemId))
              {
                flag = true;
                break;
              }
            }
          }
          if (flag)
          {
            if (index1 >= this.mUnits.Count)
            {
              this.UnitTemplate.SetActive(true);
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.UnitTemplate);
              gameObject.transform.SetParent((Transform) this.UnitLayoutParent, false);
              this.mUnits.Add(gameObject);
              this.UnitTemplate.SetActive(false);
            }
            GameObject gameObject1 = this.mUnits[index1].gameObject;
            DataSource.Bind<UnitData>(gameObject1, data2);
            gameObject1.SetActive(true);
            this.EnableEquipUnitWindow.gameObject.SetActive(true);
            ++index1;
          }
        }
      }
      DataSource.Bind<EventShopItem>(this.gameObject, data1);
      DataSource.Bind<ItemData>(this.gameObject, itemDataByItemId);
      DataSource.Bind<ItemParam>(this.gameObject, MonoSingleton<GameManager>.Instance.GetItemParam(data1.iname));
      GameParameter.UpdateAll(this.gameObject);
    }
  }
}
