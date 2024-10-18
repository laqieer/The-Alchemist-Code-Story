// Decompiled with JetBrains decompiler
// Type: SRPG.UsageRateRankingItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class UsageRateRankingItem : MonoBehaviour
  {
    public Text rank;
    public Text unit_name;
    public ImageArray RankIconArray;
    public RawImage_Transparent JobIcon;

    public void Refresh(int rank_num, RankingUnitData data)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      JobParam jobParam = instance.GetJobParam(data.job_iname);
      UnitParam unitParam = instance.GetUnitParam(data.unit_iname);
      UnitData data1 = new UnitData();
      data1.Setup(unitParam.iname, 0, 1, 1, jobParam.iname, 1, unitParam.element);
      DataSource.Bind<UnitData>(this.gameObject, data1);
      if ((UnityEngine.Object) this.rank != (UnityEngine.Object) null)
        this.rank.text = LocalizedText.Get("sys.RANKING_RANK", new object[1]
        {
          (object) rank_num
        });
      if ((UnityEngine.Object) this.unit_name != (UnityEngine.Object) null)
        this.unit_name.text = LocalizedText.Get("sys.RANKING_UNIT_NAME", (object) data1.UnitParam.name, (object) jobParam.name);
      this.RankIconArray.enabled = this.RankIconArray.Images.Length >= rank_num;
      this.rank.enabled = !this.RankIconArray.enabled;
      if ((UnityEngine.Object) this.JobIcon != (UnityEngine.Object) null)
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync((RawImage) this.JobIcon, jobParam == null ? (string) null : AssetPath.JobIconSmall(jobParam));
      if (!this.RankIconArray.enabled)
        return;
      this.RankIconArray.ImageIndex = rank_num - 1;
    }
  }
}
