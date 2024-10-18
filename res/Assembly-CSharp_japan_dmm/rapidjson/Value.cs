// Decompiled with JetBrains decompiler
// Type: rapidjson.Value
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;

#nullable disable
namespace rapidjson
{
  public struct Value
  {
    private readonly Document doc;
    private IntPtr ptr;

    public Value(Document doc, ref IntPtr ptr)
    {
      this.doc = doc;
      this.ptr = ptr;
    }

    public Object GetObject() => new Object(this.doc, ref this.ptr);

    public bool IsAllocated() => this.doc != null && this.ptr != IntPtr.Zero;

    public bool IsObject()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_object(this.ptr);
    }

    public Array GetArray() => new Array(this.doc, ref this.ptr);

    public bool IsArray()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_array(this.ptr);
    }

    public Value this[string name]
    {
      get
      {
        this.doc.CheckDisposed();
        IntPtr ptr = DLL.Get(ref this.ptr, name);
        return new Value(this.doc, ref ptr);
      }
    }

    public Value this[int index]
    {
      get
      {
        if (index < 0)
          throw new IndexOutOfRangeException();
        this.doc.CheckDisposed();
        IntPtr ptr = DLL.Get(ref this.ptr, (uint) index);
        return new Value(this.doc, ref ptr);
      }
    }

    public static explicit operator int(Value self) => self.ToInt();

    public int ToInt()
    {
      this.doc.CheckDisposed();
      int dst;
      if (!DLL._rapidjson_get_int(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsInt()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_int(this.ptr);
    }

    public static explicit operator uint(Value self) => self.ToUInt();

    public uint ToUInt()
    {
      this.doc.CheckDisposed();
      uint dst;
      if (!DLL._rapidjson_get_uint(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsUInt()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_uint(this.ptr);
    }

    public static explicit operator long(Value self) => self.ToLong();

    public long ToLong()
    {
      this.doc.CheckDisposed();
      long dst;
      if (!DLL._rapidjson_get_int64(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsLong()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_int64(this.ptr);
    }

    public static explicit operator ulong(Value self) => self.ToULong();

    public ulong ToULong()
    {
      this.doc.CheckDisposed();
      ulong dst;
      if (!DLL._rapidjson_get_uint64(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsULong()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_uint64(this.ptr);
    }

    public static explicit operator float(Value self) => self.ToFloat();

    public float ToFloat()
    {
      if (this.IsInt())
        return (float) this.ToInt();
      this.doc.CheckDisposed();
      float dst;
      if (!DLL._rapidjson_get_float(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsFloat()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_float(this.ptr);
    }

    public static explicit operator double(Value self) => self.ToDouble();

    public double ToDouble()
    {
      this.doc.CheckDisposed();
      double dst;
      if (!DLL._rapidjson_get_double(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsDouble()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_double(this.ptr);
    }

    public static explicit operator bool(Value self) => self.ToBool();

    public bool ToBool()
    {
      this.doc.CheckDisposed();
      bool dst;
      if (!DLL._rapidjson_get_bool(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsBool()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_bool(this.ptr);
    }

    public static explicit operator string(Value self) => self.ToString();

    public override string ToString()
    {
      this.doc.CheckDisposed();
      string dst;
      if (!DLL._rapidjson_get_string(this.ptr, out dst))
        throw new InvalidCastException();
      return dst;
    }

    public bool IsString()
    {
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_string(this.ptr);
    }

    public bool IsNull()
    {
      if (this.ptr == IntPtr.Zero)
        return true;
      this.doc.CheckDisposed();
      return DLL._rapidjson_is_null(this.ptr);
    }

    public bool TryGetValueByPointer(string pointer, out Value value)
    {
      this.doc.CheckDisposed();
      IntPtr dst = IntPtr.Zero;
      bool isValid = false;
      bool valueByPointer = DLL._rapidjson_get_value_by_pointer(this.ptr, pointer, (uint) pointer.Length, out isValid, out dst);
      if (!isValid)
        throw new InvalidPointerError(pointer);
      value = new Value(!valueByPointer ? (Document) null : this.doc, ref dst);
      return valueByPointer;
    }

    public int GetValueByPointer(string pointer, int defaultValue)
    {
      Value obj;
      return this.TryGetValueByPointer(pointer, out obj) && obj.IsInt() ? (int) obj : defaultValue;
    }

    public uint GetValueByPointer(string pointer, uint defaultValue)
    {
      Value obj;
      return this.TryGetValueByPointer(pointer, out obj) && obj.IsUInt() ? (uint) obj : defaultValue;
    }

    public long GetValueByPointer(string pointer, long defaultValue)
    {
      Value obj;
      return this.TryGetValueByPointer(pointer, out obj) && obj.IsLong() ? (long) obj : defaultValue;
    }

    public ulong GetValueByPointer(string pointer, ulong defaultValue)
    {
      Value obj;
      return this.TryGetValueByPointer(pointer, out obj) && obj.IsULong() ? (ulong) obj : defaultValue;
    }

    public float GetValueByPointer(string pointer, float defaultValue)
    {
      Value obj;
      return this.TryGetValueByPointer(pointer, out obj) && obj.IsFloat() ? (float) obj : defaultValue;
    }

    public double GetValueByPointer(string pointer, double defaultValue)
    {
      Value obj;
      return this.TryGetValueByPointer(pointer, out obj) && obj.IsDouble() ? (double) obj : defaultValue;
    }

    public bool GetValueByPointer(string pointer, bool defaultValue)
    {
      Value obj;
      return this.TryGetValueByPointer(pointer, out obj) && obj.IsBool() ? (bool) obj : defaultValue;
    }

    public string GetValueByPointer(string pointer, string defaultValue)
    {
      Value obj;
      return this.TryGetValueByPointer(pointer, out obj) && obj.IsString() ? (string) obj : defaultValue;
    }
  }
}
