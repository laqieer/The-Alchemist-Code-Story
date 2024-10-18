// Decompiled with JetBrains decompiler
// Type: SRPG.MapParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;

#nullable disable
namespace SRPG
{
  public class MapParam
  {
    public string mapSceneName;
    public string mapSetName;
    private short battleSceneName_index = -1;
    public string eventSceneName;
    private short bgmName_index = -1;

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
      this.mapSceneName = json != null ? json.scn : throw new InvalidJSONException();
      this.mapSetName = json.set;
      this.battleSceneName = json.btl;
      this.eventSceneName = json.ev;
      this.bgmName = json.bgm;
    }
  }
}
