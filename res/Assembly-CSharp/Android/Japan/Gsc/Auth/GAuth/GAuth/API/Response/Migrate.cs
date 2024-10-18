// Decompiled with JetBrains decompiler
// Type: Gsc.Auth.GAuth.GAuth.API.Response.Migrate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.Auth.GAuth.GAuth.API.Generic;
using Gsc.DOM;
using Gsc.Network;

namespace Gsc.Auth.GAuth.GAuth.API.Response
{
  public class Migrate : GAuthResponse<Migrate>
  {
    public Migrate(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.OldDeviceId = document.Root["old_device_id"].ToString();
    }

    public string OldDeviceId { get; private set; }
  }
}
