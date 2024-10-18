﻿// Decompiled with JetBrains decompiler
// Type: SRPG.MultiInvitationSendWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiInvitationSendWindow : FlowWindowBase
  {
    private static MultiInvitationSendWindow m_Instance = (MultiInvitationSendWindow) null;
    private static List<string> m_Invited = new List<string>();
    private List<string> m_SendList = new List<string>();
    private const int CHECK_MAX = 5;
    private MultiInvitationSendWindow.SerializeParam m_Param;
    private bool m_Destroy;
    private MultiInvitationSendWindow.Content.ItemSource m_ContentSource;
    private ContentController m_ContentController;
    private SerializeValueBehaviour m_ValueList;

    public override string name
    {
      get
      {
        return "FriendInvicationSendWindow";
      }
    }

    public static MultiInvitationSendWindow instance
    {
      get
      {
        return MultiInvitationSendWindow.m_Instance;
      }
    }

    public static void ClearInvited()
    {
      MultiInvitationSendWindow.m_Invited.Clear();
    }

    public static void AddInvited(string uid)
    {
      MultiInvitationSendWindow.m_Invited.Add(uid);
    }

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      MultiInvitationSendWindow.m_Instance = this;
      base.Initialize(param);
      this.m_Param = param as MultiInvitationSendWindow.SerializeParam;
      if (this.m_Param == null)
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      this.m_ValueList = this.m_Param.window.GetComponent<SerializeValueBehaviour>();
      if ((UnityEngine.Object) this.m_ValueList != (UnityEngine.Object) null)
        this.m_ValueList.list.SetField("checkmax", 5);
      if ((UnityEngine.Object) this.m_Param.list != (UnityEngine.Object) null)
      {
        this.m_ContentController = this.m_Param.list.GetComponentInChildren<ContentController>();
        if ((UnityEngine.Object) this.m_ContentController != (UnityEngine.Object) null)
          this.m_ContentController.SetWork((object) this);
      }
      this.Close(true);
    }

    public override void Release()
    {
      this.ReleaseContentList();
      base.Release();
      MultiInvitationSendWindow.m_Instance = (MultiInvitationSendWindow) null;
    }

    public override int Update()
    {
      base.Update();
      if (this.m_Destroy && this.isClosed)
      {
        this.SetActiveChild(false);
        return 191;
      }
      this.RefreshUI();
      return -1;
    }

    private void RefreshUI()
    {
      if (this.m_ContentSource == null)
        return;
      int count = this.m_ContentSource.GetCount();
      if (this.m_SendList.Count >= 5)
      {
        for (int index = 0; index < count; ++index)
        {
          MultiInvitationSendWindow.Content.ItemSource.ItemParam param = this.m_ContentSource.GetParam(index) as MultiInvitationSendWindow.Content.ItemSource.ItemParam;
          if (param != null && param.IsValid())
          {
            if (this.m_SendList.FindIndex((Predicate<string>) (prop => prop == param.friend.UID)) == -1)
              param.accessor.SetHatch(false);
            else
              param.accessor.SetHatch(true);
          }
        }
      }
      else
      {
        for (int index = 0; index < count; ++index)
        {
          MultiInvitationSendWindow.Content.ItemSource.ItemParam itemParam = this.m_ContentSource.GetParam(index) as MultiInvitationSendWindow.Content.ItemSource.ItemParam;
          if (itemParam != null && itemParam.IsValid())
            itemParam.accessor.SetHatch(true);
        }
      }
      if (!((UnityEngine.Object) this.m_ValueList != (UnityEngine.Object) null))
        return;
      this.m_ValueList.list.SetField("checknum", this.m_SendList.Count);
      this.m_ValueList.list.SetInteractable("btn_invication", this.m_SendList.Count > 0);
    }

    public void InitializeContentList()
    {
      this.ReleaseContentList();
      if (!((UnityEngine.Object) this.m_ContentController != (UnityEngine.Object) null))
        return;
      this.m_ContentSource = new MultiInvitationSendWindow.Content.ItemSource();
      List<FriendData> list = new List<FriendData>((IEnumerable<FriendData>) MonoSingleton<GameManager>.Instance.Player.Friends);
      MyPhoton instance = PunMonoSingleton<MyPhoton>.Instance;
      if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
      {
        List<MyPhoton.MyPlayer> roomPlayerList = instance.GetRoomPlayerList();
        for (int index1 = 0; index1 < roomPlayerList.Count; ++index1)
        {
          if (roomPlayerList[index1] != null && !string.IsNullOrEmpty(roomPlayerList[index1].json))
          {
            JSON_MyPhotonPlayerParam param = JSON_MyPhotonPlayerParam.Parse(roomPlayerList[index1].json);
            if (param != null)
            {
              int index2 = list.FindIndex((Predicate<FriendData>) (prop => prop.UID == param.UID));
              if (index2 != -1)
                list.RemoveAt(index2);
            }
          }
        }
      }
      for (int index = 0; index < list.Count; ++index)
      {
        FriendData data = list[index];
        bool flag = false;
        if (data != null)
        {
          if (MultiInvitationSendWindow.m_Invited.FindIndex((Predicate<string>) (prop => prop == data.UID)) != -1)
            flag = true;
          else if (!data.MultiPush)
            flag = true;
          else if (TimeManager.GetUnixSec(DateTime.Now) - data.LastLogin > 86400L)
            flag = true;
        }
        else
          flag = true;
        if (flag)
        {
          list.RemoveAt(index);
          --index;
        }
      }
      SortUtility.StableSort<FriendData>(list, (Comparison<FriendData>) ((p1, p2) => (!p1.IsFavorite ? p1.LastLogin : long.MaxValue).CompareTo(!p2.IsFavorite ? p2.LastLogin : long.MaxValue)));
      list.Reverse();
      for (int index = 0; index < list.Count; ++index)
      {
        FriendData friend = list[index];
        if (friend != null)
        {
          MultiInvitationSendWindow.Content.ItemSource.ItemParam itemParam = new MultiInvitationSendWindow.Content.ItemSource.ItemParam(friend);
          if (itemParam.IsValid())
            this.m_ContentSource.Add(itemParam);
        }
      }
      this.m_ContentController.Initialize((ContentSource) this.m_ContentSource, Vector2.zero);
    }

    public void ReleaseContentList()
    {
      if ((UnityEngine.Object) this.m_ContentController != (UnityEngine.Object) null)
        this.m_ContentController.Release();
      this.m_ContentSource = (MultiInvitationSendWindow.Content.ItemSource) null;
      this.m_SendList.Clear();
    }

    public string[] GetSendList()
    {
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.m_SendList.Count; ++index)
      {
        if (this.m_SendList[index] != null)
          stringList.Add(this.m_SendList[index]);
      }
      return stringList.ToArray();
    }

    public bool IsSendList(string uid)
    {
      return this.m_SendList.FindIndex((Predicate<string>) (prop => prop == uid)) != -1;
    }

    public override int OnActivate(int pinId)
    {
      switch (pinId)
      {
        case 100:
          this.InitializeContentList();
          this.RefreshUI();
          this.Open();
          break;
        case 110:
          this.m_Destroy = true;
          this.Close(false);
          break;
        case 120:
          SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
          if (currentValue != null)
          {
            MultiInvitationSendWindow.Content.ItemAccessor dataSource = currentValue.GetDataSource<MultiInvitationSendWindow.Content.ItemAccessor>("_self");
            if (dataSource != null && dataSource.isValid)
            {
              string uid = dataSource.friend.UID;
              if (!this.IsSendList(uid))
              {
                dataSource.isOn = true;
                this.m_SendList.Add(uid);
                break;
              }
              dataSource.isOn = false;
              this.m_SendList.Remove(uid);
              break;
            }
            break;
          }
          break;
        case 130:
          return 190;
      }
      return -1;
    }

    public static class Content
    {
      public static MultiInvitationSendWindow.Content.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private ContentNode m_Node;
        private FriendData m_Friend;
        private DataSource m_DataSource;
        private Toggle m_Toggle;
        private GameObject m_Hatch;

        public ContentNode node
        {
          get
          {
            return this.m_Node;
          }
        }

        public FriendData friend
        {
          get
          {
            return this.m_Friend;
          }
        }

        public Toggle tgl
        {
          get
          {
            return this.m_Toggle;
          }
        }

        public bool isOn
        {
          set
          {
            if (!((UnityEngine.Object) this.m_Toggle != (UnityEngine.Object) null))
              return;
            this.m_Toggle.isOn = value;
          }
          get
          {
            if ((UnityEngine.Object) this.m_Toggle != (UnityEngine.Object) null)
              return this.m_Toggle.isOn;
            return false;
          }
        }

        public bool isValid
        {
          get
          {
            return this.m_Friend != null;
          }
        }

        public void Setup(FriendData friend)
        {
          this.m_Friend = friend;
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          this.m_DataSource = DataSource.Create(node.gameObject);
          this.m_DataSource.Add(typeof (FriendData), (object) this.m_Friend);
          this.m_DataSource.Add(typeof (UnitData), (object) this.m_Friend.Unit);
          this.m_DataSource.Add(typeof (MultiInvitationSendWindow.Content.ItemAccessor), (object) this);
          SerializeValueBehaviour component = this.m_Node.GetComponent<SerializeValueBehaviour>();
          if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
            return;
          bool flag = MultiInvitationSendWindow.instance != null && MultiInvitationSendWindow.instance.IsSendList(this.m_Friend.UID);
          this.m_Toggle = component.list.GetUIToggle("toggle");
          if ((UnityEngine.Object) this.m_Toggle != (UnityEngine.Object) null)
            this.m_Toggle.isOn = flag;
          this.m_Hatch = component.list.GetGameObject("hatch");
          if ((UnityEngine.Object) this.m_Hatch != (UnityEngine.Object) null)
            this.m_Hatch.SetActive(false);
          component.list.SetField("comment", this.m_Friend.MultiComment);
        }

        public void Clear()
        {
          if ((UnityEngine.Object) this.m_DataSource != (UnityEngine.Object) null)
          {
            this.m_DataSource.Clear();
            this.m_DataSource = (DataSource) null;
          }
          this.m_Node = (ContentNode) null;
          this.m_Toggle = (Toggle) null;
          this.m_Hatch = (GameObject) null;
        }

        public void ForceUpdate()
        {
        }

        public void SetHatch(bool value)
        {
          if (!((UnityEngine.Object) this.m_Hatch != (UnityEngine.Object) null))
            return;
          this.m_Hatch.SetActive(!value);
        }
      }

      public class ItemSource : ContentSource
      {
        private List<MultiInvitationSendWindow.Content.ItemSource.ItemParam> m_Params = new List<MultiInvitationSendWindow.Content.ItemSource.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release()
        {
          base.Release();
        }

        public void Add(MultiInvitationSendWindow.Content.ItemSource.ItemParam param)
        {
          if (!param.IsValid())
            return;
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<MultiInvitationSendWindow.Content.ItemSource.ItemParam, bool> predicate = (Func<MultiInvitationSendWindow.Content.ItemSource.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<MultiInvitationSendWindow.Content.ItemSource.ItemParam>(predicate).ToArray<MultiInvitationSendWindow.Content.ItemSource.ItemParam>());
          else
            this.SetTable((ContentSource.Param[]) this.m_Params.ToArray());
          this.contentController.Resize(0);
          bool flag = false;
          Vector2 anchoredPosition = this.contentController.anchoredPosition;
          Vector2 lastPageAnchorePos = this.contentController.GetLastPageAnchorePos();
          if ((double) anchoredPosition.x < (double) lastPageAnchorePos.x)
          {
            flag = true;
            anchoredPosition.x = lastPageAnchorePos.x;
          }
          if ((double) anchoredPosition.y < (double) lastPageAnchorePos.y)
          {
            flag = true;
            anchoredPosition.y = lastPageAnchorePos.y;
          }
          if (flag)
            this.contentController.anchoredPosition = anchoredPosition;
          this.contentController.scroller.StopMovement();
        }

        public class ItemParam : ContentSource.Param
        {
          private MultiInvitationSendWindow.Content.ItemAccessor m_Accessor = new MultiInvitationSendWindow.Content.ItemAccessor();

          public ItemParam(FriendData friend)
          {
            this.m_Accessor.Setup(friend);
          }

          public override bool IsValid()
          {
            return this.m_Accessor.isValid;
          }

          public MultiInvitationSendWindow.Content.ItemAccessor accessor
          {
            get
            {
              return this.m_Accessor;
            }
          }

          public FriendData friend
          {
            get
            {
              return this.m_Accessor.friend;
            }
          }

          public override void OnEnable(ContentNode node)
          {
            this.m_Accessor.Bind(node);
            this.m_Accessor.ForceUpdate();
          }

          public override void OnDisable(ContentNode node)
          {
            this.m_Accessor.Clear();
          }

          public override void OnClick(ContentNode node)
          {
          }
        }
      }
    }

    [Serializable]
    public class SerializeParam : FlowWindowBase.SerializeParamBase
    {
      public GameObject list;

      public override System.Type type
      {
        get
        {
          return typeof (MultiInvitationSendWindow);
        }
      }
    }
  }
}
