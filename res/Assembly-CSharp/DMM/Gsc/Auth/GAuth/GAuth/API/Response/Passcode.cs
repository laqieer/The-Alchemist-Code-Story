// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.Passcode
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

#nullable disable
namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class Passcode : GAuthResponse<Passcode>
  {
    public Passcode(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
      {
        this.Code = document.Root["passcode"].ToString();
        this.ExpiresIn = document.Root["expires_in"].ToInt();
      }
    }

    public string Code { get; private set; }

    public int ExpiresIn { get; private set; }
  }
}
