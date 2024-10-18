// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDropParam
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc.Network.Encoding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

#nullable disable
namespace SRPG
{
  public class QuestDropParam : MonoBehaviour
  {
    [SerializeField]
    public bool IsWarningPopupDisable = true;
    private static QuestDropParam mQuestDropParam;
    private List<SimpleLocalMapsParam> mSimpleLocalMaps = new List<SimpleLocalMapsParam>();
    private Dictionary<string, EnemyDropList> mSimpleLocalMapsDict = new Dictionary<string, EnemyDropList>();
    private List<SimpleDropTableParam> mSimpleDropTables = new List<SimpleDropTableParam>();
    private Dictionary<string, SimpleDropTableList> mSimpleDropTableDict = new Dictionary<string, SimpleDropTableList>();
    private List<SimpleQuestDropParam> mSimpleQuestDrops = new List<SimpleQuestDropParam>();
    private readonly string MASTER_PATH = "Data/QuestDropParam";
    private readonly float LOAD_ASYNC_OWN_TIME_LIMIT = 0.0333333351f;
    private bool mIsLoaded;
    private IEnumerator mStartLoadAsyncIEnumerator;

    public static QuestDropParam Instance => QuestDropParam.mQuestDropParam;

    protected void Awake() => QuestDropParam.mQuestDropParam = this;

    protected void OnDestroy()
    {
      QuestDropParam.mQuestDropParam = (QuestDropParam) null;
      if (this.mIsLoaded || this.mStartLoadAsyncIEnumerator == null)
        return;
      this.StopCoroutine(this.mStartLoadAsyncIEnumerator);
      this.mStartLoadAsyncIEnumerator = (IEnumerator) null;
    }

    protected void Start() => this.LoadAsync();

    public bool Load() => this.LoadJson(this.MASTER_PATH, false);

    private void LoadAsync() => this.LoadJson(this.MASTER_PATH, true);

    private bool LoadJson(string path, bool isAsync)
    {
      if (this.mIsLoaded || string.IsNullOrEmpty(path))
        return false;
      string src = (string) null;
      if (GameUtility.Config_UseEncryption.Value)
      {
        try
        {
          src = System.Text.Encoding.UTF8.GetString(EncryptionHelper.Decrypt(EncryptionHelper.KeyType.APP, AssetManager.LoadBinaryData(path + "Serialized"), SRPG.Network.QuestDigest, EncryptionHelper.DecryptOptions.IsFile));
        }
        catch (Exception ex)
        {
          FlowNode_SendLogMessage.SendLogGenerator sendLogGenerator = new FlowNode_SendLogMessage.SendLogGenerator();
          sendLogGenerator.Add(GameManager.ELoadMasterDataResult.ERROR_QUEST_DROP_PARAM_DECRYPT.ToString(), ex.Message);
          sendLogGenerator.Add("StackTrace", ex.StackTrace);
          sendLogGenerator.Add("Digest", SRPG.Network.QuestDigest);
          sendLogGenerator.Add("DataLength", (src == null ? 0 : src.Length).ToString() + string.Empty);
          GameManager.HandleAnyLoadMasterDataErrors(new GameManager.LoadMasterDataResult()
          {
            Exception = ex,
            LogData = sendLogGenerator,
            Result = GameManager.ELoadMasterDataResult.ERROR_QUEST_DROP_PARAM_DECRYPT
          });
        }
      }
      else
        src = AssetManager.LoadTextData(path);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_QuestDropParam jsonObject = JSONParser.parseJSONObject<JSON_QuestDropParam>(src);
        if (jsonObject == null)
          throw new InvalidJSONException();
        if (isAsync)
        {
          this.DeserializeAsync(jsonObject);
        }
        else
        {
          this.Deserialize(jsonObject);
          this.mIsLoaded = true;
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }

    public void Deserialize(JSON_QuestDropParam json)
    {
      this.mSimpleDropTables.Clear();
      this.mSimpleLocalMaps.Clear();
      this.mSimpleQuestDrops.Clear();
      if (json.simpleDropTable != null)
      {
        for (int index = 0; index < json.simpleDropTable.Length; ++index)
        {
          SimpleDropTableParam simpleDropTableParam = new SimpleDropTableParam();
          if (simpleDropTableParam.Deserialize(json.simpleDropTable[index]))
            this.mSimpleDropTables.Add(simpleDropTableParam);
        }
      }
      if (json.simpleLocalMaps != null)
      {
        for (int index = 0; index < json.simpleLocalMaps.Length; ++index)
        {
          SimpleLocalMapsParam simpleLocalMapsParam = new SimpleLocalMapsParam();
          if (simpleLocalMapsParam.Deserialize(json.simpleLocalMaps[index]))
            this.mSimpleLocalMaps.Add(simpleLocalMapsParam);
        }
      }
      if (json.simpleQuestDrops != null)
      {
        for (int index = 0; index < json.simpleQuestDrops.Length; ++index)
        {
          SimpleQuestDropParam simpleQuestDropParam = new SimpleQuestDropParam();
          if (simpleQuestDropParam.Deserialize(json.simpleQuestDrops[index]))
            this.mSimpleQuestDrops.Add(simpleQuestDropParam);
        }
      }
      this.mIsLoaded = true;
    }

    private void DeserializeAsync(JSON_QuestDropParam json)
    {
      this.mStartLoadAsyncIEnumerator = this.StartLoadAsync(json);
      this.StartCoroutine(this.mStartLoadAsyncIEnumerator);
    }

    [DebuggerHidden]
    private IEnumerator StartLoadAsync(JSON_QuestDropParam json)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new QuestDropParam.\u003CStartLoadAsync\u003Ec__Iterator0()
      {
        json = json,
        \u0024this = this
      };
    }

