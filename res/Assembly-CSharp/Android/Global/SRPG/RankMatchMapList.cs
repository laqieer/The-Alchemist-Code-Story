// Decompiled with JetBrains decompiler
// Type: SRPG.RankMatchMapList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;

namespace SRPG
{
  public class RankMatchMapList : SRPG_ListBase
  {
    public ListItemEvents ListItem_Normal;

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) this.ListItem_Normal != (UnityEngine.Object) null)
        this.ListItem_Normal.gameObject.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      this.ClearItems();
      if ((UnityEngine.Object) this.ListItem_Normal == (UnityEngine.Object) null)
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
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ListItem_Normal);
        DataSource.Bind<VersusEnableTimeScheduleParam>(listItemEvents.gameObject, versusRankMapSchedule[index1]);
        this.AddItem(listItemEvents);
        listItemEvents.transform.SetParent(this.transform, false);
        listItemEvents.gameObject.SetActive(true);
      }
    }
  }
}
