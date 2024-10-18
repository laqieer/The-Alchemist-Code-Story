// Decompiled with JetBrains decompiler
// Type: SRPG.GvGBeatRankingWindowContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGBeatRankingWindowContent : MonoBehaviour, IPagination
  {
    [SerializeField]
    private ImageArray mRankImage;
    [SerializeField]
    private Text mRankText;
    [SerializeField]
    private ImageArray mRoleImage;
    [SerializeField]
    private Text mNameText;
    [SerializeField]
    private Text mLevelText;
    [SerializeField]
    private GameObject mUnit;
    [SerializeField]
    private Text mBeat;
    [Space(10f)]
    [SerializeField]
    private Button PageNextButton;
    [SerializeField]
    private Button PagePrevButton;
    [SerializeField]
    private Text PageCurrentText;
    [SerializeField]
    private Text PageTotalText;
    [SerializeField]
    private ScrollRect BeatRankingScrollRect;
    private int mTotalRankingPage = 1;
    private const int DEFAULT_RANKING_NOTEXT = 3;

    public int mRankingPage { get; private set; }

    public void Setup(GvGScoreRankingData beatData, int totalPage)
    {
      this.mTotalRankingPage = totalPage;
      if (beatData == null)
        return;
      if (beatData.Rank > 0)
      {
        if (Object.op_Inequality((Object) this.mRankImage, (Object) null))
        {
          int num = beatData.Rank - 1;
          if (num >= this.mRankImage.Images.Length)
            num = this.mRankImage.Images.Length - 1;
          this.mRankImage.ImageIndex = num;
        }
        if (Object.op_Inequality((Object) this.mRankText, (Object) null))
          this.mRankText.text = beatData.Rank <= 3 ? string.Empty : string.Format(LocalizedText.Get("sys.GVG_SET_RANK"), (object) beatData.Rank.ToString());
      }
      if (Object.op_Inequality((Object) this.mRoleImage, (Object) null) && beatData.Role > 0)
      {
        int num = beatData.Role - 1;
        if (num >= this.mRoleImage.Images.Length)
          num = this.mRoleImage.Images.Length - 1;
        this.mRoleImage.ImageIndex = num;
      }
      if (Object.op_Inequality((Object) this.mNameText, (Object) null))
        this.mNameText.text = beatData.Name;
      if (Object.op_Inequality((Object) this.mLevelText, (Object) null))
        this.mLevelText.text = beatData.Level.ToString();
      if (Object.op_Inequality((Object) this.mBeat, (Object) null))
        this.mBeat.text = beatData.Score.ToString();
      if (Object.op_Inequality((Object) this.mUnit, (Object) null) && beatData.Unit != null)
        DataSource.Bind<UnitData>(this.mUnit, beatData.Unit);
      if (beatData.Guild != null)
        DataSource.Bind<ViewGuildData>(((Component) this).gameObject, beatData.Guild);
      this.RefreshPagination();
    }

    private void RefreshPagination()
    {
      if (Object.op_Inequality((Object) this.PageTotalText, (Object) null))
        this.PageTotalText.text = Mathf.Max(this.mTotalRankingPage, 1).ToString();
      if (Object.op_Inequality((Object) this.PageCurrentText, (Object) null))
        this.PageCurrentText.text = Mathf.Max(this.mRankingPage, 1).ToString();
      if (Object.op_Inequality((Object) this.PageNextButton, (Object) null))
        ((Selectable) this.PageNextButton).interactable = this.mRankingPage < this.mTotalRankingPage;
      if (Object.op_Inequality((Object) this.PagePrevButton, (Object) null))
        ((Selectable) this.PagePrevButton).interactable = this.mRankingPage > 1;
      if (!Object.op_Inequality((Object) this.BeatRankingScrollRect, (Object) null))
        return;
      this.BeatRankingScrollRect.normalizedPosition = Vector2.up;
    }

    public void NextPage()
    {
      if (this.mRankingPage >= this.mTotalRankingPage)
        return;
      ++this.mRankingPage;
    }

    public void PrevPage()
    {
      if (1 >= this.mRankingPage)
        return;
      --this.mRankingPage;
    }
  }
}
