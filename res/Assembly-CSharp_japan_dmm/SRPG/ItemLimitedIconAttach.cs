// Decompiled with JetBrains decompiler
// Type: SRPG.ItemLimitedIconAttach
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ItemLimitedIconAttach : MonoBehaviour
  {
    private GameObject mLimitedIcon;

    public void Refresh(ItemParam _item_param = null)
    {
      ItemParam itemParam = _item_param ?? this.GetItemParam();
      if (itemParam == null)
        this.Hide();
      else if (!itemParam.IsLimited)
        this.Hide();
      else
        this.Display();
    }

    private ItemParam GetItemParam()
    {
      ItemParam itemParam = (ItemParam) null;
      if (itemParam == null)
      {
        ItemData dataOfClass = DataSource.FindDataOfClass<ItemData>(((Component) this).gameObject, (ItemData) null);
        if (dataOfClass != null)
          itemParam = dataOfClass.Param;
      }
      if (itemParam == null)
      {
        ItemParam dataOfClass = DataSource.FindDataOfClass<ItemParam>(((Component) this).gameObject, (ItemParam) null);
        if (dataOfClass != null)
          itemParam = dataOfClass;
      }
      return itemParam;
    }

    private void Display()
    {
      if (Object.op_Equality((Object) this.mLimitedIcon, (Object) null))
      {
        GameObject limitedIcon = GameSettings.Instance.ItemIcons.LimitedIcon;
        if (!Object.op_Inequality((Object) limitedIcon, (Object) null))
          return;
        this.mLimitedIcon = Object.Instantiate<GameObject>(limitedIcon);
        this.mLimitedIcon.transform.SetParent(((Component) this).gameObject.transform, false);
      }
      else
        this.mLimitedIcon.SetActive(true);
    }

    public void Hide()
    {
      if (Object.op_Equality((Object) this.mLimitedIcon, (Object) null))
        return;
      this.mLimitedIcon.SetActive(false);
    }
  }
}
