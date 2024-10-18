// Decompiled with JetBrains decompiler
// Type: SRPG.BackHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#nullable disable
namespace SRPG
{
  public class BackHandler : MonoBehaviour
  {
    private static List<BackHandler> mHdls = new List<BackHandler>();

    private void OnEnable() => BackHandler.mHdls.Add(this);

    private void OnDisable() => BackHandler.mHdls.Remove(this);

    public static void Invoke()
    {
      SceneBattle instance = SceneBattle.Instance;
      if (Object.op_Implicit((Object) instance) && !instance.IsControlBattleUI(SceneBattle.eMaskBattleUI.BACK_KEY))
        return;
      for (int count = BackHandler.mHdls.Count; 0 < count; --count)
      {
        BackHandler mHdl = BackHandler.mHdls[count - 1];
        if (!Object.op_Equality((Object) null, (Object) mHdl))
        {
          ButtonEvent component1 = ((Component) mHdl).gameObject.GetComponent<ButtonEvent>();
          if (Object.op_Inequality((Object) component1, (Object) null))
          {
            GraphicRaycaster componentInParent = ((Component) mHdl).gameObject.GetComponentInParent<GraphicRaycaster>();
            if (!Object.op_Equality((Object) componentInParent, (Object) null) && ((Behaviour) componentInParent).enabled)
            {
              Graphic component2 = ((Component) mHdl).gameObject.GetComponent<Graphic>();
              if (!Object.op_Equality((Object) component2, (Object) null) && ((Behaviour) component2).enabled && component2.raycastTarget)
              {
                CanvasGroup[] componentsInParent = ((Component) mHdl).GetComponentsInParent<CanvasGroup>();
                bool flag = false;
                for (int index = 0; index < componentsInParent.Length; ++index)
                {
                  if (!componentsInParent[index].blocksRaycasts)
                  {
                    flag = true;
                    break;
                  }
                  if (componentsInParent[index].ignoreParentGroups)
                    break;
                }
                if (!flag)
                {
                  ((EventTrigger) component1).OnPointerClick(new PointerEventData(EventSystem.current)
                  {
                    position = Vector2.op_Implicit(((Component) mHdl).gameObject.transform.position),
                    clickCount = 1
                  });
                  break;
                }
              }
            }
          }
          else
          {
            Button component3 = ((Component) mHdl).gameObject.GetComponent<Button>();
            if (Object.op_Inequality((Object) null, (Object) component3))
            {
              GraphicRaycaster componentInParent = ((Component) mHdl).gameObject.GetComponentInParent<GraphicRaycaster>();
              if (!Object.op_Equality((Object) null, (Object) componentInParent) && ((Behaviour) componentInParent).enabled)
              {
                CanvasGroup[] componentsInParent = ((Component) mHdl).GetComponentsInParent<CanvasGroup>();
                bool flag = false;
                for (int index = 0; index < componentsInParent.Length; ++index)
                {
                  if (!componentsInParent[index].blocksRaycasts)
                  {
                    flag = true;
                    break;
                  }
                  if (componentsInParent[index].ignoreParentGroups)
                    break;
                }
                if (!flag)
                {
                  component3.OnPointerClick(new PointerEventData(EventSystem.current)
                  {
                    position = Vector2.op_Implicit(((Component) mHdl).gameObject.transform.position),
                    clickCount = 1
                  });
                  break;
                }
              }
            }
          }
        }
      }
    }
  }
}
