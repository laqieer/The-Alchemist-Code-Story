// Decompiled with JetBrains decompiler
// Type: SRPG.EmptyListActiveObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "Refresh", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(10, "Refreshed", FlowNode.PinTypes.Output, 10)]
  public class EmptyListActiveObject : MonoBehaviour, IFlowInterface, IGameParameter
  {
    private const int PIN_IN_REFRESH = 0;
    private const int PIN_OUT_EXIT = 10;
    [HeaderBar("中身が入る親オブジェクト(指定しなければ自分自身)")]
    [SerializeField]
    private Transform TargetListParent;
    [HeaderBar("リストが空のときに表示したいゲームオブジェクト")]
    [SerializeField]
    private GameObject EmptyTextObject;
    [HeaderBar("常に監視するか？")]
    [SerializeField]
    private bool AlwaysCheck = true;
    private bool mNeedCheck;

    private void Awake()
    {
      if (Object.op_Equality((Object) this.TargetListParent, (Object) null))
        this.TargetListParent = ((Component) this).gameObject.transform;
      if (Object.op_Inequality((Object) this.EmptyTextObject, (Object) null))
        this.EmptyTextObject.SetActive(false);
      else
        DebugUtility.LogWarning("EmptyListActiveObject:EmptyTextObjectがnullはおかしい");
      this.mNeedCheck = false;
    }

    public void UpdateValue() => this.mNeedCheck = true;

    public void Activated(int pinID)
    {
      if (pinID != 0)
        return;
      this.mNeedCheck = true;
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 10);
    }

    private void LateUpdate()
    {
      if (!this.AlwaysCheck && !this.mNeedCheck)
        return;
      if (Object.op_Inequality((Object) this.TargetListParent, (Object) null))
      {
        bool active = true;
        if (this.TargetListParent.childCount > 0)
        {
          for (int index = 0; index < this.TargetListParent.childCount; ++index)
          {
            Transform child = this.TargetListParent.GetChild(index);
            if (Object.op_Inequality((Object) child, (Object) null) && ((Component) child).gameObject.activeSelf && Vector3.op_Inequality(((Component) child).transform.localScale, Vector3.zero))
              active = false;
          }
        }
        GameUtility.SetGameObjectActive(this.EmptyTextObject, active);
      }
      this.mNeedCheck = false;
    }
  }
}
