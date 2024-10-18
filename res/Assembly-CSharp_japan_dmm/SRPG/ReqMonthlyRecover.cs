// Decompiled with JetBrains decompiler
// Type: SRPG.ReqMonthlyRecover
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqMonthlyRecover : WebAPI
  {
    public ReqMonthlyRecover(
      string select_iname,
      int select_dau,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "loginbonus/monthly/recover";
      this.body = WebAPI.GetRequestString<ReqMonthlyRecover.RequestParam>(new ReqMonthlyRecover.RequestParam()
      {
        iname = select_iname,
        day = select_dau
      });
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string iname;
      public int day;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_Notify_Monthly notify;
    }
  }
}
