// Decompiled with JetBrains decompiler
// Type: SRPG.GenesisChapterSelectorItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    public GenesisChapterParam ChapterParam
    {
      get
      {
        return this.mChapterParam;
      }
    }

    public bool IsOutOfPeriod
    {
      get
      {
        return this.mIsOutOfPeriod;
      }
    }

    public bool IsLiberation
    {
      get
      {
        return this.mIsLiberation;
      }
    }

    public void SetItem(GenesisChapterParam chapter_param, UnityAction action, bool is_out_of_period, bool is_liberation)
    {
      if (chapter_param == null)
        return;
      this.mChapterParam = chapter_param;
      this.mIsOutOfPeriod = is_out_of_period;
      this.mIsLiberation = is_liberation;
      if ((bool) ((UnityEngine.Object) this.TextTitle))
        this.TextTitle.text = chapter_param.Name;
      if ((bool) ((UnityEngine.Object) this.TrParentBanner) && !string.IsNullOrEmpty(chapter_param.ChapterBanner) && (bool) ((UnityEngine.Object) GenesisManager.Instance))
        GenesisManager.Instance.LoadAssets<GameObject>(chapter_param.ChapterBanner, (GenesisManager.LoadAssetCallback<GameObject>) (prefab =>
        {
          if ((UnityEngine.Object) prefab == (UnityEngine.Object) null)
          {
            DebugUtility.LogError("GenesisChapterSelectorItem/AssetLoad Error! name=" + this.mChapterParam.ChapterBanner);
          }
          else
          {
            prefab.gameObject.SetActive(true);
            UnityEngine.Object.Instantiate<GameObject>(prefab, this.TrParentBanner);
          }
        }));
      if ((bool) ((UnityEngine.Object) this.GoLock))
        this.GoLock.SetActive(is_out_of_period || !is_liberation);
      if (action == null || !(bool) ((UnityEngine.Object) this.SelectBtn))
        return;
      this.SelectBtn.onClick.RemoveListener(action);
      this.SelectBtn.onClick.AddListener(action);
    }
  }
}
