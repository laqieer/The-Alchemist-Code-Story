// Decompiled with JetBrains decompiler
// Type: SRPG.CriticalSections
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
