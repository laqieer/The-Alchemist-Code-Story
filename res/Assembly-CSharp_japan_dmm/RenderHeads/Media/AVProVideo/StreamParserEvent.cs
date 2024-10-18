// Decompiled with JetBrains decompiler
// Type: RenderHeads.Media.AVProVideo.StreamParserEvent
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using UnityEngine.Events;

#nullable disable
namespace RenderHeads.Media.AVProVideo
{
  [Serializable]
  public class StreamParserEvent : UnityEvent<StreamParser, StreamParserEvent.EventType>
  {
    public enum EventType
    {
      Success,
      Failed,
    }
  }
}
