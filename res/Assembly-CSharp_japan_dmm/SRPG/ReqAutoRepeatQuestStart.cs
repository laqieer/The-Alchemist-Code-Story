// Decompiled with JetBrains decompiler
// Type: SRPG.ReqAutoRepeatQuestStart
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ReqAutoRepeatQuestStart : WebAPI
  {
    public ReqAutoRepeatQuestStart(
      string iname,
      int lap_num,
      bool is_full_box,
      bool use_drip,
      bool use_notification,
      Vector2 location,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod serializeCompressMethod)
    {
      this.name = "btl/auto_repeat/req";
      ReqAutoRepeatQuestStart.RequestParam requestParam = new ReqAutoRepeatQuestStart.RequestParam()
      {
        quest_iname = iname,
        lap_num = lap_num,
        is_full_box = !is_full_box ? 0 : 1,
        use_drip = !use_drip ? 0 : 1,
        is_push = !use_notification ? 0 : 1,
        location = new ReqAutoRepeatQuestStart.Location()
      };
      requestParam.location.lat = location.x;
      requestParam.location.lng = location.y;
      this.body = WebAPI.GetRequestString<ReqAutoRepeatQuestStart.RequestParam>(requestParam);
      this.callback = response;
      this.serializeCompressMethod = serializeCompressMethod;
    }

    [Serializable]
    public class RequestParam
    {
      public string quest_iname;
      public int lap_num;
      public int is_full_box;
      public int use_drip;
      public int is_push;
      public ReqAutoRepeatQuestStart.Location location;
    }

    [Serializable]
    public class Location
    {
      public float lat;
      public float lng;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public Json_AutoRepeatQuestData auto_repeat;
      public Json_PlayerData player;
      public Json_Item[] items;
    }
  }
}
