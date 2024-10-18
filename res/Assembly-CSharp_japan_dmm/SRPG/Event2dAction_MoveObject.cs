// Decompiled with JetBrains decompiler
// Type: SRPG.Event2dAction_MoveObject
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using UnityEngine;

#nullable disable
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