    public ItemParam GetHardDropPiece(string quest_iname, DateTime date_time)
    {
      List<ItemParam> enemyDropItems = this.GetEnemyDropItems(quest_iname, date_time);
      if (enemyDropItems == null)
        return (ItemParam) null;
      foreach (ItemParam hardDropPiece in enemyDropItems)
      {
        if (hardDropPiece != null && hardDropPiece.type == EItemType.UnitPiece)
          return hardDropPiece;
      }
      return (ItemParam) null;
    }

    private void CompleteLoading()
    {
      if (this.mIsLoaded)
        return;
      while (!this.mIsLoaded)
        this.mStartLoadAsyncIEnumerator.MoveNext();
    }

    public List<ItemParam> GetQuestDropList(string quest_iname, DateTime date_time)
    {
      List<ItemParam> questDropList = new List<ItemParam>();
      SimpleDropTableList simpleDropTables = this.FindSimpleDropTables(quest_iname);
      if (simpleDropTables != null)
      {
        List<ItemParam> currTimeDropItems = this.GetCurrTimeDropItems(new List<SimpleDropTableList>()
        {
          simpleDropTables
        }, date_time);
        if (currTimeDropItems != null)
          questDropList.AddRange((IEnumerable<ItemParam>) currTimeDropItems);
      }
      List<ItemParam> enemyDropItems = this.GetEnemyDropItems(quest_iname, date_time);
      if (enemyDropItems != null)
      {
        foreach (ItemParam itemParam in enemyDropItems)
        {
          if (!questDropList.Contains(itemParam))
            questDropList.Add(itemParam);
        }
      }
      return questDropList;
    }

    public List<BattleCore.DropItemParam> GetQuestDropItemParamList(
      string quest_iname,
      DateTime date_time)
    {
      List<BattleCore.DropItemParam> dropItemParamList = new List<BattleCore.DropItemParam>();
      SimpleDropTableList simpleDropTables = this.FindSimpleDropTables(quest_iname);
      if (simpleDropTables != null)
      {
        List<BattleCore.DropItemParam> timeDropItemParams = this.GetCurrTimeDropItemParams(new List<SimpleDropTableList>()
        {
          simpleDropTables
        }, date_time);
        if (timeDropItemParams != null)
          dropItemParamList.AddRange((IEnumerable<BattleCore.DropItemParam>) timeDropItemParams);
      }
      List<BattleCore.DropItemParam> enemyDropItemParams = this.GetEnemyDropItemParams(quest_iname, date_time);
      if (enemyDropItemParams != null)
      {
        foreach (BattleCore.DropItemParam dropItemParam in enemyDropItemParams)
        {
          BattleCore.DropItemParam param = dropItemParam;
          if (!dropItemParamList.Exists((Predicate<BattleCore.DropItemParam>) (drop => drop.Iname == param.Iname)))
            dropItemParamList.Add(param);
        }
      }
      return dropItemParamList;
    }

