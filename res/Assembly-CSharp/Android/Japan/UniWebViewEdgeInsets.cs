// Decompiled with JetBrains decompiler
// Type: UniWebViewEdgeInsets
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

using System;

[Serializable]
public class UniWebViewEdgeInsets
{
  public int top;
  public int left;
  public int bottom;
  public int right;

  public UniWebViewEdgeInsets(int aTop, int aLeft, int aBottom, int aRight)
  {
    this.top = aTop;
    this.left = aLeft;
    this.bottom = aBottom;
    this.right = aRight;
  }

  public static bool operator ==(UniWebViewEdgeInsets inset1, UniWebViewEdgeInsets inset2)
  {
    return inset1.Equals((object) inset2);
  }

  public static bool operator !=(UniWebViewEdgeInsets inset1, UniWebViewEdgeInsets inset2)
  {
    return !inset1.Equals((object) inset2);
  }

  public override int GetHashCode()
  {
    return (this.top + this.left + this.bottom + this.right).GetHashCode();
  }

  public override bool Equals(object obj)
  {
    if (obj == null || this.GetType() != obj.GetType())
      return false;
    UniWebViewEdgeInsets webViewEdgeInsets = (UniWebViewEdgeInsets) obj;
    if (this.top == webViewEdgeInsets.top && this.left == webViewEdgeInsets.left && this.bottom == webViewEdgeInsets.bottom)
      return this.right == webViewEdgeInsets.right;
    return false;
  }
}
