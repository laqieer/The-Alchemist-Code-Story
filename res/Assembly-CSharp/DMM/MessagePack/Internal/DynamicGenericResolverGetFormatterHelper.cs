// Decompiled with JetBrains decompiler
// Type: MessagePack.Internal.DynamicGenericResolverGetFormatterHelper
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: BE2A90B7-A8AB-4E1F-A9DE-BBA047493101
// Assembly location: C:\r\The-Alchemist-Code-Story\res\Assembly-CSharp_japan_dmm.dll

using MessagePack.Formatters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

#nullable disable
namespace MessagePack.Internal
{
  internal static class DynamicGenericResolverGetFormatterHelper
  {
    private static readonly Dictionary<Type, Type> formatterMap = new Dictionary<Type, Type>()
    {
      {
        typeof (List<>),
        typeof (ListFormatter<>)
      },
      {
        typeof (LinkedList<>),
        typeof (LinkedListFormatter<>)
      },
      {
        typeof (Queue<>),
        typeof (QeueueFormatter<>)
      },
      {
        typeof (Stack<>),
        typeof (StackFormatter<>)
      },
      {
        typeof (HashSet<>),
        typeof (HashSetFormatter<>)
      },
      {
        typeof (ReadOnlyCollection<>),
        typeof (ReadOnlyCollectionFormatter<>)
      },
      {
        typeof (IList<>),
        typeof (InterfaceListFormatter<>)
      },
      {
        typeof (ICollection<>),
        typeof (InterfaceCollectionFormatter<>)
      },
      {
        typeof (IEnumerable<>),
        typeof (InterfaceEnumerableFormatter<>)
      },
      {
        typeof (Dictionary<,>),
        typeof (DictionaryFormatter<,>)
      },
      {
        typeof (IDictionary<,>),
        typeof (InterfaceDictionaryFormatter<,>)
      },
      {
        typeof (SortedDictionary<,>),
        typeof (SortedDictionaryFormatter<,>)
      },
      {
        typeof (SortedList<,>),
        typeof (SortedListFormatter<,>)
      },
      {
        typeof (ILookup<,>),
        typeof (InterfaceLookupFormatter<,>)
      },
      {
        typeof (IGrouping<,>),
        typeof (InterfaceGroupingFormatter<,>)
      }
    };

