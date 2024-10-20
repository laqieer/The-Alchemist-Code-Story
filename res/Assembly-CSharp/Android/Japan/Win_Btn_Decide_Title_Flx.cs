﻿// Decompiled with JetBrains decompiler
// Type: Win_Btn_Decide_Title_Flx
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("UI/Drafts/Win_Btn_Decide_Title_Flx")]
public class Win_Btn_Decide_Title_Flx : UIDraft
{
  [UIDraft.AutoGenerated]
  public Text Text_Title;
  [UIDraft.AutoGenerated]
  public Text Text_Message;
  [UIDraft.AutoGenerated]
  public Button Btn_Decide;
  public UIUtility.DialogResultEvent OnClickYes;

  private void OnWindowClose(UIWindow window)
  {
    Object.DestroyImmediate((Object) this.GetComponentInParent<Canvas>().gameObject);
  }

  public void BeginClose()
  {
    UIUtility.PopCanvas(true);
    UIWindow component = this.GetComponent<UIWindow>();
    component.OnWindowClose = new UIWindow.WindowEvent(this.OnWindowClose);
    component.Close();
  }

  private void OnClickButton(GameObject obj)
  {
    this.BeginClose();
    if (!((Object) obj == (Object) this.Btn_Decide.gameObject) || this.OnClickYes == null)
      return;
    this.OnClickYes(this.gameObject);
  }

  private void Awake()
  {
    UIUtility.AddEventListener(this.Btn_Decide.gameObject, (UnityEvent) this.Btn_Decide.onClick, new UIUtility.EventListener(this.OnClickButton));
  }
}