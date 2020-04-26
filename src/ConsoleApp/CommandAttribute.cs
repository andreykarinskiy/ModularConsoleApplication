using System;
using System.ComponentModel.Composition;

namespace ConsoleApp
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class CommandAttribute : ExportAttribute
    {
        public CommandAttribute(string name) : base(typeof(Command))
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
