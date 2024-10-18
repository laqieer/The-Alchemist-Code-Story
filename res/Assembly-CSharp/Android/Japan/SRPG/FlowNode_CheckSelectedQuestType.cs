// Decompiled with JetBrains decompiler
// Type: SRPG.FlowNode_CheckSelectedQuestType
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Linq;
using UnityEngine;

namespace SRPG
{
  [FlowNode.NodeType("System/CheckSelectedQuestType", 32741)]
  [FlowNode.Pin(0, "判定", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(1001, "ストーリー", FlowNode.PinTypes.Output, 1001)]
  [FlowNode.Pin(1002, "闘技場", FlowNode.PinTypes.Output, 1002)]
  [FlowNode.Pin(1003, "チュートリアル", FlowNode.PinTypes.Output, 1003)]
  [FlowNode.Pin(1004, "フリー", FlowNode.PinTypes.Output, 1004)]
  [FlowNode.Pin(1005, "イベント", FlowNode.PinTypes.Output, 1005)]
  [FlowNode.Pin(1006, "キャラクター", FlowNode.PinTypes.Output, 1006)]
  [FlowNode.Pin(1007, "塔", FlowNode.PinTypes.Output, 1007)]
  [FlowNode.Pin(1008, "GPS", FlowNode.PinTypes.Output, 1008)]
  [FlowNode.Pin(1009, "Exクエスト", FlowNode.PinTypes.Output, 1009)]
  [FlowNode.Pin(1010, "初心者", FlowNode.PinTypes.Output, 1010)]
  [FlowNode.Pin(1011, "試練", FlowNode.PinTypes.Output, 1011)]
  [FlowNode.Pin(1012, "レイド", FlowNode.PinTypes.Output, 1012)]
  [FlowNode.Pin(1013, "創世編ストーリー", FlowNode.PinTypes.Output, 1013)]
  [FlowNode.Pin(1014, "創世編ボス", FlowNode.PinTypes.Output, 1014)]
  [FlowNode.Pin(2001, "[マルチ]", FlowNode.PinTypes.Output, 2001)]
  [FlowNode.Pin(2002, "[マルチ]塔", FlowNode.PinTypes.Output, 2002)]
  [FlowNode.Pin(2003, "[マルチ]GPS", FlowNode.PinTypes.Output, 2003)]
  [FlowNode.Pin(3001, "[PvP]フリー", FlowNode.PinTypes.Output, 3001)]
  [FlowNode.Pin(3002, "[PvP]タワーマッチ", FlowNode.PinTypes.Output, 3002)]
  [FlowNode.Pin(3003, "[PvP]ランクマッチ", FlowNode.PinTypes.Output, 3003)]
  public class FlowNode_CheckSelectedQuestType : FlowNode
  {
    public const int INPUT_CHECK = 0;
    public const int OUTPUT_STORY = 1001;
    public const int OUTPUT_ARENA = 1002;
    public const int OUTPUT_TUTORIAL = 1003;
    public const int OUTPUT_FREE = 1004;
    public const int OUTPUT_EVENT = 1005;
    public const int OUTPUT_CHARACTER = 1006;
    public const int OUTPUT_TOWER = 1007;
    public const int OUTPUT_GPS = 1008;
    public const int OUTPUT_EXTRA = 1009;
    public const int OUTPUT_BEGINNER = 1010;
    public const int OUTPUT_ORDEAL = 1011;
    public const int OUTPUT_RAID = 1012;
    public const int OUTPUT_GENESIS_STORY = 1013;
    public const int OUTPUT_GENESIS_BOSS = 1014;
    public const int OUTPUT_MULTI = 2001;
    public const int OUTPUT_MULTITOWER = 2002;
    public const int OUTPUT_MULTIGPS = 2003;
    public const int OUTPUT_VERSUSFREE = 3001;
    public const int OUTPUT_VERSUSRANK = 3002;
    public const int OUTPUT_RANKMATCH = 3003;
    [SerializeField]
    [HideInInspector]
    private FlowNode_CheckSelectedQuestType.eDataType m_TargetDataType;

    public override void OnActivate(int pinID)
    {
      if (pinID != 0)
        return;
      QuestTypes questTypeAuto = this.GetQuestTypeAuto();
      switch (questTypeAuto)
      {
        case QuestTypes.Story:
          this.ActivateOutputLinks(1001);
          break;
        case QuestTypes.Multi:
          this.ActivateOutputLinks(2001);
          break;
        case QuestTypes.Arena:
          this.ActivateOutputLinks(1002);
          break;
        case QuestTypes.Tutorial:
          this.ActivateOutputLinks(1003);
          break;
        case QuestTypes.Free:
          this.ActivateOutputLinks(1004);
          break;
        case QuestTypes.Event:
          this.ActivateOutputLinks(1005);
          break;
        case QuestTypes.Character:
          this.ActivateOutputLinks(1006);
          break;
        case QuestTypes.Tower:
          this.ActivateOutputLinks(1007);
          break;
        case QuestTypes.VersusFree:
          this.ActivateOutputLinks(3001);
          break;
        case QuestTypes.VersusRank:
          this.ActivateOutputLinks(3002);
          break;
        case QuestTypes.Gps:
          this.ActivateOutputLinks(1008);
          break;
        case QuestTypes.Extra:
          this.ActivateOutputLinks(1009);
          break;
        case QuestTypes.MultiTower:
          this.ActivateOutputLinks(2002);
          break;
        case QuestTypes.Beginner:
          this.ActivateOutputLinks(1010);
          break;
        case QuestTypes.MultiGps:
          this.ActivateOutputLinks(2003);
          break;
        case QuestTypes.Ordeal:
          this.ActivateOutputLinks(1011);
          break;
        case QuestTypes.RankMatch:
          this.ActivateOutputLinks(3003);
          break;
        case QuestTypes.Raid:
          this.ActivateOutputLinks(1012);
          break;
        case QuestTypes.GenesisStory:
          this.ActivateOutputLinks(1013);
          break;
        case QuestTypes.GenesisBoss:
          this.ActivateOutputLinks(1014);
          break;
        default:
          DebugUtility.LogError("クエストの種類が判別できませんでした => m_TargetDataType : " + (object) this.m_TargetDataType + " type : " + (object) questTypeAuto);
          break;
      }
    }

    private QuestTypes GetQuestTypeAuto()
    {
      if (this.m_TargetDataType == FlowNode_CheckSelectedQuestType.eDataType.GlobalVars_SelectedChapter)
        return this.GetQuestTypeByChapter(MonoSingleton<GameManager>.Instance.FindArea((string) GlobalVars.SelectedChapter));
      if (this.m_TargetDataType == FlowNode_CheckSelectedQuestType.eDataType.GlobalVars_SelectedArchiveID)
        return this.GetQuestTypeByChapter(MonoSingleton<GameManager>.Instance.FindArea(GlobalVars.SelectedArchiveID));
      if (this.m_TargetDataType == FlowNode_CheckSelectedQuestType.eDataType.GlobalVars_SelectedQuestID)
        return this.GetQuestType(MonoSingleton<GameManager>.Instance.FindQuest(GlobalVars.SelectedQuestID));
      return QuestTypes.None;
    }

    private QuestTypes GetQuestType(QuestParam questParam)
    {
      if (questParam == null)
        return QuestTypes.None;
      return questParam.type;
    }

    private QuestTypes GetQuestTypeByChapter(ChapterParam chapterParam)
    {
      if (chapterParam == null)
        return QuestTypes.None;
      if (chapterParam.children != null && chapterParam.children.Count > 0)
        return this.GetQuestTypeByChapter(chapterParam.children[0]);
      if (chapterParam.quests == null || chapterParam.quests.Count <= 0)
        return QuestTypes.None;
      chapterParam.quests.ToList<QuestParam>().Sort((Comparison<QuestParam>) ((left, right) =>
      {
        if (left.IsJigen != right.IsJigen && left.IsJigen)
          return -1;
        if (left.IsJigen != right.IsJigen && right.IsJigen || left.IsMulti != right.IsMulti && left.IsMulti)
          return 1;
        return left.IsMulti != right.IsMulti && right.IsMulti ? -1 : 0;
      }));
      return this.GetQuestType(chapterParam.quests[0]);
    }

    public enum eDataType
    {
      GlobalVars_SelectedChapter,
      GlobalVars_SelectedArchiveID,
      GlobalVars_SelectedQuestID,
    }
  }
}
