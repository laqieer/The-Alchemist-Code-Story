// Decompiled with JetBrains decompiler
// Type: SRPG.Network
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using Gsc;
using Gsc.App.NetworkHelper;
using Gsc.Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

#nullable disable
namespace SRPG
{
  [AddComponentMenu("Scripts/SRPG/Manager/Network")]
  public class Network : MonoSingleton<SRPG.Network>
  {
    public static SRPG.Network.RequestResults RequestResult;
    public static readonly float WEBAPI_TIMEOUT_SEC = 60f;
    public static SRPG.Network.EConnectMode Mode = SRPG.Network.EConnectMode.Offline;
    public static readonly string OfficialUrl = "https://al.fg-games.co.jp/";
    public static readonly string DefaultHost = "https://alchemist.gu3.jp/";
    public static readonly string DefaultDLHost = "https://alchemist.gu3.jp/";
    public static readonly string DefaultSiteHost = "https://st-al.fg-games.co.jp/";
    public static readonly string DefaultNewsHost = "https://st-al.fg-games.co.jp/";
    private static long ServerTime;
    private static long LastRealTime;
    private string mSessionID = string.Empty;
    private string mVersion = string.Empty;
    private string mAssets = string.Empty;
    private string mAssetsEx = string.Empty;
    private int mTicket = 1;
    private bool mBusy;
    private bool mRetry;
    private bool mError;
    private string mErrMsg;
    private SRPG.Network.EErrCode mErrCode;
    private bool mImmediateMode;
    private bool mMenteCheckFlag;
    private WebAPI mCurrentRequest;
    private List<WebAPI> mRequests = new List<WebAPI>(4);
    private bool mIndicator = true;
    private UnityWebRequest mWebReq;
    private bool mAbort;
    private bool mNoVersion;
    private bool mForceBusy;
    private string mDefaultHostConfigured = SRPG.Network.DefaultHost;
    private static string _updatedMasterDigest;
    private static string _updatedQuestDigest;
    private static string _updatedEnvFlg2;
    public static bool DoChkver2InJson;

    public static bool IsImmediateMode => MonoSingleton<SRPG.Network>.Instance.mImmediateMode;

    public static void SetDefaultHostConfigured(string host)
    {
      MonoSingleton<SRPG.Network>.Instance.mDefaultHostConfigured = host;
    }

    public static string GetDefaultHostConfigured()
    {
      return MonoSingleton<SRPG.Network>.Instance.mDefaultHostConfigured;
    }

    public static Gsc.App.Environment GetEnvironment => SDK.Configuration.GetEnv<Gsc.App.Environment>();

    public static Gsc.App.Environment.EnvironmentFlagBit GetEnvironmentFlagBitSafely
    {
      get
      {
        try
        {
          return SRPG.Network.GetEnvironment.EnvironmentFlag;
        }
        catch (NullReferenceException ex)
        {
          return Gsc.App.Environment.EnvironmentFlagBit.ENV_FLG_NONE;
        }
      }
    }

    public static string Host => SRPG.Network.GetEnvironment.ServerUrl;

    public static string DLHost => SRPG.Network.GetEnvironment.DLHost;

    public static string SiteHost => SRPG.Network.GetEnvironment.SiteHost;

    public static string NewsHost => SRPG.Network.GetEnvironment.NewsHost;

    public static string MasterDigest
    {
      get
      {
        return SRPG.Network._updatedMasterDigest != null ? SRPG.Network._updatedMasterDigest : SRPG.Network.GetEnvironment.MasterDigest;
      }
      set
      {
        if (string.IsNullOrEmpty(SRPG.Network.GetEnvironment.MasterDigest))
          return;
        SRPG.Network._updatedMasterDigest = value;
      }
    }

    public static string QuestDigest
    {
      get
      {
        return SRPG.Network._updatedQuestDigest != null ? SRPG.Network._updatedQuestDigest : SRPG.Network.GetEnvironment.QuestDigest;
      }
      set
      {
        if (string.IsNullOrEmpty(SRPG.Network.GetEnvironment.QuestDigest))
          return;
        SRPG.Network._updatedQuestDigest = value;
      }
    }

    public static string EnvFlg2
    {
      get
      {
        return SRPG.Network._updatedEnvFlg2 != null ? SRPG.Network._updatedEnvFlg2 : SRPG.Network.GetEnvironment.EnvFlg2;
      }
      set
      {
        if (string.IsNullOrEmpty(SRPG.Network.GetEnvironment.EnvFlg2))
          return;
        SRPG.Network._updatedEnvFlg2 = value;
      }
    }

    public static string Pub => SRPG.Network.GetEnvironment.Pub;

    public static string PubU => SRPG.Network.GetEnvironment.PubU;

    public static string AssetVersion
    {
      get => MonoSingleton<SRPG.Network>.Instance.mAssets;
      set => MonoSingleton<SRPG.Network>.Instance.mAssets = value;
    }

    public static string AssetVersionEx
    {
      get => MonoSingleton<SRPG.Network>.Instance.mAssetsEx;
      set => MonoSingleton<SRPG.Network>.Instance.mAssetsEx = value;
    }

    public static string Version
    {
      get => MonoSingleton<SRPG.Network>.Instance.mVersion;
      set => MonoSingleton<SRPG.Network>.Instance.mVersion = value;
    }

    public static string SessionID
    {
      get => MonoSingleton<SRPG.Network>.Instance.mSessionID;
      set => MonoSingleton<SRPG.Network>.Instance.mSessionID = value;
    }