    internal static object GetFormatter(Type t)
    {
      TypeInfo typeInfo = System.Reflection.ReflectionExtensions.GetTypeInfo(t);
      if (t.IsArray)
      {
        switch (t.GetArrayRank())
        {
          case 1:
            if ((object) t.GetElementType() == (object) typeof (byte))
              return (object) ByteArrayFormatter.Instance;
            return Activator.CreateInstance(typeof (ArrayFormatter<>).MakeGenericType(t.GetElementType()));
          case 2:
            return Activator.CreateInstance(typeof (TwoDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
          case 3:
            return Activator.CreateInstance(typeof (ThreeDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
          case 4:
            return Activator.CreateInstance(typeof (FourDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
          default:
            return (object) null;
        }
      }
      else
      {
        if (typeInfo.IsGenericType)
        {
          Type genericTypeDefinition = typeInfo.GetGenericTypeDefinition();
          bool flag = System.Reflection.ReflectionExtensions.GetTypeInfo(genericTypeDefinition).IsNullable();
          Type genericTypeArgument = !flag ? (Type) null : typeInfo.GenericTypeArguments[0];
          if ((object) genericTypeDefinition == (object) typeof (KeyValuePair<,>))
            return DynamicGenericResolverGetFormatterHelper.CreateInstance(typeof (KeyValuePairFormatter<,>), typeInfo.GenericTypeArguments);
          if (flag && System.Reflection.ReflectionExtensions.GetTypeInfo(genericTypeArgument).IsConstructedGenericType() && (object) genericTypeArgument.GetGenericTypeDefinition() == (object) typeof (KeyValuePair<,>))
            return DynamicGenericResolverGetFormatterHelper.CreateInstance(typeof (NullableFormatter<>), new Type[1]
            {
              genericTypeArgument
            });
          if ((object) genericTypeDefinition == (object) typeof (ArraySegment<>))
            return (object) typeInfo.GenericTypeArguments[0] == (object) typeof (byte) ? (object) ByteArraySegmentFormatter.Instance : DynamicGenericResolverGetFormatterHelper.CreateInstance(typeof (ArraySegmentFormatter<>), typeInfo.GenericTypeArguments);
          if (flag && System.Reflection.ReflectionExtensions.GetTypeInfo(genericTypeArgument).IsConstructedGenericType() && (object) genericTypeArgument.GetGenericTypeDefinition() == (object) typeof (ArraySegment<>))
          {
            if ((object) genericTypeArgument == (object) typeof (ArraySegment<byte>))
              return (object) new StaticNullableFormatter<ArraySegment<byte>>((IMessagePackFormatter<ArraySegment<byte>>) ByteArraySegmentFormatter.Instance);
            return DynamicGenericResolverGetFormatterHelper.CreateInstance(typeof (NullableFormatter<>), new Type[1]
            {
              genericTypeArgument
            });
          }
          Type genericType;
          if (DynamicGenericResolverGetFormatterHelper.formatterMap.TryGetValue(genericTypeDefinition, out genericType))
            return DynamicGenericResolverGetFormatterHelper.CreateInstance(genericType, typeInfo.GenericTypeArguments);
          if (typeInfo.GenericTypeArguments.Length == 1 && ((IEnumerable<Type>) typeInfo.ImplementedInterfaces).Any<Type>((Func<Type, bool>) (x => System.Reflection.ReflectionExtensions.GetTypeInfo(x).IsConstructedGenericType() && (object) x.GetGenericTypeDefinition() == (object) typeof (ICollection<>))) && typeInfo.DeclaredConstructors.Any<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.GetParameters().Length == 0)))
            return DynamicGenericResolverGetFormatterHelper.CreateInstance(typeof (GenericCollectionFormatter<,>), new Type[2]
            {
              typeInfo.GenericTypeArguments[0],
              t
            });
          if (typeInfo.GenericTypeArguments.Length == 2 && ((IEnumerable<Type>) typeInfo.ImplementedInterfaces).Any<Type>((Func<Type, bool>) (x => System.Reflection.ReflectionExtensions.GetTypeInfo(x).IsConstructedGenericType() && (object) x.GetGenericTypeDefinition() == (object) typeof (IDictionary<,>))) && typeInfo.DeclaredConstructors.Any<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.GetParameters().Length == 0)))
            return DynamicGenericResolverGetFormatterHelper.CreateInstance(typeof (GenericDictionaryFormatter<,,>), new Type[3]
            {
              typeInfo.GenericTypeArguments[0],
              typeInfo.GenericTypeArguments[1],
              t
            });
        }
        else
        {
          if ((object) t == (object) typeof (IList))
            return (object) NonGenericInterfaceListFormatter.Instance;
          if ((object) t == (object) typeof (IDictionary))
            return (object) NonGenericInterfaceDictionaryFormatter.Instance;
          if (System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (IList)).IsAssignableFrom(typeInfo) && typeInfo.DeclaredConstructors.Any<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.GetParameters().Length == 0)))
            return Activator.CreateInstance(typeof (NonGenericListFormatter<>).MakeGenericType(t));
          if (System.Reflection.ReflectionExtensions.GetTypeInfo(typeof (IDictionary)).IsAssignableFrom(typeInfo) && typeInfo.DeclaredConstructors.Any<ConstructorInfo>((Func<ConstructorInfo, bool>) (x => x.GetParameters().Length == 0)))
            return Activator.CreateInstance(typeof (NonGenericDictionaryFormatter<>).MakeGenericType(t));
        }
        return (object) null;
      }
    }

    private static object CreateInstance(
      Type genericType,
      Type[] genericTypeArguments,
      params object[] arguments)
    {
      return Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
    }
  }
}
