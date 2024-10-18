// Decompiled with JetBrains decompiler
// Type: SRPG.ReqTutUpdate
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Text;

namespace SRPG
{
  public class ReqTutUpdate : WebAPI
  {
    public ReqTutUpdate(long flags, Network.ResponseCallback response)
    {
      this.name = "tut/update";
      StringBuilder stringBuilder = WebAPI.GetStringBuilder();
      stringBuilder.Append("\"tut\":");
      stringBuilder.Append(flags);
      this.body = WebAPI.GetRequestString(stringBuilder.ToString());
      this.callback = response;
    }
  }
}