    public static int TicketID
    {
      get => MonoSingleton<SRPG.Network>.Instance.mTicket;
      private set => MonoSingleton<SRPG.Network>.Instance.mTicket = value;
    }

    public static bool IsBusy
    {
      get
      {
        if (MonoSingleton<SRPG.Network>.Instance.mBusy)
          return true;
        return WebQueue.defaultQueue != null && WebQueue.defaultQueue.isRunning;
      }
      private set => MonoSingleton<SRPG.Network>.Instance.mBusy = value;
    }

    public static bool IsRetry
    {
      get => MonoSingleton<SRPG.Network>.Instance.mRetry;
      set => MonoSingleton<SRPG.Network>.Instance.mRetry = value;
    }

    public static bool IsError
    {
      get => MonoSingleton<SRPG.Network>.Instance.mError || GsccBridge.HasUnhandledTasks;
      private set => MonoSingleton<SRPG.Network>.Instance.mError = value;
    }

    public static string ErrMsg
    {
      get => MonoSingleton<SRPG.Network>.Instance.mErrMsg;
      set => MonoSingleton<SRPG.Network>.Instance.mErrMsg = value;
    }

    public static SRPG.Network.EErrCode ErrCode
    {
      get => MonoSingleton<SRPG.Network>.Instance.mErrCode;
      set => MonoSingleton<SRPG.Network>.Instance.mErrCode = value;
    }

    public static bool IsConnecting
    {
      get => SRPG.Network.IsBusy || MonoSingleton<SRPG.Network>.Instance.mRequests.Count > 0;
    }

    public static bool IsIndicator
    {
      get => MonoSingleton<SRPG.Network>.Instance.mIndicator;
      set => MonoSingleton<SRPG.Network>.Instance.mIndicator = value;
    }

    public static UnityWebRequest uniWebRequest
    {
      get => MonoSingleton<SRPG.Network>.Instance.mWebReq;
      set => MonoSingleton<SRPG.Network>.Instance.mWebReq = value;
    }

    public static bool IsStreamConnecting => SRPG.Network.uniWebRequest != null;

    public static bool Aborted
    {
      get => MonoSingleton<SRPG.Network>.Instance.mAbort;
      set => MonoSingleton<SRPG.Network>.Instance.mAbort = value;
    }

    public static bool IsNoVersion
    {
      get => MonoSingleton<SRPG.Network>.Instance.mNoVersion;
      set => MonoSingleton<SRPG.Network>.Instance.mNoVersion = value;
    }

    public static bool IsForceBusy
    {
      get => MonoSingleton<SRPG.Network>.Instance.mForceBusy;
      set => MonoSingleton<SRPG.Network>.Instance.mForceBusy = value;
    }

    public static bool MenteCheckFlag
    {
      get => MonoSingleton<SRPG.Network>.Instance.mMenteCheckFlag;
      set => MonoSingleton<SRPG.Network>.Instance.mMenteCheckFlag = value;
    }

    protected override void Initialize()
    {
      SRPG.Network.Reset();
      UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this);
    }

    protected override void Release()
    {
    }

    public static void Reset()
    {
      MonoSingleton<SRPG.Network>.Instance.mTicket = 1;
      MonoSingleton<SRPG.Network>.Instance.mRequests.Clear();
      GsccBridge.Reset();
    }

    public static void RequestAPI(WebAPI api, bool highPriority = false)
    {
      DebugUtility.Log("Request WebAPI: " + api.name);
      if (highPriority)
        MonoSingleton<SRPG.Network>.Instance.mRequests.Insert(0, api);
      else
        MonoSingleton<SRPG.Network>.Instance.mRequests.Add(api);
      if (MonoSingleton<SRPG.Network>.Instance.mRequests.Count != 1)
        return;
      CriticalSection.Enter(CriticalSections.Network);
    }

    public static void RequestAPIImmediate(WebAPI api, bool autoRetry)
    {
      MonoSingleton<SRPG.Network>.Instance.mImmediateMode = true;
      GsccBridge.SendImmediate(api);
      if (!MonoSingleton<SRPG.Network>.Instance.mImmediateMode)
        return;
      MonoSingleton<SRPG.Network>.Instance.mImmediateMode = false;
      if (!autoRetry)
        return;
      --SRPG.Network.TicketID;
      SRPG.Network.ResetError();
      SRPG.Network.RequestAPI(api, true);
    }

    public static void RemoveAPI()
    {
      GsccBridge.Reset();
      if (MonoSingleton<SRPG.Network>.Instance.mImmediateMode)
        MonoSingleton<SRPG.Network>.Instance.mImmediateMode = false;
      else if (MonoSingleton<SRPG.Network>.Instance.mRequests.Count <= 0)
      {
        DebugUtility.LogWarning("Instance.mRequestsGsc.Count <= 0");
      }
      else
      {
        MonoSingleton<SRPG.Network>.Instance.mRequests.Remove(MonoSingleton<SRPG.Network>.Instance.mCurrentRequest);
        if (MonoSingleton<SRPG.Network>.Instance.mRequests.Count != 0)
          return;
        CriticalSection.Leave(CriticalSections.Network);
      }
    }

    public static void ResetError()
    {
      MonoSingleton<SRPG.Network>.Instance.mError = false;
      MonoSingleton<SRPG.Network>.Instance.mMenteCheckFlag = false;
      SRPG.Network.DoChkver2InJson = false;
    }

    public static void SetRetry() => GsccBridge.Retry();

