// Decompiled with JetBrains decompiler
// Type: SRPG.FirstChargeButton
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [FlowNode.Pin(0, "Click FirstChargeEvent", FlowNode.PinTypes.Input, 0)]
  public class FirstChargeButton : AppealItemBase, IFlowInterface
  {
    private static readonly string SPRITES_PATH = "AppealSprites/charge";
    private static readonly string MASTER_PATH = "Data/appeal/AppealCharge";
    private static readonly string CHARGEINFO_PATH = "UI/ChargeInfo";
    private string m_CurrentAppealId = string.Empty;
    private List<AppealChargeParam> m_ValidAppeal = new List<AppealChargeParam>();
    private Dictionary<string, Sprite> mCacheAppealSprites = new Dictionary<string, Sprite>();
    private static readonly int INPUT_CLICK_CHARGE_EVENT;
    [SerializeField]
    private GameObject Badge;
    [SerializeField]
    private GameObject Ballon;
    [SerializeField]
    private bool IsDebug;
    private int m_CurrentAppealIndex;
    private bool m_IsSpriteLoaded;
    private GameObject m_ChargeInfoView;

    protected override void Awake()
    {
      base.Awake();
      if (this.IsDebug)
        return;
      this.gameObject.SetActive(false);
    }

    protected override void Start()
    {
      base.Start();
      if ((UnityEngine.Object) this.Ballon != (UnityEngine.Object) null)
        this.Ballon.gameObject.SetActive(false);
      if ((UnityEngine.Object) this.Badge != (UnityEngine.Object) null)
        this.Badge.gameObject.SetActive(false);
      this.InitMaster();
    }

    private void OnEnable()
    {
      this.InitMaster();
    }

    protected override void Update()
    {
      base.Update();
      if (!this.m_IsSpriteLoaded)
        return;
      this.Refresh();
    }

    protected override void Refresh()
    {
      if (MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus == -1)
        return;
      if (this.IsValidState(FirstChargeState.CampaignFinished) || this.IsValidState(FirstChargeState.NotValidCampaign))
      {
        this.gameObject.SetActive(false);
      }
      else
      {
        if ((UnityEngine.Object) this.Badge != (UnityEngine.Object) null)
          this.Badge.SetActive(this.IsValidState(FirstChargeState.Purchased));
        if ((UnityEngine.Object) this.Ballon != (UnityEngine.Object) null)
          this.Ballon.SetActive(!this.IsValidState(FirstChargeState.Purchased));
        this.SetCurrentAppeal();
        base.Refresh();
      }
    }

    public void Activated(int pinID)
    {
      if (pinID != FirstChargeButton.INPUT_CLICK_CHARGE_EVENT)
        return;
      if (this.IsDebug)
        this.StartCoroutine(this.CreateResultWindow());
      else
        this.StartCoroutine(this.CreateInfoView());
    }

    [DebuggerHidden]
    private IEnumerator CreateInfoView()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FirstChargeButton.\u003CCreateInfoView\u003Ec__Iterator0() { \u0024this = this };
    }

    private void InitMaster()
    {
      if (this.m_ValidAppeal != null && this.m_ValidAppeal.Count > 0)
      {
        if (this.m_IsSpriteLoaded)
          return;
        this.StartCoroutine(this.LoadAppealSprite(FirstChargeButton.SPRITES_PATH));
      }
      else
      {
        if (!this.LoadAppealMaster(FirstChargeButton.MASTER_PATH) || this.m_IsSpriteLoaded)
          return;
        this.StartCoroutine(this.LoadAppealSprite(FirstChargeButton.SPRITES_PATH));
      }
    }

    private bool IsValidState(FirstChargeState _state)
    {
      return (FirstChargeState) MonoSingleton<GameManager>.Instance.Player.FirstChargeStatus == _state;
    }

    private void SetCurrentAppeal()
    {
      if (this.m_ValidAppeal == null)
        return;
      long serverTime = Network.GetServerTime();
      for (int index = 0; index < this.m_ValidAppeal.Count; ++index)
      {
        if (this.m_ValidAppeal[index].start_at < serverTime && this.m_ValidAppeal[index].end_at > serverTime)
        {
          this.m_CurrentAppealId = this.m_ValidAppeal[index].appeal_id;
          this.m_CurrentAppealIndex = index;
          break;
        }
      }
      if (string.IsNullOrEmpty(this.m_CurrentAppealId) || !this.mCacheAppealSprites.ContainsKey(this.m_CurrentAppealId))
        return;
      this.AppealSprite = this.mCacheAppealSprites[this.m_CurrentAppealId];
    }

    private bool LoadAppealMaster(string _path)
    {
      bool flag = false;
      if (!string.IsNullOrEmpty(_path))
      {
        string src = AssetManager.LoadTextData(_path);
        if (!string.IsNullOrEmpty(src))
        {
          try
          {
            JSON_AppealChargeParam[] jsonArray = JSONParser.parseJSONArray<JSON_AppealChargeParam>(src);
            if (jsonArray == null)
              throw new InvalidJSONException();
            long serverTime = Network.GetServerTime();
            foreach (JSON_AppealChargeParam _json in jsonArray)
            {
              AppealChargeParam appealChargeParam = new AppealChargeParam();
              appealChargeParam.Deserialize(_json);
              if (appealChargeParam != null && appealChargeParam.end_at >= serverTime)
                this.m_ValidAppeal.Add(appealChargeParam);
            }
            flag = true;
          }
          catch (Exception ex)
          {
            DebugUtility.LogError(ex.ToString());
          }
        }
      }
      return flag;
    }

    [DebuggerHidden]
    private IEnumerator LoadAppealSprite(string _path)
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new FirstChargeButton.\u003CLoadAppealSprite\u003Ec__Iterator1() { _path = _path, \u0024this = this };
    }

    [DebuggerHidden]
    private IEnumerator CreateResultWindow()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FirstChargeButton.\u003CCreateResultWindow\u003Ec__Iterator2 windowCIterator2 = new FirstChargeButton.\u003CCreateResultWindow\u003Ec__Iterator2();
      return (IEnumerator) windowCIterator2;
    }
  }
}
