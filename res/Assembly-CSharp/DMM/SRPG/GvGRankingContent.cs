// Decompiled with JetBrains decompiler
// Type: SRPG.GvGRankingContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGRankingContent : MonoBehaviour
  {
    [SerializeField]
    private int DRAW_STRING_UNDER_RANK = 4;
    [SerializeField]
    private ImageArray mRankImages;
    [SerializeField]
    private Text mRankText;
    [SerializeField]
    private Text mScoreText;

    public void Setup(GvGRankingGuildData guild)
    {
      if (Object.op_Inequality((Object) this.mRankImages, (Object) null))
        this.mRankImages.ImageIndex = Mathf.Clamp(guild.Rank - 1, 0, this.mRankImages.Images.Length - 1);
      if (Object.op_Inequality((Object) this.mRankText, (Object) null) && guild.Rank >= this.DRAW_STRING_UNDER_RANK)
        this.mRankText.text = string.Format(LocalizedText.Get("sys.GVG_RANKING_RANK"), (object) guild.Rank);
      if (Object.op_Inequality((Object) this.mScoreText, (Object) null))
        this.mScoreText.text = guild.Point.ToString();
      DataSource.Bind<ViewGuildData>(((Component) this).gameObject, (ViewGuildData) guild);
    }
  }
}
