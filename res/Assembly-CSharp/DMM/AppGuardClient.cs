// Decompiled with JetBrains decompiler
// Type: AppGuardClient
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using SRPG;
using System;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

#nullable disable
public class AppGuardClient
{
  public static readonly OString GAMEGUARD_ERROR_EVENT = (OString) "GAMEGUARD_ERROR";
  public static AppGuardClient.EGameGuardStatus GameGuardState;
  private static bool bAppExit;
  private static bool isQuitting = false;
  private static string strMsg;
  private static uint timechk;
  private static OBool isErrorRaised;
  private const string UnityWindowClassName = "UnityWndClass";
  private static IntPtr windowHandle = IntPtr.Zero;

  [DllImport("NPGameDLL")]
  public static extern void SetCallbackToGameMon(AppGuardClient.NPGMCALLBACK fnCallback);

  [DllImport("NPGameDLL")]
  public static extern uint PreInitNPGameMonW([MarshalAs(UnmanagedType.LPWStr)] string szGameName);

  [DllImport("NPGameDLL")]
  public static extern uint InitNPGameMon();

  [DllImport("NPGameDLL")]
  public static extern bool CloseNPGameMon();

  [DllImport("NPGameDLL")]
  public static extern void SetHwndToGameMon(IntPtr hWnd);

  [DllImport("NPGameDLL")]
  public static extern bool SendUserIDToGameMonW([MarshalAs(UnmanagedType.LPWStr)] string szID);

  [DllImport("NPGameDLL")]
  public static extern uint CheckNPGameMon();

  [DllImport("NPGameDLL")]
  public static extern bool SendCSAuth3ToGameMon(
    IntPtr pbPacket,
    uint dwPacketSize,
    uint dwServerNumber);

  private static IntPtr GetActiveWindow()
  {
    AppGuardClient.GetWindowHandle();
    return AppGuardClient.windowHandle;
  }

  private static OBool isInited
  {
    get
    {
      return (OBool) (AppGuardClient.GameGuardState == AppGuardClient.EGameGuardStatus.NPGAMEMON_SUCCESS);
    }
    set
    {
    }
  }

  public static void Init(string gameObjName, string callbackMethodName)
  {
    AppGuardClient.bAppExit = false;
    // ISSUE: reference to a compiler-generated field
    if (AppGuardClient.\u003C\u003Ef__mg\u0024cache0 == null)
    {
      // ISSUE: reference to a compiler-generated field
      AppGuardClient.\u003C\u003Ef__mg\u0024cache0 = new AppGuardClient.NPGMCALLBACK(AppGuardClient.NPCALLBACK);
    }
    // ISSUE: reference to a compiler-generated field
    AppGuardClient.SetCallbackToGameMon(AppGuardClient.\u003C\u003Ef__mg\u0024cache0);
    int num = (int) AppGuardClient.PreInitNPGameMonW("TagatameJP");
    if ((AppGuardClient.GameGuardState = (AppGuardClient.EGameGuardStatus) AppGuardClient.InitNPGameMon()) != AppGuardClient.EGameGuardStatus.NPGAMEMON_SUCCESS)
    {
      Debug.LogWarning((object) AppGuardClient.GameGuardState);
      AppGuardClient.isErrorRaised = (OBool) true;
    }
    else
    {
      AppGuardClient.isInited = (OBool) true;
      AppGuardClient.SetHwndToGameMon(AppGuardClient.GetActiveWindow());
      AppGuardClient.timechk = 0U;
    }
  }

  public static void SetUserId(string _user_id)
  {
    if (!(bool) AppGuardClient.isInited)
      return;
    AppGuardClient.SendUserIDToGameMonW(_user_id);
  }

  public static void SetUniqueClientID(string clientId)
  {
  }

  public static void OnApplicationQuit(bool yes = false)
  {
    if (AppGuardClient.isQuitting)
      return;
    AppGuardClient.isQuitting = true;
    try
    {
      Debug.LogWarning((object) "[CloseNPGameMon]");
      AppGuardClient.CloseNPGameMon();
    }
    catch (Exception ex)
    {
      Debug.LogException(ex);
    }
    if (!AppGuardClient.bAppExit)
      return;
    Debug.LogError((object) AppGuardClient.strMsg);
  }

  public static void DoGameGuardCheck()
  {
    if (!(bool) AppGuardClient.isInited || (bool) AppGuardClient.isErrorRaised || ++AppGuardClient.timechk < 900U)
      return;
    AppGuardClient.timechk = 0U;
    if (!AppGuardClient.bAppExit || (AppGuardClient.GameGuardState = (AppGuardClient.EGameGuardStatus) AppGuardClient.CheckNPGameMon()) == AppGuardClient.EGameGuardStatus.NPGAMEMON_SUCCESS)
      return;
    AppGuardClient.isErrorRaised = (OBool) true;
    GlobalEvent.Invoke((string) AppGuardClient.GAMEGUARD_ERROR_EVENT, (object) (int) AppGuardClient.GameGuardState);
    AppGuardClient.SendErrorLog();
  }

