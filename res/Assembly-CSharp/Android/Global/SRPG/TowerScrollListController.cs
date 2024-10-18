// Decompiled with JetBrains decompiler
// Type: SRPG.TowerScrollListController
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(0, "背景ロード完了", FlowNode.PinTypes.Output, 0)]
  public class TowerScrollListController : MonoBehaviour
  {
    private static string BGTexturePath = "Tower/TowerBGs";
    private static string FloorBGTexturePath = "Tower/TowerFloors";
    private static string LockFloorBGTexturePath = "Tower/TowerLockFloors";
    [SerializeField]
    [Range(0.0f, 30f)]
    protected int m_ItemCnt = 8;
    [SerializeField]
    private float m_Margin = 1.1f;
    public TowerScrollListController.OnItemPositionChange OnItemUpdate = new TowerScrollListController.OnItemPositionChange();
    public TowerScrollListController.ListItemFocusEvent OnListItemFocus = new TowerScrollListController.ListItemFocusEvent();
    private float m_ItemScale = -1f;
    [SerializeField]
    private RectTransform m_ItemBase;
    [SerializeField]
    internal TowerScrollListController.ScrollMode m_ScrollMode;
    public List<RectTransform> m_ItemList;
    private float m_PrevPosition;
    private int m_CurrentItemID;
    public TowerScrollListController.Direction m_Direction;
    private RectTransform m_RectTransform;
    [SerializeField]
    private RectTransform Cursor;
    [SerializeField]
    private ScrollAutoFit m_ScrollAutoFit;
    [SerializeField]
    private SyncScroll m_ScrollBG;
    public Selectable PageUpButton;
    public Selectable PageDownButton;
    [SerializeField]
    private Button mChallengeButton;
    [SerializeField]
    private Animator FadeAnimator;
    [SerializeField]
    private RawImage Bg;

    public float Margin
    {
      get
      {
        return this.m_Margin;
      }
    }

    protected RectTransform GetRectTransForm
    {
      get
      {
        if ((UnityEngine.Object) this.m_RectTransform == (UnityEngine.Object) null)
          this.m_RectTransform = this.GetComponent<RectTransform>();
        return this.m_RectTransform;
      }
    }

    public float ItemScale
    {
      get
      {
        if ((UnityEngine.Object) this.m_ItemBase != (UnityEngine.Object) null && (double) this.m_ItemScale == -1.0)
          this.m_ItemScale = this.m_Direction != TowerScrollListController.Direction.Vertical ? this.m_ItemBase.sizeDelta.x : this.m_ItemBase.sizeDelta.y;
        return this.m_ItemScale;
      }
    }

    public float ItemScaleMargin
    {
      get
      {
        return this.ItemScale * this.Margin;
      }
    }

    internal static void SetAnchor(RectTransform rt, TowerScrollListController.ScrollMode scrollMode)
    {
      if ((UnityEngine.Object) rt == (UnityEngine.Object) null)
        return;
      if (scrollMode == TowerScrollListController.ScrollMode.Normal)
      {
        Vector2 anchoredPosition = rt.anchoredPosition;
        float x1 = rt.anchorMin.x;
        float x2 = rt.anchorMax.x;
        rt.anchorMin = new Vector2(x1, 1f);
        rt.anchorMax = new Vector2(x2, 1f);
        rt.pivot = new Vector2(0.0f, 1f);
        rt.anchoredPosition = anchoredPosition;
      }
      else
      {
        Vector2 anchoredPosition = rt.anchoredPosition;
        float x1 = rt.anchorMin.x;
        float x2 = rt.anchorMax.x;
        rt.anchorMin = new Vector2(x1, 0.0f);
        rt.anchorMax = new Vector2(x2, 0.0f);
        rt.pivot = new Vector2(0.0f, 0.0f);
        rt.anchoredPosition = anchoredPosition;
      }
    }

    internal static void SetItemAnchor(RectTransform rt, TowerScrollListController.ScrollMode scrollMode)
    {
      if ((UnityEngine.Object) rt == (UnityEngine.Object) null)
        return;
      if (scrollMode == TowerScrollListController.ScrollMode.Normal)
      {
        Vector2 anchoredPosition = rt.anchoredPosition;
        anchoredPosition.y = -anchoredPosition.y;
        float x1 = rt.anchorMin.x;
        float x2 = rt.anchorMax.x;
        rt.anchorMin = new Vector2(x1, 1f);
        rt.anchorMax = new Vector2(x2, 1f);
        rt.anchoredPosition = anchoredPosition;
      }
      else
      {
        Vector2 anchoredPosition = rt.anchoredPosition;
        anchoredPosition.y = -anchoredPosition.y;
        float x1 = rt.anchorMin.x;
        float x2 = rt.anchorMax.x;
        rt.anchorMin = new Vector2(x1, 0.0f);
        rt.anchorMax = new Vector2(x2, 0.0f);
        rt.anchoredPosition = anchoredPosition;
      }
    }

    internal void SetAnchor(TowerScrollListController.ScrollMode scrollMode)
    {
      if (!Application.isPlaying)
      {
        TowerScrollListController.SetAnchor(this.GetRectTransForm, scrollMode);
        if ((UnityEngine.Object) this.m_ScrollBG != (UnityEngine.Object) null)
        {
          TowerScrollListController.SetAnchor(this.m_ScrollBG.GetComponent<RectTransform>(), scrollMode);
          this.m_ScrollBG.isNormal = scrollMode == TowerScrollListController.ScrollMode.Normal;
        }
        TowerScrollListController.SetItemAnchor(this.m_ItemBase, scrollMode);
      }
      else
      {
        TowerScrollListController.SetAnchor(this.GetRectTransForm, scrollMode);
        if ((UnityEngine.Object) this.m_ScrollBG != (UnityEngine.Object) null)
        {
          TowerScrollListController.SetAnchor(this.m_ScrollBG.GetComponent<RectTransform>(), scrollMode);
          this.m_ScrollBG.isNormal = scrollMode == TowerScrollListController.ScrollMode.Normal;
        }
        using (List<RectTransform>.Enumerator enumerator = this.m_ItemList.GetEnumerator())
        {
          while (enumerator.MoveNext())
            TowerScrollListController.SetItemAnchor(enumerator.Current, scrollMode);
        }
      }
    }

    protected virtual void Start()
    {
      List<ScrollListSetUp> list = ((IEnumerable<MonoBehaviour>) this.GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>();
      if ((UnityEngine.Object) this.m_ScrollAutoFit != (UnityEngine.Object) null)
      {
        this.m_ScrollAutoFit.content = this.GetRectTransForm;
        this.m_ScrollAutoFit.ItemScale = this.ItemScaleMargin;
        this.m_ScrollAutoFit.OnScrollStop.AddListener(new UnityAction(this.OnScrollStop));
      }
      this.m_ItemBase.gameObject.SetActive(false);
      float num = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      List<TowerQuestListItem> towerQuestListItemList = new List<TowerQuestListItem>();
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        RectTransform rectTransform = UnityEngine.Object.Instantiate<RectTransform>(this.m_ItemBase);
        rectTransform.SetParent(this.transform, false);
        rectTransform.anchoredPosition = new Vector2(0.0f, (float) ((double) this.ItemScale * (double) this.Margin * (double) index + (double) this.ItemScale * 0.5) * num);
        this.m_ItemList.Add(rectTransform);
        TowerQuestListItem component = rectTransform.GetComponent<TowerQuestListItem>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          towerQuestListItemList.Add(component);
        rectTransform.gameObject.SetActive(true);
      }
      using (List<ScrollListSetUp>.Enumerator enumerator = list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ScrollListSetUp current = enumerator.Current;
          current.OnSetUpItems();
          for (int idx = 0; idx < this.m_ItemCnt; ++idx)
            current.OnUpdateItems(idx, this.m_ItemList[idx].gameObject);
        }
      }
      this.m_ScrollMode = !MonoSingleton<GameManager>.Instance.FindTower(GlobalVars.SelectedTowerID).is_down ? TowerScrollListController.ScrollMode.Reverse : TowerScrollListController.ScrollMode.Normal;
      this.ChangeScrollMode(this.m_ScrollMode);
      this.StartCoroutine(this.LoadTowerBG(towerQuestListItemList.ToArray()));
    }

    private float AnchoredPosition
    {
      get
      {
        if (this.m_ScrollMode == TowerScrollListController.ScrollMode.Normal)
        {
          if (this.m_Direction == TowerScrollListController.Direction.Vertical)
            return -this.GetRectTransForm.anchoredPosition.y;
          return this.GetRectTransForm.anchoredPosition.x;
        }
        if (this.m_Direction == TowerScrollListController.Direction.Vertical)
          return this.GetRectTransForm.anchoredPosition.y;
        return this.GetRectTransForm.anchoredPosition.x;
      }
    }

    public void SetAnchoredPosition(float position)
    {
      if (this.m_Direction == TowerScrollListController.Direction.Vertical)
      {
        Vector2 anchoredPosition = this.GetRectTransForm.anchoredPosition;
        anchoredPosition.y = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? -position : position;
        this.GetRectTransForm.anchoredPosition = anchoredPosition;
      }
      else
      {
        Vector2 anchoredPosition = this.GetRectTransForm.anchoredPosition;
        anchoredPosition.x = position;
        this.GetRectTransForm.anchoredPosition = anchoredPosition;
      }
    }

    private void OnScrollStop()
    {
      float scrollDir = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      this.MovePosition(scrollDir);
      Rect rect = this.Cursor.rect;
      rect.center = new Vector2(0.0f, (float) ((double) this.ItemScaleMargin * 3.0 - (double) this.ItemScaleMargin * 0.5) * scrollDir);
      rect.size = this.Cursor.sizeDelta;
      using (List<RectTransform>.Enumerator enumerator = this.m_ItemList.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          RectTransform current = enumerator.Current;
          if (current.gameObject.activeInHierarchy)
          {
            Vector2 anchoredPosition = current.anchoredPosition;
            anchoredPosition.y = this.GetRectTransForm.anchoredPosition.y + anchoredPosition.y;
            if (rect.Contains(anchoredPosition))
              this.OnListItemFocus.Invoke(current.gameObject);
          }
        }
      }
    }

    private void FocusUpdate()
    {
      float num = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      Rect rect = this.Cursor.rect;
      rect.center = new Vector2(0.0f, (float) ((double) this.ItemScaleMargin * 3.0 - (double) this.ItemScaleMargin * 0.5) * num);
      rect.size = this.Cursor.sizeDelta;
      using (List<TowerQuestListItem>.Enumerator enumerator = this.m_ItemList.ConvertAll<TowerQuestListItem>((Converter<RectTransform, TowerQuestListItem>) (item => item.GetComponent<TowerQuestListItem>())).GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TowerQuestListItem current = enumerator.Current;
          if (current.gameObject.activeInHierarchy)
          {
            Vector2 anchoredPosition = current.rectTransform.anchoredPosition;
            anchoredPosition.y = this.GetRectTransForm.anchoredPosition.y + anchoredPosition.y;
            current.OnFocus(rect.Contains(anchoredPosition));
          }
        }
      }
    }

    private void LateUpdate()
    {
      this.MovePosition(this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f);
      float num1 = Mathf.Abs(Vector2.Dot(this.m_ScrollAutoFit.normalizedPosition, this.ScrollDir));
      RectTransform transform1 = this.m_ScrollAutoFit.transform as RectTransform;
      RectTransform transform2 = this.m_ScrollAutoFit.content.transform as RectTransform;
      if ((UnityEngine.Object) this.m_ScrollAutoFit.content != (UnityEngine.Object) null)
      {
        float num2 = Mathf.Abs(Vector2.Dot(transform1.rect.size, this.ScrollDir));
        float num3 = Mathf.Abs(Vector2.Dot(transform2.rect.size, this.ScrollDir));
        if (this.m_ScrollAutoFit.horizontal)
          num1 = 1f - num1;
        if ((UnityEngine.Object) this.PageUpButton != (UnityEngine.Object) null)
          this.PageUpButton.interactable = (double) num1 < 0.999000012874603 && (double) num2 < (double) num3;
        if ((UnityEngine.Object) this.PageDownButton != (UnityEngine.Object) null)
          this.PageDownButton.interactable = (double) num1 > 1.0 / 1000.0 && (double) num2 < (double) num3;
      }
      this.FocusUpdate();
    }

    private void MovePosition(float scrollDir)
    {
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition < -((double) this.ItemScaleMargin + (double) this.ItemScale * 0.5))
      {
        this.m_PrevPosition -= this.ItemScaleMargin;
        RectTransform rectTransform1 = this.m_ItemList[0];
        RectTransform rectTransform2 = this.m_ItemList.Last<RectTransform>();
        this.m_ItemList.RemoveAt(0);
        this.m_ItemList.Add(rectTransform1);
        float y = rectTransform2.anchoredPosition.y + this.ItemScaleMargin * scrollDir;
        rectTransform1.anchoredPosition = new Vector2(0.0f, y);
        this.OnItemUpdate.Invoke(this.m_CurrentItemID + this.m_ItemCnt, rectTransform1.gameObject);
        ++this.m_CurrentItemID;
      }
      while ((double) this.AnchoredPosition - (double) this.m_PrevPosition > -(double) this.ItemScale * 0.5)
      {
        this.m_PrevPosition += this.ItemScaleMargin;
        int index = this.m_ItemCnt - 1;
        RectTransform rectTransform1 = this.m_ItemList[index];
        RectTransform rectTransform2 = this.m_ItemList[0];
        this.m_ItemList.RemoveAt(index);
        this.m_ItemList.Insert(0, rectTransform1);
        --this.m_CurrentItemID;
        float y = rectTransform2.anchoredPosition.y - this.ItemScaleMargin * scrollDir;
        rectTransform1.anchoredPosition = new Vector2(0.0f, y);
        this.OnItemUpdate.Invoke(this.m_CurrentItemID, rectTransform1.gameObject);
      }
    }

    public void UpdateList()
    {
      List<ScrollListSetUp> list = ((IEnumerable<MonoBehaviour>) this.GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>();
      this.GetComponentInParent<ScrollRect>().content = this.GetRectTransForm;
      this.m_ItemBase.gameObject.SetActive(false);
      float num = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        RectTransform rectTransform = this.m_ItemList[index];
        rectTransform.SetParent(this.transform, false);
        rectTransform.anchoredPosition = new Vector2(0.0f, (float) ((double) this.ItemScale * (double) this.Margin * (double) index + (double) this.ItemScale * 0.5) * num);
        rectTransform.gameObject.SetActive(true);
      }
      using (List<ScrollListSetUp>.Enumerator enumerator = list.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          ScrollListSetUp current = enumerator.Current;
          current.OnSetUpItems();
          for (int idx = 0; idx < this.m_ItemCnt; ++idx)
            current.OnUpdateItems(idx, this.m_ItemList[idx].gameObject);
        }
      }
      this.m_PrevPosition = 0.0f;
      this.m_CurrentItemID = 0;
      RectTransform component = this.transform.GetComponent<RectTransform>();
      Vector2 anchoredPosition = component.anchoredPosition;
      anchoredPosition.y = 0.0f;
      component.anchoredPosition = anchoredPosition;
    }

    public void ChangeScrollMode(TowerScrollListController.ScrollMode scrollMode)
    {
      this.m_ScrollMode = scrollMode;
      this.SetAnchor(scrollMode);
      this.m_ItemList.Reverse();
      if (!Application.isPlaying)
        return;
      this.UpdateList();
    }

    private void _SetScrollTo(float pos)
    {
      float y = this.m_ScrollAutoFit.viewport.rect.size.y;
      float num1 = (float) ((double) this.m_ScrollAutoFit.rect.size.y * 0.5 - (double) y * 0.5);
      float num2 = this.m_ScrollMode != TowerScrollListController.ScrollMode.Normal ? 1f : -1f;
      float num3 = num1;
      float num4 = num1 + y - this.m_RectTransform.sizeDelta.y;
      float a = num3 * num2;
      float b = num4 * num2;
      float min = Mathf.Min(a, b);
      float max = Mathf.Max(a, b);
      pos = Mathf.Clamp(pos, min, max);
      this.m_ScrollAutoFit.SetScrollTo(pos);
    }

    public void SetScrollTo(float pos)
    {
      if (this.m_Direction != TowerScrollListController.Direction.Vertical)
        return;
      if (this.m_ScrollMode == TowerScrollListController.ScrollMode.Normal)
        this._SetScrollTo(pos);
      else
        this._SetScrollTo(-pos);
    }

    public void PageUp(int value)
    {
      int num = Mathf.RoundToInt(this.GetRectTransForm.anchoredPosition.y / this.ItemScale);
      this.mChallengeButton.interactable = false;
      this._SetScrollTo((float) ((double) num * (double) this.ItemScale - (double) value * (double) this.ItemScale));
    }

    public void PageDown(int value)
    {
      int num = Mathf.RoundToInt(this.GetRectTransForm.anchoredPosition.y / this.ItemScale);
      this.mChallengeButton.interactable = false;
      this._SetScrollTo((float) ((double) num * (double) this.ItemScale + (double) value * (double) this.ItemScale));
    }

    private Vector2 ScrollDir
    {
      get
      {
        if (this.m_ScrollAutoFit.vertical)
          return -Vector2.up;
        return Vector2.right;
      }
    }

    [DebuggerHidden]
    public IEnumerator LoadTowerBG(TowerQuestListItem[] tower_quest_list)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new TowerScrollListController.\u003CLoadTowerBG\u003Ec__Iterator129() { tower_quest_list = tower_quest_list, \u003C\u0024\u003Etower_quest_list = tower_quest_list, \u003C\u003Ef__this = this };
    }

    public void SetTowerImage(LoadRequest floor_req, int index, string image_name, TowerQuestListItem[] tower_quest_list)
    {
      GachaTabSprites asset = floor_req.asset as GachaTabSprites;
      if ((UnityEngine.Object) asset == (UnityEngine.Object) null)
        return;
      for (int index1 = 0; index1 < asset.Sprites.Length; ++index1)
      {
        if (!(asset.Sprites[index1].name != image_name))
        {
          for (int index2 = 0; index2 < tower_quest_list.Length; ++index2)
          {
            tower_quest_list[index2].Banner[0].Images[index] = asset.Sprites[index1];
            tower_quest_list[index2].Banner[1].Images[index] = asset.Sprites[index1];
          }
        }
      }
    }

    [Serializable]
    public class OnItemPositionChange : UnityEvent<int, GameObject>
    {
    }

    public enum Direction
    {
      Vertical,
      Horizontal,
    }

    public enum ScrollMode
    {
      Normal,
      Reverse,
    }

    [SerializeField]
    public class ListItemFocusEvent : UnityEvent<GameObject>
    {
    }
  }
}