    public EnemyDropList FindSimpleLocalMaps(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (EnemyDropList) null;
      this.CompleteLoading();
      EnemyDropList simpleLocalMaps1;
      if (this.mSimpleLocalMapsDict.TryGetValue(iname, out simpleLocalMaps1))
        return simpleLocalMaps1;
      EnemyDropList simpleLocalMaps2 = new EnemyDropList();
      for (int index1 = this.mSimpleLocalMaps.Count - 1; index1 >= 0; --index1)
      {
        if (!(this.mSimpleLocalMaps[index1].iname != iname) && this.mSimpleLocalMaps[index1].droplist != null)
        {
          for (int index2 = 0; index2 < this.mSimpleLocalMaps[index1].droplist.Length; ++index2)
          {
            if (!string.IsNullOrEmpty(this.mSimpleLocalMaps[index1].droplist[index2]))
            {
              SimpleDropTableList simpleDropTables = this.FindSimpleDropTables(this.mSimpleLocalMaps[index1].droplist[index2]);
              simpleLocalMaps2.drp_tbls.Add(simpleDropTables);
            }
          }
        }
      }
      this.mSimpleLocalMapsDict.Add(iname, simpleLocalMaps2);
      return simpleLocalMaps2;
    }

    public SimpleDropTableList FindSimpleDropTables(string iname)
    {
      if (string.IsNullOrEmpty(iname))
        return (SimpleDropTableList) null;
      this.CompleteLoading();
      SimpleDropTableList simpleDropTables1;
      if (this.mSimpleDropTableDict.TryGetValue(iname, out simpleDropTables1))
        return simpleDropTables1;
      SimpleDropTableList simpleDropTables2 = new SimpleDropTableList();
      for (int index = this.mSimpleDropTables.Count - 1; index >= 0; --index)
      {
        if (this.mSimpleDropTables[index].GetCommonName == iname)
          simpleDropTables2.smp_drp_tbls.Add(this.mSimpleDropTables[index]);
      }
      this.mSimpleDropTableDict.Add(iname, simpleDropTables2);
      return simpleDropTables2;
    }

    public bool IsEqualsDropList(string quest_iname, DateTime time1, DateTime time2)
    {
      if (time1 == DateTime.MinValue || time2 == DateTime.MinValue)
        return true;
      List<BattleCore.DropItemParam> dropItemParamList1 = this.GetQuestDropItemParamList(quest_iname, time1);
      List<BattleCore.DropItemParam> dropItemParamList2 = this.GetQuestDropItemParamList(quest_iname, time2);
      if (dropItemParamList1.Count != dropItemParamList2.Count)
        return false;
      for (int index = 0; index < dropItemParamList1.Count; ++index)
      {
        if (dropItemParamList1[index].Iname != dropItemParamList2[index].Iname)
          return false;
      }
      return true;
    }

    private List<ItemParam> GetEnemyDropItems(string quest_iname, DateTime date_time)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(quest_iname);
      if (quest == null || quest.map.Count <= 0)
        return (List<ItemParam>) null;
      EnemyDropList simpleLocalMaps = this.FindSimpleLocalMaps(quest.map[0].mapSetName);
      return simpleLocalMaps == null ? (List<ItemParam>) null : this.GetCurrTimeDropItems(simpleLocalMaps.drp_tbls, date_time);
    }

