// Decompiled with JetBrains decompiler
// Type: SRPG.SortBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SortBadge : MonoBehaviour
  {
    [FourCC]
    public int ID;
    public Image Icon;
    public Text Value;

    public void SetValue(string value)
    {
      if (!((UnityEngine.Object) this.Value != (UnityEngine.Object) null))
        return;
      this.Value.text = value;
    }

    public void SetValue(int value)
    {
      if (!((UnityEngine.Object) this.Value != (UnityEngine.Object) null))
        return;
      this.Value.text = value.ToString();
    }
  }
}
