// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollItemCentering
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class ScrollItemCentering : SRPG_ScrollRect
  {
    public ContentNode BaseNode;
    public GameObject PageIconParent;
    public GameObject BasePageIcon;
    public GridLayoutGroup NodeLayoutGroup;
    public int StartCount;
    private ContentSource mContentSource;
    private int mMaxListCount;
    private int mCurrentPage;
    private float mCellWidth;
    private List<Toggle> mDotList = new List<Toggle>();
    private bool mDragging;
    private IEnumerator mMove;
    private float centering_offset;
    private int CellMarginLeftNum = 1;
    private int CellMarginRightNum = 2;
    private List<UnityAction> mPageActionList = new List<UnityAction>();

    protected virtual void Start()
    {
      ((UIBehaviour) this).Start();
      this.inertia = false;
      this.movementType = (ScrollRect.MovementType) 0;
      this.horizontal = true;
      this.vertical = false;
      // ISSUE: method pointer
      ((UnityEvent<Vector2>) this.onValueChanged).AddListener(new UnityAction<Vector2>((object) this, __methodptr(OnValueChanged)));
    }

    public override void OnBeginDrag(PointerEventData eventData)
    {
      base.OnBeginDrag(eventData);
      this.mDragging = true;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
      this.mDragging = false;
      base.OnEndDrag(eventData);
    }

    protected override void LateUpdate()
    {
      if (this.mMove != null)
      {
        if (!this.mMove.MoveNext())
          this.mMove = (IEnumerator) null;
      }
      else if (!this.mDragging)
      {
        RectTransform[] rectTransformArray = new RectTransform[((Transform) this.content).childCount];
        for (int index = 0; index < ((Transform) this.content).childCount; ++index)
          rectTransformArray[index] = ((Transform) this.content).GetChild(index) as RectTransform;
        float centeringOffset = this.centering_offset;
        Vector2 anchoredPosition = this.content.anchoredPosition;
        for (int index = 0; index < rectTransformArray.Length; ++index)
        {
          if (index >= this.CellMarginLeftNum && index < this.mMaxListCount + this.CellMarginRightNum)
          {
            if ((double) centeringOffset + (double) this.mCellWidth / 2.0 > -(double) anchoredPosition.x)
            {
              anchoredPosition.x = Mathf.Lerp(anchoredPosition.x, -centeringOffset, 0.1f);
              this.SetContentAnchoredPosition(anchoredPosition);
              int num = (index - this.CellMarginLeftNum) % (this.mMaxListCount == 0 ? 0 : this.mMaxListCount);
              if (this.mCurrentPage != num && this.mPageActionList != null && this.mPageActionList.Count > num)
              {
                this.SetCurrentPage(num, this.mPageActionList[num]);
                break;
              }
              break;
            }
            centeringOffset += this.mCellWidth;
          }
        }
      }
      base.LateUpdate();
    }

    public void Initialize(ContentSource _contentsource, List<UnityAction> _action_list)
    {
      this.SetCurrentSource(_contentsource);
      this.mMaxListCount = this.mContentSource.GetCount();
      this.mCurrentPage = this.StartCount;
      if (!Object.op_Implicit((Object) this.PageIconParent) || !Object.op_Implicit((Object) this.BasePageIcon))
        return;
      this.mDotList.Clear();
      for (int index = 0; index < this.mMaxListCount; ++index)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.BasePageIcon);
        gameObject.transform.SetParent(this.PageIconParent.transform, false);
        gameObject.SetActive(true);
        this.mDotList.Add(gameObject.GetComponent<Toggle>());
      }
      this.BasePageIcon.SetActive(false);
      if (!Object.op_Implicit((Object) this.content) || !Object.op_Implicit((Object) this.BaseNode) || !Object.op_Implicit((Object) this.NodeLayoutGroup))
        return;
      this.mCellWidth = this.NodeLayoutGroup.cellSize.x + this.NodeLayoutGroup.spacing.x;
      this.BaseNode.SetActive(false);
      if ((double) this.mCellWidth == 0.0)
        return;
      this.StartNodeList();
      Rect rect = this.viewport.rect;
      float num = (float) ((double) ((Rect) ref rect).width / 2.0 - (double) this.mCellWidth / 2.0);
      this.centering_offset = this.mCellWidth - num % this.mCellWidth;
      this.CellMarginLeftNum = (int) ((double) num / (double) this.mCellWidth);
      this.CellMarginRightNum = (int) ((double) num / (double) this.mCellWidth) + 1;
      this.mPageActionList = _action_list;
      this.SetContentAnchoredPosition(new Vector2(-this.getPageOffset(this.mCurrentPage), 0.0f));
      this.ChangeDot(this.mCurrentPage);
    }

    private int GetListCount(int _now_count)
    {
      if (this.mMaxListCount == 0)
        return 0;
      int num = Mathf.Abs(_now_count);
      return _now_count < 0 ? this.mMaxListCount - num % this.mMaxListCount : num % this.mMaxListCount;
    }

    private void StartNodeList()
    {
      int startCount = this.StartCount;
      for (int _display_num = 0; _display_num < this.CellMarginLeftNum; ++_display_num)
        this.CommonNodeCreate(_display_num, this.GetListCount(-(_display_num + 1)));
      for (int index = 0; index < this.mMaxListCount; ++index)
      {
        this.CommonNodeCreate(index + this.CellMarginLeftNum, this.GetListCount(startCount));
        ++startCount;
      }
      for (int index = 0; index < this.CellMarginRightNum; ++index)
      {
        this.CommonNodeCreate(index + this.CellMarginLeftNum + this.mMaxListCount, this.GetListCount(startCount));
        ++startCount;
      }
    }

    private void CommonNodeCreate(int _display_num, int _list_num)
    {
      GameObject gameObject = Object.Instantiate<GameObject>(((Component) this.BaseNode).gameObject);
      if (!Object.op_Implicit((Object) gameObject))
        return;
      gameObject.transform.SetParent(((Component) this.content).gameObject.transform, false);
      gameObject.SetActive(true);
      ContentNode component = gameObject.GetComponent<ContentNode>();
      if (!Object.op_Implicit((Object) component))
        return;
      ContentSource.Param obj = this.GetParam(_list_num);
      component.Setup(_display_num, new Vector2((float) _display_num * this.mCellWidth, 0.0f), obj);
    }

    public void SetCurrentPage(int _page, UnityAction action = null)
    {
      if (this.mCurrentPage == _page)
        return;
      action?.Invoke();
      this.mCurrentPage = _page;
      this.ChangeDot();
    }

    public bool ChangeCenterPage(int _page)
    {
      if (this.mCurrentPage == _page || this.mMove != null)
        return false;
      this.mMove = this.movePage(_page);
      this.ChangeDot(_page);
      return true;
    }

    private void ChangeDot(int _num = -1)
    {
      for (int index = 0; index < this.mDotList.Count; ++index)
      {
        int num = _num < 0 ? this.mCurrentPage : _num;
        this.mDotList[index].isOn = index == num;
      }
    }

    private void SetCurrentSource(ContentSource _source)
    {
      if (this.mContentSource != null)
        this.mContentSource.Release();
      this.mContentSource = _source;
      this.mContentSource.Initialize((ContentController) null);
    }

    public ContentSource.Param GetParam(int _index)
    {
      return this.mContentSource != null ? this.mContentSource.GetParam(_index) : (ContentSource.Param) null;
    }

    private void OnValueChanged(Vector2 value)
    {
      Rect rect1 = this.content.rect;
      double width1 = (double) ((Rect) ref rect1).width;
      Rect rect2 = this.viewport.rect;
      double width2 = (double) ((Rect) ref rect2).width;
      float num1 = (float) (width1 - width2 - (double) this.centering_offset * 2.0);
      float num2 = -this.content.anchoredPosition.x;
      if ((double) num2 > (double) num1 + (double) this.centering_offset)
      {
        this.SetContentAnchoredPosition(new Vector2(-((double) num1 == 0.0 ? 0.0f : num2 % num1), 0.0f));
      }
      else
      {
        if ((double) num2 >= 0.0)
          return;
        float num3 = (double) num1 == 0.0 ? 0.0f : (float) -(-(double) num2 % (double) num1);
        this.SetContentAnchoredPosition(new Vector2((float) -((double) num1 + (double) num3), 0.0f));
      }
    }

    [DebuggerHidden]
    private IEnumerator movePage(int _next_page, bool _is_action = false)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new ScrollItemCentering.\u003CmovePage\u003Ec__Iterator0()
      {
        _next_page = _next_page,
        _is_action = _is_action,
        \u0024this = this
      };
    }

    private float getPageOffset(int _num)
    {
      RectTransform[] rectTransformArray = new RectTransform[((Transform) this.content).childCount];
      for (int index = 0; index < ((Transform) this.content).childCount; ++index)
        rectTransformArray[index] = ((Transform) this.content).GetChild(index) as RectTransform;
      float centeringOffset = this.centering_offset;
      for (int index = 0; index < rectTransformArray.Length; ++index)
      {
        if (index == _num)
          return centeringOffset;
        centeringOffset += this.mCellWidth;
      }
      return this.centering_offset;
    }

    private float decelerate(float value)
    {
      return (float) (1.0 - (1.0 - (double) value) * (1.0 - (double) value) * (1.0 - (double) value));
    }

    public void OnLeftButtonClick()
    {
      if (this.mMove != null)
        return;
      this.mMove = this.movePage(this.mCurrentPage - 1 < 0 ? this.mMaxListCount - 1 : this.mCurrentPage - 1, true);
    }

    public void OnRightButtonClick()
    {
      if (this.mMove != null)
        return;
      this.mMove = this.movePage(this.mCurrentPage + 1 >= this.mMaxListCount ? 0 : this.mCurrentPage + 1, true);
    }
  }
}
