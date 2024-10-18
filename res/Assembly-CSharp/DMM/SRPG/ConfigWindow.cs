// Decompiled with JetBrains decompiler
// Type: SRPG.ConfigWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "UpdatePlayerInfo", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(100, "OnPlay", FlowNode.PinTypes.Input, 100)]
  [FlowNode.Pin(200, "Revert Comment", FlowNode.PinTypes.Input, 200)]
  [FlowNode.Pin(101, "PostOnPlay", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(201, "Revert Comment Finished", FlowNode.PinTypes.Output, 201)]
  public class ConfigWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_UPDATE_PLAYER_INFO = 10;
    private const int PIN_INPUT_ON_PLAY = 100;
    private const int PIN_OUTPUT_POST_ON_PLAY = 101;
    private const int PIN_INPUT_REVERT_MULTI_INVITATION_COMMENT = 200;
    private const int PIN_OUTPUT_REVERT_MULTI_INVITATION_COMMENT = 201;
    public Slider SoundVolume;
    public Slider MusicVolume;
    public Slider VoiceVolume;
    public Toggle UseAssetBundle;
    public Toggle UseDevServer;
    public Toggle UseAwsServer;
    public Toggle NewGame;
    public Toggle[] InputMethods = new Toggle[0];
    public Toggle UseAutoPlay;
    public Toggle UsePushStamina;
    public Toggle UsePushNews;
    public Toggle MultiInvitationFlag;
    public InputField MultiInvitationComment;
    public GameObject LoginBonus;
    public GameObject LoginBonus28days;
    [SerializeField]
    private GameObject PremiumLoginBonusOn;
    [SerializeField]
    private GameObject PremiumLoginBonusOff;
    public InputField AssetVersion;
    public Toggle ToggleChatState;
    public Toggle ToggleMultiState;
    public Toggle MultiUserSetting;
    public InputField MultiUserName;
    public Toggle UseLocalMasterData;
    public Toggle UseServerMasterData;
    public Toggle UseSerializedParams;
    public Toggle UseEncryption;
    public Button MasterCheckButton;
    public Button VoiceCopyButton;
    public InputField ClientExPath;
    public GameObject AwardState;
    public GameObject SupportIcon;
    public GameObject Prefab_NewItemBadge;
    public GameObject TreasureList;
    public GameObject TreasureListNode;
    public Toggle ShowSizeBeforeDownloading;
    private List<GameObject> mTreasureListNodes = new List<GameObject>();

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 10:
          this.UpdatePlayerInfo();
          break;
        case 100:
          try
          {
            this.OnPlay();
          }
          catch (Exception ex)
          {
            DebugUtility.Log(ex.Message);
          }
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
          break;
        case 200:
          this.RevertMultiInvitationComment();
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 201);
          break;
      }
    }

    private void Start()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SoundVolume, (UnityEngine.Object) null))
      {
        this.SoundVolume.value = GameUtility.Config_SoundVolume;
        Slider.SliderEvent onValueChanged = this.SoundVolume.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache0 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__0));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache0 = ConfigWindow.\u003C\u003Ef__am\u0024cache0;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache0);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MusicVolume, (UnityEngine.Object) null))
      {
        this.MusicVolume.value = GameUtility.Config_MusicVolume;
        Slider.SliderEvent onValueChanged = this.MusicVolume.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache1 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__1));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache1 = ConfigWindow.\u003C\u003Ef__am\u0024cache1;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache1);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceVolume, (UnityEngine.Object) null))
      {
        this.VoiceVolume.value = GameUtility.Config_VoiceVolume;
        Slider.SliderEvent onValueChanged = this.VoiceVolume.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache2 = new UnityAction<float>((object) null, __methodptr(\u003CStart\u003Em__2));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<float> fAmCache2 = ConfigWindow.\u003C\u003Ef__am\u0024cache2;
        ((UnityEvent<float>) onValueChanged).AddListener(fAmCache2);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAssetBundle, (UnityEngine.Object) null))
      {
        this.UseAssetBundle.isOn = GameUtility.Config_UseAssetBundles.Value;
        Toggle.ToggleEvent onValueChanged = this.UseAssetBundle.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache3 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__3));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache3 = ConfigWindow.\u003C\u003Ef__am\u0024cache3;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache3);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseDevServer, (UnityEngine.Object) null))
      {
        this.UseDevServer.isOn = GameUtility.Config_UseDevServer.Value;
        Toggle.ToggleEvent onValueChanged = this.UseDevServer.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache4 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache4 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__4));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache4 = ConfigWindow.\u003C\u003Ef__am\u0024cache4;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache4);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAwsServer, (UnityEngine.Object) null))
      {
        this.UseAwsServer.isOn = GameUtility.Config_UseAwsServer.Value;
        Toggle.ToggleEvent onValueChanged = this.UseAwsServer.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache5 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache5 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__5));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache5 = ConfigWindow.\u003C\u003Ef__am\u0024cache5;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache5);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseAutoPlay, (UnityEngine.Object) null))
      {
        this.UseAutoPlay.isOn = GameUtility.Config_UseAutoPlay.Value;
        Toggle.ToggleEvent onValueChanged = this.UseAutoPlay.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache6 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache6 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__6));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache6 = ConfigWindow.\u003C\u003Ef__am\u0024cache6;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache6);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UsePushStamina, (UnityEngine.Object) null))
      {
        this.UsePushStamina.isOn = GameUtility.Config_UsePushStamina.Value;
        Toggle.ToggleEvent onValueChanged = this.UsePushStamina.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache7 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache7 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__7));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache7 = ConfigWindow.\u003C\u003Ef__am\u0024cache7;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache7);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UsePushNews, (UnityEngine.Object) null))
      {
        this.UsePushNews.isOn = GameUtility.Config_UsePushNews.Value;
        Toggle.ToggleEvent onValueChanged = this.UsePushNews.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache8 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache8 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__8));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache8 = ConfigWindow.\u003C\u003Ef__am\u0024cache8;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache8);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiInvitationFlag, (UnityEngine.Object) null))
      {
        bool multiInvitaionFlag = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
        GlobalVars.MultiInvitaionFlag = multiInvitaionFlag;
        this.MultiInvitationFlag.isOn = multiInvitaionFlag;
        Toggle.ToggleEvent onValueChanged = this.MultiInvitationFlag.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache9 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache9 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__9));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache9 = ConfigWindow.\u003C\u003Ef__am\u0024cache9;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache9);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiInvitationComment, (UnityEngine.Object) null))
      {
        string invitaionComment = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionComment;
        GlobalVars.MultiInvitaionComment = invitaionComment;
        if (!string.IsNullOrEmpty(invitaionComment))
        {
          if (this.MultiInvitationComment is InputFieldCensorship)
            (this.MultiInvitationComment as InputFieldCensorship).EndEdit(invitaionComment);
          else
            this.MultiInvitationComment.SetText(invitaionComment);
        }
        InputField.OnChangeEvent onValueChanged = this.MultiInvitationComment.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cacheA == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cacheA = new UnityAction<string>((object) null, __methodptr(\u003CStart\u003Em__A));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<string> fAmCacheA = ConfigWindow.\u003C\u003Ef__am\u0024cacheA;
        ((UnityEvent<string>) onValueChanged).AddListener(fAmCacheA);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleChatState, (UnityEngine.Object) null))
      {
        this.ToggleChatState.isOn = GameUtility.Config_ChatState.Value;
        Toggle.ToggleEvent onValueChanged = this.ToggleChatState.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cacheB == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cacheB = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__B));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCacheB = ConfigWindow.\u003C\u003Ef__am\u0024cacheB;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCacheB);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ToggleMultiState, (UnityEngine.Object) null))
      {
        this.ToggleMultiState.isOn = GameUtility.Config_MultiState.Value;
        Toggle.ToggleEvent onValueChanged = this.ToggleMultiState.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cacheC == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cacheC = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__C));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCacheC = ConfigWindow.\u003C\u003Ef__am\u0024cacheC;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCacheC);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.NewGame, (UnityEngine.Object) null))
      {
        this.NewGame.isOn = GameUtility.Config_NewGame.Value;
        Toggle.ToggleEvent onValueChanged = this.NewGame.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cacheD == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cacheD = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__D));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCacheD = ConfigWindow.\u003C\u003Ef__am\u0024cacheD;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCacheD);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiUserSetting, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent<bool>) this.MultiUserSetting.onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(\u003CStart\u003Em__E)));
        ((Component) this.MultiUserSetting).gameObject.SetActive(false);
        ((Component) this.MultiUserName).gameObject.SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseLocalMasterData, (UnityEngine.Object) null))
      {
        this.UseLocalMasterData.isOn = GameUtility.Config_UseLocalData.Value;
        Toggle.ToggleEvent onValueChanged = this.UseLocalMasterData.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cacheE == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cacheE = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__F));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCacheE = ConfigWindow.\u003C\u003Ef__am\u0024cacheE;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCacheE);
        ((Component) this.UseLocalMasterData).gameObject.SetActive(false);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseServerMasterData, (UnityEngine.Object) null))
        ((Component) this.UseServerMasterData).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseSerializedParams, (UnityEngine.Object) null))
      {
        this.UseSerializedParams.isOn = GameUtility.Config_UseSerializedParams.Value;
        Toggle.ToggleEvent onValueChanged = this.UseSerializedParams.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cacheF == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cacheF = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__10));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCacheF = ConfigWindow.\u003C\u003Ef__am\u0024cacheF;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCacheF);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.UseEncryption, (UnityEngine.Object) null))
      {
        this.UseEncryption.isOn = GameUtility.Config_UseEncryption.Value;
        Toggle.ToggleEvent onValueChanged = this.UseEncryption.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache10 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache10 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__11));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache10 = ConfigWindow.\u003C\u003Ef__am\u0024cache10;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache10);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.VoiceCopyButton, (UnityEngine.Object) null))
      {
        // ISSUE: method pointer
        ((UnityEvent) this.VoiceCopyButton.onClick).AddListener(new UnityAction((object) this, __methodptr(\u003CStart\u003Em__12)));
        ((Component) ((Component) this.VoiceCopyButton).gameObject.transform.parent).gameObject.SetActive(false);
      }
      for (int index = 0; index < this.InputMethods.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InputMethods[index], (UnityEngine.Object) null))
        {
          // ISSUE: method pointer
          ((UnityEvent<bool>) this.InputMethods[index].onValueChanged).AddListener(new UnityAction<bool>((object) this, __methodptr(OnInputMethodChange)));
        }
      }
      MoveInputMethods configInputMethod = GameUtility.Config_InputMethod;
      if (configInputMethod < (MoveInputMethods) this.InputMethods.Length && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InputMethods[(int) configInputMethod], (UnityEngine.Object) null))
        this.InputMethods[(int) configInputMethod].isOn = true;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LoginBonus, (UnityEngine.Object) null))
        this.LoginBonus.SetActive(MonoSingleton<GameManager>.Instance.Player.LoginBonus30days != null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LoginBonus28days, (UnityEngine.Object) null))
        this.LoginBonus28days.SetActive(MonoSingleton<GameManager>.Instance.Player.LoginBonus28days != null);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PremiumLoginBonusOn, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.PremiumLoginBonusOff, (UnityEngine.Object) null))
      {
        if (MonoSingleton<GameManager>.Instance.Player.PremiumLoginBonus != null)
        {
          this.PremiumLoginBonusOn.SetActive(true);
          this.PremiumLoginBonusOff.SetActive(false);
        }
        else if (MonoSingleton<GameManager>.Instance.Player.CheckEnablePremiumMember())
        {
          this.PremiumLoginBonusOn.SetActive(true);
          this.PremiumLoginBonusOff.SetActive(false);
        }
        else
        {
          this.PremiumLoginBonusOn.SetActive(false);
          this.PremiumLoginBonusOff.SetActive(true);
        }
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MasterCheckButton, (UnityEngine.Object) null))
        ((Component) this.MasterCheckButton).gameObject.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwardState, (UnityEngine.Object) null))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
          DataSource.Bind<PlayerData>(this.AwardState, player);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SupportIcon, (UnityEngine.Object) null))
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID);
        if (unitDataByUniqueId != null)
          DataSource.Bind<UnitData>(this.SupportIcon, unitDataByUniqueId);
      }
      ConfigWindow.SetupTreasureList(this.TreasureList, this.TreasureListNode, this.Prefab_NewItemBadge, ((Component) this).gameObject, this.mTreasureListNodes);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShowSizeBeforeDownloading, (UnityEngine.Object) null))
      {
        this.ShowSizeBeforeDownloading.isOn = GameUtility.Config_IsEnableShowSizeBeforeDownloading.Value;
        Toggle.ToggleEvent onValueChanged = this.ShowSizeBeforeDownloading.onValueChanged;
        // ISSUE: reference to a compiler-generated field
        if (ConfigWindow.\u003C\u003Ef__am\u0024cache11 == null)
        {
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method pointer
          ConfigWindow.\u003C\u003Ef__am\u0024cache11 = new UnityAction<bool>((object) null, __methodptr(\u003CStart\u003Em__13));
        }
        // ISSUE: reference to a compiler-generated field
        UnityAction<bool> fAmCache11 = ConfigWindow.\u003C\u003Ef__am\u0024cache11;
        ((UnityEvent<bool>) onValueChanged).AddListener(fAmCache11);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnInputMethodChange(bool y)
    {
      if (!y)
        return;
      for (int index = 0; index < this.InputMethods.Length; ++index)
      {
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.InputMethods[index], (UnityEngine.Object) null) && this.InputMethods[index].isOn)
        {
          GameUtility.Config_InputMethod = (MoveInputMethods) index;
          break;
        }
      }
    }

    private void UpdatePlayerInfo()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AwardState, (UnityEngine.Object) null))
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
          DataSource.Bind<PlayerData>(this.AwardState, player);
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.SupportIcon, (UnityEngine.Object) null))
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID);
        if (unitDataByUniqueId != null)
          DataSource.Bind<UnitData>(this.SupportIcon, unitDataByUniqueId);
      }
      GameParameter.UpdateAll(((Component) this).gameObject);
    }

    private void OnPlay()
    {
      AssetDownloader.ClearCachePath();
      AssetDownloader.ResetExistFilePath();
    }

    private void RevertMultiInvitationComment()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.MultiInvitationComment, (UnityEngine.Object) null))
        return;
      string invitaionComment = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionComment;
      if (this.MultiInvitationComment is InputFieldCensorship)
        (this.MultiInvitationComment as InputFieldCensorship).EndEdit(invitaionComment);
      else
        this.MultiInvitationComment.SetText(invitaionComment);
      GlobalVars.MultiInvitaionComment = invitaionComment;
    }

    public static void SetupTreasureList(
      GameObject list,
      GameObject node,
      GameObject newIcon,
      GameObject owner,
      List<GameObject> itemNodes)
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) node, (UnityEngine.Object) null))
        node.SetActive(false);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) newIcon, (UnityEngine.Object) null) && newIcon.gameObject.activeInHierarchy)
        newIcon.SetActive(false);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      SceneBattle instance = SceneBattle.Instance;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) instance, (UnityEngine.Object) null))
        return;
      BattleCore battle = instance.Battle;
      BattleCore.Record record = new BattleCore.Record();
      battle.GainUnitSteal(record);
      battle.GainUnitDrop(record, true);
      DataSource.Bind<BattleCore.Record>(owner, record);
      if (record == null)
        return;
      Transform transform = !UnityEngine.Object.op_Inequality((UnityEngine.Object) list, (UnityEngine.Object) null) ? node.transform.parent : list.transform;
      List<QuestResult.DropItemData> dropItemDataList = new List<QuestResult.DropItemData>();
      for (int index1 = 0; index1 < record.items.Count; ++index1)
      {
        bool flag = false;
        for (int index2 = 0; index2 < dropItemDataList.Count; ++index2)
        {
          if (dropItemDataList[index2].mIsSecret == record.items[index1].mIsSecret)
          {
            if (dropItemDataList[index2].IsItem)
            {
              if (dropItemDataList[index2].itemParam == record.items[index1].itemParam)
              {
                dropItemDataList[index2].Gain(1);
                flag = true;
                break;
              }
            }
            else if (dropItemDataList[index2].IsConceptCard && dropItemDataList[index2].conceptCardParam == record.items[index1].conceptCardParam)
            {
              dropItemDataList[index2].Gain(1);
              flag = true;
              break;
            }
          }
        }
        if (!flag)
        {
          QuestResult.DropItemData dropItemData = new QuestResult.DropItemData();
          if (record.items[index1].IsItem)
          {
            dropItemData.SetupDropItemData(EBattleRewardType.Item, 0L, record.items[index1].itemParam.iname, 1);
            if (record.items[index1].itemParam.type != EItemType.Unit)
            {
              ItemData itemDataByItemParam = player.FindItemDataByItemParam(record.items[index1].itemParam);
              dropItemData.IsNew = !player.ItemEntryExists(record.items[index1].itemParam.iname) || itemDataByItemParam == null || itemDataByItemParam.IsNew;
            }
            else
            {
              string iid = record.items[index1].itemParam.iname;
              if (player.Units.Find((Predicate<UnitData>) (p => p.UnitParam.iname == iid)) == null)
                dropItemData.IsNew = true;
            }
          }
          else if (record.items[index1].IsConceptCard)
            dropItemData.SetupDropItemData(EBattleRewardType.ConceptCard, 0L, record.items[index1].conceptCardParam.iname, 1);
          dropItemData.mIsSecret = record.items[index1].mIsSecret;
          if (dropItemData.mIsSecret)
            dropItemData.IsNew = false;
          dropItemDataList.Add(dropItemData);
        }
      }
      for (int index = 0; index < dropItemDataList.Count; ++index)
      {
        GameObject itemObject = ConfigWindow.CreateItemObject(node, newIcon, dropItemDataList[index]);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) itemObject, (UnityEngine.Object) null))
        {
          itemObject.transform.SetParent(transform, false);
          itemNodes.Add(itemObject);
        }
      }
    }

    public static GameObject CreateItemObject(
      GameObject node,
      GameObject newIcon,
      QuestResult.DropItemData item)
    {
      GameObject root = UnityEngine.Object.Instantiate<GameObject>(node);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) root, (UnityEngine.Object) null))
      {
        DataSource.Bind<QuestResult.DropItemData>(root, item);
        if (item.mIsSecret)
        {
          DropItemIcon component = root.GetComponent<DropItemIcon>();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
            component.IsSecret = true;
        }
        root.SetActive(true);
        GameParameter.UpdateAll(root);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) newIcon, (UnityEngine.Object) null) && item.IsNew)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(newIcon);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null))
          {
            RectTransform transform = gameObject.transform as RectTransform;
            ((Component) transform).gameObject.SetActive(true);
            transform.anchoredPosition = Vector2.zero;
            ((Transform) transform).SetParent(root.transform, false);
          }
        }
      }
      return root;
    }
  }
}
