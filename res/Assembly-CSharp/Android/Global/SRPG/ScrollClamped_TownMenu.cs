// Decompiled with JetBrains decompiler
// Type: SRPG.ScrollClamped_TownMenu
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [RequireComponent(typeof (ScrollListController))]
  public class ScrollClamped_TownMenu : MonoBehaviour, ScrollListSetUp
  {
    private readonly float OFFSET_X_MIN = 30f;
    private readonly float OFFSET_X = 60f;
    private readonly float OFFSET_Y = 15f;
    public float Space = 1f;
    public int Max;
    public RectTransform ViewObj;
    public ScrollAutoFit AutoFit;
    public GameObject Mask;
    public Button back;
    private ScrollListController mController;
    private float mOffset;
    private float mStartPos;
    private float mCenter;
    private ScrollClamped_TownMenu.MENU_ID mSelectIdx;
    private bool mIsSelected;

    public void Start()
    {
      this.mSelectIdx = ScrollClamped_TownMenu.MENU_ID.None;
      this.mIsSelected = false;
    }

    private void Update()
    {
      if (this.mSelectIdx == ScrollClamped_TownMenu.MENU_ID.None || this.mIsSelected || (!((UnityEngine.Object) this.AutoFit != (UnityEngine.Object) null) || this.AutoFit.IsMove))
        return;
      string EventName = string.Empty;
      switch (this.mSelectIdx)
      {
        case ScrollClamped_TownMenu.MENU_ID.Story:
          EventName = "CLICK_STORY";
          break;
        case ScrollClamped_TownMenu.MENU_ID.Event:
          EventName = "CLICK_EVENT";
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.EventQuest;
          break;
        case ScrollClamped_TownMenu.MENU_ID.Tower:
          EventName = "CLICK_TOWER";
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.Tower;
          break;
        case ScrollClamped_TownMenu.MENU_ID.Chara:
          EventName = "CLICK_CHARA";
          break;
        case ScrollClamped_TownMenu.MENU_ID.Multi:
          EventName = "CLICK_MULTI";
          break;
        case ScrollClamped_TownMenu.MENU_ID.Key:
          EventName = "CLICK_KEY";
          GlobalVars.ReqEventPageListType = GlobalVars.EventQuestListType.KeyQuest;
          break;
      }
      if (string.IsNullOrEmpty(EventName))
        return;
      FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, EventName);
      this.mIsSelected = true;
    }

    public void OnSetUpItems()
    {
      this.mController = this.GetComponent<ScrollListController>();
      this.mController.OnItemUpdate.AddListener(new UnityAction<int, GameObject>(this.OnUpdateItems));
      this.mController.OnUpdateItemEvent.AddListener(new UnityAction<List<RectTransform>>(this.OnUpdateScale));
      float num1 = 0.0f;
      if ((UnityEngine.Object) this.ViewObj != (UnityEngine.Object) null)
        num1 = this.ViewObj.rect.width * 0.5f;
      RectTransform component = this.GetComponent<RectTransform>();
      Vector2 sizeDelta = component.sizeDelta;
      Vector2 anchoredPosition = component.anchoredPosition;
      float num2 = this.mController.ItemScale * this.Space;
      anchoredPosition.x = num1 - this.mController.ItemScale * 0.5f;
      sizeDelta.x = num2 * (float) this.Max;
      component.sizeDelta = sizeDelta;
      component.anchoredPosition = anchoredPosition;
      this.mStartPos = anchoredPosition.x;
      this.mOffset = this.mController.ItemScale * 0.5f;
      if (!((UnityEngine.Object) this.AutoFit != (UnityEngine.Object) null))
        return;
      this.AutoFit.ItemScale = num2;
      this.AutoFit.Offset = this.mStartPos;
    }

    public void OnUpdateItems(int idx, GameObject obj)
    {
      if (this.Max == 0 || (UnityEngine.Object) this.mController == (UnityEngine.Object) null)
        obj.SetActive(false);
      int num = idx % this.Max;
      if (num < 0)
        num = Mathf.Abs(this.Max + num) % this.Max;
      ImageArray component = obj.GetComponent<ImageArray>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.ImageIndex = num;
    }

    public void OnUpdateScale(List<RectTransform> rects)
    {
      if ((UnityEngine.Object) this.mController == (UnityEngine.Object) null)
        return;
      RectTransform component = this.GetComponent<RectTransform>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        this.mCenter = this.mStartPos - component.anchoredPosition.x + this.mOffset;
      List<Vector2> itemPosList = this.mController.ItemPosList;
      List<Vector2> vector2List = new List<Vector2>();
      for (int index = 0; index < rects.Count; ++index)
      {
        RectTransform rect = rects[index];
        if ((UnityEngine.Object) rect != (UnityEngine.Object) null)
        {
          vector2List.Add(rect.anchoredPosition);
          float num1 = Mathf.Clamp((float) (1.0 - (double) Mathf.Abs(itemPosList[index].x - this.mCenter) / (double) (this.mController.ItemScale * 2f * this.Space)), 0.0f, 1f);
          float num2 = (float) (0.699999988079071 + 0.300000011920929 * (double) num1);
          rect.transform.localScale = new Vector3(num2, num2, num2);
          Vector2 vector2 = itemPosList[index];
          float num3 = this.OFFSET_X - this.OFFSET_X * num1;
          float num4 = this.OFFSET_Y - this.OFFSET_Y * num1;
          float num5 = (double) num1 < 0.5 ? this.OFFSET_X_MIN + (this.OFFSET_X - this.OFFSET_X * Mathf.Clamp(num1 * 2f, 0.0f, 1f)) : this.OFFSET_X_MIN * Mathf.Clamp((float) ((1.0 - (double) num1) * 2.0), 0.0f, 1f);
          double mCenter = (double) this.mCenter;
          double x = (double) itemPosList[index].x;
          rect.anchoredPosition = mCenter >= x ? new Vector2(vector2.x + num5, vector2.y + num4) : new Vector2(vector2.x - num5, vector2.y + num4);
        }
      }
    }

    public void OnNext()
    {
      if (!((UnityEngine.Object) this.GetComponent<RectTransform>() != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mController != (UnityEngine.Object) null) || !((UnityEngine.Object) this.AutoFit != (UnityEngine.Object) null))
        return;
      this.AutoFit.SetScrollToHorizontal((float) (this.AutoFit.GetCurrent() - 1) * this.AutoFit.ItemScale + this.AutoFit.Offset);
    }

    public void OnPrev()
    {
      if (!((UnityEngine.Object) this.GetComponent<RectTransform>() != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mController != (UnityEngine.Object) null) || !((UnityEngine.Object) this.AutoFit != (UnityEngine.Object) null))
        return;
      this.AutoFit.SetScrollToHorizontal((float) (this.AutoFit.GetCurrent() + 1) * this.AutoFit.ItemScale + this.AutoFit.Offset);
    }

    public void OnClick(GameObject obj)
    {
      if (!((UnityEngine.Object) this.AutoFit != (UnityEngine.Object) null) || this.mSelectIdx != ScrollClamped_TownMenu.MENU_ID.None)
        return;
      if ((double) this.AutoFit.velocity.x > (double) this.AutoFit.ItemScale)
        return;
      List<RectTransform> itemList = this.mController.ItemList;
      List<Vector2> itemPosList = this.mController.ItemPosList;
      RectTransform component1 = this.gameObject.GetComponent<RectTransform>();
      RectTransform rect = obj.GetComponent<RectTransform>();
      if (!((UnityEngine.Object) this.AutoFit != (UnityEngine.Object) null))
        return;
      int index = itemList.FindIndex((Predicate<RectTransform>) (data => (UnityEngine.Object) data == (UnityEngine.Object) rect));
      if (index == -1)
        return;
      this.AutoFit.SetScrollToHorizontal(component1.anchoredPosition.x - (itemPosList[index].x - this.mCenter));
      ImageArray component2 = obj.GetComponent<ImageArray>();
      if ((UnityEngine.Object) component2 != (UnityEngine.Object) null)
        this.mSelectIdx = (ScrollClamped_TownMenu.MENU_ID) component2.ImageIndex;
      if ((UnityEngine.Object) this.Mask != (UnityEngine.Object) null)
        this.Mask.SetActive(true);
      if (!((UnityEngine.Object) this.back != (UnityEngine.Object) null))
        return;
      this.back.interactable = false;
    }

    private enum MENU_ID
    {
      None = -1,
      Story = 0,
      Event = 1,
      Tower = 2,
      Chara = 3,
      Multi = 4,
      Key = 5,
    }
  }
}
