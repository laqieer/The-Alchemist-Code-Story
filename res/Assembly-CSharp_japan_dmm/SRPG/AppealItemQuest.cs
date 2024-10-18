// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemQuest
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
  public class AppealItemQuest : AppealItemBase
  {
    private readonly string SPRITES_PATH = "AppealSprites/quest";
    private readonly string MASTER_PATH = "Data/appeal/AppealQuest";
    [SerializeField]
    private Image AppealObject1;
    [SerializeField]
    private CanvasGroup AppealGroup0;
    [SerializeField]
    private CanvasGroup AppealGroup1;
    private string[] mAppealIds;
    private int mCurrentIndex;
    private bool IsLoaded;
    protected Dictionary<string, Sprite> mCacheAppealSprites = new Dictionary<string, Sprite>();
    private readonly float WAIT_SWAP_APPEAL = 5f;
    private float mWaitSwapeAppealTime;
    private Sprite mCurrentSprite;
    private Sprite mNextSprite;
    private bool IsUpdated = true;

    protected override void Awake()
    {
      base.Awake();
      if (!UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealObject1, (UnityEngine.Object) null))
        return;
      ((Component) this.AppealObject1).gameObject.SetActive(false);
    }

    protected override void Start()
    {
      base.Start();
      if (!this.LoadAppealMaster(this.MASTER_PATH))
        return;
      this.StartCoroutine(this.LoadAppealResources());
    }

    protected override void Update()
    {
      base.Update();
      if (!this.IsLoaded)
        return;
      if (this.IsUpdated)
        this.Refresh();
      else
        this.UpdateAppeal();
    }

    private void UpdateAppeal()
    {
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.mCurrentSprite, (UnityEngine.Object) null) || UnityEngine.Object.op_Equality((UnityEngine.Object) this.mNextSprite, (UnityEngine.Object) null))
        return;
      this.mWaitSwapeAppealTime -= Time.deltaTime;
      if ((double) this.mWaitSwapeAppealTime >= 0.0 || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealGroup0, (UnityEngine.Object) null) || !UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealGroup1, (UnityEngine.Object) null))
        return;
      float deltaTime = Time.deltaTime;
      this.AppealGroup0.alpha += -deltaTime;
      this.AppealGroup0.alpha = Mathf.Clamp(this.AppealGroup0.alpha - deltaTime, 0.0f, 1f);
      this.AppealGroup1.alpha += deltaTime;
      this.AppealGroup1.alpha = Mathf.Clamp(this.AppealGroup1.alpha + deltaTime, 0.0f, 1f);
      if ((double) this.AppealGroup1.alpha > 0.0 && (double) this.AppealGroup1.alpha < 1.0)
        return;
      this.IsUpdated = true;
      this.mWaitSwapeAppealTime = this.WAIT_SWAP_APPEAL;
    }

    protected override void Refresh()
    {
      this.mCurrentSprite = this.mAppealIds.Length <= this.mCurrentIndex || !this.mCacheAppealSprites.ContainsKey(this.mAppealIds[this.mCurrentIndex]) ? this.mCacheAppealSprites[this.mAppealIds[0]] : this.mCacheAppealSprites[this.mAppealIds[this.mCurrentIndex]];
      this.mNextSprite = this.mAppealIds.Length <= this.mCurrentIndex + 1 || !this.mCacheAppealSprites.ContainsKey(this.mAppealIds[this.mCurrentIndex + 1]) ? (this.mAppealIds.Length != 1 ? this.mCacheAppealSprites[this.mAppealIds[0]] : (Sprite) null) : this.mCacheAppealSprites[this.mAppealIds[this.mCurrentIndex + 1]];
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealObject, (UnityEngine.Object) null))
      {
        this.AppealObject.sprite = this.mCurrentSprite;
        ((Component) this.AppealObject).gameObject.SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mCurrentSprite, (UnityEngine.Object) null));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealGroup0, (UnityEngine.Object) null))
          this.AppealGroup0.alpha = 1f;
      }
      if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealObject1, (UnityEngine.Object) null) && UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNextSprite, (UnityEngine.Object) null))
      {
        this.AppealObject1.sprite = this.mNextSprite;
        ((Component) this.AppealObject1).gameObject.SetActive(UnityEngine.Object.op_Inequality((UnityEngine.Object) this.mNextSprite, (UnityEngine.Object) null));
        if (UnityEngine.Object.op_Inequality((UnityEngine.Object) this.AppealGroup1, (UnityEngine.Object) null))
          this.AppealGroup1.alpha = 0.0f;
      }
      ++this.mCurrentIndex;
      if (this.mCurrentIndex >= this.mAppealIds.Length)
        this.mCurrentIndex = 0;
      this.IsUpdated = UnityEngine.Object.op_Equality((UnityEngine.Object) this.mNextSprite, (UnityEngine.Object) null);
    }

    protected override void Destroy()
    {
      base.Destroy();
      foreach (string key in this.mCacheAppealSprites.Keys)
        Resources.UnloadAsset((UnityEngine.Object) this.mCacheAppealSprites[key]);
      this.mCacheAppealSprites = (Dictionary<string, Sprite>) null;
    }

    private bool LoadAppealMaster(string path)
    {
      if (string.IsNullOrEmpty(path))
        return false;
      string src = AssetManager.LoadTextData(path);
      if (string.IsNullOrEmpty(src))
        return false;
      try
      {
        JSON_AppealQuestMaster[] jsonArray = JSONParser.parseJSONArray<JSON_AppealQuestMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        long serverTime = Network.GetServerTime();
        List<string> stringList = new List<string>();
        foreach (JSON_AppealQuestMaster json in jsonArray)
        {
          AppealQuestMaster appealQuestMaster = new AppealQuestMaster();
          if (appealQuestMaster.Deserialize(json) && appealQuestMaster.start_at <= serverTime && appealQuestMaster.end_at > serverTime)
            stringList.Add(appealQuestMaster.appeal_id);
        }
        if (stringList != null)
        {
          if (stringList.Count > 0)
          {
            this.mAppealIds = new string[stringList.Count];
            this.mAppealIds = stringList.ToArray();
          }
        }
      }
      catch (Exception ex)
      {
        DebugUtility.LogException(ex);
        return false;
      }
      return true;
    }

    [DebuggerHidden]
    private IEnumerator LoadAppealResources()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new AppealItemQuest.\u003CLoadAppealResources\u003Ec__Iterator0()
      {
        \u0024this = this
      };
    }
  }
}
