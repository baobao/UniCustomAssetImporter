// Copyright (c) 2024 ohbashunsuke
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

using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Shibuya24.Tools.Editor
{
    public class UniCustomAssetImporterPostProcessor : AssetPostprocessor
    {
        public static readonly string RootDirPath = "Assets/Tools/UniCustomAssetImporter/Editor/";
        public static readonly string SettingDirPath = "Assets/Tools/UniCustomAssetImporter/Editor/Settings/";
        private static readonly string SettingRootFilePath = RootDirPath + "UniCustomAssetImporter.asset";
        
        private void OnPostprocessTexture(Texture2D texture)
        {
            var textureImporter = assetImporter as TextureImporter;
            if (textureImporter == null) return;
            // if (textureImporter.importSettingsMissing == false)
            // {
            //     return;
            // }
            
            if (Directory.Exists(SettingDirPath) == false)
            {
                Directory.CreateDirectory(SettingDirPath);
            }

            if (File.Exists(SettingRootFilePath) == false)
            {
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<UniCustomAssetImporter>(),
                    SettingRootFilePath);
            }
            
            var settingRoot = AssetDatabase.LoadAssetAtPath<UniCustomAssetImporter>(SettingRootFilePath);

            if (settingRoot == null || settingRoot.isEnabled == false || settingRoot.settings == null ||
                settingRoot.settings.Count <= 0) 
            {
                return;
            }
            
            Debug.Log($"OnPostprocessTexture : {texture.name} / {textureImporter}");
            if (textureImporter == null) return;
            {
                foreach (var setting in settingRoot.settings)
                {
                    if (setting.isEnabled == false) continue;
                    if (IsMatchPathRule(assetPath, setting))
                    {
                        ApplyTextureImportSetting(textureImporter, setting);
                    }
                }
            }
        }

        private bool IsMatchPathRule(string assetPath, CustomTextureImportSetting setting)
        {
            if (setting.folder != null)
            {
                var path = AssetDatabase.GetAssetPath(setting.folder);
                if (assetPath.Contains(path))
                {
                    return true;
                }
            }
            else if (string.IsNullOrEmpty(setting.regex) == false)
            {
                return Regex.IsMatch(assetPath, setting.regex);
            }

            return false;
        }

        private void ApplyTextureImportSetting(TextureImporter importer, CustomTextureImportSetting setting)
        {
            importer.textureCompression = setting.textureCompression;
            importer.textureType = setting.textureType;
            importer.spriteImportMode = setting.spriteImportMode;
            importer.filterMode = setting.filterMode;
            importer.wrapMode = setting.wrapMode;

            foreach (var platformSetting in setting.platformSettings)
            {
                importer.SetPlatformTextureSettings(platformSetting.ToTextureImporterPlatformSettings());
            }
        }
    }
}
