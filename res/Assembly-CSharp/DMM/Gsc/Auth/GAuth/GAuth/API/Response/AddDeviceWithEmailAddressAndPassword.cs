// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.AddDeviceWithEmailAddressAndPassword
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class AddDeviceWithEmailAddressAndPassword : 
    GAuthResponse<AddDeviceWithEmailAddressAndPassword>
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
