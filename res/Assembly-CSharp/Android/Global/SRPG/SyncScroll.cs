// Decompiled with JetBrains decompiler
// Type: SRPG.SyncScroll
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.UI;

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
      get
      {
        return this.m_ScrollMode == SyncScroll.ScrollMode.Normal;
      }
      set
      {
        this.m_ScrollMode = !value ? SyncScroll.ScrollMode.Reverse : SyncScroll.ScrollMode.Normal;
      }
    }

    public bool isReverse
    {
      get
      {
        return this.m_ScrollMode == SyncScroll.ScrollMode.Reverse;
      }
      set
      {
        this.m_ScrollMode = !value ? SyncScroll.ScrollMode.Normal : SyncScroll.ScrollMode.Reverse;
      }
    }

    public SyncScroll.ScrollMode scrollMode
    {
      get
      {
        return this.m_ScrollMode;
      }
      set
      {
        this.m_ScrollMode = value;
      }
    }

    private void Awake()
    {
      this.rectTransform = this.GetComponent<RectTransform>();
      this.parent = this.GetComponentInParent<RectTransform>();
      this.enabled = (UnityEngine.Object) this.m_ScrollRect != (UnityEngine.Object) null && (UnityEngine.Object) this.rectTransform != (UnityEngine.Object) null;
    }

    private void LateUpdate()
    {
      if (this.m_ScrollRect.horizontal)
      {
        Vector2 anchoredPosition = this.rectTransform.anchoredPosition;
        anchoredPosition.x = this.m_ScrollMode != SyncScroll.ScrollMode.Normal ? (float) -((double) this.rectTransform.sizeDelta.x - (double) this.parent.rect.size.x * 0.5) * this.m_ScrollRect.normalizedPosition.x : (this.rectTransform.sizeDelta.x + this.parent.rect.size.x * 0.5f) * this.m_ScrollRect.normalizedPosition.x;
        this.rectTransform.anchoredPosition = anchoredPosition;
      }
      if (!this.m_ScrollRect.vertical)
        return;
      Vector2 anchoredPosition1 = this.rectTransform.anchoredPosition;
      anchoredPosition1.y = this.m_ScrollMode != SyncScroll.ScrollMode.Normal ? (float) -((double) this.rectTransform.sizeDelta.y - (double) this.parent.rect.size.y * 0.5) * this.m_ScrollRect.normalizedPosition.y : (float) (-((double) this.rectTransform.sizeDelta.y - (double) this.parent.rect.size.y * 0.5) * (double) this.m_ScrollRect.normalizedPosition.y + (double) this.parent.rect.size.y * 0.5);
      this.rectTransform.anchoredPosition = anchoredPosition1;
    }

    public enum ScrollMode
    {
      Normal,
      Reverse,
    }
  }
}
