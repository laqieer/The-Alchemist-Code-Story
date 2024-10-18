// Decompiled with JetBrains decompiler
// Type: SRPG.AnimEvents.CameraMove
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;

namespace SRPG.AnimEvents
{
  public class CameraMove : AnimEvent
  {
    public CameraMove.eCenterType CenterType;
    public CameraMove.eDistanceType DistanceType;

    public override void OnStart(GameObject go)
    {
      TacticsUnitController componentInParent = go.GetComponentInParent<TacticsUnitController>();
      if (!(bool) ((UnityEngine.Object) componentInParent))
        return;
      SceneBattle instance = SceneBattle.Instance;
      if (!(bool) ((UnityEngine.Object) instance))
        return;
      Vector3 center = Vector3.zero;
      float distance = GameSettings.Instance.GameCamera_SkillCameraDistance;
      bool flag = false;
      if (componentInParent.Unit != null && !componentInParent.Unit.IsNormalSize)
      {
        flag = true;
      }
      else
      {
        foreach (TacticsUnitController skillTarget in componentInParent.GetSkillTargets())
        {
          if (skillTarget.Unit != null && !skillTarget.Unit.IsNormalSize)
          {
            flag = true;
            break;
          }
        }
      }
      if (flag)
        distance += GameSettings.Instance.GameCamera_BigUnitAddDistance;
      List<Vector3> vector3List = new List<Vector3>();
      switch (this.CenterType)
      {
        case CameraMove.eCenterType.Self:
        case CameraMove.eCenterType.All:
          Vector3 centerPosition1 = componentInParent.CenterPosition;
          if (componentInParent.Unit != null && !componentInParent.Unit.IsNormalSize)
            centerPosition1.y += componentInParent.Unit.OffsZ;
          vector3List.Add(centerPosition1);
          if (this.CenterType != CameraMove.eCenterType.All)
            break;
          goto case CameraMove.eCenterType.Targets;
        case CameraMove.eCenterType.Targets:
          using (List<TacticsUnitController>.Enumerator enumerator = componentInParent.GetSkillTargets().GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              TacticsUnitController current = enumerator.Current;
              Vector3 centerPosition2 = current.CenterPosition;
              if (current.Unit != null && !current.Unit.IsNormalSize)
                centerPosition2.y += current.Unit.OffsZ;
              vector3List.Add(centerPosition2);
            }
            break;
          }
      }
      instance.GetCameraTargetView(out center, out distance, vector3List.ToArray());
      switch (this.DistanceType)
      {
        case CameraMove.eDistanceType.Skill:
          distance = GameSettings.Instance.GameCamera_SkillCameraDistance;
          if (flag)
          {
            distance += GameSettings.Instance.GameCamera_BigUnitAddDistance;
            break;
          }
          break;
        case CameraMove.eDistanceType.Far:
          distance = GameSettings.Instance.GameCamera_DefaultDistance;
          break;
        case CameraMove.eDistanceType.MoreFar:
          distance = GameSettings.Instance.GameCamera_MoreFarDistance;
          break;
      }
      instance.InterpCameraTarget(center);
      instance.InterpCameraDistance(distance);
    }

    public enum eCenterType
    {
      Self,
      Targets,
      All,
    }

    public enum eDistanceType
    {
      Skill,
      Far,
      MoreFar,
      Auto,
    }
  }
}
