// Decompiled with JetBrains decompiler
// Type: SRPG.MultiTowerReward
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using GR;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SRPG
{
  public class MultiTowerReward : MonoBehaviour
  {
    private readonly float WAIT_TIME = 0.5f;
    private MultiTowerReward.MODE mMode = MultiTowerReward.MODE.REQ;
    private int mRound = 1;
    private readonly float WAIT_OPEN;
    public GameObject item;
    public GameObject root;
    public GameObject template;
    public GameObject parent;
    public GameObject arrival;
    public Text floorTxt;
    public Text floorEffTxt;
    public Text rewardTxt;
    public Text okTxt;
    public Text getTxt;
    public string openAnim;
    public string nextAnim;
    public string resultAnim;
    private int mNow;
    private int mMax;
    private float mWaitTime;

    private void Start()
    {
      this.mRound = MonoSingleton<GameManager>.Instance.GetMTRound(GlobalVars.SelectedMultiTowerFloor);
      this.Refresh();
    }

    private void Refresh()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int selectedMultiTowerFloor = GlobalVars.SelectedMultiTowerFloor;
      MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, selectedMultiTowerFloor);
      if (mtFloorParam == null || selectedMultiTowerFloor < 0)
        return;
      this.mNow = 0;
      if (!string.IsNullOrEmpty(mtFloorParam.reward_id))
        this.mMax = instance.GetMTFloorReward(mtFloorParam.reward_id, this.mRound).Count;
      DataSource.Bind<MultiTowerFloorParam>(this.item, mtFloorParam, false);
      if (this.mNow + 1 < this.mMax)
        this.SetButtonText(true);
      this.mWaitTime = this.WAIT_OPEN;
      this.mMode = MultiTowerReward.MODE.NEXT;
    }

    private void SetButtonText(bool bNext)
    {
      if ((UnityEngine.Object) this.okTxt != (UnityEngine.Object) null)
        this.okTxt.text = LocalizedText.Get(!bNext ? "sys.CMD_OK" : "sys.BTN_NEXT");
      if (bNext || !((UnityEngine.Object) this.rewardTxt != (UnityEngine.Object) null))
        return;
      this.rewardTxt.text = LocalizedText.Get("sys.MULTI_TOWER_GIFT");
    }

    private void Update()
    {
      switch (this.mMode)
      {
        case MultiTowerReward.MODE.REQ:
          while (!this.SetData(this.mNow, true, (GameObject) null))
            ++this.mNow;
          this.mMode = MultiTowerReward.MODE.COUNTDOWN;
          this.mWaitTime = this.WAIT_TIME;
          break;
        case MultiTowerReward.MODE.COUNTDOWN:
          this.mWaitTime -= Time.deltaTime;
          if ((double) this.mWaitTime > 0.0)
            break;
          this.mMode = MultiTowerReward.MODE.WAIT;
          break;
        case MultiTowerReward.MODE.NEXT:
        case MultiTowerReward.MODE.FINISH:
          this.mWaitTime -= Time.deltaTime;
          if ((double) this.mWaitTime > 0.0)
            break;
          if (this.mMode == MultiTowerReward.MODE.FINISH)
          {
            this.mMode = MultiTowerReward.MODE.END;
            break;
          }
          this.mMode = MultiTowerReward.MODE.REQ;
          break;
      }
    }

    private bool SetData(int idx, bool bPlay = false, GameObject obj = null)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      GameObject gameObject = !((UnityEngine.Object) obj != (UnityEngine.Object) null) ? this.item : obj;
      MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, GlobalVars.SelectedMultiTowerFloor);
      if (mtFloorParam != null)
      {
        List<MultiTowerRewardItem> mtFloorReward = instance.GetMTFloorReward(mtFloorParam.reward_id, this.mRound);
        MultiTowerRewardItem multiTowerRewardItem = mtFloorReward[idx];
        if (mtFloorReward != null && multiTowerRewardItem != null)
        {
          if (multiTowerRewardItem.type == MultiTowerRewardItem.RewardType.Award && instance.Player.IsHaveAward(multiTowerRewardItem.itemname))
            return false;
          if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
          {
            gameObject.SetActive(true);
            MultiTowerRewardItemUI component = gameObject.GetComponent<MultiTowerRewardItemUI>();
            if ((UnityEngine.Object) component != (UnityEngine.Object) null)
              component.SetData(idx);
            if (bPlay)
              this.ReqAnim(this.openAnim);
            this.SetRewardName(idx, mtFloorParam);
          }
        }
      }
      if ((UnityEngine.Object) this.arrival != (UnityEngine.Object) null)
        this.arrival.SetActive(false);
      return true;
    }

    private void SetRewardName(int idx, MultiTowerFloorParam param)
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      MultiTowerRewardItem multiTowerRewardItem = instance.GetMTFloorReward(param.reward_id, this.mRound)[idx];
      int num = multiTowerRewardItem.num;
      string itemname = multiTowerRewardItem.itemname;
      MultiTowerRewardItem.RewardType type = multiTowerRewardItem.type;
      string str1 = LocalizedText.Get("sys.MULTI_TOWER_REWARD_GET_MSG");
      if (!((UnityEngine.Object) this.rewardTxt != (UnityEngine.Object) null))
        return;
      string str2 = string.Empty;
      switch (type)
      {
        case MultiTowerRewardItem.RewardType.Item:
          ItemParam itemParam = instance.GetItemParam(itemname);
          if (itemParam != null)
          {
            str2 = itemParam.name + string.Format(LocalizedText.Get("sys.CROSS_NUM"), (object) num);
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Coin:
          str2 = string.Format(LocalizedText.Get("sys.CHALLENGE_REWARD_COIN"), (object) num);
          break;
        case MultiTowerRewardItem.RewardType.Artifact:
          ArtifactParam artifactParam = instance.MasterParam.GetArtifactParam(itemname);
          if (artifactParam != null)
          {
            str2 = artifactParam.name;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Award:
          AwardParam awardParam = instance.GetAwardParam(itemname);
          if (awardParam != null)
            str2 = awardParam.name;
          str1 = LocalizedText.Get("sys.MULTI_TOWER_REWARD_GET_MSG");
          break;
        case MultiTowerRewardItem.RewardType.Unit:
          UnitParam unitParam = instance.GetUnitParam(itemname);
          if (unitParam != null)
          {
            str2 = unitParam.name;
            break;
          }
          break;
        case MultiTowerRewardItem.RewardType.Gold:
          str2 = num.ToString() + LocalizedText.Get("sys.GOLD");
          break;
      }
      this.rewardTxt.text = string.Format(LocalizedText.Get("sys.MULTI_TOWER_REWARD_NAME"), (object) str2);
      if (!((UnityEngine.Object) this.getTxt != (UnityEngine.Object) null))
        return;
      this.getTxt.text = str1;
    }

    private void ReqAnim(string anim)
    {
      if (!((UnityEngine.Object) this.root != (UnityEngine.Object) null))
        return;
      Animator component = this.root.GetComponent<Animator>();
      if (!((UnityEngine.Object) component != (UnityEngine.Object) null))
        return;
      component.Play(anim);
    }

    private void CreateResult()
    {
      GameManager instance = MonoSingleton<GameManager>.Instance;
      int selectedMultiTowerFloor = GlobalVars.SelectedMultiTowerFloor;
      MultiTowerFloorParam mtFloorParam = instance.GetMTFloorParam(GlobalVars.SelectedMultiTowerID, selectedMultiTowerFloor);
      if (mtFloorParam == null || !((UnityEngine.Object) this.template != (UnityEngine.Object) null))
        return;
      List<MultiTowerRewardItem> mtFloorReward = instance.GetMTFloorReward(mtFloorParam.reward_id, this.mRound);
      for (int idx = 0; idx < mtFloorReward.Count; ++idx)
      {
        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.template);
        if ((UnityEngine.Object) gameObject != (UnityEngine.Object) null)
        {
          DataSource.Bind<MultiTowerFloorParam>(gameObject, mtFloorParam, false);
          gameObject.SetActive(true);
          if (this.SetData(idx, false, gameObject))
          {
            if ((UnityEngine.Object) this.parent != (UnityEngine.Object) null)
              gameObject.transform.SetParent(this.parent.transform, false);
          }
          else
            gameObject.SetActive(false);
        }
      }
      this.template.SetActive(false);
      this.item.SetActive(false);
    }

    public void OnClickNext()
    {
      if (this.mMode == MultiTowerReward.MODE.WAIT)
      {
        this.mWaitTime = this.WAIT_TIME;
        if (++this.mNow < this.mMax)
        {
          this.mMode = MultiTowerReward.MODE.NEXT;
          this.ReqAnim(this.nextAnim);
        }
        else
        {
          this.CreateResult();
          this.ReqAnim(this.resultAnim);
          this.SetButtonText(false);
          this.mMode = MultiTowerReward.MODE.FINISH;
        }
        MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0.0f);
      }
      else
      {
        if (this.mMode != MultiTowerReward.MODE.END)
          return;
        MonoSingleton<MySound>.Instance.PlaySEOneShot(SoundSettings.Current.Tap, 0.0f);
        FlowNode_TriggerLocalEvent.TriggerLocalEvent((Component) this, "Finish");
        this.mMode = MultiTowerReward.MODE.NONE;
      }
    }

    private enum MODE
    {
      NONE,
      REQ,
      COUNTDOWN,
      WAIT,
      NEXT,
      FINISH,
      END,
    }
  }
}
