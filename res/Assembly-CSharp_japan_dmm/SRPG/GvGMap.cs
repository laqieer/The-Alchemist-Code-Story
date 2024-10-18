// Decompiled with JetBrains decompiler
// Type: SRPG.GvGMap
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Refresh", FlowNode.PinTypes.Input, 1)]
  public class GvGMap : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_REFRESH = 1;
    [SerializeField]
    private List<GameObject> mMapNodes;
    [SerializeField]
    private GvGNode mMapIconTemplate;
    [SerializeField]
    private SRPG_ScrollRect mScrollRect;
    [SerializeField]
    private RectTransform mBgRect;
    [SerializeField]
    private AnimationCurve mScrollCurve;
    [SerializeField]
    private float mScrollTime = 0.5f;
    private static GvGMap mInstance;

    public static GvGMap Instance => GvGMap.mInstance;

    private void Awake()
    {
      GvGMap.mInstance = this;
      this.Initialize();
    }

    private void OnDestroy() => GvGMap.mInstance = (GvGMap) null;

    private void Initialize()
    {
      if (GvGManager.Instance.NodeDataList == null || Object.op_Equality((Object) this.mMapIconTemplate, (Object) null))
        return;
      GameUtility.SetGameObjectActive(((Component) this.mMapIconTemplate).gameObject, false);
      for (int index = 0; index < this.mMapNodes.Count; ++index)
      {
        if (!Object.op_Equality((Object) this.mMapNodes[index], (Object) null))
        {
          GvGNode gvGnode = Object.Instantiate<GvGNode>(this.mMapIconTemplate, this.mMapNodes[index].transform);
          if (!Object.op_Equality((Object) gvGnode, (Object) null))
          {
            GameUtility.SetGameObjectActive(((Component) gvGnode).gameObject, true);
            DataSource.Bind<GvGNodeData>(((Component) gvGnode).gameObject, GvGManager.Instance.NodeDataList[index]);
            ImageArray componentInChildren = ((Component) gvGnode).GetComponentInChildren<ImageArray>();
            if (Object.op_Inequality((Object) componentInChildren, (Object) null))
            {
              GvGNodeParam node = GvGNodeParam.GetNode(GvGManager.Instance.NodeDataList[index].NodeId);
              if (node != null && node.Rank > 0)
                componentInChildren.ImageIndex = node.Rank - 1;
            }
          }
        }
      }
      this.ForcusMyPort();
      this.Refresh();
    }

    public void Refresh()
    {
      for (int index = 0; index < this.mMapNodes.Count; ++index)
      {
        if (!Object.op_Equality((Object) this.mMapNodes[index], (Object) null))
        {
          GvGNode componentInChildren = this.mMapNodes[index].GetComponentInChildren<GvGNode>();
          if (Object.op_Inequality((Object) componentInChildren, (Object) null))
            componentInChildren.Refresh(GvGManager.Instance.NodeDataList[index]);
        }
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      this.Refresh();
    }

    public void ForcusMyPort()
    {
      int nodeID = -1;
      for (int index = 0; index < GvGManager.Instance.NodeDataList.Count; ++index)
      {
        if (GvGManager.Instance.NodeDataList[index] != null)
        {
          ViewGuildData viewGuild = GvGManager.Instance.FindViewGuild(GvGManager.Instance.NodeDataList[index].GuildId);
          if (viewGuild != null && viewGuild == GvGManager.Instance.MyGuild)
          {
            nodeID = GvGManager.Instance.NodeDataList[index].NodeId;
            break;
          }
        }
      }
      this.NodeAutoForcus(nodeID);
    }

    public void NodeAutoForcus(int nodeID)
    {
      if (nodeID < 0)
        return;
      int index1 = -1;
      for (int index2 = 0; index2 < GvGManager.Instance.NodeDataList.Count; ++index2)
      {
        if (GvGManager.Instance.NodeDataList[index2] != null && GvGManager.Instance.NodeDataList[index2].NodeId == nodeID)
        {
          index1 = index2;
          break;
        }
      }
      if (Object.op_Equality((Object) this.mScrollRect, (Object) null) || this.mScrollCurve == null || this.mMapNodes.Count <= index1 || index1 < 0)
        return;
      this.StartCoroutine(this.ScrollTo(this.mMapNodes[index1].transform, this.mScrollRect, this.mScrollCurve, this.mScrollTime));
    }

    [DebuggerHidden]
    private IEnumerator ScrollTo(
      Transform target_transform,
      SRPG_ScrollRect scroll_rect,
      AnimationCurve curve,
      float scroll_time)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new GvGMap.\u003CScrollTo\u003Ec__Iterator0()
      {
        scroll_rect = scroll_rect,
        curve = curve,
        target_transform = target_transform,
        scroll_time = scroll_time,
        \u0024this = this
      };
    }
  }
}
