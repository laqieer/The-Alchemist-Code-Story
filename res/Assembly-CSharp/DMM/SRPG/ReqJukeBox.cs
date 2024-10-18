// Decompiled with JetBrains decompiler
// Type: SRPG.ReqJukeBox
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using Gsc.Network.Encoding;
using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  public class ReqJukeBox : WebAPI
  {
    public ReqJukeBox(
      SRPG.Network.ResponseCallback response,
      EncodingTypes.ESerializeCompressMethod type)
    {
      this.name = "gallery/jukebox";
      this.body = WebAPI.GetRequestString(string.Empty);
      this.callback = response;
      this.serializeCompressMethod = type;
    }

    [MessagePackObject(true)]
    [Serializable]
    public class Response
    {
      public string[] bgms;
      public JukeBoxWindow.ResPlayList[] playlists;
    }
  }
}
