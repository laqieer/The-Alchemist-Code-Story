// Decompiled with JetBrains decompiler
// Type: SRPG.GvGLeagueParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGLeagueParam : GvGMasterParam<JSON_GvGLeagueParam>
  {
    public string Id { get; private set; }

    public string Name { get; private set; }

    public int MinRate { get; private set; }

    public int MaxRate { get; private set; }

    public DateTime BeginAt { get; private set; }

    public DateTime EndAt { get; private set; }

    public string Reward { get; private set; }

    public int SpriteKey { get; private set; }

    public string LeagueIconSpriteKey => "class_" + (object) this.SpriteKey;

    public string LeagueNameSpriteKey => "classname_" + (object) this.SpriteKey;

    public string LeagueBGSpriteKey => "classbg_" + (object) this.SpriteKey;

    public override bool Deserialize(JSON_GvGLeagueParam json)
    {
      if (json == null)
        return false;
      this.Id = json.id;
      this.Name = json.name;
      this.MinRate = json.min_rate;
      this.MaxRate = json.max_rate;
      this.Reward = json.ranking_reward;
      this.SpriteKey = json.sprite_key;
      return true;
    }

    public static GvGLeagueParam GetGvGLeagueParam(string id)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGLeagueParam) null;
      return MonoSingleton<GameManager>.Instance.mGvGLeagueParam?.Find((Predicate<GvGLeagueParam>) (p => p.Id == id));
    }

    public static GvGLeagueParam GetGvGLeagueParam(int rate)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return (GvGLeagueParam) null;
      List<GvGLeagueParam> mGvGleagueParam = MonoSingleton<GameManager>.Instance.mGvGLeagueParam;
      if (mGvGleagueParam == null)
        return (GvGLeagueParam) null;
      for (int index = 0; index < mGvGleagueParam.Count; ++index)
      {
        if (mGvGleagueParam[index].MaxRate == 0 && mGvGleagueParam[index].MinRate <= rate || mGvGleagueParam[index].MinRate == 0 && mGvGleagueParam[index].MaxRate >= rate || mGvGleagueParam[index].MinRate <= rate && rate <= mGvGleagueParam[index].MaxRate)
          return mGvGleagueParam[index];
      }
      return (GvGLeagueParam) null;
    }

    public static List<GvGLeagueParam> GetGvGLeagueParamAll()
    {
      return UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null) ? (List<GvGLeagueParam>) null : MonoSingleton<GameManager>.Instance.mGvGLeagueParam;
    }

    public static string GetGvGLeagueId(int rate)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) MonoSingleton<GameManager>.Instance, (UnityEngine.Object) null))
        return string.Empty;
      GvGLeagueParam gvGleagueParam = GvGLeagueParam.GetGvGLeagueParam(rate);
      return gvGleagueParam != null ? gvGleagueParam.Id : string.Empty;
    }
  }
}
