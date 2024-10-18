// Decompiled with JetBrains decompiler
// Type: SRPG.ReqQuestParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
