// Decompiled with JetBrains decompiler
// Type: SRPG.HighlightGiftIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

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
      if ((UnityEngine.Object) this.AnimationStamp != (UnityEngine.Object) null)
        this.AnimationStamp.SetActive(false);
      if (!((UnityEngine.Object) this.Stamp != (UnityEngine.Object) null))
        return;
      this.Stamp.SetActive(false);
    }

    public void SetStamp(bool playAnimation)
    {
      if (playAnimation)
      {
        if ((UnityEngine.Object) this.AnimationStamp != (UnityEngine.Object) null)
          this.AnimationStamp.SetActive(true);
        if (!((UnityEngine.Object) this.Stamp != (UnityEngine.Object) null))
          return;
        this.Stamp.SetActive(false);
      }
      else
      {
        if ((UnityEngine.Object) this.AnimationStamp != (UnityEngine.Object) null)
          this.AnimationStamp.SetActive(false);
        if (!((UnityEngine.Object) this.Stamp != (UnityEngine.Object) null))
          return;
        this.Stamp.SetActive(true);
      }
    }
  }
}
