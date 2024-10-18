// Decompiled with JetBrains decompiler
// Type: SRPG.AutoRepeatQuestApItemContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class AutoRepeatQuestApItemContent : MonoBehaviour
  {
    [SerializeField]
    private Toggle mToggle;
    [SerializeField]
    private ImageArray mPriorityImageArray;
    [SerializeField]
    private Text mApValueText;
    [SerializeField]
    private GameObject mLastPriorityImage;
    private ItemData mItemData;

    public string ItemIname => this.mItemData != null ? this.mItemData.ItemID : string.Empty;

    public int PriorityImageMax => this.mPriorityImageArray.Images.Length;

    public void Init(ItemParam item_param)
    {
      if (item_param == null)
        return;
      this.mItemData = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemID(item_param.iname);
      if (this.mItemData == null)
      {
        Json_Item json = new Json_Item();
        json.iname = item_param.iname;
        json.num = 0;
        this.mItemData = new ItemData();
        this.mItemData.Deserialize(json);
      }
      DataSource.Bind<ItemData>(((Component) this).gameObject, this.mItemData);
      this.Refresh(-1);
    }

    public void Refresh(int priority, bool is_last_priority = false)
    {
      if (Object.op_Inequality((Object) this.mToggle, (Object) null))
        GameUtility.SetToggle(this.mToggle, priority >= 0);
      if (Object.op_Inequality((Object) this.mPriorityImageArray, (Object) null) && priority >= 0)
        this.mPriorityImageArray.ImageIndex = priority;
      if (this.mItemData != null && this.mItemData.Param != null && Object.op_Inequality((Object) this.mApValueText, (Object) null))
        this.mApValueText.text = this.mItemData.Param.value.ToString();
      GameUtility.SetGameObjectActive(this.mLastPriorityImage, is_last_priority);
      GameParameter.UpdateAll(((Component) this).gameObject);
    }
  }
}
