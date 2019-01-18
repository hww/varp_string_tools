// =============================================================================
// MIT License
// 
// Copyright (c) 2018 Valeriya Pudova (hww.github.io)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// =============================================================================

using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Analytics;

namespace Plugins.VARP.StringTools
{
    public static class HashTools {
    
        public static string StringToSHA(string datastring)
        {
            // setup sha
            var crypt = new SHA256Managed();
            // compute hash
            var crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(datastring), 0,
                Encoding.UTF8.GetByteCount(datastring));
            // convert to hex
            var hash = string.Empty;
            foreach (var bit in crypto)
            {
                hash += bit.ToString("x2");
            }
            return hash;
        }

        public static ulong StringToHash(string text) 
        {
            ulong prime = 1099511628211u;
            ulong offset_basis = 14695981039346656037u;
            ulong hash = offset_basis;
            var len = text.Length;
            for (var i=0; i<len; i++)
                hash = (hash ^ text[i]) * prime;
            return hash;
        }
        
        public static ulong StringToHash(string text, int starts, int length) 
        {
            Debug.Assert(starts < text.Length);
            Debug.Assert(starts+length <= text.Length);
            ulong prime = 1099511628211u;
            ulong offset_basis = 14695981039346656037u;
            ulong hash = offset_basis;
            var len = System.Math.Min(text.Length, starts + length);
            for (var i=starts; i<len; i++)
                hash = (hash ^ text[i]) * prime;
            return hash;
        }
        
        public static ulong StringToHash(string text, int starts, char terminateAtCharacter) 
        {
            Debug.Assert(starts < text.Length);
            ulong prime = 1099511628211u;
            ulong offset_basis = 14695981039346656037u;
            ulong hash = offset_basis;
            var len = text.Length;
            for (var i = starts; i < len; i++)
            {
                var c = text[i];
                if (c == terminateAtCharacter)
                    return hash;
                hash = (hash ^ c) * prime;
            }
            return hash;
        }
        
        public static ulong StringToHash(char[] text) 
        {
            ulong prime = 1099511628211u;
            ulong offset_basis = 14695981039346656037u;
            ulong hash = offset_basis;
            var len = text.Length;
            for (var i=0; i<len; i++)
                hash = (hash ^ text[i]) * prime;
            return hash;
        }
        
        public static ulong StringToHash(char[] text, int starts, int length) 
        {
            Debug.Assert(starts < text.Length);
            Debug.Assert(starts+length <= text.Length);
            ulong prime = 1099511628211u;
            ulong offset_basis = 14695981039346656037u;
            ulong hash = offset_basis;
            var len = System.Math.Min(text.Length, starts + length);
            for (var i=starts; i<len; i++)
                hash = (hash ^ text[i]) * prime;
            return hash;
        }
        
        public static ulong StringToHash(char[] text, int starts, char terminateAtCharacter) 
        {
            Debug.Assert(starts < text.Length);
            ulong prime = 1099511628211u;
            ulong offset_basis = 14695981039346656037u;
            ulong hash = offset_basis;
            var len = text.Length;
            for (var i = starts; i < len; i++)
            {
                var c = text[i];
                if (c == terminateAtCharacter)
                    return hash;
                hash = (hash ^ c) * prime;
            }
            return hash;
        }
    }
}
