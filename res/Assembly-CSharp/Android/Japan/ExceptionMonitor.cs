// Decompiled with JetBrains decompiler
// Type: ExceptionMonitor
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

[AddComponentMenu("")]
public class ExceptionMonitor : MonoBehaviour
{
  private static ExceptionMonitor mInstnace;
  private bool bMessageDraw;

  public static void Start()
  {
    if (!((UnityEngine.Object) ExceptionMonitor.mInstnace == (UnityEngine.Object) null))
      return;
    ExceptionMonitor.mInstnace = new GameObject(nameof (ExceptionMonitor)).AddComponent<ExceptionMonitor>();
  }

  private void Awake()
  {
    UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object) this.gameObject);
    this.hideFlags = HideFlags.HideAndDontSave;
    Application.logMessageReceived += new Application.LogCallback(this.HandleLog);
  }

  private void HandleLog(string logString, string stackTrace, LogType type)
  {
    if (type != LogType.Exception || this.bMessageDraw)
      return;
    if (logString.IndexOf("DllNotFoundException:") >= 0)
      this.MessageBoxDLL();
    else
      this.MessageBoxDefault();
  }

  private void MessageBoxDefault()
  {
    this.bMessageDraw = true;
    EmbedSystemMessage.Create(LocalizedText.Get("embed.APP_EXCEPTION"), (EmbedSystemMessage.SystemMessageEvent) (yes =>
    {
      this.bMessageDraw = false;
      Application.Quit();
    }));
  }

  private void MessageBoxDLL()
  {
    this.bMessageDraw = true;
    EmbedSystemMessageEx embedSystemMessageEx = EmbedSystemMessageEx.Create(LocalizedText.Get("embed.APP_DLL_EXCEPTION"));
    embedSystemMessageEx.AddButton(LocalizedText.Get("embed.APP_DLL_SUPPORT"), false, (EmbedSystemMessageEx.SystemMessageEvent) (ok => Application.OpenURL("https://al.fg-games.co.jp/news/206/")));
    embedSystemMessageEx.AddButton(LocalizedText.Get("embed.APP_DLL_QUIT"), true, (EmbedSystemMessageEx.SystemMessageEvent) (ok =>
    {
      this.bMessageDraw = false;
      Application.Quit();
    }));
  }
}
