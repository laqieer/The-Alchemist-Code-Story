// Decompiled with JetBrains decompiler
// Type: SRPG.SupportRankingUnitList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SupportRankingUnitList : MonoBehaviour
  {
    [SerializeField]
    private ImageArray mRankImage;
    [SerializeField]
    private Text mRankText;
    [SerializeField]
    private Text mName;
    [SerializeField]
    private Text mScore;
    [SerializeField]
    private RawImage_Transparent mJobIcon;
    private const int DEFAULT_RANKING_NOTEXT = 3;

    private void Awake() => this.Refresh();

    private void Refresh()
    {
      SupportUnitRanking dataOfClass = DataSource.FindDataOfClass<SupportUnitRanking>(((Component) this).gameObject, (SupportUnitRanking) null);
      if (dataOfClass == null || dataOfClass.rank <= 0)
        return;
      if (Object.op_Inequality((Object) this.mRankImage, (Object) null))
      {
        int num = dataOfClass.rank - 1;
        if (num >= this.mRankImage.Images.Length)
          ((Component) this.mRankImage).gameObject.SetActive(false);
        else
          this.mRankImage.ImageIndex = num;
      }
      if (Object.op_Inequality((Object) this.mRankText, (Object) null) && dataOfClass.rank > 3)
        this.mRankText.text = string.Format(LocalizedText.Get("sys.SUPPORT_SET_RANK"), (object) dataOfClass.rank.ToString());
      JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(dataOfClass.jobName);
      if (jobParam != null)
      {
        if (Object.op_Inequality((Object) this.mJobIcon, (Object) null))
          MonoSingleton<GameManager>.Instance.ApplyTextureAsync((RawImage) this.mJobIcon, jobParam == null ? (string) null : AssetPath.JobIconSmall(jobParam));
        if (Object.op_Inequality((Object) this.mName, (Object) null))
          this.mName.text = string.Format(LocalizedText.Get("sys.SUPPORT_SET_UNITNAME"), (object) dataOfClass.unit.UnitParam.name, (object) jobParam.name);
      }
      if (Object.op_Inequality((Object) this.mScore, (Object) null))
        this.mScore.text = string.Format(LocalizedText.Get("sys.SUPPORT_SET_SCORE"), (object) dataOfClass.score);
      if (dataOfClass.unit == null)
        return;
      DataSource.Bind<UnitData>(((Component) this).gameObject, dataOfClass.unit);
    }
  }
}
