﻿// Decompiled with JetBrains decompiler
// Type: SRPG.BackHandler
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SRPG
{
  public class BackHandler : MonoBehaviour
  {
    private static List<BackHandler> mHdls = new List<BackHandler>();

    private void OnEnable()
    {
      BackHandler.mHdls.Add(this);
    }

    private void OnDisable()
    {
      BackHandler.mHdls.Remove(this);
    }

    public static void Invoke()
    {
      SceneBattle instance = SceneBattle.Instance;
      if ((bool) ((UnityEngine.Object) instance) && !instance.IsControlBattleUI(SceneBattle.eMaskBattleUI.BACK_KEY))
        return;
      for (int count = BackHandler.mHdls.Count; 0 < count; --count)
      {
        BackHandler mHdl = BackHandler.mHdls[count - 1];
        if (!((UnityEngine.Object) null == (UnityEngine.Object) mHdl))
        {
          ButtonEvent component1 = mHdl.gameObject.GetComponent<ButtonEvent>();
          if ((UnityEngine.Object) component1 != (UnityEngine.Object) null)
          {
            GraphicRaycaster componentInParent = mHdl.gameObject.GetComponentInParent<GraphicRaycaster>();
            if (!((UnityEngine.Object) componentInParent == (UnityEngine.Object) null) && componentInParent.enabled)
            {
              Graphic component2 = mHdl.gameObject.GetComponent<Graphic>();
              if (!((UnityEngine.Object) component2 == (UnityEngine.Object) null) && component2.enabled && component2.raycastTarget)
              {
                CanvasGroup[] componentsInParent = mHdl.GetComponentsInParent<CanvasGroup>();
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
                  component1.OnPointerClick(new PointerEventData(EventSystem.current)
                  {
                    position = (Vector2) mHdl.gameObject.transform.position,
                    clickCount = 1
                  });
                  break;
                }
              }
            }
          }
          else
          {
            Button component2 = mHdl.gameObject.GetComponent<Button>();
            if ((UnityEngine.Object) null != (UnityEngine.Object) component2)
            {
              GraphicRaycaster componentInParent = mHdl.gameObject.GetComponentInParent<GraphicRaycaster>();
              if (!((UnityEngine.Object) null == (UnityEngine.Object) componentInParent) && componentInParent.enabled)
              {
                CanvasGroup[] componentsInParent = mHdl.GetComponentsInParent<CanvasGroup>();
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
                  component2.OnPointerClick(new PointerEventData(EventSystem.current)
                  {
                    position = (Vector2) mHdl.gameObject.transform.position,
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