// Decompiled with JetBrains decompiler
// Type: SRPG.VersusCoinCampParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  public class VersusCoinCampParam
  {
    public string iname;
    public DateTime begin_at;
    public DateTime end_at;
    public int coinrate;

    public bool Deserialize(JSON_VersusCoinCampParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.coinrate = json.rate <= 0 ? 1 : json.rate;
      try
      {
        if (!string.IsNullOrEmpty(json.begin_at))
          this.begin_at = DateTime.Parse(json.begin_at);
        if (!string.IsNullOrEmpty(json.end_at))
          this.end_at = DateTime.Parse(json.end_at);
      }
      catch (Exception ex)
      {
        DebugUtility.LogError(ex.Message);
        return false;
      }
      return true;
    }
  }
}
