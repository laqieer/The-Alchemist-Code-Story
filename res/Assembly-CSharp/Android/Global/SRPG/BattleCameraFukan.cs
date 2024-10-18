// Decompiled with JetBrains decompiler
// Type: SRPG.BattleCameraFukan
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SRPG
{
  public class BattleCameraFukan : MonoBehaviour
  {
    public BattleCameraControl BattleCameraControl;
    public GameObject DefaultButton;
    public GameObject UpViewButton;
    private bool m_Disp;

    public bool isDisp
    {
      get
      {
        return this.m_Disp;
      }
    }

    private void Start()
    {
      if ((UnityEngine.Object) this.DefaultButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.DefaultButton, (BattleCameraFukan.ClickEvent) (() => this.OnClick(BattleCameraFukan.ButtonType.DEFAULT)));
      if ((UnityEngine.Object) this.UpViewButton != (UnityEngine.Object) null)
        this.SetButtonEvent(this.UpViewButton, (BattleCameraFukan.ClickEvent) (() => this.OnClick(BattleCameraFukan.ButtonType.UPVIEW)));
      this.SetDisp(false);
    }

    private void Update()
    {
    }

    private Button SetButtonEvent(GameObject go, BattleCameraFukan.ClickEvent callback)
    {
      Button component = go.GetComponent<Button>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.onClick.AddListener(new UnityAction(callback.Invoke));
      return component;
    }

    public void SetCameraMode(SceneBattle.CameraMode mode)
    {
      SceneBattle instance = SceneBattle.Instance;
      if (!((UnityEngine.Object) instance != (UnityEngine.Object) null))
        return;
      instance.OnCameraModeChange(mode);
      switch (mode)
      {
        case SceneBattle.CameraMode.DEFAULT:
          if ((UnityEngine.Object) this.DefaultButton != (UnityEngine.Object) null)
            this.DefaultButton.SetActive(false);
          if ((UnityEngine.Object) this.UpViewButton != (UnityEngine.Object) null)
            this.UpViewButton.SetActive(true);
          if (!((UnityEngine.Object) this.BattleCameraControl != (UnityEngine.Object) null))
            break;
          this.BattleCameraControl.SetDisp(true);
          break;
        case SceneBattle.CameraMode.UPVIEW:
          if ((UnityEngine.Object) this.DefaultButton != (UnityEngine.Object) null)
            this.DefaultButton.SetActive(true);
          if ((UnityEngine.Object) this.UpViewButton != (UnityEngine.Object) null)
            this.UpViewButton.SetActive(false);
          if (!((UnityEngine.Object) this.BattleCameraControl != (UnityEngine.Object) null))
            break;
          this.BattleCameraControl.SetDisp(false);
          break;
      }
    }

    public void SetDisp(bool value)
    {
      Animator component = this.GetComponent<Animator>();
      if ((UnityEngine.Object) component != (UnityEngine.Object) null)
        component.SetBool("open", value);
      if (value)
      {
        SceneBattle instance = SceneBattle.Instance;
        if ((UnityEngine.Object) instance != (UnityEngine.Object) null)
        {
          if (instance.isUpView)
          {
            if ((UnityEngine.Object) this.DefaultButton != (UnityEngine.Object) null)
              this.DefaultButton.SetActive(true);
            if ((UnityEngine.Object) this.UpViewButton != (UnityEngine.Object) null)
              this.UpViewButton.SetActive(false);
          }
          else
          {
            if ((UnityEngine.Object) this.DefaultButton != (UnityEngine.Object) null)
              this.DefaultButton.SetActive(false);
            if ((UnityEngine.Object) this.UpViewButton != (UnityEngine.Object) null)
              this.UpViewButton.SetActive(true);
          }
        }
        else
        {
          if ((UnityEngine.Object) this.DefaultButton != (UnityEngine.Object) null)
            this.DefaultButton.SetActive(true);
          if ((UnityEngine.Object) this.UpViewButton != (UnityEngine.Object) null)
            this.UpViewButton.SetActive(true);
        }
      }
      else
      {
        if ((UnityEngine.Object) this.DefaultButton != (UnityEngine.Object) null)
          this.DefaultButton.SetActive(false);
        if ((UnityEngine.Object) this.UpViewButton != (UnityEngine.Object) null)
          this.UpViewButton.SetActive(false);
      }
      this.m_Disp = value;
    }

    private void OnClick(BattleCameraFukan.ButtonType buttonType)
    {
      if (!((UnityEngine.Object) SceneBattle.Instance != (UnityEngine.Object) null))
        return;
      if (buttonType == BattleCameraFukan.ButtonType.DEFAULT)
      {
        this.SetCameraMode(SceneBattle.CameraMode.DEFAULT);
      }
      else
      {
        if (buttonType != BattleCameraFukan.ButtonType.UPVIEW)
          return;
        this.SetCameraMode(SceneBattle.CameraMode.UPVIEW);
      }
    }

    public void OnEventCall(string key, string value)
    {
      string key1 = key;
      if (key1 == null)
        return;
      // ISSUE: reference to a compiler-generated field
      if (BattleCameraFukan.\u003C\u003Ef__switch\u0024mapC == null)
      {
        // ISSUE: reference to a compiler-generated field
        BattleCameraFukan.\u003C\u003Ef__switch\u0024mapC = new Dictionary<string, int>(1)
        {
          {
            "DISP",
            0
          }
        };
      }
      int num;
      // ISSUE: reference to a compiler-generated field
      if (!BattleCameraFukan.\u003C\u003Ef__switch\u0024mapC.TryGetValue(key1, out num) || num != 0)
        return;
      if (value == "on")
        this.SetDisp(true);
      else
        this.SetDisp(false);
    }

    private enum ButtonType
    {
      DEFAULT,
      UPVIEW,
    }

    private delegate void ClickEvent();
  }
}
