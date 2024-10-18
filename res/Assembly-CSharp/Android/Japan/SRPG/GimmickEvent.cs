// Decompiled with JetBrains decompiler
// Type: SRPG.GimmickEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;

namespace SRPG
{
  public class GimmickEvent
  {
    public List<string> skills = new List<string>();
    public List<Unit> users = new List<Unit>();
    public List<Unit> targets = new List<Unit>();
    public List<TrickData> td_targets = new List<TrickData>();
    public GimmickEventCondition condition = new GimmickEventCondition();
    public eGimmickEventType ev_type;
    public string td_iname;
    public string td_tag;
    public int count;
    public bool IsCompleted;
    public bool IsStarter;
    public Unit starter;
  }
}
