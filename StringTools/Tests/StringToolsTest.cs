using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using VARP.StringTools;

namespace VARP.StringTools.Tests
{
    public class StringToolsTest
    {
        [Test]
        public void GetAlphanumerics()
        {

            Assert.That(StringTools.GetAlphanumerics(" Hello .< World 1 _."), 
                Is.EqualTo("HelloWorld1"));
        }
        [Test]
        public void GetAlphabet()
        {

            Assert.That(StringTools.GetAlphabet(" B C 1 A A B C 1"), 
                Is.EqualTo(" 1ABC"));
        }
        
        [Test]
        public void Humanize()
        {
            Assert.That(StringTools.Humanize("foo-bar_baz bazz"), 
                Is.EqualTo("Foo bar baz bazz"));
            Assert.That(StringTools.Humanize("foo-bar_baz bazz", false), 
                Is.EqualTo("foo bar baz bazz"));
            Assert.That(StringTools.Humanize("foo_bar_baz bazz", true, true), 
                Is.EqualTo("Foo Bar Baz Bazz"));            
        }
        
        [Test]
        public void Camelize()
        {
            Assert.That(StringTools.Camelize("foo-bar_baz bazz"), 
                Is.EqualTo("FooBarBazBazz"));
            Assert.That(StringTools.Camelize("foo-bar_baz bazz", false), 
                Is.EqualTo("fooBarBazBazz"));
            Assert.That(StringTools.Camelize("foo_bar_baz bazz"), 
                Is.EqualTo("FooBarBazBazz"));            
        }
        
        [Test]
        public void Decamelize()
        {
            Assert.That(StringTools.Decamelize("FooBar"), 
                Is.EqualTo("foo-bar"));
            Assert.That(StringTools.Decamelize("fooBar"), 
                Is.EqualTo("foo-bar"));
        }
        
        [Test]
        public void WordWrap()
        {
            Assert.That(StringTools.WordWrap("Lorem Ipsum is simply dummy text.",20), 
                Is.EqualTo("Lorem Ipsum is\r\nsimply dummy text."));
        }
        
        
        [Test]
        public void Truncate()
        {
            Assert.That(StringTools.Truncate("Lorem Ipsum is simply dummy text.",20), 
                Is.EqualTo("Lorem Ipsum is simpl..."));
        }
    }
}
