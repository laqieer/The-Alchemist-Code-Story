// Decompiled with JetBrains decompiler
// Type: SRPG.PlayBackUnitVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class PlayBackUnitVoice : MonoBehaviour
  {
    private readonly float TOOLTIP_POSITION_OFFSET_Y = 20f;
    [SerializeField]
    private Button CloseButton;
    [SerializeField]
    private Button Bg;
    [SerializeField]
    private RectTransform ItemParent;
    [SerializeField]
    private GameObject ItemTemplate;
    [SerializeField]
    private Tooltip Preafab_UnlockConditionsTooltip;
    private List<GameObject> mItems = new List<GameObject>();
    private UnitData mCurrentUnit;
    private long mLastUnitUniqueID = -1;
    private PlayBackUnitVoiceItem mLastSelectItem;
    private bool mStartPlayVoice;
    private UnitData.UnitPlaybackVoiceData mUnitVoiceData;
    private Tooltip mUnlockConditionsTooltip;
    private SRPG_ScrollRect mScrollRect;
    public PlayBackUnitVoice.CloseEvent OnCloseEvent;

    private void Awake()
    {
      if (!Object.op_Inequality((Object) this.ItemTemplate, (Object) null))
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
      if (Object.op_Inequality((Object) this.CloseButton, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.CloseButton.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClose)));
      }
      if (Object.op_Inequality((Object) this.Bg, (Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.Bg.onClick).AddListener(new UnityAction((object) this, __methodptr(OnClose)));
      }
      this.mScrollRect = ((Component) this).GetComponentInChildren<SRPG_ScrollRect>();
      if (!Object.op_Inequality((Object) this.mScrollRect, (Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent<Vector2>) this.mScrollRect.onValueChanged).AddListener(new UnityAction<Vector2>((object) this, __methodptr(OnScroll)));
    }

    private void OnScroll(Vector2 _vec)
    {
      if (!Object.op_Inequality((Object) this.mUnlockConditionsTooltip, (Object) null))
        return;
      this.mUnlockConditionsTooltip.Close();
      this.mUnlockConditionsTooltip = (Tooltip) null;
    }

    private void OnDestroy()
    {
      if (this.mUnitVoiceData == null)
        return;
      this.mUnitVoiceData.Cleanup();
    }

    private void Update()
    {
      if (!this.mStartPlayVoice)
        return;
      if (this.mUnitVoiceData.Voice == null)
      {
        this.mStartPlayVoice = false;
        this.mLastSelectItem.SetPlayingBadge(false);
      }
      else
      {
        if (this.mUnitVoiceData.Voice.IsPlaying)
          return;
        if (Object.op_Equality((Object) this.mLastSelectItem, (Object) null))
        {
          this.mStartPlayVoice = false;
        }
        else
        {
          this.mLastSelectItem.SetPlayingBadge(false);
          this.mStartPlayVoice = false;
        }
      }
    }

    public void OnOpen() => this.Refresh();

    private void Refresh()
    {
      if (Object.op_Inequality((Object) this.mScrollRect, (Object) null) && Object.op_Inequality((Object) this.mScrollRect.verticalScrollbar, (Object) null))
        this.mScrollRect.verticalScrollbar.value = 1f;
      if (this.mItems != null)
      {
        for (int index = 0; index < this.mItems.Count; ++index)
        {
          if (!Object.op_Equality((Object) this.mItems[index], (Object) null))
            this.mItems[index].SetActive(false);
        }
      }
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(((Component) this).gameObject, (UnitData) null);
      if (dataOfClass == null)
      {
        DebugUtility.LogError("UnitDataがBindされていません");
      }
      else
      {
        this.mCurrentUnit = dataOfClass;
        if (this.mUnitVoiceData != null && this.mLastUnitUniqueID != -1L && this.mLastUnitUniqueID != this.mCurrentUnit.UniqueID)
        {
          this.mUnitVoiceData.Cleanup();
          this.mUnitVoiceData = (UnitData.UnitPlaybackVoiceData) null;
        }
        this.mLastUnitUniqueID = this.mCurrentUnit.UniqueID;
        if (this.mUnitVoiceData != null)
          this.mUnitVoiceData.Cleanup();
        this.mUnitVoiceData = this.mCurrentUnit.GetUnitPlaybackVoiceData();
        if (this.mUnitVoiceData == null)
          DebugUtility.LogError(this.mCurrentUnit.UnitID + " のボイスデータの読み込みに失敗しました");
        else if (Object.op_Equality((Object) this.ItemParent, (Object) null) || Object.op_Equality((Object) this.ItemTemplate, (Object) null))
        {
          DebugUtility.LogError("リストテンプレートが存在しません");
        }
        else
        {
          if (this.mUnitVoiceData.VoiceCueList.Count > this.mItems.Count)
          {
            int num = this.mUnitVoiceData.VoiceCueList.Count - this.mItems.Count;
            for (int index = 0; index < num; ++index)
            {
              GameObject gameObject = Object.Instantiate<GameObject>(this.ItemTemplate);
              if (!Object.op_Equality((Object) gameObject, (Object) null))
              {
                gameObject.transform.SetParent((Transform) this.ItemParent, false);
                this.mItems.Add(gameObject);
                SRPG_Button componentInChildren = gameObject.GetComponentInChildren<SRPG_Button>();
                if (Object.op_Equality((Object) componentInChildren, (Object) null))
                  DebugUtility.LogError("Buttonが存在しません");
                else
                  componentInChildren.AddListener(new SRPG_Button.ButtonClickEvent(this.OnSelect));
              }
            }
          }
          for (int index = 0; index < this.mUnitVoiceData.VoiceCueList.Count; ++index)
          {
            GameObject mItem = this.mItems[index];
            PlayBackUnitVoiceItem componentInChildren = mItem.GetComponentInChildren<PlayBackUnitVoiceItem>();
            if (Object.op_Equality((Object) componentInChildren, (Object) null))
            {
              DebugUtility.LogError("PlayBackUnitVoiceItemが取得できません");
              break;
            }
            componentInChildren.SetUp(this.mUnitVoiceData.VoiceCueList[index]);
            componentInChildren.Refresh();
            componentInChildren.Unlock();
            if (this.mUnitVoiceData.VoiceCueList[index].is_locked)
              componentInChildren.Lock();
            ((Object) mItem).name = this.mUnitVoiceData.VoiceCueList[index].cueInfo.name;
            mItem.SetActive(true);
          }
        }
      }
    }

    private void OnClose()
    {
      WindowController componentInParent = ((Component) this).gameObject.GetComponentInParent<WindowController>();
      if (Object.op_Equality((Object) componentInParent, (Object) null))
      {
        DebugUtility.LogError("WindowControllerが存在しません");
      }
      else
      {
        if (this.mUnitVoiceData != null)
          this.mUnitVoiceData.Cleanup();
        if (this.OnCloseEvent != null)
          this.OnCloseEvent();
        componentInParent.Close();
      }
    }

    private void OnSelect(Button button)
    {
      if (Object.op_Equality((Object) button, (Object) null))
      {
        DebugUtility.LogError("Buttonが存在しません");
      }
      else
      {
        PlayBackUnitVoiceItem componentInChildren = ((Component) button).gameObject.GetComponentInChildren<PlayBackUnitVoiceItem>();
        if (Object.op_Equality((Object) componentInChildren, (Object) null))
          DebugUtility.LogError("PlayBackUnitVoiceItemが存在しません");
        else if (componentInChildren.IsLocked)
        {
          this.mScrollRect.StopMovement();
          this.ShowUnlockConditionsTooltip(componentInChildren);
        }
        else
        {
          if (Object.op_Inequality((Object) this.mLastSelectItem, (Object) null) && this.mLastSelectItem.CueName != componentInChildren.CueName)
            this.mLastSelectItem.SetPlayingBadge(false);
          componentInChildren.SetPlayingBadge(true);
          this.mLastSelectItem = componentInChildren;
          this.PlayVoice(componentInChildren.CueName);
        }
      }
    }

    private void PlayVoice(string name)
    {
      if (this.mCurrentUnit == null)
        DebugUtility.LogError("UnitDataが存在しません");
      else if (this.mUnitVoiceData.Voice == null)
      {
        DebugUtility.LogError("UnitVoiceが存在しません");
      }
      else
      {
        string jobVoiceSheetName = this.mCurrentUnit.GetUnitJobVoiceSheetName();
        if (string.IsNullOrEmpty(jobVoiceSheetName))
        {
          DebugUtility.LogError("UnitDataにボイス設定が存在しません");
        }
        else
        {
          this.mUnitVoiceData.Voice.Play(name.Replace(jobVoiceSheetName + "_", string.Empty), is_stopplay: true);
          this.mStartPlayVoice = true;
        }
      }
    }

    private void ShowUnlockConditionsTooltip(PlayBackUnitVoiceItem voice_item)
    {
      if (Object.op_Equality((Object) this.Preafab_UnlockConditionsTooltip, (Object) null))
        return;
      if (Object.op_Equality((Object) this.mUnlockConditionsTooltip, (Object) null))
        this.mUnlockConditionsTooltip = Object.Instantiate<Tooltip>(this.Preafab_UnlockConditionsTooltip);
      else
        this.mUnlockConditionsTooltip.ResetPosition();
      RectTransform component = ((Component) voice_item).GetComponent<RectTransform>();
      Tooltip.SetTooltipPosition(component, new Vector2()
      {
        x = 0.0f,
        y = component.sizeDelta.y / 2f + this.TOOLTIP_POSITION_OFFSET_Y
      });
      if (!Object.op_Inequality((Object) this.mUnlockConditionsTooltip.TooltipText, (Object) null))
        return;
      this.mUnlockConditionsTooltip.TooltipText.text = voice_item.GetUnlockConditionsText();
    }

    public delegate void CloseEvent();
  }
}
