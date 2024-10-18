// Decompiled with JetBrains decompiler
// Type: SRPG.CondDescTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable disable
namespace SRPG
{
  public class CondDescTooltip : MonoBehaviour
  {
    public CondDescTooltip.eDispType DispType;
    public Tooltip PrefabTooltip;
    public ImageArray ImageCond;
    public float PosLeftOffset = 50f;
    public float PosRightOffset = 50f;
    public float PosUpOffset = 50f;
    private static Tooltip mTooltip;
    private CanvasGroup[] mParentCgs;

    private void Start()
    {
      UIEventListener uiEventListener = ((Component) this).RequireComponent<UIEventListener>();
      if (uiEventListener.onMove == null)
        uiEventListener.onPointerEnter = new UIEventListener.PointerEvent(this.ShowTooltip);
      else
        uiEventListener.onPointerEnter += new UIEventListener.PointerEvent(this.ShowTooltip);
      if (Object.op_Implicit((Object) this.ImageCond))
        return;
      this.ImageCond = ((Component) this).GetComponent<ImageArray>();
    }

    private CanvasGroup[] ParentCgs
    {
      get
      {
        if (this.mParentCgs == null)
          this.mParentCgs = ((Component) this).GetComponentsInParent<CanvasGroup>();
        return this.mParentCgs;
      }
    }

    private void ShowTooltip(PointerEventData event_data)
    {
      if (!Object.op_Implicit((Object) this.PrefabTooltip) || Object.op_Implicit((Object) CondDescTooltip.mTooltip))
        return;
      RaycastResult pointerCurrentRaycast = event_data.pointerCurrentRaycast;
      if (Object.op_Equality((Object) ((RaycastResult) ref pointerCurrentRaycast).gameObject, (Object) null))
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
      if (Object.op_Equality((Object) CondDescTooltip.mTooltip, (Object) null))
        CondDescTooltip.mTooltip = Object.Instantiate<Tooltip>(this.PrefabTooltip);
      else
        CondDescTooltip.mTooltip.ResetPosition();
      if (!Object.op_Inequality((Object) CondDescTooltip.mTooltip.TooltipText, (Object) null) || !Object.op_Implicit((Object) this.ImageCond))
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
      return (IEnumerator) new CondDescTooltip.\u003CResetPosiotion\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    public enum eDispType
    {
      LEFT,
      RIGHT,
      UP,
    }
  }
}
