// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Response.ChargeAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.DOM;
using Gsc.Network;
using Gsc.Purchase.API.App;

#nullable disable
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
