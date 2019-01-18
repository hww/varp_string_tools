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

namespace Plugins.VARP.StringTools
{
    /// <summary>
    /// Has fixed maximum capacity. Does not produce garbage
    /// after invoking Clear method.
    /// </summary>
    public class BetterStringBuilder
    {

        private readonly char[] buffer;
        private int bufferPos;

        private string stringCache;

        public BetterStringBuilder(int capacity)
        {
            buffer = new char[capacity];
        }

        /// <summary>
        /// Append string to the buffer
        /// </summary>
        /// <param name="c">Character to add</param>
        public void Append(char c)
        {
            buffer[bufferPos++] = c;
            stringCache = null;
        }

        /// <summary>
        /// Clear buffer
        /// </summary>
        public void Clear()
        {
            bufferPos = 0;
            stringCache = null;
        }

        /// <summary>
        /// Get type of buffer
        /// </summary>
        public int Size
        {
            get { return bufferPos; }
        }

        /// <summary>
        /// Get string of the buffer
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            if (stringCache != null)
                return stringCache;
            return stringCache = new string(buffer, 0, bufferPos);
        }
    }

}
