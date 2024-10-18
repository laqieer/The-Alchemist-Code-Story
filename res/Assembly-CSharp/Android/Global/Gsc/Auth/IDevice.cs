// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.IDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace Gsc.Auth
{
  public interface IDevice
  {
    bool initialized { get; }

    bool hasError { get; }

    string Platform { get; }
  }
}
