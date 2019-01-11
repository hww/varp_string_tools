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

namespace VARP.StringTools
{
    /// <summary>
    /// This class is the tool for building readable table similar to report of: 'ls' command in bash 
    /// </summary>
    /// <example>
    /// 
    ///   Initialize source strings:
    /// 
    ///     var strings = new string[] { "foo1", "foo2", "foo3", "foo4", "foo5", "foo6", "foo7", "foo8" };
    /// 
    ///   To build table which fit to requested maximum with (12), span (2):
    /// 
    ///     var table = StingTableBuilder.BuildTable(strings, 12, 2);
    /// 
    ///   To build table which has exact columns number (2), and each column has required width (8), span (4):
    /// 
    ///     var table = StingTableBuilder.BuildTable(strings, 2, 8, 4);
    /// 
    /// </example>
    public class StingTableBuilder
    {

        /// <summary>
        /// Return the table from list of strings similar as result of: 'ls' command in bash.
        /// Calculate columns quantity by maximum length of the given words plus colspan
        /// </summary>
        /// <param name="words">array of words</param>
        /// <param name="resultWidth">maximum length of each result line</param>
        /// <param name="colSpan">space between columns</param>
        /// <returns>list of lines</returns>
        public static string[] BuildTable ( string[] words, int resultWidth, int colSpan)
        {
            var maxlenght = GetMaxLength ( words );
            var colwidth = maxlenght + colSpan;
            var nColumns = System.Math.Max(resultWidth / colwidth, 1);
            return BuildTable ( words, nColumns, colwidth, colSpan );
        }
        /// <summary>
        /// Return the table from list of strings similar as result of: 'ls' command in bash
        /// </summary>
        /// <param name="words"></param>
        /// <param name="resultWidth"></param>
        /// <param name="colSpan"></param>
        /// <returns></returns>
        public static string[] BuildTable ( string[] words, int colNum, int colWidth, int colSpan )
        {
            UnityEngine.Debug.Assert ( colNum > 0 );
            UnityEngine.Debug.Assert ( colWidth > 0 );
            var wodWidth = colWidth - colSpan;
            // Note: shorter tat 4 characters words will not work with truncating
            // because: truncating add ellipse string to te end '...' 
            UnityEngine.Debug.Assert ( wodWidth > 3 ); 
            var nRows = words.Length / colNum;
            if ( nRows * colNum < words.Length )
                nRows++;
            var result = new string[ nRows ];
            var rowcount = 0;
            var colcount = 0;
            var sentence = string.Empty;
            foreach ( var word in words )
            {
                if ( word.Length > wodWidth )
                    sentence += StringTools.Truncate ( word, wodWidth ).PadRight ( colWidth );
                else
                    sentence += word.PadRight ( colWidth );

                if ( ++colcount >= colWidth )
                {
                    colcount = 0;
                    result[ rowcount++ ] = sentence;
                    sentence = string.Empty;
                }
            }
            if ( rowcount < result.Length )
                result[ rowcount ] = sentence;
            return result;
        }
        /// <summary>
        ///  Return maximum lenght of the list of strings
        /// </summary>
        private static int GetMaxLength ( string[] words )
        {
            var maxSize = 0;
            foreach ( var word in words )
                maxSize = word == null ? 0 : System.Math.Max ( maxSize, word.Length );
            return maxSize;
        }
    }
}
