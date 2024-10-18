// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class ArenaHistory : SRPG_ListBase
  {
    public ListItemEvents ListItem_Normal;
    public ListItemEvents ListItem_Self;
    public GameObject DetailWindow;

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
      Transform transform = ((Component) this).transform;
      ArenaPlayerHistory[] arenaHistory = MonoSingleton<GameManager>.Instance.GetArenaHistory();
      for (int index = 0; index < arenaHistory.Length; ++index)
      {
        ListItemEvents listItemEvents = Object.Instantiate<ListItemEvents>(this.ListItem_Normal);
        DataSource.Bind<ArenaPlayerHistory>(((Component) listItemEvents).gameObject, arenaHistory[index]);
        DataSource.Bind<ArenaPlayer>(((Component) listItemEvents).gameObject, arenaHistory[index].enemy);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        listItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
        this.AddItem(listItemEvents);
        ((Component) listItemEvents).transform.SetParent(transform, false);
        ((Component) listItemEvents).gameObject.SetActive(true);
      }
    }

    private void OnItemSelect(GameObject go)
    {
    }

    private void OnItemDetail(GameObject go)
    {
      if (Object.op_Equality((Object) this.DetailWindow, (Object) null))
        return;
      ArenaPlayerHistory dataOfClass = DataSource.FindDataOfClass<ArenaPlayerHistory>(go, (ArenaPlayerHistory) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject = Object.Instantiate<GameObject>(this.DetailWindow);
      DataSource.Bind<ArenaPlayer>(gameObject, dataOfClass.enemy);
      gameObject.GetComponent<ArenaPlayerInfo>().UpdateValue();
    }
  }
}
