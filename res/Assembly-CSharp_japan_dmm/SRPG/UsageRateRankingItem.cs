// Decompiled with JetBrains decompiler
// Type: SRPG.UsageRateRankingItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
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
      data1.Setup(unitParam.iname, 0, 1, 1, jobParam.iname, elem: unitParam.element);
      DataSource.Bind<UnitData>(((Component) this).gameObject, data1);
      if (Object.op_Inequality((Object) this.rank, (Object) null))
        this.rank.text = LocalizedText.Get("sys.RANKING_RANK", (object) rank_num);
      if (Object.op_Inequality((Object) this.unit_name, (Object) null))
        this.unit_name.text = LocalizedText.Get("sys.RANKING_UNIT_NAME", (object) data1.UnitParam.name, (object) jobParam.name);
      ((Behaviour) this.RankIconArray).enabled = this.RankIconArray.Images.Length >= rank_num;
      ((Behaviour) this.rank).enabled = !((Behaviour) this.RankIconArray).enabled;
      if (Object.op_Inequality((Object) this.JobIcon, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync((RawImage) this.JobIcon, jobParam == null ? (string) null : AssetPath.JobIconSmall(jobParam));
      if (!((Behaviour) this.RankIconArray).enabled)
        return;
      this.RankIconArray.ImageIndex = rank_num - 1;
    }
  }
}