  public static void SendErrorLog()
  {
    FlowNode_SendLogMessage.SendLogGenerator dict = new FlowNode_SendLogMessage.SendLogGenerator();
    dict.AddCommon(true, true, true, true);
    dict.Add("GameGuardState", AppGuardClient.GameGuardState.ToString() + string.Empty);
    FlowNode_SendLogMessage.SendLogMessage(dict, "GameGuardErrors");
  }

  private static int NPCALLBACK(uint dwMsg, uint dwArg)
  {
    AppGuardClient.EGameGuardStatus egameGuardStatus = (AppGuardClient.EGameGuardStatus) dwMsg;
    int num;
    switch (egameGuardStatus)
    {
      case AppGuardClient.EGameGuardStatus.NPGAMEMON_SPEEDHACK:
        AppGuardClient.strMsg = "NPGAMEMON_SPEEDHACK";
        AppGuardClient.bAppExit = true;
        num = 1;
        break;
      case AppGuardClient.EGameGuardStatus.NPGAMEMON_GAMEHACK_KILLED:
        AppGuardClient.strMsg = "NPGAMEMON_GAMEHACK_KILLED";
        AppGuardClient.bAppExit = true;
        num = 1;
        break;
      case AppGuardClient.EGameGuardStatus.NPGAMEMON_GAMEHACK_DETECT:
        AppGuardClient.strMsg = "NPGAMEMON_GAMEHACK_DETECT";
        AppGuardClient.bAppExit = true;
        num = 1;
        break;
      case AppGuardClient.EGameGuardStatus.NPGAMEMON_INIT_ERROR:
        AppGuardClient.strMsg = "NPGAMEMON_INIT_ERROR";
        AppGuardClient.bAppExit = true;
        num = 1;
        break;
      case AppGuardClient.EGameGuardStatus.NPGAMEMON_GAMEHACK_DOUBT:
        AppGuardClient.strMsg = "NPGAMEMON_GAMEHACK_DOUBT";
        AppGuardClient.bAppExit = true;
        num = 1;
        break;
      case AppGuardClient.EGameGuardStatus.NPGAMEMON_CHECK_CSAUTH:
        AppGuardClient.strMsg = "NPGAMEMON_CHECK_CSAUTH";
        AppGuardClient.bAppExit = true;
        num = 1;
        break;
      case AppGuardClient.EGameGuardStatus.NPGAMEMON_GAMEHACK_REPORT:
        AppGuardClient.strMsg = "NPGAMEMON_GAMEHACK_REPORT";
        AppGuardClient.bAppExit = true;
        num = 1;
        break;
      case AppGuardClient.EGameGuardStatus.NPGAMEMON_CHECK_CSAUTH3:
        AppGuardClient.strMsg = "NPGAMEMON_CHECK_CSAUTH3";
        AppGuardClient.bAppExit = true;
        num = 1;
        break;
      default:
        switch (egameGuardStatus - 1000)
        {
          case AppGuardClient.EGameGuardStatus.CUSTOM_UNDEFINED:
            AppGuardClient.strMsg = "NPGAMEMON_UNDEFINED";
            AppGuardClient.bAppExit = true;
            num = 0;
            break;
          case (AppGuardClient.EGameGuardStatus) 1:
            AppGuardClient.strMsg = "NPGAMEMON_COMM_ERROR";
            AppGuardClient.bAppExit = true;
            num = 0;
            break;
          case (AppGuardClient.EGameGuardStatus) 2:
            AppGuardClient.strMsg = "NPGAMEMON_COMM_CLOSE";
            AppGuardClient.bAppExit = true;
            num = 0;
            break;
          default:
            AppGuardClient.strMsg = "ETC (" + (object) (AppGuardClient.EGameGuardStatus) dwArg + ")";
            num = 1;
            break;
        }
        break;
    }
    Debug.LogWarning((object) AppGuardClient.strMsg);
    return num;
  }

  [DllImport("kernel32.dll")]
  private static extern uint GetCurrentThreadId();

