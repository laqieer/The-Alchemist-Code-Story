// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Response.Fulfillment
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
