// Decompiled with JetBrains decompiler
// Type: SRPG.GimmickEventCondition
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;

#nullable disable
namespace SRPG
{
  public class GimmickEventCondition
  {
    public GimmickEventTriggerType type;
    public List<Unit> units = new List<Unit>();
    public List<Unit> targets = new List<Unit>();
    public List<TrickData> td_targets = new List<TrickData>();
    public string td_iname;
    public string td_tag;
    public List<Grid> grids;
    public int count;
  }
}
