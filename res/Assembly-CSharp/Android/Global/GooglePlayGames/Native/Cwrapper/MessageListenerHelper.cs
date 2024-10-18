// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.Cwrapper.MessageListenerHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.Cwrapper
{
  internal static class MessageListenerHelper
  {
    [DllImport("gpg")]
    internal static extern void MessageListenerHelper_SetOnMessageReceivedCallback(HandleRef self, MessageListenerHelper.OnMessageReceivedCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern void MessageListenerHelper_SetOnDisconnectedCallback(HandleRef self, MessageListenerHelper.OnDisconnectedCallback callback, IntPtr callback_arg);

    [DllImport("gpg")]
    internal static extern IntPtr MessageListenerHelper_Construct();

    [DllImport("gpg")]
    internal static extern void MessageListenerHelper_Dispose(HandleRef self);

    internal delegate void OnMessageReceivedCallback(long arg0, string arg1, IntPtr arg2, UIntPtr arg3, [MarshalAs(UnmanagedType.I1)] bool arg4, IntPtr arg5);

    internal delegate void OnDisconnectedCallback(long arg0, string arg1, IntPtr arg2);
  }
}
