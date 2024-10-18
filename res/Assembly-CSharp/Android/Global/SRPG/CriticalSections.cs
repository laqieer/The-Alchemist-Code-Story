// Decompiled with JetBrains decompiler
// Type: SRPG.CriticalSections
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Flags]
  public enum CriticalSections
  {
    Default = 1,
    Network = 2,
    SceneChange = 4,
    ExDownload = 8,
  }
}
