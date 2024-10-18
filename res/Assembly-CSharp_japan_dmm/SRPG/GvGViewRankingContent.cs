// Decompiled with JetBrains decompiler
// Type: SRPG.GvGViewRankingContent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GvGViewRankingContent : MonoBehaviour
  {
    [SerializeField]
    private ImageArray mRankImages;
    [SerializeField]
    private Text mRankText;
    [SerializeField]
    private Text mName;
    [SerializeField]
    private Text mPoint;
    private const int RANK_MAX = 3;

    private void Start() => this.Refresh();

    public void Refresh()
    {
      GvGRankingGuildData dataOfClass = DataSource.FindDataOfClass<GvGRankingGuildData>(((Component) this).gameObject, (GvGRankingGuildData) null);
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.mRankImages, (Object) null))
        this.mRankImages.ImageIndex = Mathf.Clamp(dataOfClass.Rank - 1, 0, this.mRankImages.Images.Length - 1);
      if (Object.op_Inequality((Object) this.mRankText, (Object) null))
        this.mRankText.text = 3 > dataOfClass.Rank ? string.Format(LocalizedText.Get("sys.GVG_SET_RANK"), (object) dataOfClass.Rank.ToString()) : string.Empty;
      if (Object.op_Inequality((Object) this.mName, (Object) null))
        this.mName.text = dataOfClass.name;
      if (!Object.op_Inequality((Object) this.mPoint, (Object) null))
        return;
      this.mPoint.text = dataOfClass.Point.ToString();
    }
  }
}
