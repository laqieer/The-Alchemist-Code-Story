// Decompiled with JetBrains decompiler
// Type: SRPG.GachaDiscountWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Initalize", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "Initalized", FlowNode.PinTypes.Output, 1)]
  [FlowNode.Pin(11, "Selected", FlowNode.PinTypes.Output, 11)]
  public class GachaDiscountWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_INIT = 0;
    private const int PIN_OT_INIT = 1;
    private const int PIN_OT_SELECT = 11;
    [SerializeField]
    private Transform m_RootObject;
    [SerializeField]
    private GameObject m_TemplateObject;
    [SerializeField]
    private GameObject m_EmptyObject;
    public static ItemData m_SelectItemData;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      if (!this.Init())
        DebugUtility.LogError("GachaDiscountWindow:InitError!");
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 1);
    }

    private void Awake()
    {
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_TemplateObject, (UnityEngine.Object) null))
        this.m_TemplateObject.SetActive(false);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_EmptyObject, (UnityEngine.Object) null))
        return;
      this.m_EmptyObject.SetActive(false);
    }

    private void OnEnable() => GachaDiscountWindow.m_SelectItemData = (ItemData) null;

    private void OnDisable() => GachaDiscountWindow.m_SelectItemData = (ItemData) null;

    private bool Init()
    {
      GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
        return false;
      List<ItemData> list = instanceDirect.Player.Items.Where<ItemData>((Func<ItemData, bool>) (item => item.ItemType == EItemType.DISCOUNT_GACHA && item.Num > 0 && item.Param.IsLimited)).ToList<ItemData>();
      if (list == null || list.Count <= 0)
        this.m_EmptyObject.SetActive(true);
      else
        this.CreateItemList(list.ToArray());
      return true;
    }

    private void CreateItemList(ItemData[] items)
    {
      if (items == null || items.Length <= 0)
        return;
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_RootObject, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_TemplateObject, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("RootObject、もしくはTemplateObjectの指定がありません.");
      }
      else
      {
        List<ItemData> itemDataList = new List<ItemData>((IEnumerable<ItemData>) items);
        itemDataList.Sort((Comparison<ItemData>) ((item1, item2) => GachaDiscountWindow.ComparaEndAt(item1, item2)));
        for (int index = 0; index < itemDataList.Count; ++index)
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: variable of a compiler-generated type
          GachaDiscountWindow.\u003CCreateItemList\u003Ec__AnonStorey0 listCAnonStorey0 = new GachaDiscountWindow.\u003CCreateItemList\u003Ec__AnonStorey0();
          // ISSUE: reference to a compiler-generated field
          listCAnonStorey0.\u0024this = this;
          // ISSUE: reference to a compiler-generated field
          listCAnonStorey0.item = itemDataList[index];
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          if (listCAnonStorey0.item.Num > 0 && listCAnonStorey0.item.Param.IsLimited && !listCAnonStorey0.item.Param.IsExpire)
          {
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.m_TemplateObject);
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
            {
              gameObject.transform.SetParent(this.m_RootObject, false);
              // ISSUE: reference to a compiler-generated field
              DataSource.Bind<ItemData>(gameObject, listCAnonStorey0.item);
              gameObject.SetActive(true);
              Button componentInChildren = gameObject.GetComponentInChildren<Button>();
              if (UnityEngine.Object.op_Inequality((UnityEngine.Object) componentInChildren, (UnityEngine.Object) null))
              {
                // ISSUE: method pointer
                ((UnityEvent) componentInChildren.onClick).AddListener(new UnityAction((object) listCAnonStorey0, __methodptr(\u003C\u003Em__0)));
              }
            }
          }
        }
        GameParameter.UpdateAll(((Component) this.m_RootObject).gameObject);
      }
    }

    private static int ComparaEndAt(ItemData item1, ItemData item2)
    {
      long num1 = TimeManager.FromDateTime(item1.Param.end_at);
      long num2 = TimeManager.FromDateTime(item2.Param.end_at);
      if (num1 == num2)
        return 0;
      return num1 > num2 ? 1 : -11;
    }

    private void Select(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return;
      FlowNode_Variable.Set("USE_TICKET_INAME", iname);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 11);
    }
  }
}
