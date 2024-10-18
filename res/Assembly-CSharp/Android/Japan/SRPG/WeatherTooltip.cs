// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherTooltip
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

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
    private static Tooltip mTooltip;
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
      if (!(bool) ((UnityEngine.Object) this.PrefabTooltip) || (bool) ((UnityEngine.Object) WeatherTooltip.mTooltip) || (UnityEngine.Object) event_data.pointerCurrentRaycast.gameObject == (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) WeatherTooltip.mTooltip == (UnityEngine.Object) null)
        WeatherTooltip.mTooltip = UnityEngine.Object.Instantiate<Tooltip>(this.PrefabTooltip);
      else
        WeatherTooltip.mTooltip.ResetPosition();
      WeatherParam dataOfClass = DataSource.FindDataOfClass<WeatherParam>(this.gameObject, (WeatherParam) null);
      if (!((UnityEngine.Object) WeatherTooltip.mTooltip.TooltipText != (UnityEngine.Object) null) || dataOfClass == null)
        return;
      WeatherTooltip.mTooltip.TooltipText.text = string.Format(LocalizedText.Get("quest.WEATHER_DESC"), (object) dataOfClass.Name, (object) dataOfClass.Expr);
      WeatherTooltip.mTooltip.EnableDisp = false;
      this.StartCoroutine(this.ResetPosiotion());
    }

    [DebuggerHidden]
    private IEnumerator ResetPosiotion()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WeatherTooltip.\u003CResetPosiotion\u003Ec__Iterator0() { \u0024this = this };
    }

    public enum eDispType
    {
      LEFT,
      RIGHT,
      UP,
    }
  }
}
