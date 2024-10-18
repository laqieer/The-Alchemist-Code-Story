// Decompiled with JetBrains decompiler
// Type: SRPG.ReqQuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

namespace SRPG
{
  public class ReqQuestParam : WebAPI
  {
    public ReqQuestParam(Network.ResponseCallback response)
    {
      this.name = "mst/10/quest";
      this.callback = response;
    }
  }
}
