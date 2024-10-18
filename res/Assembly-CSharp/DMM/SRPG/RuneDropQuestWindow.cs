// Decompiled with JetBrains decompiler
// Type: SRPG.RuneDropQuestWindow
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(10, "初期化", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(20, "閉じる直前準備", FlowNode.PinTypes.Input, 20)]
  [FlowNode.Pin(100, "クエスト選択した", FlowNode.PinTypes.Output, 100)]
  public class RuneDropQuestWindow : MonoBehaviour, IFlowInterface
  {
    private const int PIN_INPUT_INIT = 10;
    private const int PIN_INPUT_SETUP_DESTROY = 20;
    private const int PIN_OUTPUT_SELECT_QUEST = 100;
    private readonly string SVB_KEY_SET_EFF_ICON_ON = "SET_EFF_ICON_ON";
    private readonly string SVB_KEY_SET_EFF_ICON_OFF = "SET_EFF_ICON_OFF";
    private readonly string SVB_KEY_SET_EFF_TEXT_ON = "SET_EFF_TEXT_ON";
    private readonly string SVB_KEY_SET_EFF_TEXT_OFF = "SET_EFF_TEXT_OFF";
    [SerializeField]
    private string GAMEOBJECT_ID_EQUIP_SETEFFECT_DETAIL;
    [SerializeField]
    private Transform mEquipedSetEffectDetailParent;
    [SerializeField]
    private SRPG_Button mQuestContentTemplate;
    [SerializeField]
    private Toggle mRuneSetEffectTabTemplate;
    [SerializeField]
    private Toggle[] mRuneSlotTabs;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string mSingleQuestDetailPrefabPath;
    [StringIsResourcePath(typeof (GameObject))]
    [SerializeField]
    private string mMultiQuestDetailPrefabPath;
    private Transform mPreEquipedSetEffectDetailParent;
    private Dictionary<int, Dictionary<byte, List<QuestParam>>> mDropQuestTable;
    private List<SRPG_Button> mQuestContentList = new List<SRPG_Button>();
    private Dictionary<int, Toggle> mRuneSetEffectTabs = new Dictionary<int, Toggle>();
    private static int SELECTED_RUNE_SLOT_INDEX = -1;
    private static int SELECTED_RUNE_SET_EFFECT_INDEX = -1;

    public static bool IsSelectedTab
    {
      get
      {
        return RuneDropQuestWindow.SELECTED_RUNE_SET_EFFECT_INDEX >= 0 && RuneDropQuestWindow.SELECTED_RUNE_SLOT_INDEX >= 0;
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != 10)
      {
        if (pinID != 20)
          return;
        this.SetupDestroy();
      }
      else
        this.Init(false);
    }

    private void SetupDestroy()
    {
      GameObject gameObject = GameObjectID.FindGameObject(this.GAMEOBJECT_ID_EQUIP_SETEFFECT_DETAIL);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mPreEquipedSetEffectDetailParent, (UnityEngine.Object) null))
        return;
      gameObject.transform.SetParent(this.mPreEquipedSetEffectDetailParent, false);
    }

    public void Init(bool is_restore)
    {
      this.mDropQuestTable = new Dictionary<int, Dictionary<byte, List<QuestParam>>>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) QuestDropParam.Instance, (UnityEngine.Object) null))
        this.mDropQuestTable = QuestDropParam.Instance.GetRuneDropQuestTable(GlobalVars.GetDropTableGeneratedDateTime());
      if (!is_restore)
        this.SyncSelectedTab();
      this.CreateTabs_RuneSetEffect();
      this.SelectTab(RuneDropQuestWindow.SELECTED_RUNE_SET_EFFECT_INDEX, RuneDropQuestWindow.SELECTED_RUNE_SLOT_INDEX);
      GameObject gameObject = GameObjectID.FindGameObject(this.GAMEOBJECT_ID_EQUIP_SETEFFECT_DETAIL);
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mEquipedSetEffectDetailParent, (UnityEngine.Object) null))
        return;
      this.mPreEquipedSetEffectDetailParent = gameObject.transform.parent;
      gameObject.transform.SetParent(this.mEquipedSetEffectDetailParent, false);
    }

    private void SyncSelectedTab()
    {
      int slot_index = -1;
      this.GetDefaultIndex(ref RuneDropQuestWindow.SELECTED_RUNE_SET_EFFECT_INDEX, ref slot_index);
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) RuneSelectableListWindow.Instance, (UnityEngine.Object) null) && (byte) RuneSelectableListWindow.Instance.SelectedRuneSlotIndex != byte.MaxValue)
        RuneDropQuestWindow.SELECTED_RUNE_SLOT_INDEX = (int) (byte) RuneSelectableListWindow.Instance.SelectedRuneSlotIndex;
      else
        RuneDropQuestWindow.SELECTED_RUNE_SLOT_INDEX = slot_index;
    }

    private void GetDefaultIndex(ref int set_eff_index, ref int slot_index)
    {
      foreach (int key in this.mDropQuestTable.Keys)
      {
        set_eff_index = key;
        using (Dictionary<byte, List<QuestParam>>.KeyCollection.Enumerator enumerator = this.mDropQuestTable[key].Keys.GetEnumerator())
        {
          if (enumerator.MoveNext())
          {
            byte current = enumerator.Current;
            slot_index = (int) current;
            break;
          }
        }
      }
    }

    private void CreateTabs_RuneSetEffect()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneSetEffectTabTemplate, (UnityEngine.Object) null))
        return;
      ((Component) this.mRuneSetEffectTabTemplate).gameObject.SetActive(false);
      foreach (byte key in this.mDropQuestTable.Keys)
      {
        RuneSetEff runeSetEff = MonoSingleton<GameManager>.Instance.MasterParam.GetRuneSetEff((int) key);
        if (runeSetEff != null && this.GetDictionaryItemCount<byte>(this.mDropQuestTable[(int) key]) > 0)
        {
          Toggle toggle = UnityEngine.Object.Instantiate<Toggle>(this.mRuneSetEffectTabTemplate);
          ((Component) toggle).transform.SetParent(((Component) this.mRuneSetEffectTabTemplate).transform.parent, false);
          ((Component) toggle).gameObject.SetActive(true);
          this.SetupSetEffectTab(toggle, runeSetEff);
          this.mRuneSetEffectTabs.Add((int) key, toggle);
        }
      }
    }

    private void SetupSetEffectTab(Toggle toggle, RuneSetEff set_eff_param)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) toggle, (UnityEngine.Object) null) || set_eff_param == null)
        return;
      SerializeValueBehaviour component1 = ((Component) toggle).GetComponent<SerializeValueBehaviour>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null))
        return;
      ImageArray component2 = component1.list.GetGameObject(this.SVB_KEY_SET_EFF_ICON_ON).GetComponent<ImageArray>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component2, (UnityEngine.Object) null))
        component2.ImageIndex = (int) set_eff_param.icon_index;
      Text component3 = component1.list.GetGameObject(this.SVB_KEY_SET_EFF_TEXT_ON).GetComponent<Text>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component3, (UnityEngine.Object) null))
        component3.text = set_eff_param.name;
      ImageArray component4 = component1.list.GetGameObject(this.SVB_KEY_SET_EFF_ICON_OFF).GetComponent<ImageArray>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component4, (UnityEngine.Object) null))
        component4.ImageIndex = (int) set_eff_param.icon_index;
      Text component5 = component1.list.GetGameObject(this.SVB_KEY_SET_EFF_TEXT_OFF).GetComponent<Text>();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) component5, (UnityEngine.Object) null))
        return;
      component5.text = set_eff_param.name;
    }

    private void CreateQuestList(int rune_set_effect, int rune_slot_index)
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mQuestContentTemplate, (UnityEngine.Object) null))
        return;
      byte key = (byte) rune_set_effect;
      this.RemoveQuestList();
      if (this.mDropQuestTable == null || !this.mDropQuestTable.ContainsKey((int) key) || !this.mDropQuestTable[(int) key].ContainsKey((byte) rune_slot_index))
      {
        DebugUtility.LogError("ルーン逆引き : ドロップクエストのテーブルの初期化が正常におこなわれていない可能性があります");
      }
      else
      {
        List<QuestParam> questParamList = this.mDropQuestTable[(int) key][(byte) rune_slot_index];
        if (questParamList == null)
          return;
        int num = Mathf.Max(0, questParamList.Count - this.mQuestContentList.Count);
        for (int index = 0; index < num; ++index)
        {
          SRPG_Button srpgButton = UnityEngine.Object.Instantiate<SRPG_Button>(this.mQuestContentTemplate);
          ((Component) srpgButton).transform.SetParent(((Component) this.mQuestContentTemplate).transform.parent, false);
          srpgButton.AddListener(new SRPG_Button.ButtonClickEvent(this.OnQuestSelect));
          this.mQuestContentList.Add(srpgButton);
        }
        for (int index = 0; index < questParamList.Count && this.mQuestContentList.Count > index; ++index)
        {
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mQuestContentList[index], (UnityEngine.Object) null))
          {
            DataSource.Bind<QuestParam>(((Component) this.mQuestContentList[index]).gameObject, questParamList[index]);
            GameUtility.SetGameObjectActive((Component) this.mQuestContentList[index], true);
            GameParameter.UpdateAll(((Component) this.mQuestContentList[index]).gameObject);
          }
        }
      }
    }

    private void RemoveQuestList()
    {
      ((Component) this.mQuestContentTemplate).gameObject.SetActive(false);
      for (int index = 0; index < this.mQuestContentList.Count; ++index)
        GameUtility.SetGameObjectActive((Component) this.mQuestContentList[index], false);
    }

    private void OnQuestSelect(SRPG_Button button)
    {
      QuestParam quest = DataSource.FindDataOfClass<QuestParam>(((Component) button).gameObject, (QuestParam) null);
      if (quest == null)
        return;
      PlayerData player = MonoSingleton<GameManager>.Instance.Player;
      ArchiveParam archiveByArea = MonoSingleton<GameManager>.Instance.FindArchiveByArea(quest.ChapterID);
      if (archiveByArea != null && archiveByArea.IsAvailable() && player.CheckUnlock(UnlockTargets.Archive))
      {
        if (LevelLock.IsNeedCheckUnlockConds(quest))
        {
          UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.EventQuest);
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
            return;
        }
        if (Array.Find<QuestParam>(player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null);
        else if (player.IsQuestArchiveOpen(archiveByArea.iname))
        {
          GlobalVars.SelectedQuestID = quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
        }
        else
        {
          GlobalVars.SelectedArchiveID = archiveByArea.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 110);
        }
      }
      else if (!quest.IsDateUnlock())
      {
        UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_DATE_UNLOCK"), (UIUtility.DialogResultEvent) null);
      }
      else
      {
        if (LevelLock.IsNeedCheckUnlockConds(quest))
        {
          UnlockTargets targetByQuestId = LevelLock.GetTargetByQuestId(quest.iname, UnlockTargets.EventQuest);
          if (LevelLock.ShowLockMessage(player.Lv, player.VipRank, targetByQuestId))
            return;
        }
        if (Array.Find<QuestParam>(player.AvailableQuests, (Predicate<QuestParam>) (p => p == quest)) == null)
        {
          UIUtility.NegativeSystemMessage((string) null, LocalizedText.Get("sys.DISABLE_QUEST_CHALLENGE"), (UIUtility.DialogResultEvent) null);
        }
        else
        {
          GlobalVars.SelectedQuestID = quest.iname;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
        }
      }
    }

    private int GetDictionaryItemCount<T>(Dictionary<T, List<QuestParam>> rune_quests)
    {
      int dictionaryItemCount = 0;
      foreach (List<QuestParam> questParamList in rune_quests.Values)
      {
        if (questParamList != null)
          dictionaryItemCount += questParamList.Count;
      }
      return dictionaryItemCount;
    }

    public void OpenQuestDetail(GameObject obj)
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(obj, (QuestParam) null);
      if (dataOfClass == null)
        return;
      GameObject gameObject1 = AssetManager.Load<GameObject>(!dataOfClass.IsMulti ? this.mSingleQuestDetailPrefabPath : this.mMultiQuestDetailPrefabPath);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject1, (UnityEngine.Object) null))
        return;
      GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject1);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) gameObject2, (UnityEngine.Object) null))
        return;
      QuestCampaignData[] questCampaigns = MonoSingleton<GameManager>.Instance.FindQuestCampaigns(dataOfClass);
      DataSource.Bind<QuestParam>(gameObject2, dataOfClass);
      DataSource.Bind<QuestCampaignData[]>(gameObject2, questCampaigns);
    }

    private void SelectTab(int set_effect_index, int slot_index)
    {
      this.SelectTab_RuneSetEffect(set_effect_index);
      this.SelectTab_RuneSlot(slot_index);
    }

    private void SelectTab_RuneSlot(int slot_index)
    {
      if (this.mRuneSlotTabs.Length <= slot_index)
      {
        int set_eff_index = -1;
        this.GetDefaultIndex(ref set_eff_index, ref slot_index);
      }
      if (slot_index <= -1)
      {
        this.RemoveQuestList();
      }
      else
      {
        GameUtility.SetToggle(this.mRuneSlotTabs[slot_index], true);
        RuneDropQuestWindow.SELECTED_RUNE_SLOT_INDEX = slot_index;
        this.CreateQuestList(RuneDropQuestWindow.SELECTED_RUNE_SET_EFFECT_INDEX, RuneDropQuestWindow.SELECTED_RUNE_SLOT_INDEX);
      }
    }

    public void SelectTab_RuneSlot(Toggle self)
    {
      if (!self.isOn)
        return;
      this.SelectTab_RuneSlot(Array.FindIndex<Toggle>(this.mRuneSlotTabs, (Predicate<Toggle>) (tab => UnityEngine.Object.op_Equality((UnityEngine.Object) tab, (UnityEngine.Object) self))));
    }

    private void SelectTab_RuneSetEffect(int set_effect_index)
    {
      if (!this.mRuneSetEffectTabs.ContainsKey(set_effect_index))
      {
        int slot_index = -1;
        this.GetDefaultIndex(ref set_effect_index, ref slot_index);
      }
      if (!this.mRuneSetEffectTabs.ContainsKey(set_effect_index))
      {
        this.RemoveQuestList();
        DebugUtility.LogError("該当クエストが1つも見つからなかった可能性があります");
      }
      else
      {
        GameUtility.SetToggle(this.mRuneSetEffectTabs[set_effect_index], true);
        RuneDropQuestWindow.SELECTED_RUNE_SET_EFFECT_INDEX = set_effect_index;
        this.CreateQuestList(RuneDropQuestWindow.SELECTED_RUNE_SET_EFFECT_INDEX, RuneDropQuestWindow.SELECTED_RUNE_SLOT_INDEX);
      }
    }

    public void SelectTab_RuneSetEffect(Toggle self)
    {
      if (!self.isOn)
        return;
      int set_effect_index = 0;
      foreach (int key in this.mRuneSetEffectTabs.Keys)
      {
        if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mRuneSetEffectTabs[key], (UnityEngine.Object) self))
        {
          set_effect_index = key;
          break;
        }
      }
      this.SelectTab_RuneSetEffect(set_effect_index);
    }
  }
}
