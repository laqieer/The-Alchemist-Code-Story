// Decompiled with JetBrains decompiler
// Type: ApplicationEventHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using SRPG;
using UnityEngine;

public class ApplicationEventHandler : MonoBehaviour
{
  private bool m_IsQuiting;
  private EmbedWindowYesNo m_QuitWindow;

  public void OpenQuitWindow()
  {
    if (!((UnityEngine.Object) this.m_QuitWindow == (UnityEngine.Object) null) || this.m_IsQuiting)
      return;
    this.m_QuitWindow = EmbedWindowYesNo.Create(LocalizedText.Get("embed.APP_QUIT"), new EmbedWindowYesNo.YesNoWindowEvent(this.OnApplicationQuitWindowResult));
  }

  public void OnApplicationQuitWindowResult(bool yes)
  {
    if (yes)
      this.OnDecide();
    else
      this.OnCancel();
  }

  private void OnCancel()
  {
    this.m_QuitWindow = (EmbedWindowYesNo) null;
    this.m_IsQuiting = false;
  }

  private void OnDecide()
  {
    this.m_QuitWindow = (EmbedWindowYesNo) null;
    this.m_IsQuiting = true;
    Application.Quit();
  }
}
