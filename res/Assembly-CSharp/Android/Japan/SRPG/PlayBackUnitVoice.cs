// Decompiled with JetBrains decompiler
// Type: SRPG.PlayBackUnitVoice
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class PlayBackUnitVoice : MonoBehaviour
  {
    private readonly float TOOLTIP_POSITION_OFFSET_Y = 20f;
    private List<GameObject> mItems = new List<GameObject>();
    private long mLastUnitUniqueID = -1;
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
    private UnitData mCurrentUnit;
    private PlayBackUnitVoiceItem mLastSelectItem;
    private bool mStartPlayVoice;
    private UnitData.UnitPlaybackVoiceData mUnitVoiceData;
    private Tooltip mUnlockConditionsTooltip;
    private SRPG_ScrollRect mScrollRect;
    public PlayBackUnitVoice.CloseEvent OnCloseEvent;

    private void Awake()
    {
      if (!((UnityEngine.Object) this.ItemTemplate != (UnityEngine.Object) null))
        return;
      this.ItemTemplate.SetActive(false);
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.CloseButton != (UnityEngine.Object) null)
        this.CloseButton.onClick.AddListener(new UnityAction(this.OnClose));
      if ((UnityEngine.Object) this.Bg != (UnityEngine.Object) null)
        this.Bg.onClick.AddListener(new UnityAction(this.OnClose));
      this.mScrollRect = this.GetComponentInChildren<SRPG_ScrollRect>();
      if (!((UnityEngine.Object) this.mScrollRect != (UnityEngine.Object) null))
        return;
      this.mScrollRect.onValueChanged.AddListener(new UnityAction<Vector2>(this.OnScroll));
    }

    private void OnScroll(Vector2 _vec)
    {
      if (!((UnityEngine.Object) this.mUnlockConditionsTooltip != (UnityEngine.Object) null))
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
        if ((UnityEngine.Object) this.mLastSelectItem == (UnityEngine.Object) null)
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

    public void OnOpen()
    {
      this.Refresh();
    }

    private void Refresh()
    {
      if ((UnityEngine.Object) this.mScrollRect != (UnityEngine.Object) null && (UnityEngine.Object) this.mScrollRect.verticalScrollbar != (UnityEngine.Object) null)
        this.mScrollRect.verticalScrollbar.value = 1f;
      if (this.mItems != null)
      {
        for (int index = 0; index < this.mItems.Count; ++index)
        {
          if (!((UnityEngine.Object) this.mItems[index] == (UnityEngine.Object) null))
            this.mItems[index].SetActive(false);
        }
      }
      UnitData dataOfClass = DataSource.FindDataOfClass<UnitData>(this.gameObject, (UnitData) null);
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
        else if ((UnityEngine.Object) this.ItemParent == (UnityEngine.Object) null || (UnityEngine.Object) this.ItemTemplate == (UnityEngine.Object) null)
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
              GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ItemTemplate);
              if (!((UnityEngine.Object) gameObject == (UnityEngine.Object) null))
              {
                gameObject.transform.SetParent((Transform) this.ItemParent, false);
                this.mItems.Add(gameObject);
                SRPG_Button componentInChildren = gameObject.GetComponentInChildren<SRPG_Button>();
                if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
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
            if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
            {
              DebugUtility.LogError("PlayBackUnitVoiceItemが取得できません");
              break;
            }
            componentInChildren.SetUp(this.mUnitVoiceData.VoiceCueList[index]);
            componentInChildren.Refresh();
            componentInChildren.Unlock();
            if (this.mUnitVoiceData.VoiceCueList[index].is_locked)
              componentInChildren.Lock();
            mItem.name = this.mUnitVoiceData.VoiceCueList[index].cueInfo.name;
            mItem.SetActive(true);
          }
        }
      }
    }

    private void OnClose()
    {
      WindowController componentInParent = this.gameObject.GetComponentInParent<WindowController>();
      if ((UnityEngine.Object) componentInParent == (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) button == (UnityEngine.Object) null)
      {
        DebugUtility.LogError("Buttonが存在しません");
      }
      else
      {
        PlayBackUnitVoiceItem componentInChildren = button.gameObject.GetComponentInChildren<PlayBackUnitVoiceItem>();
        if ((UnityEngine.Object) componentInChildren == (UnityEngine.Object) null)
          DebugUtility.LogError("PlayBackUnitVoiceItemが存在しません");
        else if (componentInChildren.IsLocked)
        {
          this.mScrollRect.StopMovement();
          this.ShowUnlockConditionsTooltip(componentInChildren);
        }
        else
        {
          if ((UnityEngine.Object) this.mLastSelectItem != (UnityEngine.Object) null && this.mLastSelectItem.CueName != componentInChildren.CueName)
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
        string jobVoiceSheetName = this.mCurrentUnit.GetUnitJobVoiceSheetName(-1);
        if (string.IsNullOrEmpty(jobVoiceSheetName))
        {
          DebugUtility.LogError("UnitDataにボイス設定が存在しません");
        }
        else
        {
          this.mUnitVoiceData.Voice.Play(name.Replace(jobVoiceSheetName + "_", string.Empty), 0.0f, true);
          this.mStartPlayVoice = true;
        }
      }
    }

    private void ShowUnlockConditionsTooltip(PlayBackUnitVoiceItem voice_item)
    {
      if ((UnityEngine.Object) this.Preafab_UnlockConditionsTooltip == (UnityEngine.Object) null)
        return;
      if ((UnityEngine.Object) this.mUnlockConditionsTooltip == (UnityEngine.Object) null)
        this.mUnlockConditionsTooltip = UnityEngine.Object.Instantiate<Tooltip>(this.Preafab_UnlockConditionsTooltip);
      else
        this.mUnlockConditionsTooltip.ResetPosition();
      RectTransform component = voice_item.GetComponent<RectTransform>();
      Tooltip.SetTooltipPosition(component, new Vector2()
      {
        x = 0.0f,
        y = component.sizeDelta.y / 2f + this.TOOLTIP_POSITION_OFFSET_Y
      });
      if (!((UnityEngine.Object) this.mUnlockConditionsTooltip.TooltipText != (UnityEngine.Object) null))
        return;
      this.mUnlockConditionsTooltip.TooltipText.text = voice_item.GetUnlockConditionsText();
    }

    public delegate void CloseEvent();
  }
}
