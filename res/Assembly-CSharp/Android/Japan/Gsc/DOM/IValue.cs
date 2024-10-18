// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.IValue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 059BC2E0-629D-4929-B655-9E68C13AB758
// Assembly location: S:\Program Files (x86)\DMMGamePlayer\games\tagatame\tagatame_Data\Managed\Assembly-CSharp.dll

namespace Gsc.DOM
{
  public interface IValue
  {
    bool IsNull();

    bool IsObject();

    bool IsArray();

    bool IsBool();

    bool IsString();

    bool IsInt();

    bool IsUInt();

    bool IsLong();

    bool IsULong();

    bool IsFloat();

    bool IsDouble();

    IObject GetObject();

    IArray GetArray();

    bool ToBool();

    string ToString();

    int ToInt();

    uint ToUInt();

    long ToLong();

    ulong ToULong();

    float ToFloat();

    double ToDouble();

    IValue this[int index] { get; }

    IValue this[string name] { get; }
  }
}
