// Decompiled with JetBrains decompiler
// Type: SRPG.JobIconScrollListController
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
  public class JobIconScrollListController : MonoBehaviour
  {
    private float ITEM_DISTANCE = 10f;
    private float SINGLE_ICON_ZERO_MERGIN = 20f;
    private float SINGLE_ICON_ONE_MERGIN = 22f;
    private float SINGLE_ICON_TWO_MERGIN = 45f;
    private float SINGLE_ICON_THREE_MERGIN = 80f;
    [SerializeField]
    [Range(0.0f, 30f)]
    protected int m_ItemCnt = 8;
    public JobIconScrollListController.OnItemPositionChange OnItemUpdate = new JobIconScrollListController.OnItemPositionChange();
    public JobIconScrollListController.OnAfterStartUpEvent OnAfterStartup = new JobIconScrollListController.OnAfterStartUpEvent();
    public JobIconScrollListController.OnUpdateEvent OnUpdateItemEvent = new JobIconScrollListController.OnUpdateEvent();
    public JobIconScrollListController.OnItemPositionAreaOverEvent OnItemPositionAreaOver = new JobIconScrollListController.OnItemPositionAreaOverEvent();
    [SerializeField]
    private GameObject mTemplateItem;
    public JobIconScrollListController.Direction m_Direction;
    public JobIconScrollListController.Mode m_ScrollMode;
    private RectTransform m_RectTransform;
    private List<JobIconScrollListController.ItemData> mItems;
    private Rect mViewArea;
    private float mPreAnchoredPositionX;
    private bool IsInitialized;
    [SerializeField]
    private RectTransform mViewPort;

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
        if (this.m_ScrollMode == JobIconScrollListController.Mode.Normal)
        {
          if (this.m_Direction == JobIconScrollListController.Direction.Vertical)
            return -this.GetRectTransForm.anchoredPosition.y;
          return this.GetRectTransForm.anchoredPosition.x;
        }
        if (this.m_Direction == JobIconScrollListController.Direction.Vertical)
          return this.GetRectTransForm.anchoredPosition.y;
        return this.GetRectTransForm.anchoredPosition.x;
      }
    }

    public float ScrollDir
    {
      get
      {
        return this.m_ScrollMode == JobIconScrollListController.Mode.Normal ? -1f : 1f;
      }
    }

    public List<JobIconScrollListController.ItemData> Items
    {
      get
      {
        return this.mItems;
      }
    }

    private void Start()
    {
      this.GetComponentInParent<ScrollRect>().content = this.GetRectTransForm;
    }

    public void Init()
    {
      if (this.OnAfterStartup == null)
        return;
      this.OnAfterStartup.Invoke(true);
    }

    public void CreateInstance()
    {
      this.mItems = new List<JobIconScrollListController.ItemData>();
      this.mTemplateItem.SetActive(false);
      for (int index = 0; index < this.m_ItemCnt; ++index)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.mTemplateItem);
        gameObject.transform.SetParent(this.transform, false);
        gameObject.SetActive(true);
        this.mItems.Add(new JobIconScrollListController.ItemData(gameObject));
      }
      foreach (ScrollListSetUp scrollListSetUp in ((IEnumerable<MonoBehaviour>) this.GetComponents<MonoBehaviour>()).Where<MonoBehaviour>((Func<MonoBehaviour, bool>) (item => item is ScrollListSetUp)).Select<MonoBehaviour, ScrollListSetUp>((Func<MonoBehaviour, ScrollListSetUp>) (item => item as ScrollListSetUp)).ToList<ScrollListSetUp>())
      {
        scrollListSetUp.OnSetUpItems();
        for (int idx = 0; idx < this.m_ItemCnt; ++idx)
          scrollListSetUp.OnUpdateItems(idx, this.mItems[idx].gameObject);
      }
    }

    public void Repotision()
    {
      this.GetRectTransForm.anchoredPosition = Vector2.zero;
      this.mPreAnchoredPositionX = this.AnchoredPosition;
      float num1 = 0.0f;
      float num2 = 0.0f;
      float num3 = 0.0f;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        UnitInventoryJobIcon jobIcon = this.mItems[index].job_icon;
        float num4;
        if (index <= 0)
        {
          num4 = num3 + jobIcon.HalfWidth;
          num1 = jobIcon.HalfWidth;
        }
        else
        {
          num4 = num3 + (jobIcon.HalfWidth + this.ITEM_DISTANCE);
          num2 = jobIcon.HalfWidth;
        }
        this.mItems[index].rectTransform.anchoredPosition = new Vector2(num4 * this.ScrollDir, 0.0f);
        this.mItems[index].position = this.mItems[index].rectTransform.anchoredPosition;
        num3 = num4 + jobIcon.HalfWidth;
      }
      this.mViewArea = new Rect(this.mItems[0].rectTransform.anchoredPosition.x - num1, 0.0f, this.mItems[this.mItems.Count - 1].rectTransform.anchoredPosition.x + num2, 0.0f);
      int num5 = 0;
      List<string> stringList = new List<string>();
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (!stringList.Contains(this.mItems[index].job_icon.BaseJobIconButton.name))
        {
          stringList.Add(this.mItems[index].job_icon.BaseJobIconButton.name);
          if (this.mItems[index].job_icon.IsSingleIcon)
            ++num5;
        }
      }
      float x = this.SINGLE_ICON_ZERO_MERGIN;
      if (num5 == 1)
        x = this.SINGLE_ICON_ONE_MERGIN;
      if (num5 == 2)
        x = this.SINGLE_ICON_TWO_MERGIN;
      if (num5 >= 3)
        x = this.SINGLE_ICON_THREE_MERGIN;
      this.mViewPort.offsetMin = new Vector2(x, 0.0f);
      this.mViewPort.offsetMax = new Vector2(-x, 0.0f);
      this.IsInitialized = true;
    }

    private bool CheckRightAreaOut(JobIconScrollListController.ItemData item)
    {
      return (double) this.mViewArea.width < (double) (this.transform as RectTransform).anchoredPosition.x + (double) item.position.x + (double) item.job_icon.HalfWidth;
    }

    private bool CheckLeftAreaOut(JobIconScrollListController.ItemData item)
    {
      return (double) this.mViewArea.x > (double) (this.transform as RectTransform).anchoredPosition.x + (double) item.position.x + (double) item.job_icon.HalfWidth;
    }

    private void Update()
    {
      if (!this.IsInitialized)
        return;
      switch (this.m_ScrollMode)
      {
        case JobIconScrollListController.Mode.Normal:
          this.UpdateModeNormal();
          break;
        case JobIconScrollListController.Mode.Reverse:
          this.UpdateModeReverse();
          break;
      }
    }

    private void UpdateModeNormal()
    {
      if ((double) this.mPreAnchoredPositionX != (double) this.AnchoredPosition)
      {
        this.UpdateItemsPositionNormal((double) this.AnchoredPosition - (double) this.mPreAnchoredPositionX > 0.0, (double) this.AnchoredPosition - (double) this.mPreAnchoredPositionX < 0.0);
        this.mPreAnchoredPositionX = this.AnchoredPosition;
      }
      if (this.OnUpdateItemEvent == null)
        return;
      this.OnUpdateItemEvent.Invoke(this.mItems);
    }

    private void UpdateModeReverse()
    {
      if ((double) this.mPreAnchoredPositionX != (double) this.AnchoredPosition)
      {
        bool is_move_right = (double) this.AnchoredPosition - (double) this.mPreAnchoredPositionX > 0.0;
        bool is_move_left = (double) this.AnchoredPosition - (double) this.mPreAnchoredPositionX < 0.0;
        this.mPreAnchoredPositionX = this.AnchoredPosition;
        this.UpdateItemsPositionReverse(is_move_right, is_move_left);
      }
      if (this.OnUpdateItemEvent == null)
        return;
      this.OnUpdateItemEvent.Invoke(this.mItems);
    }

    private void UpdateItemsPositionReverse(bool is_move_right, bool is_move_left)
    {
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        UnitInventoryJobIcon jobIcon1 = this.mItems[index].job_icon;
        UnitInventoryJobIcon jobIcon2 = this.mItems[0].job_icon;
        UnitInventoryJobIcon jobIcon3 = this.mItems[this.mItems.Count - 1].job_icon;
        if (is_move_right && this.CheckRightAreaOut(this.mItems[index]))
        {
          this.OnItemUpdate.Invoke(int.Parse(this.mItems[0].gameObject.name) - 1, this.mItems[index].gameObject);
          float x = this.mItems[0].position.x - jobIcon2.HalfWidth - this.ITEM_DISTANCE - jobIcon1.HalfWidth;
          this.mItems[index].position = new Vector2(x, this.mItems[index].position.y);
          JobIconScrollListController.ItemData mItem = this.mItems[index];
          this.mItems.RemoveAt(index);
          this.mItems.Insert(0, mItem);
          index = -1;
        }
        else if (is_move_left && this.CheckLeftAreaOut(this.mItems[index]))
        {
          this.OnItemUpdate.Invoke(int.Parse(this.mItems[this.mItems.Count - 1].gameObject.name) + 1, this.mItems[index].gameObject);
          float x = this.mItems[this.mItems.Count - 1].position.x + jobIcon3.HalfWidth + this.ITEM_DISTANCE + jobIcon1.HalfWidth;
          this.mItems[index].position = new Vector2(x, this.mItems[index].position.y);
          JobIconScrollListController.ItemData mItem = this.mItems[index];
          this.mItems.RemoveAt(index);
          this.mItems.Add(mItem);
          index = -1;
        }
      }
    }

    private void UpdateItemsPositionNormal(bool is_move_right, bool is_move_left)
    {
      RectTransform transform = this.transform as RectTransform;
      if (is_move_right)
      {
        for (int index = this.mItems.Count - 1; index >= 0; --index)
        {
          if (this.mItems[index].gameObject.activeSelf)
          {
            float num1 = (float) ((double) this.mItems[index].rectTransform.sizeDelta.x * (double) this.mItems[index].rectTransform.localScale.x * 0.5);
            if ((double) this.mViewPort.rect.width < (double) transform.anchoredPosition.x + (double) this.mItems[index].gameObject.transform.localPosition.x + (double) num1)
            {
              float num2 = this.mViewPort.rect.width - (transform.anchoredPosition.x + this.mItems[index].gameObject.transform.localPosition.x + num1);
              float x = transform.anchoredPosition.x + num2;
              transform.anchoredPosition = new Vector2(x, transform.anchoredPosition.y);
              if (this.OnItemPositionAreaOver != null)
              {
                this.OnItemPositionAreaOver.Invoke(this.mItems[index].gameObject);
                break;
              }
              break;
            }
          }
        }
      }
      if (!is_move_left)
        return;
      for (int index = 0; index < this.mItems.Count; ++index)
      {
        if (this.mItems[index].gameObject.activeSelf)
        {
          float num1 = (float) ((double) this.mItems[index].rectTransform.sizeDelta.x * (double) this.mItems[index].rectTransform.localScale.x * 0.5);
          if (0.0 > (double) transform.anchoredPosition.x + (double) this.mItems[index].gameObject.transform.localPosition.x - (double) num1)
          {
            float num2 = (float) (0.0 - ((double) transform.anchoredPosition.x + (double) this.mItems[index].gameObject.transform.localPosition.x - (double) num1));
            float x = transform.anchoredPosition.x + num2;
            transform.anchoredPosition = new Vector2(x, transform.anchoredPosition.y);
            if (this.OnItemPositionAreaOver == null)
              break;
            this.OnItemPositionAreaOver.Invoke(this.mItems[index].gameObject);
            break;
          }
        }
      }
    }

    public void Step()
    {
      this.Update();
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
    public class OnUpdateEvent : UnityEvent<List<JobIconScrollListController.ItemData>>
    {
    }

    [Serializable]
    public class OnItemPositionAreaOverEvent : UnityEvent<GameObject>
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

    public class ItemData
    {
      public GameObject gameObject;
      public RectTransform rectTransform;
      public Vector2 position;
      public UnitInventoryJobIcon job_icon;

      public ItemData(GameObject obj)
      {
        this.gameObject = obj;
        this.rectTransform = obj.transform as RectTransform;
        this.position = this.rectTransform.anchoredPosition;
        this.job_icon = obj.GetComponent<UnitInventoryJobIcon>();
      }
    }
  }
}
