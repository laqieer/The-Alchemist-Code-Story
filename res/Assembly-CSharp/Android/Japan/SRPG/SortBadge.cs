// Decompiled with JetBrains decompiler
// Type: SRPG.SortBadge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
    public Text Name;

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

    public void SetName(string name)
    {
      if (!((UnityEngine.Object) this.Name != (UnityEngine.Object) null))
        return;
      this.Name.text = name;
    }
  }
}
