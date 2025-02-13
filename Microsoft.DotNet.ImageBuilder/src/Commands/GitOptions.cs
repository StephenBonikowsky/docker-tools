﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.CommandLine;

namespace Microsoft.DotNet.ImageBuilder.Commands
{
    public class GitOptions
    {
        public string AuthToken { get; set; }
        public string Branch { get; set; }
        public string Email { get; set; }
        public string Owner { get; set; }
        public string Path { get; set; }
        public string Repo { get; set; }
        public string Username { get; set; }

        public GitOptions(string defaultOwner, string defaultRepo, string defaultBranch, string defaultPath)
        {
            this.Owner = defaultOwner ?? throw new ArgumentNullException(nameof(defaultOwner));
            this.Repo = defaultRepo ?? throw new ArgumentNullException(nameof(defaultRepo));
            this.Branch = defaultBranch ?? throw new ArgumentNullException(nameof(defaultBranch));
            this.Path = defaultPath ?? throw new ArgumentNullException(nameof(defaultPath));
        }

        public void ParseCommandLine(ArgumentSyntax syntax)
        {
            string branch = Branch;
            syntax.DefineOption(
                "git-branch",
                ref branch,
                $"GitHub branch to write to (defaults to {branch})");
            Branch = branch;

            string owner = Owner;
            syntax.DefineOption(
                "git-owner",
                ref owner,
                $"Owner of the GitHub repo to write to (defaults to {owner})");
            Owner = owner;

            string path = Path;
            syntax.DefineOption(
                "git-path",
                ref path,
                $"Path within the GitHub repo to write to (defaults to {path})");
            Path = path;

            string repo = Repo;
            syntax.DefineOption(
                "git-repo",
                ref repo,
                $"GitHub repo to write to (defaults to {repo})");
            Repo = repo;

            string username = null;
            syntax.DefineParameter(
                "git-username",
                ref username,
                "GitHub username");
            Username = username;

            string email = null;
            syntax.DefineParameter(
                "git-email",
                ref email,
                "GitHub email");
            Email = email;

            string authToken = null;
            syntax.DefineParameter(
                "git-auth-token",
                ref authToken,
                "GitHub authentication token");
            AuthToken = authToken;
        }
    }
}
