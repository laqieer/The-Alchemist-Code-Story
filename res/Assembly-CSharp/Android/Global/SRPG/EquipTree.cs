// Decompiled with JetBrains decompiler
// Type: SRPG.EquipTree
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
