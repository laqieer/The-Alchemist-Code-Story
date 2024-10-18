// Decompiled with JetBrains decompiler
// Type: SRPG.GvGNodeListWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "Attack List", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "Occupy List", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(100, "Select Defense", FlowNode.PinTypes.Output, 100)]
  public class GvGNodeListWindow : MonoBehaviour, IFlowInterface
  {
    public const int PIN_INPUT_ATTACK = 10;
    public const int PIN_INPUT_OCCUPY = 11;
    public const int PIN_INPUT_SELECTDEFENSE = 100;
    [SerializeField]
    private GvGNodeListWindowContent nodeTemplate;
    [SerializeField]
    private GameObject mTitleOffense;
    [SerializeField]
    private GameObject mTitleDefense;
    [SerializeField]
    private ImageArray mWindowColor;
    private GvGNodeListWindow.GVG_OFFENSENODE_TYPE mType;

    private void Refresh()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) GvGManager.Instance, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.nodeTemplate, (UnityEngine.Object) null))
        return;
      GameUtility.SetGameObjectActive((Component) this.nodeTemplate, false);
      GameUtility.SetGameObjectActive(this.mTitleOffense, this.mType == GvGNodeListWindow.GVG_OFFENSENODE_TYPE.Attack);
      GameUtility.SetGameObjectActive(this.mTitleDefense, this.mType != GvGNodeListWindow.GVG_OFFENSENODE_TYPE.Attack);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mWindowColor, (UnityEngine.Object) null))
        this.mWindowColor.ImageIndex = (int) this.mType;
      List<GvGNodeData> gvGnodeDataList = (List<GvGNodeData>) null;
      if (this.mType == GvGNodeListWindow.GVG_OFFENSENODE_TYPE.Attack)
        gvGnodeDataList = GvGManager.Instance.NodeDataList.FindAll((Predicate<GvGNodeData>) (nd => nd.State == GvGNodeState.DeclareSelf));
      else if (this.mType == GvGNodeListWindow.GVG_OFFENSENODE_TYPE.OccupySelf)
        gvGnodeDataList = GvGManager.Instance.NodeDataList.FindAll((Predicate<GvGNodeData>) (nd => nd.State == GvGNodeState.OccupySelf || nd.State == GvGNodeState.DeclaredEnemy));
      if (gvGnodeDataList == null || gvGnodeDataList.Count == 0)
        return;
      for (int index = 0; index < gvGnodeDataList.Count; ++index)
      {
        GvGNodeListWindowContent listWindowContent = UnityEngine.Object.Instantiate<GvGNodeListWindowContent>(this.nodeTemplate, ((Component) this.nodeTemplate).transform.parent);
        if (!UnityEngine.Object.op_Equality((UnityEngine.Object) listWindowContent, (UnityEngine.Object) null) && gvGnodeDataList[index] != null)
        {
          DataSource.Bind<GvGNodeData>(((Component) listWindowContent).gameObject, gvGnodeDataList[index]);
          ((Component) listWindowContent).gameObject.SetActive(true);
          listWindowContent.Initialize();
        }
      }
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.mType = GvGNodeListWindow.GVG_OFFENSENODE_TYPE.Attack;
          break;
        case 11:
          this.mType = GvGNodeListWindow.GVG_OFFENSENODE_TYPE.OccupySelf;
          break;
      }
      if (pinID != 10 && pinID != 11)
        return;
      this.Refresh();
    }

    public void onNodeDetail(GameObject obj)
    {
      GvGNodeData dataOfClass = DataSource.FindDataOfClass<GvGNodeData>(obj, (GvGNodeData) null);
      if (dataOfClass == null || dataOfClass.NodeId < 0)
        return;
      GvGManager.Instance.OpenNodeInfo(dataOfClass.NodeId);
    }

    public void onNodeDefenseSetting(GameObject obj)
    {
      GvGNodeData dataOfClass = DataSource.FindDataOfClass<GvGNodeData>(obj, (GvGNodeData) null);
      if (dataOfClass == null || dataOfClass.NodeId < 0)
        return;
      GvGManager.Instance.SelectNodeId = dataOfClass.NodeId;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }

    private enum GVG_OFFENSENODE_TYPE
    {
      Attack,
      OccupySelf,
    }
  }
}
