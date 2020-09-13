using Suilder.Extensions;
using Xunit;

namespace Suilder.Test.Builder.Operators
{
    public class ToLikeTest : BuilderBaseTest
    {
        [Fact]
        public void Builder_ToLikeStart()
        {
            string value = sql.ToLikeStart("abcd");
            Assert.Equal("abcd%", value);
        }

        [Fact]
        public void Builder_ToLikeStart_Already_Has_Wildcard()
        {
            string value = sql.ToLikeStart("abcd%");
            Assert.Equal("abcd%", value);
        }

        [Fact]
        public void Builder_ToLikeStart_Empty_String()
        {
            string value = sql.ToLikeStart("");
            Assert.Equal("%", value);
        }

        [Fact]
        public void Extension_ToLikeStart_Object()
        {
            string value = "abcd".ToLikeStart();
            Assert.Equal("abcd%", value);
        }

        [Fact]
        public void Builder_ToLikeEnd()
        {
            string value = sql.ToLikeEnd("abcd");
            Assert.Equal("%abcd", value);
        }

        [Fact]
        public void Builder_ToLikeEnd_Already_Has_Wildcard()
        {
            string value = sql.ToLikeEnd("%abcd");
            Assert.Equal("%abcd", value);
        }

        [Fact]
        public void Builder_ToLikeEnd_Empty_String()
        {
            string value = sql.ToLikeEnd("");
            Assert.Equal("%", value);
        }

        [Fact]
        public void Extension_ToLikeEnd()
        {
            string value = "abcd".ToLikeEnd();
            Assert.Equal("%abcd", value);
        }

        [Fact]
        public void Builder_ToLikeAny()
        {
            string value = sql.ToLikeAny("abcd");
            Assert.Equal("%abcd%", value);
        }

        [Fact]
        public void Builder_ToLikeAny_Already_Has_Wildcard()
        {
            string value = sql.ToLikeAny("%abcd%");
            Assert.Equal("%abcd%", value);
        }

        [Fact]
        public void Builder_ToLikeAny_Empty_String()
        {
            string value = sql.ToLikeAny("");
            Assert.Equal("%", value);
        }

        [Fact]
        public void Extension_ToLikeAny()
        {
            string value = "abcd".ToLikeAny();
            Assert.Equal("%abcd%", value);
        }
    }
}