  [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
  private static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

  [DllImport("user32.dll")]
  [return: MarshalAs(UnmanagedType.Bool)]
  private static extern bool EnumThreadWindows(
    uint dwThreadId,
    AppGuardClient.EnumWindowsProc lpEnumFunc,
    IntPtr lParam);

  private static void GetWindowHandle()
  {
    AppGuardClient.EnumThreadWindows(AppGuardClient.GetCurrentThreadId(), (AppGuardClient.EnumWindowsProc) ((hWnd, lParam) =>
    {
      StringBuilder lpString = new StringBuilder("UnityWndClass".Length + 1);
      AppGuardClient.GetClassName(hWnd, lpString, lpString.Capacity);
      if (!(lpString.ToString() == "UnityWndClass"))
        return true;
      AppGuardClient.windowHandle = hWnd;
      return false;
    }), IntPtr.Zero);
  }

  public enum EGameGuardStatus
  {
    CUSTOM_UNDEFINED = 0,
    NPGAMEMON_ERROR_EXIST = 110, // 0x0000006E
    NPGAMEMON_ERROR_CREATE = 111, // 0x0000006F
    NPGAMEMON_ERROR_NPSCAN = 112, // 0x00000070
    NPGAMEMON_ERROR_THREAD = 113, // 0x00000071
    NPGAMEMON_ERROR_INIT = 114, // 0x00000072
    NPGAMEMON_ERROR_GAME_EXIST = 115, // 0x00000073
    NPGAMEMON_ERROR_AUTH_INI = 120, // 0x00000078
    NPGAMEMON_ERROR_AUTH_NPGMUP = 121, // 0x00000079
    NPGAMEMON_ERROR_AUTH_GAMEMON = 122, // 0x0000007A
    NPGAMEMON_ERROR_AUTH_NEWUP = 123, // 0x0000007B
    NPGAMEMON_ERROR_AUTH_GAMEGUARD = 124, // 0x0000007C
    NPGAMEMON_ERROR_AUTH_DLL = 125, // 0x0000007D
    NPGAMEMON_ERROR_DECRYPT = 130, // 0x00000082
    NPGAMEMON_ERROR_CORRUPT_INI = 141, // 0x0000008D
    NPGAMEMON_ERROR_CORRUPT_INI2 = 142, // 0x0000008E
    NPGAMEMON_ERROR_NFOUND_INI = 150, // 0x00000096
    NPGAMEMON_ERROR_NFOUND_NPGMUP = 151, // 0x00000097
    NPGAMEMON_ERROR_NFOUND_NEWUP = 152, // 0x00000098
    NPGAMEMON_ERROR_NFOUND_GG = 153, // 0x00000099
    NPGAMEMON_ERROR_NFOUND_GM = 154, // 0x0000009A
    NPGAMEMON_ERROR_CRYPTOAPI = 155, // 0x0000009B
    NPGAMEMON_ERROR_COMM = 160, // 0x000000A0
    NPGAMEMON_ERROR_EXECUTE = 170, // 0x000000AA
    NPGAMEMON_ERROR_EVENT = 171, // 0x000000AB
    NPGAMEMON_ERROR_NPGMUP = 180, // 0x000000B4
    NPGAMEMON_ERROR_MOVE_INI = 191, // 0x000000BF
    NPGAMEMON_ERROR_MOVE_NEWUP = 192, // 0x000000C0
    NPGAMEMON_ERROR_ILLEGAL_PRG = 200, // 0x000000C8
    NPGAMEMON_ERROR_GAMEMON = 210, // 0x000000D2
    NPGAMEMON_ERROR_SPEEDCHECK = 220, // 0x000000DC
    NPGAMEMON_ERROR_GAMEGUARD = 230, // 0x000000E6
    _255 = 255, // 0x000000FF
    NPGMUP_ERROR_PARAM = 320, // 0x00000140
    NPGMUP_ERROR_INIT = 330, // 0x0000014A
    NPGMUP_ERROR_DOWNCFG = 340, // 0x00000154
    NPGMUP_ERROR_ABORT = 350, // 0x0000015E
    NPGMUP_ERROR_AUTH = 360, // 0x00000168
    NPGMUP_ERROR_AUTH_INI = 361, // 0x00000169
    NPGMUP_ERROR_DECRYPT = 370, // 0x00000172
    NPGMUP_ERROR_CONNECT = 380, // 0x0000017C
    NPGMUP_ERROR_INI = 390, // 0x00000186
    NPGG_ERROR_COLLISION = 500, // 0x000001F4
    _620 = 620, // 0x0000026C
    _660 = 660, // 0x00000294
    NPGAMEMON_UNDEFINED = 1000, // 0x000003E8
    NPGAMEMON_COMM_ERROR = 1001, // 0x000003E9
    NPGAMEMON_COMM_CLOSE = 1002, // 0x000003EA
    NPGAMEMON_SPEEDHACK = 1011, // 0x000003F3
    NPGAMEMON_GAMEHACK_KILLED = 1012, // 0x000003F4
    NPGAMEMON_GAMEHACK_DETECT = 1013, // 0x000003F5
    NPGAMEMON_INIT_ERROR = 1014, // 0x000003F6
    NPGAMEMON_GAMEHACK_DOUBT = 1015, // 0x000003F7
    NPGAMEMON_CHECK_CSAUTH = 1016, // 0x000003F8
    NPGAMEMON_CHECK_CSAUTH2 = 1017, // 0x000003F9
    NPGAMEMON_GAMEHACK_REPORT = 1018, // 0x000003FA
    NPGAMEMON_CHECK_CSAUTH3 = 1019, // 0x000003FB
    NPGAMEMON_SUCCESS = 1877, // 0x00000755
  }

  public delegate int NPGMCALLBACK(uint dwMsg, uint dwArg);

  public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
}
