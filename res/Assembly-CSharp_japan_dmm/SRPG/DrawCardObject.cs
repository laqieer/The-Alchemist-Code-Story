// Decompiled with JetBrains decompiler
// Type: SRPG.DrawCardObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(11, "OnClick", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(101, "Open", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(111, "Close", FlowNode.PinTypes.Output, 111)]
  [FlowNode.Pin(121, "Draw", FlowNode.PinTypes.Output, 121)]
  [FlowNode.Pin(131, "Miss", FlowNode.PinTypes.Output, 131)]
  [FlowNode.Pin(141, "Selectable", FlowNode.PinTypes.Output, 141)]
  [FlowNode.Pin(151, "Reset", FlowNode.PinTypes.Output, 151)]
  public class DrawCardObject : MonoBehaviour, IFlowInterface
  {
    private const int INPUT_PIN_INITIALIZE = 1;
    private const int INPUT_PIN_ONCLICK = 11;
    private const int OUTPUT_PIN_OPEN = 101;
    private const int OUTPUT_PIN_CLOSE = 111;
    private const int OUTPUT_PIN_DRAW = 121;
    private const int OUTPUT_PIN_MISS = 131;
    private const int OUTPUT_PIN_SELECTABLE = 141;
    private const int OUTPUT_PIN_RESET = 151;
    [SerializeField]
    private GameObject SelectCursor;
    [SerializeField]
    private DrawCardGetItem Item;
    [SerializeField]
    private Button Button;
    [SerializeField]
    private GameObject mMask;
    [SerializeField]
    private Transform mMoveObject;
    [SerializeField]
    private Transform mRootObject;
    [SerializeField]
    private AnimationCurve mCurve = AnimationCurve.Linear(0.0f, 0.0f, 1f, 1f);
    private bool mSelected;
    private DrawCardContent mContent;
    private DrawCardParam.CardData mCardData;

    public void Initialize(DrawCardContent content)
    {
      this.mContent = content;
      this.Select(false);
      ((Component) this.Item).gameObject.SetActive(false);
      if (!Object.op_Inequality((Object) this.mMask, (Object) null))
        return;
      this.mMask.SetActive(false);
    }

    public void Refresh()
    {
      this.Select(false);
      ((Component) this.Item).gameObject.SetActive(true);
      DataSource.Bind<DrawCardParam.CardData>(((Component) this.Item).gameObject, this.mCardData, true);
      GameParameter.UpdateAll(((Component) this.Item).gameObject);
    }

    public void SetCardData(DrawCardParam.CardData card_data) => this.mCardData = card_data;

    public void CardItemActive(bool enable)
    {
      if (!Object.op_Inequality((Object) this.mMoveObject, (Object) null))
        return;
      ((Component) this.mMoveObject).gameObject.SetActive(enable);
    }

    public void Select(bool select)
    {
      this.mSelected = select;
      if (!Object.op_Inequality((Object) this.SelectCursor, (Object) null))
        return;
      this.SelectCursor.SetActive(this.mSelected);
    }

    public void Mask(bool mask)
    {
      if (!Object.op_Inequality((Object) this.mMask, (Object) null))
        return;
      this.mMask.SetActive(mask);
    }

    public void ButtonActive(bool select)
    {
      if (!Object.op_Inequality((Object) this.Button, (Object) null))
        return;
      ((Behaviour) this.Button).enabled = select;
    }

    public void Activated(int pinID)
    {
      if (pinID == 1 || pinID != 11 || !Object.op_Inequality((Object) this.mContent, (Object) null))
        return;
      this.mContent.SelectCard(this);
    }

    public void SetStartPosition(Vector3 position)
    {
      if (!Object.op_Inequality((Object) this.mMoveObject, (Object) null))
        return;
      this.mMoveObject.position = position;
    }

    public Vector3 GetRootPosition() => this.mRootObject.position;

    public GameObject GetMoveObject() => ((Component) this.mMoveObject).gameObject;

    public void SetPosition(
      Vector3 start_position,
      Vector3 target_position,
      float start_scale,
      float target_scale,
      float start_rotate,
      float target_rotate,
      float time)
    {
      if (!Object.op_Inequality((Object) this.mMoveObject, (Object) null))
        return;
      float num1 = this.mCurve.Evaluate(time);
      Vector3 vector3 = Vector3.Lerp(start_position, target_position, num1);
      Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, Mathf.Lerp(start_rotate, target_rotate, num1));
      float num2 = Mathf.Lerp(start_scale, target_scale, num1);
      this.mMoveObject.position = vector3;
      this.mMoveObject.localRotation = quaternion;
      this.mMoveObject.localScale = new Vector3(num2, num2, this.mMoveObject.localScale.z);
    }

    [DebuggerHidden]
    public IEnumerator Move(
      Vector3 start_position,
      Vector3 target_position,
      float start_scale,
      float target_scale,
      float start_rotate,
      float target_rotate,
      float target_time)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new DrawCardObject.\u003CMove\u003Ec__Iterator0()
      {
        target_time = target_time,
        start_position = start_position,
        target_position = target_position,
        start_scale = start_scale,
        target_scale = target_scale,
        start_rotate = start_rotate,
        target_rotate = target_rotate,
        \u0024this = this
      };
    }

    public void CardReset()
    {
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 151);
    }

    public void Open()
    {
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public void Draw()
    {
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 121);
    }

    public void Selectable()
    {
      ((Component) this.Item).gameObject.SetActive(false);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 141);
    }

    public void Miss()
    {
      this.Refresh();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 131);
    }

    public void Close()
    {
      ((Component) this.Item).gameObject.SetActive(false);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }
  }
}
