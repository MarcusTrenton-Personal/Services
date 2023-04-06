﻿/*Copyright(C) 2022 Marcus Trenton, marcus.trenton@gmail.com

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <https://www.gnu.org/licenses/>.*/

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using System;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class ParamUtilTests
    {
        [TestMethod]
        public void VerifyStringHasContentWhenValid()
        {
            string value = "Red";
            ParamUtil.VerifyHasContent(nameof(value), value);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void VerifyStringHasContentWhenNull()
        {
            string value = null;
            ParamUtil.VerifyHasContent(nameof(value), value);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyStringHasContentWhenEmpty()
        {
            string value = string.Empty;
            ParamUtil.VerifyHasContent(nameof(value), value);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyStringHasContentWhenWhitespace()
        {
            string value = " ";
            ParamUtil.VerifyHasContent(nameof(value), value);
        }

        [TestMethod]
        public void VerifyNotNullWhenValid()
        {
            object value = new object();
            ParamUtil.VerifyNotNull(nameof(value), value);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void VerifyNotNullWhenNull()
        {
            object value = null;
            ParamUtil.VerifyNotNull(nameof(value), value);
        }

        [TestMethod]
        public void VerifyElementsAreNotNullWhenValidNotEmptyArray()
        {
            object[] values = new object[1] { new object() };
            ParamUtil.VerifyElementsAreNotNull(nameof(values), values);
        }

        [TestMethod]
        public void VerifyElementsAreNotNullWhenEmptyArray()
        {
            object[] values = Array.Empty<object>();
            ParamUtil.VerifyElementsAreNotNull(nameof(values), values);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void VerifyElementsAreNotNullWhenArrayIsNull()
        {
            object[] values = null;
            ParamUtil.VerifyElementsAreNotNull("values", values);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyElementsAreNotNullWhenArrayHasNulls()
        {
            object[] values = new object[1] { null };
            ParamUtil.VerifyElementsAreNotNull(nameof(values), values);
        }

        [TestMethod]
        public void VerifyElementsAreNotNullWhenValidNotEmptyList()
        {
            List<object> values = new List<object> { new object() };
            ParamUtil.VerifyElementsAreNotNull(nameof(values), values);
        }

        [TestMethod]
        public void VerifyElementsAreNotNullWhenValidNotEmptyListOfInts()
        {
            List<int> values = new List<int> { 0 };
            ParamUtil.VerifyElementsAreNotNull(nameof(values), values);
        }

        [TestMethod]
        public void VerifyWholeNumberOf1()
        {
            int x = 1;
            ParamUtil.VerifyWholeNumber(nameof(x), x);
        }

        [TestMethod]
        public void VerifyWholeNumberOf0()
        {
            int x = 0;
            ParamUtil.VerifyWholeNumber(nameof(x), x);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyWholeNumberOfNegative1()
        {
            int x = -1;
            ParamUtil.VerifyWholeNumber(nameof(x), x);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyPositiveNumberOf0()
        {
            int x = 0;
            ParamUtil.VerifyPositiveNumber(nameof(x), x);
        }

        [TestMethod]
        public void VerifyPositiveNumberOf1()
        {
            int x = 1;
            ParamUtil.VerifyPositiveNumber(nameof(x), x);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyPositiveNumberOfNegative1()
        {
            int x = -1;
            ParamUtil.VerifyPositiveNumber(nameof(x), x);
        }

        [DataTestMethod]
        [DataRow(1, 0, 2)]
        [DataRow(0, 0, 2)]
        [DataRow(2, 0, 2)]
        [DataRow(2, 2, 2, DisplayName = "Range of 0")]
        [DataRow(-2, -2, 2)]
        [DataRow(-22, -200, -20)]
        [DataRow(1_000_000, 800_000, 1_200_000)]
        public void VerifyInRangeSucceeds(int value, int minInclusive, int maxInclusive)
        {
            ParamUtil.VerifyInRange(nameof(value), value, minInclusive, maxInclusive);
        }

        [DataTestMethod, ExpectedException(typeof(ArgumentException))]
        [DataRow(1, 2, 0, DisplayName = "Max greater than min")]
        [DataRow(-1, 0, 2)]
        [DataRow(3, 0, 2)]
        public void VerifyInRangeFails(int value, int minInclusive, int maxInclusive)
        {
            ParamUtil.VerifyInRange(nameof(value), value, minInclusive, maxInclusive);
        }

        [TestMethod]
        public void VerifyDictionaryKeyExistsElementFound()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>
            {
                [1] = "One"
            };

            int key = 1;
            ParamUtil.VerifyDictionaryKeyExists(nameof(dictionary), dictionary, nameof(key), key);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyDictionaryKeyExistsElementMissing()
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>
            {
                [1] = "One"
            };

            int key = 2;
            ParamUtil.VerifyDictionaryKeyExists(nameof(dictionary), dictionary, nameof(key), key);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void VerifyDictionaryKeyExistsDictionaryIsNull()
        {
            Dictionary<int, string> dictionary = null;

            int key = 1;
            ParamUtil.VerifyDictionaryKeyExists(nameof(dictionary), dictionary, nameof(key), key);
        }

        [TestMethod]
        public void VerifyDictionaryValuesNotNullWhenNotNull()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                ["Rhino"] = new object()
            };
            ParamUtil.VerifyDictionaryValuesNotNull(nameof(dictionary), dictionary);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void VerifyDictionaryValuesNotNullWhenDictionaryIsNull()
        {
            Dictionary<object, string> dictionary = null;
            ParamUtil.VerifyDictionaryValuesNotNull(nameof(dictionary), dictionary);
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyDictionaryValuesNotNullWhenValueIsNull()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                ["Rhino"] = null
            };
            ParamUtil.VerifyDictionaryValuesNotNull(nameof(dictionary), dictionary);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void VerifyStringMatchesPatternNull()
        {
            string text = "abc";
            ParamUtil.VerifyMatchesPattern(nameof(text), text, pattern: null, "String does not match pattern");
        }

        [TestMethod]
        public void VerifyStringMatchesPatternEmpty()
        {
            string text = "abc";
            ParamUtil.VerifyMatchesPattern(nameof(text), text, pattern: "", "String does not match pattern");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void VerifyStringMatchesPatternValueNull()
        {
            ParamUtil.VerifyMatchesPattern("text",value: null, "[+-]?([0-9]*[.])?[0-9]+", "String does not match pattern");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyStringMatchesPatternValueEmpty()
        {
            ParamUtil.VerifyMatchesPattern("text", value: String.Empty, "[+-]?([0-9]*[.])?[0-9]+", "String does not match pattern");
        }

        [TestMethod]
        public void VerifyStringMatchesPatternValueEmptyPatternEmpty()
        {
            ParamUtil.VerifyMatchesPattern("text", String.Empty, pattern: String.Empty, "String does not match pattern");
        }

        [TestMethod]
        public void VerifyStringMatchesPatternThatExists()
        {
            string text = "-1.3";
            ParamUtil.VerifyMatchesPattern(nameof(text), text, "[+-]?([0-9]*[.])?[0-9]+", "String does not match pattern");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void VerifyStringMatchesPatternThatDoesNotExist()
        {
            string text = "abc";
            ParamUtil.VerifyMatchesPattern(nameof(text), text, "[+-]?([0-9]*[.])?[0-9]+", "String does not match pattern");
        }

        [DataTestMethod]
        [DataRow(@"abcdefghijklmnopqrstuvwxyz@email.com", DisplayName = "Personal Info has lower case ascii")]
        [DataRow(@"ABCDEFGHIJKLMNOPQRSTUVWXYZ@e.a", DisplayName = "Personal Info has upper case ascii")]
        [DataRow(@"a1234567890@e.ca", DisplayName = "Personal Info has numbers")]
        [DataRow(@"¢¥ÆØýЍձףಥᢁશ@e.ca", DisplayName = "Personal Info has unicode")]
        [DataRow(@"1234567890@e.ca", DisplayName = "Personal Info starts with numbers")]
        [DataRow(@"!#$%&'*+-/=?^_`{|}~()*@e.ca", DisplayName = "Personal Info has special characters !#$%&'*+-/=?^_`{|}~()*")]
        [DataRow("a b@b.com", DisplayName = "Personal Info has space")]
        [DataRow(@"A.b@e.a", DisplayName = "Personal Info has dot in the middle")]
        [DataRow("ab.@b.com", DisplayName = "Personal Info ends with .")]
        [DataRow("a..b@b.com", DisplayName = "Personal Info has double dots")]
        [DataRow(@"A.b@e", DisplayName = "Top Level Domain does not need a .")]
        [DataRow(@"a@abcdefghijklmnopqrstuvwxyz.com", DisplayName = "Top Level Domain has lower case ascii.")]
        [DataRow(@"a@ABCDEFGHIJKLMNOPQRSTUVWXYZ.com", DisplayName = "Top Level Domain has upper case ascii.")]
        [DataRow(@"a@a1234567890.com", DisplayName = "Top Level Domain has numbers.")]
        [DataRow(@"a@¢¥ÆØýЍձףಥᢁશ.ca", DisplayName = "Top Level Domain has unicode")]
        [DataRow(@"a@1234567890.com", DisplayName = "Top Level Domain has start with numbers.")]
        [DataRow("a@b.", DisplayName = "Top Level Domain ends with .")]
        [DataRow(@"a@a-b.com", DisplayName = "Top Level Domain has -.")]
        [DataRow("a@!#$%&'*+-/=?^_`{|}~.com", DisplayName = "Top Level Domain has special characeters !#$%&'*+-/=?^_`{|}~.")]
        [DataRow("a@b..com", DisplayName = "Top Level Domain has double dots")]
        public void EmailAddressRegexPasses(string pattern)
        {
            ParamUtil.VerifyIsEmailAddress(nameof(pattern), pattern);
        }

        [DataTestMethod, ExpectedException(typeof(ArgumentException))]
        [DataRow("", DisplayName = "Empty")]
        [DataRow("a.com", DisplayName = "Missing @")]
        [DataRow("a@@b.com", DisplayName = "Double @")]
        [DataRow("a@b@c.com", DisplayName = "Multiple @")]
        [DataRow("a\n@b.com", DisplayName = "Personal Info has new line")]
        [DataRow(".ab@b.com", DisplayName = "Personal Info starts with .")]
        [DataRow("@b.com", DisplayName = "Personal Info is empty")]
        [DataRow("a@", DisplayName = "Top Level Domain is empty")]
        [DataRow("a@.com", DisplayName = "Top Level Domain starts with .")]
        [DataRow("a@(.com", DisplayName = "Top Level Domain has (.")]
        [DataRow("a@).com", DisplayName = "Top Level Domain has ).")]
        [DataRow("a@b c.com", DisplayName = "Top Level Domain has space")]
        public void EmailRegexRegexFails(string pattern)
        {
            ParamUtil.VerifyIsEmailAddress(nameof(pattern), pattern);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void EmailRegexRegexFailsNull()
        {
            string pattern = null;
            ParamUtil.VerifyIsEmailAddress(nameof(pattern), pattern);
        }
    }
}
