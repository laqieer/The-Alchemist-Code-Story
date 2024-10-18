// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Response.ChargeAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Purchase.API.App;

namespace Gsc.Purchase.API.Response
{
  public class ChargeAge : GenericResponse<ChargeAge>
  {
    public ChargeAge(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.Age = document.Root["age"].ToInt();
    }

    public int Age { get; private set; }
  }
}
