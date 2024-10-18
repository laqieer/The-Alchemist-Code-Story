// Decompiled with JetBrains decompiler
// Type: Gsc.DOM.Json.Value
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

#nullable disable
namespace Gsc.DOM.Json
{
  public struct Value : IValue
  {
    private readonly rapidjson.Value value;

    public Value(rapidjson.Value value) => this.value = value;

    public bool IsNull() => this.value.IsNull();

    public bool IsObject() => this.value.IsObject();

    public bool IsArray() => this.value.IsArray();

    public bool IsBool() => this.value.IsBool();

    public bool IsString() => this.value.IsString();

    public bool IsInt() => this.value.IsInt();

    public bool IsUInt() => this.value.IsUInt();

    public bool IsLong() => this.value.IsLong();

    public bool IsULong() => this.value.IsULong();

    public bool IsFloat() => this.value.IsFloat();

    public bool IsDouble() => this.value.IsDouble();

    public Object GetObject() => new Object(this.value.GetObject());

    public Array GetArray() => new Array(this.value.GetArray());

    IObject IValue.GetObject() => (IObject) this.GetObject();

    IArray IValue.GetArray() => (IArray) this.GetArray();

    public bool ToBool() => this.value.ToBool();

    public override string ToString() => this.value.ToString();

    public int ToInt() => this.value.ToInt();

    public uint ToUInt() => this.value.ToUInt();

    public long ToLong() => this.value.ToLong();

    public ulong ToULong() => this.value.ToULong();

    public float ToFloat() => this.value.ToFloat();

    public double ToDouble() => this.value.ToDouble();

    public bool GetValueByPointer(string pointer, bool defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public string GetValueByPointer(string pointer, string defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public int GetValueByPointer(string pointer, int defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public uint GetValueByPointer(string pointer, uint defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public long GetValueByPointer(string pointer, long defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public ulong GetValueByPointer(string pointer, ulong defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public float GetValueByPointer(string pointer, float defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public double GetValueByPointer(string pointer, double defaultValue)
    {
      return this.value.GetValueByPointer(pointer, defaultValue);
    }

    public Value this[string name] => new Value(this.value[name]);

    public Value this[int index] => new Value(this.value[index]);

    IValue IValue.this[string name] => (IValue) this[name];

    IValue IValue.this[int index] => (IValue) this[index];

    public static explicit operator bool(Value self) => self.ToBool();

    public static explicit operator string(Value self) => self.ToString();

    public static explicit operator int(Value self) => self.ToInt();

    public static explicit operator uint(Value self) => self.ToUInt();

    public static explicit operator long(Value self) => self.ToLong();

    public static explicit operator ulong(Value self) => self.ToULong();

    public static explicit operator float(Value self) => self.ToFloat();

    public static explicit operator double(Value self) => self.ToDouble();
  }
}
