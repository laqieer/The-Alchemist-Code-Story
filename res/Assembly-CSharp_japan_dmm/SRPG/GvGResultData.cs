// Decompiled with JetBrains decompiler
// Type: SRPG.GvGResultData
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GvGResultData
  {
    public int Rank { get; private set; }

    public int Point { get; private set; }

    public List<int> CaptureNodes { get; private set; }

    public bool Deserialize(JSON_GvGResult json)
    {
      if (json == null)
        return false;
      this.Rank = json.rank;
      this.Point = json.point;
      this.CaptureNodes = new List<int>();
      if (json.capture_nodes != null)
        this.CaptureNodes.AddRange((IEnumerable<int>) json.capture_nodes);
      return true;
    }
  }
}
