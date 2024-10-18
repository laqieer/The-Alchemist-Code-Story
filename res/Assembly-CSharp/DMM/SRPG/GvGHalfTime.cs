// Decompiled with JetBrains decompiler
// Type: SRPG.GvGHalfTime
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace SRPG
{
  public class GvGHalfTime
  {
    public int Point { get; private set; }

    public int Gid { get; private set; }

    public string Name { get; private set; }

    public bool Deserialize(JSON_GvGHalfTime json)
    {
      if (json == null)
        return false;
      this.Point = json.point;
      this.Gid = json.gid;
      this.Name = json.name;
      return true;
    }
  }
}
