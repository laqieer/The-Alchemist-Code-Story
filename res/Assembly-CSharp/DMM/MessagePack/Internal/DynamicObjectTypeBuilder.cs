// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.DynamicObjectTypeBuilder
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading;

#nullable disable
namespace MessagePack.Internal
{
  internal static class DynamicObjectTypeBuilder
  {
    private static readonly Regex SubtractFullNameRegex = new Regex(", Version=\\d+.\\d+.\\d+.\\d+, Culture=\\w+, PublicKeyToken=\\w+");
    private static int nameSequence = 0;
    private static HashSet<Type> ignoreTypes = new HashSet<Type>()
    {
      typeof (object),
      typeof (short),
      typeof (int),
      typeof (long),
      typeof (ushort),
      typeof (uint),
      typeof (ulong),
      typeof (float),
      typeof (double),
      typeof (bool),
      typeof (byte),
      typeof (sbyte),
      typeof (Decimal),
      typeof (char),
      typeof (string),
      typeof (Guid),
      typeof (TimeSpan),
      typeof (DateTime),
      typeof (DateTimeOffset),
      typeof (Nil)
    };
    private static readonly Type refByte = typeof (byte[]).MakeByRefType();
    private static readonly Type refInt = typeof (int).MakeByRefType();
    private static readonly MethodInfo getFormatterWithVerify = ((IEnumerable<MethodInfo>) System.Reflection.ReflectionExtensions.GetRuntimeMethods(typeof (FormatterResolverExtensions))).First<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == "GetFormatterWithVerify"));
    private static readonly Func<Type, MethodInfo> getSerialize = (Func<Type, MethodInfo>) (t => System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (IMessagePackFormatter<>).MakeGenericType(t), "Serialize", new Type[4]
    {
      DynamicObjectTypeBuilder.refByte,
      typeof (int),
      t,
      typeof (IFormatterResolver)
    }));
    private static readonly Func<Type, MethodInfo> getDeserialize = (Func<Type, MethodInfo>) (t => System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (IMessagePackFormatter<>).MakeGenericType(t), "Deserialize", new Type[4]
    {
      typeof (byte[]),
      typeof (int),
      typeof (IFormatterResolver),
      DynamicObjectTypeBuilder.refInt
    }));
    private static readonly ConstructorInfo invalidOperationExceptionConstructor = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (InvalidOperationException)).DeclaredConstructors.First<ConstructorInfo>((Func<ConstructorInfo, bool>) (x =>
    {
      ParameterInfo[] parameters = x.GetParameters();
      return parameters.Length == 1 && (object) parameters[0].ParameterType == (object) typeof (string);
    }));
    private static readonly MethodInfo onBeforeSerialize = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (IMessagePackSerializationCallbackReceiver), "OnBeforeSerialize", Type.EmptyTypes);
    private static readonly MethodInfo onAfterDeserialize = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (IMessagePackSerializationCallbackReceiver), "OnAfterDeserialize", Type.EmptyTypes);
    private static readonly ConstructorInfo objectCtor = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (object)).DeclaredConstructors.First<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.GetParameters().Length == 0));

    public static TypeInfo BuildType(
      DynamicAssembly assembly,
      Type type,
      bool forceStringKey,
      bool contractless)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DynamicObjectTypeBuilder.\u003CBuildType\u003Ec__AnonStorey1 typeCAnonStorey1_1 = new DynamicObjectTypeBuilder.\u003CBuildType\u003Ec__AnonStorey1();
      if (DynamicObjectTypeBuilder.ignoreTypes.Contains(type))
        return (TypeInfo) null;
      ObjectSerializationInfo orNull = ObjectSerializationInfo.CreateOrNull(type, forceStringKey, contractless, false);
      if (orNull == null)
        return (TypeInfo) null;
      Type type1 = typeof (IMessagePackFormatter<>).MakeGenericType(type);
      TypeBuilder typeBuilder = assembly.DefineType("MessagePack.Formatters." + DynamicObjectTypeBuilder.SubtractFullNameRegex.Replace(type.FullName, string.Empty).Replace(".", "_") + "Formatter" + (object) Interlocked.Increment(ref DynamicObjectTypeBuilder.nameSequence), TypeAttributes.Public | TypeAttributes.Sealed, (Type) null, new Type[1]
      {
        type1
      });
      // ISSUE: reference to a compiler-generated field
      typeCAnonStorey1_1.stringByteKeysField = (FieldBuilder) null;
      // ISSUE: reference to a compiler-generated field
      typeCAnonStorey1_1.customFormatterLookup = (Dictionary<ObjectSerializationInfo.EmittableMember, FieldInfo>) null;
      if (orNull.IsStringKey)
      {
        ConstructorBuilder method = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
        // ISSUE: reference to a compiler-generated field
        typeCAnonStorey1_1.stringByteKeysField = typeBuilder.DefineField("stringByteKeys", typeof (byte[][]), FieldAttributes.Private | FieldAttributes.InitOnly);
        ILGenerator ilGenerator = method.GetILGenerator();
        // ISSUE: reference to a compiler-generated field
        DynamicObjectTypeBuilder.BuildConstructor(type, orNull, (ConstructorInfo) method, typeCAnonStorey1_1.stringByteKeysField, ilGenerator);
        // ISSUE: reference to a compiler-generated field
        typeCAnonStorey1_1.customFormatterLookup = DynamicObjectTypeBuilder.BuildCustomFormatterField(typeBuilder, orNull, ilGenerator);
        ilGenerator.Emit(OpCodes.Ret);
      }
      else
      {
        ILGenerator ilGenerator = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes).GetILGenerator();
        ilGenerator.EmitLoadThis();
        ilGenerator.Emit(OpCodes.Call, DynamicObjectTypeBuilder.objectCtor);
        // ISSUE: reference to a compiler-generated field
        typeCAnonStorey1_1.customFormatterLookup = DynamicObjectTypeBuilder.BuildCustomFormatterField(typeBuilder, orNull, ilGenerator);
        ilGenerator.Emit(OpCodes.Ret);
      }
      ILGenerator il1 = typeBuilder.DefineMethod("Serialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual, typeof (int), new Type[4]
      {
        typeof (byte[]).MakeByRefType(),
        typeof (int),
        type,
        typeof (IFormatterResolver)
      }).GetILGenerator();
      DynamicObjectTypeBuilder.BuildSerialize(type, orNull, il1, (Action) (() =>
      {
        il1.EmitLoadThis();
        il1.EmitLdfld((FieldInfo) stringByteKeysField);
      }), (Func<int, ObjectSerializationInfo.EmittableMember, Action>) ((index, member) =>
      {
        // ISSUE: variable of a compiler-generated type
        DynamicObjectTypeBuilder.\u003CBuildType\u003Ec__AnonStorey1 typeCAnonStorey1 = typeCAnonStorey1_1;
        // ISSUE: variable of a compiler-generated type
        DynamicObjectTypeBuilder.\u003CBuildType\u003Ec__AnonStorey0 typeCAnonStorey0 = this;
        FieldInfo fi;
        return !customFormatterLookup.TryGetValue(member, out fi) ? (Action) null : (Action) (() =>
        {
          // ISSUE: reference to a compiler-generated field
          typeCAnonStorey0.il.EmitLoadThis();
          // ISSUE: reference to a compiler-generated field
          typeCAnonStorey0.il.EmitLdfld(fi);
        });
      }), 1);
      ILGenerator il2 = typeBuilder.DefineMethod("Deserialize", MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual, type, new Type[4]
      {
        typeof (byte[]),
        typeof (int),
        typeof (IFormatterResolver),
        typeof (int).MakeByRefType()
      }).GetILGenerator();
      DynamicObjectTypeBuilder.BuildDeserialize(type, orNull, il2, (Func<int, ObjectSerializationInfo.EmittableMember, Action>) ((index, member) =>
      {
        // ISSUE: variable of a compiler-generated type
        DynamicObjectTypeBuilder.\u003CBuildType\u003Ec__AnonStorey1 typeCAnonStorey1 = typeCAnonStorey1_1;
        // ISSUE: variable of a compiler-generated type
        DynamicObjectTypeBuilder.\u003CBuildType\u003Ec__AnonStorey3 typeCAnonStorey3 = this;
        FieldInfo fi;
        return !customFormatterLookup.TryGetValue(member, out fi) ? (Action) null : (Action) (() =>
        {
          // ISSUE: reference to a compiler-generated field
          typeCAnonStorey3.il.EmitLoadThis();
          // ISSUE: reference to a compiler-generated field
          typeCAnonStorey3.il.EmitLdfld(fi);
        });
      }), 1);
      return System.Reflection.ReflectionExtensions.CreateTypeInfo(typeBuilder);
    }

    public static object BuildFormatterToDynamicMethod(
      Type type,
      bool forceStringKey,
      bool contractless,
      bool allowPrivate)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DynamicObjectTypeBuilder.\u003CBuildFormatterToDynamicMethod\u003Ec__AnonStorey6 methodCAnonStorey6_1 = new DynamicObjectTypeBuilder.\u003CBuildFormatterToDynamicMethod\u003Ec__AnonStorey6();
      ObjectSerializationInfo orNull = ObjectSerializationInfo.CreateOrNull(type, forceStringKey, contractless, allowPrivate);
      if (orNull == null)
        return (object) null;
      DynamicMethod dynamicMethod1 = new DynamicMethod("Serialize", typeof (int), new Type[6]
      {
        typeof (byte[][]),
        typeof (object[]),
        typeof (byte[]).MakeByRefType(),
        typeof (int),
        type,
        typeof (IFormatterResolver)
      }, type, true);
      DynamicMethod dynamicMethod2 = (DynamicMethod) null;
      List<byte[]> numArrayList = new List<byte[]>();
      // ISSUE: reference to a compiler-generated field
      methodCAnonStorey6_1.serializeCustomFormatters = new List<object>();
      // ISSUE: reference to a compiler-generated field
      methodCAnonStorey6_1.deserializeCustomFormatters = new List<object>();
      if (orNull.IsStringKey)
      {
        int num = 0;
        foreach (ObjectSerializationInfo.EmittableMember emittableMember in ((IEnumerable<ObjectSerializationInfo.EmittableMember>) orNull.Members).Where<ObjectSerializationInfo.EmittableMember>((Func<ObjectSerializationInfo.EmittableMember, bool>) (x => x.IsReadable)))
        {
          numArrayList.Add(MessagePackBinary.GetEncodedStringBytes(emittableMember.StringKey));
          ++num;
        }
      }
      foreach (ObjectSerializationInfo.EmittableMember emittableMember in ((IEnumerable<ObjectSerializationInfo.EmittableMember>) orNull.Members).Where<ObjectSerializationInfo.EmittableMember>((Func<ObjectSerializationInfo.EmittableMember, bool>) (x => x.IsReadable)))
      {
        MessagePackFormatterAttribute formatterAttribtue = emittableMember.GetMessagePackFormatterAttribtue();
        if (formatterAttribtue != null)
        {
          object instance = Activator.CreateInstance(formatterAttribtue.FormatterType, formatterAttribtue.Arguments);
          // ISSUE: reference to a compiler-generated field
          methodCAnonStorey6_1.serializeCustomFormatters.Add(instance);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          methodCAnonStorey6_1.serializeCustomFormatters.Add((object) null);
        }
      }
      foreach (ObjectSerializationInfo.EmittableMember member in orNull.Members)
      {
        MessagePackFormatterAttribute formatterAttribtue = member.GetMessagePackFormatterAttribtue();
        if (formatterAttribtue != null)
        {
          object instance = Activator.CreateInstance(formatterAttribtue.FormatterType, formatterAttribtue.Arguments);
          // ISSUE: reference to a compiler-generated field
          methodCAnonStorey6_1.deserializeCustomFormatters.Add(instance);
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          methodCAnonStorey6_1.deserializeCustomFormatters.Add((object) null);
        }
      }
      ILGenerator il1 = dynamicMethod1.GetILGenerator();
      DynamicObjectTypeBuilder.BuildSerialize(type, orNull, il1, (Action) (() => il1.EmitLdarg(0)), (Func<int, ObjectSerializationInfo.EmittableMember, Action>) ((index, member) =>
      {
        // ISSUE: variable of a compiler-generated type
        DynamicObjectTypeBuilder.\u003CBuildFormatterToDynamicMethod\u003Ec__AnonStorey6 methodCAnonStorey6 = methodCAnonStorey6_1;
        // ISSUE: variable of a compiler-generated type
        DynamicObjectTypeBuilder.\u003CBuildFormatterToDynamicMethod\u003Ec__AnonStorey5 methodCAnonStorey5 = this;
        int index1 = index;
        if (serializeCustomFormatters.Count == 0)
          return (Action) null;
        return serializeCustomFormatters[index1] == null ? (Action) null : (Action) (() =>
        {
          // ISSUE: reference to a compiler-generated field
          methodCAnonStorey5.il.EmitLdarg(1);
          // ISSUE: reference to a compiler-generated field
          methodCAnonStorey5.il.EmitLdc_I4(index1);
          // ISSUE: reference to a compiler-generated field
          methodCAnonStorey5.il.Emit(OpCodes.Ldelem_Ref);
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          methodCAnonStorey5.il.Emit(OpCodes.Castclass, methodCAnonStorey6.serializeCustomFormatters[index1].GetType());
        });
      }), 2);
      if (orNull.IsStruct || (object) orNull.BestmatchConstructor != null)
      {
        dynamicMethod2 = new DynamicMethod("Deserialize", type, new Type[5]
        {
          typeof (object[]),
          typeof (byte[]),
          typeof (int),
          typeof (IFormatterResolver),
          typeof (int).MakeByRefType()
        }, type, true);
        ILGenerator il2 = dynamicMethod2.GetILGenerator();
        DynamicObjectTypeBuilder.BuildDeserialize(type, orNull, il2, (Func<int, ObjectSerializationInfo.EmittableMember, Action>) ((index, member) =>
        {
          // ISSUE: variable of a compiler-generated type
          DynamicObjectTypeBuilder.\u003CBuildFormatterToDynamicMethod\u003Ec__AnonStorey6 methodCAnonStorey6 = methodCAnonStorey6_1;
          // ISSUE: variable of a compiler-generated type
          DynamicObjectTypeBuilder.\u003CBuildFormatterToDynamicMethod\u003Ec__AnonStorey8 methodCAnonStorey8 = this;
          int index2 = index;
          if (deserializeCustomFormatters.Count == 0)
            return (Action) null;
          return deserializeCustomFormatters[index2] == null ? (Action) null : (Action) (() =>
          {
            // ISSUE: reference to a compiler-generated field
            methodCAnonStorey8.il.EmitLdarg(0);
            // ISSUE: reference to a compiler-generated field
            methodCAnonStorey8.il.EmitLdc_I4(index2);
            // ISSUE: reference to a compiler-generated field
            methodCAnonStorey8.il.Emit(OpCodes.Ldelem_Ref);
            // ISSUE: reference to a compiler-generated field
            // ISSUE: reference to a compiler-generated field
            methodCAnonStorey8.il.Emit(OpCodes.Castclass, methodCAnonStorey6.deserializeCustomFormatters[index2].GetType());
          });
        }), 1);
      }
      object obj1 = (object) dynamicMethod1.CreateDelegate(typeof (AnonymousSerializeFunc<>).MakeGenericType(type));
      Delegate @delegate;
      if (dynamicMethod2 == null)
        @delegate = (Delegate) null;
      else
        @delegate = dynamicMethod2.CreateDelegate(typeof (AnonymousDeserializeFunc<>).MakeGenericType(type));
      object obj2 = (object) @delegate;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return Activator.CreateInstance(typeof (AnonymousSerializableFormatter<>).MakeGenericType(type), (object) numArrayList.ToArray(), (object) methodCAnonStorey6_1.serializeCustomFormatters.ToArray(), (object) methodCAnonStorey6_1.deserializeCustomFormatters.ToArray(), obj1, obj2);
    }

    private static void BuildConstructor(
      Type type,
      ObjectSerializationInfo info,
      ConstructorInfo method,
      FieldBuilder stringByteKeysField,
      ILGenerator il)
    {
      il.EmitLoadThis();
      il.Emit(OpCodes.Call, DynamicObjectTypeBuilder.objectCtor);
      int num1 = ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).Count<ObjectSerializationInfo.EmittableMember>((Func<ObjectSerializationInfo.EmittableMember, bool>) (x => x.IsReadable));
      il.EmitLoadThis();
      il.EmitLdc_I4(num1);
      il.Emit(OpCodes.Newarr, typeof (byte[]));
      int num2 = 0;
      foreach (ObjectSerializationInfo.EmittableMember emittableMember in ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).Where<ObjectSerializationInfo.EmittableMember>((Func<ObjectSerializationInfo.EmittableMember, bool>) (x => x.IsReadable)))
      {
        il.Emit(OpCodes.Dup);
        il.EmitLdc_I4(num2);
        il.Emit(OpCodes.Ldstr, emittableMember.StringKey);
        il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.GetEncodedStringBytes);
        il.Emit(OpCodes.Stelem_Ref);
        ++num2;
      }
      il.Emit(OpCodes.Stfld, (FieldInfo) stringByteKeysField);
    }

    private static Dictionary<ObjectSerializationInfo.EmittableMember, FieldInfo> BuildCustomFormatterField(
      TypeBuilder builder,
      ObjectSerializationInfo info,
      ILGenerator il)
    {
      Dictionary<ObjectSerializationInfo.EmittableMember, FieldInfo> dictionary = new Dictionary<ObjectSerializationInfo.EmittableMember, FieldInfo>();
      foreach (ObjectSerializationInfo.EmittableMember key in ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).Where<ObjectSerializationInfo.EmittableMember>((Func<ObjectSerializationInfo.EmittableMember, bool>) (x => x.IsReadable || x.IsWritable)))
      {
        MessagePackFormatterAttribute formatterAttribtue = key.GetMessagePackFormatterAttribtue();
        if (formatterAttribtue != null)
        {
          FieldBuilder field = builder.DefineField(key.Name + "_formatter", formatterAttribtue.FormatterType, FieldAttributes.Private | FieldAttributes.InitOnly);
          int num = 52;
          LocalBuilder local = il.DeclareLocal(typeof (MessagePackFormatterAttribute));
          il.Emit(OpCodes.Ldtoken, info.Type);
          il.EmitCall(DynamicObjectTypeBuilder.EmitInfo.GetTypeFromHandle);
          il.Emit(OpCodes.Ldstr, key.Name);
          il.EmitLdc_I4(num);
          if (key.IsProperty)
            il.EmitCall(DynamicObjectTypeBuilder.EmitInfo.TypeGetProperty);
          else
            il.EmitCall(DynamicObjectTypeBuilder.EmitInfo.TypeGetField);
          il.EmitTrue();
          il.EmitCall(DynamicObjectTypeBuilder.EmitInfo.GetCustomAttributeMessagePackFormatterAttribute);
          il.EmitStloc(local);
          il.EmitLoadThis();
          il.EmitLdloc(local);
          il.EmitCall(DynamicObjectTypeBuilder.EmitInfo.MessagePackFormatterAttr.FormatterType);
          il.EmitLdloc(local);
          il.EmitCall(DynamicObjectTypeBuilder.EmitInfo.MessagePackFormatterAttr.Arguments);
          il.EmitCall(DynamicObjectTypeBuilder.EmitInfo.ActivatorCreateInstance);
          il.Emit(OpCodes.Castclass, formatterAttribtue.FormatterType);
          il.Emit(OpCodes.Stfld, (FieldInfo) field);
          dictionary.Add(key, (FieldInfo) field);
        }
      }
      return dictionary;
    }

    private static void BuildSerialize(
      Type type,
      ObjectSerializationInfo info,
      ILGenerator il,
      Action emitStringByteKeys,
      Func<int, ObjectSerializationInfo.EmittableMember, Action> tryEmitLoadCustomFormatter,
      int firstArgIndex)
    {
      ArgumentField argBytes = new ArgumentField(il, firstArgIndex);
      ArgumentField argOffset = new ArgumentField(il, firstArgIndex + 1);
      ArgumentField argValue = new ArgumentField(il, firstArgIndex + 2, type);
      ArgumentField argResolver = new ArgumentField(il, firstArgIndex + 3);
      if (System.Reflection.ReflectionExtensions.GetTypeInfo(type).IsClass)
      {
        Label label = il.DefineLabel();
        argValue.EmitLoad();
        il.Emit(OpCodes.Brtrue_S, label);
        argBytes.EmitLoad();
        argOffset.EmitLoad();
        il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.WriteNil);
        il.Emit(OpCodes.Ret);
        il.MarkLabel(label);
      }
      if (((IEnumerable<Type>) System.Reflection.ReflectionExtensions.GetTypeInfo(type).ImplementedInterfaces).Any<Type>((Func<Type, bool>) (x => (object) x == (object) typeof (IMessagePackSerializationCallbackReceiver))))
      {
        MethodInfo[] array = ((IEnumerable<MethodInfo>) System.Reflection.ReflectionExtensions.GetRuntimeMethods(type)).Where<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == "OnBeforeSerialize")).ToArray<MethodInfo>();
        if (array.Length == 1)
        {
          argValue.EmitLoad();
          il.Emit(OpCodes.Call, array[0]);
        }
        else
        {
          argValue.EmitLdarg();
          il.EmitBoxOrDoNothing(type);
          il.EmitCall(DynamicObjectTypeBuilder.onBeforeSerialize);
        }
      }
      LocalBuilder local = il.DeclareLocal(typeof (int));
      argOffset.EmitLoad();
      il.EmitStloc(local);
      if (info.IsIntKey)
      {
        int maxKey = ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).Where<ObjectSerializationInfo.EmittableMember>((Func<ObjectSerializationInfo.EmittableMember, bool>) (x => x.IsReadable)).Select<ObjectSerializationInfo.EmittableMember, int>((Func<ObjectSerializationInfo.EmittableMember, int>) (x => x.IntKey)).DefaultIfEmpty<int>(-1).Max();
        Dictionary<int, ObjectSerializationInfo.EmittableMember> dictionary = ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).Where<ObjectSerializationInfo.EmittableMember>((Func<ObjectSerializationInfo.EmittableMember, bool>) (x => x.IsReadable)).ToDictionary<ObjectSerializationInfo.EmittableMember, int>((Func<ObjectSerializationInfo.EmittableMember, int>) (x => x.IntKey));
        DynamicObjectTypeBuilder.EmitOffsetPlusEqual(il, (Action) null, (Action) (() =>
        {
          int num = maxKey + 1;
          il.EmitLdc_I4(num);
          if (num <= 15)
            il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.WriteFixedArrayHeaderUnsafe);
          else
            il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.WriteArrayHeader);
        }), argBytes, argOffset);
        for (int index = 0; index <= maxKey; ++index)
        {
          ObjectSerializationInfo.EmittableMember member;
          if (dictionary.TryGetValue(index, out member))
            DynamicObjectTypeBuilder.EmitSerializeValue(il, System.Reflection.ReflectionExtensions.GetTypeInfo(type), member, index, tryEmitLoadCustomFormatter, argBytes, argOffset, argValue, argResolver);
          else
            DynamicObjectTypeBuilder.EmitOffsetPlusEqual(il, (Action) null, (Action) (() => il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.WriteNil)), argBytes, argOffset);
        }
      }
      else
      {
        int writeCount = ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).Count<ObjectSerializationInfo.EmittableMember>((Func<ObjectSerializationInfo.EmittableMember, bool>) (x => x.IsReadable));
        DynamicObjectTypeBuilder.EmitOffsetPlusEqual(il, (Action) null, (Action) (() =>
        {
          il.EmitLdc_I4(writeCount);
          if (writeCount <= 15)
            il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.WriteFixedMapHeaderUnsafe);
          else
            il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.WriteMapHeader);
        }), argBytes, argOffset);
        int index = 0;
        foreach (ObjectSerializationInfo.EmittableMember member in ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).Where<ObjectSerializationInfo.EmittableMember>((Func<ObjectSerializationInfo.EmittableMember, bool>) (x => x.IsReadable)))
        {
          DynamicObjectTypeBuilder.EmitOffsetPlusEqual(il, (Action) null, (Action) (() =>
          {
            emitStringByteKeys();
            il.EmitLdc_I4(index);
            il.Emit(OpCodes.Ldelem_Ref);
            il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.WriteRaw);
          }), argBytes, argOffset);
          DynamicObjectTypeBuilder.EmitSerializeValue(il, System.Reflection.ReflectionExtensions.GetTypeInfo(type), member, index, tryEmitLoadCustomFormatter, argBytes, argOffset, argValue, argResolver);
          ++index;
        }
      }
      argOffset.EmitLoad();
      il.EmitLdloc(local);
      il.Emit(OpCodes.Sub);
      il.Emit(OpCodes.Ret);
    }

    private static void EmitOffsetPlusEqual(
      ILGenerator il,
      Action loadEmit,
      Action emit,
      ArgumentField argBytes,
      ArgumentField argOffset)
    {
      argOffset.EmitLoad();
      if (loadEmit != null)
        loadEmit();
      argBytes.EmitLoad();
      argOffset.EmitLoad();
      emit();
      il.Emit(OpCodes.Add);
      argOffset.EmitStore();
    }

    private static void EmitSerializeValue(
      ILGenerator il,
      TypeInfo type,
      ObjectSerializationInfo.EmittableMember member,
      int index,
      Func<int, ObjectSerializationInfo.EmittableMember, Action> tryEmitLoadCustomFormatter,
      ArgumentField argBytes,
      ArgumentField argOffset,
      ArgumentField argValue,
      ArgumentField argResolver)
    {
      Type t = member.Type;
      Action emitter = tryEmitLoadCustomFormatter(index, member);
      if (emitter != null)
        DynamicObjectTypeBuilder.EmitOffsetPlusEqual(il, (Action) (() => emitter()), (Action) (() =>
        {
          argValue.EmitLoad();
          member.EmitLoadValue(il);
          argResolver.EmitLoad();
          il.EmitCall(System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (IMessagePackFormatter<>).MakeGenericType(t), "Serialize", new Type[4]
          {
            DynamicObjectTypeBuilder.refByte,
            typeof (int),
            t,
            typeof (IFormatterResolver)
          }));
        }), argBytes, argOffset);
      else if (DynamicObjectTypeBuilder.IsOptimizeTargetType(t))
        DynamicObjectTypeBuilder.EmitOffsetPlusEqual(il, (Action) null, (Action) (() =>
        {
          argValue.EmitLoad();
          member.EmitLoadValue(il);
          if ((object) t == (object) typeof (byte[]))
            il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.WriteBytes);
          else
            il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.TypeInfo.GetDeclaredMethods("Write" + t.Name).OrderByDescending<MethodInfo, int>((Func<MethodInfo, int>) (x => x.GetParameters().Length)).First<MethodInfo>());
        }), argBytes, argOffset);
      else
        DynamicObjectTypeBuilder.EmitOffsetPlusEqual(il, (Action) (() =>
        {
          argResolver.EmitLoad();
          il.Emit(OpCodes.Call, DynamicObjectTypeBuilder.getFormatterWithVerify.MakeGenericMethod(t));
        }), (Action) (() =>
        {
          argValue.EmitLoad();
          member.EmitLoadValue(il);
          argResolver.EmitLoad();
          il.EmitCall(DynamicObjectTypeBuilder.getSerialize(t));
        }), argBytes, argOffset);
    }

    private static void BuildDeserialize(
      Type type,
      ObjectSerializationInfo info,
      ILGenerator il,
      Func<int, ObjectSerializationInfo.EmittableMember, Action> tryEmitLoadCustomFormatter,
      int firstArgIndex)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DynamicObjectTypeBuilder.\u003CBuildDeserialize\u003Ec__AnonStoreyF deserializeCAnonStoreyF1 = new DynamicObjectTypeBuilder.\u003CBuildDeserialize\u003Ec__AnonStoreyF()
      {
        il = il,
        tryEmitLoadCustomFormatter = tryEmitLoadCustomFormatter
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argBytes = new ArgumentField(deserializeCAnonStoreyF1.il, firstArgIndex);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argOffset = new ArgumentField(deserializeCAnonStoreyF1.il, firstArgIndex + 1);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argResolver = new ArgumentField(deserializeCAnonStoreyF1.il, firstArgIndex + 2);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argReadSize = new ArgumentField(deserializeCAnonStoreyF1.il, firstArgIndex + 3);
      // ISSUE: reference to a compiler-generated field
      Label label1 = deserializeCAnonStoreyF1.il.DefineLabel();
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argBytes.EmitLoad();
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argOffset.EmitLoad();
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.IsNil);
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.il.Emit(OpCodes.Brfalse_S, label1);
      if (System.Reflection.ReflectionExtensions.GetTypeInfo(type).IsClass)
      {
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.argReadSize.EmitLoad();
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.EmitLdc_I4(1);
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Stind_I4);
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Ldnull);
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Ret);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Ldstr, "typecode is null, struct not supported");
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Newobj, DynamicObjectTypeBuilder.invalidOperationExceptionConstructor);
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Throw);
      }
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.il.MarkLabel(label1);
      // ISSUE: reference to a compiler-generated field
      LocalBuilder local1 = deserializeCAnonStoreyF1.il.DeclareLocal(typeof (int));
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argOffset.EmitLoad();
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.il.EmitStloc(local1);
      // ISSUE: reference to a compiler-generated field
      LocalBuilder localBuilder = deserializeCAnonStoreyF1.il.DeclareLocal(typeof (int));
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argBytes.EmitLoad();
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argOffset.EmitLoad();
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argReadSize.EmitLoad();
      if (info.IsIntKey)
      {
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.ReadArrayHeader);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.ReadMapHeader);
      }
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.il.EmitStloc(localBuilder);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      DynamicObjectTypeBuilder.EmitOffsetPlusReadSize(deserializeCAnonStoreyF1.il, deserializeCAnonStoreyF1.argOffset, deserializeCAnonStoreyF1.argReadSize);
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.gotoDefault = new Label?();
      if (info.IsIntKey)
      {
        int count = ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).Select<ObjectSerializationInfo.EmittableMember, int>((Func<ObjectSerializationInfo.EmittableMember, int>) (x => x.IntKey)).DefaultIfEmpty<int>(-1).Max() + 1;
        Dictionary<int, ObjectSerializationInfo.EmittableMember> intKeyMap = ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).ToDictionary<ObjectSerializationInfo.EmittableMember, int>((Func<ObjectSerializationInfo.EmittableMember, int>) (x => x.IntKey));
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.infoList = Enumerable.Range(0, count).Select<int, DynamicObjectTypeBuilder.DeserializeInfo>((Func<int, DynamicObjectTypeBuilder.DeserializeInfo>) (x =>
        {
          ObjectSerializationInfo.EmittableMember emittableMember;
          if (intKeyMap.TryGetValue(x, out emittableMember))
            return new DynamicObjectTypeBuilder.DeserializeInfo()
            {
              MemberInfo = emittableMember,
              LocalField = il.DeclareLocal(emittableMember.Type),
              SwitchLabel = il.DefineLabel()
            };
          if (!gotoDefault.HasValue)
            gotoDefault = new Label?(il.DefineLabel());
          return new DynamicObjectTypeBuilder.DeserializeInfo()
          {
            MemberInfo = (ObjectSerializationInfo.EmittableMember) null,
            LocalField = (LocalBuilder) null,
            SwitchLabel = gotoDefault.Value
          };
        })).ToArray<DynamicObjectTypeBuilder.DeserializeInfo>();
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated method
        deserializeCAnonStoreyF1.infoList = ((IEnumerable<ObjectSerializationInfo.EmittableMember>) info.Members).Select<ObjectSerializationInfo.EmittableMember, DynamicObjectTypeBuilder.DeserializeInfo>(new Func<ObjectSerializationInfo.EmittableMember, DynamicObjectTypeBuilder.DeserializeInfo>(deserializeCAnonStoreyF1.\u003C\u003Em__0)).ToArray<DynamicObjectTypeBuilder.DeserializeInfo>();
      }
      if (info.IsStringKey)
      {
        AutomataDictionary automata = new AutomataDictionary();
        for (int index = 0; index < info.Members.Length; ++index)
          automata.Add(info.Members[index].StringKey, index);
        // ISSUE: reference to a compiler-generated field
        LocalBuilder buffer = deserializeCAnonStoreyF1.il.DeclareLocal(typeof (byte).MakeByRefType(), true);
        // ISSUE: reference to a compiler-generated field
        LocalBuilder keyArraySegment = deserializeCAnonStoreyF1.il.DeclareLocal(typeof (ArraySegment<byte>));
        // ISSUE: reference to a compiler-generated field
        LocalBuilder longKey = deserializeCAnonStoreyF1.il.DeclareLocal(typeof (ulong));
        // ISSUE: reference to a compiler-generated field
        LocalBuilder p = deserializeCAnonStoreyF1.il.DeclareLocal(typeof (byte*));
        // ISSUE: reference to a compiler-generated field
        LocalBuilder rest = deserializeCAnonStoreyF1.il.DeclareLocal(typeof (int));
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.argBytes.EmitLoad();
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.EmitLdc_I4(0);
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Ldelema, typeof (byte));
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.EmitStloc(buffer);
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.EmitIncrementFor(localBuilder, (Action<LocalBuilder>) (forILocal =>
        {
          // ISSUE: variable of a compiler-generated type
          DynamicObjectTypeBuilder.\u003CBuildDeserialize\u003Ec__AnonStoreyF deserializeCAnonStoreyF = deserializeCAnonStoreyF1;
          // ISSUE: variable of a compiler-generated type
          DynamicObjectTypeBuilder.\u003CBuildDeserialize\u003Ec__AnonStorey10 deserializeCAnonStorey10 = this;
          Label readNext = il.DefineLabel();
          Label loopEnd = il.DefineLabel();
          argBytes.EmitLoad();
          argOffset.EmitLoad();
          argReadSize.EmitLoad();
          il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.ReadStringSegment);
          il.EmitStloc(keyArraySegment);
          DynamicObjectTypeBuilder.EmitOffsetPlusReadSize(il, argOffset, argReadSize);
          il.EmitLdloc(buffer);
          il.Emit(OpCodes.Conv_I);
          il.EmitLdloca(keyArraySegment);
          il.EmitCall(System.Reflection.ReflectionExtensions.GetRuntimeProperty(typeof (ArraySegment<byte>), "Offset").GetGetMethod());
          il.Emit(OpCodes.Add);
          il.EmitStloc(p);
          il.EmitLdloca(keyArraySegment);
          il.EmitCall(System.Reflection.ReflectionExtensions.GetRuntimeProperty(typeof (ArraySegment<byte>), "Count").GetGetMethod());
          il.EmitStloc(rest);
          il.EmitLdloc(rest);
          il.Emit(OpCodes.Brfalse, readNext);
          // ISSUE: reference to a compiler-generated field
          automata.EmitMatch(il, p, rest, longKey, (Action<KeyValuePair<string, int>>) (x =>
          {
            int index = x.Value;
            // ISSUE: reference to a compiler-generated field
            if (deserializeCAnonStoreyF.infoList[index].MemberInfo != null)
            {
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              // ISSUE: reference to a compiler-generated field
              DynamicObjectTypeBuilder.EmitDeserializeValue(deserializeCAnonStoreyF.il, deserializeCAnonStoreyF.infoList[index], index, deserializeCAnonStoreyF.tryEmitLoadCustomFormatter, deserializeCAnonStoreyF.argBytes, deserializeCAnonStoreyF.argOffset, deserializeCAnonStoreyF.argResolver, deserializeCAnonStoreyF.argReadSize);
              // ISSUE: reference to a compiler-generated field
              deserializeCAnonStoreyF.il.Emit(OpCodes.Br, loopEnd);
            }
            else
            {
              // ISSUE: reference to a compiler-generated field
              deserializeCAnonStoreyF.il.Emit(OpCodes.Br, readNext);
            }
          }), (Action) (() => deserializeCAnonStoreyF.il.Emit(OpCodes.Br, readNext)));
          il.MarkLabel(readNext);
          argReadSize.EmitLoad();
          argBytes.EmitLoad();
          argOffset.EmitLoad();
          il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.ReadNextBlock);
          il.Emit(OpCodes.Stind_I4);
          il.MarkLabel(loopEnd);
          DynamicObjectTypeBuilder.EmitOffsetPlusReadSize(il, argOffset, argReadSize);
        }));
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Ldc_I4_0);
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Conv_U);
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.EmitStloc(buffer);
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        LocalBuilder key = deserializeCAnonStoreyF1.il.DeclareLocal(typeof (int));
        // ISSUE: reference to a compiler-generated field
        Label switchDefault = deserializeCAnonStoreyF1.il.DefineLabel();
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.EmitIncrementFor(localBuilder, (Action<LocalBuilder>) (forILocal =>
        {
          Label label2 = il.DefineLabel();
          il.EmitLdloc(forILocal);
          il.EmitStloc(key);
          il.EmitLdloc(key);
          il.Emit(OpCodes.Switch, ((IEnumerable<DynamicObjectTypeBuilder.DeserializeInfo>) infoList).Select<DynamicObjectTypeBuilder.DeserializeInfo, Label>((Func<DynamicObjectTypeBuilder.DeserializeInfo, Label>) (x => x.SwitchLabel)).ToArray<Label>());
          il.MarkLabel(switchDefault);
          argReadSize.EmitLoad();
          argBytes.EmitLoad();
          argOffset.EmitLoad();
          il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.ReadNextBlock);
          il.Emit(OpCodes.Stind_I4);
          il.Emit(OpCodes.Br, label2);
          if (gotoDefault.HasValue)
          {
            il.MarkLabel(gotoDefault.Value);
            il.Emit(OpCodes.Br, switchDefault);
          }
          int num = 0;
          foreach (DynamicObjectTypeBuilder.DeserializeInfo info1 in infoList)
          {
            if (info1.MemberInfo != null)
            {
              il.MarkLabel(info1.SwitchLabel);
              DynamicObjectTypeBuilder.EmitDeserializeValue(il, info1, num++, tryEmitLoadCustomFormatter, argBytes, argOffset, argResolver, argReadSize);
              il.Emit(OpCodes.Br, label2);
            }
          }
          il.MarkLabel(label2);
          DynamicObjectTypeBuilder.EmitOffsetPlusReadSize(il, argOffset, argReadSize);
        }));
      }
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argReadSize.EmitLoad();
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.argOffset.EmitLoad();
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.il.EmitLdloc(local1);
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.il.Emit(OpCodes.Sub);
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.il.Emit(OpCodes.Stind_I4);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      LocalBuilder local2 = DynamicObjectTypeBuilder.EmitNewObject(deserializeCAnonStoreyF1.il, type, info, deserializeCAnonStoreyF1.infoList);
      if (((IEnumerable<Type>) System.Reflection.ReflectionExtensions.GetTypeInfo(type).ImplementedInterfaces).Any<Type>((Func<Type, bool>) (x => (object) x == (object) typeof (IMessagePackSerializationCallbackReceiver))))
      {
        MethodInfo[] array = ((IEnumerable<MethodInfo>) System.Reflection.ReflectionExtensions.GetRuntimeMethods(type)).Where<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == "OnAfterDeserialize")).ToArray<MethodInfo>();
        if (array.Length == 1)
        {
          if (info.IsClass)
          {
            // ISSUE: reference to a compiler-generated field
            deserializeCAnonStoreyF1.il.Emit(OpCodes.Dup);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            deserializeCAnonStoreyF1.il.EmitLdloca(local2);
          }
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStoreyF1.il.Emit(OpCodes.Call, array[0]);
        }
        else
        {
          if (info.IsStruct)
          {
            // ISSUE: reference to a compiler-generated field
            deserializeCAnonStoreyF1.il.EmitLdloc(local2);
            // ISSUE: reference to a compiler-generated field
            deserializeCAnonStoreyF1.il.Emit(OpCodes.Box, type);
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            deserializeCAnonStoreyF1.il.Emit(OpCodes.Dup);
          }
          // ISSUE: reference to a compiler-generated field
          deserializeCAnonStoreyF1.il.EmitCall(DynamicObjectTypeBuilder.onAfterDeserialize);
        }
      }
      if (info.IsStruct)
      {
        // ISSUE: reference to a compiler-generated field
        deserializeCAnonStoreyF1.il.Emit(OpCodes.Ldloc, local2);
      }
      // ISSUE: reference to a compiler-generated field
      deserializeCAnonStoreyF1.il.Emit(OpCodes.Ret);
    }

    private static void EmitOffsetPlusReadSize(
      ILGenerator il,
      ArgumentField argOffset,
      ArgumentField argReadSize)
    {
      argOffset.EmitLoad();
      argReadSize.EmitLoad();
      il.Emit(OpCodes.Ldind_I4);
      il.Emit(OpCodes.Add);
      argOffset.EmitStore();
    }

    private static void EmitDeserializeValue(
      ILGenerator il,
      DynamicObjectTypeBuilder.DeserializeInfo info,
      int index,
      Func<int, ObjectSerializationInfo.EmittableMember, Action> tryEmitLoadCustomFormatter,
      ArgumentField argBytes,
      ArgumentField argOffset,
      ArgumentField argResolver,
      ArgumentField argReadSize)
    {
      ObjectSerializationInfo.EmittableMember memberInfo = info.MemberInfo;
      Type type = memberInfo.Type;
      Action action = tryEmitLoadCustomFormatter(index, memberInfo);
      if (action != null)
      {
        action();
        argBytes.EmitLoad();
        argOffset.EmitLoad();
        argResolver.EmitLoad();
        argReadSize.EmitLoad();
        il.EmitCall(System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (IMessagePackFormatter<>).MakeGenericType(type), "Deserialize", new Type[4]
        {
          typeof (byte[]),
          typeof (int),
          typeof (IFormatterResolver),
          DynamicObjectTypeBuilder.refInt
        }));
      }
      else if (DynamicObjectTypeBuilder.IsOptimizeTargetType(type))
      {
        il.EmitLdarg(1);
        il.EmitLdarg(2);
        il.EmitLdarg(4);
        if ((object) type == (object) typeof (byte[]))
          il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.ReadBytes);
        else
          il.EmitCall(DynamicObjectTypeBuilder.MessagePackBinaryTypeInfo.TypeInfo.GetDeclaredMethods("Read" + type.Name).OrderByDescending<MethodInfo, int>((Func<MethodInfo, int>) (x => x.GetParameters().Length)).First<MethodInfo>());
      }
      else
      {
        argResolver.EmitLoad();
        il.EmitCall(DynamicObjectTypeBuilder.getFormatterWithVerify.MakeGenericMethod(type));
        argBytes.EmitLoad();
        argOffset.EmitLoad();
        argResolver.EmitLoad();
        argReadSize.EmitLoad();
        il.EmitCall(DynamicObjectTypeBuilder.getDeserialize(type));
      }
      il.EmitStloc(info.LocalField);
    }

    private static LocalBuilder EmitNewObject(
      ILGenerator il,
      Type type,
      ObjectSerializationInfo info,
      DynamicObjectTypeBuilder.DeserializeInfo[] members)
    {
      if (info.IsClass)
      {
        foreach (ObjectSerializationInfo.EmittableMember constructorParameter in info.ConstructorParameters)
        {
          ObjectSerializationInfo.EmittableMember item = constructorParameter;
          DynamicObjectTypeBuilder.DeserializeInfo deserializeInfo = ((IEnumerable<DynamicObjectTypeBuilder.DeserializeInfo>) members).First<DynamicObjectTypeBuilder.DeserializeInfo>((Func<DynamicObjectTypeBuilder.DeserializeInfo, bool>) (x => x.MemberInfo == item));
          il.EmitLdloc(deserializeInfo.LocalField);
        }
        il.Emit(OpCodes.Newobj, info.BestmatchConstructor);
        foreach (DynamicObjectTypeBuilder.DeserializeInfo deserializeInfo in ((IEnumerable<DynamicObjectTypeBuilder.DeserializeInfo>) members).Where<DynamicObjectTypeBuilder.DeserializeInfo>((Func<DynamicObjectTypeBuilder.DeserializeInfo, bool>) (x => x.MemberInfo != null && x.MemberInfo.IsWritable)))
        {
          il.Emit(OpCodes.Dup);
          il.EmitLdloc(deserializeInfo.LocalField);
          deserializeInfo.MemberInfo.EmitStoreValue(il);
        }
        return (LocalBuilder) null;
      }
      LocalBuilder local = il.DeclareLocal(type);
      if ((object) info.BestmatchConstructor == null)
      {
        il.Emit(OpCodes.Ldloca, local);
        il.Emit(OpCodes.Initobj, type);
      }
      else
      {
        foreach (ObjectSerializationInfo.EmittableMember constructorParameter in info.ConstructorParameters)
        {
          ObjectSerializationInfo.EmittableMember item = constructorParameter;
          DynamicObjectTypeBuilder.DeserializeInfo deserializeInfo = ((IEnumerable<DynamicObjectTypeBuilder.DeserializeInfo>) members).First<DynamicObjectTypeBuilder.DeserializeInfo>((Func<DynamicObjectTypeBuilder.DeserializeInfo, bool>) (x => x.MemberInfo == item));
          il.EmitLdloc(deserializeInfo.LocalField);
        }
        il.Emit(OpCodes.Newobj, info.BestmatchConstructor);
        il.Emit(OpCodes.Stloc, local);
      }
      foreach (DynamicObjectTypeBuilder.DeserializeInfo deserializeInfo in ((IEnumerable<DynamicObjectTypeBuilder.DeserializeInfo>) members).Where<DynamicObjectTypeBuilder.DeserializeInfo>((Func<DynamicObjectTypeBuilder.DeserializeInfo, bool>) (x => x.MemberInfo != null && x.MemberInfo.IsWritable)))
      {
        il.EmitLdloca(local);
        il.EmitLdloc(deserializeInfo.LocalField);
        deserializeInfo.MemberInfo.EmitStoreValue(il);
      }
      return local;
    }

    private static bool IsOptimizeTargetType(Type type)
    {
      return (object) type == (object) typeof (short) || (object) type == (object) typeof (int) || (object) type == (object) typeof (long) || (object) type == (object) typeof (ushort) || (object) type == (object) typeof (uint) || (object) type == (object) typeof (ulong) || (object) type == (object) typeof (float) || (object) type == (object) typeof (double) || (object) type == (object) typeof (bool) || (object) type == (object) typeof (byte) || (object) type == (object) typeof (sbyte) || (object) type == (object) typeof (char);
    }

    internal static class MessagePackBinaryTypeInfo
    {
      public static TypeInfo TypeInfo = System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (MessagePackBinary));
      public static readonly MethodInfo GetEncodedStringBytes = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (GetEncodedStringBytes), new Type[1]
      {
        typeof (string)
      });
      public static MethodInfo WriteFixedMapHeaderUnsafe = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteFixedMapHeaderUnsafe), new Type[3]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteFixedArrayHeaderUnsafe = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteFixedArrayHeaderUnsafe), new Type[3]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteMapHeader = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteMapHeader), new Type[3]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteArrayHeader = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteArrayHeader), new Type[3]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WritePositiveFixedIntUnsafe = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WritePositiveFixedIntUnsafe), new Type[3]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteInt32 = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteInt32), new Type[3]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (int)
      });
      public static MethodInfo WriteBytes = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteBytes), new Type[3]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (byte[])
      });
      public static MethodInfo WriteNil = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteNil), new Type[2]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int)
      });
      public static MethodInfo ReadBytes = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadBytes), new Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicObjectTypeBuilder.refInt
      });
      public static MethodInfo ReadInt32 = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadInt32), new Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicObjectTypeBuilder.refInt
      });
      public static MethodInfo ReadString = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadString), new Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicObjectTypeBuilder.refInt
      });
      public static MethodInfo ReadStringSegment = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadStringSegment), new Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicObjectTypeBuilder.refInt
      });
      public static MethodInfo IsNil = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (IsNil), new Type[2]
      {
        typeof (byte[]),
        typeof (int)
      });
      public static MethodInfo ReadNextBlock = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadNextBlock), new Type[2]
      {
        typeof (byte[]),
        typeof (int)
      });
      public static MethodInfo WriteStringUnsafe = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteStringUnsafe), new Type[4]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (string),
        typeof (int)
      });
      public static MethodInfo WriteStringBytes = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteStringBytes), new Type[3]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (byte[])
      });
      public static MethodInfo WriteRaw = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (WriteRaw), new Type[3]
      {
        DynamicObjectTypeBuilder.refByte,
        typeof (int),
        typeof (byte[])
      });
      public static MethodInfo ReadArrayHeader = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadArrayHeader), new Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicObjectTypeBuilder.refInt
      });
      public static MethodInfo ReadMapHeader = System.Reflection.ReflectionExtensions.GetRuntimeMethod(typeof (MessagePackBinary), nameof (ReadMapHeader), new Type[3]
      {
        typeof (byte[]),
        typeof (int),
        DynamicObjectTypeBuilder.refInt
      });
    }

    internal static class EmitInfo
    {
      public static readonly MethodInfo GetTypeFromHandle;
      public static readonly MethodInfo TypeGetProperty;
      public static readonly MethodInfo TypeGetField;
      public static readonly MethodInfo GetCustomAttributeMessagePackFormatterAttribute;
      public static readonly MethodInfo ActivatorCreateInstance;

      static EmitInfo()
      {
        ParameterExpression parameterExpression1 = Expression.Parameter(typeof (Type), "t");
        ParameterExpression parameterExpression2 = Expression.Parameter(typeof (Type), "t");
        DynamicObjectTypeBuilder.EmitInfo.GetTypeFromHandle = ExpressionUtility.GetMethodInfo<Type>((Expression<Func<Type>>) (() => Type.GetTypeFromHandle(new RuntimeTypeHandle())));
        DynamicObjectTypeBuilder.EmitInfo.TypeGetProperty = ExpressionUtility.GetMethodInfo<Type, PropertyInfo>((Expression<Func<Type, PropertyInfo>>) (parameterExpression => parameterExpression.GetTypeInfo().GetProperty(default (string), BindingFlags.Default)));
        DynamicObjectTypeBuilder.EmitInfo.TypeGetField = ExpressionUtility.GetMethodInfo<Type, FieldInfo>((Expression<Func<Type, FieldInfo>>) (parameterExpression => parameterExpression.GetTypeInfo().GetField(default (string), BindingFlags.Default)));
        DynamicObjectTypeBuilder.EmitInfo.GetCustomAttributeMessagePackFormatterAttribute = ExpressionUtility.GetMethodInfo<MessagePackFormatterAttribute>((Expression<Func<MessagePackFormatterAttribute>>) (() => CustomAttributeExtensions.GetCustomAttribute<MessagePackFormatterAttribute>(default (MemberInfo), false)));
        DynamicObjectTypeBuilder.EmitInfo.ActivatorCreateInstance = ExpressionUtility.GetMethodInfo<object>((Expression<Func<object>>) (() => Activator.CreateInstance(default (Type), default (object[]))));
      }

      internal static class MessagePackFormatterAttr
      {
        internal static readonly MethodInfo FormatterType;
        internal static readonly MethodInfo Arguments;

        static MessagePackFormatterAttr()
        {
          ParameterExpression parameterExpression1 = Expression.Parameter(typeof (MessagePackFormatterAttribute), "attr");
          ParameterExpression parameterExpression2 = Expression.Parameter(typeof (MessagePackFormatterAttribute), "attr");
          DynamicObjectTypeBuilder.EmitInfo.MessagePackFormatterAttr.FormatterType = ExpressionUtility.GetPropertyInfo<MessagePackFormatterAttribute, Type>((Expression<Func<MessagePackFormatterAttribute, Type>>) (parameterExpression => parameterExpression.FormatterType)).GetGetMethod();
          DynamicObjectTypeBuilder.EmitInfo.MessagePackFormatterAttr.Arguments = ExpressionUtility.GetPropertyInfo<MessagePackFormatterAttribute, object[]>((Expression<Func<MessagePackFormatterAttribute, object[]>>) (parameterExpression => parameterExpression.Arguments)).GetGetMethod();
        }
      }
    }

    private class DeserializeInfo
    {
      public ObjectSerializationInfo.EmittableMember MemberInfo { get; set; }

      public LocalBuilder LocalField { get; set; }

      public Label SwitchLabel { get; set; }
    }
  }
}
