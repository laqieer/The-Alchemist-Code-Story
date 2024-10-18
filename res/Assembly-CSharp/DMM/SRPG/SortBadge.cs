// Decompiled with JetBrains decompiler
// Type: SRPG.SortBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SortBadge : MonoBehaviour
  {
    [FourCC]
    public int ID;
    public Image Icon;
    public Text Value;
    public Text Name;

    public void SetValue(string value)
    {
      if (!Object.op_Inequality((Object) this.Value, (Object) null))
        return;
      this.Value.text = value;
    }

    public void SetValue(int value)
    {
      if (!Object.op_Inequality((Object) this.Value, (Object) null))
        return;
      this.Value.text = value.ToString();
    }

    public void SetName(string name)
    {
      if (!Object.op_Inequality((Object) this.Name, (Object) null))
        return;
      this.Name.text = name;
    }
  }
}
