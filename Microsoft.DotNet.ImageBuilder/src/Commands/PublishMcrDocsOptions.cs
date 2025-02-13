// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.CommandLine;

namespace Microsoft.DotNet.ImageBuilder.Commands
{
    public class PublishMcrDocsOptions : ManifestOptions, IGitOptionsHost
    {
        protected override string CommandHelp => "Publishes the readmes to MCR";

        public GitOptions GitOptions { get; } = new GitOptions("Microsoft", "mcrdocs", "master", "teams");
        
        public string SourceUrl { get; set; }

        public PublishMcrDocsOptions() : base()
        {
        }

        public override void ParseCommandLine(ArgumentSyntax syntax)
        {
            base.ParseCommandLine(syntax);

            GitOptions.ParseCommandLine(syntax);

            string sourceUrl = null;
            syntax.DefineParameter("source-url", ref sourceUrl, "Base URL of the Dockerfile sources");
            SourceUrl = sourceUrl;
        }
    }
}
