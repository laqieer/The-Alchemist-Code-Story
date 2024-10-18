// Decompiled with JetBrains decompiler
// Type: SRPG_Extensions
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using SRPG;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
public static class SRPG_Extensions
{
  private static readonly string[] mPrefixes = new string[3]
  {
    "U_",
    "M_",
    "F_"
  };

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
    return (double) ((Rect) ref rect).yMin >= (double) ((Rect) ref other).yMin && (double) ((Rect) ref rect).yMax >= (double) ((Rect) ref other).yMax && (double) ((Rect) ref rect).xMin >= (double) ((Rect) ref other).xMin && (double) ((Rect) ref rect).xMax >= (double) ((Rect) ref other).xMax;
  }

  public static UnitData GetInstanceData(
    this GameParameter.UnitInstanceTypes InstanceType,
    GameObject gameObject)
  {
    UnitData instanceData = (UnitData) null;
    switch (InstanceType)
    {
      case GameParameter.UnitInstanceTypes.Any:
        instanceData = DataSource.FindDataOfClass<UnitData>(gameObject, (UnitData) null);
        if (instanceData == null)
        {
          Unit dataOfClass = DataSource.FindDataOfClass<Unit>(gameObject, (Unit) null);
          if (dataOfClass != null)
          {
            instanceData = dataOfClass.UnitData;
            break;
          }
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.CurrentTurn:
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null) && SceneBattle.Instance.Battle.CurrentUnit != null)
          return SceneBattle.Instance.Battle.CurrentUnit.UnitData;
        break;
      case GameParameter.UnitInstanceTypes.ArenaPlayerUnit0:
      case GameParameter.UnitInstanceTypes.ArenaPlayerUnit1:
      case GameParameter.UnitInstanceTypes.ArenaPlayerUnit2:
        ArenaPlayer dataOfClass1 = DataSource.FindDataOfClass<ArenaPlayer>(gameObject, (ArenaPlayer) null);
        if (dataOfClass1 != null)
        {
          int index = (int) (InstanceType - 4);
          instanceData = UnitOverWriteUtility.Apply(dataOfClass1.Unit[index], eOverWritePartyType.Arena);
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
          instanceData = selectedArenaPlayer.Unit[index];
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.PartyUnit0:
      case GameParameter.UnitInstanceTypes.PartyUnit1:
      case GameParameter.UnitInstanceTypes.PartyUnit2:
        PlayerData player1 = MonoSingleton<GameManager>.Instance.Player;
        int index1 = (int) (InstanceType - 10);
        long unitUniqueId1 = player1.Partys[(int) GlobalVars.SelectedPartyIndex].GetUnitUniqueID(index1);
        instanceData = player1.FindUnitDataByUniqueID(unitUniqueId1);
        break;
      case GameParameter.UnitInstanceTypes.VersusUnit:
        PlayerData player2 = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId2 = player2.Partys[7].GetUnitUniqueID(0);
        instanceData = player2.FindUnitDataByUniqueID(unitUniqueId2);
        break;
      case GameParameter.UnitInstanceTypes.MultiUnit:
        PlayerData player3 = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId3 = player3.Partys[2].GetUnitUniqueID(0);
        instanceData = player3.FindUnitDataByUniqueID(unitUniqueId3);
        break;
      case GameParameter.UnitInstanceTypes.MultiTowerUnit:
        PlayerData player4 = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId4 = player4.Partys[8].GetUnitUniqueID(0);
        instanceData = player4.FindUnitDataByUniqueID(unitUniqueId4);
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
            instanceData = dataOfClass2.Units[index2];
            break;
          }
          break;
        }
        break;
      case GameParameter.UnitInstanceTypes.RankMatchUnit:
        PlayerData player5 = MonoSingleton<GameManager>.Instance.Player;
        long unitUniqueId5 = player5.Partys[10].GetUnitUniqueID(0);
        instanceData = player5.FindUnitDataByUniqueID(unitUniqueId5);
        break;
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit0:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit1:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit2:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit3:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit4:
      case GameParameter.UnitInstanceTypes.VersusDraftPlayerUnit5:
        if (VersusDraftList.VersusDraftUnitDataListPlayer != null)
        {
          int index3 = (int) (InstanceType - 21);
          if (VersusDraftList.VersusDraftUnitDataListPlayer.Count > index3)
          {
            instanceData = VersusDraftList.VersusDraftUnitDataListPlayer[index3];
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
          int index4 = (int) (InstanceType - 27);
          if (VersusDraftList.VersusDraftUnitDataListEnemy.Count > index4)
          {
            instanceData = VersusDraftList.VersusDraftUnitDataListEnemy[index4];
            break;
          }
          break;
        }
        break;
    }
    return instanceData;
  }

  public static void GetInstanceData(
    this GameParameter.ItemInstanceTypes instanceType,
    int index,
    GameObject gameObject,
    out ItemParam itemParam,
    out int itemNum)
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
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) SceneBattle.Instance, (UnityEngine.Object) null))
        {
          QuestParam questParam = MonoSingleton<GameManager>.Instance.FindQuest(SceneBattle.Instance.Battle.QuestID) ?? DataSource.FindDataOfClass<QuestParam>(gameObject, (QuestParam) null);
          if (questParam != null && 0 <= index && questParam.bonusObjective != null && index < questParam.bonusObjective.Length)
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
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) root, (UnityEngine.Object) null))
    {
      for (Transform transform = go.transform; UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null); transform = transform.parent)
      {
        stringBuilder.Insert(0, ((UnityEngine.Object) ((Component) transform).gameObject).name);
        stringBuilder.Insert(0, '/');
      }
    }
    else
    {
      for (Transform transform = go.transform; UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) ((Component) transform).gameObject, (UnityEngine.Object) root); transform = transform.parent)
      {
        stringBuilder.Insert(0, ((UnityEngine.Object) ((Component) transform).gameObject).name);
        stringBuilder.Insert(0, '/');
      }
    }
    return stringBuilder.ToString();
  }

  public static T RequireComponent<T>(this Component component) where T : Component
  {
    Component component1 = (Component) component.GetComponent<T>();
    return UnityEngine.Object.op_Inequality((UnityEngine.Object) component1, (UnityEngine.Object) null) ? (T) component1 : component.gameObject.AddComponent<T>();
  }

  public static T RequireComponent<T>(this GameObject go) where T : Component
  {
    Component component = (Component) go.GetComponent<T>();
    return UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null) ? (T) component : go.AddComponent<T>();
  }

  public static void AddClickListener(this Button button, ButtonExt.ButtonClickEvent listener)
  {
    ((Component) button).gameObject.RequireComponent<ButtonExt>().AddListener(listener);
  }

  public static void RemoveClickListener(this Button button, ButtonExt.ButtonClickEvent listener)
  {
    ((Component) button).gameObject.RequireComponent<ButtonExt>().RemoveListener(listener);
  }

  public static string ComposeURL(this FlowNode_WebView.URL_Mode URLMode, string URL)
  {
    switch (URLMode)
    {
      case FlowNode_WebView.URL_Mode.APIHost:
        return Network.Host + URL;
      case FlowNode_WebView.URL_Mode.SiteHost:
        return Network.SiteHost + URL;
      case FlowNode_WebView.URL_Mode.NewsHost:
        return Network.NewsHost + URL;
      default:
        return URL;
    }
  }

  public static float Evaluate(this ObjectAnimator.CurveType curve, float t)
  {
    switch (curve)
    {
      case ObjectAnimator.CurveType.EaseIn:
        return 1f - Mathf.Cos((float) ((double) t * 3.1415927410125732 * 0.5));
      case ObjectAnimator.CurveType.EaseOut:
        return Mathf.Cos((float) ((1.0 - (double) t) * 3.1415927410125732 * 0.5));
      case ObjectAnimator.CurveType.EaseInOut:
        return (float) ((1.0 - (double) Mathf.Cos(t * 3.14159274f)) * 0.5);
      default:
        return t;
    }
  }

  public static float ToSpan(this CameraInterpSpeed speed)
  {
    return speed == CameraInterpSpeed.Immediate ? 0.0f : (float) ((double) speed * 0.25 + 0.5);
  }

  public static void SetNormalizedPosition(
    this ScrollRect scrollRect,
    Vector2 normalizedPos,
    bool blockRectSound)
  {
    if (blockRectSound)
    {
      ScrollRectSound component = ((Component) scrollRect).gameObject.GetComponent<ScrollRectSound>();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
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
      case EShopType.Event:
        return UnlockTargets.EventShop;
      case EShopType.Limited:
        return UnlockTargets.LimitedShop;
      case EShopType.Port:
        return UnlockTargets.Guild;
      default:
        return (UnlockTargets) 0;
    }
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

  public static string ToPrefix(this ESex sex) => SRPG_Extensions.mPrefixes[(int) sex];

  public static string ToColorValue(this Color32 src)
  {
    return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", (object) src.r, (object) src.g, (object) src.b, (object) src.a);
  }

  public static int GetMaxTeamCount(this PartyWindow2.EditPartyTypes type)
  {
    GameManager instanceDirect = MonoSingleton<GameManager>.GetInstanceDirect();
    if (UnityEngine.Object.op_Equality((UnityEngine.Object) instanceDirect, (UnityEngine.Object) null))
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
        case PartyWindow2.EditPartyTypes.GuildRaid:
          return fixParam.PartyNumGuildRaid;
        case PartyWindow2.EditPartyTypes.StoryExtra:
          return fixParam.PartyNumExtra;
        case PartyWindow2.EditPartyTypes.GvG:
          return fixParam.PartyNumGvG;
        case PartyWindow2.EditPartyTypes.WorldRaid:
          return fixParam.PartyNumWorldRaid;
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
      case PartyWindow2.EditPartyTypes.GuildRaid:
        return PlayerPartyTypes.GuildRaid;
      case PartyWindow2.EditPartyTypes.StoryExtra:
        return PlayerPartyTypes.StoryExtra;
      case PartyWindow2.EditPartyTypes.GvG:
        return PlayerPartyTypes.GvG;
      case PartyWindow2.EditPartyTypes.WorldRaid:
        return PlayerPartyTypes.WorldRaid;
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
      case PlayerPartyTypes.GuildRaid:
        return PartyWindow2.EditPartyTypes.GuildRaid;
      case PlayerPartyTypes.StoryExtra:
        return PartyWindow2.EditPartyTypes.StoryExtra;
      case PlayerPartyTypes.GvG:
        return PartyWindow2.EditPartyTypes.GvG;
      case PlayerPartyTypes.WorldRaid:
        return PartyWindow2.EditPartyTypes.WorldRaid;
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
