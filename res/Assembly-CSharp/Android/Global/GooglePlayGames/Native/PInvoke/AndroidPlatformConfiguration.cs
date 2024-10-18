﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.AndroidPlatformConfiguration
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using AOT;
using GooglePlayGames.Native.Cwrapper;
using GooglePlayGames.OurUtils;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal sealed class AndroidPlatformConfiguration : PlatformConfiguration
  {
    private AndroidPlatformConfiguration(IntPtr selfPointer)
      : base(selfPointer)
    {
    }

    internal void SetActivity(IntPtr activity)
    {
      GooglePlayGames.Native.Cwrapper.AndroidPlatformConfiguration.AndroidPlatformConfiguration_SetActivity(this.SelfPtr(), activity);
    }

    internal void SetOptionalIntentHandlerForUI(Action<IntPtr> intentHandler)
    {
      Misc.CheckNotNull<Action<IntPtr>>(intentHandler);
      GooglePlayGames.Native.Cwrapper.AndroidPlatformConfiguration.AndroidPlatformConfiguration_SetOptionalIntentHandlerForUI(this.SelfPtr(), new GooglePlayGames.Native.Cwrapper.AndroidPlatformConfiguration.IntentHandler(AndroidPlatformConfiguration.InternalIntentHandler), Callbacks.ToIntPtr((Delegate) intentHandler));
    }

    internal void EnableAppState()
    {
      InternalHooks.InternalHooks_EnableAppState(this.SelfPtr());
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      GooglePlayGames.Native.Cwrapper.AndroidPlatformConfiguration.AndroidPlatformConfiguration_Dispose(selfPointer);
    }

    [MonoPInvokeCallback(typeof (AndroidPlatformConfiguration.IntentHandlerInternal))]
    private static void InternalIntentHandler(IntPtr intent, IntPtr userData)
    {
      Callbacks.PerformInternalCallback("AndroidPlatformConfiguration#InternalIntentHandler", Callbacks.Type.Permanent, intent, userData);
    }

    internal static AndroidPlatformConfiguration Create()
    {
      return new AndroidPlatformConfiguration(GooglePlayGames.Native.Cwrapper.AndroidPlatformConfiguration.AndroidPlatformConfiguration_Construct());
    }

    private delegate void IntentHandlerInternal(IntPtr intent, IntPtr userData);
  }
}
