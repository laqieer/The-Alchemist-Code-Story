// Decompiled with JetBrains decompiler
// Type: SRPG.SRPG_ListBase
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class SRPG_ListBase : MonoBehaviour
  {
    private Transform mItemBodyPool;
    private List<ListItemEvents> mItems = new List<ListItemEvents>(32);
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
        if (Object.op_Inequality((Object) this.mItems[index].Body, (Object) null))
        {
          Object.Destroy((Object) ((Component) this.mItems[index].Body).gameObject);
          this.mItems[index].Body = (Transform) null;
        }
      }
      GameUtility.DestroyGameObjects<ListItemEvents>(this.mItems);
      this.mItems.Clear();
    }

    protected bool IsEmpty => this.mItems.Count == 0;

    protected ListItemEvents[] Items => this.mItems.ToArray();

    private void InitPool()
    {
      if (!Object.op_Equality((Object) this.mItemBodyPool, (Object) null))
        return;
      this.mItemBodyPool = (Transform) UIUtility.Pool;
    }

    protected virtual ScrollRect GetScrollRect()
    {
      return ((Component) this).GetComponentInParent<ScrollRect>();
    }

    protected virtual RectTransform GetRectTransform()
    {
      return ((Component) this).transform as RectTransform;
    }

    protected virtual void Start()
    {
      this.mScrollRect = this.GetScrollRect();
      this.mTransform = this.GetRectTransform();
      if (Object.op_Inequality((Object) this.mScrollRect, (Object) null))
        this.mScrollRectTransform = ((Component) this.mScrollRect).transform as RectTransform;
      this.InitPool();
    }

    protected virtual void OnDestroy()
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (Object.op_Inequality((Object) this.mItems[index], (Object) null) && Object.op_Inequality((Object) this.mItems[index].Body, (Object) null))
        {
          Object.Destroy((Object) ((Component) this.mItems[index].Body).gameObject);
          this.mItems[index].Body = (Transform) null;
        }
      }
    }

    protected virtual void LateUpdate()
    {
      if (!Object.op_Inequality((Object) this.mScrollRect, (Object) null))
        return;
      Rect rect1 = this.mScrollRectTransform.rect;
      Vector2 vector2_1 = new Vector2();
      Vector2 vector2_2 = new Vector2();
      vector2_1.x = Mathf.Lerp(0.0f, ((Rect) ref rect1).width, this.mTransform.anchorMin.x);
      vector2_1.y = Mathf.Lerp(0.0f, ((Rect) ref rect1).height, this.mTransform.anchorMin.y);
      vector2_2.x = Mathf.Lerp(0.0f, ((Rect) ref rect1).width, this.mTransform.anchorMax.x);
      vector2_2.y = Mathf.Lerp(0.0f, ((Rect) ref rect1).height, this.mTransform.anchorMax.y);
      Vector2 vector2_3 = new Vector2();
      vector2_3.x = Mathf.Lerp(vector2_1.x, vector2_2.x, this.mTransform.pivot.x);
      vector2_3.y = Mathf.Lerp(vector2_1.y, vector2_2.y, this.mTransform.pivot.y);
      Vector2 anchoredPosition = this.mTransform.anchoredPosition;
      Rect rect2 = this.mTransform.rect;
      Vector2 position = ((Rect) ref rect2).position;
      Vector2 vector2_4 = Vector2.op_Addition(Vector2.op_Addition(anchoredPosition, position), vector2_3);
      Rect rect3 = this.mTransform.rect;
      float height = ((Rect) ref rect3).height;
      ((Rect) ref rect1).position = Vector2.op_UnaryNegation(vector2_4);
      for (int index = this.mItems.Count - 1; index >= 0; --index)
      {
        if (!Object.op_Equality((Object) this.mItems[index], (Object) null) && ((Component) this.mItems[index]).gameObject.activeInHierarchy)
        {
          RectTransform rectTransform = this.mItems[index].GetRectTransform();
          Rect rect4 = rectTransform.rect;
          ref Rect local1 = ref rect4;
          ((Rect) ref local1).x = ((Rect) ref local1).x + (rectTransform.anchoredPosition.x - this.mItems[index].DisplayRectMergin.x);
          ref Rect local2 = ref rect4;
          ((Rect) ref local2).y = ((Rect) ref local2).y + (height + rectTransform.anchoredPosition.y - this.mItems[index].DisplayRectMergin.y);
          if ((double) this.mItems[index].ParentScale.y < 1.0)
          {
            ref Rect local3 = ref rect4;
            ((Rect) ref local3).y = ((Rect) ref local3).y + ((Rect) ref rect4).height * (1f - this.mItems[index].ParentScale.y) * (float) index;
            ref Rect local4 = ref rect4;
            ((Rect) ref local4).height = ((Rect) ref local4).height * this.mItems[index].ParentScale.y;
          }
          if (((Rect) ref rect4).Overlaps(rect1))
            this.mItems[index].AttachBody();
          else
            this.mItems[index].DetachBody(this.mItemBodyPool);
        }
      }
    }
  }
}
