// Decompiled with JetBrains decompiler
// Type: SRPG.AppealItemEvent
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
  public class AppealItemEvent : AppealItemBase
  {
    private readonly string SPRITES_PATH = "AppealSprites/event";
    private readonly string MASTER_PATH = "Data/appeal/AppealEvent";
    protected Dictionary<string, Sprite> mCacheAppealSprites = new Dictionary<string, Sprite>();
    private string[] mAppealIds;
    private string mAppealId;
    private bool IsLoaded;
    [SerializeField]
    private GameObject Ballon;
    private bool IsNew;

    protected override void Awake()
    {
      base.Awake();
      if (!((UnityEngine.Object) this.Ballon != (UnityEngine.Object) null))
        return;
      this.Ballon.SetActive(false);
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
      if (!this.IsLoaded || !((UnityEngine.Object) this.AppealSprite == (UnityEngine.Object) null) || !this.mCacheAppealSprites.ContainsKey(this.mAppealId))
        return;
      this.AppealSprite = this.mCacheAppealSprites[this.mAppealId];
      if ((UnityEngine.Object) this.Ballon != (UnityEngine.Object) null)
      {
        if ((UnityEngine.Object) this.AppealSprite == (UnityEngine.Object) null)
          this.Ballon.SetActive(false);
        else
          this.Ballon.SetActive(this.IsNew);
      }
      this.Refresh();
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
        JSON_AppealEventMaster[] jsonArray = JSONParser.parseJSONArray<JSON_AppealEventMaster>(src);
        if (jsonArray == null)
          throw new InvalidJSONException();
        long serverTime = Network.GetServerTime();
        string str = string.Empty;
        foreach (JSON_AppealEventMaster json in jsonArray)
        {
          AppealEventMaster appealEventMaster = new AppealEventMaster();
          if (appealEventMaster.Deserialize(json) && appealEventMaster.start_at <= serverTime && appealEventMaster.end_at > serverTime)
          {
            str = appealEventMaster.appeal_id;
            break;
          }
        }
        if (!string.IsNullOrEmpty(str))
          this.mAppealId = str;
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
      return (IEnumerator) new AppealItemEvent.\u003CLoadAppealResources\u003Ec__Iterator0() { \u0024this = this };
    }
  }
}
