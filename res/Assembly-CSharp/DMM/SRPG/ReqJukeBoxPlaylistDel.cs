// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJukeBoxPlaylistDel
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqJukeBoxPlaylistDel : WebAPI
  {
    public ReqJukeBoxPlaylistDel(
      string[] bgm_list,
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod type)
    {
      this.name = "gallery/jukebox/delete";
      this.body = WebAPI.GetRequestString<ReqJukeBoxPlaylistDel.RequestParam>(new ReqJukeBoxPlaylistDel.RequestParam()
      {
        bgms = bgm_list
      });
      this.callback = response;
      this.serializeCompressMethod = type;
    }

    [Serializable]
    public class RequestParam
    {
      public string[] bgms;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public JukeBoxWindow.ResPlayList[] playlists;
    }
  }
}
