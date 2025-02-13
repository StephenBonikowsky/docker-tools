// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.IO;
using System.Linq;
using Microsoft.DotNet.ImageBuilder.Commands;
using Microsoft.DotNet.ImageBuilder.Models.Image;
using Microsoft.DotNet.ImageBuilder.Models.Manifest;
using Newtonsoft.Json;

namespace Microsoft.DotNet.ImageBuilder.ViewModel
{
    public class TagInfo
    {
        private string BuildContextPath { get; set; }
        public string FullyQualifiedName { get; private set; }
        public Tag Model { get; private set; }
        public string Name { get; private set; }

        private TagInfo()
        {
        }

        public static TagInfo Create(
            string name,
            Tag model,
            string repoName,
            VariableHelper variableHelper,
            string buildContextPath = null)
        {
            TagInfo tagInfo = new TagInfo
            {
                Model = model,
                BuildContextPath = buildContextPath
            };
            tagInfo.Name = variableHelper.SubstituteValues(name, tagInfo.GetVariableValue);
            tagInfo.FullyQualifiedName = GetFullyQualifiedName(repoName, tagInfo.Name);

            return tagInfo;
        }

        public static string GetFullyQualifiedName(string repoName, string tagName)
        {
            return $"{repoName}:{tagName}";
        }

        private string GetVariableValue(string variableType, string variableName)
        {
            string variableValue = null;

            if (string.Equals(variableType, VariableHelper.SystemVariableTypeId, StringComparison.Ordinal)
                && string.Equals(variableName, VariableHelper.DockerfileGitCommitShaVariableName, StringComparison.Ordinal)
                && BuildContextPath != null)
            {
                variableValue = GitHelper.GetCommitSha(BuildContextPath);
            }

            return variableValue;
        }
    }
}