    private static int FindStat(string response)
    {
      Regex regex = new Regex("\"stat\":(?<stat>\\d+)", RegexOptions.None);
      return !regex.Match(response).Success ? 0 : Convert.ToInt32(regex.Match(response).Result("${stat}"));
    }

    private static string FindMessage(string response)
    {
      Regex regex = new Regex("\"stat_msg\":\"(?<stat_msg>.+?)\"[,}]", RegexOptions.None);
      return !regex.Match(response).Success ? string.Empty : regex.Match(response).Result("${stat_msg}");
    }

    private static long FindTime(string response)
    {
      Regex regex = new Regex("\"time\":(?<time>\\d+)", RegexOptions.None);
      return !regex.Match(response).Success ? 0L : Convert.ToInt64(regex.Match(response).Result("${time}"));
    }

    public static long GetServerTime()
    {
      if (SRPG.Network.Mode == SRPG.Network.EConnectMode.Offline)
        return TimeManager.Now();
      long systemUptime = SRPG.Network.GetSystemUptime();
      return SRPG.Network.ServerTime + (systemUptime - SRPG.Network.LastRealTime);
    }

    public static long LastConnectionTime => SRPG.Network.ServerTime;

    private void Update()
    {
      if (SRPG.Network.IsBusy || SRPG.Network.IsError || SRPG.Network.IsForceBusy || this.mRequests.Count <= 0)
        return;
      WebAPI mRequest = this.mRequests[0];
      if (mRequest == null)
        return;
      if (mRequest.reqtype == WebAPI.RequestType.REQ_GSC)
        this.ConnectingGsc(mRequest);
      else
        this.StartCoroutine(SRPG.Network.Connecting(mRequest));
    }

    private void ConnectingGsc(WebAPI api)
    {
      ++SRPG.Network.TicketID;
      SRPG.Network.IsError = false;
      SRPG.Network.ErrCode = SRPG.Network.EErrCode.Success;
      MonoSingleton<SRPG.Network>.Instance.mCurrentRequest = api;
      GsccBridge.Send(api, false);
    }

    public static void ConnectingResponse(WebResponse response, SRPG.Network.ResponseCallback callback)
    {
      SRPG.Network.ErrCode = response.ErrorCode;
      SRPG.Network.ErrMsg = response.ErrorMessage;
      SRPG.Network.IsError = SRPG.Network.ErrCode != SRPG.Network.EErrCode.Success;
      if (FlowNode_Network.HasCommonError(response.Result) || callback == null)
        return;
      if (response.ServerTime != 0L)
        SRPG.Network.ServerTime = response.ServerTime;
      SRPG.Network.LastRealTime = SRPG.Network.GetSystemUptime();
      callback(response.Result);
    }

    public static void SetServerTime(long time)
    {
      if (time != 0L)
        SRPG.Network.ServerTime = time;
      SRPG.Network.LastRealTime = SRPG.Network.GetSystemUptime();
    }

