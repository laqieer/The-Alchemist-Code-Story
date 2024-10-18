// Decompiled with JetBrains decompiler
// Type: Fabric.Runtime.Internal.Impl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Fabric.Internal.Runtime;

namespace Fabric.Runtime.Internal
{
  internal class Impl
  {
    protected const string Name = "Fabric";

    public static Impl Make()
    {
      return (Impl) new AndroidImpl();
    }

    public virtual string Initialize()
    {
      Utils.Log("Fabric", "Method Initialize () is unimplemented on this platform");
      return string.Empty;
    }
  }
}
