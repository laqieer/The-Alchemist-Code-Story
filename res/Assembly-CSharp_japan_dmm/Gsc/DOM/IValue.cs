// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.IValue
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
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
