// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MoveObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  [EventActionInfo("オブジェクト/パン(2D)", "パン", 5592405, 4473992)]
  public class Event2dAction_MoveObject : EventAction
  {
    public Vector3 MoveFrom;
    public Vector3 MoveTo;
    public float MoveTime;
    public bool Async;
    private Vector3 FromPosition;
    private Vector3 ToPosition;

    public override void PreStart()
    {
    }

    public override void OnActivate()
    {
    }

    public override void Update()
    {
    }
  }
}
