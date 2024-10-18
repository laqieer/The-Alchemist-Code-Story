// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailTag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleUnitDetailTag : MonoBehaviour
  {
    public Text TextValue;

    public void SetTag(string tag)
    {
      if (tag == null)
        tag = string.Empty;
      if (!(bool) ((UnityEngine.Object) this.TextValue))
        return;
      this.TextValue.text = tag;
    }
  }
}
