// Decompiled with JetBrains decompiler
// Type: SRPG.BattleCameraControl
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleCameraControl : MonoBehaviour
  {
    public float RotateAmount = 0.25f;
    public float RotateTime = 0.2f;
    public Button RotateLeft;
    public Button RotateRight;
    public Slider RotationSlider;
    public Scrollbar RotationScroll;
    private Animator m_Animator;
    private Canvas m_Canvas;
    private GraphicRaycaster m_GraphicRaycatser;
    private bool m_Disp;

    private void Start()
    {
      if ((UnityEngine.Object) this.RotateLeft != (UnityEngine.Object) null)
        this.RotateLeft.onClick.AddListener(new UnityAction(this.OnRotateLeft));
      if ((UnityEngine.Object) this.RotateRight != (UnityEngine.Object) null)
        this.RotateRight.onClick.AddListener(new UnityAction(this.OnRotateRight));
      if ((UnityEngine.Object) this.RotationSlider != (UnityEngine.Object) null)
        this.RotationSlider.onValueChanged.AddListener(new UnityAction<float>(this.OnRotationValueChange));
      this.m_Animator = this.GetComponent<Animator>();
      this.m_Canvas = this.GetComponent<Canvas>();
      this.m_GraphicRaycatser = this.GetComponent<GraphicRaycaster>();
      this.SetDisp(false);
    }

    private void Update()
    {
      SceneBattle instance = SceneBattle.Instance;
      if ((UnityEngine.Object) instance == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.RotateLeft != (UnityEngine.Object) null)
        this.RotateLeft.interactable = instance.isCameraLeftMove;
      if ((UnityEngine.Object) this.RotateRight != (UnityEngine.Object) null)
        this.RotateRight.interactable = instance.isCameraRightMove;
      if (!((UnityEngine.Object) this.m_Animator != (UnityEngine.Object) null))
        return;
      bool flag = this.m_Animator.GetBool("open");
      AnimatorStateInfo animatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
      if (flag)
      {
        if ((UnityEngine.Object) this.m_Canvas != (UnityEngine.Object) null)
          this.m_Canvas.enabled = true;
        if ((double) animatorStateInfo.normalizedTime < 1.0 || !((UnityEngine.Object) this.m_GraphicRaycatser != (UnityEngine.Object) null))
          return;
        this.m_GraphicRaycatser.enabled = true;
      }
      else
      {
        if ((double) animatorStateInfo.normalizedTime >= 1.0 && (UnityEngine.Object) this.m_Canvas != (UnityEngine.Object) null)
          this.m_Canvas.enabled = false;
        if (!((UnityEngine.Object) this.m_GraphicRaycatser != (UnityEngine.Object) null))
          return;
        this.m_GraphicRaycatser.enabled = false;
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
      Animator component = this.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.SetBool("open", value);
    }

    public void OnEventCall(string key, string value)
    {
      if (key == null)
        return;
      if (!(key == "DISP"))
      {
        if (!(key == "FULLROTATION"))
          return;
        if (value == "on")
          SceneBattle.Instance.SetFullRotationCamera(true);
        else
          SceneBattle.Instance.SetFullRotationCamera(false);
      }
      else if (value == "on")
        this.SetDisp(true);
      else
        this.SetDisp(false);
    }
  }
}
