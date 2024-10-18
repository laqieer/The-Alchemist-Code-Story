// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.ObjectSerializationInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace MessagePack.Internal
{
  internal class ObjectSerializationInfo
  {
    private ObjectSerializationInfo()
    {
    }

    public Type Type { get; set; }

    public bool IsIntKey { get; set; }

    public bool IsStringKey => !this.IsIntKey;

    public bool IsClass { get; set; }

    public bool IsStruct => !this.IsClass;

    public ConstructorInfo BestmatchConstructor { get; set; }

    public ObjectSerializationInfo.EmittableMember[] ConstructorParameters { get; set; }

    public ObjectSerializationInfo.EmittableMember[] Members { get; set; }

    public static ObjectSerializationInfo CreateOrNull(
      Type type,
      bool forceStringKey,
      bool contractless,
      bool allowPrivate)
    {
      TypeInfo typeInfo = System.Reflection.ReflectionExtensions.GetTypeInfo(type);
      bool flag1 = typeInfo.IsClass || typeInfo.IsInterface || typeInfo.IsAbstract;
      MessagePackObjectAttribute customAttribute1 = typeInfo.GetCustomAttribute<MessagePackObjectAttribute>();
      DataContractAttribute customAttribute2 = typeInfo.GetCustomAttribute<DataContractAttribute>();
      if (customAttribute1 == null && customAttribute2 == null && !forceStringKey && !contractless)
        return (ObjectSerializationInfo) null;
      bool flag2 = true;
      Dictionary<int, ObjectSerializationInfo.EmittableMember> dictionary = new Dictionary<int, ObjectSerializationInfo.EmittableMember>();
      Dictionary<string, ObjectSerializationInfo.EmittableMember> source1 = new Dictionary<string, ObjectSerializationInfo.EmittableMember>();
      if (forceStringKey || contractless || customAttribute1 != null && customAttribute1.KeyAsPropertyName)
      {
        flag2 = (forceStringKey ? 1 : (customAttribute1 == null ? 0 : (customAttribute1.KeyAsPropertyName ? 1 : 0))) == 0;
        int num = 0;
        foreach (PropertyInfo runtimeProperty in System.Reflection.ReflectionExtensions.GetRuntimeProperties(type))
        {
          if (System.Reflection.ReflectionExtensions.GetCustomAttribute<IgnoreMemberAttribute>(runtimeProperty, true) == null && System.Reflection.ReflectionExtensions.GetCustomAttribute<IgnoreDataMemberAttribute>(runtimeProperty, true) == null && !runtimeProperty.IsIndexer())
          {
            MethodInfo getMethod = runtimeProperty.GetGetMethod(true);
            MethodInfo setMethod = runtimeProperty.GetSetMethod(true);
            ObjectSerializationInfo.EmittableMember emittableMember = new ObjectSerializationInfo.EmittableMember()
            {
              PropertyInfo = runtimeProperty,
              IsReadable = (object) getMethod != null && (allowPrivate || getMethod.IsPublic) && !getMethod.IsStatic,
              IsWritable = (object) setMethod != null && (allowPrivate || setMethod.IsPublic) && !setMethod.IsStatic,
              StringKey = runtimeProperty.Name
            };
            if (emittableMember.IsReadable || emittableMember.IsWritable)
            {
              emittableMember.IntKey = num++;
              if (flag2)
                dictionary.Add(emittableMember.IntKey, emittableMember);
              else
                source1.Add(emittableMember.StringKey, emittableMember);
            }
          }
        }
        foreach (FieldInfo runtimeField in System.Reflection.ReflectionExtensions.GetRuntimeFields(type))
        {
          if (System.Reflection.ReflectionExtensions.GetCustomAttribute<IgnoreMemberAttribute>(runtimeField, true) == null && System.Reflection.ReflectionExtensions.GetCustomAttribute<IgnoreDataMemberAttribute>(runtimeField, true) == null && System.Reflection.ReflectionExtensions.GetCustomAttribute<CompilerGeneratedAttribute>(runtimeField, true) == null && !runtimeField.IsStatic)
          {
            ObjectSerializationInfo.EmittableMember emittableMember = new ObjectSerializationInfo.EmittableMember()
            {
              FieldInfo = runtimeField,
              IsReadable = allowPrivate || runtimeField.IsPublic,
              IsWritable = allowPrivate || runtimeField.IsPublic && !runtimeField.IsInitOnly,
              StringKey = runtimeField.Name
            };
            if (emittableMember.IsReadable || emittableMember.IsWritable)
            {
              emittableMember.IntKey = num++;
              if (flag2)
                dictionary.Add(emittableMember.IntKey, emittableMember);
              else
                source1.Add(emittableMember.StringKey, emittableMember);
            }
          }
        }
      }
      else
      {
        bool flag3 = true;
        int num = 0;
        foreach (PropertyInfo runtimeProperty in System.Reflection.ReflectionExtensions.GetRuntimeProperties(type))
        {
          if (System.Reflection.ReflectionExtensions.GetCustomAttribute<IgnoreMemberAttribute>(runtimeProperty, true) == null && System.Reflection.ReflectionExtensions.GetCustomAttribute<IgnoreDataMemberAttribute>(runtimeProperty, true) == null && !runtimeProperty.IsIndexer())
          {
            MethodInfo getMethod = runtimeProperty.GetGetMethod(true);
            MethodInfo setMethod = runtimeProperty.GetSetMethod(true);
            ObjectSerializationInfo.EmittableMember emittableMember = new ObjectSerializationInfo.EmittableMember()
            {
              PropertyInfo = runtimeProperty,
              IsReadable = (object) getMethod != null && (allowPrivate || getMethod.IsPublic) && !getMethod.IsStatic,
              IsWritable = (object) setMethod != null && (allowPrivate || setMethod.IsPublic) && !setMethod.IsStatic
            };
            if (emittableMember.IsReadable || emittableMember.IsWritable)
            {
              KeyAttribute keyAttribute;
              if (customAttribute1 != null)
              {
                keyAttribute = System.Reflection.ReflectionExtensions.GetCustomAttribute<KeyAttribute>(runtimeProperty, true);
                if (keyAttribute == null)
                  throw new MessagePackDynamicObjectResolverException("all public members must mark KeyAttribute or IgnoreMemberAttribute. type: " + type.FullName + " member:" + runtimeProperty.Name);
                if (!keyAttribute.IntKey.HasValue && keyAttribute.StringKey == null)
                  throw new MessagePackDynamicObjectResolverException("both IntKey and StringKey are null. type: " + type.FullName + " member:" + runtimeProperty.Name);
              }
              else
              {
                DataMemberAttribute customAttribute3 = System.Reflection.ReflectionExtensions.GetCustomAttribute<DataMemberAttribute>(runtimeProperty, true);
                if (customAttribute3 == null)
                  throw new MessagePackDynamicObjectResolverException("all public members must mark DataMemberAttribute or IgnoreMemberAttribute. type: " + type.FullName + " member:" + runtimeProperty.Name);
                keyAttribute = customAttribute3.Order == -1 ? (customAttribute3.Name == null ? new KeyAttribute(runtimeProperty.Name) : new KeyAttribute(customAttribute3.Name)) : new KeyAttribute(customAttribute3.Order);
              }
              if (flag3)
              {
                flag3 = false;
                flag2 = keyAttribute.IntKey.HasValue;
              }
              else if (flag2 && !keyAttribute.IntKey.HasValue || !flag2 && keyAttribute.StringKey == null)
                throw new MessagePackDynamicObjectResolverException("all members key type must be same. type: " + type.FullName + " member:" + runtimeProperty.Name);
              if (flag2)
              {
                emittableMember.IntKey = keyAttribute.IntKey.Value;
                if (dictionary.ContainsKey(emittableMember.IntKey))
                  throw new MessagePackDynamicObjectResolverException("key is duplicated, all members key must be unique. type: " + type.FullName + " member:" + runtimeProperty.Name);
                dictionary.Add(emittableMember.IntKey, emittableMember);
              }
              else
              {
                emittableMember.StringKey = keyAttribute.StringKey;
                if (source1.ContainsKey(emittableMember.StringKey))
                  throw new MessagePackDynamicObjectResolverException("key is duplicated, all members key must be unique. type: " + type.FullName + " member:" + runtimeProperty.Name);
                emittableMember.IntKey = num++;
                source1.Add(emittableMember.StringKey, emittableMember);
              }
            }
          }
        }
        foreach (FieldInfo runtimeField in System.Reflection.ReflectionExtensions.GetRuntimeFields(type))
        {
          if (System.Reflection.ReflectionExtensions.GetCustomAttribute<IgnoreMemberAttribute>(runtimeField, true) == null && System.Reflection.ReflectionExtensions.GetCustomAttribute<IgnoreDataMemberAttribute>(runtimeField, true) == null && System.Reflection.ReflectionExtensions.GetCustomAttribute<CompilerGeneratedAttribute>(runtimeField, true) == null && !runtimeField.IsStatic)
          {
            ObjectSerializationInfo.EmittableMember emittableMember = new ObjectSerializationInfo.EmittableMember()
            {
              FieldInfo = runtimeField,
              IsReadable = allowPrivate || runtimeField.IsPublic,
              IsWritable = allowPrivate || runtimeField.IsPublic && !runtimeField.IsInitOnly
            };
            if (emittableMember.IsReadable || emittableMember.IsWritable)
            {
              KeyAttribute keyAttribute;
              if (customAttribute1 != null)
              {
                keyAttribute = System.Reflection.ReflectionExtensions.GetCustomAttribute<KeyAttribute>(runtimeField, true);
                if (keyAttribute == null)
                  throw new MessagePackDynamicObjectResolverException("all public members must mark KeyAttribute or IgnoreMemberAttribute. type: " + type.FullName + " member:" + runtimeField.Name);
                if (!keyAttribute.IntKey.HasValue && keyAttribute.StringKey == null)
                  throw new MessagePackDynamicObjectResolverException("both IntKey and StringKey are null. type: " + type.FullName + " member:" + runtimeField.Name);
              }
              else
              {
                DataMemberAttribute customAttribute4 = System.Reflection.ReflectionExtensions.GetCustomAttribute<DataMemberAttribute>(runtimeField, true);
                if (customAttribute4 == null)
                  throw new MessagePackDynamicObjectResolverException("all public members must mark DataMemberAttribute or IgnoreMemberAttribute. type: " + type.FullName + " member:" + runtimeField.Name);
                keyAttribute = customAttribute4.Order == -1 ? (customAttribute4.Name == null ? new KeyAttribute(runtimeField.Name) : new KeyAttribute(customAttribute4.Name)) : new KeyAttribute(customAttribute4.Order);
              }
              if (flag3)
              {
                flag3 = false;
                flag2 = keyAttribute.IntKey.HasValue;
              }
              else if (flag2 && !keyAttribute.IntKey.HasValue || !flag2 && keyAttribute.StringKey == null)
                throw new MessagePackDynamicObjectResolverException("all members key type must be same. type: " + type.FullName + " member:" + runtimeField.Name);
              if (flag2)
              {
                emittableMember.IntKey = keyAttribute.IntKey.Value;
                if (dictionary.ContainsKey(emittableMember.IntKey))
                  throw new MessagePackDynamicObjectResolverException("key is duplicated, all members key must be unique. type: " + type.FullName + " member:" + runtimeField.Name);
                dictionary.Add(emittableMember.IntKey, emittableMember);
              }
              else
              {
                emittableMember.StringKey = keyAttribute.StringKey;
                if (source1.ContainsKey(emittableMember.StringKey))
                  throw new MessagePackDynamicObjectResolverException("key is duplicated, all members key must be unique. type: " + type.FullName + " member:" + runtimeField.Name);
                emittableMember.IntKey = num++;
                source1.Add(emittableMember.StringKey, emittableMember);
              }
            }
          }
        }
      }
      IEnumerator<ConstructorInfo> ctorEnumerator = (IEnumerator<ConstructorInfo>) null;
      ConstructorInfo ctor = typeInfo.DeclaredConstructors.Where<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.IsPublic)).SingleOrDefault<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => System.Reflection.ReflectionExtensions.GetCustomAttribute<SerializationConstructorAttribute>(x, false) != null));
      if ((object) ctor == null)
      {
        ctorEnumerator = typeInfo.DeclaredConstructors.Where<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.IsPublic)).OrderBy<ConstructorInfo, int>((Func<ConstructorInfo, int>) (x => x.GetParameters().Length)).GetEnumerator();
        if (ctorEnumerator.MoveNext())
          ctor = ctorEnumerator.Current;
      }
      if ((object) ctor == null && flag1)
        throw new MessagePackDynamicObjectResolverException("can't find public constructor. type:" + type.FullName);
      List<ObjectSerializationInfo.EmittableMember> emittableMemberList = new List<ObjectSerializationInfo.EmittableMember>();
      if ((object) ctor != null)
      {
        ILookup<string, KeyValuePair<string, ObjectSerializationInfo.EmittableMember>> lookup = source1.ToLookup<KeyValuePair<string, ObjectSerializationInfo.EmittableMember>, string, KeyValuePair<string, ObjectSerializationInfo.EmittableMember>>((Func<KeyValuePair<string, ObjectSerializationInfo.EmittableMember>, string>) (x => x.Key), (Func<KeyValuePair<string, ObjectSerializationInfo.EmittableMember>, KeyValuePair<string, ObjectSerializationInfo.EmittableMember>>) (x => x), (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        do
        {
          emittableMemberList.Clear();
          int key = 0;
          foreach (ParameterInfo parameter in ctor.GetParameters())
          {
            ObjectSerializationInfo.EmittableMember emittableMember;
            if (flag2)
            {
              if (dictionary.TryGetValue(key, out emittableMember))
              {
                if ((object) parameter.ParameterType == (object) emittableMember.Type && emittableMember.IsReadable)
                {
                  emittableMemberList.Add(emittableMember);
                }
                else
                {
                  if (ctorEnumerator != null)
                  {
                    ctor = (ConstructorInfo) null;
                    continue;
                  }
                  throw new MessagePackDynamicObjectResolverException("can't find matched constructor parameter, parameterType mismatch. type:" + type.FullName + " parameterIndex:" + (object) key + " paramterType:" + parameter.ParameterType.Name);
                }
              }
              else
              {
                if (ctorEnumerator != null)
                {
                  ctor = (ConstructorInfo) null;
                  continue;
                }
                throw new MessagePackDynamicObjectResolverException("can't find matched constructor parameter, index not found. type:" + type.FullName + " parameterIndex:" + (object) key);
              }
            }
            else
            {
              IEnumerable<KeyValuePair<string, ObjectSerializationInfo.EmittableMember>> source2 = lookup[parameter.Name];
              switch (source2.Count<KeyValuePair<string, ObjectSerializationInfo.EmittableMember>>())
              {
                case 0:
                  if (ctorEnumerator == null)
                    throw new MessagePackDynamicObjectResolverException("can't find matched constructor parameter, index not found. type:" + type.FullName + " parameterName:" + parameter.Name);
                  ctor = (ConstructorInfo) null;
                  continue;
                case 1:
                  emittableMember = source2.First<KeyValuePair<string, ObjectSerializationInfo.EmittableMember>>().Value;
                  if ((object) parameter.ParameterType == (object) emittableMember.Type && emittableMember.IsReadable)
                  {
                    emittableMemberList.Add(emittableMember);
                    break;
                  }
                  if (ctorEnumerator != null)
                  {
                    ctor = (ConstructorInfo) null;
                    continue;
                  }
                  throw new MessagePackDynamicObjectResolverException("can't find matched constructor parameter, parameterType mismatch. type:" + type.FullName + " parameterName:" + parameter.Name + " paramterType:" + parameter.ParameterType.Name);
                default:
                  if (ctorEnumerator != null)
                  {
                    ctor = (ConstructorInfo) null;
                    continue;
                  }
                  throw new MessagePackDynamicObjectResolverException("duplicate matched constructor parameter name:" + type.FullName + " parameterName:" + parameter.Name + " paramterType:" + parameter.ParameterType.Name);
              }
            }
            ++key;
          }
        }
        while (ObjectSerializationInfo.TryGetNextConstructor(ctorEnumerator, ref ctor));
        if ((object) ctor == null)
          throw new MessagePackDynamicObjectResolverException("can't find matched constructor. type:" + type.FullName);
      }
      ObjectSerializationInfo.EmittableMember[] emittableMemberArray = !flag2 ? source1.Values.OrderBy<ObjectSerializationInfo.EmittableMember, int>((Func<ObjectSerializationInfo.EmittableMember, int>) (x =>
      {
        DataMemberAttribute dataMemberAttribute = x.GetDataMemberAttribute();
        return dataMemberAttribute == null ? int.MaxValue : dataMemberAttribute.Order;
      })).ToArray<ObjectSerializationInfo.EmittableMember>() : dictionary.Values.OrderBy<ObjectSerializationInfo.EmittableMember, int>((Func<ObjectSerializationInfo.EmittableMember, int>) (x => x.IntKey)).ToArray<ObjectSerializationInfo.EmittableMember>();
      return new ObjectSerializationInfo()
      {
        Type = type,
        IsClass = flag1,
        BestmatchConstructor = ctor,
        ConstructorParameters = emittableMemberList.ToArray(),
        IsIntKey = flag2,
        Members = emittableMemberArray
      };
    }

    private static bool TryGetNextConstructor(
      IEnumerator<ConstructorInfo> ctorEnumerator,
      ref ConstructorInfo ctor)
    {
      if (ctorEnumerator == null || (object) ctor != null)
        return false;
      if (ctorEnumerator.MoveNext())
      {
        ctor = ctorEnumerator.Current;
        return true;
      }
      ctor = (ConstructorInfo) null;
      return false;
    }

    public class EmittableMember
    {
      public bool IsProperty => (object) this.PropertyInfo != null;

      public bool IsField => (object) this.FieldInfo != null;

      public bool IsWritable { get; set; }

      public bool IsReadable { get; set; }

      public int IntKey { get; set; }

      public string StringKey { get; set; }

      public Type Type => this.IsField ? this.FieldInfo.FieldType : this.PropertyInfo.PropertyType;

      public FieldInfo FieldInfo { get; set; }

      public PropertyInfo PropertyInfo { get; set; }

      public string Name => this.IsProperty ? this.PropertyInfo.Name : this.FieldInfo.Name;

      public bool IsValueType
      {
        get
        {
          return System.Reflection.ReflectionExtensions.GetTypeInfo((!this.IsProperty ? (MemberInfo) this.FieldInfo : (MemberInfo) this.PropertyInfo).DeclaringType).IsValueType;
        }
      }

      public MessagePackFormatterAttribute GetMessagePackFormatterAttribtue()
      {
        return this.IsProperty ? System.Reflection.ReflectionExtensions.GetCustomAttribute<MessagePackFormatterAttribute>(this.PropertyInfo, true) : System.Reflection.ReflectionExtensions.GetCustomAttribute<MessagePackFormatterAttribute>(this.FieldInfo, true);
      }

      public DataMemberAttribute GetDataMemberAttribute()
      {
        return this.IsProperty ? System.Reflection.ReflectionExtensions.GetCustomAttribute<DataMemberAttribute>(this.PropertyInfo, true) : System.Reflection.ReflectionExtensions.GetCustomAttribute<DataMemberAttribute>(this.FieldInfo, true);
      }

      public void EmitLoadValue(ILGenerator il)
      {
        if (this.IsProperty)
          il.EmitCall(this.PropertyInfo.GetGetMethod(true));
        else
          il.Emit(OpCodes.Ldfld, this.FieldInfo);
      }

      public void EmitStoreValue(ILGenerator il)
      {
        if (this.IsProperty)
          il.EmitCall(this.PropertyInfo.GetSetMethod(true));
        else
          il.Emit(OpCodes.Stfld, this.FieldInfo);
      }
    }
  }
}
