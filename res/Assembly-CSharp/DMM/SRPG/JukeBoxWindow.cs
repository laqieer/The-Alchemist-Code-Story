// Decompiled with JetBrains decompiler
// Type: SRPG.JukeBoxWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using MessagePack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(1, "Initialize", FlowNode.PinTypes.Input, 1)]
  [FlowNode.Pin(2, "Refresh", FlowNode.PinTypes.Input, 2)]
  [FlowNode.Pin(11, "ToMusicList", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(21, "ToPlayList", FlowNode.PinTypes.Input, 21)]
  [FlowNode.Pin(31, "PlayList Add", FlowNode.PinTypes.Input, 31)]
  [FlowNode.Pin(32, "PlayList Del", FlowNode.PinTypes.Input, 32)]
  [FlowNode.Pin(101, "Initialized", FlowNode.PinTypes.Output, 101)]
  public class JukeBoxWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_INITIALIZE = 1;
    private const int PIN_IN_REFRESH = 2;
    private const int PIN_IN_TO_MUSIC_LIST = 11;
    private const int PIN_IN_TO_PLAY_LIST = 21;
    private const int PIN_IN_PLAY_LIST_ADD = 31;
    private const int PIN_IN_PLAY_LIST_DEL = 32;
    private const int PIN_OUT_INITIALIZED = 101;
    [Space(5f)]
    [SerializeField]
    private GameObject Window;
    [SerializeField]
    private GameObject GoMusicListTop;
    [SerializeField]
    private GameObject GoPlayListTop;
    [SerializeField]
    private GameObject EmptyPlayListText;
    [Space(5f)]
    [SerializeField]
    private ScrollItemCentering SICSection;
    [Space(5f)]
    [SerializeField]
    private ContentController CcMusic;
    [Space(5f)]
    [SerializeField]
    private ContentController CcPlayList;
    [Space(5f)]
    [SerializeField]
    private GameObject PlayingBase;
    [SerializeField]
    private GameObject PlayingInfo;
    [SerializeField]
    private GameObject GoPlayListAdd;
    [SerializeField]
    private GameObject GoPlayListDel;
    [Space(5f)]
    [SerializeField]
    private bool IsUnlockCondDisp;
    [Space(5f)]
    [SerializeField]
    private Slider BGMVolume;
    [Space(5f)]
    [SerializeField]
    private Text LyricsName;
    [SerializeField]
    private GameObject LyricsObject;
    [Space(5f)]
    [SerializeField]
    private GameObject ExternalLinkButton;
    [SerializeField]
    private GameObject ExternalLinkMaskButton;
    [Space(5f)]
    [SerializeField]
    private string StartBGMName = "BGM_0002";
    private const float BGM_FADE_OUT_TIME = 1f;
    private const float BGM_MYLIST_FADEOUT_TIME = 3f;
    private const string EVENT_TAG_NAME = "tag_end";
    private List<JukeBoxItemSectionParam> mSectionList = new List<JukeBoxItemSectionParam>();
    private static List<JukeBoxWindow.JukeBoxData> mJukeBoxDataList = new List<JukeBoxWindow.JukeBoxData>();
    private List<JukeBoxWindow.PlayListData> mPlayListDatas = new List<JukeBoxWindow.PlayListData>();
    private JukeBoxWindow.PlayListData mCurrentPlayList;
    private JukeBoxItemSectionParam mCurrentSection;
    private JukeBoxWindow.JukeBoxData mCurrentMusic;
    private JukeBoxWindow.eMode mCurrentMode;
    private List<JukeBoxItemParam> mMusicItemParamList = new List<JukeBoxItemParam>();
    private List<JukeBoxItemParam> mPlayListItemParamList = new List<JukeBoxItemParam>();
    private Vector2 mAnchorPositionSection;
    private Dictionary<string, Vector2> mAnchorPositionMusics = new Dictionary<string, Vector2>();
    private Vector2 mAnchorPositionPlayList;
    private static JukeBoxWindow mInstance = (JukeBoxWindow) null;
    private static readonly string mPrefsKey = PlayerPrefsUtility.JUKEBOX_UNLOCK_BADGE_INFO;

    public static List<JukeBoxWindow.JukeBoxData> JukeBoxDataList => JukeBoxWindow.mJukeBoxDataList;

    public static JukeBoxWindow Instance => JukeBoxWindow.mInstance;

    private void Awake()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) JukeBoxWindow.mInstance, (UnityEngine.Object) null))
        JukeBoxWindow.mInstance = this;
      GameUtility.SetNeverSleep();
      JukeBoxWindow.mJukeBoxDataList.Clear();
      foreach (JukeBoxParam jukeBoxParam in MonoSingleton<GameManager>.Instance.MasterParam.JukeBoxParams)
      {
        if (jukeBoxParam != null)
          JukeBoxWindow.mJukeBoxDataList.Add(new JukeBoxWindow.JukeBoxData()
          {
            param = jukeBoxParam,
            is_unlock = jukeBoxParam.DefaultUnlock
          });
      }
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.Window))
        this.Window.SetActive(false);
      GameUtility.SetGameObjectActive(this.ExternalLinkButton, false);
      GameUtility.SetGameObjectActive(this.ExternalLinkMaskButton, true);
      GameUtility.Config_JukeboxVolume = GameUtility.Config_MusicVolume;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.BGMVolume, (UnityEngine.Object) null))
        return;
      this.BGMVolume.value = GameUtility.Config_MusicVolume;
      Slider.SliderEvent onValueChanged = this.BGMVolume.onValueChanged;
      // ISSUE: reference to a compiler-generated field
      if (JukeBoxWindow.\u003C\u003Ef__am\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        JukeBoxWindow.\u003C\u003Ef__am\u0024cache0 = new UnityAction<float>((object) null, __methodptr(\u003CAwake\u003Em__0));
      }
      // ISSUE: reference to a compiler-generated field
      UnityAction<float> fAmCache0 = JukeBoxWindow.\u003C\u003Ef__am\u0024cache0;
      ((UnityEvent<float>) onValueChanged).AddListener(fAmCache0);
    }

    private void OnDestroy()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) JukeBoxWindow.mInstance, (UnityEngine.Object) this))
        JukeBoxWindow.mInstance = (JukeBoxWindow) null;
      GameUtility.SetDefaultSleepSetting();
      FlowNode_PlayBGM.PlayHomeBGM();
      // ISSUE: method pointer
      MySound.RemoveEventTagCallback(new CriAtomExSequencer.EventCbFunc((object) this, __methodptr(EventTagCallback)));
    }

    private void Update()
    {
    }

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 1:
          this.Initialize();
          break;
        case 2:
          this.Refresh();
          break;
        case 11:
          this.CurrentModeChange(JukeBoxWindow.eMode.MUSIC_LIST);
          break;
        case 21:
          this.CurrentModeChange(JukeBoxWindow.eMode.PLAY_LIST);
          break;
        case 31:
          this.PlayListAdd();
          this.CreatePlayList();
          break;
        case 32:
          this.PlayListDel();
          this.CreatePlayList();
          break;
      }
    }

    private void Initialize()
    {
      MonoSingleton<MySound>.Instance.PlayBGMJukebox(this.StartBGMName, (string) null, 1f, false);
      this.mCurrentMode = JukeBoxWindow.eMode.MUSIC_LIST;
      this.CreateSectionList();
      this.CreateMusicList(this.mCurrentSection);
      this.CreatePlayList();
      this.CurrentModeChange(JukeBoxWindow.eMode.MUSIC_LIST, true);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.PlayingBase))
        this.PlayingBase.SetActive(true);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.PlayingInfo))
        this.PlayingInfo.SetActive(false);
      // ISSUE: method pointer
      MySound.AddEventTagCallback(new CriAtomExSequencer.EventCbFunc((object) this, __methodptr(EventTagCallback)), "tag_end");
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.Window))
        this.Window.SetActive(true);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    private void CreateSectionList()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.SICSection))
        return;
      List<JukeBoxSectionParam> boxSectionParams = MonoSingleton<GameManager>.Instance.MasterParam.JukeBoxSectionParams;
      if (boxSectionParams == null || boxSectionParams.Count <= 0)
        return;
      ContentSource contentSource = new ContentSource();
      this.mSectionList.Clear();
      JukeBoxWindow.PrefsUnlockBadgeInfo prefsUnlockBadgeInfo = JukeBoxWindow.LoadPrefsUnlockBadgeData();
      List<UnityAction> _action_list = new List<UnityAction>();
      foreach (JukeBoxSectionParam jukeBoxSectionParam in boxSectionParams)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        JukeBoxWindow.\u003CCreateSectionList\u003Ec__AnonStorey2 listCAnonStorey2 = new JukeBoxWindow.\u003CCreateSectionList\u003Ec__AnonStorey2();
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey2.section = jukeBoxSectionParam;
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey2.\u0024this = this;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        JukeBoxWindow.\u003CCreateSectionList\u003Ec__AnonStorey1 listCAnonStorey1 = new JukeBoxWindow.\u003CCreateSectionList\u003Ec__AnonStorey1()
        {
          \u003C\u003Ef__ref\u00242 = listCAnonStorey2,
          param = new JukeBoxItemSectionParam(),
          count = _action_list.Count
        };
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey1.param.SectionParam = listCAnonStorey2.section;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        listCAnonStorey1.param.OnClickAction = new UnityAction((object) listCAnonStorey1, __methodptr(\u003C\u003Em__0));
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey1.param.Initialize(contentSource);
        // ISSUE: reference to a compiler-generated field
        this.mSectionList.Add(listCAnonStorey1.param);
        if (this.mCurrentSection == null)
        {
          // ISSUE: reference to a compiler-generated field
          this.mCurrentSection = listCAnonStorey1.param;
          this.mCurrentSection.SetCurrent(true);
        }
        // ISSUE: reference to a compiler-generated method
        JukeBoxWindow.PrefsUnlockBadgeInfo.Data data = prefsUnlockBadgeInfo.list.Find(new Predicate<JukeBoxWindow.PrefsUnlockBadgeInfo.Data>(listCAnonStorey1.\u003C\u003Em__1));
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey1.param.SetNewBadge(data != null);
        // ISSUE: method pointer
        _action_list.Add(new UnityAction((object) listCAnonStorey1, __methodptr(\u003C\u003Em__2)));
      }
      contentSource.SetTable((ContentSource.Param[]) this.mSectionList.ToArray());
      this.SICSection.Initialize(contentSource, _action_list);
    }

    private void CreateMusicList(JukeBoxItemSectionParam section)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      JukeBoxWindow.\u003CCreateMusicList\u003Ec__AnonStorey3 listCAnonStorey3 = new JukeBoxWindow.\u003CCreateMusicList\u003Ec__AnonStorey3();
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey3.section = section;
      // ISSUE: reference to a compiler-generated field
      listCAnonStorey3.\u0024this = this;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.CcMusic))
        return;
      // ISSUE: reference to a compiler-generated method
      List<JukeBoxWindow.JukeBoxData> all = JukeBoxWindow.mJukeBoxDataList.FindAll(new Predicate<JukeBoxWindow.JukeBoxData>(listCAnonStorey3.\u003C\u003Em__0));
      this.CcMusic.Release();
      ContentSource source = new ContentSource();
      this.mMusicItemParamList.Clear();
      JukeBoxWindow.PrefsUnlockBadgeInfo prefsUnlockBadgeInfo = JukeBoxWindow.LoadPrefsUnlockBadgeData();
      foreach (JukeBoxWindow.JukeBoxData jukeBoxData in all)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        JukeBoxWindow.\u003CCreateMusicList\u003Ec__AnonStorey4 listCAnonStorey4 = new JukeBoxWindow.\u003CCreateMusicList\u003Ec__AnonStorey4();
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey4.data = jukeBoxData;
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        JukeBoxWindow.\u003CCreateMusicList\u003Ec__AnonStorey5 listCAnonStorey5 = new JukeBoxWindow.\u003CCreateMusicList\u003Ec__AnonStorey5()
        {
          \u003C\u003Ef__ref\u00243 = listCAnonStorey3,
          \u003C\u003Ef__ref\u00244 = listCAnonStorey4,
          param = new JukeBoxItemParam()
        };
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey5.param.ItemData = listCAnonStorey4.data;
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        listCAnonStorey5.param.OnClickAction = new UnityAction((object) listCAnonStorey5, __methodptr(\u003C\u003Em__0));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        listCAnonStorey5.param.OnClickActionLock = new UnityAction((object) listCAnonStorey5, __methodptr(\u003C\u003Em__1));
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        listCAnonStorey5.param.LongTapAction = new UnityAction((object) listCAnonStorey5, __methodptr(\u003C\u003Em__2));
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey5.param.Initialize(source);
        // ISSUE: reference to a compiler-generated field
        this.mMusicItemParamList.Add(listCAnonStorey5.param);
        // ISSUE: reference to a compiler-generated field
        if (this.IsCurrentMusic(listCAnonStorey5.param))
        {
          // ISSUE: reference to a compiler-generated field
          listCAnonStorey5.param.SetCurrent(true);
        }
        // ISSUE: reference to a compiler-generated method
        JukeBoxWindow.PrefsUnlockBadgeInfo.Data data = prefsUnlockBadgeInfo.list.Find(new Predicate<JukeBoxWindow.PrefsUnlockBadgeInfo.Data>(listCAnonStorey5.\u003C\u003Em__3));
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey5.param.SetNewBadge(data != null);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        listCAnonStorey5.param.SetMylistFlag(this.mCurrentPlayList.bgm_list.Find(new Predicate<JukeBoxWindow.JukeBoxData>(listCAnonStorey5.\u003C\u003Em__4)) != null);
      }
      source.SetTable((ContentSource.Param[]) this.mMusicItemParamList.ToArray());
      this.CcMusic.Initialize(source, Vector2.zero);
    }

    private void CreatePlayList()
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.CcPlayList))
        return;
      this.CcPlayList.Release();
      ContentSource source = new ContentSource();
      this.mPlayListItemParamList.Clear();
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.EmptyPlayListText))
        this.EmptyPlayListText.SetActive(this.mCurrentPlayList.bgm_list.Count <= 0);
      foreach (JukeBoxWindow.JukeBoxData bgm in this.mCurrentPlayList.bgm_list)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        JukeBoxWindow.\u003CCreatePlayList\u003Ec__AnonStorey6 listCAnonStorey6 = new JukeBoxWindow.\u003CCreatePlayList\u003Ec__AnonStorey6();
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey6.data = bgm;
        // ISSUE: reference to a compiler-generated field
        listCAnonStorey6.\u0024this = this;
        JukeBoxItemParam jukeBoxItemParam = new JukeBoxItemParam();
        // ISSUE: reference to a compiler-generated field
        jukeBoxItemParam.ItemData = listCAnonStorey6.data;
        // ISSUE: method pointer
        jukeBoxItemParam.OnClickAction = new UnityAction((object) listCAnonStorey6, __methodptr(\u003C\u003Em__0));
        // ISSUE: method pointer
        jukeBoxItemParam.LongTapAction = new UnityAction((object) listCAnonStorey6, __methodptr(\u003C\u003Em__1));
        jukeBoxItemParam.Initialize(source);
        this.mPlayListItemParamList.Add(jukeBoxItemParam);
        if (this.IsCurrentMusic(jukeBoxItemParam))
          jukeBoxItemParam.SetCurrent(true);
        jukeBoxItemParam.SetMylistFlag(true);
      }
      source.SetTable((ContentSource.Param[]) this.mPlayListItemParamList.ToArray());
      this.CcPlayList.Initialize(source, Vector2.zero);
    }

    private bool IsCurrentMusic(JukeBoxItemParam item)
    {
      return item != null && this.mCurrentMusic != null && item.ItemData == this.mCurrentMusic;
    }

    private void EventTagCallback(string eventParamsString)
    {
      if (this.mCurrentMusic == null || this.mCurrentMode != JukeBoxWindow.eMode.PLAY_LIST || this.mCurrentPlayList.bgm_list.Count <= 1)
        return;
      int num = this.mCurrentPlayList.bgm_list.IndexOf(this.mCurrentMusic);
      if (num < 0)
        return;
      int index = num + 1;
      if (index >= this.mCurrentPlayList.bgm_list.Count)
        index = 0;
      this.Play(this.mCurrentPlayList.bgm_list[index], true);
    }

    private void Refresh()
    {
      switch (this.mCurrentMode)
      {
        case JukeBoxWindow.eMode.MUSIC_LIST:
          this.CreateMusicList(this.mCurrentSection);
          break;
        case JukeBoxWindow.eMode.PLAY_LIST:
          this.CreatePlayList();
          break;
      }
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.PlayingInfo))
        return;
      GameParameter.UpdateAll(this.PlayingInfo);
      this.SetLyricsName(this.mCurrentMusic.param);
    }

    private void CurrentModeChange(JukeBoxWindow.eMode mode, bool is_forced = false)
    {
      if (!is_forced && this.mCurrentMode == mode)
        return;
      this.mCurrentMode = mode;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoMusicListTop))
        this.GoMusicListTop.SetActive(this.mCurrentMode == JukeBoxWindow.eMode.MUSIC_LIST);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoPlayListTop))
        return;
      this.GoPlayListTop.SetActive(this.mCurrentMode == JukeBoxWindow.eMode.PLAY_LIST);
    }

    private void PlayListAdd()
    {
      if (this.mCurrentMusic == null || this.mCurrentPlayList == null)
        return;
      JukeBoxWindow.JukeBoxData data = JukeBoxWindow.mJukeBoxDataList.Find((Predicate<JukeBoxWindow.JukeBoxData>) (t => t.param.Iname == this.mCurrentMusic.param.Iname));
      if (data == null || !data.is_unlock)
        return;
      this.RefrectPlayListAddDel(true);
      this.mMusicItemParamList.Find((Predicate<JukeBoxItemParam>) (t => t.ItemData.param.Iname == data.param.Iname))?.SetMylistFlag(true);
    }

    private void PlayListDel()
    {
      if (this.mCurrentMusic == null || this.mCurrentPlayList == null)
        return;
      JukeBoxWindow.JukeBoxData data = JukeBoxWindow.mJukeBoxDataList.Find((Predicate<JukeBoxWindow.JukeBoxData>) (t => t.param.Iname == this.mCurrentMusic.param.Iname));
      if (data == null)
        return;
      this.RefrectPlayListAddDel(false);
      this.mMusicItemParamList.Find((Predicate<JukeBoxItemParam>) (t => t.ItemData.param.Iname == data.param.Iname))?.SetMylistFlag(false);
    }

    private void SetCurrentAllItem(JukeBoxWindow.JukeBoxData data)
    {
      if (data == null)
        return;
      List<JukeBoxItemParam> jukeBoxItemParamList = new List<JukeBoxItemParam>();
      jukeBoxItemParamList.AddRange((IEnumerable<JukeBoxItemParam>) this.mMusicItemParamList);
      jukeBoxItemParamList.AddRange((IEnumerable<JukeBoxItemParam>) this.mPlayListItemParamList);
      foreach (JukeBoxItemParam jukeBoxItemParam in jukeBoxItemParamList)
      {
        if (jukeBoxItemParam != null && jukeBoxItemParam.ItemData == data)
          jukeBoxItemParam.SetCurrent(true);
      }
    }

    private void ClearCurrentAllItem()
    {
      List<JukeBoxItemParam> jukeBoxItemParamList = new List<JukeBoxItemParam>();
      jukeBoxItemParamList.AddRange((IEnumerable<JukeBoxItemParam>) this.mMusicItemParamList);
      jukeBoxItemParamList.AddRange((IEnumerable<JukeBoxItemParam>) this.mPlayListItemParamList);
      foreach (JukeBoxItemParam jukeBoxItemParam in jukeBoxItemParamList)
        jukeBoxItemParam?.SetCurrent(false);
    }

    private void OnTapItemSection(JukeBoxItemSectionParam item_section, int _count = -1)
    {
      if (item_section == null || item_section.IsCurrent() || UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SICSection, (UnityEngine.Object) null) && _count >= 0 && !this.SICSection.ChangeCenterPage(_count))
        return;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.CcMusic))
      {
        string iname = this.mCurrentSection.SectionParam.Iname;
        Vector2 anchoredPosition = this.CcMusic.anchoredPosition;
        if (this.mAnchorPositionMusics.ContainsKey(iname))
          this.mAnchorPositionMusics[iname] = anchoredPosition;
        else
          this.mAnchorPositionMusics.Add(iname, anchoredPosition);
      }
      this.mCurrentSection.SetCurrent(false);
      this.mCurrentSection = item_section;
      this.mCurrentSection.SetCurrent(true);
      this.CreateMusicList(this.mCurrentSection);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.CcMusic))
        return;
      string iname1 = this.mCurrentSection.SectionParam.Iname;
      if (!this.mAnchorPositionMusics.ContainsKey(iname1))
        return;
      this.CcMusic.anchoredPosition = this.mAnchorPositionMusics[iname1];
    }

    private void OnTapItem(JukeBoxWindow.JukeBoxData data, JukeBoxItemParam param)
    {
      if (data == null)
        return;
      this.Play(data);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ExternalLinkButton))
        this.ExternalLinkButton.SetActive(data.param.ExternalLink > 0);
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.ExternalLinkMaskButton))
        this.ExternalLinkMaskButton.SetActive(data.param.ExternalLink <= 0);
      if (this.mCurrentMode != JukeBoxWindow.eMode.MUSIC_LIST || !this.mMusicItemParamList.Find((Predicate<JukeBoxItemParam>) (t => t.ItemData.param.Iname == data.param.Iname)).IsNewBadge())
        return;
      this.mMusicItemParamList.Find((Predicate<JukeBoxItemParam>) (t => t.ItemData.param.Iname == data.param.Iname)).SetNewBadge(false);
      JukeBoxWindow.PrefsUnlockBadgeInfo info = JukeBoxWindow.LoadPrefsUnlockBadgeData();
      bool flag = false;
      for (int index = 0; index < info.list.Count; ++index)
      {
        if (info.list[index].mIname == data.param.Iname)
        {
          info.list.RemoveAt(index);
          flag = true;
          break;
        }
      }
      if (info.list.Find((Predicate<JukeBoxWindow.PrefsUnlockBadgeInfo.Data>) (t => t.mSection == data.param.SectionId)) == null)
        this.mSectionList.Find((Predicate<JukeBoxItemSectionParam>) (t => t.SectionParam.Iname == data.param.SectionId)).SetNewBadge(false);
      if (!flag)
        return;
      JukeBoxWindow.SavePrefsUnlockBadgeData(info);
    }

    private bool IsEntryPlayList(JukeBoxParam param)
    {
      return this.mCurrentPlayList != null && param != null && this.mCurrentPlayList.bgm_list.Find((Predicate<JukeBoxWindow.JukeBoxData>) (t => t.param.Iname == param.Iname)) != null;
    }

    private void RefrectPlayListAddDel(bool is_entry_play_list)
    {
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoPlayListAdd))
        this.GoPlayListAdd.SetActive(!is_entry_play_list);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.GoPlayListDel))
        return;
      this.GoPlayListDel.SetActive(is_entry_play_list);
    }

    private void Play(JukeBoxWindow.JukeBoxData data, bool _is_mylist_fade = false)
    {
      if (data == null || data.param == null || this.mCurrentMusic != null && this.mCurrentMusic == data)
        return;
      this.Stop();
      this.mCurrentMusic = data;
      this.SetCurrentAllItem(this.mCurrentMusic);
      JukeBoxParam jukeBoxParam = data.param;
      if (jukeBoxParam == null)
        return;
      MonoSingleton<MySound>.Instance.PlayBGMJukebox(jukeBoxParam.Cue, jukeBoxParam.Sheet, !_is_mylist_fade ? 1f : 3f, EventAction.IsUnManagedAssets(jukeBoxParam.Sheet, true));
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) this.PlayingBase))
        this.PlayingBase.SetActive(false);
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.PlayingInfo))
        return;
      float _wait_time = !_is_mylist_fade ? 0.0f : 3f;
      this.StartCoroutine(this.ChangeJukeBoxParamWait(jukeBoxParam, _wait_time));
    }

    [DebuggerHidden]
    private IEnumerator ChangeJukeBoxParamWait(JukeBoxParam _param, float _wait_time)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new JukeBoxWindow.\u003CChangeJukeBoxParamWait\u003Ec__Iterator0()
      {
        _wait_time = _wait_time,
        _param = _param,
        \u0024this = this
      };
    }

    private void Stop()
    {
      if (this.mCurrentMusic == null)
        return;
      this.ClearCurrentAllItem();
      this.mCurrentMusic = (JukeBoxWindow.JukeBoxData) null;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.PlayingBase))
        return;
      this.PlayingBase.SetActive(true);
    }

    private void OnTapItemLocked(JukeBoxWindow.JukeBoxData data)
    {
      if (data == null)
        return;
      string msg = LocalizedText.Get("sys.JUKE_BOX_LOCKED");
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (UnityEngine.Object.op_Implicit((UnityEngine.Object) instance) && this.IsUnlockCondDisp && data.param != null)
      {
        JukeBoxParam jukeBoxParam = data.param;
        if (jukeBoxParam.CondList.Count != 0)
        {
          msg = LocalizedText.Get("sys.JUKE_BOX_LOCKED") + "\n" + LocalizedText.Get("sys.JUKE_BOX_LOCKED_COND");
          for (int index = 0; index < jukeBoxParam.CondList.Count; ++index)
          {
            string cond = jukeBoxParam.CondList[index];
            msg += "\n";
            switch (jukeBoxParam.UnlockType)
            {
              case JukeBoxParam.eUnlockType.QUEST:
                QuestParam quest = instance.FindQuest(cond);
                if (quest != null)
                {
                  msg += string.Format(LocalizedText.Get("sys.JUKE_BOX_LOCKED_COND_QUEST"), (object) quest.title, (object) quest.name);
                  break;
                }
                break;
              case JukeBoxParam.eUnlockType.AREA:
                ChapterParam area = instance.FindArea(cond);
                SectionParam world = instance.FindWorld(area.section);
                if (area != null)
                {
                  msg += string.Format(LocalizedText.Get("sys.JUKE_BOX_LOCKED_COND_AREA"), (object) world.name, (object) area.name);
                  break;
                }
                break;
            }
          }
        }
      }
      UIUtility.SystemMessage(msg, (UIUtility.DialogResultEvent) null);
    }

    private void LongTapItem(JukeBoxWindow.JukeBoxData data)
    {
      string msg = string.Format(LocalizedText.Get("sys.JUKE_BOX_SITUATION"), (object) string.Empty);
      if (data != null && data.param != null && data.param.Situation != null)
        msg = string.Format(LocalizedText.Get("sys.JUKE_BOX_SITUATION"), (object) data.param.Situation);
      UIUtility.SystemMessage(msg, (UIUtility.DialogResultEvent) null);
    }

    public static bool ReflectJukeboxUnlockData(string[] bgm_list)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) JukeBoxWindow.mInstance) || bgm_list == null)
        return false;
      foreach (string bgm1 in bgm_list)
      {
        string bgm = bgm1;
        if (!string.IsNullOrEmpty(bgm))
        {
          JukeBoxWindow.JukeBoxData jukeBoxData = JukeBoxWindow.mJukeBoxDataList.Find((Predicate<JukeBoxWindow.JukeBoxData>) (t => t.param.Iname == bgm));
          if (jukeBoxData != null)
            jukeBoxData.is_unlock = true;
        }
      }
      return true;
    }

    public static bool SetJukeboxPlayListData(JukeBoxWindow.ResPlayList[] playlists)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) JukeBoxWindow.mInstance))
        return false;
      if (playlists == null || playlists.Length <= 0)
      {
        JukeBoxWindow.mInstance.mPlayListDatas.Add(new JukeBoxWindow.PlayListData()
        {
          index = 0,
          bgm_list = new List<JukeBoxWindow.JukeBoxData>()
        });
        JukeBoxWindow.mInstance.mCurrentPlayList = JukeBoxWindow.mInstance.mPlayListDatas[0];
        return true;
      }
      JukeBoxWindow.mInstance.mPlayListDatas.Clear();
      foreach (JukeBoxWindow.ResPlayList playlist in playlists)
      {
        if (playlist != null)
        {
          JukeBoxWindow.PlayListData playListData = new JukeBoxWindow.PlayListData();
          playListData.index = playlist.index;
          playListData.bgm_list = new List<JukeBoxWindow.JukeBoxData>();
          if (playlist.bgm_list != null && playlist.bgm_list.Length != 0)
          {
            foreach (string bgm1 in playlist.bgm_list)
            {
              string bgm = bgm1;
              JukeBoxWindow.JukeBoxData jukeBoxData = JukeBoxWindow.mJukeBoxDataList.Find((Predicate<JukeBoxWindow.JukeBoxData>) (t => t.param.Iname == bgm));
              if (jukeBoxData != null && jukeBoxData.is_unlock && !playListData.bgm_list.Contains(jukeBoxData))
                playListData.bgm_list.Add(jukeBoxData);
            }
          }
          JukeBoxWindow.mInstance.mPlayListDatas.Add(playListData);
        }
      }
      if (JukeBoxWindow.mInstance.mPlayListDatas.Count == 0)
        JukeBoxWindow.mInstance.mPlayListDatas.Add(new JukeBoxWindow.PlayListData()
        {
          index = 0,
          bgm_list = new List<JukeBoxWindow.JukeBoxData>()
        });
      JukeBoxWindow.mInstance.mCurrentPlayList = JukeBoxWindow.mInstance.mPlayListDatas[0];
      return true;
    }

    public static JukeBoxWindow.JukeBoxData GetCurrentJukeBoxData()
    {
      return !UnityEngine.Object.op_Implicit((UnityEngine.Object) JukeBoxWindow.mInstance) ? (JukeBoxWindow.JukeBoxData) null : JukeBoxWindow.mInstance.mCurrentMusic;
    }

    public static void UnlockMusic(string[] bgms)
    {
      if (bgms == null || bgms.Length == 0)
        return;
      GameManager instance = MonoSingleton<GameManager>.Instance;
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) instance))
        return;
      JukeBoxWindow.PrefsUnlockBadgeInfo info = JukeBoxWindow.LoadPrefsUnlockBadgeData();
      bool flag = false;
      foreach (string bgm1 in bgms)
      {
        string bgm = bgm1;
        JukeBoxParam param = instance.MasterParam.JukeBoxParams.Find((Predicate<JukeBoxParam>) (t => t.Iname == bgm));
        if (param != null)
        {
          NotifyList.Push(string.Format(LocalizedText.Get("sys.JUKE_BOX_UNLOCK_TELOP"), (object) param.Title));
          if (info.list.Find((Predicate<JukeBoxWindow.PrefsUnlockBadgeInfo.Data>) (t => t.mIname == param.Iname)) == null)
          {
            info.list.Add(new JukeBoxWindow.PrefsUnlockBadgeInfo.Data(param.Iname, param.SectionId));
            flag = true;
          }
        }
      }
      if (!flag)
        return;
      JukeBoxWindow.SavePrefsUnlockBadgeData(info);
    }

    private static JukeBoxWindow.PrefsUnlockBadgeInfo LoadPrefsUnlockBadgeData()
    {
      JukeBoxWindow.PrefsUnlockBadgeInfo prefsUnlockBadgeInfo = (JukeBoxWindow.PrefsUnlockBadgeInfo) null;
      if (PlayerPrefsUtility.HasKey(JukeBoxWindow.mPrefsKey))
      {
        string str = PlayerPrefsUtility.GetString(JukeBoxWindow.mPrefsKey, string.Empty);
        if (!string.IsNullOrEmpty(str))
        {
          prefsUnlockBadgeInfo = JsonUtility.FromJson<JukeBoxWindow.PrefsUnlockBadgeInfo>(str);
          if (prefsUnlockBadgeInfo == null)
            DebugUtility.LogError(string.Format("JukeBox/PrefsUnlockBadgeInfoの読み込みに失敗しました"));
        }
      }
      if (prefsUnlockBadgeInfo == null)
        prefsUnlockBadgeInfo = new JukeBoxWindow.PrefsUnlockBadgeInfo();
      return prefsUnlockBadgeInfo;
    }

    private static bool SavePrefsUnlockBadgeData(JukeBoxWindow.PrefsUnlockBadgeInfo info)
    {
      if (info == null)
        return false;
      string json = JsonUtility.ToJson((object) info);
      PlayerPrefsUtility.SetString(JukeBoxWindow.mPrefsKey, json, true);
      return true;
    }

    public void OnClickURLLink()
    {
      if (this.mCurrentMusic == null || this.mCurrentMusic.param.ExternalLink <= 0)
        return;
      Application.OpenURL(LocalizedText.Get("sys.JUKE_BOX_EXTERNAL_LINK_" + (object) this.mCurrentMusic.param.ExternalLink));
    }

    private void SetLyricsName(JukeBoxParam param)
    {
      if (!UnityEngine.Object.op_Implicit((UnityEngine.Object) this.LyricsName) || !UnityEngine.Object.op_Implicit((UnityEngine.Object) this.LyricsObject))
        return;
      if (param != null && !string.IsNullOrEmpty(param.Lyricist))
      {
        this.LyricsName.text = param.Lyricist;
        this.LyricsObject.SetActive(true);
      }
      else
      {
        this.LyricsName.text = string.Empty;
        this.LyricsObject.SetActive(false);
      }
    }

    public class JukeBoxData
    {
      public JukeBoxParam param;
      public bool is_unlock;
    }

    private class PlayListData
    {
      public int index;
      public List<JukeBoxWindow.JukeBoxData> bgm_list = new List<JukeBoxWindow.JukeBoxData>();
    }

    private enum eMode
    {
      MUSIC_LIST,
      PLAY_LIST,
    }

    [MessagePackObject(true)]
    [Serializable]
    public class ResPlayList
    {
      public int index;
      public string[] bgm_list;
    }

    [Serializable]
    public class PrefsUnlockBadgeInfo
    {
      public List<JukeBoxWindow.PrefsUnlockBadgeInfo.Data> list = new List<JukeBoxWindow.PrefsUnlockBadgeInfo.Data>();

      [Serializable]
      public class Data
      {
        public string mIname;
        public string mSection;

        public Data(string iname, string section)
        {
          this.mIname = iname;
          this.mSection = section;
        }
      }
    }
  }
}
