// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_SpawnObject3
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace SRPG
{
  [EventActionInfo("New/オブジェクト/配置2(2D)", "シーンにオブジェクトを配置します。", 5592405, 4473992)]
  public class Event2dAction_SpawnObject3 : EventAction
  {
    private Vector2 pvt = new Vector2(0.5f, 0.5f);
    public const string ResourceDir = "Event2dAssets/";
    [StringIsResourcePathPopup(typeof (GameObject), "Event2dAssets/")]
    public string ResourceID;
    public string ObjectID;
    private LoadRequest mResourceLoadRequest;
    [HideInInspector]
    public bool Persistent;
    [HideInInspector]
    public Vector2 Position;
    private GameObject mGO;
    public Event2dAction_SpawnObject3.SiblingOrder Order;
    [HideInInspector]
    public string CharaID;
    [HideInInspector]
    public Event2dAction_SpawnObject3.ActorChildOrder ChildOrder;
    private RectTransform rectTransform;

    public override bool IsPreloadAssets
    {
      get
      {
        return true;
      }
    }

    [DebuggerHidden]
    public override IEnumerator PreloadAssets()
    {
      // ISSUE: object of a compiler-generated type is created
      return (IEnumerator) new Event2dAction_SpawnObject3.\u003CPreloadAssets\u003Ec__Iterator0() { \u0024this = this };
    }

    public override void OnActivate()
    {
      if (this.mResourceLoadRequest != null && this.mResourceLoadRequest.asset != (UnityEngine.Object) null)
      {
        GameObject asset = this.mResourceLoadRequest.asset as GameObject;
        RectTransform component = asset.GetComponent<RectTransform>();
        if ((UnityEngine.Object) component != (UnityEngine.Object) null)
          this.pvt = component.pivot;
        this.mGO = UnityEngine.Object.Instantiate(this.mResourceLoadRequest.asset, Vector3.zero, asset.transform.rotation) as GameObject;
        if (!string.IsNullOrEmpty(this.ObjectID))
          GameUtility.RequireComponent<GameObjectID>(this.mGO).ID = this.ObjectID;
        if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.Root)
        {
          if (this.Persistent && (UnityEngine.Object) TacticsSceneSettings.Instance != (UnityEngine.Object) null)
            this.mGO.transform.SetParent(TacticsSceneSettings.Instance.transform, true);
        }
        else if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.ChildOfActor)
        {
          if (!string.IsNullOrEmpty(this.CharaID))
          {
            EventStandCharaController2 instances = EventStandCharaController2.FindInstances(this.CharaID);
            if ((UnityEngine.Object) instances != (UnityEngine.Object) null)
            {
              this.mGO.transform.SetParent(instances.gameObject.transform, true);
              if (this.ChildOrder == Event2dAction_SpawnObject3.ActorChildOrder.Over)
                this.mGO.transform.SetAsLastSibling();
              else
                this.mGO.transform.SetAsFirstSibling();
              this.rectTransform = this.mGO.GetComponent<RectTransform>();
              if ((UnityEngine.Object) this.rectTransform != (UnityEngine.Object) null)
              {
                this.rectTransform.pivot = this.pvt;
                this.rectTransform.anchoredPosition = Vector2.zero;
                RectTransform rectTransform = this.rectTransform;
                Vector2 vector2_1 = this.convertPosition(this.Position);
                this.rectTransform.anchorMax = vector2_1;
                Vector2 vector2_2 = vector2_1;
                rectTransform.anchorMin = vector2_2;
              }
            }
          }
        }
        else
        {
          float num = 0.0f;
          for (int index = 0; index < this.ActiveCanvas.transform.childCount; ++index)
          {
            Transform child = this.ActiveCanvas.transform.GetChild(index);
            if ((UnityEngine.Object) child.GetComponent<EventDialogBubbleCustom>() != (UnityEngine.Object) null)
            {
              num = child.transform.position.z;
              break;
            }
          }
          this.mGO.transform.SetParent(this.ActiveCanvas.transform, true);
          this.mGO.transform.SetAsLastSibling();
          if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.OnDialog)
          {
            Vector3 position = this.mGO.transform.position;
            position.z = num - 1f;
            this.mGO.transform.position = position;
          }
          else if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.OnStandChara)
          {
            int index1 = -1;
            for (int index2 = 0; index2 < this.ActiveCanvas.transform.childCount; ++index2)
            {
              if ((UnityEngine.Object) this.ActiveCanvas.transform.GetChild(index2).GetComponent<EventDialogBubbleCustom>() != (UnityEngine.Object) null)
              {
                index1 = index2;
                break;
              }
            }
            if (index1 > 0)
              this.mGO.transform.SetSiblingIndex(index1);
            if ((double) num < -1.0)
            {
              Vector3 position = this.mGO.transform.position;
              position.z = num + 1f;
              this.mGO.transform.position = position;
            }
          }
          else if (this.Order == Event2dAction_SpawnObject3.SiblingOrder.OnBackGround)
          {
            int index1 = -1;
            for (int index2 = 0; index2 < this.ActiveCanvas.transform.childCount; ++index2)
            {
              if ((UnityEngine.Object) this.ActiveCanvas.transform.GetChild(index2).GetComponent<EventStandCharaController2>() != (UnityEngine.Object) null)
              {
                index1 = index2;
                break;
              }
            }
            if (index1 > 0)
              this.mGO.transform.SetSiblingIndex(index1);
          }
          this.rectTransform = this.mGO.GetComponent<RectTransform>();
          if ((UnityEngine.Object) this.rectTransform != (UnityEngine.Object) null)
          {
            this.rectTransform.pivot = this.pvt;
            this.rectTransform.anchoredPosition = Vector2.zero;
            RectTransform rectTransform = this.rectTransform;
            Vector2 vector2_1 = this.convertPosition(this.Position);
            this.rectTransform.anchorMax = vector2_1;
            Vector2 vector2_2 = vector2_1;
            rectTransform.anchorMin = vector2_2;
          }
        }
        this.Sequence.SpawnedObjects.Add(this.mGO);
      }
      this.ActivateNext();
    }

    protected override void OnDestroy()
    {
      base.OnDestroy();
      if (!((UnityEngine.Object) this.mGO != (UnityEngine.Object) null) || this.Persistent && !((UnityEngine.Object) this.mGO.transform.parent == (UnityEngine.Object) null))
        return;
      UnityEngine.Object.Destroy((UnityEngine.Object) this.mGO);
    }

    private Vector2 convertPosition(Vector2 pos)
    {
      return Vector2.Scale(pos + Vector2.one, new Vector2(0.5f, 0.5f));
    }

    public enum SiblingOrder
    {
      Root,
      OnDialog,
      OnStandChara,
      OnBackGround,
      ChildOfActor,
    }

    public enum ActorChildOrder
    {
      Over,
      Under,
    }
  }
}
