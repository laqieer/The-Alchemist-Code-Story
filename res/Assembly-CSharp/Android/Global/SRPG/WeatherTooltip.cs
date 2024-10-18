// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SRPG
{
  public class WeatherTooltip : MonoBehaviour
  {
    public float PosLeftOffset = 50f;
    public float PosRightOffset = 50f;
    public float PosUpOffset = 50f;
    public WeatherTooltip.eDispType DispType;
    public Tooltip PrefabTooltip;
    private Tooltip mTooltip;
    private CanvasGroup[] mParentCgs;

    private void Start()
    {
      UIEventListener uiEventListener = this.RequireComponent<UIEventListener>();
      if (uiEventListener.onMove == null)
        uiEventListener.onPointerEnter = new UIEventListener.PointerEvent(this.ShowTooltip);
      else
        uiEventListener.onPointerEnter += new UIEventListener.PointerEvent(this.ShowTooltip);
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
      if (!(bool) ((UnityEngine.Object) this.PrefabTooltip) || (bool) ((UnityEngine.Object) this.mTooltip) || (UnityEngine.Object) event_data.pointerCurrentRaycast.gameObject == (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) this.mTooltip == (UnityEngine.Object) null)
        this.mTooltip = UnityEngine.Object.Instantiate<Tooltip>(this.PrefabTooltip);
      else
        this.mTooltip.ResetPosition();
      WeatherParam dataOfClass = DataSource.FindDataOfClass<WeatherParam>(this.gameObject, (WeatherParam) null);
      if (!((UnityEngine.Object) this.mTooltip.TooltipText != (UnityEngine.Object) null) || dataOfClass == null)
        return;
      this.mTooltip.TooltipText.text = string.Format(LocalizedText.Get("quest.WEATHER_DESC"), (object) dataOfClass.Name, (object) dataOfClass.Expr);
      this.mTooltip.EnableDisp = false;
      this.StartCoroutine(this.ResetPosiotion());
    }

    [DebuggerHidden]
    private IEnumerator ResetPosiotion()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WeatherTooltip.\u003CResetPosiotion\u003Ec__Iterator59()
      {
        \u003C\u003Ef__this = this
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
