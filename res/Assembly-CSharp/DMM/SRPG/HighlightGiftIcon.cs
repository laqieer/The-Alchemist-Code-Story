// Decompiled with JetBrains decompiler
// Type: SRPG.HighlightGiftIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  public class HighlightGiftIcon : MonoBehaviour
  {
    [SerializeField]
    private GameObject AnimationStamp;
    [SerializeField]
    private GameObject Stamp;

    public void Initialize()
    {
      if (Object.op_Inequality((Object) this.AnimationStamp, (Object) null))
        this.AnimationStamp.SetActive(false);
      if (!Object.op_Inequality((Object) this.Stamp, (Object) null))
        return;
      this.Stamp.SetActive(false);
    }

    public void SetStamp(bool playAnimation)
    {
      if (playAnimation)
      {
        if (Object.op_Inequality((Object) this.AnimationStamp, (Object) null))
          this.AnimationStamp.SetActive(true);
        if (!Object.op_Inequality((Object) this.Stamp, (Object) null))
          return;
        this.Stamp.SetActive(false);
      }
      else
      {
        if (Object.op_Inequality((Object) this.AnimationStamp, (Object) null))
          this.AnimationStamp.SetActive(false);
        if (!Object.op_Inequality((Object) this.Stamp, (Object) null))
          return;
        this.Stamp.SetActive(true);
      }
    }
  }
}
