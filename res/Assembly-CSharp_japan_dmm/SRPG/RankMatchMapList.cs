// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMapList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class RankMatchMapList : SRPG_ListBase
  {
    public ListItemEvents ListItem_Normal;

    protected override void Start()
    {
      base.Start();
      if (Object.op_Inequality((Object) this.ListItem_Normal, (Object) null))
        ((Component) this.ListItem_Normal).gameObject.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      this.ClearItems();
      if (Object.op_Equality((Object) this.ListItem_Normal, (Object) null))
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      List<VersusEnableTimeScheduleParam> versusRankMapSchedule = instance.GetVersusRankMapSchedule(instance.RankMatchScheduleId);
      int ymd = TimeManager.ServerTime.ToYMD();
      for (int index1 = 0; index1 < versusRankMapSchedule.Count; ++index1)
      {
        if (versusRankMapSchedule[index1].AddDateList != null && versusRankMapSchedule[index1].AddDateList.Count > 0)
        {
          bool flag = false;
          for (int index2 = 0; index2 < versusRankMapSchedule[index1].AddDateList.Count; ++index2)
          {
            if (versusRankMapSchedule[index1].AddDateList[index2].ToYMD() == ymd)
              flag = true;
          }
          if (!flag)
            continue;
        }
        ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(this.ListItem_Normal);
        DataSource.Bind<VersusEnableTimeScheduleParam>(((Component) listItemEvents).gameObject, versusRankMapSchedule[index1]);
        this.AddItem(listItemEvents);
        ((Component) listItemEvents).transform.SetParent(((Component) this).transform, false);
        ((Component) listItemEvents).gameObject.SetActive(true);
      }
    }
  }
}
