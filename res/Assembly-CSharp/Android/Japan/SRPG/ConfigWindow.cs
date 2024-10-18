// Decompiled with JetBrains decompiler
// Type: SRPG.ConfigWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  [FlowNode.Pin(10, "UpdatePlayerInfo", FlowNode.PinTypes.Input, 0)]
  public class ConfigWindow : MonoBehaviour, IFlowInterface
  {
    public Toggle[] InputMethods = new Toggle[0];
    private List<GameObject> mTreasureListNodes = new List<GameObject>();
    public Slider SoundVolume;
    public Slider MusicVolume;
    public Slider VoiceVolume;
    public Toggle UseAssetBundle;
    public Toggle UseDevServer;
    public Toggle UseAwsServer;
    public Toggle NewGame;
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
    public Button MasterCheckButton;
    public Button VoiceCopyButton;
    public InputField ClientExPath;
    public GameObject AwardState;
    public GameObject SupportIcon;
    public GameObject Prefab_NewItemBadge;
    public GameObject TreasureList;
    public GameObject TreasureListNode;

    public void Activated(int pinID)
    {
      if (pinID != 10)
        return;
      this.UpdatePlayerInfo();
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.SoundVolume != (UnityEngine.Object) null)
      {
        this.SoundVolume.value = GameUtility.Config_SoundVolume;
        this.SoundVolume.onValueChanged.AddListener((UnityAction<float>) (value => GameUtility.Config_SoundVolume = value));
      }
      if ((UnityEngine.Object) this.MusicVolume != (UnityEngine.Object) null)
      {
        this.MusicVolume.value = GameUtility.Config_MusicVolume;
        this.MusicVolume.onValueChanged.AddListener((UnityAction<float>) (value => GameUtility.Config_MusicVolume = value));
      }
      if ((UnityEngine.Object) this.VoiceVolume != (UnityEngine.Object) null)
      {
        this.VoiceVolume.value = GameUtility.Config_VoiceVolume;
        this.VoiceVolume.onValueChanged.AddListener((UnityAction<float>) (value => GameUtility.Config_VoiceVolume = value));
      }
      if ((UnityEngine.Object) this.UseAssetBundle != (UnityEngine.Object) null)
      {
        this.UseAssetBundle.isOn = GameUtility.Config_UseAssetBundles.Value;
        this.UseAssetBundle.onValueChanged.AddListener((UnityAction<bool>) (yes =>
        {
          GameUtility.Config_UseAssetBundles.Value = yes;
          if (yes)
            return;
          AssetDownloader.ClearCache();
          AssetManager.UnloadAll();
        }));
      }
      if ((UnityEngine.Object) this.UseDevServer != (UnityEngine.Object) null)
      {
        this.UseDevServer.isOn = GameUtility.Config_UseDevServer.Value;
        this.UseDevServer.onValueChanged.AddListener((UnityAction<bool>) (yes => GameUtility.Config_UseDevServer.Value = yes));
      }
      if ((UnityEngine.Object) this.UseAwsServer != (UnityEngine.Object) null)
      {
        this.UseAwsServer.isOn = GameUtility.Config_UseAwsServer.Value;
        this.UseAwsServer.onValueChanged.AddListener((UnityAction<bool>) (yes => GameUtility.Config_UseAwsServer.Value = yes));
      }
      if ((UnityEngine.Object) this.UseAutoPlay != (UnityEngine.Object) null)
      {
        this.UseAutoPlay.isOn = GameUtility.Config_UseAutoPlay.Value;
        this.UseAutoPlay.onValueChanged.AddListener((UnityAction<bool>) (yes => GameUtility.Config_UseAutoPlay.Value = yes));
      }
      if ((UnityEngine.Object) this.UsePushStamina != (UnityEngine.Object) null)
      {
        this.UsePushStamina.isOn = GameUtility.Config_UsePushStamina.Value;
        this.UsePushStamina.onValueChanged.AddListener((UnityAction<bool>) (yes => GameUtility.Config_UsePushStamina.Value = yes));
      }
      if ((UnityEngine.Object) this.UsePushNews != (UnityEngine.Object) null)
      {
        this.UsePushNews.isOn = GameUtility.Config_UsePushNews.Value;
        this.UsePushNews.onValueChanged.AddListener((UnityAction<bool>) (yes => GameUtility.Config_UsePushNews.Value = yes));
      }
      if ((UnityEngine.Object) this.MultiInvitationFlag != (UnityEngine.Object) null)
      {
        bool multiInvitaionFlag = MonoSingleton<GameManager>.Instance.Player.MultiInvitaionFlag;
        GlobalVars.MultiInvitaionFlag = multiInvitaionFlag;
        this.MultiInvitationFlag.isOn = multiInvitaionFlag;
        this.MultiInvitationFlag.onValueChanged.AddListener((UnityAction<bool>) (yes => GlobalVars.MultiInvitaionFlag = yes));
      }
      if ((UnityEngine.Object) this.MultiInvitationComment != (UnityEngine.Object) null)
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
        this.MultiInvitationComment.onValueChanged.AddListener((UnityAction<string>) (text => GlobalVars.MultiInvitaionComment = text));
      }
      if ((UnityEngine.Object) this.ToggleChatState != (UnityEngine.Object) null)
      {
        this.ToggleChatState.isOn = GameUtility.Config_ChatState.Value;
        this.ToggleChatState.onValueChanged.AddListener((UnityAction<bool>) (yes => GameUtility.Config_ChatState.Value = yes));
      }
      if ((UnityEngine.Object) this.ToggleMultiState != (UnityEngine.Object) null)
      {
        this.ToggleMultiState.isOn = GameUtility.Config_MultiState.Value;
        this.ToggleMultiState.onValueChanged.AddListener((UnityAction<bool>) (yes => GameUtility.Config_MultiState.Value = yes));
      }
      if ((UnityEngine.Object) this.NewGame != (UnityEngine.Object) null)
      {
        this.NewGame.isOn = GameUtility.Config_NewGame.Value;
        this.NewGame.onValueChanged.AddListener((UnityAction<bool>) (yes => GameUtility.Config_NewGame.Value = yes));
      }
      if ((UnityEngine.Object) this.MultiUserSetting != (UnityEngine.Object) null)
      {
        this.MultiUserSetting.onValueChanged.AddListener((UnityAction<bool>) (flag =>
        {
          if (!((UnityEngine.Object) this.MultiUserName != (UnityEngine.Object) null))
            return;
          this.MultiUserName.readOnly = !flag;
        }));
        this.MultiUserSetting.gameObject.SetActive(false);
        this.MultiUserName.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.UseLocalMasterData != (UnityEngine.Object) null)
      {
        this.UseLocalMasterData.isOn = GameUtility.Config_UseLocalData.Value;
        this.UseLocalMasterData.onValueChanged.AddListener((UnityAction<bool>) (yes => GameUtility.Config_UseLocalData.Value = yes));
        this.UseLocalMasterData.gameObject.SetActive(false);
      }
      if ((UnityEngine.Object) this.VoiceCopyButton != (UnityEngine.Object) null)
      {
        this.VoiceCopyButton.onClick.AddListener((UnityAction) (() =>
        {
          string path = this.ClientExPath.text + "/Assets/sound";
          string str = "Assets/StreamingAssets/";
          if (string.IsNullOrEmpty(path))
            return;
          if (Directory.Exists(path))
          {
            string[] files = Directory.GetFiles(path);
            for (int index = 0; index < files.Length; ++index)
            {
              string destFileName = str + Path.GetFileName(files[index]);
              File.Copy(files[index], destFileName, true);
            }
          }
          else
            Debug.Log((object) ("not exist directory" + path));
        }));
        this.VoiceCopyButton.gameObject.transform.parent.gameObject.SetActive(false);
      }
      for (int index = 0; index < this.InputMethods.Length; ++index)
      {
        if ((UnityEngine.Object) this.InputMethods[index] != (UnityEngine.Object) null)
          this.InputMethods[index].onValueChanged.AddListener(new UnityAction<bool>(this.OnInputMethodChange));
      }
      MoveInputMethods configInputMethod = GameUtility.Config_InputMethod;
      if (configInputMethod < (MoveInputMethods) this.InputMethods.Length && (UnityEngine.Object) this.InputMethods[(int) configInputMethod] != (UnityEngine.Object) null)
        this.InputMethods[(int) configInputMethod].isOn = true;
      if ((UnityEngine.Object) this.LoginBonus != (UnityEngine.Object) null)
        this.LoginBonus.SetActive(MonoSingleton<GameManager>.Instance.Player.LoginBonus != null);
      if ((UnityEngine.Object) this.LoginBonus28days != (UnityEngine.Object) null)
        this.LoginBonus28days.SetActive(MonoSingleton<GameManager>.Instance.Player.LoginBonus28days != null);
      if ((UnityEngine.Object) this.PremiumLoginBonusOn != (UnityEngine.Object) null && (UnityEngine.Object) this.PremiumLoginBonusOff != (UnityEngine.Object) null)
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
      if ((UnityEngine.Object) this.MasterCheckButton != (UnityEngine.Object) null)
        this.MasterCheckButton.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.AwardState != (UnityEngine.Object) null)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
          DataSource.Bind<PlayerData>(this.AwardState, player, false);
      }
      if ((UnityEngine.Object) this.SupportIcon != (UnityEngine.Object) null)
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID);
        if (unitDataByUniqueId != null)
          DataSource.Bind<UnitData>(this.SupportIcon, unitDataByUniqueId, false);
      }
      ConfigWindow.SetupTreasureList(this.TreasureList, this.TreasureListNode, this.Prefab_NewItemBadge, this.gameObject, this.mTreasureListNodes);
      GameParameter.UpdateAll(this.gameObject);
    }

    private void OnInputMethodChange(bool y)
    {
      if (!y)
        return;
      for (int index = 0; index < this.InputMethods.Length; ++index)
      {
        if ((UnityEngine.Object) this.InputMethods[index] != (UnityEngine.Object) null && this.InputMethods[index].isOn)
        {
          GameUtility.Config_InputMethod = (MoveInputMethods) index;
          break;
        }
      }
    }

    private void UpdatePlayerInfo()
    {
      if ((UnityEngine.Object) this.AwardState != (UnityEngine.Object) null)
      {
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (player != null)
          DataSource.Bind<PlayerData>(this.AwardState, player, false);
      }
      if ((UnityEngine.Object) this.SupportIcon != (UnityEngine.Object) null)
      {
        UnitData unitDataByUniqueId = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUniqueID((long) GlobalVars.SelectedSupportUnitUniqueID);
        if (unitDataByUniqueId != null)
          DataSource.Bind<UnitData>(this.SupportIcon, unitDataByUniqueId, false);
      }
      GameParameter.UpdateAll(this.gameObject);
    }

    public static void SetupTreasureList(GameObject list, GameObject node, GameObject newIcon, GameObject owner, List<GameObject> itemNodes)
    {
      if ((UnityEngine.Object) node != (UnityEngine.Object) null)
        node.SetActive(false);
      if ((UnityEngine.Object) newIcon != (UnityEngine.Object) null && newIcon.gameObject.activeInHierarchy)
        newIcon.SetActive(false);
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      SceneBattle instance = SceneBattle.Instance;
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      BattleCore battle = instance.Battle;
      BattleCore.Record record = new BattleCore.Record();
      battle.GainUnitSteal(record);
      battle.GainUnitDrop(record, true);
      DataSource.Bind<BattleCore.Record>(owner, record, false);
      if (record == null)
        return;
      Transform parent = !((UnityEngine.Object) list != (UnityEngine.Object) null) ? node.transform.parent : list.transform;
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
              dropItemData.IsNew = !player.ItemEntryExists(record.items[index1].itemParam.iname) || (itemDataByItemParam == null || itemDataByItemParam.IsNew);
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
        if ((UnityEngine.Object) itemObject != (UnityEngine.Object) null)
        {
          itemObject.transform.SetParent(parent, false);
          itemNodes.Add(itemObject);
        }
      }
    }

    public static GameObject CreateItemObject(GameObject node, GameObject newIcon, QuestResult.DropItemData item)
    {
      GameObject root = UnityEngine.Object.Instantiate<GameObject>(node);
      if ((UnityEngine.Object) root != (UnityEngine.Object) null)
      {
        DataSource.Bind<QuestResult.DropItemData>(root, item, false);
        if (item.mIsSecret)
        {
          DropItemIcon component = root.GetComponent<DropItemIcon>();
          if ((UnityEngine.Object) component != (UnityEngine.Object) null)
            component.IsSecret = true;
        }
        root.SetActive(true);
        GameParameter.UpdateAll(root);
        if ((UnityEngine.Object) newIcon != (UnityEngine.Object) null && item.IsNew)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(newIcon);
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          {
            RectTransform transform = gameObject.transform as RectTransform;
            transform.gameObject.SetActive(true);
            transform.anchoredPosition = Vector2.zero;
            transform.SetParent(root.transform, false);
          }
        }
      }
      return root;
    }
  }
}
