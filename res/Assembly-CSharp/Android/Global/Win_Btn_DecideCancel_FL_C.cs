﻿// Decompiled with JetBrains decompiler
// Type: Win_Btn_DecideCancel_FL_C
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[AddComponentMenu("UI/Drafts/Win_Btn_DecideCancel_FL_C")]
public class Win_Btn_DecideCancel_FL_C : UIDraft
{
  [UIDraft.AutoGenerated]
  public Text Text_Message;
  [UIDraft.AutoGenerated]
  public Button Btn_Cancel;
  [UIDraft.AutoGenerated]
  public Button Btn_Decide;
  public Text Txt_No;
  public Text Txt_Yes;
  public UIUtility.DialogResultEvent OnClickYes;
  public UIUtility.DialogResultEvent OnClickNo;

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
    if ((Object) obj == (Object) this.Btn_Decide.gameObject)
    {
      if (this.OnClickYes == null)
        return;
      this.OnClickYes(this.gameObject);
    }
    else
    {
      if (this.OnClickNo == null)
        return;
      this.OnClickNo(this.gameObject);
    }
  }

  private void Awake()
  {
    UIUtility.AddEventListener(this.Btn_Decide.gameObject, (UnityEvent) this.Btn_Decide.onClick, new UIUtility.EventListener(this.OnClickButton));
    UIUtility.AddEventListener(this.Btn_Cancel.gameObject, (UnityEvent) this.Btn_Cancel.onClick, new UIUtility.EventListener(this.OnClickButton));
  }
}
