// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBoxExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class ReqBoxExec : WebAPI
  {
    public ReqBoxExec(string box_iname, int lottery_num, Network.ResponseCallback response)
    {
      this.name = "box_lottery/exec";
      this.body = WebAPI.GetRequestString<ReqBoxExec.RequestParam>(new ReqBoxExec.RequestParam()
      {
        box_iname = box_iname,
        lottery_num = lottery_num
      });
      this.callback = response;
    }

    [Serializable]
    public class RequestParam
    {
      public string box_iname;
      public int lottery_num;
    }

    [Serializable]
    public class Response
    {
      public string box_iname;
      public int step;
      public int total_num;
      public int remain_num;
      public int is_reset_enable;
      public Json_GachaReceipt receipt;
      public Json_DropInfo[] add;
      public Json_DropInfo[] add_mail;
      public Json_PlayerData player;
      public Json_Item[] items;
      public Json_Unit[] units;
      public Json_Artifact[] artifacts;
    }
  }
}
