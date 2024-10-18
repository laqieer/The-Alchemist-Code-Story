// Decompiled with JetBrains decompiler
// Type: SRPG.ReqUnitUnlock
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqUnitUnlock : WebAPI
  {
    public ReqUnitUnlock(string iname, Network.ResponseCallback response)
    {
      this.name = "unit/add";
      this.body = WebAPI.GetRequestString("\"iname\":\"" + iname + "\"");
      this.callback = response;
    }
  }
}
