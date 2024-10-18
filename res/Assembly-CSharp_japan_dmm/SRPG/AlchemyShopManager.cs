// Decompiled with JetBrains decompiler
// Type: SRPG.AlchemyShopManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "初期化", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "初期化完了", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ショップリストがない", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(111, "カテゴリが選択された", FlowNode.PinTypes.Output, 111)]
  public class AlchemyShopManager : MonoBehaviour, IFlowInterface
  {
    [SerializeField]
    private GameObject WindowCategory;
    [SerializeField]
    private GameObject WindowShop;
    [Space(5f)]
    [SerializeField]
    private GameObject GoCategoryParent;
    [SerializeField]
    private AlchemyShopItemCategory TemplateCategory;
    [Space(5f)]
    [SerializeField]
    private ScrollRect SrControllerShop;
    private const int PIN_IN_INIT = 1;
    private const int PIN_OUT_INIT_FINISHED = 101;
    private const int PIN_OUT_NO_SHOP_LIST = 102;
    private const int PIN_OUT_CATEGORY_SELECTED = 111;
    private static AlchemyShopManager mInstance;
    private JSON_ShopListArray.Shops[] mShopArray;
    private LimitedShopListItem mShopItem;

    public static AlchemyShopManager Instance => AlchemyShopManager.mInstance;

    private void Awake()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) AlchemyShopManager.mInstance))
        AlchemyShopManager.mInstance = this;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.WindowCategory))
        this.WindowCategory.SetActive(false);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.WindowShop))
        return;
      this.WindowShop.SetActive(false);
    }

    private void OnDestroy()
    {
      if (!UnityEngine.Object.op_Equality((UnityEngine.Object) AlchemyShopManager.mInstance, (UnityEngine.Object) this))
        return;
      this.mShopArray = (JSON_ShopListArray.Shops[]) null;
      this.InternalClearGlobalLimitedShopItemInfo();
      AlchemyShopManager.mInstance = (AlchemyShopManager) null;
    }

    private void InternalClearGlobalLimitedShopItemInfo()
    {
      GlobalVars.LimitedShopItem = (LimitedShopListItem) null;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        instance.Player.SetLimitedShopData(new LimitedShopData());
      this.mShopItem = (LimitedShopListItem) null;
    }

    private void Init()
    {
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoCategoryParent) && UnityEngine.Object.op_Implicit((UnityEngine.Object) this.TemplateCategory))
      {
        GameUtility.SetGameObjectActive(((Component) this.TemplateCategory).gameObject, false);
        GameUtility.DestroyChildGameObjects(this.GoCategoryParent, new List<GameObject>((IEnumerable<GameObject>) new GameObject[1]
        {
          ((Component) this.TemplateCategory).gameObject
        }));
        if (this.mShopArray != null && this.mShopArray.Length != 0)
        {
          for (int index = 0; index < this.mShopArray.Length; ++index)
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            AlchemyShopManager.\u003CInit\u003Ec__AnonStorey0 initCAnonStorey0 = new AlchemyShopManager.\u003CInit\u003Ec__AnonStorey0();
            // ISSUE: reference to a compiler-generated field
            initCAnonStorey0.\u0024this = this;
            JSON_ShopListArray.Shops mShop = this.mShopArray[index];
            // ISSUE: reference to a compiler-generated field
            initCAnonStorey0.item_category = UnityEngine.Object.Instantiate<AlchemyShopItemCategory>(this.TemplateCategory, this.GoCategoryParent.transform, false);
            // ISSUE: reference to a compiler-generated field
            if (UnityEngine.Object.op_Implicit((UnityEngine.Object) initCAnonStorey0.item_category))
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: method pointer
              // ISSUE: method pointer
              initCAnonStorey0.item_category.SetItem(index, mShop, new UnityAction((object) initCAnonStorey0, __methodptr(\u003C\u003Em__0)), new UnityAction((object) initCAnonStorey0, __methodptr(\u003C\u003Em__1)));
              // ISSUE: reference to a compiler-generated field
              ((Component) initCAnonStorey0.item_category).gameObject.SetActive(true);
            }
          }
          this.SetGlobalVarsShopData(this.mShopArray[0]);
          if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.WindowCategory))
            this.WindowCategory.SetActive(true);
        }
      }
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.WindowShop))
        return;
      this.WindowShop.SetActive(true);
    }

    private bool SetGlobalVarsShopData(JSON_ShopListArray.Shops shop)
    {
      if (shop == null)
        return false;
      LimitedShopListItem limitedShopListItem = ((Component) this).RequireComponent<LimitedShopListItem>();
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) limitedShopListItem) || limitedShopListItem.shops == shop)
        return false;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoCategoryParent))
      {
        AlchemyShopItemCategory[] componentsInChildren = this.GoCategoryParent.GetComponentsInChildren<AlchemyShopItemCategory>(true);
        if (componentsInChildren != null)
        {
          for (int index = 0; index < componentsInChildren.Length; ++index)
          {
            AlchemyShopItemCategory shopItemCategory = componentsInChildren[index];
            if (!UnityEngine.Object.op_Equality((UnityEngine.Object) shopItemCategory, (UnityEngine.Object) null))
              shopItemCategory.SetCurrent(shopItemCategory.Shop == shop);
          }
        }
      }
      limitedShopListItem.SetShopList(shop);
      this.mShopItem = limitedShopListItem;
      GlobalVars.LimitedShopItem = this.mShopItem;
      return true;
    }

    private bool IsExistShopList() => this.mShopArray != null && this.mShopArray.Length != 0;

    private bool IsExistLimitedShopData()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      return UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) && instance.Player.GetLimitedShopData().items.Count != 0;
    }

    private void OnTapCategoryItem(AlchemyShopItemCategory item)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) item) || !this.SetGlobalVarsShopData(item.Shop))
        return;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.SrControllerShop))
      {
        this.SrControllerShop.velocity = Vector2.zero;
        this.SrControllerShop.normalizedPosition = Vector2.up;
      }
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Init();
      if (this.IsExistShopList())
      {
        if (!this.IsExistLimitedShopData())
          return;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      }
      else
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
    }

    public static bool EntryShopList(JSON_ShopListArray.Shops[] shops)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) AlchemyShopManager.mInstance) || shops == null)
        return false;
      AlchemyShopManager.mInstance.mShopArray = shops;
      return true;
    }

    public static void ClearGlobalLimitedShopItemInfo()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) AlchemyShopManager.mInstance))
        return;
      AlchemyShopManager.mInstance.InternalClearGlobalLimitedShopItemInfo();
    }

    public static bool SetCurrentShopData(Json_LimitedShopResponse shop_data)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) AlchemyShopManager.mInstance))
        return false;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) || instance.Player == null || shop_data == null)
        return false;
      LimitedShopData limitedShopData = instance.Player.GetLimitedShopData();
      if (!limitedShopData.Deserialize(shop_data))
        return false;
      instance.Player.SetLimitedShopData(limitedShopData);
      return true;
    }

    public static int GetShopListIndex(JSON_ShopListArray.Shops shop)
    {
      return !UnityEngine.Object.op_Implicit((UnityEngine.Object) AlchemyShopManager.mInstance) || AlchemyShopManager.mInstance.mShopArray == null || AlchemyShopManager.mInstance.mShopArray.Length == 0 || shop == null ? -1 : Array.IndexOf<JSON_ShopListArray.Shops>(AlchemyShopManager.mInstance.mShopArray, shop);
    }
  }
}
