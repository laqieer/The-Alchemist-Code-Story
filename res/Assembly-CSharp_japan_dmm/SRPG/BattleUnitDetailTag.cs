// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailTag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BattleUnitDetailTag : MonoBehaviour
  {
    public Text TextValue;

    public void SetTag(string tag)
    {
      if (tag == null)
        tag = string.Empty;
      if (!Object.op_Implicit((Object) this.TextValue))
        return;
      this.TextValue.text = tag;
    }
  }
}
