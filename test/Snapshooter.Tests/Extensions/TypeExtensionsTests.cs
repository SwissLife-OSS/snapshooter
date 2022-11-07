using System;
using System.Collections.Generic;
using Xunit;
using Snapshooter.Extensions;

#nullable enable

namespace Snapshooter.Tests.Extensions
{
    public class TypeExtensionsTests
    {
        [Fact]
        public void GetAliasName_DifferentScalarTypes()
        {
            Assert.Equal("Boolean", typeof(bool).GetAliasName());
            Assert.Equal("Byte", typeof(byte).GetAliasName());
            Assert.Equal("SByte", typeof(sbyte).GetAliasName());
            Assert.Equal("Char", typeof(char).GetAliasName());
            Assert.Equal("Decimal", typeof(decimal).GetAliasName());
            Assert.Equal("Double", typeof(double).GetAliasName());
            Assert.Equal("Float", typeof(float).GetAliasName());
            Assert.Equal("UInteger", typeof(uint).GetAliasName());
            Assert.Equal("Long", typeof(long).GetAliasName());
            Assert.Equal("ULong", typeof(ulong).GetAliasName());
            Assert.Equal("Short", typeof(short).GetAliasName());
            Assert.Equal("UShort", typeof(ushort).GetAliasName());
            Assert.Equal("Object", typeof(object).GetAliasName());
            Assert.Equal("String", typeof(string).GetAliasName());
        }

        [Fact]
        public void GetAliasName_DifferentNullableScalarTypes()
        {
            Assert.Equal("Boolean?", typeof(bool?).GetAliasName());
            Assert.Equal("Byte?", typeof(byte?).GetAliasName());
            Assert.Equal("SByte?", typeof(sbyte?).GetAliasName());
            Assert.Equal("Char?", typeof(char?).GetAliasName());
            Assert.Equal("Decimal?", typeof(decimal?).GetAliasName());
            Assert.Equal("Double?", typeof(double?).GetAliasName());
            Assert.Equal("Float?", typeof(float?).GetAliasName());
            Assert.Equal("UInteger?", typeof(uint?).GetAliasName());
            Assert.Equal("Long?", typeof(long?).GetAliasName());
            Assert.Equal("ULong?", typeof(ulong?).GetAliasName());
            Assert.Equal("Short?", typeof(short?).GetAliasName());
            Assert.Equal("UShort?", typeof(ushort?).GetAliasName());
        }

        [Fact]
        public void GetAliasName_DifferentListTypes()
        {
            Assert.Equal("Integer[]",
                typeof(int[]).GetAliasName());
            Assert.Equal("Integer[][]",
                typeof(int[][]).GetAliasName());
            Assert.Equal("List<Object>",
                typeof(List<object>).GetAliasName());
            Assert.Equal("List<Object>",
                typeof(List<dynamic>).GetAliasName());
            Assert.Equal("List<Integer>",
                typeof(List<int>).GetAliasName());
            Assert.Equal("List<Byte[]>",
                typeof(List<byte[]>).GetAliasName());
            Assert.Equal("IEnumerable<Decimal>",
                typeof(IEnumerable<decimal>).GetAliasName());
            Assert.Equal("KeyValuePair<Integer, String>",
                typeof(KeyValuePair<int, string>).GetAliasName());
            Assert.Equal("Tuple<Integer, String>",
                typeof(Tuple<int, string>).GetAliasName());
            Assert.Equal("Tuple<KeyValuePair<Object, Long>, String>",
                typeof(Tuple<KeyValuePair<object, long>, string>).GetAliasName());
            Assert.Equal("List<Tuple<Integer, String>>",
                typeof(List<Tuple<int, string>>).GetAliasName());
            Assert.Equal("Tuple<Short[], String>",
                typeof(Tuple<short[], string>).GetAliasName());
        }

        [Fact]
        public void GetAliasName_ComplexTypes()
        {
            Assert.Equal("User", typeof(User).GetAliasName());
            Assert.Equal("GenericUser<User>", typeof(GenericUser<User>).GetAliasName());
            Assert.Equal("GenericUser<User>", typeof(GenericUser<User?>).GetAliasName());
        }

        private class User
        {
        }

        private class GenericUser<User>
        {
        }
    }
}
