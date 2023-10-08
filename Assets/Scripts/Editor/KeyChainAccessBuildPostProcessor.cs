// MIT License
// 
// Copyright(c) 2023 Mikan31
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

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

namespace Build.KeyChainAccess
{
    /// <summary>
	/// Key Chain Access Build Post Processor class
	/// </summary>
    public static class KeyChainAccessBuildPostProcessor
    {
        /// <summary>
        /// On Post Process Build
        /// </summary>
        /// <param name="buildTarget"></param>
        /// <param name="buildPath"></param>
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string buildPath)
        {
            if (buildTarget != BuildTarget.iOS)
            {
                return;
            }

            var projPath = buildPath + "/Unity-iPhone.xcodeproj/project.pbxproj";
            var proj = new PBXProject();
            proj.ReadFromFile(projPath);
            string xcodeTarget = proj.GetUnityMainTargetGuid();
            string xcodeFrameworkTarget = proj.GetUnityFrameworkTargetGuid();
            proj.SetBuildProperty(xcodeFrameworkTarget, "ALWAYS_EMBED_SWIFT_STANDARD_LIBRARIES", "NO");
            // SwiftVersionは環境に応じて変更する
            // Change SwiftVersion depending on the environment
            proj.AddBuildProperty(xcodeTarget, "SWIFT_VERSION", "5.7");
            proj.WriteToFile(projPath);

        }
    }
}
#endif