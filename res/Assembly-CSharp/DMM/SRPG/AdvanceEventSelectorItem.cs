// Decompiled with JetBrains decompiler
// Type: SRPG.AdvanceEventSelectorItem
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "表示更新", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1, "選択された", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(100, "期間外だった", FlowNode.PinTypes.Output, 100)]
  [FlowNode.Pin(101, "未解放だった", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(102, "鍵解放ポップ", FlowNode.PinTypes.Output, 102)]
  [FlowNode.Pin(103, "鍵の有効期限が来た", FlowNode.PinTypes.Output, 103)]
  [FlowNode.Pin(105, "イベント一覧に遷移", FlowNode.PinTypes.Output, 105)]
  public class AdvanceEventSelectorItem : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_REFRESH = 0;
    private const int PIN_INPUT_ONCLICK = 1;
    private const int PIN_OUT_OUT_OF_PERIOD = 100;
    private const int PIN_OUT_NO_LIBERATION = 101;
    private const int PIN_OUTPUT_ONCLICK_LOCKED_KEY = 102;
    private const int PIN_OUTPUT_LOCK_KEY = 103;
    private const int PIN_OUTPUT_OPEN_KEY = 104;
    private const int PIN_OUTPUT_TO_EVENTLIST = 105;
    [SerializeField]
    private Text TextTitle;
    [SerializeField]
    private Transform TrParentBanner;
    [SerializeField]
    private GameObject NotAvailable;
    [Space(5f)]
    [SerializeField]
    private SRPG_Button SelectBtn;
    [Space(5f)]
    [SerializeField]
    private GameObject KeyLocked;
    [SerializeField]
    private RawImage KeyIcon;
    [SerializeField]
    private Image KeyItemFrame;
    [SerializeField]
    private Text UseKeyNum;
    [SerializeField]
    private Text KeyAmount;
    [SerializeField]
    private string ChapterBannerPath;
    [Space(5f)]
    [SerializeField]
    private QuestTimeLimit QuestTimer;
    private AdvanceEventParam mEventParam;
    private ChapterParam mChapterParam;
    private AdvanceEventSelector mSelector;
    private KeyItem mKeyItem;
    private bool mIsNoLiberation;

    public AdvanceEventParam EventParam => this.mEventParam;

    public ChapterParam ChapterParam => this.mChapterParam;

    public void Activated(int pinID)
    {
      if (pinID != 0)
      {
        if (pinID != 1)
          return;
        this.OnClick();
      }
      else
        this.Refresh();
    }

    public void SetItem()
    {
      this.mEventParam = DataSource.FindDataOfClass<AdvanceEventParam>(((Component) this).gameObject, (AdvanceEventParam) null);
      this.mChapterParam = DataSource.FindDataOfClass<ChapterParam>(((Component) this).gameObject, (ChapterParam) null);
      this.mSelector = DataSource.FindDataOfClass<AdvanceEventSelector>(((Component) this).gameObject, (AdvanceEventSelector) null);
      if (this.mEventParam == null && this.mChapterParam == null || Object.op_Equality((Object) this.mSelector, (Object) null))
        return;
      this.mIsNoLiberation = false;
      if (Object.op_Inequality((Object) this.TextTitle, (Object) null) && this.mEventParam != null)
        this.TextTitle.text = this.mEventParam.Name;
      string prefab_path = string.Empty;
      prefab_path = this.mEventParam == null || this.mEventParam.TransType != eTransType.None && this.mEventParam.TransType != eTransType.Normal ? this.ChapterBannerPath + "/" + this.mChapterParam.prefabPath : this.mEventParam.EventBanner;
      if (Object.op_Implicit((Object) this.TrParentBanner) && !string.IsNullOrEmpty(prefab_path) && Object.op_Implicit((Object) AdvanceManager.Instance))
        AdvanceManager.Instance.LoadAssets<GameObject>(prefab_path, (AdvanceManager.LoadAssetCallback<GameObject>) (prefab =>
        {
          if (Object.op_Equality((Object) prefab, (Object) null))
          {
            DebugUtility.LogError("AdvanceEventSelectorItem/AssetLoad Error! name=" + prefab_path);
          }
          else
          {
            prefab.gameObject.SetActive(true);
            Object.Instantiate<GameObject>(prefab, this.TrParentBanner.position, Quaternion.identity, this.TrParentBanner);
          }
        }));
      if (Object.op_Inequality((Object) this.QuestTimer, (Object) null))
        DataSource.Bind<ChapterParam>(((Component) this.QuestTimer).gameObject, this.mChapterParam);
      if (!this.mChapterParam.IsKeyQuest())
        return;
      this.mKeyItem = this.mChapterParam.keys[0];
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(this.mKeyItem.iname);
      if (itemParam == null)
        return;
      if (Object.op_Inequality((Object) this.KeyIcon, (Object) null))
        MonoSingleton<GameManager>.Instance.ApplyTextureAsync(this.KeyIcon, AssetPath.ItemIcon(itemParam));
      if (!Object.op_Inequality((Object) this.KeyItemFrame, (Object) null))
        return;
      this.KeyItemFrame.sprite = GameSettings.Instance.GetItemFrame(itemParam);
    }

    public void OnClick()
    {
      if (Object.op_Equality((Object) this.mSelector, (Object) null) || this.mChapterParam == null)
        return;
      if (this.mIsNoLiberation)
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
      else if (!AdvanceEventSelector.IsDateUnlock(this.mChapterParam, Network.GetServerTime()))
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
      else if (this.IsKeyLocked())
      {
        GlobalVars.SelectedChapter.Set(this.mChapterParam.iname);
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 102);
      }
      else if (this.EventParam.TransType == eTransType.EventList)
      {
        GlobalVars.SelectedQuestID = string.Empty;
        GlobalVars.SelectedChapter.Set(this.mChapterParam.iname);
        GlobalVars.SelectedSection.Set("WD_DAILY");
        FlowNode_GameObject.ActivateOutputLinks((Component) this, 105);
      }
      else
        this.mSelector.OnSelectItem(this);
    }

    public void Refresh()
    {
      if (this.mChapterParam == null)
        return;
      this.UpdateKeyItemValue();
      this.UpdateKeyStatus();
      long serverTime = Network.GetServerTime();
      if (Object.op_Inequality((Object) this.NotAvailable, (Object) null))
        this.NotAvailable.SetActive(!AdvanceEventSelector.IsDateUnlock(this.mChapterParam, serverTime) || this.mIsNoLiberation);
      if (!Object.op_Inequality((Object) this.QuestTimer, (Object) null))
        return;
      this.QuestTimer.UpdateValue();
    }

    private void Awake()
    {
    }

    private void OnDestroy()
    {
      if (!Object.op_Inequality((Object) this.QuestTimer, (Object) null))
        return;
      this.QuestTimer.mOnTimeLimit -= new QuestTimeLimit.OnTimeLimit(this.PlayStartLockKey);
    }

    private void UpdateKeyItemValue()
    {
      if (this.mChapterParam == null || !this.mChapterParam.IsKeyQuest())
        return;
      ItemData itemDataByItemParam = MonoSingleton<GameManager>.Instance.Player.FindItemDataByItemParam(MonoSingleton<GameManager>.Instance.GetItemParam(this.mKeyItem.iname));
      int num = itemDataByItemParam == null ? 0 : itemDataByItemParam.Num;
      if (Object.op_Inequality((Object) this.UseKeyNum, (Object) null))
        this.UseKeyNum.text = this.mKeyItem.num.ToString();
      if (!Object.op_Inequality((Object) this.KeyAmount, (Object) null))
        return;
      this.KeyAmount.text = num.ToString();
    }

    private bool IsKeyLocked()
    {
      if (Object.op_Inequality((Object) this.QuestTimer, (Object) null))
        this.QuestTimer.mOnTimeLimit -= new QuestTimeLimit.OnTimeLimit(this.PlayStartLockKey);
      if (this.mChapterParam.IsKeyQuest())
      {
        if (!AdvanceEventSelector.IsKeyUnlock(this.mChapterParam, Network.GetServerTime()))
          return true;
        if (Object.op_Inequality((Object) this.QuestTimer, (Object) null))
          this.QuestTimer.mOnTimeLimit += new QuestTimeLimit.OnTimeLimit(this.PlayStartLockKey);
      }
      return false;
    }

    private void UpdateKeyStatus()
    {
      if (!Object.op_Inequality((Object) this.KeyLocked, (Object) null))
        return;
      this.KeyLocked.SetActive(this.IsKeyLocked());
    }

    public void PlayStartLockKey()
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 103);
    }
  }
}
