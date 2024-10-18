// Decompiled with JetBrains decompiler
// Type: SRPG.StoryPartList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(1, "アイコン選択後の挙動", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "解放されているアイコンが選択された", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "ロックされているアイコンが選択された", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "ストーリーパート解放演出再生", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "ストーリーパートのアイコンを押した", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(104, "ストーリーパートのアイコン選択後の移動が終わった", FlowNode.PinTypes.Output, 104)]
  public class StoryPartList : ScrollContentsInfo, IFlowInterface
  {
    private List<StoryPartIcon> mStoryPartIconList = new List<StoryPartIcon>();
    private List<Toggle> mPageIconList = new List<Toggle>();
    private const int PIN_SELECT_ICON_ACTION = 1;
    private const int PIN_SELECT_RELEASE_ICON = 100;
    private const int PIN_SELECT_LOCK_ICON = 101;
    private const int PIN_PLAY_RELEASE_ANIMATION = 102;
    private const int PIN_PUTON_ICON = 103;
    private const int PIN_MOVE_END = 104;
    public string WorldMapControllerID;
    [SerializeField]
    private GameObject TemplateGo;
    [SerializeField]
    private GameObject ScrollArea;
    [SerializeField]
    private GameObject PageNext;
    [SerializeField]
    private GameObject PagePrev;
    [SerializeField]
    private GameObject TogglePagesGroup;
    [SerializeField]
    private GameObject TemplatePageIcon;
    private bool SetRangeFlag;
    private QuestSectionList mQuestSectionList;
    private RectTransform mMoveRect;
    private bool AnimationReleaseFlag;
    private StoryPartIcon ReleaseStoryPartIcon;
    private bool mReleaseAction;
    private int mSelectIconNum;
    private Button mNextButton;
    private Button mPrevButton;
    private StoryPartIcon mSelectIcon;
    private bool mCheckSelectIconMoveFlag;
    private StoryPartIcon mSelectBeforeIcon;

    private void Start()
    {
      this.mMoveRect = (RectTransform) null;
      this.mQuestSectionList = (QuestSectionList) null;
      this.ReleaseStoryPartIcon = (StoryPartIcon) null;
      this.AnimationReleaseFlag = false;
      this.mSelectIconNum = 1;
      this.mSelectIcon = (StoryPartIcon) null;
      this.mCheckSelectIconMoveFlag = false;
      this.mSelectBeforeIcon = (StoryPartIcon) null;
      this.mNextButton = (Button) null;
      if ((UnityEngine.Object) this.PageNext != (UnityEngine.Object) null)
        this.mNextButton = this.PageNext.GetComponent<Button>();
      this.mPrevButton = (Button) null;
      if ((UnityEngine.Object) this.PagePrev != (UnityEngine.Object) null)
        this.mPrevButton = this.PagePrev.GetComponent<Button>();
      SerializeValueList currentValue = FlowNode_ButtonEvent.currentValue as SerializeValueList;
      if (currentValue != null)
        this.mQuestSectionList = currentValue.GetComponent<QuestSectionList>("_self");
      this.mReleaseAction = MonoSingleton<GameManager>.Instance.CheckReleaseStoryPart();
      this.SetRangeFlag = false;
      this.TemplateGo.SetActive(false);
      this.TemplatePageIcon.SetActive(false);
      int storyPartNum = MonoSingleton<GameManager>.Instance.GetStoryPartNum();
      int partNumPresentTime = MonoSingleton<GameManager>.Instance.GetStoryPartNumPresentTime();
      Vector2 zero = Vector2.zero;
      for (int index = 0; index < storyPartNum; ++index)
      {
        GameObject gameObject1 = UnityEngine.Object.Instantiate<GameObject>(this.TemplateGo);
        Vector2 localScale1 = (Vector2) gameObject1.transform.localScale;
        gameObject1.transform.SetParent(this.transform);
        gameObject1.transform.localScale = (Vector3) localScale1;
        gameObject1.gameObject.SetActive(true);
        gameObject1.name = this.TemplateGo.name + (index + 1).ToString();
        StoryPartIcon component = gameObject1.GetComponent<StoryPartIcon>();
        if (this.mReleaseAction && index + 1 == partNumPresentTime)
        {
          component.Setup(true, index + 1);
          this.ReleaseStoryPartIcon = component;
        }
        else if (!component.Setup(index + 1 > partNumPresentTime, index + 1))
        {
          UnityEngine.Object.Destroy((UnityEngine.Object) gameObject1);
          continue;
        }
        this.mStoryPartIconList.Add(component);
        Toggle toggle = (Toggle) null;
        if (storyPartNum > 1)
        {
          GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.TemplatePageIcon);
          Vector2 localScale2 = (Vector2) gameObject2.transform.localScale;
          gameObject2.transform.SetParent(this.TogglePagesGroup.transform);
          gameObject2.transform.localScale = (Vector3) localScale2;
          gameObject2.gameObject.SetActive(true);
          gameObject2.name = this.TemplatePageIcon.name + (index + 1).ToString();
          toggle = gameObject2.GetComponent<Toggle>();
          this.mPageIconList.Add(toggle);
        }
        if (this.mReleaseAction)
        {
          if (index + 1 == partNumPresentTime)
          {
            this.mMoveRect = gameObject1.GetComponent<RectTransform>();
            this.mSelectIconNum = partNumPresentTime;
            if ((UnityEngine.Object) toggle != (UnityEngine.Object) null)
              toggle.isOn = true;
          }
        }
        else if (index + 1 == GlobalVars.SelectedStoryPart.Get())
        {
          this.mMoveRect = gameObject1.GetComponent<RectTransform>();
          this.mSelectIconNum = partNumPresentTime;
          if ((UnityEngine.Object) toggle != (UnityEngine.Object) null)
            toggle.isOn = true;
        }
      }
      if (storyPartNum != 1)
        return;
      if ((UnityEngine.Object) this.PageNext != (UnityEngine.Object) null)
        this.PageNext.SetActive(false);
      if (!((UnityEngine.Object) this.PagePrev != (UnityEngine.Object) null))
        return;
      this.PagePrev.SetActive(false);
    }

    private void Update()
    {
      if ((UnityEngine.Object) this.mMoveRect != (UnityEngine.Object) null)
      {
        RectTransform component = this.GetComponent<RectTransform>();
        Vector2 anchoredPosition = component.anchoredPosition;
        anchoredPosition.x = -this.mMoveRect.anchoredPosition.x;
        component.anchoredPosition = anchoredPosition;
        this.mMoveRect = (RectTransform) null;
        if ((UnityEngine.Object) this.mNextButton != (UnityEngine.Object) null && (UnityEngine.Object) this.mPrevButton != (UnityEngine.Object) null && MonoSingleton<GameManager>.Instance.GetStoryPartNum() == 1)
        {
          this.mNextButton.interactable = false;
          this.mPrevButton.interactable = false;
          if (this.mStoryPartIconList.Count == 1)
            this.mStoryPartIconList[0].SetMask(false);
        }
      }
      if (this.mReleaseAction)
      {
        if ((UnityEngine.Object) this.ReleaseStoryPartIcon != (UnityEngine.Object) null)
          this.ReleaseStoryPartIcon.PlayReleaseAnim();
        this.mReleaseAction = false;
        this.AnimationReleaseFlag = true;
      }
      if (this.AnimationReleaseFlag && !this.ReleaseStoryPartIcon.IsPlayingReleaseAnim())
      {
        this.ReleaseStoryPartIcon.ReleaseIcon();
        this.SaveReleaseActionKey(this.ReleaseStoryPartIcon.StoryPartNum);
        this.ReleaseStoryPartIcon = (StoryPartIcon) null;
        this.AnimationReleaseFlag = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      }
      if (this.mCheckSelectIconMoveFlag && !this.ScrollArea.GetComponent<ScrollAutoFit>().IsMove)
      {
        this.mCheckSelectIconMoveFlag = false;
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 104);
      }
      this.UpdateButtonInteractable();
    }

    private void UpdateButtonInteractable()
    {
      if (MonoSingleton<GameManager>.Instance.GetStoryPartNum() == 1)
        return;
      double nearIconPos = (double) this.GetNearIconPos(this.GetComponent<RectTransform>().localPosition.x);
    }

    public override Vector2 SetRangePos(Vector2 position)
    {
      Vector2 vector2 = position;
      if (!this.SetRangeFlag)
      {
        int num = 0;
        IEnumerator enumerator = this.transform.GetEnumerator();
        try
        {
          while (enumerator.MoveNext())
          {
            Transform current = (Transform) enumerator.Current;
            if (current.gameObject.activeSelf)
            {
              RectTransform component = current.GetComponent<RectTransform>();
              if (!((UnityEngine.Object) component == (UnityEngine.Object) null))
              {
                if (num == 0 || (double) this.mStartPosX > (double) component.anchoredPosition.x)
                  this.mStartPosX = component.anchoredPosition.x;
                if (num == 0 || (double) this.mEndPosX < (double) component.anchoredPosition.x)
                  this.mEndPosX = component.anchoredPosition.x;
                ++num;
              }
            }
          }
        }
        finally
        {
          IDisposable disposable;
          if ((disposable = enumerator as IDisposable) != null)
            disposable.Dispose();
        }
        this.mStartPosX = -this.mStartPosX;
        this.mEndPosX = -this.mEndPosX;
        this.SetRangeFlag = true;
      }
      if ((double) vector2.x > (double) this.mStartPosX)
        vector2.x = this.mStartPosX;
      else if ((double) vector2.x < (double) this.mEndPosX)
        vector2.x = this.mEndPosX;
      return vector2;
    }

    public override bool CheckRangePos(float pos)
    {
      bool flag = false;
      if ((double) pos > (double) this.mStartPosX)
        flag = true;
      else if ((double) pos < (double) this.mEndPosX)
        flag = true;
      return flag;
    }

    public override float GetNearIconPos(float pos)
    {
      float num1 = pos;
      float num2 = 0.0f;
      int num3 = 0;
      StoryPartIcon icon = (StoryPartIcon) null;
      IEnumerator enumerator = this.transform.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Transform current = (Transform) enumerator.Current;
          if (current.gameObject.activeSelf)
          {
            RectTransform component = current.GetComponent<RectTransform>();
            if (!((UnityEngine.Object) component == (UnityEngine.Object) null) && (num3 == 0 || (double) num2 > (double) Mathf.Abs(pos - -component.anchoredPosition.x)))
            {
              num2 = Mathf.Abs(pos - -component.anchoredPosition.x);
              num1 = -component.anchoredPosition.x;
              ++num3;
              icon = current.GetComponent<StoryPartIcon>();
            }
          }
        }
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
      this.SetButtonInteractable(icon);
      return num1;
    }

    public void OnPrev()
    {
      if ((UnityEngine.Object) this.ScrollArea == (UnityEngine.Object) null)
        return;
      ScrollAutoFit component1 = this.ScrollArea.GetComponent<ScrollAutoFit>();
      if ((UnityEngine.Object) component1 == (UnityEngine.Object) null)
        return;
      float x = this.transform.GetComponent<RectTransform>().anchoredPosition.x;
      float num1 = 0.0f;
      int num2 = 0;
      float pos = x;
      IEnumerator enumerator = this.transform.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Transform current = (Transform) enumerator.Current;
          if (current.gameObject.activeSelf)
          {
            RectTransform component2 = current.GetComponent<RectTransform>();
            if (!((UnityEngine.Object) component2 == (UnityEngine.Object) null) && (double) x < -(double) component2.anchoredPosition.x && (num2 == 0 || (double) num1 > (double) Mathf.Abs(x - -component2.anchoredPosition.x)))
            {
              num1 = Mathf.Abs(x - -component2.anchoredPosition.x);
              pos = -component2.anchoredPosition.x;
              ++num2;
            }
          }
        }
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
      component1.SetScrollToHorizontal(pos);
      if (!((UnityEngine.Object) this.mNextButton != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mPrevButton != (UnityEngine.Object) null))
        return;
      --this.mSelectIconNum;
      if (this.mSelectIconNum == 1)
        this.mPrevButton.interactable = false;
      this.mNextButton.interactable = true;
    }

    public void OnNext()
    {
      if ((UnityEngine.Object) this.ScrollArea == (UnityEngine.Object) null)
        return;
      ScrollAutoFit component1 = this.ScrollArea.GetComponent<ScrollAutoFit>();
      if ((UnityEngine.Object) component1 == (UnityEngine.Object) null)
        return;
      float x = this.transform.GetComponent<RectTransform>().anchoredPosition.x;
      float num1 = 0.0f;
      int num2 = 0;
      float pos = x;
      IEnumerator enumerator = this.transform.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Transform current = (Transform) enumerator.Current;
          if (current.gameObject.activeSelf)
          {
            RectTransform component2 = current.GetComponent<RectTransform>();
            if (!((UnityEngine.Object) component2 == (UnityEngine.Object) null) && (double) x > -(double) component2.anchoredPosition.x && (num2 == 0 || (double) num1 > (double) Mathf.Abs(x - -component2.anchoredPosition.x)))
            {
              num1 = Mathf.Abs(x - -component2.anchoredPosition.x);
              pos = -component2.anchoredPosition.x;
              ++num2;
            }
          }
        }
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
      component1.SetScrollToHorizontal(pos);
      if (!((UnityEngine.Object) this.mNextButton != (UnityEngine.Object) null) || !((UnityEngine.Object) this.mPrevButton != (UnityEngine.Object) null))
        return;
      ++this.mSelectIconNum;
      this.mNextButton.interactable = true;
      if (this.mSelectIconNum != MonoSingleton<GameManager>.Instance.GetStoryPartNum())
        return;
      this.mPrevButton.interactable = false;
    }

    public void OnIcon(GameObject go)
    {
      if ((UnityEngine.Object) go == (UnityEngine.Object) null)
        return;
      this.mSelectIcon = (StoryPartIcon) null;
      if ((UnityEngine.Object) this.mQuestSectionList == (UnityEngine.Object) null)
      {
        WorldMapController instance = WorldMapController.FindInstance(this.WorldMapControllerID);
        if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
          return;
        this.mQuestSectionList = instance.SectionList;
      }
      StoryPartIcon component1 = go.GetComponent<StoryPartIcon>();
      if ((UnityEngine.Object) component1 == (UnityEngine.Object) null || (UnityEngine.Object) this.ScrollArea == (UnityEngine.Object) null)
        return;
      ScrollAutoFit component2 = this.ScrollArea.GetComponent<ScrollAutoFit>();
      if ((UnityEngine.Object) component2 == (UnityEngine.Object) null)
        return;
      IEnumerator enumerator = this.transform.GetEnumerator();
      try
      {
        while (enumerator.MoveNext())
        {
          Transform current = (Transform) enumerator.Current;
          if (current.gameObject.activeSelf && (UnityEngine.Object) component1.gameObject == (UnityEngine.Object) current.gameObject)
          {
            RectTransform component3 = current.GetComponent<RectTransform>();
            if (!((UnityEngine.Object) component3 == (UnityEngine.Object) null))
            {
              component2.SetScrollToHorizontal(-component3.anchoredPosition.x);
              this.mSelectIcon = component1;
              this.mCheckSelectIconMoveFlag = true;
              FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
              break;
            }
          }
        }
      }
      finally
      {
        IDisposable disposable;
        if ((disposable = enumerator as IDisposable) != null)
          disposable.Dispose();
      }
    }

    private void SaveReleaseActionKey(int story_num)
    {
      PlayerPrefsUtility.SetString(PlayerPrefsUtility.RELEASE_STORY_PART_KEY + (object) story_num, "1", true);
    }

    private void SetButtonInteractable(StoryPartIcon icon)
    {
      if ((UnityEngine.Object) this.mSelectBeforeIcon == (UnityEngine.Object) icon)
        return;
      this.mSelectBeforeIcon = icon;
      if ((UnityEngine.Object) icon != (UnityEngine.Object) null && (UnityEngine.Object) this.mNextButton != (UnityEngine.Object) null && ((UnityEngine.Object) this.mPrevButton != (UnityEngine.Object) null && MonoSingleton<GameManager>.Instance.GetStoryPartNum() > 1))
      {
        this.mSelectIconNum = icon.StoryPartNum;
        if ((UnityEngine.Object) this.mNextButton != (UnityEngine.Object) null && this.mSelectIconNum == 1)
        {
          this.mNextButton.interactable = true;
          this.mPrevButton.interactable = false;
        }
        else if (this.mSelectIconNum == MonoSingleton<GameManager>.Instance.GetStoryPartNum())
        {
          this.mPrevButton.interactable = true;
          this.mNextButton.interactable = false;
        }
        else
        {
          this.mNextButton.interactable = true;
          this.mPrevButton.interactable = true;
        }
      }
      for (int index = 0; index < this.mStoryPartIconList.Count; ++index)
      {
        if ((UnityEngine.Object) icon == (UnityEngine.Object) this.mStoryPartIconList[index])
          this.mStoryPartIconList[index].SetMask(false);
        else
          this.mStoryPartIconList[index].SetMask(true);
      }
      for (int index = 0; index < this.mPageIconList.Count; ++index)
        this.mPageIconList[index].isOn = index + 1 == icon.StoryPartNum;
    }

    public void Activated(int pinID)
    {
      if (pinID != 1 || !((UnityEngine.Object) this.mSelectIcon != (UnityEngine.Object) null))
        return;
      if (!this.mSelectIcon.LockFlag)
      {
        GlobalVars.SelectedStoryPart.Set(this.mSelectIcon.StoryPartNum);
        this.mQuestSectionList.Refresh();
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      }
      else
      {
        string partMessageSysId = MonoSingleton<GameManager>.Instance.GetReleaseStoryPartMessageSysID(this.mSelectIcon.StoryPartNum);
        if (string.IsNullOrEmpty(partMessageSysId))
          return;
        UIUtility.SystemMessage((string) null, LocalizedText.Get("sys." + partMessageSysId), (UIUtility.DialogResultEvent) null, (GameObject) null, false, -1);
      }
    }
  }
}
