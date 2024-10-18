// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.AddDevice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class AddDevice : GAuthResponse<AddDevice>
  {
    public AddDevice(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.IsSucceeded = document.Root["is_succeeded"].ToBool();
    }

    public bool IsSucceeded { get; private set; }
  }
}
