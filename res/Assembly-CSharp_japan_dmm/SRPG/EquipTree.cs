// Decompiled with JetBrains decompiler
// Type: SRPG.EquipTree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class EquipTree : MonoBehaviour
  {
    public Image CursorImage;

    public void SetIsLast(bool is_last) => ((Behaviour) this.CursorImage).enabled = is_last;
  }
}
