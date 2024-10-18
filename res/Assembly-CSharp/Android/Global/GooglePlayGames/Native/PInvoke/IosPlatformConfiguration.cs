// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.IosPlatformConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.OurUtils;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal sealed class IosPlatformConfiguration : PlatformConfiguration
  {
    private IosPlatformConfiguration(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    internal void SetClientId(string clientId)
    {
      Misc.CheckNotNull<string>(clientId);
      GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration.IosPlatformConfiguration_SetClientID(this.SelfPtr(), clientId);
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration.IosPlatformConfiguration_Dispose(selfPointer);
    }

    internal static IosPlatformConfiguration Create()
    {
      return new IosPlatformConfiguration(GooglePlayGames.Native.Cwrapper.IosPlatformConfiguration.IosPlatformConfiguration_Construct());
    }
  }
}
