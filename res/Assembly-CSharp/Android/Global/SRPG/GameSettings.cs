// Decompiled with JetBrains decompiler
// Type: SRPG.GameSettings
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System;
using UnityEngine;

namespace SRPG
{
  [ExecuteInEditMode]
  public class GameSettings : ScriptableObject
  {
    public int Network_BGDLChunkSize = 131072;
    public string[] Tutorial_Steps = new string[0];
    public string[] Tutorial_Flags = new string[0];
    [Tooltip("カメラの上下の傾きの角度")]
    public float GameCamera_AngleX = -45f;
    [Tooltip("クエストでのカメラの最低回転角度")]
    public float GameCamera_YawMin = 45f;
    [Tooltip("クエストでのカメラの最大回転角度")]
    public float GameCamera_YawMax = 145f;
    [Tooltip("クエストカメラで限界を超えて回転できる角度")]
    public float GameCamera_YawSoftLimit = 30f;
    [Tooltip("カメラでユニットを注視する際の高さオフセット")]
    public float GameCamera_UnitHeightOffset = 1f;
    [Tooltip("カメラのデフォルトの距離")]
    public float GameCamera_DefaultDistance = 10f;
    [Tooltip("引いたカメラの距離")]
    public float GameCamera_MoreFarDistance = 24f;
    [Tooltip("イベントカメラのデフォルトの距離")]
    public float GameCamera_EventCameraDistance = 10f;
    [Tooltip("カメラのマップ確認時の距離")]
    public float GameCamera_MapDistance = 10f;
    [Tooltip("カメラの最大距離")]
    public float GameCamera_MaxDistance = 30f;
    [Range(1f, 80f)]
    public float GameCamera_TacticsSceneFOV = 40f;
    [Range(1f, 80f)]
    public float GameCamera_BattleSceneFOV = 15f;
    [Tooltip("マップ上でスキルを使用する際のカメラの距離")]
    public float GameCamera_SkillCameraDistance = 5f;
    [Tooltip("敵の行動前の待機時間　1/MoveWait秒")]
    public float AiUnit_MoveWait = 1f;
    [Tooltip("敵の行動前の待機時間　1/SkillWait秒")]
    public float AiUnit_SkillWait = 1f;
    [Tooltip("キャラクターの最大の発光強度")]
    public float Unit_MaxGlowStrength = 0.3f;
    [Tooltip("小ジャンプの閾値")]
    public float Unit_StepAnimationThreshold = 0.2f;
    [Tooltip("段差登りの閾値")]
    public float Unit_JumpAnimationThreshold = 0.5f;
    [Tooltip("落下の閾値")]
    public float Unit_FallAnimationThreshold = -0.4f;
    [Tooltip("段差登り時の前方方向へのカーブ")]
    public AnimationCurve Unit_JumpZCurve = new AnimationCurve(new Keyframe[2]{ new Keyframe(0.0f, 0.0f), new Keyframe(1f, 1f) });
    [Tooltip("段差登り時の上方向へのカーブ")]
    public AnimationCurve Unit_JumpYCurve = new AnimationCurve(new Keyframe[3]{ new Keyframe(0.0f, 0.0f), new Keyframe(0.5f, 1f), new Keyframe(1f, 0.0f) });
    [Tooltip("段差登り時の最小時間")]
    public float Unit_JumpMinTime = 0.5f;
    [Tooltip("段差登り時の高さ1毎の追加時間")]
    public float Unit_JumpTimePerHeight = 0.2f;
    [Tooltip("段差降り時の最小時間")]
    public float Unit_FallMinTime = 0.5f;
    [Tooltip("段差降り時の高さ1毎の追加時間")]
    public float Unit_FallTimePerHeight = 0.2f;
    [Tooltip("キャラクターを揺らす幅")]
    public float ShakeUnit_Offset = 0.0125f;
    [Tooltip("キャラクターを揺らす回数")]
    public int ShakeUnit_MaxCount = 8;
    [Tooltip("キャラクターの最大の発光強度")]
    public int Gem_DrainCount_FrontHit = 4;
    public int Gem_DrainCount_SideHit = 9;
    public int Gem_DrainCount_BackHit = 15;
    public int Gem_DrainCount_Randomness = 3;
    public Color32 Buff_TextTopColor = new Color32((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue);
    public Color32 Buff_TextBottomColor = new Color32((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue);
    public Color32 Debuff_TextTopColor = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
    public Color32 Debuff_TextBottomColor = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
    public Color32 FailCondition_TextTopColor = new Color32((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue);
    public Color32 FailCondition_TextBottomColor = new Color32((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue);
    [Tooltip("被ヒット時のポップアップの発生間隔")]
    public float HitPopup_PopDeley = 0.4f;
    [Tooltip("クリティカル演出で発生するフラッシュ効果の強さ")]
    public float CriticalHit_FlashStrength = 0.75f;
    [Tooltip("クリティカル演出で発生するフラッシュ効果の表示時間")]
    public float CriticalHit_FlashDuration = 0.3f;
    [Tooltip("クリティカル演出で発生するカメラシェイクの時間")]
    public float CriticalHit_ShakeDuration = 0.3f;
    [Tooltip("クリティカル演出で発生するカメラシェイクの横揺れ回数")]
    public float CriticalHit_ShakeFrequencyX = 10f;
    [Tooltip("クリティカル演出で発生するカメラシェイクの縦揺れ回数")]
    public float CriticalHit_ShakeFrequencyY = 10f;
    [Tooltip("クリティカル演出で発生するカメラシェイクの横揺れの強さ")]
    public float CriticalHit_ShakeAmplitudeX = 1f;
    [Tooltip("クリティカル演出で発生するカメラシェイクの縦揺れの強さ")]
    public float CriticalHit_ShakeAmplitudeY = 1f;
    [Tooltip("ブルームのぼかし強度")]
    public float PostEffect_BloomBlurStrength = 2f;
    [Tooltip("ブルームの最大強度")]
    public float PostEffect_BloomMaxStrength = 16f;
    public Color Character_DefaultDirectLitColor = new Color(1f, 1f, 1f, 1f);
    public Color Character_DefaultIndirectLitColor = new Color(1f, 1f, 1f, 1f);
    public Color32 Character_PlayerGlowColor = new Color32((byte) 0, (byte) 128, byte.MaxValue, (byte) 0);
    public Color32 Character_EnemyGlowColor = new Color32(byte.MaxValue, (byte) 0, (byte) 0, (byte) 0);
    public Sprite[] Elements_IconSmall = new Sprite[0];
    public Sprite[] UnitIcon_Frames = new Sprite[0];
    public Sprite[] UnitIcon_Rarity = new Sprite[0];
    public Sprite[] ArtifactIcon_Frames = new Sprite[0];
    public Sprite[] ArtifactIcon_Rarity = new Sprite[0];
    public Sprite[] ArtifactIcon_RarityBG = new Sprite[0];
    public Color32 Gauge_HP_Base = new Color32((byte) 0, byte.MaxValue, byte.MaxValue, byte.MaxValue);
    public Color32 Gauge_HP_Damage = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
    public Color32 Gauge_HP_Heal = new Color32((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue);
    public Color32 Gauge_PlayerHP_Base = new Color32((byte) 0, (byte) 0, byte.MaxValue, byte.MaxValue);
    public Color32 Gauge_PlayerHP_Damage = new Color32((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue);
    public Color32 Gauge_PlayerHP_Heal = new Color32(byte.MaxValue, byte.MaxValue, (byte) 0, byte.MaxValue);
    public Color32 Gauge_EnemyHP_Base = new Color32(byte.MaxValue, (byte) 0, (byte) 0, byte.MaxValue);
    public Color32 Gauge_EnemyHP_Damage = new Color32(byte.MaxValue, byte.MaxValue, (byte) 0, byte.MaxValue);
    public Color32 Gauge_EnemyHP_Heal = new Color32((byte) 0, byte.MaxValue, (byte) 0, byte.MaxValue);
    public GameSettings.DialogSettings Dialogs = new GameSettings.DialogSettings();
    public GameSettings.CameraSettings Cameras = new GameSettings.CameraSettings();
    public GameSettings.ColorSettings Colors = new GameSettings.ColorSettings();
    public GameSettings.QuestSettings Quest = new GameSettings.QuestSettings();
    public GameSettings.ItemIconSettings ItemIcons = new GameSettings.ItemIconSettings();
    public char[] ValidInputChars = new char[0];
    public string QuestLoad_OkyakusamaCode = string.Empty;
    public GameSettings.HoldCountSettings[] HoldCount = new GameSettings.HoldCountSettings[0];
    [StringIsResourcePath(typeof (GameObject))]
    public string UnitGet_EffectTemplate = string.Empty;
    public string CharacterQuestSection = "WD_CHARA";
    public const float ListRefreshFadeTime = 0.3f;
    private static GameSettings mInstance;
    [Tooltip("被ヒット時のポップアップの表示間隔")]
    public float HitPopup_YSpacing;
    public GameSettings.UnitSortIcon[] UnitSort_Modes;
    public Sprite ArtifactIcon_Weapon;
    public Sprite ArtifactIcon_Armor;
    public Sprite ArtifactIcon_Misc;
    public GameObject Dialog_BuyCoin;
    [StringIsResourcePath(typeof (GameObject))]
    public string Dialog_AbilityDetail;
    public string CharacterQuest_Unlock;
    public Canvas Canvas2D;
    public Transform EnemyPosRig;
    public Transform CameraPosRig;
    public Sprite[] ItemPriceIconFrames;
    public float QuestLoad_WaitSecond;
    [SerializeField]
    [Range(1f, 100f)]
    public int HoldMargin;
    public FlowNode_WebView.URL_Mode WebHelp_URLMode;
    [StringIsResourcePath(typeof (GameObject))]
    public string WebHelp_PrefabPath;

    public static GameSettings Instance
    {
      get
      {
        if ((UnityEngine.Object) GameSettings.mInstance == (UnityEngine.Object) null)
          GameSettings.mInstance = AssetManager.Load<GameSettings>(nameof (GameSettings));
        return GameSettings.mInstance;
      }
    }

    public Sprite GetUnitSortModeIcon(GameUtility.UnitSortModes mode)
    {
      for (int index = 0; index < this.UnitSort_Modes.Length; ++index)
      {
        if (this.UnitSort_Modes[index].Mode == mode)
          return this.UnitSort_Modes[index].Icon;
      }
      return (Sprite) null;
    }

    public Sprite GetItemFrame(EItemType type, int rare)
    {
      Sprite[] spriteArray;
      switch (type)
      {
        case EItemType.UnitPiece:
        case EItemType.ItemPiecePiece:
          spriteArray = this.ItemIcons.KakeraFrames;
          break;
        case EItemType.ArtifactPiece:
          spriteArray = this.ItemIcons.ArtifactKakeraFrames;
          break;
        default:
          spriteArray = this.ItemIcons.NormalFrames;
          break;
      }
      int index = Mathf.Clamp(rare, 0, spriteArray.Length - 1);
      if (0 <= index)
        return spriteArray[index];
      return (Sprite) null;
    }

    public Sprite GetItemFrame(ItemParam itemParam)
    {
      return this.GetItemFrame(itemParam.type, (int) itemParam.rare);
    }

    public long CreateTutorialFlagMask(string flagName)
    {
      for (int index = 1; index < this.Tutorial_Flags.Length; ++index)
      {
        if (this.Tutorial_Flags[index] == flagName)
          return 1L << index;
      }
      return 0;
    }

    [Serializable]
    public struct UnitSortIcon
    {
      public GameUtility.UnitSortModes Mode;
      public Sprite Icon;
    }

    [Serializable]
    public struct DialogSettings
    {
      public Win_Btn_DecideCancel_FL_Check_C YesNoDialogWithCheckBox;
      public Win_Btn_DecideCancel_FL_C YesNoDialog;
      public Win_Btn_Decide_Title_Flx YesDialogWithTitle;
      public Win_Btn_YN_Title_Flx YesNoDialogWithTitle;
      public Win_Btn_Decide_Flx YesDialog;
      public Win_SysMessage_Flx SysMsgDialog;
    }

    [Serializable]
    public struct CameraSettings
    {
      public UnityEngine.Camera OverlayCamera;
    }

    [Serializable]
    public struct ColorSettings
    {
      public Color Enemy;
      public Color Player;
      public Color Helper;
      public Color DamageDigits;
      public Color HealDigits;
      public Color CriticalDigits;
      public Color WalkableArea;
      public Color StartGrid;
      public Color AttackArea;
      public Color AttackArea2;
      public Color ChargeAreaGrn;
      public Color ChargeAreaRed;
    }

    [Serializable]
    public struct QuestSettings
    {
      public Transform TacticsCamera;
      public Transform MoveCamera;
      public Transform UnitCamera;
      public Transform BattleCamera;
      public Transform RunCamera;
      public AnimationCurve RunCameraInterpRate;
      public float BattleRunSpeed;
      public float MapRunSpeedMin;
      public float MapRunSpeedMax;
      public float MapCharacterScale;
      public float AnimateGridSnapRadius;
      public float GridSnapDelay;
      public float GridSnapSpeed;
      public float MapTransitionSpeed;
      public float DoorEnterTime;
      public float TreasureTime;
      public float ViewingUnitSnapSpeed;
      public float BattleTurnEndWait;
      public float RenkeiEndWait;
      public float WaitAfterUnitPickupGimmick;
      [Description("きりもみ状態での毎秒の回転角度")]
      public float KirimomiRotationRate;
      [Description("ユニット交代時のエフェクト待ち時間")]
      public float UnitChangeEffectWaitTime;
      [Description("オートプレイ時のイベントステップ待ち時間")]
      public float WaitTimeScriptEventForward;
      [Description("ユニット撤退時のエフェクト待ち時間")]
      public float WithdrawUnitEffectWaitTime;
      [Description("壊れるオブジェクトの設置最大許容数")]
      public int BreakObjAllowEntryMax;
      [Description("天候エフェクトの切り替え時間")]
      public float WeatherEffectChangeTime;
    }

    [Serializable]
    public struct ItemIconSettings
    {
      public Sprite[] NormalFrames;
      public Sprite[] KakeraFrames;
      public Sprite[] ArtifactKakeraFrames;
    }

    [Serializable]
    public struct HoldCountSettings
    {
      public int Count;
      public float UseSpan;
    }
  }
}
