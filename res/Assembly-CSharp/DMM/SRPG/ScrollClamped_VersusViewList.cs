// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_VersusViewList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_VersusViewList : MonoBehaviour, ScrollListSetUp
  {
    private readonly string VS_LOBBY_NAME = "vs";
    private readonly string VS_FRIEND_SUFFIX = "_friend";
    public float Space = 1f;
    private int m_Max;
    private List<MyPhoton.MyRoom> m_Rooms = new List<MyPhoton.MyRoom>();

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
      ScrollListController component1 = ((Component) this).GetComponent<ScrollListController>();
      // ISSUE: method pointer
      component1.OnItemUpdate.AddListener(new UnityAction<int, GameObject>((object) this, __methodptr(OnUpdateItems)));
      ((Component) this).GetComponentInParent<ScrollRect>().movementType = (ScrollRect.MovementType) 2;
      RectTransform component2 = ((Component) this).GetComponent<RectTransform>();
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
        if (!Object.op_Inequality((Object) component, (Object) null))
          return;
        component.Refresh();
      }
    }
  }
}
