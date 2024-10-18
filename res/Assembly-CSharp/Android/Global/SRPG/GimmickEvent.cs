// Decompiled with JetBrains decompiler
// Type: SRPG.GimmickEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

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
