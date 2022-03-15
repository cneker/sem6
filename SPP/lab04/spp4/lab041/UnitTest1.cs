using System;
using Xunit;
using spp4;

namespace lab041
{
    public class UnitTest1
    {
        //[Fact]
        //public void TestSmallInt()
        //{
        //    //arrange
        //    int a = 1;
        //    int b = 2;
        //    int c = 3;
        //    int d = 4;
        //    int expected = 10;
        //    //act
        //    int actual = Sum.Accum(a, b, c, d);
        //    //assert
        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void TestBigInt1()
        //{
        //    int a = 2_000_000_000;
        //    int b = 1_000_000_000;
        //    uint expected = 3_000_000_000;

        //    int actual = Sum.Accum(a, b);

        //    Assert.Equal(expected, (uint)actual);
        //}

        [Fact]
        public void TestBigInt2()
        {
            int a = 2_000_000_000;
            int b = 1_000_000_000;
            long expected = 3_000_000_000;

            long actual = Sum.Accum(a, b);

            Assert.Equal(expected, actual);
        }
    }

    public class UnitTest2
    {
        [Fact]
        public void TestLooseNullAndNull()
        {
            string a = null;
            string b = null;

            Assert.Throws<NullReferenceException>(() => StringUtils.Loose(a, b));
        }

        [Fact]
        public void TestLoose_NullAndAsterisk()
        {
            string a = null;
            string b = "test";
            string expected = a;

            string result = StringUtils.Loose(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLoose_EmptyStringAndAsterisk()
        {
            string a = string.Empty;
            string b = "test";
            string expected = a;

            string result = StringUtils.Loose(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLoose_AsteriskAndNull()
        {
            string a = "test";
            string b = null;
            string expected = a;

            string result = StringUtils.Loose(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLoose_AsteriskAndEmptyString()
        {
            string a = "test";
            string b = string.Empty;
            string expected = a;

            string result = StringUtils.Loose(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLoose_helloAndhl()
        {
            string a = "hello";
            string b = "hl";
            string expected = "eo";

            string result = StringUtils.Loose(a, b);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestLoose_helloAndle()
        {
            string a = "hello";
            string b = "le";
            string expected = "ho";

            string result = StringUtils.Loose(a, b);

            Assert.Equal(expected, result);
        }
    }

    public class UnitTest3
    {
        private Stack<string> _stack = new Stack<string>();

        [Fact]
        public void TestStack_PopEmptyStack()
        {
            Assert.Throws<ArgumentNullException>(() => _stack.Pop());
        }

        [Fact]
        public void TestStack_PeekEmptyStack()
        {
            Assert.Throws<ArgumentNullException>(() => _stack.Peek());
        }

        [Fact]
        public void TestStack_IsEmpty()
        {
            bool expected = true;

            bool actual = _stack.IsEmpty();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestStack_Size()
        {
            int expected = 0;

            int actual = _stack.Size();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestStack_Push555()
        {
            string expected = "555";

            _stack.Push("555");
            string actual = _stack.Pop();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestStack_Peek()
        {
            string expected = "555";

            _stack.Push("555");
            string actual = _stack.Peek();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestStack_ToString()
        {
            string expected = "333 - 444 - 555";

            _stack.Push("555");
            _stack.Push("444");
            _stack.Push("333");
            string actual = _stack.ToString();

            Assert.Equal(expected, actual);
        }
    }
}
