// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Response.Fulfillment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Purchase.API.App;

namespace Gsc.Purchase.API.Response
{
  public class Fulfillment : GenericResponse<Fulfillment>
  {
    public Fulfillment(WebInternalResponse response)
    {
      using (IDocument document = this.Parse(response))
        this.Result = new FulfillmentResult(document.Root.GetObject());
    }

    public FulfillmentResult Result { get; private set; }
  }
}
