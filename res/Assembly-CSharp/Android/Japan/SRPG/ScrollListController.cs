// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollListController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class ScrollListController : MonoBehaviour
  {
    [SerializeField]
    [Range(0.0f, 30f)]
    protected int m_ItemCnt = 8;
    public ScrollListController.OnItemPositionChange OnItemUpdate = new ScrollListController.OnItemPositionChange();
    public ScrollListController.OnAfterStartUpEvent OnAfterStartup = new ScrollListController.OnAfterStartUpEvent();
    public ScrollListController.OnUpdateEvent OnUpdateItemEvent = new ScrollListController.OnUpdateEvent();
    private List<Vector2> m_ItemPos = new List<Vector2>();
    public float Space = 1.2f;
    private float m_ItemScale = -1f;
    [SerializeField]
    private RectTransform m_ItemBase;
    public List<RectTransform> m_ItemList;
    private float m_PrevPosition;
    private int m_CurrentItemID;
    public ScrollListController.Direction m_Direction;
    public ScrollListController.Mode m_ScrollMode;
    private RectTransform m_RectTransform;

    protected RectTransform GetRectTransForm
    {
      get
      {
        if ((UnityEngine.Object) this.m_RectTransform == (UnityEngine.Object) null)
          this.m_RectTransform = this.GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    public float AnchoredPosition
    {
      get
      {
        if (this.m_ScrollMode == ScrollListController.Mode.Normal)
        {
          if (this.m_Direction == ScrollListController.Direction.Vertical)
            return -this.GetRectTransForm.anchoredPosition.y;
          return this.GetRectTransForm.anchoredPosition.x;
        }
        if (this.m_Direction == ScrollListController.Direction.Vertical)
          return this.GetRectTransForm.anchoredPosition.y;
        return this.GetRectTransForm.anchoredPosition.x;
      }
    }

    public float ItemScale
    {
      get
      {
        if ((UnityEngine.Object) this.m_ItemBase != (UnityEngine.Object) null && (double) this.m_ItemScale == -1.0)
          this.m_ItemScale = this.m_Direction != ScrollListController.Direction.Vertical ? this.m_ItemBase.sizeDelta.x : this.m_ItemBase.sizeDelta.y;
        return this.m_ItemScale;
      }
    }

    public float ScrollDir
    {
      get
      {
        return this.m_ScrollMode == ScrollListController.Mode.Normal ? -1f : 1f;
      }
    }

    public List<RectTransform> ItemList
    {
      get
      {
        return this.m_ItemList;
      }
    }

    public List<Vector2> ItemPosList
    {
      get
      {
        return this.m_ItemPos;
      }
    }

    protected virtual void Start()
    {
      List<ScrollListSetUp> list = ((IEnumerable<MonoBehaviour>) this.GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>();
      this.GetComponentInParent<ScrollRect>().content = this.GetRectTransForm;
      this.m_ItemBase.gameObject.SetActive(false);
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.m_ItemBase);
        rectTransform.SetParent(this.transform, false);
        rectTransform.anchoredPosition = this.m_Direction != ScrollListController.Direction.Horizontal ? new Vector2(0.0f, (float) ((double) this.ItemScale * (double) this.Space * (double) index + (double) this.ItemScale * 0.5) * this.ScrollDir) : new Vector2((float) ((double) this.ItemScale * (double) this.Space * (double) index + (double) this.ItemScale * 0.5) * this.ScrollDir, 0.0f);
        this.m_ItemList.Add(rectTransform);
        this.m_ItemPos.Add(rectTransform.anchoredPosition);
        rectTransform.gameObject.SetActive(true);
      }
      foreach (ScrollListSetUp scrollListSetUp in list)
      {
        scrollListSetUp.OnSetUpItems();
        for (int idx = 0; idx < this.m_ItemCnt; ++idx)
          scrollListSetUp.OnUpdateItems(idx, this.m_ItemList[idx].gameObject);
      }
      if (this.OnAfterStartup == null)
        return;
      this.OnAfterStartup.Invoke(true);
    }

    private void Update()
    {
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition < -((double) this.ItemScale * (double) this.Space + (double) this.ItemScale * 0.5))
      {
        this.m_PrevPosition -= this.ItemScale * this.Space;
        RectTransform rectTransform = this.m_ItemList[0];
        this.m_ItemList.RemoveAt(0);
        this.m_ItemList.Add(rectTransform);
        Vector2 vector2 = this.m_ItemPos.Last<Vector2>();
        if (this.m_Direction == ScrollListController.Direction.Horizontal)
        {
          float x = vector2.x + this.ItemScale * this.Space * this.ScrollDir;
          rectTransform.anchoredPosition = new Vector2(x, 0.0f);
        }
        else
        {
          float y = vector2.y + this.ItemScale * this.Space * this.ScrollDir;
          rectTransform.anchoredPosition = new Vector2(0.0f, y);
        }
        this.m_ItemPos.RemoveAt(0);
        this.m_ItemPos.Add(rectTransform.anchoredPosition);
        this.OnItemUpdate.Invoke(this.m_CurrentItemID + this.m_ItemCnt, rectTransform.gameObject);
        ++this.m_CurrentItemID;
      }
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition > -(double) this.ItemScale * 0.5)
      {
        this.m_PrevPosition += this.ItemScale * this.Space;
        int index = this.m_ItemCnt - 1;
        RectTransform rectTransform = this.m_ItemList[index];
        this.m_ItemList.RemoveAt(index);
        this.m_ItemList.Insert(0, rectTransform);
        --this.m_CurrentItemID;
        Vector2 itemPo = this.m_ItemPos[0];
        if (this.m_Direction == ScrollListController.Direction.Horizontal)
        {
          float x = itemPo.x - this.ItemScale * this.Space * this.ScrollDir;
          rectTransform.anchoredPosition = new Vector2(x, 0.0f);
        }
        else
        {
          float y = itemPo.y - this.ItemScale * this.Space * this.ScrollDir;
          rectTransform.anchoredPosition = new Vector2(0.0f, y);
        }
        this.m_ItemPos.RemoveAt(index);
        this.m_ItemPos.Insert(0, rectTransform.anchoredPosition);
        this.OnItemUpdate.Invoke(this.m_CurrentItemID, rectTransform.gameObject);
      }
      if (this.OnUpdateItemEvent == null)
        return;
      this.OnUpdateItemEvent.Invoke(this.m_ItemList);
    }

    public void UpdateList()
    {
      List<ScrollListSetUp> list = ((IEnumerable<MonoBehaviour>) this.GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>();
      this.GetComponentInParent<ScrollRect>().content = this.GetRectTransForm;
      this.m_ItemBase.gameObject.SetActive(false);
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        RectTransform rectTransform = this.m_ItemList[index];
        rectTransform.SetParent(this.transform, false);
        rectTransform.anchoredPosition = this.m_Direction != ScrollListController.Direction.Horizontal ? new Vector2(0.0f, (float) (-(double) this.ItemScale * (double) this.Space * (double) index - (double) this.ItemScale * 0.5)) : new Vector2((float) (-(double) this.ItemScale * (double) this.Space * (double) index - (double) this.ItemScale * 0.5), 0.0f);
        rectTransform.gameObject.SetActive(true);
      }
      foreach (ScrollListSetUp scrollListSetUp in list)
      {
        scrollListSetUp.OnSetUpItems();
        for (int idx = 0; idx < this.m_ItemCnt; ++idx)
          scrollListSetUp.OnUpdateItems(idx, this.m_ItemList[idx].gameObject);
      }
      this.m_PrevPosition = 0.0f;
      this.m_CurrentItemID = 0;
      RectTransform component = this.transform.GetComponent<RectTransform>();
      Vector2 anchoredPosition = component.anchoredPosition;
      anchoredPosition.y = 0.0f;
      component.anchoredPosition = anchoredPosition;
    }

    public void Refresh()
    {
      foreach (ScrollListSetUp scrollListSetUp in ((IEnumerable<MonoBehaviour>) this.GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>())
      {
        for (int idx = 0; idx < this.m_ItemList.Count; ++idx)
        {
          RectTransform rectTransform = this.m_ItemList[idx];
          rectTransform.anchoredPosition = this.m_Direction != ScrollListController.Direction.Horizontal ? new Vector2(0.0f, (float) ((double) this.ItemScale * (double) this.Space * (double) idx + (double) this.ItemScale * 0.5) * this.ScrollDir) : new Vector2((float) ((double) this.ItemScale * (double) this.Space * (double) idx + (double) this.ItemScale * 0.5) * this.ScrollDir, 0.0f);
          this.m_ItemPos[idx] = rectTransform.anchoredPosition;
          scrollListSetUp.OnUpdateItems(idx, rectTransform.gameObject);
        }
      }
      this.m_PrevPosition = 0.0f;
      this.m_CurrentItemID = 0;
    }

    public void ClearItem()
    {
      if (this.m_ItemList == null)
        return;
      for (int index = 0; index < this.m_ItemList.Count; ++index)
      {
        if ((UnityEngine.Object) this.m_ItemList[index] != (UnityEngine.Object) null)
          UnityEngine.Object.Destroy((UnityEngine.Object) this.m_ItemList[index]);
      }
      this.m_ItemList.Clear();
    }

    public bool MovePos(float goal, float move)
    {
      bool flag1 = false;
      Vector2 anchoredPosition = this.GetRectTransForm.anchoredPosition;
      if (this.m_ScrollMode == ScrollListController.Mode.Normal)
        anchoredPosition.y *= -1f;
      bool flag2 = (double) anchoredPosition.y < (double) goal;
      float num = !flag2 ? -move : move;
      anchoredPosition.y += num;
      if (!flag2 ? (double) anchoredPosition.y < (double) goal : (double) anchoredPosition.y > (double) goal)
      {
        anchoredPosition.y = goal;
        flag1 = true;
      }
      this.GetRectTransForm.anchoredPosition = anchoredPosition;
      return flag1;
    }

    [Serializable]
    public class OnItemPositionChange : UnityEvent<int, GameObject>
    {
    }

    [Serializable]
    public class OnAfterStartUpEvent : UnityEvent<bool>
    {
    }

    [Serializable]
    public class OnUpdateEvent : UnityEvent<List<RectTransform>>
    {
    }

    public enum Direction
    {
      Vertical,
      Horizontal,
    }

    public enum Mode
    {
      Normal,
      Reverse,
    }
  }
}
