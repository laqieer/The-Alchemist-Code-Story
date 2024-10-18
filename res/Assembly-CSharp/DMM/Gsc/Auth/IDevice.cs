// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.IDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.Auth
{
  public interface IDevice
  {
    bool initialized { get; }

    bool hasError { get; }

    string Platform { get; }
  }
}
