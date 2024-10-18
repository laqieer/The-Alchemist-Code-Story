// Decompiled with JetBrains decompiler
// Type: SRPG.GachaUnitShard
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class GachaUnitShard : MonoBehaviour
  {
    private StateMachine<GachaUnitShard> mState;
    public float WaitGaugeActionStarted = 1f;
    public float WaitGaugeActioned = 1f;
    public float WaitRebirthStarActioned = 1f;
    public float WaitRebirthTextActioned = 1f;
    public float GaugeUpAnimSpan;
    public int GaugeUpAnimSoundSpan = 5;
    public string[] GaugeUpAnimSoundList;
    private string[] DefaultGaugeUpAnimSoundList = new string[5]
    {
      "SE_1027",
      "SE_1028",
      "SE_1029",
      "SE_1030",
      "SE_1031"
    };
    public GameObject gauge_bar;
    public GameObject rebirthstar_root;
    public GameObject rebirthstar_template;
    public RawImage unit_img;
    public RawImage unit_img_blur01;
    public RawImage unit_img_blur02;
    public RawPolyImage unit_icon;
    public Image_Transparent element_icon;
    public Text ShardName;
    public Text JobName;
    public Text JobComment;
    public Text ShardNext;
    public Text ShardCurrent;
    public SliderAnimator ShardGauge;
    public GameObject Rebirthtext_root;
    private string[] mJobNameList;
    private bool mUnlockUnit;
    private bool mNotUnlockUnit = true;
    private const string TRIGGER_SHARDGAUGE_CLOSE = "close";
    private const string TRIGGER_JOBOPEN_START = "jobopen_start";
    private const string TRIGGER_JOBOPEN_NEXT = "job_next";
    private const string TRIGGER_JOBOPEN_END = "jobopen_end";
    private const string TRIGGER_JOBOPEN_CLOSE = "jobopen_close";
    private string ShardGaugeStartAnim = "UnitShard_gauge";
    private const string GachaShardAwakeText = "sys.GACHA_TEXT_SHARD_AWAKE";
    private bool isRunningAnimator = true;
    private int StartAwakeLv;
    private int NowAwakeLv;
    private int NextAwakeLv;
    private int AwakeLvCap;
    private int mCurrentAwakeJobIndex;
    private bool mClicked;
    private List<GameObject> mRebirthStars = new List<GameObject>();
    private List<string> mJobID;
    private List<int> mJobAwakeLv;
    private const float ShardAnimSpan = 1f;
    private float mShardStart;
    private float mShardEnd;
    private float mShardAnimTime;
    private float mShardGet;
    private bool mJobEffectFlag;
    private string mCurrentUnitIname;
    private int mget_shard;
    private int msub_shard;
    private int muse_shard;
    private int mremain_shard;
    private int mnext_shard;
    private int mstart_gauge;

    public bool UnlockUnit => this.mUnlockUnit;

    private void Start()
    {
      if (this.GaugeUpAnimSoundList.Length <= 0)
      {
        this.GaugeUpAnimSoundList = new string[this.DefaultGaugeUpAnimSoundList.Length];
        for (int index = 0; index < this.DefaultGaugeUpAnimSoundList.Length; ++index)
          this.GaugeUpAnimSoundList[index] = this.DefaultGaugeUpAnimSoundList[index];
      }
      this.mState = new StateMachine<GachaUnitShard>(this);
      this.isRunningAnimator = true;
      this.Restart();
    }

    private void Update()
    {
      if (this.mState == null)
        return;
      this.mState.Update();
    }

    public bool IsRunningAnimator => this.isRunningAnimator;

    public void Reset() => this.isRunningAnimator = true;

    public void Restart()
    {
      if (this.mState == null)
        this.mState = new StateMachine<GachaUnitShard>(this);
      this.mCurrentAwakeJobIndex = 0;
      this.isRunningAnimator = true;
      if (this.mNotUnlockUnit || !this.mNotUnlockUnit && this.StartAwakeLv < this.AwakeLvCap)
        this.mState.GotoState<GachaUnitShard.State_Init>();
      else
        this.mState.GotoState<GachaUnitShard.State_WaitEndUnitShard>();
    }

    public void OnClicked() => this.mClicked = true;

    private int GetPastShard(int index = 0, string iname = "")
    {
      if (string.IsNullOrEmpty(iname))
        return -1;
      int pastShard = 0;
      for (int index1 = 0; index1 < index; ++index1)
      {
        GachaDropData drop = GachaResultData.drops[index1];
        if (drop.item != null && drop.item.type == EItemType.UnitPiece && !(iname != drop.item.iname))
          pastShard += drop.num;
      }
      return pastShard;
    }

    private int GetAmountShardEx(int index = 0, string iname = "")
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      for (int index1 = GachaResultData.drops.Length - 1; index1 > index; --index1)
      {
        GachaDropData drop = GachaResultData.drops[index1];
        if (drop.unit == null && drop.artifact == null && !(iname != drop.item.iname) && instance.MasterParam.GetUnitParamForPiece(drop.item.iname) != null)
        {
          int num = !dictionary.ContainsKey(iname) ? instance.Player.GetItemAmount(iname) : dictionary[iname];
          if (num > 0)
            num -= drop.num;
          dictionary[iname] = num;
        }
      }
      return dictionary.ContainsKey(iname) ? dictionary[iname] : 0;
    }

    private void RefreshShardValue()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      UnitData unitDataByUnitId = instance.Player.FindUnitDataByUnitID(this.mCurrentUnitIname);
      this.NowAwakeLv = this.NextAwakeLv;
      this.muse_shard = 0;
      this.ShardGauge.AnimateValue(0.0f, 0.0f);
      this.ShardCurrent.text = this.muse_shard.ToString();
      this.mstart_gauge = 0;
      if (unitDataByUnitId == null)
        return;
      this.mnext_shard = instance.MasterParam.GetAwakeNeedPieces(this.NowAwakeLv);
      this.muse_shard = this.mremain_shard >= this.mnext_shard ? this.mnext_shard : this.mremain_shard;
      this.mremain_shard -= this.muse_shard;
      this.NextAwakeLv = this.NextAwakeLv + 1 < this.AwakeLvCap ? this.NextAwakeLv + 1 : this.AwakeLvCap;
      this.ShardNext.text = this.mnext_shard.ToString();
    }

    private void RefreshJobWindow()
    {
      if (this.mJobID == null || this.mJobID.Count < this.mCurrentAwakeJobIndex)
        return;
      JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(this.mJobID[this.mCurrentAwakeJobIndex]);
      this.JobName.text = jobParam.name;
      this.JobComment.text = LocalizedText.Get("sys.GACHA_TEXT_SHARD_AWAKE", (object) jobParam.name);
    }

    private void InitRebirthStar()
    {
      foreach (GameObject mRebirthStar in this.mRebirthStars)
        GameUtility.DestroyGameObject(mRebirthStar);
      this.mRebirthStars.Clear();
    }

    public bool IsReachingAwakelv() => !this.mNotUnlockUnit && this.StartAwakeLv >= this.AwakeLvCap;

    public bool IsReachingUnlockUnit()
    {
      return this.mNotUnlockUnit && this.msub_shard + this.mget_shard >= this.mnext_shard;
    }

    public void Refresh(
      UnitParam param,
      string shard_name = "",
      int awake_lv = 0,
      int get_shard = 0,
      int current_index = 0)
    {
      if (param == null)
        return;
      GameManager instance1 = MonoSingleton<GameManager>.Instance;
      this.InitRebirthStar();
      this.rebirthstar_template.SetActive(false);
      this.mNotUnlockUnit = true;
      this.mget_shard = get_shard;
      this.msub_shard = this.GetPastShard(current_index, param.piece);
      this.muse_shard = 0;
      this.mstart_gauge = 0;
      this.mremain_shard = this.mget_shard;
      UnitData unitDataByUnitId = instance1.Player.FindUnitDataByUnitID(param.iname);
      this.mCurrentUnitIname = param.iname;
      string path1 = AssetPath.UnitImage(param, param.GetJobId(0));
      instance1.ApplyTextureAsync(this.unit_img, path1);
      instance1.ApplyTextureAsync(this.unit_img_blur01, path1);
      instance1.ApplyTextureAsync(this.unit_img_blur02, path1);
      string path2 = AssetPath.UnitIconSmall(param, param.GetJobId(0));
      instance1.ApplyTextureAsync((RawImage) this.unit_icon, path2);
      GameSettings instance2 = GameSettings.Instance;
      if (unitDataByUnitId != null && EElement.None <= unitDataByUnitId.Element && unitDataByUnitId.Element < (EElement) instance2.Elements_IconSmall.Length)
        this.element_icon.sprite = instance2.Elements_IconSmall[(int) unitDataByUnitId.Element];
      else
        this.element_icon.sprite = (Sprite) null;
      this.ShardName.text = shard_name;
      this.StartAwakeLv = this.NowAwakeLv = awake_lv;
      if (unitDataByUnitId != null)
      {
        this.mNotUnlockUnit = false;
        this.AwakeLvCap = unitDataByUnitId.GetAwakeLevelCap();
        int num1 = (int) instance1.MasterParam.GetRarityParam(unitDataByUnitId.Rarity).UnitAwakeLvCap / 5;
        for (int index = 0; index < num1; ++index)
        {
          GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.rebirthstar_template);
          gameObject.transform.SetParent(this.rebirthstar_root.transform, false);
          gameObject.SetActive(true);
          this.mRebirthStars.Add(gameObject);
        }
        int index1 = this.StartAwakeLv / 5;
        for (int index2 = 0; index2 < index1 && this.mRebirthStars.Count >= index2; ++index2)
        {
          Transform transform = this.mRebirthStars[index2].transform.Find("Rebirth_star_anim");
          ((Component) transform.Find("Rebirthstar_01")).gameObject.SetActive(true);
          ((Component) transform.Find("Rebirthstar_02")).gameObject.SetActive(true);
          ((Component) transform.Find("Rebirthstar_03")).gameObject.SetActive(true);
          ((Component) transform.Find("Rebirthstar_04")).gameObject.SetActive(true);
          ((Component) transform.Find("Rebirthstar_05")).gameObject.SetActive(true);
        }
        int num2 = this.StartAwakeLv % 5;
        if (num2 > 0)
        {
          Transform transform = this.mRebirthStars[index1].transform.Find("Rebirth_star_anim");
          for (int index3 = 0; index3 < num2; ++index3)
          {
            string str = "Rebirthstar_0" + (object) (index3 + 1);
            ((Component) transform.Find(str)).gameObject.SetActive(true);
          }
        }
        if (this.msub_shard > 0)
        {
          int startAwakeLv = this.StartAwakeLv;
          int msubShard = this.msub_shard;
          int awakeNeedPieces = instance1.MasterParam.GetAwakeNeedPieces(startAwakeLv);
          int num3 = this.AwakeLvCap / 5;
          while (msubShard >= awakeNeedPieces)
          {
            int num4 = startAwakeLv / 5;
            int num5 = Math.Min(5, startAwakeLv % 5) + 1;
            Transform transform = this.mRebirthStars[num4 + 1 < num3 ? num4 : num3 - 1].transform.Find("Rebirth_star_anim").Find("Rebirthstar_0" + num5.ToString());
            if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
              ((Component) transform).gameObject.SetActive(true);
            ++startAwakeLv;
            if (startAwakeLv < this.AwakeLvCap)
            {
              msubShard -= awakeNeedPieces;
              awakeNeedPieces = instance1.MasterParam.GetAwakeNeedPieces(startAwakeLv);
              if (msubShard < awakeNeedPieces)
                break;
            }
            else
              break;
          }
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardNext, (UnityEngine.Object) null))
            this.ShardNext.text = awakeNeedPieces.ToString();
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardCurrent, (UnityEngine.Object) null))
            this.ShardCurrent.text = msubShard.ToString();
          this.mstart_gauge = msubShard;
          this.ShardGauge.AnimateValue((float) msubShard / (float) awakeNeedPieces, 0.0f);
          this.StartAwakeLv = startAwakeLv;
          this.NowAwakeLv = this.StartAwakeLv;
          this.NextAwakeLv = this.StartAwakeLv + 1 <= this.AwakeLvCap ? this.StartAwakeLv + 1 : this.AwakeLvCap;
        }
      }
      if (!this.mNotUnlockUnit && (this.mNotUnlockUnit || this.NowAwakeLv >= this.AwakeLvCap))
        return;
      int num6 = unitDataByUnitId == null ? param.GetUnlockNeedPieces() : instance1.MasterParam.GetAwakeNeedPieces(this.StartAwakeLv);
      this.mnext_shard = num6;
      this.muse_shard = this.mget_shard >= num6 ? num6 : this.mget_shard;
      if (this.mstart_gauge > 0 && this.mstart_gauge < this.mnext_shard)
      {
        int num7 = this.mnext_shard - this.mstart_gauge;
        this.muse_shard = num7 >= this.muse_shard ? this.muse_shard : num7;
      }
      this.mremain_shard -= this.muse_shard;
      if (unitDataByUnitId == null && this.muse_shard >= this.mnext_shard)
        this.mUnlockUnit = true;
      if (this.muse_shard >= this.mnext_shard && this.msub_shard <= 0)
        this.NextAwakeLv = this.NowAwakeLv + 1 <= this.AwakeLvCap ? this.NowAwakeLv + 1 : this.AwakeLvCap;
      if (param.jobsets != null && param.jobsets.Length > 0)
      {
        this.mJobID = new List<string>();
        this.mJobAwakeLv = new List<int>();
        for (int index = 0; index < param.jobsets.Length; ++index)
        {
          JobSetParam jobSetParam = MonoSingleton<GameManager>.Instance.GetJobSetParam(param.jobsets[index]);
          if (jobSetParam != null && jobSetParam.lock_awakelv > 0 && (unitDataByUnitId == null || !unitDataByUnitId.Jobs[index].IsActivated) && this.StartAwakeLv < jobSetParam.lock_awakelv)
          {
            this.mJobID.Add(jobSetParam.job);
            this.mJobAwakeLv.Add(jobSetParam.lock_awakelv);
          }
        }
        if (this.mJobID != null && this.mJobID.Count > 1)
        {
          JobParam jobParam = MonoSingleton<GameManager>.Instance.GetJobParam(this.mJobID[0]);
          this.JobName.text = jobParam.name;
          this.JobComment.text = LocalizedText.Get("sys.GACHA_TEXT_SHARD_AWAKE", (object) jobParam.name);
        }
      }
      this.isRunningAnimator = true;
    }

    private void RefreshAddStar()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int awakeNeedPieces = instance.MasterParam.GetAwakeNeedPieces(this.StartAwakeLv);
      int num1 = this.AwakeLvCap / 5;
      int num2 = this.mget_shard;
      int num3 = this.muse_shard;
      int startAwakeLv = this.StartAwakeLv;
      for (; num2 + this.mstart_gauge >= awakeNeedPieces; this.mstart_gauge = 0)
      {
        int num4 = startAwakeLv / 5;
        int num5 = Math.Min(5, startAwakeLv % 5) + 1;
        Transform transform = this.mRebirthStars[num4 + 1 < num1 ? num4 : num1 - 1].transform.Find("Rebirth_star_anim").Find("Rebirthstar_0" + num5.ToString());
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
          ((Component) transform).gameObject.SetActive(true);
        ++startAwakeLv;
        if (startAwakeLv >= this.AwakeLvCap)
        {
          num2 = awakeNeedPieces;
          break;
        }
        num2 -= num3;
        awakeNeedPieces = instance.MasterParam.GetAwakeNeedPieces(startAwakeLv);
        if (num2 >= awakeNeedPieces)
          num3 = awakeNeedPieces;
        else
          break;
      }
      this.NowAwakeLv = startAwakeLv;
      this.NextAwakeLv = this.NowAwakeLv + 1 <= this.AwakeLvCap ? this.NowAwakeLv + 1 : this.AwakeLvCap;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardNext, (UnityEngine.Object) null))
        this.ShardNext.text = awakeNeedPieces.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardCurrent, (UnityEngine.Object) null))
        this.ShardCurrent.text = num2.ToString();
      this.ShardGauge.AnimateValue((float) num2 / (float) awakeNeedPieces, 0.0f);
    }

    private void RefreshAddGauge()
    {
      this.ShardGauge.AnimateValue((float) this.muse_shard / (float) this.mnext_shard, 0.0f);
      this.ShardCurrent.text = Mathf.FloorToInt((float) this.muse_shard).ToString();
    }

    private void SetUsedShard(int end_gauge)
    {
      this.mShardStart = this.mShardEnd = 0.0f;
      this.mShardAnimTime = 0.0f;
      this.mShardEnd = (float) end_gauge;
    }

    private void AnimationShard()
    {
      this.mShardAnimTime += Time.unscaledDeltaTime;
      float num1 = Mathf.Lerp(this.mShardStart, this.mShardEnd, Mathf.Clamp01(this.mShardAnimTime / 1f));
      int num2 = Mathf.FloorToInt(num1);
      int mnextShard = this.mnext_shard;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardCurrent, (UnityEngine.Object) null))
        this.ShardCurrent.text = Mathf.FloorToInt((float) (this.mstart_gauge + num2)).ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardGauge, (UnityEngine.Object) null))
      {
        float num3 = ((float) this.mstart_gauge + num1) / (float) mnextShard;
        MonoSingleton<MySound>.Instance.PlaySEOneShot(this.GaugeUpAnimSoundList[0]);
        this.ShardGauge.AnimateValue(num3, 0.0f);
      }
      if ((double) this.mShardAnimTime < 1.0)
        return;
      this.mShardStart = this.mShardEnd;
      this.mShardAnimTime = 0.0f;
    }

    private void RefreshShardGaugeImmediate()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ShardGauge, (UnityEngine.Object) null) || this.msub_shard > 0)
        return;
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardNext, (UnityEngine.Object) null))
        this.ShardNext.text = this.mnext_shard.ToString();
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.ShardCurrent, (UnityEngine.Object) null))
        this.ShardCurrent.text = "0";
      this.ShardGauge.AnimateValue(Mathf.Clamp01(0.0f / (float) this.mnext_shard), 0.0f);
    }

    private void SetJobEffectIndex()
    {
      if (this.mJobID == null)
        return;
      for (int index = 0; index < this.mJobID.Count && this.NowAwakeLv >= this.mJobAwakeLv[index]; ++index)
        this.mCurrentAwakeJobIndex = index;
    }

    private bool IsOpenJobAnimation()
    {
      return this.mJobAwakeLv != null && this.mJobAwakeLv.Count > 0 && this.NowAwakeLv >= this.mJobAwakeLv[this.mCurrentAwakeJobIndex];
    }

    private class State_Init : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
        self.mState.GotoState<GachaUnitShard.State_WaitStartAnimation>();
      }
    }

    private class State_WaitStartAnimation : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
      }

      public override void Update(GachaUnitShard self)
      {
        if (!self.mClicked)
        {
          if (!GameUtility.IsAnimatorRunning((Component) self) || !GameUtility.CompareAnimatorStateName((Component) self, self.ShardGaugeStartAnim))
            return;
          self.mState.GotoState<GachaUnitShard.State_WaitGaugeActionStarted>();
        }
        else
          self.mState.GotoState<GachaUnitShard.State_AddingGaugeSkip>();
      }
    }

    private class State_AddingGaugeSkip : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
        Animator component = ((Component) self).GetComponent<Animator>();
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) component, (UnityEngine.Object) null))
          component.SetBool("is_skip", true);
        self.StartCoroutine(this.WaitGaugeObject());
      }

      [DebuggerHidden]
      private IEnumerator WaitGaugeObject()
      {
        // ISSUE: object of a compiler-generated type is created
        return (IEnumerator) new GachaUnitShard.State_AddingGaugeSkip.\u003CWaitGaugeObject\u003Ec__Iterator0()
        {
          \u0024this = this
        };
      }
    }

    private class State_WaitGaugeActionStarted : State<GachaUnitShard>
    {
      private float mWaitTime;
      private float mTimer;

      public override void Begin(GachaUnitShard self)
      {
        this.mWaitTime = this.mTimer = 0.0f;
        this.mWaitTime = self.WaitGaugeActionStarted;
      }

      public override void Update(GachaUnitShard self)
      {
        if (self.mClicked)
          self.mState.GotoState<GachaUnitShard.State_AddingGaugeSkip>();
        if ((double) this.mWaitTime < (double) this.mTimer)
        {
          this.mTimer = 0.0f;
          self.mState.GotoState<GachaUnitShard.State_AddingGauge>();
        }
        else
          this.mTimer += Time.deltaTime;
      }
    }

    private class State_AddingGauge : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
        self.RefreshShardGaugeImmediate();
        self.SetUsedShard(self.muse_shard);
      }

      public override void Update(GachaUnitShard self)
      {
        if (self.mClicked)
          self.mState.GotoState<GachaUnitShard.State_GaugeSkip>();
        else if ((double) self.mShardStart < (double) self.mShardEnd)
          self.AnimationShard();
        else
          self.mState.GotoState<GachaUnitShard.State_WaitGaugeActioned>();
      }
    }

    private class State_GaugeSkip : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
        self.mClicked = false;
        if (!self.mNotUnlockUnit)
        {
          self.RefreshAddStar();
          self.SetJobEffectIndex();
          if (self.IsOpenJobAnimation())
          {
            self.mJobEffectFlag = false;
            self.RefreshJobWindow();
            self.mState.GotoState<GachaUnitShard.State_StartJobOpenAnimation>();
          }
          else
            self.mState.GotoState<GachaUnitShard.State_EndShardGaugeBarAnimation>();
        }
        else
        {
          self.RefreshAddGauge();
          self.mState.GotoState<GachaUnitShard.State_EndShardGaugeBarAnimation>();
        }
      }
    }

    private class State_WaitGaugeActioned : State<GachaUnitShard>
    {
      private float mWaitTime;
      private float mTimer;

      public override void Begin(GachaUnitShard self)
      {
        self.mClicked = false;
        this.mWaitTime = this.mTimer = 0.0f;
        this.mWaitTime = self.WaitGaugeActioned;
      }

      public override void Update(GachaUnitShard self)
      {
        if (self.mClicked)
          self.mState.GotoState<GachaUnitShard.State_AddingGaugeSkip>();
        else if ((double) this.mWaitTime < (double) this.mTimer)
        {
          this.mTimer = 0.0f;
          if (!self.mNotUnlockUnit)
            self.mState.GotoState<GachaUnitShard.State_AddingRebirthStar>();
          else
            self.mState.GotoState<GachaUnitShard.State_CheckRemainPiece>();
        }
        else
          this.mTimer += Time.deltaTime;
      }
    }

    private class State_AddingRebirthStar : State<GachaUnitShard>
    {
      private Transform rebirth_star;
      private int new_star;

      public override void Begin(GachaUnitShard self)
      {
        self.mClicked = false;
        if (self.muse_shard + self.mstart_gauge != self.mnext_shard)
        {
          self.mState.GotoState<GachaUnitShard.State_CheckRemainPiece>();
        }
        else
        {
          int num1 = self.AwakeLvCap / 5;
          int num2 = self.NowAwakeLv / 5;
          int index = num2 + 1 < num1 ? num2 : num1 - 1;
          int num3 = Math.Min(5, self.NowAwakeLv % 5) + 1;
          this.new_star = num3;
          this.rebirth_star = self.mRebirthStars[index].transform.Find("Rebirth_star_anim");
          ((Component) this.rebirth_star).GetComponent<Animator>().SetInteger("new", num3);
        }
      }

      public override void Update(GachaUnitShard self)
      {
        if (self.mClicked)
        {
          Animator component = ((Component) this.rebirth_star).GetComponent<Animator>();
          component.SetInteger("new", 0);
          component.SetTrigger("close");
          self.mState.GotoState<GachaUnitShard.State_AddingGaugeSkip>();
        }
        else
        {
          if (GameUtility.IsAnimatorRunning((Component) this.rebirth_star))
            return;
          Transform transform = this.rebirth_star.Find("Rebirthstar_0" + this.new_star.ToString());
          if (UnityEngine.Object.op_Inequality((UnityEngine.Object) transform, (UnityEngine.Object) null))
            ((Component) transform).gameObject.SetActive(true);
          Animator component = ((Component) this.rebirth_star).GetComponent<Animator>();
          component.SetInteger("new", 0);
          component.SetTrigger("close");
          self.mState.GotoState<GachaUnitShard.State_WaitAddingRebirthStarActioned>();
        }
      }
    }

    private class State_WaitAddingRebirthStarActioned : State<GachaUnitShard>
    {
      private float mWaitTime;
      private float mTimer;

      public override void Begin(GachaUnitShard self)
      {
        self.mClicked = false;
        this.mWaitTime = this.mTimer = 0.0f;
        this.mWaitTime = self.WaitRebirthStarActioned;
      }

      public override void Update(GachaUnitShard self)
      {
        if ((double) this.mWaitTime < (double) this.mTimer)
        {
          this.mTimer = 0.0f;
          self.mState.GotoState<GachaUnitShard.State_ShowRebirthText>();
        }
        else
          this.mTimer += Time.deltaTime;
      }
    }

    private class State_ShowRebirthText : State<GachaUnitShard>
    {
      private GameObject anim;

      public override void Begin(GachaUnitShard self)
      {
        self.mClicked = false;
        if (self.NextAwakeLv == self.NowAwakeLv)
        {
          self.mState.GotoState<GachaUnitShard.State_CheckRemainPiece>();
        }
        else
        {
          self.Rebirthtext_root.SetActive(true);
          this.anim = ((Component) self.Rebirthtext_root.transform.Find("Rebirth_txtanim")).gameObject;
        }
      }

      public override void Update(GachaUnitShard self)
      {
        if (self.mClicked)
        {
          self.Rebirthtext_root.SetActive(false);
          self.mState.GotoState<GachaUnitShard.State_AddingGaugeSkip>();
        }
        else
        {
          if (GameUtility.IsAnimatorRunning(this.anim))
            return;
          self.Rebirthtext_root.SetActive(false);
          self.mState.GotoState<GachaUnitShard.State_WaitRebirthTextActioned>();
        }
      }
    }

    private class State_WaitRebirthTextActioned : State<GachaUnitShard>
    {
      private float mWaitTime;
      private float mTimer;

      public override void Begin(GachaUnitShard self)
      {
        self.mClicked = false;
        this.mWaitTime = this.mTimer = 0.0f;
        this.mWaitTime = self.WaitRebirthTextActioned;
      }

      public override void Update(GachaUnitShard self)
      {
        if (self.mClicked)
          self.mState.GotoState<GachaUnitShard.State_AddingGaugeSkip>();
        else if ((double) this.mWaitTime < (double) this.mTimer)
        {
          this.mTimer = 0.0f;
          self.mState.GotoState<GachaUnitShard.State_CheckRemainPiece>();
        }
        else
          this.mTimer += Time.deltaTime;
      }
    }

    private class State_CheckRemainPiece : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
        if (self.mremain_shard > 0 && self.NowAwakeLv + 1 < self.AwakeLvCap)
        {
          self.RefreshShardValue();
          self.mState.GotoState<GachaUnitShard.State_AddingGauge>();
        }
        else
        {
          self.NowAwakeLv = self.NextAwakeLv;
          self.SetJobEffectIndex();
          if (self.IsOpenJobAnimation())
          {
            self.mJobEffectFlag = false;
            self.RefreshJobWindow();
            self.mState.GotoState<GachaUnitShard.State_StartJobOpenAnimation>();
          }
          else
            self.mState.GotoState<GachaUnitShard.State_EndShardGaugeBarAnimation>();
        }
      }
    }

    private class State_EndShardGaugeBarAnimation : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
        Animator component = ((Component) self).GetComponent<Animator>();
        component.SetBool("is_skip", false);
        component.SetTrigger("close");
        self.mClicked = false;
      }

      public override void Update(GachaUnitShard self)
      {
        if (self.mClicked)
        {
          self.mState.GotoState<GachaUnitShard.State_EndUnitShard>();
        }
        else
        {
          if (!self.isRunningAnimator || !GameUtility.CompareAnimatorStateName((Component) self, "closed"))
            return;
          GameUtility.DestroyGameObjects(self.mRebirthStars);
          self.mRebirthStars.Clear();
          self.isRunningAnimator = false;
        }
      }
    }

    private class State_StartJobOpenAnimation : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
        Animator component = ((Component) self).GetComponent<Animator>();
        component.SetBool("is_skip", false);
        if (GameUtility.CompareAnimatorStateName((Component) self, "UnitShard_gauge_jobopen_loop") || self.mJobEffectFlag)
          return;
        self.mJobEffectFlag = true;
        component.SetTrigger("jobopen_start");
      }

      public override void Update(GachaUnitShard self)
      {
        if (!GameUtility.CompareAnimatorStateName((Component) self, "UnitShard_gauge_jobopen_start") && !GameUtility.CompareAnimatorStateName((Component) self, "UnitShard_gauge_jobopen_loop") || !self.mClicked)
          return;
        self.mClicked = false;
        ((Component) self).GetComponent<Animator>().SetTrigger("jobopen_end");
        self.mState.GotoState<GachaUnitShard.State_CheckJobOpen>();
      }
    }

    private class State_CheckJobOpen : State<GachaUnitShard>
    {
      private Animator at;

      public override void Begin(GachaUnitShard self)
      {
        this.at = ((Component) self).GetComponent<Animator>();
      }

      public override void Update(GachaUnitShard self)
      {
        if (GameUtility.IsAnimatorRunning((Component) self) || !GameUtility.CompareAnimatorStateName((Component) self, "UnitShard_gauge_jobopen_end"))
          return;
        this.at.SetTrigger("jobopen_close");
        self.mState.GotoState<GachaUnitShard.State_CloseJobOpenAnimation>();
      }
    }

    private class State_CloseJobOpenAnimation : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
      }

      public override void Update(GachaUnitShard self)
      {
        if (!self.isRunningAnimator || !GameUtility.CompareAnimatorStateName((Component) self, "closed"))
          return;
        self.isRunningAnimator = false;
        self.mClicked = false;
      }
    }

    private class State_WaitEndUnitShard : State<GachaUnitShard>
    {
      private float mWaitTime;
      private float mTimer;

      public override void Begin(GachaUnitShard self)
      {
        self.mClicked = false;
        this.mWaitTime = this.mTimer = 0.0f;
        this.mWaitTime = 2f;
      }

      public override void Update(GachaUnitShard self)
      {
        if (self.mClicked)
          self.mState.GotoState<GachaUnitShard.State_EndUnitShard>();
        else if ((double) this.mWaitTime < (double) this.mTimer)
        {
          this.mTimer = 0.0f;
          self.mState.GotoState<GachaUnitShard.State_EndUnitShard>();
        }
        else
          this.mTimer += Time.deltaTime;
      }
    }

    private class State_EndUnitShard : State<GachaUnitShard>
    {
      public override void Begin(GachaUnitShard self)
      {
        GameUtility.DestroyGameObjects(self.mRebirthStars);
        self.mRebirthStars.Clear();
        self.mClicked = false;
        self.isRunningAnimator = false;
      }
    }
  }
}
