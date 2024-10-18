// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.PlatformConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal abstract class PlatformConfiguration : BaseReferenceHolder
  {
    protected PlatformConfiguration(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    internal HandleRef AsHandle()
    {
      return this.SelfPtr();
    }
  }
}
