﻿// Decompiled with JetBrains decompiler
// Type: GooglePlayGames.Native.PInvoke.NativeEndpointDetails
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GooglePlayGames.BasicApi.Nearby;
using GooglePlayGames.Native.Cwrapper;
using System;
using System.Runtime.InteropServices;

namespace GooglePlayGames.Native.PInvoke
{
  internal class NativeEndpointDetails : BaseReferenceHolder
  {
    internal NativeEndpointDetails(IntPtr pointer)
      : base(pointer)
    {
    }

    internal string EndpointId()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnectionTypes.EndpointDetails_GetEndpointId(this.SelfPtr(), out_arg, out_size)));
    }

    internal string DeviceId()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnectionTypes.EndpointDetails_GetDeviceId(this.SelfPtr(), out_arg, out_size)));
    }

    internal string Name()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnectionTypes.EndpointDetails_GetName(this.SelfPtr(), out_arg, out_size)));
    }

    internal string ServiceId()
    {
      return PInvokeUtilities.OutParamsToString((PInvokeUtilities.OutStringMethod) ((out_arg, out_size) => NearbyConnectionTypes.EndpointDetails_GetServiceId(this.SelfPtr(), out_arg, out_size)));
    }

    protected override void CallDispose(HandleRef selfPointer)
    {
      NearbyConnectionTypes.EndpointDetails_Dispose(selfPointer);
    }

    internal EndpointDetails ToDetails()
    {
      return new EndpointDetails(this.EndpointId(), this.DeviceId(), this.Name(), this.ServiceId());
    }

    internal static NativeEndpointDetails FromPointer(IntPtr pointer)
    {
      if (pointer.Equals((object) IntPtr.Zero))
        return (NativeEndpointDetails) null;
      return new NativeEndpointDetails(pointer);
    }
  }
}
