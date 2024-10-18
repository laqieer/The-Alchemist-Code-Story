// Decompiled with JetBrains decompiler
// Type: SRPG.HomeUnitManager
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(5, "ユニットリストを開く", FlowNode.PinTypes.Input, 5)]
  [FlowNode.Pin(10, "Refresh", FlowNode.PinTypes.Input, 10)]
  [FlowNode.Pin(11, "ユニット選択", FlowNode.PinTypes.Input, 11)]
  [FlowNode.Pin(12, "切り替え演出再生", FlowNode.PinTypes.Input, 12)]
  [FlowNode.Pin(50, "ユニット選択終了", FlowNode.PinTypes.Output, 50)]
  [FlowNode.Pin(51, "ユニットリストを開く", FlowNode.PinTypes.Output, 51)]
  public class HomeUnitManager : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_OPEN_UNITLIST = 5;
    private const int PIN_IN_REFRESH = 10;
    private const int PIN_IN_SELECT = 11;
    private const int PIN_IN_PLAY_EFFECT = 12;
    private const int PIN_OT_SELECTED = 50;
    private const int PIN_OT_OPEN_UNITLIST = 51;
    [Header("3Dモデルとカメラの管理ROOTの移動先のGameObjectID")]
    [SerializeField]
    private string HOME_UNIT_PREVIEW_ROOT = "HomeUnitPreviewRoot";
    [Header("選択されているユニットの2D背景を表示するImage")]
    [SerializeField]
    private RawImage m_BGUnitImage;
    [Header("選択されているユニットの3D表示空間")]
    [SerializeField]
    private Transform m_UnitPreviewParent;
    [Header("3D表示空間とカメラを管理しているRoot")]
    [SerializeField]
    private UnitPreviewForRT m_PreviewWorkObject;
    [Header("ユニットのタップリアクション領域")]
    [SerializeField]
    private SRPG_Button ReactionButton;
    [Header("ユニット切り替え時に挟むAnimator")]
    [SerializeField]
    private Animator LoadAnimation;
    [Header("タップ時に再生するボイスのcueID")]
    [SerializeField]
    private string REACTION_VOICE = "chara_0017";
    private UnitPreview m_HomeUnitPreview;
    private MySound.Voice m_UnitVoice;
    private bool m_Initalized;
    private bool m_OpenUnitList;

    public void Activated(int pinID)
    {
      switch (pinID)
      {
        case 5:
          this.m_OpenUnitList = true;
          FlowNode_GameObject.ActivateOutputLinks((Component) this, 51);
          break;
        case 10:
          UnitData unit = (UnitData) null;
          if (PlayerPrefsUtility.HasKey(PlayerPrefsUtility.PREFS_KEY_LAST_SELECT_HOME_UNIT))
            unit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID(PlayerPrefsUtility.GetString(PlayerPrefsUtility.PREFS_KEY_LAST_SELECT_HOME_UNIT, string.Empty));
          if (unit == null)
            unit = MonoSingleton<GameManager>.Instance.Player.FindUnitDataByUnitID("UN_V2_LOGI");
          this.Refresh(unit);
          break;
        case 11:
          if (!this.m_OpenUnitList)
            break;
          this.m_OpenUnitList = false;
          this.ChangeUnitPreview();
          break;
        case 12:
          this.PlayChangeUnit();
          break;
      }
    }

    private void Awake()
    {
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ReactionButton, (UnityEngine.Object) null))
        return;
      // ISSUE: method pointer
      ((UnityEvent) this.ReactionButton.onClick).AddListener(new UnityAction((object) this, __methodptr(PlayReaction)));
    }

    private void OnDestroy()
    {
      this.DestroyPreviewObject();
      if (this.m_UnitVoice == null)
        return;
      this.m_UnitVoice.StopAll();
      this.m_UnitVoice.Cleanup();
      this.m_UnitVoice = (MySound.Voice) null;
    }

    private void Refresh(UnitData unit)
    {
      if (unit == null)
      {
        DebugUtility.LogError("unitが指定されていません.");
      }
      else
      {
        if (!this.m_Initalized)
        {
          GameObject gameObject = GameObjectID.FindGameObject(this.HOME_UNIT_PREVIEW_ROOT);
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) gameObject, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PreviewWorkObject, (UnityEngine.Object) null))
            ((Component) this.m_PreviewWorkObject).transform.SetParent(gameObject.transform.parent, false);
        }
        this.SetupUnitVoice(unit);
        this.StartCoroutine(this.RefreshUnitImage(unit));
        this.RefreshUnitPreview(unit);
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_PreviewWorkObject, (UnityEngine.Object) null))
        {
          this.m_PreviewWorkObject.SetUnitController(this.m_HomeUnitPreview);
          this.m_PreviewWorkObject.Refresh();
        }
        this.PlayChangeVoice();
      }
    }

    private void RefreshUnitPreview(UnitData unit)
    {
      if (unit == null || UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_UnitPreviewParent, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("指定されたunitがない、もしくは3DモデルのParentが指定されていません.");
      }
      else
      {
        this.DestroyPreviewObject();
        this.m_HomeUnitPreview = this.CreatePreview(unit);
        if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_HomeUnitPreview, (UnityEngine.Object) null))
          return;
        this.StartCoroutine(this.WaitLoadUnitPreview());
      }
    }

    [DebuggerHidden]
    private IEnumerator WaitLoadUnitPreview()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new HomeUnitManager.\u003CWaitLoadUnitPreview\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }

    private UnitPreview CreatePreview(UnitData unit)
    {
      if (unit == null)
      {
        DebugUtility.LogError("unitが指定されていません.");
        return (UnitPreview) null;
      }
      GameObject gameObject = new GameObject("Unit", new System.Type[1]
      {
        typeof (UnitPreview)
      });
      gameObject.SetActive(false);
      UnitPreview component = gameObject.GetComponent<UnitPreview>();
      component.DefaultLayer = GameUtility.LayerCH;
      component.SetupUnit(unit);
      ((Component) component).transform.SetParent(this.m_UnitPreviewParent, false);
      gameObject.SetActive(true);
      return component;
    }

    private void DestroyPreviewObject()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_HomeUnitPreview, (UnityEngine.Object) null))
        UnityEngine.Object.Destroy((UnityEngine.Object) ((Component) this.m_HomeUnitPreview).gameObject);
      this.m_HomeUnitPreview = (UnitPreview) null;
    }

    [DebuggerHidden]
    private IEnumerator RefreshUnitImage(UnitData unit)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new HomeUnitManager.\u003CRefreshUnitImage\u003Ec__Iterator1()
      {
        unit = unit,
        \u0024this = this
      };
    }

    private void ChangeUnitPreview()
    {
      UnitData unitData = MonoSingleton<GameManager>.Instance.Player.GetUnitData((long) GlobalVars.SelectedUnitUniqueID);
      if (unitData == null)
      {
        DebugUtility.LogError("指定されたiidのユニットがありませんでした.");
      }
      else
      {
        this.Refresh(unitData);
        PlayerPrefsUtility.SetString(PlayerPrefsUtility.PREFS_KEY_LAST_SELECT_HOME_UNIT, unitData.UnitParam.iname);
        GlobalVars.SelectedUnitUniqueID.Reset();
      }
    }

    private void PlayChangeUnit()
    {
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.m_HomeUnitPreview, (UnityEngine.Object) null))
        this.m_HomeUnitPreview.PlayAction = true;
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.LoadAnimation, (UnityEngine.Object) null))
        return;
      this.LoadAnimation.SetTrigger("close");
    }

    private void PlayReaction()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.m_HomeUnitPreview, (UnityEngine.Object) null))
      {
        DebugUtility.LogError("ユニットが表示されていません.");
      }
      else
      {
        this.m_HomeUnitPreview.PlayAction = true;
        this.PlayReactionVoice();
      }
    }

    private void SetupUnitVoice(UnitData unit)
    {
      if (unit == null)
      {
        DebugUtility.LogError("UnitDataが指定されていません.");
      }
      else
      {
        if (this.m_UnitVoice != null)
        {
          this.m_UnitVoice.StopAll();
          this.m_UnitVoice.Cleanup();
          this.m_UnitVoice = (MySound.Voice) null;
        }
        string skinVoiceSheetName = unit.GetUnitSkinVoiceSheetName();
        if (string.IsNullOrEmpty(skinVoiceSheetName))
        {
          DebugUtility.LogError("UnitDataにボイス設定がありません.");
        }
        else
        {
          string sheetName = "VO_" + skinVoiceSheetName;
          string cueNamePrefix = unit.GetUnitSkinVoiceCueName() + "_";
          this.m_UnitVoice = new MySound.Voice(sheetName, skinVoiceSheetName, cueNamePrefix);
        }
      }
    }

    private void PlayReactionVoice() => this.PlayUnitVoice(this.REACTION_VOICE);

    private void PlayChangeVoice() => this.PlayUnitVoice(UnitParam.GetCurrentHourVoice());

    private void PlayUnitVoice(string name)
    {
      if (this.m_UnitVoice == null)
        DebugUtility.LogError("UnitVoiceが存在しません.");
      else
        this.m_UnitVoice.Play(name, is_stopplay: true);
    }
  }
}
