// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Response.Fulfillment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Purchase.API.App;

#nullable disable
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
