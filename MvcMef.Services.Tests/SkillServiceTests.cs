using System;
using System.Linq;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MefMvc.Data;

namespace MefMvc.Services.Tests
{
    [TestClass]
    public class SkillServiceTests
    {
        private CompositionContainer container;

        public SkillServiceTests()
        {
            container = new CompositionContainer(new DirectoryCatalog(".", "*.dll"));
            container.SatisfyImportsOnce(this);
        }

        [TestMethod]
        public void TestMethod1()
        {
            #region Skills TXT

            var skillTxt = @"C#
    Expression Trees
    Threads
    Lambda Expressions
    Async
    Extension Methods
    Assemblies & GAC
    Partial
        Classes
        Methods
    Type categories
        Reference
        Value
    Serialization
    Attributes
    Exception
    Logging
    Interfaces
        IEnumerable<T>
        INotifyPropertyChanged
        IDisposable
    Classes
        Nested Classes
        Delegates
        Events
        Properties
            Auto-properties
        Indexed properties
    Struct
    Naming Guidelines
        Capitalization Conventions
        General Naming Guidelines
        Prefixes and Suffixes
        Class Structure
    Reflection
    Unit Test
    Named arguments
    Covariance / Contravariance
    Special Types
        Generics
        Implicitly typed arrays
        Lazy
        ExpandoObject
        Anonymous Types
    Optional Parameters
    var
    dynamic
    File System
        FileInfo
        DirectoryInfo
    Collections
        Linq
        Lists
        Stacks
        Queues
        Arrays
        Dictionary
    6.0
        nameof operator
        Null propagator
        Exception Filters
        Auto-property Initializer
        Default values for getter only property
        Expression-bodied members
        String Interpolation
        Dictionary Initializer";

            #endregion

            var list = skillTxt.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<int, Skill> dic = new Dictionary<int, Skill>();

            var result = list.Select(o =>
                {
                    Skill parent = null;
                    Skill skill = new Skill { Name = o.Trim() };

                    int depth = o.TakeWhile(c => c == ' ').Count() / 4;

                    if (depth > 0 && dic.TryGetValue(depth - 1, out parent))
                    {
                        skill.ParentSkill = parent;
                    }

                    dic[depth] = skill;

                    return skill;
                })
                .Select(o => o.Name + (o.ParentSkill == null ? "" : " : " + o.ParentSkill.Name))
                .ToList();

        }
    }
}
