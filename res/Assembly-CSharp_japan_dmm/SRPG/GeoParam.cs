// Decompiled with JetBrains decompiler
// Type: SRPG.GeoParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack;
using System;

#nullable disable
namespace SRPG
{
  [MessagePackObject(true)]
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
