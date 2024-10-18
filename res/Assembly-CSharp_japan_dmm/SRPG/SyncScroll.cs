// Decompiled with JetBrains decompiler
// Type: SRPG.SyncScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SyncScroll : MonoBehaviour
  {
    [SerializeField]
    private ScrollRect m_ScrollRect;
    [SerializeField]
    private SyncScroll.ScrollMode m_ScrollMode;
    [SerializeField]
    private RectTransform parent;
    private RectTransform rectTransform;

    public bool isNormal
    {
      get => this.m_ScrollMode == SyncScroll.ScrollMode.Normal;
      set
      {
        this.m_ScrollMode = !value ? SyncScroll.ScrollMode.Reverse : SyncScroll.ScrollMode.Normal;
      }
    }

    public bool isReverse
    {
      get => this.m_ScrollMode == SyncScroll.ScrollMode.Reverse;
      set
      {
        this.m_ScrollMode = !value ? SyncScroll.ScrollMode.Normal : SyncScroll.ScrollMode.Reverse;
      }
    }

    public SyncScroll.ScrollMode scrollMode
    {
      get => this.m_ScrollMode;
      set => this.m_ScrollMode = value;
    }

    private void Awake()
    {
      this.rectTransform = ((Component) this).GetComponent<RectTransform>();
      this.parent = ((Component) this).GetComponentInParent<RectTransform>();
      ((Behaviour) this).enabled = Object.op_Inequality((Object) this.m_ScrollRect, (Object) null) && Object.op_Inequality((Object) this.rectTransform, (Object) null);
    }

    private void LateUpdate()
    {
      if (this.m_ScrollRect.horizontal)
      {
        Vector2 anchoredPosition = this.rectTransform.anchoredPosition;
        if (this.m_ScrollMode == SyncScroll.ScrollMode.Normal)
        {
          ref Vector2 local = ref anchoredPosition;
          double x = (double) this.rectTransform.sizeDelta.x;
          Rect rect = this.parent.rect;
          double num1 = (double) ((Rect) ref rect).size.x * 0.5;
          double num2 = (x + num1) * (double) this.m_ScrollRect.normalizedPosition.x;
          local.x = (float) num2;
        }
        else
        {
          ref Vector2 local = ref anchoredPosition;
          double x = (double) this.rectTransform.sizeDelta.x;
          Rect rect = this.parent.rect;
          double num3 = (double) ((Rect) ref rect).size.x * 0.5;
          double num4 = -(x - num3) * (double) this.m_ScrollRect.normalizedPosition.x;
          local.x = (float) num4;
        }
        this.rectTransform.anchoredPosition = anchoredPosition;
      }
      if (!this.m_ScrollRect.vertical)
        return;
      Vector2 anchoredPosition1 = this.rectTransform.anchoredPosition;
      if (this.m_ScrollMode == SyncScroll.ScrollMode.Normal)
      {
        ref Vector2 local = ref anchoredPosition1;
        double y = (double) this.rectTransform.sizeDelta.y;
        Rect rect1 = this.parent.rect;
        double num5 = (double) ((Rect) ref rect1).size.y * 0.5;
        double num6 = -(y - num5) * (double) this.m_ScrollRect.normalizedPosition.y;
        Rect rect2 = this.parent.rect;
        double num7 = (double) ((Rect) ref rect2).size.y * 0.5;
        double num8 = num6 + num7;
        local.y = (float) num8;
      }
      else
      {
        ref Vector2 local = ref anchoredPosition1;
        double y = (double) this.rectTransform.sizeDelta.y;
        Rect rect = this.parent.rect;
        double num9 = (double) ((Rect) ref rect).size.y * 0.5;
        double num10 = -(y - num9) * (double) this.m_ScrollRect.normalizedPosition.y;
        local.y = (float) num10;
      }
      this.rectTransform.anchoredPosition = anchoredPosition1;
    }

    public enum ScrollMode
    {
      Normal,
      Reverse,
    }
  }
}
