﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MapParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;

namespace SRPG
{
  public class MapParam
  {
    private short battleSceneName_index = -1;
    private short bgmName_index = -1;
    public string mapSceneName;
    public string mapSetName;
    public string eventSceneName;

    public string battleSceneName
    {
      set
      {
        this.battleSceneName_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.MapParam_battleSceneName, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.MapParam_battleSceneName, this.battleSceneName_index);
      }
    }

    public string bgmName
    {
      set
      {
        this.bgmName_index = Singleton<ShareVariable>.Instance.str.Set(ShareString.Type.MapParam_bgmName, value);
      }
      get
      {
        return Singleton<ShareVariable>.Instance.str.Get(ShareString.Type.MapParam_bgmName, this.bgmName_index);
      }
    }

    public void Deserialize(JSON_MapParam json)
    {
      if (json == null)
        throw new InvalidJSONException();
      this.mapSceneName = json.scn;
      this.mapSetName = json.set;
      this.battleSceneName = json.btl;
      this.eventSceneName = json.ev;
      this.bgmName = json.bgm;
    }
  }
}
