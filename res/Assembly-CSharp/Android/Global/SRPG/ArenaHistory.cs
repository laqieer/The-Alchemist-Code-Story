// Decompiled with JetBrains decompiler
// Type: SRPG.ArenaHistory
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

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
      if ((UnityEngine.Object) this.ListItem_Normal != (UnityEngine.Object) null)
        this.ListItem_Normal.gameObject.SetActive(false);
      this.Refresh();
    }

    private void Refresh()
    {
      this.ClearItems();
      if ((UnityEngine.Object) this.ListItem_Normal == (UnityEngine.Object) null)
        return;
      Transform transform = this.transform;
      ArenaPlayerHistory[] arenaHistory = MonoSingleton<GameManager>.Instance.GetArenaHistory();
      for (int index = 0; index < arenaHistory.Length; ++index)
      {
        ListItemEvents listItemEvents = UnityEngine.Object.Instantiate<ListItemEvents>(this.ListItem_Normal);
        DataSource.Bind<ArenaPlayerHistory>(listItemEvents.gameObject, arenaHistory[index]);
        DataSource.Bind<ArenaPlayer>(listItemEvents.gameObject, arenaHistory[index].enemy);
        listItemEvents.OnSelect = new ListItemEvents.ListItemEvent(this.OnItemSelect);
        listItemEvents.OnOpenDetail = new ListItemEvents.ListItemEvent(this.OnItemDetail);
        this.AddItem(listItemEvents);
        listItemEvents.transform.SetParent(transform, false);
        listItemEvents.gameObject.SetActive(true);
      }
    }

    private void OnItemSelect(GameObject go)
    {
    }

    private void OnItemDetail(GameObject go)
    {
      if ((UnityEngine.Object) this.DetailWindow == (UnityEngine.Object) null)
        return;
      ArenaPlayerHistory dataOfClass = DataSource.FindDataOfClass<ArenaPlayerHistory>(go, (ArenaPlayerHistory) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.DetailWindow);
      DataSource.Bind<ArenaPlayer>(gameObject, dataOfClass.enemy);
      gameObject.GetComponent<ArenaPlayerInfo>().UpdateValue();
    }
  }
}
