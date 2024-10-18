// Decompiled with JetBrains decompiler
// Type: SRPG.FriendPresentGaveWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class FriendPresentGaveWindow : FlowWindowBase
  {
    private FriendPresentGaveWindow.Content.ItemSource m_ContentSource;
    private ContentController m_ContentController;
    private List<string> m_FriendUidList = new List<string>();
    private static FriendPresentGaveWindow m_Instance;

    public override string name => nameof (FriendPresentGaveWindow);

    public static FriendPresentGaveWindow instance => FriendPresentGaveWindow.m_Instance;

    public override void Initialize(FlowWindowBase.SerializeParamBase param)
    {
      FriendPresentGaveWindow.m_Instance = this;
      base.Initialize(param);
      if (!(param is FriendPresentGaveWindow.SerializeParam serializeParam))
        throw new Exception(this.ToString() + " > Failed serializeParam null.");
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) serializeParam.list, (UnityEngine.Object) null))
      {
        this.m_ContentController = serializeParam.list.GetComponentInChildren<ContentController>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
          this.m_ContentController.SetWork((object) this);
      }
      this.Close(true);
    }

    public override void Release()
    {
      this.ReleaseContentList();
      base.Release();
      FriendPresentGaveWindow.m_Instance = (FriendPresentGaveWindow) null;
    }

    public override int Update()
    {
      base.Update();
      if (!this.isClosed)
        return -1;
      this.SetActiveChild(false);
      return 390;
    }

    public void InitializeContentList()
    {
      this.ReleaseContentList();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        return;
      this.m_ContentSource = new FriendPresentGaveWindow.Content.ItemSource();
      for (int index = 0; index < this.m_FriendUidList.Count; ++index)
      {
        FriendPresentGaveWindow.Content.ItemSource.ItemParam itemParam = new FriendPresentGaveWindow.Content.ItemSource.ItemParam(this.m_FriendUidList[index]);
        if (itemParam.IsValid())
          this.m_ContentSource.Add(itemParam);
      }
      this.m_ContentController.Initialize((ContentSource) this.m_ContentSource, Vector2.zero);
    }

    public void ReleaseContentList()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_ContentController, (UnityEngine.Object) null))
        this.m_ContentController.Release();
      this.m_ContentSource = (FriendPresentGaveWindow.Content.ItemSource) null;
    }

    public void ClearFuids() => this.m_FriendUidList.Clear();

    public void AddUid(string uid) => this.m_FriendUidList.Add(uid);

    public override int OnActivate(int pinId)
    {
      switch (pinId)
      {
        case 300:
          this.InitializeContentList();
          this.Open();
          break;
        case 310:
          this.Close();
          break;
      }
      return -1;
    }

    public static class Content
    {
      public static FriendPresentGaveWindow.Content.ItemAccessor clickItem;

      public class ItemAccessor
      {
        private ContentNode m_Node;
        private FriendData m_FriendData;

        public ContentNode node => this.m_Node;

        public FriendData friendData => this.m_FriendData;

        public bool isValid => this.m_FriendData != null;

        public void Setup(string uid)
        {
          this.m_FriendData = MonoSingleton<GameManager>.Instance.Player.Friends.Find((Predicate<FriendData>) (prop => prop.UID == uid));
        }

        public void Bind(ContentNode node)
        {
          this.m_Node = node;
          SerializeValueBehaviour component = ((Component) this.m_Node).GetComponent<SerializeValueBehaviour>();
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            return;
          component.list.SetField("name", this.m_FriendData.PlayerName);
        }

        public void Clear() => this.m_Node = (ContentNode) null;

        public void ForceUpdate()
        {
        }
      }

      public class ItemSource : ContentSource
      {
        private List<FriendPresentGaveWindow.Content.ItemSource.ItemParam> m_Params = new List<FriendPresentGaveWindow.Content.ItemSource.ItemParam>();

        public override void Initialize(ContentController controller)
        {
          base.Initialize(controller);
          this.Setup();
        }

        public override void Release() => base.Release();

        public void Add(
          FriendPresentGaveWindow.Content.ItemSource.ItemParam param)
        {
          if (!param.IsValid())
            return;
          this.m_Params.Add(param);
        }

        public void Setup()
        {
          Func<FriendPresentGaveWindow.Content.ItemSource.ItemParam, bool> predicate = (Func<FriendPresentGaveWindow.Content.ItemSource.ItemParam, bool>) (prop => true);
          this.Clear();
          if (predicate != null)
            this.SetTable((ContentSource.Param[]) this.m_Params.Where<FriendPresentGaveWindow.Content.ItemSource.ItemParam>(predicate).ToArray<FriendPresentGaveWindow.Content.ItemSource.ItemParam>());
          else
            this.SetTable((ContentSource.Param[]) this.m_Params.ToArray());
          this.contentController.Resize();
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
          if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.contentController.scroller, (UnityEngine.Object) null))
            return;
          this.contentController.scroller.StopMovement();
        }

        public class ItemParam : ContentSource.Param
        {
          private FriendPresentGaveWindow.Content.ItemAccessor m_Accessor = new FriendPresentGaveWindow.Content.ItemAccessor();

          public ItemParam(string uid) => this.m_Accessor.Setup(uid);

          public override bool IsValid() => this.m_Accessor.isValid;

          public FriendPresentGaveWindow.Content.ItemAccessor accerror => this.m_Accessor;

          public FriendData friendData => this.m_Accessor.friendData;

          public override void OnEnable(ContentNode node)
          {
            this.m_Accessor.Bind(node);
            this.m_Accessor.ForceUpdate();
          }

          public override void OnDisable(ContentNode node) => this.m_Accessor.Clear();

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

      public override System.Type type => typeof (FriendPresentGaveWindow);
    }
  }
}
