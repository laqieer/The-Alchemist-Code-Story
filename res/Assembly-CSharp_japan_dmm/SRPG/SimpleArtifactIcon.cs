// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleArtifactIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SimpleArtifactIcon : BaseIcon
  {
    [SerializeField]
    private Text Num;
    [SerializeField]
    private Text HaveNum;

    public override void UpdateValue()
    {
      ArtifactParam dataOfClass = DataSource.FindDataOfClass<ArtifactParam>(((Component) this).gameObject, (ArtifactParam) null);
      if (dataOfClass == null)
        return;
      if (Object.op_Inequality((Object) this.Num, (Object) null))
        this.Num.text = DataSource.FindDataOfClass<int>(((Component) this).gameObject, 0).ToString();
      if (!Object.op_Inequality((Object) this.HaveNum, (Object) null))
        return;
      int artifactNumByRarity = MonoSingleton<GameManager>.Instance.Player.GetArtifactNumByRarity(dataOfClass.iname, dataOfClass.rareini);
      if (artifactNumByRarity <= 0)
        return;
      this.HaveNum.text = LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", (object) artifactNumByRarity);
    }
  }
}
