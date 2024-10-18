// Decompiled with JetBrains decompiler
// Type: SRPG.GeoParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

namespace SRPG
{
  [Serializable]
  public class GeoParam
  {
    public string iname;
    public string name;
    public OInt cost;
    public OBool DisableStopped;

    public bool Deserialize(JSON_GeoParam json)
    {
      if (json == null)
        return false;
      this.iname = json.iname;
      this.name = json.name;
      this.cost = (OInt) Math.Max(json.cost, 1);
      this.DisableStopped = (OBool) (json.stop != 0);
      return true;
    }
  }
}
