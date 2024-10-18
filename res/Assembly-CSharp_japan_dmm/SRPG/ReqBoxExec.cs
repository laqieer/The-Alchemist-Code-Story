// Decompiled with JetBrains decompiler
// Type: SRPG.ReqBoxExec
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
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
      public JSON_TrophyProgress[] trophyprogs;
      public JSON_TrophyProgress[] bingoprogs;
    }
  }
}
