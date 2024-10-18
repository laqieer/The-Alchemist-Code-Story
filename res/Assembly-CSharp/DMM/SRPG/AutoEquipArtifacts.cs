// Decompiled with JetBrains decompiler
// Type: SRPG.AutoEquipArtifacts
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using GR;
using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace SRPG
{
  [FlowNode.Pin(0, "初期化", FlowNode.PinTypes.Input, 0)]
  [FlowNode.Pin(101, "初期化完了", FlowNode.PinTypes.Output, 101)]
  [FlowNode.Pin(111, "詳細表示", FlowNode.PinTypes.Output, 111)]
  public class AutoEquipArtifacts : MonoBehaviour, IFlowInterface
  {
    private const int PIN_IN_INIT = 0;
    private const int PIN_IN_SELECT_ITEM = 11;
    private const int PIN_OUT_INIT = 101;
    private const int PIN_OUT_SHOW_DETAIL = 111;
    [SerializeField]
    private GameObject ArtifactIconTemplate;

    private bool Init()
    {
      SerializeValueBehaviour component = ((Component) this).GetComponent<SerializeValueBehaviour>();
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) component, (UnityEngine.Object) null))
        return false;
      UnitData unit = component.list.GetObject<UnitData>(ArtifactSVB_Key.UNIT);
      int job_index = component.list.GetObject<int>(ArtifactSVB_Key.JOB_INDEX);
      if (unit == null)
        return false;
      List<ArtifactData> autoEquipArtifacts = AutoEquipArtifacts.CreateAutoEquipArtifacts(unit, job_index);
      if (autoEquipArtifacts == null || autoEquipArtifacts.Count <= 0)
        return false;
      component.list.SetObject(ArtifactSVB_Key.ARTIFACTS, (object) autoEquipArtifacts);
      if (UnityEngine.Object.op_Equality((UnityEngine.Object) this.ArtifactIconTemplate, (UnityEngine.Object) null))
        return false;
      this.ArtifactIconTemplate.SetActive(false);
      foreach (ArtifactData data in autoEquipArtifacts)
      {
        if (data != null)
        {
          GameObject root = UnityEngine.Object.Instantiate<GameObject>(this.ArtifactIconTemplate, this.ArtifactIconTemplate.transform.parent);
          root.SetActive(true);
          DataSource.Bind<ArtifactData>(root, data);
          GameParameter.UpdateAll(root);
        }
      }
      return true;
    }

    public void Activated(int PinID)
    {
      if (PinID != 0)
        return;
      this.Init();
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 101);
    }

    public static List<ArtifactData> CreateAutoEquipArtifacts(UnitData unit, int job_index)
    {
      if (unit == null)
        return new List<ArtifactData>();
      JobData jobData = unit.GetJobData(job_index);
      FixParam fixParam = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.FixParam;
      int length = fixParam == null || fixParam.EquipArtifactSlotUnlock == null ? 0 : fixParam.EquipArtifactSlotUnlock.Length;
      int slot_count = 0;
      for (int slot = 0; slot < length && !ArtifactSlots.IsLockedArtifactSlot(slot, jobData, unit); ++slot)
        ++slot_count;
      if (slot_count <= 0)
        return new List<ArtifactData>();
      List<ArtifactData> player_artifacts = new List<ArtifactData>((IEnumerable<ArtifactData>) MonoSingleton<GameManager>.Instance.Player.Artifacts);
      for (int index = 0; index < unit.Jobs.Length; ++index)
      {
        if (unit.Jobs[index].UniqueID == jobData.UniqueID)
        {
          job_index = index;
          break;
        }
      }
      for (int index = 0; index < player_artifacts.Count; ++index)
      {
        if (player_artifacts[index] == null)
          player_artifacts.RemoveAt(index--);
        else if (!player_artifacts[index].CheckEnableEquip(unit, job_index))
          player_artifacts.RemoveAt(index--);
        else if (player_artifacts[index].IsEquipped())
        {
          UnitData unit1 = (UnitData) null;
          JobData job = (JobData) null;
          MonoSingleton<GameManager>.Instance.Player.FindOwner(player_artifacts[index], out unit1, out job);
          if (unit1.UniqueID != unit.UniqueID)
            player_artifacts.RemoveAt(index--);
          else if (job.UniqueID != jobData.UniqueID)
            player_artifacts.RemoveAt(index--);
        }
      }
      List<ArtifactData> artifactDataList = player_artifacts;
      // ISSUE: reference to a compiler-generated field
      if (AutoEquipArtifacts.\u003C\u003Ef__mg\u0024cache0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AutoEquipArtifacts.\u003C\u003Ef__mg\u0024cache0 = new Comparison<ArtifactData>(AutoEquipArtifacts.CompareArtifactData);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<ArtifactData> fMgCache0 = AutoEquipArtifacts.\u003C\u003Ef__mg\u0024cache0;
      artifactDataList.Sort(fMgCache0);
      RecommendArtifactParams recommendedArtifactParams = MonoSingleton<GameManager>.GetInstanceDirect().MasterParam.GetRecommendedArtifactParams(unit, false);
      Dictionary<ArtifactTypes, bool> dictionary = new Dictionary<ArtifactTypes, bool>();
      dictionary.Add(ArtifactTypes.Arms, false);
      dictionary.Add(ArtifactTypes.Armor, false);
      dictionary.Add(ArtifactTypes.Accessory, false);
      List<ArtifactData> source = new List<ArtifactData>();
      List<ArtifactData> recommendArtifacts1 = recommendedArtifactParams.GetRecommendArtifacts(player_artifacts, ArtifactTypes.Arms, 1);
      if (recommendArtifacts1.Count >= 1)
      {
        source.AddRange((IEnumerable<ArtifactData>) recommendArtifacts1);
        dictionary[ArtifactTypes.Arms] = true;
        if (source.Count >= slot_count)
          return AutoEquipArtifacts.CreatePreparedArtifactDatas(source, slot_count, length);
      }
      List<ArtifactData> recommendArtifacts2 = recommendedArtifactParams.GetRecommendArtifacts(player_artifacts, ArtifactTypes.Armor, 1);
      if (recommendArtifacts2.Count >= 1)
      {
        source.AddRange((IEnumerable<ArtifactData>) recommendArtifacts2);
        dictionary[ArtifactTypes.Armor] = true;
        if (source.Count >= slot_count)
          return AutoEquipArtifacts.CreatePreparedArtifactDatas(source, slot_count, length);
      }
      int num1 = slot_count - source.Count;
      source.AddRange((IEnumerable<ArtifactData>) recommendedArtifactParams.GetRecommendArtifacts(player_artifacts, ArtifactTypes.Accessory, num1));
      if (source.Count >= slot_count)
        return AutoEquipArtifacts.CreatePreparedArtifactDatas(source, slot_count, length);
      if (!dictionary[ArtifactTypes.Arms])
      {
        List<ArtifactData> abilityArtifacts = recommendedArtifactParams.GetMasterAbilityArtifacts(player_artifacts, ArtifactTypes.Arms, 1);
        if (abilityArtifacts.Count >= 1)
        {
          source.AddRange((IEnumerable<ArtifactData>) abilityArtifacts);
          dictionary[ArtifactTypes.Arms] = true;
          if (source.Count >= slot_count)
            return AutoEquipArtifacts.CreatePreparedArtifactDatas(source, slot_count, length);
        }
      }
      if (!dictionary[ArtifactTypes.Armor])
      {
        List<ArtifactData> abilityArtifacts = recommendedArtifactParams.GetMasterAbilityArtifacts(player_artifacts, ArtifactTypes.Armor, 1);
        if (abilityArtifacts.Count >= 1)
        {
          source.AddRange((IEnumerable<ArtifactData>) abilityArtifacts);
          dictionary[ArtifactTypes.Armor] = true;
          if (source.Count >= slot_count)
            return AutoEquipArtifacts.CreatePreparedArtifactDatas(source, slot_count, length);
        }
      }
      int num2 = slot_count - source.Count;
      source.AddRange((IEnumerable<ArtifactData>) recommendedArtifactParams.GetMasterAbilityArtifacts(player_artifacts, ArtifactTypes.Accessory, num2));
      if (source.Count >= slot_count)
        return AutoEquipArtifacts.CreatePreparedArtifactDatas(source, slot_count, length);
      List<ArtifactData> all1 = player_artifacts.FindAll((Predicate<ArtifactData>) (arti => arti.ArtifactParam.type == ArtifactTypes.Arms));
      if (all1 != null && all1.Count > 0 && !dictionary[ArtifactTypes.Arms])
      {
        source.Add(all1[0]);
        if (source.Count >= slot_count)
          return AutoEquipArtifacts.CreatePreparedArtifactDatas(source, slot_count, length);
      }
      List<ArtifactData> all2 = player_artifacts.FindAll((Predicate<ArtifactData>) (arti => arti.ArtifactParam.type == ArtifactTypes.Armor));
      if (all2 != null && all2.Count > 0 && !dictionary[ArtifactTypes.Armor])
      {
        source.Add(all2[0]);
        if (source.Count >= slot_count)
          return AutoEquipArtifacts.CreatePreparedArtifactDatas(source, slot_count, length);
      }
      List<ArtifactData> accessories = player_artifacts.FindAll((Predicate<ArtifactData>) (arti => arti.ArtifactParam.type == ArtifactTypes.Accessory));
      if (accessories != null)
      {
        for (int i = 0; i < accessories.Count && source.Count < slot_count; ++i)
        {
          if (source.FindIndex((Predicate<ArtifactData>) (r => r.ArtifactParam.iname == accessories[i].ArtifactParam.iname)) < 0)
            source.Add(accessories[i]);
        }
      }
      return AutoEquipArtifacts.CreatePreparedArtifactDatas(source, slot_count, length);
    }

    public static List<ArtifactData> CreatePreparedArtifactDatas(
      List<ArtifactData> source,
      int slot_count,
      int slot_max)
    {
      List<ArtifactData> artifactDataList = source;
      // ISSUE: reference to a compiler-generated field
      if (AutoEquipArtifacts.\u003C\u003Ef__mg\u0024cache1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        AutoEquipArtifacts.\u003C\u003Ef__mg\u0024cache1 = new Comparison<ArtifactData>(AutoEquipArtifacts.CompareArtifactType);
      }
      // ISSUE: reference to a compiler-generated field
      Comparison<ArtifactData> fMgCache1 = AutoEquipArtifacts.\u003C\u003Ef__mg\u0024cache1;
      artifactDataList.Sort(fMgCache1);
      ArtifactData[] array = source.ToArray();
      Array.Resize<ArtifactData>(ref array, slot_count);
      Array.Resize<ArtifactData>(ref array, slot_max);
      return new List<ArtifactData>((IEnumerable<ArtifactData>) array);
    }

    private static int CompareArtifactData(ArtifactData a, ArtifactData b)
    {
      if ((int) a.Lv > (int) b.Lv)
        return -1;
      if ((int) a.Lv < (int) b.Lv)
        return 1;
      if ((int) a.ArtifactParam.Rarity > (int) b.ArtifactParam.Rarity)
        return -1;
      return (int) a.ArtifactParam.Rarity < (int) b.ArtifactParam.Rarity ? 1 : 0;
    }

    private static int CompareArtifactType(ArtifactData a, ArtifactData b)
    {
      if (a.ArtifactParam.type == b.ArtifactParam.type)
        return 0;
      if (a.ArtifactParam.type == ArtifactTypes.Arms)
        return -1;
      if (b.ArtifactParam.type == ArtifactTypes.Arms)
        return 1;
      if (a.ArtifactParam.type == ArtifactTypes.Armor)
        return -1;
      return b.ArtifactParam.type == ArtifactTypes.Armor ? 1 : 0;
    }

    public void OnClickItem(GameObject item)
    {
      ArtifactData dataOfClass = DataSource.FindDataOfClass<ArtifactData>(item, (ArtifactData) null);
      if (dataOfClass == null)
        return;
      GlobalVars.SelectedArtifactUniqueID.Set((long) dataOfClass.UniqueID);
      FlowNode_GameObject.ActivateOutputLinks((Component) this, 111);
    }
  }
}
