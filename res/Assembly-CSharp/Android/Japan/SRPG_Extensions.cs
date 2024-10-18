// Decompiled with JetBrains decompiler
// Type: SRPG_Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using SRPG;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class SRPG_Extensions
{
  private static readonly string[] mPrefixes = new string[3]{ "U_", "M_", "F_" };

  public static IntVector2 ToOffset(this EUnitDirection direction)
  {
    switch (direction)
    {
      case EUnitDirection.PositiveX:
        return new IntVector2(1, 0);
      case EUnitDirection.PositiveY:
        return new IntVector2(0, 1);
      case EUnitDirection.NegativeY:
        return new IntVector2(0, -1);
      default:
        return new IntVector2(-1, 0);
    }
  }

  public static Vector3 ToVector(this EUnitDirection direction)
  {
    switch (direction)
    {
      case EUnitDirection.PositiveX:
        return Vector3.right;
      case EUnitDirection.PositiveY:
        return Vector3.forward;
      case EUnitDirection.NegativeX:
        return Vector3.left;
      case EUnitDirection.NegativeY:
        return Vector3.back;
      default:
        return Vector3.left;
    }
  }

  public static Quaternion ToRotation(this EUnitDirection direction)
  {
    switch (direction)
    {
      case EUnitDirection.PositiveX:
        return Quaternion.LookRotation(Vector3.right);
      case EUnitDirection.PositiveY:
        return Quaternion.LookRotation(Vector3.forward);
      case EUnitDirection.NegativeX:
        return Quaternion.LookRotation(Vector3.left);
      case EUnitDirection.NegativeY:
        return Quaternion.LookRotation(Vector3.back);
      default:
        return Quaternion.identity;
    }
  }

  public static bool Contains(this Rect rect, Rect other)
  {
    return (double) rect.yMin >= (double) other.yMin && (double) rect.yMax >= (double) other.yMax && ((double) rect.xMin >= (double) other.xMin && (double) rect.xMax >= (double) other.xMax);
  }

  public static UnitData GetInstanceData(this GameParameter.UnitInstanceTypes InstanceType, GameObject gameObject)
  {
    UnitData unitData = (UnitData) null;
    switch (InstanceType)
    {
      case GameParameter.UnitInstanceTypes.Any:
        unitData = DataSource.FindDataOfClass<UnitData>(gameObject, (UnitData) null);
        if (unitData == null)
        {
          SRPG.Unit dataOfClass = DataSource.FindDataOfClass<SRPG.Unit>(gameObject, (SRPG.Unit) null);
          if (dataOfClass != null)
          {
            unitData = dataOfClass.UnitData;
            break;
          }
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.CurrentTurn:
        if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null && SceneBattle.Instance.Battle.CurrentUnit != null)
          return SceneBattle.Instance.Battle.CurrentUnit.UnitData;
        break;
      case GameParameter.UnitInstanceTypes.ArenaPlayerUnit0:
      case GameParameter.UnitInstanceTypes.ArenaPlayerUnit1:
      case GameParameter.UnitInstanceTypes.ArenaPlayerUnit2:
        ArenaPlayer dataOfClass1 = DataSource.FindDataOfClass<ArenaPlayer>(gameObject, (ArenaPlayer) null);
        if (dataOfClass1 != null)
        {
          int index = (int) (InstanceType - 4);
          unitData = dataOfClass1.Unit[index];
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.EnemyArenaPlayerUnit0:
      case GameParameter.UnitInstanceTypes.EnemyArenaPlayerUnit1:
      case GameParameter.UnitInstanceTypes.EnemyArenaPlayerUnit2:
        ArenaPlayer selectedArenaPlayer = (ArenaPlayer) GlobalVars.SelectedArenaPlayer;
        if (selectedArenaPlayer != null)
        {
          int index = (int) (InstanceType - 7);
          unitData = selectedArenaPlayer.Unit[index];
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.PartyUnit0:
      case GameParameter.UnitInstanceTypes.PartyUnit1:
      case GameParameter.UnitInstanceTypes.PartyUnit2:
        PlayerData player1 = MonoSingleton<GameManager>.Instance.Player;
        int index1 = (int) (InstanceType - 10);
        long unitUniqueId1 = player1.Partys[(int) GlobalVars.SelectedPartyIndex].GetUnitUniqueID(index1);
        unitData = player1.FindUnitDataByUniqueID(unitUniqueId1);
        break;
      case GameParameter.UnitInstanceTypes.VersusUnit:
        PlayerData player2 = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId2 = player2.Partys[7].GetUnitUniqueID(0);
        unitData = player2.FindUnitDataByUniqueID(unitUniqueId2);
        break;
      case GameParameter.UnitInstanceTypes.MultiUnit:
        PlayerData player3 = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId3 = player3.Partys[2].GetUnitUniqueID(0);
        unitData = player3.FindUnitDataByUniqueID(unitUniqueId3);
        break;
      case GameParameter.UnitInstanceTypes.MultiTowerUnit:
        PlayerData player4 = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId4 = player4.Partys[8].GetUnitUniqueID(0);
        unitData = player4.FindUnitDataByUniqueID(unitUniqueId4);
        break;
      case GameParameter.UnitInstanceTypes.VersusCpuUnit0:
      case GameParameter.UnitInstanceTypes.VersusCpuUnit1:
      case GameParameter.UnitInstanceTypes.VersusCpuUnit2:
        VersusCpuData dataOfClass2 = DataSource.FindDataOfClass<VersusCpuData>(gameObject, (VersusCpuData) null);
        if (dataOfClass2 != null)
        {
          int index2 = (int) (InstanceType - 16);
          if (dataOfClass2.Units.Length > index2)
          {
            unitData = dataOfClass2.Units[index2];
            break;
          }
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.RankMatchUnit:
        PlayerData player5 = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId5 = player5.Partys[10].GetUnitUniqueID(0);
        unitData = player5.FindUnitDataByUniqueID(unitUniqueId5);
        break;
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit0:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit1:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit2:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit3:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit4:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit5:
        if (VersusDraftList.VersusDraftUnitDataListPlayer != null)
        {
          int index2 = (int) (InstanceType - 21);
          if (VersusDraftList.VersusDraftUnitDataListPlayer.Count > index2)
          {
            unitData = VersusDraftList.VersusDraftUnitDataListPlayer[index2];
            break;
          }
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.VersusDraftEnemyUnit0:
      case GameParameter.UnitInstanceTypes.VersusDraftEnemyUnit1:
      case GameParameter.UnitInstanceTypes.VersusDraftEnemyUnit2:
      case GameParameter.UnitInstanceTypes.VersusDraftEnemyUnit3:
      case GameParameter.UnitInstanceTypes.VersusDraftEnemyUnit4:
      case GameParameter.UnitInstanceTypes.VersusDraftEnemyUnit5:
        if (VersusDraftList.VersusDraftUnitDataListEnemy != null)
        {
          int index2 = (int) (InstanceType - 27);
          if (VersusDraftList.VersusDraftUnitDataListEnemy.Count > index2)
          {
            unitData = VersusDraftList.VersusDraftUnitDataListEnemy[index2];
            break;
          }
          break;
        }
        break;
    }
    return unitData;
  }

  public static void GetInstanceData(this GameParameter.ItemInstanceTypes instanceType, int index, GameObject gameObject, out ItemParam itemParam, out int itemNum)
  {
    switch (instanceType)
    {
      case GameParameter.ItemInstanceTypes.Any:
        ItemData dataOfClass1 = DataSource.FindDataOfClass<ItemData>(gameObject, (ItemData) null);
        if (dataOfClass1 != null)
        {
          itemParam = dataOfClass1.Param;
          itemNum = dataOfClass1.Num;
          return;
        }
        itemParam = DataSource.FindDataOfClass<ItemParam>(gameObject, (ItemParam) null);
        itemNum = 0;
        return;
      case GameParameter.ItemInstanceTypes.Inventory:
        PlayerData player = MonoSingleton<GameManager>.Instance.Player;
        if (0 <= index && index < player.Inventory.Length && player.Inventory[index] != null)
        {
          itemParam = player.Inventory[index].Param;
          itemNum = player.Inventory[index].Num;
          return;
        }
        break;
      case GameParameter.ItemInstanceTypes.QuestReward:
        if ((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null)
        {
          QuestParam questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID) ?? DataSource.FindDataOfClass<QuestParam>(gameObject, (QuestParam) null);
          if (questParam != null && 0 <= index && (questParam.bonusObjective != null && index < questParam.bonusObjective.Length))
          {
            itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(questParam.bonusObjective[index].item);
            itemNum = questParam.bonusObjective[index].itemNum;
            return;
          }
          break;
        }
        break;
      case GameParameter.ItemInstanceTypes.Equipment:
        EquipData dataOfClass2 = DataSource.FindDataOfClass<EquipData>(gameObject, (EquipData) null);
        if (dataOfClass2 != null)
        {
          itemParam = dataOfClass2.ItemParam;
          itemNum = 0;
          return;
        }
        break;
      case GameParameter.ItemInstanceTypes.EnhanceMaterial:
        EnhanceMaterial dataOfClass3 = DataSource.FindDataOfClass<EnhanceMaterial>(gameObject, (EnhanceMaterial) null);
        if (dataOfClass3 != null && dataOfClass3.item != null)
        {
          itemParam = dataOfClass3.item.Param;
          itemNum = dataOfClass3.item.Num;
          return;
        }
        break;
      case GameParameter.ItemInstanceTypes.EnhanceEquipData:
        EnhanceEquipData dataOfClass4 = DataSource.FindDataOfClass<EnhanceEquipData>(gameObject, (EnhanceEquipData) null);
        if (dataOfClass4 != null && dataOfClass4.equip != null)
        {
          itemParam = dataOfClass4.equip.ItemParam;
          itemNum = 0;
          return;
        }
        break;
      case GameParameter.ItemInstanceTypes.SellItem:
        SellItem dataOfClass5 = DataSource.FindDataOfClass<SellItem>(gameObject, (SellItem) null);
        if (dataOfClass5 != null && dataOfClass5.item != null)
        {
          itemParam = dataOfClass5.item.Param;
          itemNum = dataOfClass5.num;
          return;
        }
        break;
    }
    itemParam = (ItemParam) null;
    itemNum = 0;
  }

  public static string GetPath(this GameObject go, GameObject root = null)
  {
    StringBuilder stringBuilder = GameUtility.GetStringBuilder();
    if ((UnityEngine.Object) root == (UnityEngine.Object) null)
    {
      for (Transform transform = go.transform; (UnityEngine.Object) transform != (UnityEngine.Object) null; transform = transform.parent)
      {
        stringBuilder.Insert(0, transform.gameObject.name);
        stringBuilder.Insert(0, '/');
      }
    }
    else
    {
      for (Transform transform = go.transform; (UnityEngine.Object) transform != (UnityEngine.Object) null && (UnityEngine.Object) transform.gameObject != (UnityEngine.Object) root; transform = transform.parent)
      {
        stringBuilder.Insert(0, transform.gameObject.name);
        stringBuilder.Insert(0, '/');
      }
    }
    return stringBuilder.ToString();
  }

  public static T RequireComponent<T>(this Component component) where T : Component
  {
    Component component1 = (Component) component.GetComponent<T>();
    if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
      return (T) component1;
    return component.gameObject.AddComponent<T>();
  }

  public static T RequireComponent<T>(this GameObject go) where T : Component
  {
    Component component = (Component) go.GetComponent<T>();
    if ((UnityEngine.Object) component != (UnityEngine.Object) null)
      return (T) component;
    return go.AddComponent<T>();
  }

  public static void AddClickListener(this Button button, ButtonExt.ButtonClickEvent listener)
  {
    button.gameObject.RequireComponent<ButtonExt>().AddListener(listener);
  }

  public static void RemoveClickListener(this Button button, ButtonExt.ButtonClickEvent listener)
  {
    button.gameObject.RequireComponent<ButtonExt>().RemoveListener(listener);
  }

  public static string ComposeURL(this FlowNode_WebView.URL_Mode URLMode, string URL)
  {
    switch (URLMode)
    {
      case FlowNode_WebView.URL_Mode.APIHost:
        return SRPG.Network.Host + URL;
      case FlowNode_WebView.URL_Mode.SiteHost:
        return SRPG.Network.SiteHost + URL;
      case FlowNode_WebView.URL_Mode.NewsHost:
        return SRPG.Network.NewsHost + URL;
      default:
        return URL;
    }
  }

  public static float Evaluate(this ObjectAnimator.CurveType curve, float t)
  {
    switch (curve)
    {
      case ObjectAnimator.CurveType.EaseIn:
        return 1f - Mathf.Cos((float) ((double) t * 3.14159274101257 * 0.5));
      case ObjectAnimator.CurveType.EaseOut:
        return Mathf.Cos((float) ((1.0 - (double) t) * 3.14159274101257 * 0.5));
      case ObjectAnimator.CurveType.EaseInOut:
        return (float) ((1.0 - (double) Mathf.Cos(t * 3.141593f)) * 0.5);
      default:
        return t;
    }
  }

  public static float ToSpan(this CameraInterpSpeed speed)
  {
    if (speed == CameraInterpSpeed.Immediate)
      return 0.0f;
    return (float) ((double) speed * 0.25 + 0.5);
  }

  public static void SetNormalizedPosition(this ScrollRect scrollRect, Vector2 normalizedPos, bool blockRectSound)
  {
    if (blockRectSound)
    {
      ScrollRectSound component = scrollRect.gameObject.GetComponent<ScrollRectSound>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.Reset();
    }
    scrollRect.normalizedPosition = normalizedPos;
  }

  public static UnlockTargets ToUnlockTargets(this EShopType type)
  {
    switch (type)
    {
      case EShopType.Normal:
        return UnlockTargets.Shop;
      case EShopType.Tabi:
        return UnlockTargets.ShopTabi;
      case EShopType.Kimagure:
        return UnlockTargets.ShopKimagure;
      case EShopType.Monozuki:
        return UnlockTargets.ShopMonozuki;
      case EShopType.Tour:
        return UnlockTargets.Tour;
      case EShopType.Arena:
        return UnlockTargets.Arena;
      case EShopType.Multi:
        return UnlockTargets.MultiPlay;
      case EShopType.AwakePiece:
        return UnlockTargets.ShopAwakePiece;
      case EShopType.Artifact:
        return UnlockTargets.Artifact;
      default:
        return (UnlockTargets) 0;
    }
  }

  public static string GetDescription(this TrophyObjective self)
  {
    GameManager instance = MonoSingleton<GameManager>.Instance;
    string str1 = string.Empty;
    string str2 = string.Empty;
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    char[] chArray = new char[1]{ ',' };
    if (!string.IsNullOrEmpty(self.Param.Expr))
      return string.Format(LocalizedText.Get(self.Param.Expr), (object) self.ival);
    switch (self.type)
    {
      case TrophyConditionTypes.winquest:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_NORMAL"), (object) self.ival);
        if (self.sval.Count == 1)
        {
          QuestParam quest = instance.FindQuest(self.sval_base);
          return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST"), quest == null ? (object) ("?" + (object) self.sval) : (object) quest.name, (object) self.ival);
        }
        string empty3 = string.Empty;
        for (int index = 0; index < self.sval.Count; ++index)
        {
          QuestParam quest = instance.FindQuest(self.sval[index]);
          if (index != 0)
            empty3 += LocalizedText.Get("sys.TROPHY_OR");
          empty3 += quest == null ? "?" + self.sval[index] : quest.name;
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_OR"), (object) empty3, (object) self.ival);
      case TrophyConditionTypes.killenemy:
        UnitParam unitParam1 = instance.GetUnitParam(self.sval_base);
        return string.Format(LocalizedText.Get("sys.TROPHY_KILLENEMY"), unitParam1 == null ? (object) ("?" + self.sval_base) : (object) unitParam1.name, (object) self.ival);
      case TrophyConditionTypes.getitem:
        ItemParam itemParam = instance.GetItemParam(self.sval_base);
        return string.Format(LocalizedText.Get("sys.TROPHY_GETITEM"), itemParam == null ? (object) ("?" + self.sval_base) : (object) itemParam.name, (object) self.ival);
      case TrophyConditionTypes.playerlv:
        return string.Format(LocalizedText.Get("sys.TROPHY_PLAYERLV"), (object) self.ival);
      case TrophyConditionTypes.winelite:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_ELITE"), (object) self.ival);
      case TrophyConditionTypes.winevent:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINQUEST_EVENT"), (object) self.ival);
      case TrophyConditionTypes.gacha:
        return string.Format(LocalizedText.Get("sys.TROPHY_GACHA"), (object) self.ival);
      case TrophyConditionTypes.multiplay:
        return string.Format(LocalizedText.Get("sys.TROPHY_MULTIPLAY"), (object) self.ival);
      case TrophyConditionTypes.ability:
        return string.Format(LocalizedText.Get("sys.TROPHY_ABILITY"), (object) self.ival);
      case TrophyConditionTypes.soubi:
        return string.Format(LocalizedText.Get("sys.TROPHY_SOUBI"), (object) self.ival);
      case TrophyConditionTypes.buygold:
        return string.Format(LocalizedText.Get("sys.TROPHY_BUYGOLD"), (object) self.ival);
      case TrophyConditionTypes.vip:
        return string.Format(LocalizedText.Get("sys.TROPHY_VIP"), (object) self.ival);
      case TrophyConditionTypes.stamina:
        return string.Format(LocalizedText.Get("sys.TROPHY_STAMINA"), (object) int.Parse(self.sval_base.Substring(0, 2)), (object) int.Parse(self.sval_base.Substring(3, 2)));
      case TrophyConditionTypes.arena:
        return string.Format(LocalizedText.Get("sys.TROPHY_ARENA"), (object) self.ival);
      case TrophyConditionTypes.winarena:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINARENA"), (object) self.ival);
      case TrophyConditionTypes.review:
        return LocalizedText.Get("sys.TROPHY_REVIEW");
      case TrophyConditionTypes.followtwitter:
        return LocalizedText.Get("sys.TROPHY_FOLLOWTWITTER");
      case TrophyConditionTypes.fggid:
        return LocalizedText.Get("sys.TROPHY_FGGID");
      case TrophyConditionTypes.unitlevel:
        UnitParam unitParam2 = instance.GetUnitParam(self.sval_base);
        return string.Format(LocalizedText.Get("sys.TROPHY_UNITLV"), unitParam2 == null ? (object) ("?" + self.sval_base) : (object) unitParam2.name, (object) self.ival);
      case TrophyConditionTypes.evolutionnum:
        UnitParam unitParam3 = instance.GetUnitParam(self.sval_base);
        return string.Format(LocalizedText.Get("sys.TROPHY_EVOLUTIONCNT"), unitParam3 == null ? (object) ("?" + self.sval_base) : (object) unitParam3.name, (object) (self.ival + 1));
      case TrophyConditionTypes.joblevel:
        string[] strArray1 = self.sval_base.Split(chArray);
        UnitParam unitParam4 = instance.GetUnitParam(strArray1[0]);
        JobParam jobParam1 = instance.GetJobParam(strArray1[1]);
        return string.Format(LocalizedText.Get("sys.TROPHY_JOBLV"), unitParam4 == null ? (object) ("?" + strArray1[0]) : (object) unitParam4.name, jobParam1 == null ? (object) ("?" + strArray1[1]) : (object) jobParam1.name, (object) self.ival);
      case TrophyConditionTypes.logincount:
        return string.Format(LocalizedText.Get("sys.TROPHY_LOGINCNT"), (object) self.ival);
      case TrophyConditionTypes.upunitlevel:
        return string.Format(LocalizedText.Get("sys.TROPHY_UNITLVUP"), (object) SRPG_Extensions.GetUnitName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.makeunitlevel:
        return string.Format(LocalizedText.Get("sys.TROPHY_UNITLVMAKE"), (object) SRPG_Extensions.GetUnitName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.unitequip:
        return string.Format(LocalizedText.Get("sys.TROPHY_EQUIP"), (object) SRPG_Extensions.GetUnitName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.upjoblevel:
        if (!string.IsNullOrEmpty(self.sval_base))
        {
          string[] strArray2 = self.sval_base.Split(chArray);
          UnitParam unitParam5 = instance.GetUnitParam(strArray2[0]);
          JobParam jobParam2 = instance.GetJobParam(strArray2[1]);
          str2 = unitParam5 == null ? "?" + strArray2[0] : unitParam5.name;
          str1 = jobParam2 == null ? "?" + strArray2[1] : jobParam2.name;
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVUP"), (object) str2, (object) str1, (object) self.ival);
      case TrophyConditionTypes.makejoblevel:
        if (!string.IsNullOrEmpty(self.sval_base))
        {
          string[] strArray2 = self.sval_base.Split(chArray);
          UnitParam unitParam5 = instance.GetUnitParam(strArray2[0]);
          JobParam jobParam2 = instance.GetJobParam(strArray2[1]);
          str2 = unitParam5 == null ? "?" + strArray2[0] : unitParam5.name;
          str1 = jobParam2 == null ? "?" + strArray2[1] : jobParam2.name;
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_JOBLVMAKE"), (object) str2, (object) str1, (object) self.ival);
      case TrophyConditionTypes.limitbreak:
        return string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAK"), (object) SRPG_Extensions.GetUnitName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.evoltiontimes:
        return string.Format(LocalizedText.Get("sys.TROPHY_EVOLUTIONTIMES"), (object) SRPG_Extensions.GetUnitName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.changejob:
        return string.Format(LocalizedText.Get("sys.TROPHY_CHANGEJOB"), (object) SRPG_Extensions.GetUnitName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.changeability:
        return string.Format(LocalizedText.Get("sys.TROPHY_CHANGEABILITY"), (object) SRPG_Extensions.GetUnitName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.makeabilitylevel:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_ABILITYLV"), (object) string.Empty, (object) string.Empty, (object) self.ival);
        string[] strArray3 = self.sval_base.Split(chArray);
        string str3 = string.Empty;
        if (!string.IsNullOrEmpty(strArray3[1]))
        {
          AbilityParam abilityParam = instance.GetAbilityParam(strArray3[1]);
          str3 = abilityParam == null ? "?" + strArray3[1] : abilityParam.name;
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_ABILITYLV"), (object) SRPG_Extensions.GetUnitName(strArray3[0]), (object) str3, (object) self.ival);
      case TrophyConditionTypes.winquestsoldier:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINSOLIDER"), (object) self.ival);
      case TrophyConditionTypes.winmulti:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTI"), (object) SRPG_Extensions.GetQuestName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.buyatshop:
        string str4 = string.Empty;
        string str5 = string.Empty;
        if (!string.IsNullOrEmpty(self.sval_base))
        {
          string[] strArray2 = self.sval_base.Split(chArray);
          if (!string.IsNullOrEmpty(strArray2[0]))
          {
            int shopType = instance.MasterParam.GetShopType(strArray2[0]);
            str4 = instance.Player.GetShopName((EShopType) shopType);
          }
          str5 = SRPG_Extensions.GetItemName(strArray2[1]);
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_BUYATSHOP"), (object) str4, (object) str5, (object) self.ival);
      case TrophyConditionTypes.artifacttransmute:
        return string.Format(LocalizedText.Get("sys.TROPHY_AFORDRILL"), (object) SRPG_Extensions.GetArtifactName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.artifactstrength:
        return string.Format(LocalizedText.Get("sys.TROPHY_AFSTRENGTHEN"), (object) SRPG_Extensions.GetArtifactName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.artifactevolution:
        return string.Format(LocalizedText.Get("sys.TROPHY_AFVOLUTION"), (object) SRPG_Extensions.GetArtifactName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.winmultimore:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTIMORE"), (object) SRPG_Extensions.GetQuestName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.winmultiless:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINMULTILESS"), (object) SRPG_Extensions.GetQuestName(self.sval_base), (object) self.ival);
      case TrophyConditionTypes.collectunits:
        return string.Format(LocalizedText.Get("sys.TROPHY_COLLECTUNITS"), (object) self.ival);
      case TrophyConditionTypes.totaljoblv11:
        return string.Format(LocalizedText.Get("sys.TROPHY_TOTALJOBLV11"), (object) self.ival);
      case TrophyConditionTypes.totalunitlvs:
        return string.Format(LocalizedText.Get("sys.TROPHY_TOTALUNITLVS"), (object) self.ival);
      case TrophyConditionTypes.childrencomp:
        return string.Format(LocalizedText.Get("sys.TROPHY_CHILDRENCOMP"), (object) self.ival);
      case TrophyConditionTypes.wintower:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_WINTOWER_NORMAL"), (object) self.ival);
        QuestParam quest1 = instance.FindQuest(self.sval_base);
        if (quest1 != null)
          return string.Format(LocalizedText.Get("sys.TROPHY_WINTOWER"), (object) quest1.title, (object) quest1.name, (object) self.ival);
        DebugUtility.Log("「" + (object) self.sval + "」quest_id is not found.");
        return string.Empty;
      case TrophyConditionTypes.losequest:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_NORMAL"), (object) self.ival);
        if (self.sval.Count == 1)
        {
          QuestParam quest2 = instance.FindQuest(self.sval_base);
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST"), quest2 == null ? (object) ("?" + self.sval_base) : (object) quest2.name, (object) self.ival);
        }
        string empty4 = string.Empty;
        for (int index = 0; index < self.sval.Count; ++index)
        {
          QuestParam quest2 = instance.FindQuest(self.sval[index]);
          if (index != 0)
            empty4 += LocalizedText.Get("sys.TROPHY_OR");
          empty4 += quest2 == null ? "?" + self.sval[index] : quest2.name;
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_OR"), (object) empty4, (object) self.ival);
      case TrophyConditionTypes.loseelite:
        return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_ELITE"), (object) self.ival);
      case TrophyConditionTypes.loseevent:
        return string.Format(LocalizedText.Get("sys.TROPHY_LOSEQUEST_EVENT"), (object) self.ival);
      case TrophyConditionTypes.losetower:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSETOWER_NORMAL"), (object) self.ival);
        QuestParam quest3 = instance.FindQuest(self.sval_base);
        if (quest3 != null)
          return string.Format(LocalizedText.Get("sys.TROPHY_LOSETOWER"), (object) quest3.title, (object) quest3.name, (object) self.ival);
        DebugUtility.Log("「" + self.sval_base + "」quest_id is not found.");
        return string.Empty;
      case TrophyConditionTypes.losearena:
        return string.Format(LocalizedText.Get("sys.TROPHY_LOSEARENA"), (object) self.ival);
      case TrophyConditionTypes.dailyall:
        return string.Format(LocalizedText.Get("sys.TROPHY_DAILYALL"), (object) self.ival);
      case TrophyConditionTypes.vs:
        return string.Format(LocalizedText.Get("sys.TROPHY_VS"), (object) self.ival);
      case TrophyConditionTypes.vswin:
        return string.Format(LocalizedText.Get("sys.TROPHY_VSWIN"), (object) self.ival);
      case TrophyConditionTypes.vslose:
        return string.Format(LocalizedText.Get("sys.TROPHY_VSLOSE"), (object) self.ival);
      case TrophyConditionTypes.exclear_fire:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_FIRE"), (object) self.ival);
      case TrophyConditionTypes.exclear_water:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_WATER"), (object) self.ival);
      case TrophyConditionTypes.exclear_wind:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_WIND"), (object) self.ival);
      case TrophyConditionTypes.exclear_thunder:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_THUNDER"), (object) self.ival);
      case TrophyConditionTypes.exclear_light:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_SHINE"), (object) self.ival);
      case TrophyConditionTypes.exclear_dark:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR"), (object) LocalizedText.Get("sys.PARAM_ASSIST_DARK"), (object) self.ival);
      case TrophyConditionTypes.exclear_fire_nocon:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_FIRE"), (object) self.ival);
      case TrophyConditionTypes.exclear_water_nocon:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_WATER"), (object) self.ival);
      case TrophyConditionTypes.exclear_wind_nocon:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_WIND"), (object) self.ival);
      case TrophyConditionTypes.exclear_thunder_nocon:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_THUNDER"), (object) self.ival);
      case TrophyConditionTypes.exclear_light_nocon:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_SHINE"), (object) self.ival);
      case TrophyConditionTypes.exclear_dark_nocon:
        return string.Format(LocalizedText.Get("sys.TROPHY_EXTRA_CLEAR_NOCON"), (object) LocalizedText.Get("sys.PARAM_ASSIST_DARK"), (object) self.ival);
      case TrophyConditionTypes.winstory_extra:
        return string.Format(LocalizedText.Get("sys.TROPHY_WINSTORY_EXTRA"), (object) self.ival);
      case TrophyConditionTypes.multitower_help:
        return string.Format(LocalizedText.Get("sys.TROPHY_MULTITOWER_HELP"), (object) self.ival);
      case TrophyConditionTypes.multitower:
        return string.Format(LocalizedText.Get("sys.TROPHY_MULTITOWER"), (object) self.ival);
      case TrophyConditionTypes.damage_over:
        return string.Format(LocalizedText.Get("sys.TROPHY_DAMAGE_OVER"), (object) self.ival);
      case TrophyConditionTypes.complete_all_quest_mission:
        return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_ALL_QUESTMISSION"), (object) SRPG_Extensions.GetQuestName(self.sval_base));
      case TrophyConditionTypes.has_gold_over:
        return string.Format(LocalizedText.Get("sys.TROPHY_HAS_GOLD_OVER"), (object) self.ival);
      case TrophyConditionTypes.up_conceptcard_level:
        return string.Format(LocalizedText.Get("sys.TROPHY_LEVELUP_CONCEPTCARD"), (object) self.ival);
      case TrophyConditionTypes.up_conceptcard_level_target:
        if (string.IsNullOrEmpty(self.sval_base))
        {
          DebugUtility.LogError(self.Param.iname + "には念装の指定がありません。");
          return string.Empty;
        }
        ConceptCardParam conceptCardParam1 = MonoSingleton<GameManager>.Instance.GetConceptCardParam(self.sval_base);
        string str6 = string.Empty;
        if (conceptCardParam1 != null)
          str6 = conceptCardParam1.name;
        else
          DebugUtility.LogError("真理念装「" + self.sval_base + "」は存在しません.");
        return string.Format(LocalizedText.Get("sys.TROPHY_LEVELUP_TARGET_CONCEPTCARD"), (object) str6, (object) self.ival);
      case TrophyConditionTypes.limitbreak_conceptcard:
        return string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAKE_CONCEPTCARD"), (object) self.ival);
      case TrophyConditionTypes.limitbreak_conceptcard_target:
        if (string.IsNullOrEmpty(self.sval_base))
        {
          DebugUtility.LogError(self.Param.iname + "には念装の指定がありません。");
          return string.Empty;
        }
        ConceptCardParam conceptCardParam2 = MonoSingleton<GameManager>.Instance.GetConceptCardParam(self.sval_base);
        string str7 = string.Empty;
        if (conceptCardParam2 != null)
          str7 = conceptCardParam2.name;
        else
          DebugUtility.LogError("真理念装「" + self.sval_base + "」は存在しません.");
        return string.Format(LocalizedText.Get("sys.TROPHY_LIMITBREAKE_TARGET_CONCEPTCARD"), (object) str7, (object) self.ival);
      case TrophyConditionTypes.up_conceptcard_trust:
        return string.Format(LocalizedText.Get("sys.TROPHY_TRUSTUP_CONCEPTCARD"), (object) string.Format("{0:0.0}", (object) (float) ((double) self.ival / 100.0)));
      case TrophyConditionTypes.up_conceptcard_trust_target:
        if (string.IsNullOrEmpty(self.sval_base))
        {
          DebugUtility.LogError(self.Param.iname + "には念装の指定がありません。");
          return string.Empty;
        }
        ConceptCardParam conceptCardParam3 = MonoSingleton<GameManager>.Instance.GetConceptCardParam(self.sval_base);
        string str8 = string.Empty;
        if (conceptCardParam3 != null)
          str8 = conceptCardParam3.name;
        else
          DebugUtility.LogError("真理念装「" + self.sval_base + "」は存在しません.");
        return string.Format(LocalizedText.Get("sys.TROPHY_TRUSTUP_TARGET_CONCEPTCARD"), (object) str8, (object) string.Format("{0:0.0}", (object) (float) ((double) self.ival / 100.0)));
      case TrophyConditionTypes.max_conceptcard_trust:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_MAX_TRUST_CONCEPTCARD"));
        ConceptCardParam conceptCardParam4 = MonoSingleton<GameManager>.Instance.GetConceptCardParam(self.sval_base);
        string str9 = string.Empty;
        if (conceptCardParam4 != null)
          str9 = conceptCardParam4.name;
        else
          DebugUtility.LogError("真理念装「" + self.sval_base + "」は存在しません.");
        return string.Format(LocalizedText.Get("sys.TROPHY_MAX_TRUST_TARGET_CONCEPTCARD"), (object) str9);
      case TrophyConditionTypes.unlock_tobira_total:
        return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_TOTAL"), (object) self.ival);
      case TrophyConditionTypes.unlock_tobira_unit:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Empty;
        return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_UNIT_TARGET"), (object) SRPG_Extensions.GetUnitName(self.sval_base));
      case TrophyConditionTypes.envy_unlock_unit:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_ENVY"));
        return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_UNIT_TARGET"), (object) SRPG_Extensions.GetUnitName(self.sval_base));
      case TrophyConditionTypes.sloth_unlock_unit:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_SLOTH"));
        return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_SLOTH_TARGET"), (object) SRPG_Extensions.GetUnitName(self.sval_base));
      case TrophyConditionTypes.lust_unlock_unit:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_LUST"));
        return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_LUST_TARGET"), (object) SRPG_Extensions.GetUnitName(self.sval_base));
      case TrophyConditionTypes.greed_unlock_unit:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GREED"));
        return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GREED_TARGET"), (object) SRPG_Extensions.GetUnitName(self.sval_base));
      case TrophyConditionTypes.wrath_unlock_unit:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_WRATH"));
        return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_WRATH_TARGET"), (object) SRPG_Extensions.GetUnitName(self.sval_base));
      case TrophyConditionTypes.gluttonny_unlock_unit:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GLUTTONNY"));
        return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_GLUTTONNY_TARGET"), (object) SRPG_Extensions.GetUnitName(self.sval_base));
      case TrophyConditionTypes.pride_unlock_unit:
        if (string.IsNullOrEmpty(self.sval_base))
          return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_PRIDE"));
        return string.Format(LocalizedText.Get("sys.TROPHY_UNLOCK_TOBIRA_PRIDE_TARGET"), (object) SRPG_Extensions.GetUnitName(self.sval_base));
      case TrophyConditionTypes.send_present:
        return string.Format(LocalizedText.Get("sys.TROPHY_SEND_PRESENT"), (object) self.ival);
      case TrophyConditionTypes.complete_all_quest_mission_total:
        if (!string.IsNullOrEmpty(self.sval_base))
        {
          QuestParam quest2 = instance.FindQuest(self.sval_base);
          empty2 += quest2.name;
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_MISSION_ALL", new object[1]{ (object) empty2 }));
      case TrophyConditionTypes.complete_all_mission_count:
        return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_ALL_MISSION_COUNT", new object[1]{ (object) self.ival }));
      case TrophyConditionTypes.complete_story_mission_count:
        if (self.sval != null && self.sval.Count > 0)
          return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_COUNT_ORDER", (object) SRPG_Extensions.GetAreaList(self.sval), (object) self.ival));
        return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_STORY_MISSION_COUNT", new object[1]{ (object) self.ival }));
      case TrophyConditionTypes.complete_event_mission_count:
        if (self.sval != null && self.sval.Count > 0)
          return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_COUNT_ORDER", (object) SRPG_Extensions.GetAreaList(self.sval), (object) self.ival));
        return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_EVENT_COUNT", new object[1]{ (object) self.ival }));
      case TrophyConditionTypes.complete_ordeal_mission_count:
        if (self.sval != null && self.sval.Count > 0)
          return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_COUNT_ORDER", (object) SRPG_Extensions.GetAreaList(self.sval), (object) self.ival));
        return string.Format(LocalizedText.Get("sys.TROPHY_COMPLETE_ORDEAL_COUNT", new object[1]{ (object) self.ival }));
      case TrophyConditionTypes.clear_ordeal:
        if (!string.IsNullOrEmpty(self.sval_base))
        {
          QuestParam quest2 = instance.FindQuest(self.sval_base);
          if (quest2 != null)
          {
            empty2 += quest2.name;
          }
          else
          {
            empty2 = string.Empty;
            DebugUtility.LogError("トロフィー「" + self.Param.iname + "」に指定されたクエストが存在しません");
          }
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_CLEAR_ORDEAL", (object) empty2, (object) self.ival));
      case TrophyConditionTypes.view_news:
        return string.Format(LocalizedText.Get("sys.TROPHY_VIEW_ANNOUNCEMENT"));
      case TrophyConditionTypes.makeunitandjoblevel:
        int result1 = 0;
        int result2 = 0;
        if (!string.IsNullOrEmpty(self.sval_base))
        {
          string[] strArray2 = self.sval_base.Split(chArray);
          if (strArray2.Length > 1)
          {
            str2 = SRPG_Extensions.GetUnitName(strArray2[0]);
            int.TryParse(strArray2[1], out result1);
          }
          if (strArray2.Length > 3)
          {
            JobParam jobParam2 = instance.GetJobParam(strArray2[2]);
            str1 = jobParam2 == null ? "?" + strArray2[2] : jobParam2.name;
            int.TryParse(strArray2[3], out result2);
          }
        }
        return string.Format(LocalizedText.Get("sys.TROPHY_UNITANDJOBLVMAKE"), (object) str2, (object) result1, (object) str1, (object) result2);
      default:
        return string.Empty;
    }
  }

  private static string GetAreaList(List<string> svals)
  {
    bool flag = true;
    string empty = string.Empty;
    foreach (string sval in svals)
    {
      ChapterParam area = MonoSingleton<GameManager>.Instance.FindArea(sval);
      if (area != null)
      {
        if (!flag)
          empty += ",";
        else
          flag = false;
        empty += area.name;
      }
    }
    return empty;
  }

  private static string GetUnitName(string unitid)
  {
    string str = string.Empty;
    if (!string.IsNullOrEmpty(unitid))
    {
      UnitParam unitParam = MonoSingleton<GameManager>.Instance.GetUnitParam(unitid);
      str = unitParam == null ? "?" + unitid : unitParam.name;
    }
    return str;
  }

  private static string GetItemName(string itemid)
  {
    string str = string.Empty;
    if (!string.IsNullOrEmpty(itemid))
    {
      ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(itemid);
      str = itemParam == null ? "?" + itemid : itemParam.name;
    }
    return str;
  }

  private static string GetArtifactName(string itemid)
  {
    string str = string.Empty;
    if (!string.IsNullOrEmpty(itemid))
    {
      ArtifactParam artifactParam = MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(itemid);
      str = artifactParam == null ? "?" + itemid : artifactParam.name;
    }
    return str;
  }

  private static string GetQuestName(string questid)
  {
    string str = string.Empty;
    if (!string.IsNullOrEmpty(questid))
    {
      QuestParam quest = MonoSingleton<GameManager>.Instance.FindQuest(questid);
      str = quest == null ? "?" + questid : quest.name;
    }
    return str;
  }

  public static float ToFloat(this EventAction_Dialog.TextSpeedTypes speed)
  {
    switch (speed)
    {
      case EventAction_Dialog.TextSpeedTypes.Slow:
        return 0.2f;
      case EventAction_Dialog.TextSpeedTypes.Fast:
        return 0.01f;
      default:
        return 0.05f;
    }
  }

  public static float ToFloat(this Event2dAction_Dialog.TextSpeedTypes speed)
  {
    switch (speed)
    {
      case Event2dAction_Dialog.TextSpeedTypes.Slow:
        return 0.2f;
      case Event2dAction_Dialog.TextSpeedTypes.Fast:
        return 0.01f;
      default:
        return 0.05f;
    }
  }

  public static float ToFloat(this Event2dAction_Dialog2.TextSpeedTypes speed)
  {
    switch (speed)
    {
      case Event2dAction_Dialog2.TextSpeedTypes.Slow:
        return 0.2f;
      case Event2dAction_Dialog2.TextSpeedTypes.Fast:
        return 0.01f;
      default:
        return 0.05f;
    }
  }

  public static float ToFloat(this Event2dAction_Dialog3.TextSpeedTypes speed)
  {
    switch (speed)
    {
      case Event2dAction_Dialog3.TextSpeedTypes.Slow:
        return 0.2f;
      case Event2dAction_Dialog3.TextSpeedTypes.Fast:
        return 0.01f;
      default:
        return 0.05f;
    }
  }

  public static float ToFloat(this Event2dAction_Telop.TextSpeedTypes speed)
  {
    switch (speed)
    {
      case Event2dAction_Telop.TextSpeedTypes.Slow:
        return 0.2f;
      case Event2dAction_Telop.TextSpeedTypes.Fast:
        return 0.01f;
      default:
        return 0.05f;
    }
  }

  public static int ToYMD(this DateTime date)
  {
    return date.Year % 100 * 10000 + date.Month % 100 * 100 + date.Day % 100;
  }

  public static DateTime FromYMD(this int ymd)
  {
    return new DateTime(ymd / 10000 + 2000, ymd / 100 % 100, ymd % 100);
  }

  public static string ToPrefix(this ESex sex)
  {
    return SRPG_Extensions.mPrefixes[(int) sex];
  }

  public static string ToColorValue(this Color32 src)
  {
    return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", (object) src.r, (object) src.g, (object) src.b, (object) src.a);
  }

  public static int GetMaxTeamCount(this PartyWindow2.EditPartyTypes type)
  {
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    if ((UnityEngine.Object) instanceDirect == (UnityEngine.Object) null)
    {
      switch (type)
      {
        case PartyWindow2.EditPartyTypes.Normal:
        case PartyWindow2.EditPartyTypes.Event:
          return 5;
        case PartyWindow2.EditPartyTypes.Tower:
          return 6;
        default:
          return 1;
      }
    }
    else
    {
      FixParam fixParam = instanceDirect.MasterParam.FixParam;
      switch (type)
      {
        case PartyWindow2.EditPartyTypes.Normal:
          return fixParam.PartyNumNormal;
        case PartyWindow2.EditPartyTypes.Event:
          return fixParam.PartyNumEvent;
        case PartyWindow2.EditPartyTypes.MP:
          return fixParam.PartyNumMulti;
        case PartyWindow2.EditPartyTypes.Arena:
          return fixParam.PartyNumArenaAttack;
        case PartyWindow2.EditPartyTypes.ArenaDef:
          return fixParam.PartyNumArenaDefense;
        case PartyWindow2.EditPartyTypes.Character:
          return fixParam.PartyNumChQuest;
        case PartyWindow2.EditPartyTypes.Tower:
          return fixParam.PartyNumTower;
        case PartyWindow2.EditPartyTypes.MultiTower:
          return fixParam.PartyNumMultiTower;
        case PartyWindow2.EditPartyTypes.Ordeal:
          return fixParam.PartyNumOrdeal;
        case PartyWindow2.EditPartyTypes.Raid:
          return fixParam.PartyNumRaid;
        default:
          return 1;
      }
    }
  }

  public static PlayerPartyTypes ToPlayerPartyType(this PartyWindow2.EditPartyTypes type)
  {
    switch (type)
    {
      case PartyWindow2.EditPartyTypes.Normal:
        return PlayerPartyTypes.Normal;
      case PartyWindow2.EditPartyTypes.Event:
        return PlayerPartyTypes.Event;
      case PartyWindow2.EditPartyTypes.MP:
        return PlayerPartyTypes.Multiplay;
      case PartyWindow2.EditPartyTypes.Arena:
        return PlayerPartyTypes.Arena;
      case PartyWindow2.EditPartyTypes.ArenaDef:
        return PlayerPartyTypes.ArenaDef;
      case PartyWindow2.EditPartyTypes.Character:
        return PlayerPartyTypes.Character;
      case PartyWindow2.EditPartyTypes.Tower:
        return PlayerPartyTypes.Tower;
      case PartyWindow2.EditPartyTypes.Versus:
        return PlayerPartyTypes.Versus;
      case PartyWindow2.EditPartyTypes.MultiTower:
        return PlayerPartyTypes.MultiTower;
      case PartyWindow2.EditPartyTypes.Ordeal:
        return PlayerPartyTypes.Ordeal;
      case PartyWindow2.EditPartyTypes.RankMatch:
        return PlayerPartyTypes.RankMatch;
      case PartyWindow2.EditPartyTypes.Raid:
        return PlayerPartyTypes.Raid;
      default:
        throw new InvalidPartyTypeException();
    }
  }

  public static PartyWindow2.EditPartyTypes ToEditPartyType(this PlayerPartyTypes type)
  {
    switch (type)
    {
      case PlayerPartyTypes.Normal:
        return PartyWindow2.EditPartyTypes.Normal;
      case PlayerPartyTypes.Event:
        return PartyWindow2.EditPartyTypes.Event;
      case PlayerPartyTypes.Multiplay:
        return PartyWindow2.EditPartyTypes.MP;
      case PlayerPartyTypes.Arena:
        return PartyWindow2.EditPartyTypes.Arena;
      case PlayerPartyTypes.ArenaDef:
        return PartyWindow2.EditPartyTypes.ArenaDef;
      case PlayerPartyTypes.Character:
        return PartyWindow2.EditPartyTypes.Character;
      case PlayerPartyTypes.Tower:
        return PartyWindow2.EditPartyTypes.Tower;
      case PlayerPartyTypes.Versus:
        return PartyWindow2.EditPartyTypes.Versus;
      case PlayerPartyTypes.MultiTower:
        return PartyWindow2.EditPartyTypes.MultiTower;
      case PlayerPartyTypes.Ordeal:
        return PartyWindow2.EditPartyTypes.Ordeal;
      case PlayerPartyTypes.RankMatch:
        return PartyWindow2.EditPartyTypes.RankMatch;
      case PlayerPartyTypes.Raid:
        return PartyWindow2.EditPartyTypes.Raid;
      default:
        throw new InvalidPartyTypeException();
    }
  }

  public static void SetText(this InputField inputField, string value)
  {
    if (inputField is SRPG_InputField)
      (inputField as SRPG_InputField).ForceSetText(value);
    else
      inputField.text = value;
  }
}
