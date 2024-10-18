// Decompiled with JetBrains decompiler
// Type: SRPG.BattleCameraControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BattleCameraControl : MonoBehaviour
  {
    public Button RotateLeft;
    public Button RotateRight;
    public Slider RotationSlider;
    public Scrollbar RotationScroll;
    public float RotateAmount = 0.25f;
    public float RotateTime = 0.2f;
    private Animator m_Animator;
    private Canvas m_Canvas;
    private GraphicRaycaster m_GraphicRaycatser;
    private bool m_Disp;

    private void Start()
    {
      if (Object.op_Inequality((Object) this.RotateLeft, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.RotateLeft.onClick).AddListener(new UnityAction((object) this, __methodptr(OnRotateLeft)));
      }
      if (Object.op_Inequality((Object) this.RotateRight, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.RotateRight.onClick).AddListener(new UnityAction((object) this, __methodptr(OnRotateRight)));
      }
      if (Object.op_Inequality((Object) this.RotationSlider, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<float>) this.RotationSlider.onValueChanged).AddListener(new UnityAction<float>((object) this, __methodptr(OnRotationValueChange)));
      }
      this.m_Animator = ((Component) this).GetComponent<Animator>();
      this.m_Canvas = ((Component) this).GetComponent<Canvas>();
      this.m_GraphicRaycatser = ((Component) this).GetComponent<GraphicRaycaster>();
      this.SetDisp(false);
    }

    private void Update()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Equality((Object) instance, (Object) null))
        return;
      if (Object.op_Inequality((Object) this.RotateLeft, (Object) null))
        ((Selectable) this.RotateLeft).interactable = instance.isCameraLeftMove;
      if (Object.op_Inequality((Object) this.RotateRight, (Object) null))
        ((Selectable) this.RotateRight).interactable = instance.isCameraRightMove;
      if (!Object.op_Inequality((Object) this.m_Animator, (Object) null))
        return;
      bool flag = this.m_Animator.GetBool("open");
      AnimatorStateInfo animatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
      if (flag)
      {
        if (Object.op_Inequality((Object) this.m_Canvas, (Object) null))
          ((Behaviour) this.m_Canvas).enabled = true;
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime < 1.0 || !Object.op_Inequality((Object) this.m_GraphicRaycatser, (Object) null))
          return;
        ((Behaviour) this.m_GraphicRaycatser).enabled = true;
      }
      else
      {
        if ((double) ((AnimatorStateInfo) ref animatorStateInfo).normalizedTime >= 1.0 && Object.op_Inequality((Object) this.m_Canvas, (Object) null))
          ((Behaviour) this.m_Canvas).enabled = false;
        if (!Object.op_Inequality((Object) this.m_GraphicRaycatser, (Object) null))
          return;
        ((Behaviour) this.m_GraphicRaycatser).enabled = false;
      }
    }

    private void OnRotateLeft()
    {
      SceneBattle.Instance.RotateCamera(-this.RotateAmount, this.RotateTime);
    }

    private void OnRotateRight()
    {
      SceneBattle.Instance.RotateCamera(this.RotateAmount, this.RotateTime);
    }

    private void OnRotationValueChange(float value)
    {
    }

    public void SetDisp(bool value)
    {
      if (value && SceneBattle.Instance.isUpView)
        value = false;
      Animator component = ((Component) this).GetComponent<Animator>();
      if (!Object.op_Inequality((Object) component, (Object) null))
        return;
      component.SetBool("open", value);
    }

    public void OnEventCall(string key, string value)
    {
      switch (key)
      {
        case "DISP":
          if (value == "on")
          {
            this.SetDisp(true);
            break;
          }
          this.SetDisp(false);
          break;
        case "FULLROTATION":
          if (value == "on")
          {
            SceneBattle.Instance.SetFullRotationCamera(true);
            break;
          }
          SceneBattle.Instance.SetFullRotationCamera(false);
          break;
      }
    }
  }
}
