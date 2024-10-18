// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterSelectorItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GenesisChapterSelectorItem : MonoBehaviour
  {
    [SerializeField]
    private Text TextTitle;
    [SerializeField]
    private Transform TrParentBanner;
    [SerializeField]
    private GameObject GoLock;
    [Space(5f)]
    [SerializeField]
    private SRPG_Button SelectBtn;
    private GenesisChapterParam mChapterParam;
    private bool mIsOutOfPeriod;
    private bool mIsLiberation;

    public GenesisChapterParam ChapterParam => this.mChapterParam;

    public bool IsOutOfPeriod => this.mIsOutOfPeriod;

    public bool IsLiberation => this.mIsLiberation;

    public void SetItem(
      GenesisChapterParam chapter_param,
      UnityAction action,
      bool is_out_of_period,
      bool is_liberation)
    {
      if (chapter_param == null)
        return;
      this.mChapterParam = chapter_param;
      this.mIsOutOfPeriod = is_out_of_period;
      this.mIsLiberation = is_liberation;
      if (Object.op_Implicit((Object) this.TextTitle))
        this.TextTitle.text = chapter_param.Name;
      if (Object.op_Implicit((Object) this.TrParentBanner) && !string.IsNullOrEmpty(chapter_param.ChapterBanner) && Object.op_Implicit((Object) GenesisManager.Instance))
        GenesisManager.Instance.LoadAssets<GameObject>(chapter_param.ChapterBanner, (GenesisManager.LoadAssetCallback<GameObject>) (prefab =>
        {
          if (Object.op_Equality((Object) prefab, (Object) null))
          {
            DebugUtility.LogError("GenesisChapterSelectorItem/AssetLoad Error! name=" + this.mChapterParam.ChapterBanner);
          }
          else
          {
            prefab.gameObject.SetActive(true);
            Object.Instantiate<GameObject>(prefab, this.TrParentBanner);
          }
        }));
      if (Object.op_Implicit((Object) this.GoLock))
        this.GoLock.SetActive(is_out_of_period || !is_liberation);
      if (action == null || !Object.op_Implicit((Object) this.SelectBtn))
        return;
      ((UnityEvent) this.SelectBtn.onClick).RemoveListener(action);
      ((UnityEvent) this.SelectBtn.onClick).AddListener(action);
    }
  }
}