    [DebuggerHidden]
    private static IEnumerator Connecting(WebAPI api)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new SRPG.Network.\u003CConnecting\u003Ec__Iterator0()
      {
        api = api
      };
    }

    public static void SetServerSessionExpired()
    {
      SRPG.Network.ErrCode = SRPG.Network.EErrCode.DmmSessionExpired;
      SRPG.Network.ErrMsg = LocalizedText.Get("embed.DMM_EXPIRED");
      SRPG.Network.IsError = true;
    }

    public static void SetServerMetaDataAsError()
    {
      SRPG.Network.ErrCode = SRPG.Network.EErrCode.Failed;
      SRPG.Network.ErrMsg = LocalizedText.Get("embed.NETWORKERR");
      SRPG.Network.IsError = true;
    }

    public static void SetServerInvalidDeviceError()
    {
      SRPG.Network.ErrCode = SRPG.Network.EErrCode.Authorize;
      SRPG.Network.ErrMsg = LocalizedText.Get("sys.AUTHORIZEERR");
      SRPG.Network.IsError = true;
    }

    public static void SetServerMetaDataAsError(SRPG.Network.EErrCode code, string msg)
    {
      SRPG.Network.ErrCode = code;
      SRPG.Network.ErrMsg = msg;
      SRPG.Network.IsError = true;
    }

    private static long GetSystemUptime() => (long) Time.realtimeSinceStartup;

    public static void Abort()
    {
      if (SRPG.Network.uniWebRequest == null)
        return;
      SRPG.Network.uniWebRequest.Abort();
      SRPG.Network.Aborted = true;
    }

    public enum EErrCode
    {
      TimeOut = -2, // 0xFFFFFFFE
      Failed = -1, // 0xFFFFFFFF
      Success = 0,
      Unknown = 1,
      Version = 2,
      AssetVersion = 3,
      NoVersionDbg = 4,
      NoSID = 100, // 0x00000064
      Maintenance = 200, // 0x000000C8
      ChatMaintenance = 201, // 0x000000C9
      MultiMaintenance = 202, // 0x000000CA
      VsMaintenance = 203, // 0x000000CB
      BattleRecordMaintenance = 204, // 0x000000CC
      MultiVersionMaintenance = 205, // 0x000000CD
      MultiTowerMaintenance = 206, // 0x000000CE
      RankingQuestMaintenance = 207, // 0x000000CF
      IllegalParam = 300, // 0x0000012C
      API = 400, // 0x00000190
      NotLocation = 401, // 0x00000191
      ServerNotify = 500, // 0x000001F4
      ServerNotifyAndGoToHome = 501, // 0x000001F5
      ServerNotifyAndReloadScene = 502, // 0x000001F6
      NoFile = 1000, // 0x000003E8
      NoVersion = 1100, // 0x0000044C
      SessionFailure = 1200, // 0x000004B0
      CreateStopped = 1300, // 0x00000514
      IllegalName = 1400, // 0x00000578
      IllegalComment = 1401, // 0x00000579
      NoMail = 1500, // 0x000005DC
      MailReadable = 1501, // 0x000005DD
      ReqFriendRequestMax = 1600, // 0x00000640
      ReqFriendIsFull = 1601, // 0x00000641
      ReqNoFriend = 1602, // 0x00000642
      ReqFriendRegistered = 1603, // 0x00000643
      ReqFriendRequesting = 1604, // 0x00000644
      RmNoFriend = 1700, // 0x000006A4
      RmFriendIsMe = 1701, // 0x000006A5
      NoUnitParty = 1800, // 0x00000708
      IllegalParty = 1801, // 0x00000709
      ExpMaterialShort = 1900, // 0x0000076C
      RareMaterialShort = 2000, // 0x000007D0
      RarePlayerLvShort = 2001, // 0x000007D1
      PlusMaterialShot = 2100, // 0x00000834
      PlusPlayerLvShort = 2101, // 0x00000835
      AbilityMaterialShort = 2200, // 0x00000898
      AbilityNotFound = 2201, // 0x00000899
      NoJobSetJob = 2300, // 0x000008FC
      CantSelectJob = 2301, // 0x000008FD
      NoUnitSetJob = 2302, // 0x000008FE
      NoAbilitySetAbility = 2400, // 0x00000960
      NoJobSetAbility = 2401, // 0x00000961
      UnsetAbility = 2402, // 0x00000962
      NoJobSetEquip = 2500, // 0x000009C4
      NoEquipItem = 2501, // 0x000009C5
      Equipped = 2502, // 0x000009C6
      NoJobEnforceEquip = 2600, // 0x00000A28
      NoEquipEnforce = 2601, // 0x00000A29
      ForceMax = 2602, // 0x00000A2A
      MaterialShort = 2603, // 0x00000A2B
      EnforcePlayerLvShort = 2604, // 0x00000A2C
      NoJobLvUpEquip = 2700, // 0x00000A8C
      EquipNotComp = 2701, // 0x00000A8D
      PlusShort = 2702, // 0x00000A8E
      NoItemSell = 2800, // 0x00000AF0
      ConvertAnotherItem = 2801, // 0x00000AF1
      GoldOverSell = 2802, // 0x00000AF2
      StaminaCoinShort = 2900, // 0x00000B54
      AddStaminaLimit = 2901, // 0x00000B55
      AbilityCoinShort = 3000, // 0x00000BB8
      AbilityVipLvShort = 3001, // 0x00000BB9
      AbilityPlayerLvShort = 3002, // 0x00000BBA
      GouseiNoTarget = 3200, // 0x00000C80
      GouseiMaterialShort = 3201, // 0x00000C81
      GouseiCostShort = 3202, // 0x00000C82
      UnSelectable = 3300, // 0x00000CE4
      OutOfDateQuest = 3301, // 0x00000CE5
      QuestNotEnd = 3302, // 0x00000CE6
      ChallengeLimit = 3303, // 0x00000CE7
      NotGpsQuest = 3308, // 0x00000CEC
      RecordLimitUpload = 3309, // 0x00000CED
      CanNotResetQuest = 3320, // 0x00000CF8
      NotEnoughItemToReset = 3321, // 0x00000CF9
      ChallengesCountStillRemain = 3322, // 0x00000CFA
      QuestResetNotEnoughCoin = 3323, // 0x00000CFB
      QuestResume = 3400, // 0x00000D48
      QuestEnd = 3500, // 0x00000DAC
      ContinueCostShort = 3600, // 0x00000E10
      CantContinue = 3601, // 0x00000E11
      Advance_KeyClose = 3604, // 0x00000E14
      NoBtlInfo = 3700, // 0x00000E74
      MultiPlayerLvShort = 3701, // 0x00000E75
      MultiBtlNotEnd = 3702, // 0x00000E76
      MultiVersionMismatch = 3704, // 0x00000E78
      DiffBattleVersion = 3705, // 0x00000E79
      ColoCantSelect = 3800, // 0x00000ED8
      ColoIsBusy = 3801, // 0x00000ED9
      ColoCostShort = 3802, // 0x00000EDA
      ColoIntervalShort = 3803, // 0x00000EDB
      ColoBattleNotEnd = 3804, // 0x00000EDC
      ColoPlayerLvShort = 3805, // 0x00000EDD
      ColoVipShort = 3806, // 0x00000EDE
      ColoRankLower = 3807, // 0x00000EDF
      ColoNoBattle = 3900, // 0x00000F3C
      ColoRankModify = 3901, // 0x00000F3D
      ColoMyRankModify = 3902, // 0x00000F3E
      NoGacha = 4000, // 0x00000FA0
      GachaCostShort = 4001, // 0x00000FA1
      GachaItemMax = 4002, // 0x00000FA2
      GachaNotFree = 4003, // 0x00000FA3
      GachaPaidLimitOver = 4004, // 0x00000FA4
      GachaPlyLvOver = 4005, // 0x00000FA5
      GachaPlyNewOver = 4006, // 0x00000FA6
      GachaLimitSoldOut = 4007, // 0x00000FA7
      GachaLimitCntOver = 4008, // 0x00000FA8
      GachaOutofPeriod = 4010, // 0x00000FAA
      CanNotApplyDiscount = 4011, // 0x00000FAB
      AlreadyApplyDiscount = 4012, // 0x00000FAC
      NotPickupSelect = 4017, // 0x00000FB1
      NotPickupItems = 4018, // 0x00000FB2
      IncorrectPickups = 4019, // 0x00000FB3
      NoGachaPickup = 4020, // 0x00000FB4
      CanNotChangePickupItems = 4021, // 0x00000FB5
      TrophyRewarded = 4100, // 0x00001004
      TrophyOutOfDate = 4101, // 0x00001005
      TrophyRollBack = 4102, // 0x00001006
      BingoOutofDate = 4110, // 0x0000100E
      BingoRemainingChildren = 4111, // 0x0000100F
      BingoOutofDateReceive = 4112, // 0x00001010
      ShopRefreshCostShort = 4200, // 0x00001068
      ShopRefreshLvSort = 4201, // 0x00001069
      ShopSoldOut = 4300, // 0x000010CC
      ShopBuyCostShort = 4301, // 0x000010CD
      ShopBuyLvShort = 4302, // 0x000010CE
      ShopBuyNotFound = 4303, // 0x000010CF
      ShopBuyItemNotFound = 4304, // 0x000010D0
      ShopRefreshItemList = 4305, // 0x000010D1
      ShopBuyOutofItemPeriod = 4306, // 0x000010D2
      GoldBuyCostShort = 4400, // 0x00001130
      GoldBuyLimit = 4401, // 0x00001131
      EventShopOutOfPeriod = 4403, // 0x00001133
      LimitedShopOutOfPeriod = 4403, // 0x00001133
      ShopBuyOutofPeriod = 4403, // 0x00001133
      EventShopOutOfBuyLimit = 4405, // 0x00001135
      LimitedShopOutOfBuyLimit = 4405, // 0x00001135
      ShopLimitOver = 4405, // 0x00001135
      ProductIllegalDate = 4500, // 0x00001194
      ProductPurchaseMax = 4600, // 0x000011F8
      ProductCantPurchase = 4601, // 0x000011F9
      HikkoshiNoToken = 4700, // 0x0000125C
      RoomFailedMakeRoom = 4800, // 0x000012C0
      RoomIllegalComment = 4801, // 0x000012C1
      RoomNoRoom = 4900, // 0x00001324
      NoWatching = 4901, // 0x00001325
      RepelledBlockList = 4902, // 0x00001326
      NoDevice = 5000, // 0x00001388
      Authorize = 5001, // 0x00001389
      GauthNoSid = 5002, // 0x0000138A
      ReturnForceTitle = 5003, // 0x0000138B
      MigrateIllCode = 5100, // 0x000013EC
      MigrateSameDev = 5101, // 0x000013ED
      MigrateLockCode = 5102, // 0x000013EE
      ColoResetCostShort = 5500, // 0x0000157C
      RaidTicketShort = 5600, // 0x000015E0
      UnitAddExist = 5700, // 0x00001644
      UnitAddCostShort = 5701, // 0x00001645
      UnitAddCantUnlock = 5702, // 0x00001646
      RoomPlayerLvShort = 5800, // 0x000016A8
      ApprNoFriend = 5900, // 0x0000170C
      ApprNoRequest = 5901, // 0x0000170D
      ApprRequestMax = 5902, // 0x0000170E
      ApprFriendIsFull = 5903, // 0x0000170F
      FindNoFriend = 6000, // 0x00001770
      FindIsMine = 6001, // 0x00001771
      CountLimitForPlayer = 8005, // 0x00001F45
      QR_OutOfPeriod = 8008, // 0x00001F48
      QR_InvalidQRSerial = 8009, // 0x00001F49
      QR_CanNotReward = 8010, // 0x00001F4A
      QR_LockSerialCampaign = 8011, // 0x00001F4B
      QR_AlreadyRewardSkin = 8012, // 0x00001F4C
      ChargeError = 8100, // 0x00001FA4
      ChargeAge000 = 8101, // 0x00001FA5
      ChargeVipRemains = 8102, // 0x00001FA6
      FirstChargeInvalid = 8103, // 0x00001FA7
      FirstChargeNoLog = 8104, // 0x00001FA8
      FirstChargeReceipt = 8105, // 0x00001FA9
      FirstChargePast = 8106, // 0x00001FAA
      ChargePremiumRemains = 8107, // 0x00001FAB
      ChargePremiumInvalid = 8108, // 0x00001FAC
      CoinCanNotBuyAnyMore = 8109, // 0x00001FAD
      CoinSalesHaveEnded = 8110, // 0x00001FAE
      ExpansionTermRemains = 8111, // 0x00001FAF
      CanNotBuyUnlockLv = 8112, // 0x00001FB0
      TowerLocked = 8201, // 0x00002009
      ConditionsErr = 8202, // 0x0000200A
      NotRecovery_permit = 8203, // 0x0000200B
      NotExist_tower = 8211, // 0x00002013
      NotExist_reward = 8212, // 0x00002014
      NotExist_floor = 8213, // 0x00002015
      NoMatch_party = 8221, // 0x0000201D
      NoMatch_mid = 8222, // 0x0000201E
      IncorrectCoin = 8231, // 0x00002027
      IncorrectBtlparam = 8232, // 0x00002028
      AlreadyClear = 8241, // 0x00002031
      AlreadyBtlend = 8242, // 0x00002032
      FaildRegistration = 8243, // 0x00002033
      FaildReset = 8244, // 0x00002034
      NoChannelAction = 8500, // 0x00002134
      NoUserAction = 8501, // 0x00002135
      SendChatInterval = 8502, // 0x00002136
      CanNotAddBlackList = 8503, // 0x00002137
      NotGpsMail = 8600, // 0x00002198
      ReceivedGpsMail = 8601, // 0x00002199
      MailIDDupulicate = 8602, // 0x0000219A
      AcheiveMigrateIllcode = 8800, // 0x00002260
      AcheiveMigrateNoCoop = 8801, // 0x00002261
      AcheiveMigrateLock = 8802, // 0x00002262
      AcheiveMigrateAuthorize = 8803, // 0x00002263
      ArtifactBoxLimit = 9000, // 0x00002328
      ArtifactPieceShort = 9001, // 0x00002329
      ArtifactMatShort = 9002, // 0x0000232A
      ArtifactFavorite = 9003, // 0x0000232B
      SkinNoSkin = 9010, // 0x00002332
      SkinNoJob = 9011, // 0x00002333
      Gift_ConceptCardBoxLimit = 9020, // 0x0000233C
      NotExistConceptCard = 9022, // 0x0000233E
      VS_NotSelfBattle = 10000, // 0x00002710
      VS_NotPlayer = 10001, // 0x00002711
      VS_NotQuestInfo = 10002, // 0x00002712
      VS_NotLINERoomInfo = 10003, // 0x00002713
      VS_FailRoomID = 10004, // 0x00002714
      VS_BattleEnd = 10005, // 0x00002715
      VS_NotQuestData = 10006, // 0x00002716
      VS_NotPhotonAppID = 10007, // 0x00002717
      VS_Version = 10008, // 0x00002718
      VS_IllComment = 10009, // 0x00002719
      VS_LvShort = 10010, // 0x0000271A
      VS_BattleNotEnd = 10011, // 0x0000271B
      VS_NoRoom = 10012, // 0x0000271C
      VS_ComBattleEnd = 10013, // 0x0000271D
      VS_FaildSeasonGift = 10014, // 0x0000271E
      VS_TowerNotPlay = 10015, // 0x0000271F
      VS_NotContinuousEnemy = 10016, // 0x00002720
      VS_RowerNotMatching = 10017, // 0x00002721
      VS_EnableTimeOutOfPriod = 10018, // 0x00002722
      QuestBookmark_RequestMax = 10100, // 0x00002774
      QuestBookmark_AlreadyLimited = 10101, // 0x00002775
      DmmSessionExpired = 11000, // 0x00002AF8
      MT_NotClearFloor = 12001, // 0x00002EE1
      MT_AlreadyFinish = 12002, // 0x00002EE2
      MT_NoRoom = 12003, // 0x00002EE3
      MT_CanNotSkipFloor = 12004, // 0x00002EE4
      RankingQuest_NotNewScore = 13001, // 0x000032C9
      RankingQuest_AlreadyEntry = 13002, // 0x000032CA
      RankingQuest_OutOfPeriod = 13003, // 0x000032CB
      Gallery_MigrationInProgress = 14001, // 0x000036B1
      Gallery_JukeBox_NotHaveBgm = 14101, // 0x00003715
      Gallery_JukeBox_NotCreatePlayList = 14102, // 0x00003716
      Gallery_JukeBox_NotInPlayList = 14103, // 0x00003717
      Gallery_JukeBox_AlreadyAddPlayList = 14104, // 0x00003718
      QuestArchive_ArchiveNotFound = 16001, // 0x00003E81
      QuestArchive_ArchiveNotOpened = 16002, // 0x00003E82
      QuestArchive_ArchiveAlreadyOpened = 16003, // 0x00003E83
      InspSkill_IncorrectParam = 17001, // 0x00004269
      InspSkill_NotExistArtifact = 17002, // 0x0000426A
      InspSkill_NotExistInspirationSkill = 17003, // 0x0000426B
      InspSkill_ArtifactFavorite = 17004, // 0x0000426C
      InspSkill_IncorrectMixArtifact = 17005, // 0x0000426D
      InspSkill_CannotLevelUpLevelMax = 17006, // 0x0000426E
      InspSkill_CannotResetNotFound = 17007, // 0x0000426F
      InspSkill_CostShort = 17008, // 0x00004270
      InspSkill_CannotAddSlotArtifact = 17009, // 0x00004271
      InspSkill_CannotAddSlotNumMax = 17010, // 0x00004272
      BoxGacha_NotExistBox = 18001, // 0x00004651
      BoxGacha_CostShort = 18002, // 0x00004652
      BoxGacha_RemainShort = 18003, // 0x00004653
      BoxGacha_NotResetBox = 18004, // 0x00004654
      Genesis_OutOfPeriod = 19001, // 0x00004A39
      CoinBuyUse_OutOfPeriod = 20001, // 0x00004E21
      Guild_JoinedAlready = 20010, // 0x00004E2A
      Guild_NotJoined_First = 20020, // 0x00004E34
      Guild_NotJoined = 20021, // 0x00004E35
      Guild_AutoJoinTargetMissing = 20022, // 0x00004E36
      Guild_NotFound = 20040, // 0x00004E48
      Guild_NotFoundEntryRequest = 20050, // 0x00004E52
      Guild_NotMaster = 20060, // 0x00004E5C
      Guild_AlredyMember = 20070, // 0x00004E66
      Guild_MemberMax = 20080, // 0x00004E70
      Guild_SubMasterMax = 20081, // 0x00004E71
      Guild_RaidDissolutionFaild = 20091, // 0x00004E7B
      Guild_NotDissoluteGVGInPeriod = 20092, // 0x00004E7C
      Guild_RaidLeaveFaild = 20102, // 0x00004E86
      Guild_NotLeaveGVGInPeriod = 20103, // 0x00004E87
      Guild_LeaveFailed = 20110, // 0x00004E8E
      Guild_RaidKickFaild = 20135, // 0x00004EA7
      Guild_NotBanishGVGInPeriod = 20136, // 0x00004EA8
      Guild_InputNgWord = 20140, // 0x00004EAC
      Guild_InvestLimitOneDay = 20202, // 0x00004EEA
      Guild_PayerCoinShort = 20220, // 0x00004EFC
      Guild_ShortPlayerLv = 20230, // 0x00004F06
      Guild_ApplySendLevelShort = 20231, // 0x00004F07
      Guild_EntryCoolTime = 20240, // 0x00004F10
      Guild_InvestCoolTime = 20241, // 0x00004F11
      Guild_NotAttendJoinDay = 20250, // 0x00004F1A
      Guild_OutOfAttendPeriod = 20251, // 0x00004F1B
      Guild_MasterOnly = 20260, // 0x00004F24
      Guild_OutOfMasterRewarPeriod = 20261, // 0x00004F25
      Guild_NotAchieveTrophy = 20280, // 0x00004F38
      Guild_PastTrophy = 20281, // 0x00004F39
      Guild_ReceivedTrophyReward = 20282, // 0x00004F3A
      Advance_NotOpen = 21001, // 0x00005209
      Raid_OutOfPeriod = 21010, // 0x00005212
      Raid_OutOfOenTime = 21011, // 0x00005213
      Raid_NotRaidbossCurrent = 21020, // 0x0000521C
      Raid_IncorrectBtlparam = 21030, // 0x00005226
      Raid_AlreadyClear = 21040, // 0x00005230
      Raid_AlreadyBtlend = 21050, // 0x0000523A
      Raid_AlreadyRaidbossSelected = 21060, // 0x00005244
      Raid_AlreadyBeat = 21070, // 0x0000524E
      Raid_NotFound = 21080, // 0x00005258
      Raid_NotRewardReady = 21090, // 0x00005262
      Raid_NotRescueBattle = 21100, // 0x0000526C
      Raid_OverRescue = 21101, // 0x0000526D
      Raid_AlredyRescueCancel = 21102, // 0x0000526E
      Raid_NotRewardAreaRound = 21110, // 0x00005276
      Raid_CanNotNextAreaBossNotBeat = 21120, // 0x00005280
      Raid_CanNotNextAreaNotGetReward = 21130, // 0x0000528A
      Raid_CanNotNextAreaCurrentAreaIsLast = 21140, // 0x00005294
      Raid_CanNotRoundBossNotBeat = 21150, // 0x0000529E
      Raid_CanNotRoundNotGetReward = 21160, // 0x000052A8
      Raid_CanNotRoundCurrentAreaIsNotLast = 21170, // 0x000052B2
      Raid_NotComplete = 21180, // 0x000052BC
      Raid_CanNotSubBp = 21190, // 0x000052C6
      Raid_CanNotSelectAreaClear = 21200, // 0x000052D0
      Raid_CanNotRescuePlLvShort = 21300, // 0x00005334
      Raid_CanNotRescueTimeOver = 21400, // 0x00005398
      Raid_RankRewardOutOfPeriod = 21500, // 0x000053FC
      Raid_RankRewardAlreadyReceived = 21600, // 0x00005460
      UnitRental_OutOfPeriod = 22001, // 0x000055F1
      UnitRental_AlreadyRented = 22002, // 0x000055F2
      UnitRental_AlreadyJoin = 22003, // 0x000055F3
      UnitRental_FavpointShort = 22004, // 0x000055F4
      UnitRental_FavpointMax = 22005, // 0x000055F5
      UnitRental_NotFoundUnit = 22006, // 0x000055F6
      DrawCard_OutOfPeriod = 23001, // 0x000059D9
      DrawCard_CanNotExec = 23002, // 0x000059DA
      TrophyStarMission_AlreadyReceived = 24001, // 0x00005DC1
      TrophyStarMission_NotAchieved = 24002, // 0x00005DC2
      TrophyStarMission_Future = 24003, // 0x00005DC3
      TrophyStarMission_OutOfPeriod = 24004, // 0x00005DC4
      MonthlyLoginBonus_OutOfPeriod = 25001, // 0x000061A9
      MonthlyLoginBonus_RemainRecoverShort = 25002, // 0x000061AA
      MonthlyLoginBonus_AlreadyReceived = 25003, // 0x000061AB
      MonthlyLoginBonus_CanNotRecoverFuture = 25004, // 0x000061AC
      AutoRepeatQuest_NotQuest = 26001, // 0x00006591
      AutoRepeatQuest_NotQuestMissionComplete = 26002, // 0x00006592
      AutoRepeatQuest_AlreadyLap = 26003, // 0x00006593
      AutoRepeatQuest_NotReqLap = 26004, // 0x00006594
      AutoRepeatQuest_InvalidLapNum = 26005, // 0x00006595
      AutoRepeatQuest_AutoRepeatLocked = 26006, // 0x00006596
      AutoRepeatQuest_InvalidTurnNum = 26007, // 0x00006597
      AutoRepeatQuest_NotStart = 26008, // 0x00006598
      AutoRepeatQuest_BoxSizeLimit = 26009, // 0x00006599
      AutoRepeatQuest_NotEnoughCoin = 26010, // 0x0000659A
      GuildRaid_OutOfPeriod = 27001, // 0x00006979
      GuildRaid_EnableTimeOutOfPeriod = 27002, // 0x0000697A
      GuildRaid_ChallengeLimit = 27003, // 0x0000697B
      GuildRaid_AlreadyBeat = 27004, // 0x0000697C
      GuildRaid_CanNotChallengeByThereIsNoBoss = 27005, // 0x0000697D
      GuildRaid_NotChallenge = 27006, // 0x0000697E
      GuildRaid_NotMainBattle = 27007, // 0x0000697F
      GuildRaid_NewAreaCanBeReleased = 27010, // 0x00006982
      GuildRaid_CanNotOpenAreaNotBeatBosses = 27011, // 0x00006983
      GuildRaid_CanNotOpenAreaNotEnoughGuildLevel = 27012, // 0x00006984
      GuildRaid_CanNotNextAreaCurrentAreaIsLast = 27013, // 0x00006985
      GuildRaid_AlreadyMailReceived = 27030, // 0x00006996
      GuildRaid_MailNotReceived = 27031, // 0x00006997
      GvG_OutOfPeriod = 28000, // 0x00006D60
      GvG_NotGvGEntry = 28001, // 0x00006D61
      GvG_NotUsedUnit = 28002, // 0x00006D62
      GvG_NotMatchUnit = 28003, // 0x00006D63
      GvG_ExceedUnitUsedCount = 28004, // 0x00006D64
      GVG_InPreparePeriod = 28005, // 0x00006D65
      GvG_AlreadyDeclare = 28010, // 0x00006D6A
      GvG_DeclareLimitOver = 28011, // 0x00006D6B
      GvG_DeclareOutOfPeriod = 28012, // 0x00006D6C
      GvG_CanNotAuthorityInDeclare = 28013, // 0x00006D6D
      GvG_NotAdjacentNode = 28016, // 0x00006D70
      GvG_NotDeclareNode = 28017, // 0x00006D71
      GvG_NotDeclareForOnTheDay = 28018, // 0x00006D72
      GvG_IsDeclareCoolTime = 28019, // 0x00006D73
      GvG_MaxDefense = 28020, // 0x00006D74
      GvG_HasNotDeclare = 28021, // 0x00006D75
      GvG_CanNotModifiedOffenseParty = 28022, // 0x00006D76
      GvG_NotOffensePartyEntryForOnTheDay = 28023, // 0x00006D77
      GvG_HasNotCapture = 28030, // 0x00006D7E
      GvG_NotDefensePartyEntryForOnTheDay = 28031, // 0x00006D7F
      GvG_NotReachedDefenseUnitMinNum = 28032, // 0x00006D80
      GvG_BtlOutOfPeriod = 28040, // 0x00006D88
      GvG_CanNotAttack = 28041, // 0x00006D89
      GvG_AlreadyBeat = 28042, // 0x00006D8A
      GvG_OtherUserAttacking = 28043, // 0x00006D8B
      GvG_HasCapture = 28044, // 0x00006D8C
      GvG_CanNotCapture = 28045, // 0x00006D8D
      GvG_IsCoolTime = 28046, // 0x00006D8E
      GvG_NoAttackParty = 28047, // 0x00006D8F
      GvG_BattleHalfTime = 28048, // 0x00006D90
      Rune_CanNotEquip = 29001, // 0x00007149
      Rune_CanNotEquipToSlot = 29002, // 0x0000714A
      Rune_IsUpperLimitForEnforce = 29003, // 0x0000714B
      Rune_NotEnoughMaterial = 29004, // 0x0000714C
      Rune_NotEnoughCurrency = 29005, // 0x0000714D
      Rune_NotHaveRune = 29006, // 0x0000714E
      Rune_CanNotSetSameRune = 29007, // 0x0000714F
      Rune_IsUpperLimitForEvo = 29008, // 0x00007150
      Rune_CanNotEvoEnforceShort = 29009, // 0x00007151
      Rune_IncludesEquippedRunes = 29010, // 0x00007152
      Rune_IsRuneStorageFull = 29011, // 0x00007153
      Rune_NotFoundEvoSlot = 29012, // 0x00007154
      Rune_IsUpperLimitForStorage = 29013, // 0x00007155
      Rune_NotEnoughCoin = 29014, // 0x00007156
      Rune_IsFavorite = 29015, // 0x00007157
      WorldRaid_OutOfPeriod = 30000, // 0x00007530
      WorldRaid_EnabletimeOutOfPeriod = 30001, // 0x00007531
      WorldRaid_IncorrectParam = 30002, // 0x00007532
      WorldRaid_NotFound = 30003, // 0x00007533
      WorldRaid_AlreadyBeat = 30004, // 0x00007534
      WorldRaid_NotEnableAP = 30005, // 0x00007535
      WorldRaid_AlreadyUsedUnit = 30006, // 0x00007536
      WorldRaid_AlreadyBtlEnd = 30007, // 0x00007537
      WorldRaid_NotAppearLastBoss = 30008, // 0x00007538
    }

    public enum RequestResults
    {
      Success,
      Failure,
      Retry,
      Back,
      Timeout,
      Maintenance,
      VersionMismatch,
      InvalidSession,
      IllegalParam,
      ServerNotify,
      ServerNotifyAndGoToHome,
      ServerNotifyAndReloadScene,
    }

    public delegate void ResponseCallback(WWWResult result);

    public enum EConnectMode
    {
      Online,
      Offline,
    }
  }
}
