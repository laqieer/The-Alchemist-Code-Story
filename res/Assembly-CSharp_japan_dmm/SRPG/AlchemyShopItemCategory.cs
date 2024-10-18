// Decompiled with JetBrains decompiler
// Type: SRPG.AlchemyShopItemCategory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class AlchemyShopItemCategory : MonoBehaviour
  {
    [SerializeField]
    private Text TextTitle;
    [Space(5f)]
    [SerializeField]
    private GameObject GoActive;
    [Space(5f)]
    [SerializeField]
    private GameObject GoLocked;
    [SerializeField]
    private SRPG_Button BtnLock;
    [Space(5f)]
    [SerializeField]
    private SRPG_Button BtnSelect;
    private int mIndex;
    private JSON_ShopListArray.Shops mShop;

    public int Index => this.mIndex;

    public JSON_ShopListArray.Shops Shop => this.mShop;

    public void SetItem(
      int index,
      JSON_ShopListArray.Shops shop,
      UnityAction action = null,
      UnityAction lock_action = null)
    {
      if (index < 0 || shop == null)
        return;
      this.mIndex = index;
      this.mShop = shop;
      if (Object.op_Implicit((Object) this.TextTitle) && shop.info != null)
        this.TextTitle.text = shop.info.title;
      if (Object.op_Implicit((Object) this.GoLocked))
      {
        this.GoLocked.SetActive(shop.info == null || shop.info.unlock == 0);
        if (lock_action != null && Object.op_Implicit((Object) this.BtnLock))
          ((UnityEvent) this.BtnLock.onClick).AddListener(lock_action);
      }
      if (action == null || !Object.op_Implicit((Object) this.BtnSelect))
        return;
      ((UnityEvent) this.BtnSelect.onClick).AddListener(action);
    }

    public void SetCurrent(bool is_active)
    {
      if (!Object.op_Implicit((Object) this.GoActive))
        return;
      this.GoActive.SetActive(is_active);
    }
  }
}
