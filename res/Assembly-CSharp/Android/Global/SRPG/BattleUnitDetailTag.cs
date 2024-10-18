// Decompiled with JetBrains decompiler
// Type: SRPG.BattleUnitDetailTag
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
