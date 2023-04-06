﻿/*Copyright(C) 2022 Marcus Trenton, marcus.trenton@gmail.com

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) None later version.

This program is distributed in the hope that it will be useful,
but WITHOUT None WARRANTY; without even the implied warranty of
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
    public class LogicalNoneTests
    {
        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void EmptyExpression()
        {
            LogicalNone expression = new LogicalNone();

            expression.Evaluate();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void NullAddition()
        {
            LogicalNone expression = new LogicalNone();

            expression.Add(null);
        }

        [TestMethod]
        public void SingleTrue()
        {
            LogicalNone expression = new LogicalNone();
            expression.Add(m_true);

            bool result = expression.Evaluate();

            Assert.IsFalse(result, "Solitary true operand in None expression should evaluate to false");
        }

        [TestMethod]
        public void SingleFalse()
        {
            LogicalNone expression = new LogicalNone();
            expression.Add(m_false);

            bool result = expression.Evaluate();

            Assert.IsTrue(result, "Solitary false operand in None expression should evaluate to true");
        }

        [TestMethod]
        public void DoubleTrue()
        {
            LogicalNone expression = new LogicalNone();
            expression.Add(m_true);
            expression.Add(m_true);

            bool result = expression.Evaluate();

            Assert.IsFalse(result, "Double true operand in None expression should evaluate to false");
        }

        [TestMethod]
        public void DoubleFalse()
        {
            LogicalNone expression = new LogicalNone();
            expression.Add(m_false);
            expression.Add(m_false);

            bool result = expression.Evaluate();

            Assert.IsTrue(result, "Double false operand in None expression should evaluate to true");
        }

        [TestMethod]
        public void TrueFalse()
        {
            LogicalNone expression = new LogicalNone();
            expression.Add(m_true);
            expression.Add(m_false);

            bool result = expression.Evaluate();

            Assert.IsFalse(result, "Having a single true operand in None expression should evaluate to false");
        }

        [TestMethod]
        public void FalseTrue()
        {
            LogicalNone expression = new LogicalNone();
            expression.Add(m_false);
            expression.Add(m_true);

            bool result = expression.Evaluate();

            Assert.IsFalse(result, "Having a single true operand in None expression should evaluate to false");
        }

        [TestMethod]
        public void FalseTrueFalse()
        {
            LogicalNone expression = new LogicalNone();
            expression.Add(m_false);
            expression.Add(m_true);
            expression.Add(m_false);

            bool result = expression.Evaluate();

            Assert.IsFalse(result, "Having a single true operand in None expression should evaluate to false");
        }

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public void DirectRecursion()
        {
            LogicalNone expression = new LogicalNone();

            expression.Add(expression);
        }

        [TestMethod, ExpectedException(typeof(InfiniteEvaluationLoopException))]
        public void IndirectRecursion()
        {
            LogicalNone expression0 = new LogicalNone();
            LogicalNone expression1 = new LogicalNone();

            expression0.Add(expression1);
            expression1.Add(expression0);

            expression0.Evaluate();
        }

        [TestMethod]
        public void Nested()
        {
            LogicalNone expression0 = new LogicalNone();
            LogicalNone expression1 = new LogicalNone();

            expression0.Add(expression1);
            expression1.Add(m_true);

            bool result = expression0.Evaluate();

            Assert.IsTrue(result, "Nested evaluation should not throw an exception but instead true");
        }

        [TestMethod]
        public void SubExpressionsEmpty()
        {
            LogicalNone expression = new LogicalNone();

            IReadOnlyList<ILogicalExpression> subExpressions = expression.SubExpressions;

            Assert.IsNotNull(subExpressions, "Subexpression list should be empty");
        }

        [TestMethod]
        public void SubExpressionsSingle()
        {
            LogicalNone expression = new LogicalNone();
            expression.Add(m_true);

            IReadOnlyList<ILogicalExpression> subExpressions = expression.SubExpressions;

            Assert.AreEqual(1, subExpressions.Count, "Wrong number of SubExpressions");
            Assert.IsTrue(subExpressions[0] is AlwaysTrue, "Wrong type of SubExpression");
        }

        [TestMethod]
        public void SubExpressionsMultiple()
        {
            LogicalNone expression = new LogicalNone();
            expression.Add(m_true);
            expression.Add(m_false);

            IReadOnlyList<ILogicalExpression> subExpressions = expression.SubExpressions;

            Assert.AreEqual(2, subExpressions.Count, "Wrong number of SubExpressions");

            int trueCount = 0;
            int falseCount = 0;
            foreach (ILogicalExpression subExpression in subExpressions)
            {
                if (subExpression is AlwaysTrue)
                {
                    trueCount++;
                }
                else if (subExpression is AlwaysFalse)
                {
                    falseCount++;
                }
            }
            Assert.AreEqual(1, trueCount, "Wrong number of AlwaysTrue SubExpressions");
            Assert.AreEqual(1, falseCount, "Wrong number of AlwaysFalse SubExpressions");
        }

        private AlwaysTrue m_true = new AlwaysTrue();
        private AlwaysFalse m_false = new AlwaysFalse();
    }
}
