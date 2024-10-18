// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_VersusViewList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_VersusViewList : MonoBehaviour, ScrollListSetUp
  {
    private readonly string VS_LOBBY_NAME = "vs";
    private readonly string VS_FRIEND_SUFFIX = "_friend";
    public float Space = 1f;
    private List<MyPhoton.MyRoom> m_Rooms = new List<MyPhoton.MyRoom>();
    private int m_Max;

    public void Start()
    {
    }

    public void OnSetUpItems()
    {
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if (instance.CurrentState != MyPhoton.MyState.LOBBY)
        return;
      if (this.m_Rooms == null)
        this.m_Rooms = new List<MyPhoton.MyRoom>();
      this.m_Rooms.Clear();
      List<MyPhoton.MyRoom> roomList = instance.GetRoomList();
      for (int index = 0; index < roomList.Count; ++index)
      {
        if (roomList[index].lobby == this.VS_LOBBY_NAME && roomList[index].name.IndexOf(this.VS_FRIEND_SUFFIX) == -1 && roomList[index].start)
          this.m_Rooms.Add(roomList[index]);
      }
      this.m_Max = this.m_Rooms.Count;
      ScrollListController component1 = this.GetComponent<ScrollListController>();
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this.OnUpdateItems));
      this.GetComponentInParent<ScrollRect>().movementType = ScrollRect.MovementType.Clamped;
      RectTransform component2 = this.GetComponent<RectTransform>();
      Vector2 sizeDelta = component2.sizeDelta;
      Vector2 anchoredPosition = component2.anchoredPosition;
      anchoredPosition.y = 0.0f;
      sizeDelta.y = component1.ItemScale * this.Space * (float) this.m_Max;
      component2.sizeDelta = sizeDelta;
      component2.anchoredPosition = anchoredPosition;
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (idx < 0 || idx >= this.m_Max)
      {
        obj.SetActive(false);
      }
      else
      {
        obj.SetActive(true);
        DataSource.Bind<MyPhoton.MyRoom>(obj, this.m_Rooms[idx]);
        VersusViewRoomInfo component = obj.GetComponent<VersusViewRoomInfo>();
        if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
          return;
        component.Refresh();
      }
    }
  }
}
