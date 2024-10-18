// Decompiled with JetBrains decompiler
// Type: SRPG.GenericSlotFlags
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [DisallowMultipleComponent]
  public class GenericSlotFlags : MonoBehaviour
  {
    [BitMask]
    public GenericSlotFlags.VisibleFlags Flags;

    [System.Flags]
    public enum VisibleFlags
    {
      Empty = 1,
      NonEmpty = 2,
      Locked = 4,
    }
  }
}
