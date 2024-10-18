// Decompiled with JetBrains decompiler
// Type: Gsc.Purchase.API.Response.ChargeAge
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
