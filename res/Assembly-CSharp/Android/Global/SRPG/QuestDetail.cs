// Decompiled with JetBrains decompiler
// Type: SRPG.QuestDetail
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: E2D362ED-2CBE-44F0-8985-22128799036A
// Assembly location: D:\User\Desktop\Assembly-CSharp.dll

using GR;
using UnityEngine;

namespace SRPG
{
  public class QuestDetail : MonoBehaviour
  {
    public GameObject[] Missions = new GameObject[0];
    public GameObject NoMissionText;
    [SerializeField]
    private GameObject[] mission_reward_parent_objects;
    [SerializeField]
    private GameObject[] mission_reward_item_objects;
    [SerializeField]
    private GameObject[] mission_reward_unit_objects;
    [SerializeField]
    private GameObject[] mission_reward_artifact_objects;

    private void Start()
    {
      QuestParam dataOfClass = DataSource.FindDataOfClass<QuestParam>(this.gameObject, (QuestParam) null);
      if (dataOfClass == null)
        return;
      this.SetMissionListActive(dataOfClass);
    }

    private void SetMissionListActive(QuestParam param)
    {
      bool flag = !string.IsNullOrEmpty(param.mission);
      for (int index = 0; index < this.Missions.Length; ++index)
      {
        if (this.Missions != null)
          this.Missions[index].SetActive(flag);
      }
      if ((UnityEngine.Object) this.NoMissionText != (UnityEngine.Object) null)
        this.NoMissionText.SetActive(!flag);
      this.HideAllRewardObjects();
      this.RefreshActiveRewardObjects(param);
    }

    private void HideAllRewardObjects()
    {
      if (this.mission_reward_parent_objects != null)
      {
        for (int index = 0; index < this.mission_reward_parent_objects.Length; ++index)
        {
          if (!((UnityEngine.Object) this.mission_reward_parent_objects[index] == (UnityEngine.Object) null))
            this.mission_reward_parent_objects[index].SetActive(false);
        }
      }
      if (this.mission_reward_item_objects != null)
      {
        for (int index = 0; index < this.mission_reward_item_objects.Length; ++index)
        {
          if (!((UnityEngine.Object) this.mission_reward_item_objects[index] == (UnityEngine.Object) null))
            this.mission_reward_item_objects[index].SetActive(false);
        }
      }
      if (this.mission_reward_unit_objects != null)
      {
        for (int index = 0; index < this.mission_reward_unit_objects.Length; ++index)
        {
          if (!((UnityEngine.Object) this.mission_reward_unit_objects[index] == (UnityEngine.Object) null))
            this.mission_reward_unit_objects[index].SetActive(false);
        }
      }
      if (this.mission_reward_artifact_objects == null)
        return;
      for (int index = 0; index < this.mission_reward_artifact_objects.Length; ++index)
      {
        if (!((UnityEngine.Object) this.mission_reward_artifact_objects[index] == (UnityEngine.Object) null))
          this.mission_reward_artifact_objects[index].SetActive(false);
      }
    }

    private void RefreshActiveRewardObjects(QuestParam param)
    {
      for (int index = 0; index < param.bonusObjective.Length; ++index)
      {
        if (this.mission_reward_parent_objects.Length > index && (UnityEngine.Object) this.mission_reward_parent_objects[index] != (UnityEngine.Object) null)
          this.mission_reward_parent_objects[index].SetActive(true);
        if (param.bonusObjective[index].itemType == RewardType.Artifact)
        {
          if (MonoSingleton<GameManager>.Instance.MasterParam.GetArtifactParam(param.bonusObjective[index].item) != null && this.mission_reward_artifact_objects.Length > index && (UnityEngine.Object) this.mission_reward_artifact_objects[index] != (UnityEngine.Object) null)
            this.mission_reward_artifact_objects[index].SetActive(true);
        }
        else
        {
          ItemParam itemParam = MonoSingleton<GameManager>.Instance.GetItemParam(param.bonusObjective[index].item);
          if (itemParam != null)
          {
            if (itemParam.type == EItemType.Unit)
            {
              if (this.mission_reward_unit_objects.Length > index && (UnityEngine.Object) this.mission_reward_unit_objects[index] != (UnityEngine.Object) null)
                this.mission_reward_unit_objects[index].SetActive(true);
            }
            else if (this.mission_reward_item_objects.Length > index && (UnityEngine.Object) this.mission_reward_item_objects[index] != (UnityEngine.Object) null)
              this.mission_reward_item_objects[index].SetActive(true);
          }
        }
      }
    }
  }
}
