// Decompiled with JetBrains decompiler
// Type: SRPG.Tooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class Tooltip : MonoBehaviour
  {
    public static Vector2 TooltipPosition;
    public RectTransform Body;
    public RectTransform SizeBody;
    public Text TooltipText;
    public Text TextName;
    public Text TextDesc;
    public string CloseTrigger;
    public float DestroyDelay = 1f;
    private Animator mAnimator;
    private bool mDestroying;
    public bool CloseOnPress;

    public static void SetTooltipPosition(RectTransform rect, Vector2 localPos)
    {
      Vector2 vector2 = Vector2.op_Implicit(((Transform) rect).TransformPoint(Vector2.op_Implicit(localPos)));
      CanvasScaler componentInParent = ((Component) rect).GetComponentInParent<CanvasScaler>();
      if (Object.op_Inequality((Object) componentInParent, (Object) null))
      {
        Vector3 localScale = ((Component) componentInParent).transform.localScale;
        vector2.x /= localScale.x;
        vector2.y /= localScale.y;
      }
      Tooltip.TooltipPosition = vector2;
    }

    public void ResetPosition()
    {
      if (!Object.op_Inequality((Object) this.Body, (Object) null))
        return;
      this.Body.anchorMin = Vector2.zero;
      this.Body.anchorMax = Vector2.zero;
      this.Body.anchoredPosition = Tooltip.TooltipPosition;
    }

    public Vector2 BodySize
    {
      get => Object.op_Implicit((Object) this.SizeBody) ? this.SizeBody.sizeDelta : Vector2.zero;
    }

    public bool EnableDisp
    {
      set
      {
        if (!Object.op_Implicit((Object) this.SizeBody))
          return;
        CanvasGroup component = ((Component) this.SizeBody).GetComponent<CanvasGroup>();
        if (!Object.op_Implicit((Object) component))
          return;
        component.alpha = !value ? 0.0f : 1f;
      }
    }

    private void Start()
    {
      this.mAnimator = ((Component) this).GetComponent<Animator>();
      this.ResetPosition();
    }

    public void Close()
    {
      this.mDestroying = true;
      if (Object.op_Inequality((Object) this.mAnimator, (Object) null) && !string.IsNullOrEmpty(this.CloseTrigger))
        this.mAnimator.SetTrigger(this.CloseTrigger);
      if ((double) Time.timeScale != 0.0)
        Object.Destroy((Object) ((Component) this).gameObject, this.DestroyDelay);
      else
        Object.Destroy((Object) ((Component) this).gameObject);
    }

    private void Update()
    {
      if (this.mDestroying)
        return;
      if (this.CloseOnPress)
      {
        if (Input.touchCount >= 2)
        {
          this.Close();
          return;
        }
        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
          return;
      }
      else if (Input.GetMouseButton(0))
        return;
      this.Close();
    }

    public void SetTooltipText(string value)
    {
      if (Object.op_Equality((Object) this.TooltipText, (Object) null))
        return;
      this.TooltipText.text = value;
    }
  }
}
