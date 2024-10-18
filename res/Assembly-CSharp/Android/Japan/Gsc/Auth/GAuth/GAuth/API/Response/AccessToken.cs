// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.AccessToken
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class AccessToken : GAuthResponse<AccessToken>
  {
    public AccessToken(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
      {
        this.Token = document.Root["access_token"].ToString();
        this.ExpiresIn = document.Root["expires_in"].ToInt();
      }
    }

    public string Token { get; private set; }

    public int ExpiresIn { get; private set; }
  }
}
