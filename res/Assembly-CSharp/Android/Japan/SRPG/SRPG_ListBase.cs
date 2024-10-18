// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ListBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class SRPG_ListBase : MonoBehaviour
  {
    private List<ListItemEvents> mItems = new List<ListItemEvents>(32);
    private Transform mItemBodyPool;
    private ScrollRect mScrollRect;
    private RectTransform mTransform;
    private RectTransform mScrollRectTransform;

    public void AddItem(ListItemEvents item)
    {
      this.mItems.Add(item);
      this.InitPool();
      item.DetachBody(this.mItemBodyPool);
    }

    public void ClearItems()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if ((UnityEngine.Object) this.mItems[index].Body != (UnityEngine.Object) null)
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index].Body.gameObject);
          this.mItems[index].Body = (Transform) null;
        }
      }
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
    }

    protected bool IsEmpty
    {
      get
      {
        return this.mItems.Count == 0;
      }
    }

    protected ListItemEvents[] Items
    {
      get
      {
        return this.mItems.ToArray();
      }
    }

    private void InitPool()
    {
      if (!((UnityEngine.Object) this.mItemBodyPool == (UnityEngine.Object) null))
        return;
      this.mItemBodyPool = (Transform) UIUtility.Pool;
    }

    protected virtual ScrollRect GetScrollRect()
    {
      return this.GetComponentInParent<ScrollRect>();
    }

    protected virtual RectTransform GetRectTransform()
    {
      return this.transform as RectTransform;
    }

    protected virtual void Start()
    {
      this.mScrollRect = this.GetScrollRect();
      this.mTransform = this.GetRectTransform();
      if ((UnityEngine.Object) this.mScrollRect != (UnityEngine.Object) null)
        this.mScrollRectTransform = this.mScrollRect.transform as RectTransform;
      this.InitPool();
    }

    protected virtual void OnDestroy()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if ((UnityEngine.Object) this.mItems[index] != (UnityEngine.Object) null && (UnityEngine.Object) this.mItems[index].Body != (UnityEngine.Object) null)
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) this.mItems[index].Body.gameObject);
          this.mItems[index].Body = (Transform) null;
        }
      }
    }

    protected virtual void LateUpdate()
    {
      if (!((UnityEngine.Object) this.mScrollRect != (UnityEngine.Object) null))
        return;
      Rect rect1 = this.mScrollRectTransform.rect;
      Vector2 vector2_1 = new Vector2();
      Vector2 vector2_2 = new Vector2();
      vector2_1.x = Mathf.Lerp(0.0f, rect1.width, this.mTransform.anchorMin.x);
      vector2_1.y = Mathf.Lerp(0.0f, rect1.height, this.mTransform.anchorMin.y);
      vector2_2.x = Mathf.Lerp(0.0f, rect1.width, this.mTransform.anchorMax.x);
      vector2_2.y = Mathf.Lerp(0.0f, rect1.height, this.mTransform.anchorMax.y);
      Vector2 vector2_3 = this.mTransform.anchoredPosition + this.mTransform.rect.position + new Vector2() { x = Mathf.Lerp(vector2_1.x, vector2_2.x, this.mTransform.pivot.x), y = Mathf.Lerp(vector2_1.y, vector2_2.y, this.mTransform.pivot.y) };
      float height = this.mTransform.rect.height;
      rect1.position = -vector2_3;
      for (int index = this.mItems.Count - 1; index >= 0; --index)
      {
        if (!((UnityEngine.Object) this.mItems[index] == (UnityEngine.Object) null) && this.mItems[index].gameObject.activeInHierarchy)
        {
          RectTransform rectTransform = this.mItems[index].GetRectTransform();
          Rect rect2 = rectTransform.rect;
          rect2.x += rectTransform.anchoredPosition.x - this.mItems[index].DisplayRectMergin.x;
          rect2.y += height + rectTransform.anchoredPosition.y - this.mItems[index].DisplayRectMergin.y;
          if ((double) this.mItems[index].ParentScale.y < 1.0)
          {
            rect2.y += rect2.height * (1f - this.mItems[index].ParentScale.y) * (float) index;
            rect2.height *= this.mItems[index].ParentScale.y;
          }
          if (rect2.Overlaps(rect1))
            this.mItems[index].AttachBody();
          else
            this.mItems[index].DetachBody(this.mItemBodyPool);
        }
      }
    }
  }
}