    private List<ItemParam> GetCurrTimeDropItems(
      List<SimpleDropTableList> drop_tbls,
      DateTime date_time)
    {
      List<string> stringList = new List<string>();
      DateTime t1 = DateTime.MinValue;
      foreach (SimpleDropTableList dropTbl in drop_tbls)
      {
        if (dropTbl.smp_drp_tbls.Count != 0)
        {
          string[] strArray1 = (string[]) null;
          string[] strArray2 = (string[]) null;
          foreach (SimpleDropTableParam smpDrpTbl in dropTbl.smp_drp_tbls)
          {
            if (!smpDrpTbl.IsSuffix)
              strArray1 = smpDrpTbl.dropList;
            else if (smpDrpTbl.IsAvailablePeriod(date_time) && (strArray2 == null || 0 < DateTime.Compare(t1, smpDrpTbl.beginAt)))
            {
              strArray2 = smpDrpTbl.dropList;
              t1 = smpDrpTbl.beginAt;
            }
          }
          string[] collection = strArray2 ?? strArray1;
          if (collection != null)
            stringList.AddRange((IEnumerable<string>) collection);
        }
      }
      if (stringList.Count == 0)
        return (List<ItemParam>) null;
      List<ItemParam> currTimeDropItems = new List<ItemParam>();
      for (int index = 0; index < stringList.Count; ++index)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(stringList[index]);
        currTimeDropItems.Add(itemParam);
      }
      return currTimeDropItems;
    }

    private List<BattleCore.DropItemParam> GetEnemyDropItemParams(
      string quest_iname,
      DateTime date_time)
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(quest_iname);
      if (quest == null || quest.map.Count <= 0)
        return (List<BattleCore.DropItemParam>) null;
      EnemyDropList simpleLocalMaps = this.FindSimpleLocalMaps(quest.map[0].mapSetName);
      return simpleLocalMaps == null ? (List<BattleCore.DropItemParam>) null : this.GetCurrTimeDropItemParams(simpleLocalMaps.drp_tbls, date_time);
    }

    private List<BattleCore.DropItemParam> GetCurrTimeDropItemParams(
      List<SimpleDropTableList> drop_tbls,
      DateTime date_time)
    {
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      DateTime t1 = DateTime.MinValue;
      foreach (SimpleDropTableList dropTbl in drop_tbls)
      {
        if (dropTbl.smp_drp_tbls.Count != 0)
        {
          string[] strArray1 = (string[]) null;
          string[] strArray2 = (string[]) null;
          string[] strArray3 = (string[]) null;
          string[] strArray4 = (string[]) null;
          bool flag = false;
          foreach (SimpleDropTableParam smpDrpTbl in dropTbl.smp_drp_tbls)
          {
            if (!smpDrpTbl.IsSuffix)
            {
              strArray1 = smpDrpTbl.dropList;
              strArray2 = smpDrpTbl.dropcards;
            }
            else if (smpDrpTbl.IsAvailablePeriod(date_time) && (strArray3 == null || 0 < DateTime.Compare(t1, smpDrpTbl.beginAt)) && (strArray4 == null || 0 < DateTime.Compare(t1, smpDrpTbl.beginAt)))
            {
              strArray3 = smpDrpTbl.dropList;
              strArray4 = smpDrpTbl.dropcards;
              t1 = smpDrpTbl.beginAt;
              flag = true;
            }
          }
          string[] collection1 = flag ? strArray3 : strArray1;
          if (collection1 != null)
            stringList1.AddRange((IEnumerable<string>) collection1);
          string[] collection2 = flag ? strArray4 : strArray2;
          if (collection2 != null)
            stringList2.AddRange((IEnumerable<string>) collection2);
        }
      }
      if (stringList1.Count == 0 && stringList2.Count == 0)
        return (List<BattleCore.DropItemParam>) null;
      List<BattleCore.DropItemParam> timeDropItemParams = new List<BattleCore.DropItemParam>();
      for (int index = 0; index < stringList1.Count; ++index)
      {
        BattleCore.DropItemParam dropItemParam = new BattleCore.DropItemParam(MonoSingleton<GameManager>.Instance.GetItemParam(stringList1[index]));
        timeDropItemParams.Add(dropItemParam);
      }
      for (int index = 0; index < stringList2.Count; ++index)
      {
        BattleCore.DropItemParam dropItemParam = new BattleCore.DropItemParam(MonoSingleton<GameManager>.Instance.MasterParam.GetConceptCardParam(stringList2[index]));
        timeDropItemParams.Add(dropItemParam);
      }
      return timeDropItemParams;
    }

    public bool IsChangedQuestDrops(QuestParam quest)
    {
      bool flag = false;
      switch (quest.type)
      {
        case QuestTypes.Story:
        case QuestTypes.Multi:
        case QuestTypes.Free:
        case QuestTypes.Event:
        case QuestTypes.Character:
        case QuestTypes.Gps:
        case QuestTypes.StoryExtra:
        case QuestTypes.Beginner:
        case QuestTypes.MultiGps:
        case QuestTypes.GenesisStory:
        case QuestTypes.AdvanceStory:
        case QuestTypes.UnitRental:
          flag = !this.IsEqualsDropList(quest.iname, GlobalVars.GetDropTableGeneratedDateTime(), TimeManager.ServerTime);
          break;
      }
      return flag;
    }

    public List<QuestParam> GetItemDropQuestList(ItemParam item, DateTime date_time)
    {
      List<QuestParam> itemDropQuestList = new List<QuestParam>();
      List<QuestParam> questParamList = new List<QuestParam>();
      this.CompleteLoading();
      foreach (SimpleQuestDropParam mSimpleQuestDrop in this.mSimpleQuestDrops)
      {
        if (mSimpleQuestDrop.item_iname == item.iname)
        {
          foreach (string iname in mSimpleQuestDrop.questlist)
          {
            QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(iname);
            if (quest != null)
              questParamList.Add(quest);
          }
          break;
        }
      }
      foreach (QuestParam questParam in questParamList)
      {
        if (!questParam.notSearch)
        {
          QuestTypes type = questParam.type;
          switch (type)
          {
            case QuestTypes.Gps:
            case QuestTypes.StoryExtra:
            case QuestTypes.Beginner:
label_17:
              using (List<ItemParam>.Enumerator enumerator = this.GetQuestDropList(questParam.iname, date_time).GetEnumerator())
              {
                while (enumerator.MoveNext())
                {
                  if (enumerator.Current == item)
                  {
                    itemDropQuestList.Add(questParam);
                    break;
                  }
                }
                continue;
              }
            default:
              if (type != QuestTypes.Free && type != QuestTypes.Event)
              {
                switch (type)
                {
                  case QuestTypes.GenesisStory:
                  case QuestTypes.AdvanceStory:
                    goto label_17;
                  default:
                    if (type == QuestTypes.Story)
                      goto label_17;
                    else
                      continue;
                }
              }
              else
                goto case QuestTypes.Gps;
          }
        }
      }
      return itemDropQuestList;
    }

    public Dictionary<int, Dictionary<byte, List<QuestParam>>> GetRuneDropQuestTable(
      DateTime date_time)
    {
      Dictionary<int, Dictionary<byte, List<QuestParam>>> runeDropQuestTable = new Dictionary<int, Dictionary<byte, List<QuestParam>>>();
      Dictionary<string, RuneParam> tableKeyItemIname = MonoSingleton<GameManager>.Instance.MasterParam.CreateRuneParamTable_KeyItemIname();
      this.CompleteLoading();
      RuneParam runeParam = (RuneParam) null;
      foreach (SimpleQuestDropParam mSimpleQuestDrop in this.mSimpleQuestDrops)
      {
        ItemParam itemParam = MonoSingleton<GameManager>.Instance.MasterParam.GetItemParam(mSimpleQuestDrop.item_iname);
        if (itemParam != null && itemParam.type == EItemType.Rune && tableKeyItemIname.TryGetValue(itemParam.iname, out runeParam))
        {
          if (!runeDropQuestTable.ContainsKey(runeParam.seteff_type))
          {
            Dictionary<byte, List<QuestParam>> dictionary = new Dictionary<byte, List<QuestParam>>();
            runeDropQuestTable.Add(runeParam.seteff_type, dictionary);
          }
          if (!runeDropQuestTable[runeParam.seteff_type].ContainsKey((byte) runeParam.slot_index))
          {
            List<QuestParam> questParamList = new List<QuestParam>();
            runeDropQuestTable[runeParam.seteff_type].Add((byte) runeParam.slot_index, questParamList);
          }
          for (int index = 0; index < mSimpleQuestDrop.questlist.Length; ++index)
          {
            QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(mSimpleQuestDrop.questlist[index]);
            if (quest != null && !runeDropQuestTable[runeParam.seteff_type][(byte) runeParam.slot_index].Contains(quest) && quest.IsAvailable() && !this.IsIgnoreDropQuest(quest, itemParam, date_time))
              runeDropQuestTable[runeParam.seteff_type][(byte) runeParam.slot_index].Add(quest);
          }
        }
      }
      return runeDropQuestTable;
    }

    private bool IsIgnoreDropQuest(QuestParam quest, ItemParam target, DateTime date_time)
    {
      if (quest == null || quest.notSearch)
        return true;
      QuestTypes type = quest.type;
      switch (type)
      {
        case QuestTypes.Gps:
        case QuestTypes.StoryExtra:
        case QuestTypes.Beginner:
label_7:
          return this.GetQuestDropList(quest.iname, date_time).FindIndex((Predicate<ItemParam>) (drop => drop == target)) <= -1;
        default:
          if (type != QuestTypes.Free && type != QuestTypes.Event)
          {
            switch (type)
            {
              case QuestTypes.GenesisStory:
              case QuestTypes.AdvanceStory:
                goto label_7;
              default:
                if (type != QuestTypes.Story)
                  return true;
                goto label_7;
            }
          }
          else
            goto case QuestTypes.Gps;
      }
    }
  }
}
