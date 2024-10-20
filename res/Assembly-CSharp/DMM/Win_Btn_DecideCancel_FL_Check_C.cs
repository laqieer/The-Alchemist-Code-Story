﻿// Decompiled with JetBrains decompiler
// Type: Win_Btn_DecideCancel_FL_Check_C
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
[AddComponentMenu("UI/Drafts/Win_Btn_DecideCancel_FL_Check_C")]
public class Win_Btn_DecideCancel_FL_Check_C : UIDraft
{
  [UIDraft.AutoGenerated]
  public Text Text_Message;
  [UIDraft.AutoGenerated]
  public Toggle Toggle;
  [UIDraft.AutoGenerated]
  public Button Btn_Cancel;
  [UIDraft.AutoGenerated]
  public Button Btn_Decide;
  public UIUtility.DialogResultEvent OnClickYes;
  public UIUtility.DialogResultEvent OnClickNo;
  [NonSerialized]
  public string ConfirmID;

  private void OnWindowClose(UIWindow window)
  {
    Object.DestroyImmediate((Object) ((Component) ((Component) this).GetComponentInParent<Canvas>()).gameObject);
  }

  private void BeginClose()
  {
    UIUtility.PopCanvas(true);
    UIWindow component = ((Component) this).GetComponent<UIWindow>();
    component.OnWindowClose = new UIWindow.WindowEvent(this.OnWindowClose);
    component.Close();
  }

  private void OnClickButton(GameObject obj)
  {
    this.BeginClose();
    bool flag = Object.op_Equality((Object) obj, (Object) ((Component) this.Btn_Decide).gameObject);
    if (this.ConfirmID != null && this.ConfirmID.Length > 0)
    {
      int num1 = 0;
      if (PlayerPrefsUtility.HasKey(this.ConfirmID))
        num1 = PlayerPrefsUtility.GetInt(this.ConfirmID);
      int num2 = !this.Toggle.isOn ? 0 : 1;
      if (num1 != num2)
        PlayerPrefsUtility.SetInt(this.ConfirmID, num2);
    }
    if (flag)
    {
      if (this.OnClickYes == null)
        return;
      this.OnClickYes(((Component) this).gameObject);
    }
    else
    {
      if (this.OnClickNo == null)
        return;
      this.OnClickNo(((Component) this).gameObject);
    }
  }

  private void Awake()
  {
    UIUtility.AddEventListener(((Component) this.Btn_Decide).gameObject, (UnityEvent) this.Btn_Decide.onClick, new UIUtility.EventListener(this.OnClickButton));
    UIUtility.AddEventListener(((Component) this.Btn_Cancel).gameObject, (UnityEvent) this.Btn_Cancel.onClick, new UIUtility.EventListener(this.OnClickButton));
  }
}