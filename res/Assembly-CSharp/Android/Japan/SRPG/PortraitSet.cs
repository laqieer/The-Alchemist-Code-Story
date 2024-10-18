// Decompiled with JetBrains decompiler
// Type: SRPG.PortraitSet
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using UnityEngine;

namespace SRPG
{
  public class PortraitSet : ScriptableObject
  {
    public Texture2D Normal;
    public Texture2D Smile;
    public Texture2D Sad;
    public Texture2D Angry;

    public Texture2D GetEmotionImage(PortraitSet.EmotionTypes type)
    {
      switch (type)
      {
        case PortraitSet.EmotionTypes.Smile:
          if (!((UnityEngine.Object) this.Smile == (UnityEngine.Object) null))
            return this.Smile;
          break;
        case PortraitSet.EmotionTypes.Sad:
          if (!((UnityEngine.Object) this.Sad == (UnityEngine.Object) null))
            return this.Sad;
          break;
        case PortraitSet.EmotionTypes.Angry:
          if (!((UnityEngine.Object) this.Angry == (UnityEngine.Object) null))
            return this.Angry;
          break;
      }
      return this.Normal;
    }

    public enum EmotionTypes
    {
      Normal,
      Smile,
      Sad,
      Angry,
    }
  }
}
