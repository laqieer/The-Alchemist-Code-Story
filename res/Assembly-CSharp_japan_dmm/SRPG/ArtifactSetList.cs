// Decompiled with JetBrains decompiler
// Type: SRPG.ArtifactSetList
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(100, "アビリティ詳細が開く", FlowNode.PinTypes.Output, 0)]
  public class ArtifactSetList : MonoBehaviour, IFlowInterface
  {
    private const int OUTPUT_ABILITY_DETAIL_OPEN = 100;
    [HeaderBar("▼セット効果のリストの親")]
    [SerializeField]
    private RectTransform m_SetListRoot;
    [HeaderBar("▼セット効果のアイテムのテンプレート")]
    [SerializeField]
    private GameObject m_SetListItemTemplate;
    private ArtifactParam m_ArtifactParam;
    private static ArtifactParam s_SelectedArtifactParam;

    public static void SetSelectedArtifactParam(ArtifactParam artifactParam)
    {
      ArtifactSetList.s_SelectedArtifactParam = artifactParam;
    }

    private void Start()
    {
      GameUtility.SetGameObjectActive(this.m_SetListItemTemplate, false);
      this.m_ArtifactParam = ArtifactSetList.s_SelectedArtifactParam;
      this.CreateListItem(MonoSingleton<GameManager>.Instance.MasterParam.FindAllSkillAbilityDeriveDataWithArtifact(this.m_ArtifactParam.iname));
    }

    private void CreateListItem(
      List<SkillAbilityDeriveData> skillAbilityDeriveData)
    {
      foreach (SkillAbilityDeriveData skillAbilityDeriveData1 in skillAbilityDeriveData)
      {
        GameObject gameObject = Object.Instantiate<GameObject>(this.m_SetListItemTemplate);
        gameObject.GetComponentInChildren<SkillAbilityDeriveListItem>().Setup(skillAbilityDeriveData1);
        gameObject.SetActive(true);
        gameObject.transform.SetParent((Transform) this.m_SetListRoot, false);
        DataSource.Bind<SkillAbilityDeriveParam>(gameObject, skillAbilityDeriveData1.m_SkillAbilityDeriveParam);
      }
    }

    public void Activated(int pinID)
    {
    }

    public void OnAbilityDetailOpen(GameObject go)
    {
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 100);
    }
  }
}
