// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDrawBelongings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class RuneDrawBelongings : MonoBehaviour
  {
    [SerializeField]
    private RawImage mItemImg;
    [SerializeField]
    private Text mItemNum;
    private ItemData mItemData;

    private void Awake()
    {
    }

    public void SetDrawParam(ItemData item_data)
    {
      this.mItemData = item_data;
      this.Refresh();
    }

    public void Refresh()
    {
      if (this.mItemData == null)
        return;
      if (Object.op_Implicit((Object) this.mItemNum))
        this.mItemNum.text = this.mItemData.Num.ToString();
      if (Object.op_Implicit((Object) this.mItemImg))
        DataSource.Bind<ItemParam>(((Component) this.mItemImg).gameObject, this.mItemData.Param);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
