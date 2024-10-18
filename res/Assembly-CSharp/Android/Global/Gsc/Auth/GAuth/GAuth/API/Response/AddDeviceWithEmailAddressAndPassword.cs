// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.AddDeviceWithEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class AddDeviceWithEmailAddressAndPassword : GAuthResponse<AddDeviceWithEmailAddressAndPassword>
  {
    public AddDeviceWithEmailAddressAndPassword(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
      {
        this.DeviceId = document.Root["device_id"].ToString();
        this.SecretKey = document.Root["secret_key"].ToString();
      }
    }

    public string DeviceId { get; private set; }

    public string SecretKey { get; private set; }
  }
}
