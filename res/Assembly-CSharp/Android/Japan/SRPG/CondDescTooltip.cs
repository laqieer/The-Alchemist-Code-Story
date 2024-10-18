// Decompiled with JetBrains decompiler
// Type: SRPG.CondDescTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class CondDescTooltip : MonoBehaviour
  {
    public float PosLeftOffset = 50f;
    public float PosRightOffset = 50f;
    public float PosUpOffset = 50f;
    public CondDescTooltip.eDispType DispType;
    public Tooltip PrefabTooltip;
    public ImageArray ImageCond;
    private static Tooltip mTooltip;
    private CanvasGroup[] mParentCgs;

    private void Start()
    {
      UIEventListener uiEventListener = this.RequireComponent<UIEventListener>();
      if (uiEventListener.onMove == null)
        uiEventListener.onPointerEnter = new UIEventListener.PointerEvent(this.ShowTooltip);
      else
        uiEventListener.onPointerEnter += new UIEventListener.PointerEvent(this.ShowTooltip);
      if ((bool) ((UnityEngine.Object) this.ImageCond))
        return;
      this.ImageCond = this.GetComponent<ImageArray>();
    }

    private CanvasGroup[] ParentCgs
    {
      get
      {
        if (this.mParentCgs == null)
          this.mParentCgs = this.GetComponentsInParent<CanvasGroup>();
        return this.mParentCgs;
      }
    }

    private void ShowTooltip(PointerEventData event_data)
    {
      if (!(bool) ((UnityEngine.Object) this.PrefabTooltip) || (bool) ((UnityEngine.Object) CondDescTooltip.mTooltip) || (UnityEngine.Object) event_data.pointerCurrentRaycast.gameObject == (UnityEngine.Object) null)
        return;
      CanvasGroup[] parentCgs = this.ParentCgs;
      if (parentCgs != null)
      {
        foreach (CanvasGroup canvasGroup in parentCgs)
        {
          if ((double) canvasGroup.alpha <= 0.0)
            return;
        }
      }
      if ((UnityEngine.Object) CondDescTooltip.mTooltip == (UnityEngine.Object) null)
        CondDescTooltip.mTooltip = UnityEngine.Object.Instantiate<Tooltip>(this.PrefabTooltip);
      else
        CondDescTooltip.mTooltip.ResetPosition();
      if (!((UnityEngine.Object) CondDescTooltip.mTooltip.TooltipText != (UnityEngine.Object) null) || !(bool) ((UnityEngine.Object) this.ImageCond))
        return;
      int imageIndex = this.ImageCond.ImageIndex;
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      if (imageIndex >= 0 && imageIndex < Unit.StrNameUnitConds.Length)
        empty1 = LocalizedText.Get(Unit.StrNameUnitConds[imageIndex]);
      if (imageIndex >= 0 && imageIndex < Unit.StrDescUnitConds.Length)
        empty2 = LocalizedText.Get(Unit.StrDescUnitConds[imageIndex]);
      CondDescTooltip.mTooltip.TooltipText.text = string.Format(LocalizedText.Get("quest.BUD_COND_DESC"), (object) empty1, (object) empty2);
      CondDescTooltip.mTooltip.EnableDisp = false;
      this.StartCoroutine(this.ResetPosiotion());
    }

    [DebuggerHidden]
    private IEnumerator ResetPosiotion()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new CondDescTooltip.\u003CResetPosiotion\u003Ec__Iterator0() { \u0024this = this };
    }

    public enum eDispType
    {
      LEFT,
      RIGHT,
      UP,
    }
  }
}
