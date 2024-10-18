// Decompiled with JetBrains decompiler
// Type: SRPG.EquipTree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class EquipTree : MonoBehaviour
  {
    public Image CursorImage;

    public void SetIsLast(bool is_last)
    {
      this.CursorImage.enabled = is_last;
    }
  }
}
