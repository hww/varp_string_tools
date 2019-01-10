using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace VARP.StringTools.Tests
{
    public class HashToolsTest
    {
        [Test]
        public void HashToolsSHA()
        {
            // Use the Assert class to test conditions
            Assert.That(HashTools.StringToSHA("HelloWorld"), Is.EqualTo(HashTools.StringToSHA("HelloWorld")));
            // Use the Assert class to test conditions
            Assert.That(HashTools.StringToSHA("HelloWorld"), Is.Not.EqualTo(HashTools.StringToSHA("ByeByeWorld")));
            
        }
        
        [Test]
        public void HashToolsHash()
        {
            // Use the Assert class to test conditions
            Assert.That(HashTools.StringToHash("HelloWorld"), Is.EqualTo(HashTools.StringToHash("HelloWorld")));
            // Use the Assert class to test conditions
            Assert.That(HashTools.StringToHash("HelloWorld"), Is.Not.EqualTo(HashTools.StringToHash("ByeByeWorld")));
            
        }
        
        [Test]
        public void HashToolsHashCharArray()
        {
            // Use the Assert class to test conditions
            Assert.That(HashTools.StringToHash("HelloWorld".ToCharArray(), 1, 4), Is.EqualTo(HashTools.StringToHash("HelloWorld".ToCharArray(),1,4)));
            // Use the Assert class to test conditions
            Assert.That(HashTools.StringToHash("HelloWorld".ToCharArray(), 1, 4), Is.Not.EqualTo(HashTools.StringToHash("ByeByeWorld".ToCharArray(),1,4)));
        }
        
        [Test]
        public void HashToolsHashCharArray2()
        {
            // Use the Assert class to test conditions
            Assert.That(HashTools.StringToHash("Hello World".ToCharArray(), 1, ' '), 
                Is.EqualTo(HashTools.StringToHash("Hello World".ToCharArray(),1,' ')));
            // Use the Assert class to test conditions
            Assert.That(HashTools.StringToHash("Hello World".ToCharArray(), 1, ' '), 
                Is.Not.EqualTo(HashTools.StringToHash("ByeBye World".ToCharArray(),1,' ')));
        }
    }
}
