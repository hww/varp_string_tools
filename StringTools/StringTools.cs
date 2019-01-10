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

using System.Text.RegularExpressions;
using System;
using System.Text;
using UnityEngine;

namespace VARP.StringTools
{

    public static partial class StringTools
    {     
        /// <summary>
        /// Remove spaces
        /// </summary>
        /// <param name="me"></param>
        /// <returns></returns>
        public static string ParseOutSpacesAndSymbols(string me)
        {
            return Regex.Replace(me, "[^A-Za-z0-9]", string.Empty);
        }

        /// <summary>
        /// Alphabetize the characters in the string.
		/// It return all characters used in the string.
        /// </summary>
        public static string Alphabetize(string s)
        {
            // Convert to char array.
            var a = s.ToCharArray();
            // Sort letters.
            Array.Sort(a);
            // Return modified string.
            return new string(a);
        }

        /// <summary>
        /// Replace characters '-' or '_' by space and adding space before each
        /// capital character:
        ///   "FooBar"   ->   "Foo Bar"
        ///   "_foo_bar" ->   "Foo Bar"
        ///   "-foo-bar" ->   "Foo Bar"
        ///   "FooBar"   ->   "Foo Bar"  
        /// </summary>
        /// <param name="str"></param>
        /// <param name="capitalize"></param>
        /// <returns></returns>
        public static string Humanize(string str)
        {
            Debug.Assert(str != null);
            var capitalize = true;
            var output = string.Empty;
            for (var i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (c == '-' || c == '_')
                {
                    capitalize = true;
                    output += ' ';
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    if (i > 0)
                        output += ' ';
                    output += c;
                    capitalize = false;
                }
                else
                {
                    if (capitalize)
                    {
                        output += char.ToUpper(c);
                        capitalize = false;
                    }
                    else
                    {
                        output += c;
                    }
                }
            }
            return output;
        }

		/// <summary>
		/// Remove ' ', '-' or '_' to change forms:
		///   "foo-bar"  ->  "FooBar"
        ///   "foo_bar"  ->  "FooBar"
        ///   "foo bar"  ->  "FooBar"
        /// </summary>
		/// <param name="str"></param>
		/// <param name="capitalize">Should be capitalized first character or not</param>
		/// <returns></returns>
        public static string Camelize(string str, bool capitalize = true)
        {
            Debug.Assert(str != null);
            var output = string.Empty;
            for (var i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (c == '-' || c == '_' || c == ' ')
                {
                    capitalize = true;
                }
                else
                {
                    if (capitalize)
                    {
                        output += char.ToUpper(c);
                        capitalize = false;
                    }
                    else
                    {
                        output += char.ToLower(c);
                    }
                }
            }
            return output;
        }

		/// <summary>
		/// Convert 'FooBar' to 'foo-bar'
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
        public static string Decamelize(string str)
        {
            Debug.Assert(str != null);
            var output = string.Empty;
            var small = false;
            var space = false;
            for (var i = 0; i < str.Length; i++)
            {
                var c = str[i];
                if (char.IsUpper(c))
                {
                    if (small)
                        output += '-';
                    output += char.ToLower(c);
                    small = false;
                    space = false;
                }
                else if (c == ' ')
                {
                    small = true; // make - if next capital
                    space = true; // make - if nex down
                }
                else
                {
                    if (space)
                        output += '-';
                    output += c;
                    small = true; // make - if next capital
                    space = false; // do not make - if next small
                }
            }
            return output;
        }


        /// <summary>
        /// Word wraps the given text to fit within the specified width.
        /// </summary>
        /// <param name="text">Text to be word wrapped</param>
        /// <param name="width">Width, in characters, to which the text
        /// should be word wrapped</param>
        /// <returns>The modified text</returns>
        public static string WordWrap ( string text, int width )
        {
            Debug.Assert(text != null);
            Debug.Assert(width > 0);
            int pos, next;
            StringBuilder sb = new StringBuilder ( );

            // Lucidity check
            if ( width < 1 )
                return text;

            // Parse each line of text
            for ( pos = 0 ; pos < text.Length ; pos = next )
            {
                // Find end of line
                int eol = text.IndexOf ( Environment.NewLine, pos );
                if ( eol == -1 )
                    next = eol = text.Length;
                else
                    next = eol + Environment.NewLine.Length;

                // Copy this line of text, breaking into smaller lines as needed
                if ( eol > pos )
                {
                    do
                    {
                        int len = eol - pos;
                        if ( len > width )
                            len = BreakLine ( text, pos, width );
                        sb.Append ( text, pos, len );
                        sb.Append ( Environment.NewLine );

                        // Trim whitespace following break
                        pos += len;
                        while ( pos < eol && Char.IsWhiteSpace ( text[ pos ] ) )
                            pos++;
                    } while ( eol > pos );
                }
                else
                    sb.Append ( Environment.NewLine ); // Empty line
            }
            return sb.ToString ( );
        }

        /// <summary>
        /// Locates position to break the given line so as to avoid
        /// breaking words.
        /// </summary>
        /// <param name="text">String that contains line of text</param>
        /// <param name="pos">Index where line of text starts</param>
        /// <param name="max">Maximum line length</param>
        /// <returns>The modified line length</returns>
        private static int BreakLine ( string text, int pos, int max )
        {
            // Find last whitespace in line
            int i = max;
            while ( i >= 0 && !Char.IsWhiteSpace ( text[ pos + i ] ) )
                i--;

            // If no whitespace found, break at maximum length
            if ( i < 0 )
                return max;

            // Find start of whitespace
            while ( i >= 0 && Char.IsWhiteSpace ( text[ pos + i ] ) )
                i--;

            // Return length of text before whitespace
            return i + 1;
        }

        /// <summary>
        /// Truncate string and add ellipses at the end
        /// </summary>
        /// <param name="value"></param>
        /// <param name="maxChars"></param>
        /// <returns></returns>
        public static string Truncate ( this string value, int maxChars )
        {
            return value.Length <= maxChars ? value : value.Substring ( 0, maxChars ) + "...";
        }
        
        /// <summary>
        /// Trivial converter of singular form to the plural by adding 's' character
        /// </summary>
        /// <param name="singularForm"></param>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public static string Pluralize(this string singularForm, int howMany)
        {
            return singularForm.Pluralize(howMany, singularForm + "s");
        }

        /// <summary>
        /// Trivial converter of singular form to the plural by choosing one of two variants
        /// </summary>
        /// <param name="singularForm"></param>
        /// <param name="howMany"></param>
        /// <param name="pluralForm"></param>
        /// <returns></returns>
        public static string Pluralize(this string singularForm, int howMany, string pluralForm)
        {
            return howMany == 1 ? singularForm : pluralForm;
        }
    }
}