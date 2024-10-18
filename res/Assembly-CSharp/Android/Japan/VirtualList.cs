// Decompiled with JetBrains decompiler
// Type: VirtualList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualList : UIBehaviour
{
  public Vector2 ItemSize = new Vector2(100f, 100f);
  private List<int> mItems = new List<int>();
  private List<VirtualList.ItemContainer> mItemObjects = new List<VirtualList.ItemContainer>();
  public ScrollRect ScrollRect;
  private bool mBoundsChanging;
  private bool mDestroyed;
  private float mLastScrollPosition;
  private int mNumVisibleItems;
  private bool mInitialized;
  public VirtualList.GetItemObjectEvent OnGetItemObject;
  public VirtualList.ListEvent OnPostListUpdate;

  public void AddItem(int id)
  {
    if (id < 0 || this.mItems.Contains(id))
      return;
    this.mItems.Add(id);
  }

  public void RemoveItem(int id)
  {
    this.mItems.Add(id);
  }

  public void ClearItems()
  {
    this.mItems.Clear();
  }

  public int NumVisibleItems
  {
    get
    {
      return this.mNumVisibleItems;
    }
  }

  public int NumItems
  {
    get
    {
      return this.mItems.Count;
    }
  }

  private float HorizontalNormalizedPosition
  {
    get
    {
      if ((Object) this.ScrollRect == (Object) null)
        return 0.0f;
      return Mathf.Clamp(this.ScrollRect.horizontalNormalizedPosition, 0.0f, 1f);
    }
  }

  public void Refresh(bool resetScrollPos = false)
  {
    this.RecalcBounds();
    if (resetScrollPos && this.ScrollRect.normalizedPosition != Vector2.zero)
      this.ScrollRect.normalizedPosition = Vector2.zero;
    this.Rebuild();
  }

  public RectTransform FindItem(int itemID)
  {
    VirtualList.ItemContainer itemContainer = this.FindItemContainer(itemID);
    if ((Object) itemContainer != (Object) null)
      return itemContainer.Body;
    return (RectTransform) null;
  }

  protected override void Awake()
  {
    base.Awake();
  }

  protected override void Start()
  {
    base.Start();
    RectTransform transform = this.transform as RectTransform;
    transform.pivot = new Vector2(0.0f, 1f);
    RectTransform rectTransform = transform;
    Vector2 pivot = transform.pivot;
    transform.anchorMax = pivot;
    Vector2 vector2 = pivot;
    rectTransform.anchorMin = vector2;
    this.RecalcBounds();
    if (this.ScrollRect.horizontal && this.ScrollRect.vertical)
      this.ScrollRect.vertical = false;
    this.mLastScrollPosition = 0.0f;
    this.ScrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnSrollRectChange));
    this.mInitialized = true;
    this.RecalcBounds();
    this.Rebuild();
  }

  protected override void OnDestroy()
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      if ((Object) this.mItemObjects[index].Body != (Object) null && (Object) this.mItemObjects[index].Body.parent == (Object) this.mItemObjects[index].Body)
      {
        this.mItemObjects[index].Body.SetParent((Transform) null, false);
        this.mItemObjects[index].Body = (RectTransform) null;
      }
    }
    this.mDestroyed = true;
    base.OnDestroy();
  }

  protected override void OnRectTransformDimensionsChange()
  {
    base.OnRectTransformDimensionsChange();
    if (this.mBoundsChanging || !this.mInitialized)
      return;
    this.ScrollRect.horizontalNormalizedPosition = this.HorizontalNormalizedPosition;
    this.RecalcBounds();
    this.Rebuild();
  }

  private void OnSrollRectChange(Vector2 pos)
  {
    if (!this.mInitialized)
      return;
    this.Rebuild();
  }

  private bool IsDiscarding()
  {
    return this.mDestroyed;
  }

  private bool IsHorizontal
  {
    get
    {
      return this.ScrollRect.horizontal;
    }
  }

  private int CalcPitch()
  {
    RectTransform transform = this.ScrollRect.transform as RectTransform;
    return !this.IsHorizontal ? (int) ((double) transform.rect.width / (double) this.ItemSize.x) : (int) ((double) transform.rect.height / (double) this.ItemSize.y);
  }

  private void RecalcBounds()
  {
    if (this.IsDiscarding())
      return;
    RectTransform transform = this.transform as RectTransform;
    Vector2 sizeDelta = transform.sizeDelta;
    Vector2 size = (this.ScrollRect.transform as RectTransform).rect.size;
    int num1 = this.CalcPitch();
    if (num1 <= 0)
      return;
    int num2 = (this.mItems.Count + num1 - 1) / num1;
    if (this.IsHorizontal)
    {
      sizeDelta.x = (float) num2 * this.ItemSize.x;
      sizeDelta.y = size.y;
    }
    else
    {
      sizeDelta.x = size.x;
      sizeDelta.y = (float) num2 * this.ItemSize.y;
    }
    this.mBoundsChanging = true;
    RectTransform rectTransform = transform;
    Vector2 vector2_1 = new Vector2(0.0f, 1f);
    transform.pivot = vector2_1;
    Vector2 vector2_2 = vector2_1;
    transform.anchorMax = vector2_2;
    Vector2 vector2_3 = vector2_2;
    rectTransform.anchorMin = vector2_3;
    transform.sizeDelta = sizeDelta;
    this.mBoundsChanging = false;
  }

  private void ReserveItems(int maxItems)
  {
    Transform transform = this.transform;
    this.mNumVisibleItems = maxItems;
    for (int count = this.mItemObjects.Count; count < maxItems; ++count)
    {
      VirtualList.ItemContainer component = new GameObject(count.ToString(), new System.Type[2]{ typeof (RectTransform), typeof (VirtualList.ItemContainer) }).GetComponent<VirtualList.ItemContainer>();
      RectTransform rectTr = component.RectTr;
      Vector2 vector2_1 = new Vector2(0.0f, 1f);
      component.RectTr.anchorMax = vector2_1;
      Vector2 vector2_2 = vector2_1;
      rectTr.anchorMin = vector2_2;
      component.RectTr.sizeDelta = this.ItemSize;
      component.RectTr.SetParent(transform, false);
      this.mItemObjects.Add(component);
    }
  }

  private void DisableAllItems()
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
      this.mItemObjects[index].SetBodyActive(false);
  }

  public void ForceUpdateItems()
  {
    if (this.OnGetItemObject == null)
      return;
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      if (this.mItemObjects[index].ItemID >= 0)
      {
        RectTransform rectTransform = this.OnGetItemObject(this.mItemObjects[index].ItemID, this.mItemObjects[index].ItemID, this.mItemObjects[index].Body);
      }
    }
  }

  private void Rebuild()
  {
    if (this.IsDiscarding())
      return;
    Rect rect1 = (this.transform as RectTransform).rect;
    int num1 = this.CalcPitch();
    if (num1 <= 0)
      return;
    Rect rect2 = (this.ScrollRect.transform as RectTransform).rect;
    bool isHorizontal = this.IsHorizontal;
    int num2;
    int num3;
    int num4;
    int num5;
    if (isHorizontal)
    {
      this.ReserveItems(Mathf.Max(Mathf.CeilToInt(rect2.width / this.ItemSize.x) + 1, 1) * num1);
      if ((double) rect1.width <= 0.0)
      {
        this.DisableAllItems();
        return;
      }
      num2 = Mathf.FloorToInt(-this.HorizontalNormalizedPosition * ((double) rect1.width >= (double) rect2.width ? rect2.width - rect1.width : 0.0f) / this.ItemSize.x);
      if ((double) this.mLastScrollPosition <= (double) this.HorizontalNormalizedPosition)
      {
        num3 = 0;
        num4 = this.mItemObjects.Count;
        num5 = 1;
      }
      else
      {
        num3 = this.mItemObjects.Count - 1;
        num4 = -1;
        num5 = -1;
      }
      this.mLastScrollPosition = this.HorizontalNormalizedPosition;
    }
    else
    {
      this.ReserveItems(num1 * Mathf.Max(Mathf.CeilToInt(rect2.width / this.ItemSize.x), 1));
      if ((double) rect1.height <= 0.0)
      {
        this.DisableAllItems();
        return;
      }
      num2 = Mathf.FloorToInt((float) -(1.0 - (double) this.ScrollRect.verticalNormalizedPosition) * (rect2.height - rect1.height) / this.ItemSize.y);
      if ((double) this.mLastScrollPosition >= (double) this.ScrollRect.verticalNormalizedPosition)
      {
        num3 = 0;
        num4 = this.mItemObjects.Count;
        num5 = 1;
      }
      else
      {
        num3 = this.mItemObjects.Count - 1;
        num4 = -1;
        num5 = -1;
      }
      this.mLastScrollPosition = this.ScrollRect.verticalNormalizedPosition;
    }
    bool flag = false;
    int index1 = num3;
    while (index1 != num4)
    {
      RectTransform rectTr = this.mItemObjects[index1].RectTr;
      int num6;
      int num7;
      int index2;
      if (isHorizontal)
      {
        num6 = index1 / num1 + num2;
        num7 = index1 % num1;
        index2 = num6 * num1 + num7;
      }
      else
      {
        num6 = index1 % num1;
        num7 = index1 / num1 + num2;
        index2 = num6 + num7 * num1;
      }
      Vector2 sizeDelta = rectTr.sizeDelta;
      Vector2 pivot = rectTr.pivot;
      float x = (float) ((double) num6 * (double) this.ItemSize.x + (double) sizeDelta.x * (double) pivot.x);
      float y = (float) ((double) -num7 * (double) this.ItemSize.y - (double) sizeDelta.y * (double) pivot.y);
      if (0 <= index2 && index2 < this.mItems.Count && index1 < this.mItems.Count)
      {
        VirtualList.ItemContainer mItemObject = this.mItemObjects[index1];
        mItemObject.SetBodyActive(true);
        this.FillContainer(mItemObject, this.mItems[index2]);
      }
      else
        this.mItemObjects[index1].SetBodyActive(false);
      rectTr.anchoredPosition = new Vector2(x, y);
      index1 += num5;
    }
    if (flag)
      this.ReparentItems();
    if (this.OnPostListUpdate == null)
      return;
    this.OnPostListUpdate();
  }

  private int FindItemAtPosition(int itemID, float x, float y)
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      float num1 = Mathf.Abs(x - this.mItemObjects[index].RectTr.anchoredPosition.x);
      float num2 = Mathf.Abs(y - this.mItemObjects[index].RectTr.anchoredPosition.y);
      if (this.mItemObjects[index].ItemID == itemID && (double) num1 <= 0.00999999977648258 && (double) num2 <= 0.00999999977648258)
        return index;
    }
    return -1;
  }

  private VirtualList.ItemContainer FindItemContainer(int itemID)
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      if (this.mItemObjects[index].ItemID == itemID)
        return this.mItemObjects[index];
    }
    return (VirtualList.ItemContainer) null;
  }

  private void FillContainer(VirtualList.ItemContainer container, int itemID)
  {
    RectTransform rectTransform = (RectTransform) null;
    if (this.OnGetItemObject != null)
      rectTransform = this.OnGetItemObject(itemID, !((Object) container.Body != (Object) null) ? -1 : container.ItemID, container.Body);
    if ((Object) rectTransform == (Object) null)
      return;
    if ((Object) container.Body != (Object) null && (Object) container.Body != (Object) rectTransform && (Object) container.Body.parent == (Object) container.RectTr)
      container.Body.SetParent((Transform) null, false);
    container.Body = rectTransform;
    container.ItemID = itemID;
    rectTransform.SetParent((Transform) container.RectTr, false);
    if ((double) rectTransform.anchoredPosition.sqrMagnitude <= 0.0)
      return;
    rectTransform.anchoredPosition = Vector2.zero;
  }

  private void ReparentItems()
  {
    for (int index = 0; index < this.mItemObjects.Count; ++index)
    {
      if ((Object) this.mItemObjects[index].Body != (Object) null && (Object) this.mItemObjects[index].Body.parent != (Object) this.mItemObjects[index].RectTr)
        this.mItemObjects[index].Body.SetParent((Transform) this.mItemObjects[index].RectTr, false);
    }
  }

  private void SwapFast(VirtualList.ItemContainer a, VirtualList.ItemContainer b)
  {
    int itemId = a.ItemID;
    a.ItemID = b.ItemID;
    b.ItemID = itemId;
    RectTransform body = a.Body;
    a.Body = b.Body;
    b.Body = body;
  }

  public delegate RectTransform GetItemObjectEvent(int item, int old, RectTransform current);

  public delegate void ListEvent();

  private class ItemContainer : MonoBehaviour
  {
    public int ItemID = -1;
    public RectTransform RectTr;
    public RectTransform Body;

    private void Awake()
    {
      this.RectTr = this.transform as RectTransform;
    }

    public void SetBodyActive(bool active)
    {
      if (!((Object) this.Body != (Object) null))
        return;
      this.Body.gameObject.SetActive(active);
    }
  }
}
