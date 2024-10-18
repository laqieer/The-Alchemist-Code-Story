// Decompiled with JetBrains decompiler
// Type: SRPG.SimpleArtifactIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

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
      ArtifactParam dataOfClass = DataSource.FindDataOfClass<ArtifactParam>(this.gameObject, (ArtifactParam) null);
      if (dataOfClass == null)
        return;
      if ((UnityEngine.Object) this.Num != (UnityEngine.Object) null)
        this.Num.text = DataSource.FindDataOfClass<int>(this.gameObject, 0).ToString();
      if (!((UnityEngine.Object) this.HaveNum != (UnityEngine.Object) null))
        return;
      int artifactNumByRarity = MonoSingleton<GameManager>.Instance.Player.GetArtifactNumByRarity(dataOfClass.iname, dataOfClass.rareini);
      if (artifactNumByRarity <= 0)
        return;
      this.HaveNum.text = LocalizedText.Get("sys.QUESTRESULT_REWARD_ITEM_HAVE", new object[1]
      {
        (object) artifactNumByRarity
      });
    }
  }
}
