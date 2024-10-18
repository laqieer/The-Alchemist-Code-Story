// Decompiled with JetBrains decompiler
// Type: SRPG.Tooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class Tooltip : MonoBehaviour
  {
    public float DestroyDelay = 1f;
    public static Vector2 TooltipPosition;
    public RectTransform Body;
    public RectTransform SizeBody;
    public Text TooltipText;
    public Text TextName;
    public Text TextDesc;
    public string CloseTrigger;
    private Animator mAnimator;
    private bool mDestroying;
    public bool CloseOnPress;

    public static void SetTooltipPosition(RectTransform rect, Vector2 localPos)
    {
      Vector2 vector2 = (Vector2) rect.TransformPoint((Vector3) localPos);
      CanvasScaler componentInParent = rect.GetComponentInParent<CanvasScaler>();
      if ((UnityEngine.Object) componentInParent != (UnityEngine.Object) null)
      {
        Vector3 localScale = componentInParent.transform.localScale;
        vector2.x /= localScale.x;
        vector2.y /= localScale.y;
      }
      Tooltip.TooltipPosition = vector2;
    }

    public void ResetPosition()
    {
      if (!((UnityEngine.Object) this.Body != (UnityEngine.Object) null))
        return;
      this.Body.anchorMin = Vector2.zero;
      this.Body.anchorMax = Vector2.zero;
      this.Body.anchoredPosition = Tooltip.TooltipPosition;
    }

    public Vector2 BodySize
    {
      get
      {
        if ((bool) ((UnityEngine.Object) this.SizeBody))
          return this.SizeBody.sizeDelta;
        return Vector2.zero;
      }
    }

    public bool EnableDisp
    {
      set
      {
        if (!(bool) ((UnityEngine.Object) this.SizeBody))
          return;
        CanvasGroup component = this.SizeBody.GetComponent<CanvasGroup>();
        if (!(bool) ((UnityEngine.Object) component))
          return;
        component.alpha = !value ? 0.0f : 1f;
      }
    }

    private void Start()
    {
      this.mAnimator = this.GetComponent<Animator>();
      this.ResetPosition();
    }

    public void Close()
    {
      this.mDestroying = true;
      if ((UnityEngine.Object) this.mAnimator != (UnityEngine.Object) null && !string.IsNullOrEmpty(this.CloseTrigger))
        this.mAnimator.SetTrigger(this.CloseTrigger);
      if ((double) Time.timeScale != 0.0)
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject, this.DestroyDelay);
      else
        UnityEngine.Object.Destroy((UnityEngine.Object) this.gameObject);
    }

    private void Update()
    {
      if (this.mDestroying)
        return;
      if (this.CloseOnPress)
      {
        if (!Input.GetMouseButton(0))
          return;
      }
      else if (Input.GetMouseButton(0))
        return;
      this.Close();
    }
  }
}
