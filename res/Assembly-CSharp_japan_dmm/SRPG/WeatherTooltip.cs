// Decompiled with JetBrains decompiler
// Type: SRPG.WeatherTooltip
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
  public class WeatherTooltip : MonoBehaviour
  {
    public WeatherTooltip.eDispType DispType;
    public Tooltip PrefabTooltip;
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
      if (!Object.op_Implicit((Object) this.PrefabTooltip) || Object.op_Implicit((Object) WeatherTooltip.mTooltip))
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
      if (Object.op_Equality((Object) WeatherTooltip.mTooltip, (Object) null))
        WeatherTooltip.mTooltip = Object.Instantiate<Tooltip>(this.PrefabTooltip);
      else
        WeatherTooltip.mTooltip.ResetPosition();
      WeatherParam dataOfClass = DataSource.FindDataOfClass<WeatherParam>(((Component) this).gameObject, (WeatherParam) null);
      if (!Object.op_Inequality((Object) WeatherTooltip.mTooltip.TooltipText, (Object) null) || dataOfClass == null)
        return;
      WeatherTooltip.mTooltip.TooltipText.text = string.Format(LocalizedText.Get("quest.WEATHER_DESC"), (object) dataOfClass.Name, (object) dataOfClass.Expr);
      WeatherTooltip.mTooltip.EnableDisp = false;
      this.StartCoroutine(this.ResetPosiotion());
    }

    [DebuggerHidden]
    private IEnumerator ResetPosiotion()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new WeatherTooltip.\u003CResetPosiotion\u003Ec__Iterator0()
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
