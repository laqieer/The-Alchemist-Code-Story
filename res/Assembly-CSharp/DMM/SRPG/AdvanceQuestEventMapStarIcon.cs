// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceQuestEventMapStarIcon
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Select", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(101, "Selected", FlowNode.PinTypes.Output, 101)]
  public class AdvanceQuestEventMapStarIcon : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_SELECT = 1;
    private const int PIN_OUT_SELECTED = 101;
    [SerializeField]
    private AdvanceQuestEventMap mParentMap;
    [SerializeField]
    private ImageArray[] mTargetIconList;
    [SerializeField]
    private Text mNeedStar;
    private int mIndex;

    public int Index => this.mIndex;

    public void Init(int count, int index, int totalStarNum, AdvanceStarRewardParam gsr)
    {
      if (this.mTargetIconList == null)
        return;
      this.mIndex = index;
      int index1 = Mathf.Min(!gsr.IsReward ? (gsr.NeedStarNum > totalStarNum ? 0 : 1) : 2, this.mTargetIconList.Length - 1);
      for (int index2 = 0; index2 < this.mTargetIconList.Length; ++index2)
        ((Component) ((Component) this.mTargetIconList[index2]).transform.parent).gameObject.SetActive(index2 == index1);
      int num;
      switch (count)
      {
        case 1:
          num = 2;
          break;
        case 2:
          num = index != 0 ? 2 : 1;
          break;
        default:
          num = index >= 3 ? 2 : index;
          break;
      }
      this.mTargetIconList[index1].ImageIndex = num;
      if (Object.op_Equality((Object) this.mNeedStar, (Object) null))
        return;
      this.mNeedStar.text = gsr.NeedStarNum.ToString();
    }

    public void Activated(int pinID)
    {
      if (pinID != 1)
        return;
      if (Object.op_Inequality((Object) this.mParentMap, (Object) null))
        this.mParentMap.OnClickStarRewardIcon(((Component) this).gameObject);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }
  }
}
