using NUnit.Framework;

namespace VARP.StringTools.Tests
{
    public class StringToolsTest
    {
        [Test]
        public void GetAlphanumerics()
        {

            Assert.That(Humanizer.GetAlphanumerics(" Hello .< World 1 _."), 
                Is.EqualTo("HelloWorld1"));
        }
        [Test]
        public void GetAlphabet()
        {

            Assert.That(Humanizer.GetAlphabet(" B C 1 A A B C 1"), 
                Is.EqualTo(" 1ABC"));
        }
        
        [Test]
        public void Humanize()
        {
            Assert.That(Humanizer.Humanize("foo-bar_baz bazz"), 
                Is.EqualTo("Foo bar baz bazz"));
            Assert.That(Humanizer.Humanize("foo-bar_baz bazz", false), 
                Is.EqualTo("foo bar baz bazz"));
            Assert.That(Humanizer.Humanize("foo_bar_baz bazz", true, true), 
                Is.EqualTo("Foo Bar Baz Bazz"));            
        }
        
        [Test]
        public void Camelize()
        {
            Assert.That(Humanizer.Camelize("foo-bar_baz bazz"), 
                Is.EqualTo("FooBarBazBazz"));
            Assert.That(Humanizer.Camelize("foo-bar_baz bazz", false), 
                Is.EqualTo("fooBarBazBazz"));
            Assert.That(Humanizer.Camelize("foo_bar_baz bazz"), 
                Is.EqualTo("FooBarBazBazz"));            
        }
        
        [Test]
        public void Decamelize()
        {
            Assert.That(Humanizer.Decamelize("FooBar"), 
                Is.EqualTo("foo-bar"));
            Assert.That(Humanizer.Decamelize("fooBar"), 
                Is.EqualTo("foo-bar"));
        }
        
        [Test]
        public void WordWrap()
        {
            Assert.That(Humanizer.WordWrap("Lorem Ipsum is simply dummy text.",20), 
                Is.EqualTo("Lorem Ipsum is\r\nsimply dummy text."));
        }
        
        
        [Test]
        public void Truncate()
        {
            Assert.That(Humanizer.Truncate("Lorem Ipsum is simply dummy text.",20), 
                Is.EqualTo("Lorem Ipsum is simpl..."));
        }
    }
}
