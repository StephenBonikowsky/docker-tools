// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.CommandLine;
using System.Linq;
using System.Reflection;
using Microsoft.DotNet.ImageBuilder.ViewModel;

namespace Microsoft.DotNet.ImageBuilder.Commands
{
    public abstract class Options : IOptionsInfo
    {
        public bool IsDryRun { get; set; }
        public bool IsVerbose { get; set; }

        protected abstract string CommandHelp { get; }

        public string GetOption(string name)
        {
            string result;

            PropertyInfo propInfo = GetType().GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.Ordinal));
            if (propInfo != null)
            {
                result = propInfo.GetValue(this)?.ToString() ?? "";
            }
            else
            {
                result = null;
            }

            return result;
        }

        public virtual void ParseCommandLine(ArgumentSyntax syntax)
        {
            ArgumentCommand command = syntax.DefineCommand(GetCommandName(), this);
            command.Help = CommandHelp;

            bool isDryRun = false;
            syntax.DefineOption("dry-run", ref isDryRun, "Dry run of what images get built and order they would get built in");
            IsDryRun = isDryRun;

            bool isVerbose = false;
            syntax.DefineOption("verbose", ref isVerbose, "Show details about the tasks run");
            IsVerbose = isVerbose;
        }

        public string GetCommandName()
        {
            string commandName = GetType().Name.TrimEnd("Options");
            return char.ToLowerInvariant(commandName[0]) + commandName.Substring(1);
        }
    }
}
