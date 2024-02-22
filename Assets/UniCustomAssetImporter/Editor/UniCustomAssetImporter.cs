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

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Shibuya24.Tools.Editor
{
    public class UniCustomAssetImporter : ScriptableObject
    {
        public bool isEnabled;
        public List<CustomTextureImportSetting> settings;
    }

    [CustomEditor(typeof(UniCustomAssetImporter))]
    class UniCustomAssetImporterEditor : UnityEditor.Editor
    {
        private static readonly string NewFileName = "NewSetting";
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Create Setting", GUILayout.Height(30f)))
            {
                var importer = (UniCustomAssetImporter)target;
                var sameObject = importer.settings.FirstOrDefault(x => x.name == NewFileName);
                if (sameObject != null)
                {
                    Debug.LogError($"Failed to create setting, Please rename -> {NewFileName}", sameObject);
                    return;
                }
                
                var setting = CreateInstance<CustomTextureImportSetting>();
                AssetDatabase.CreateAsset(setting,
                    $"{UniCustomAssetImporterPostProcessor.SettingDirPath}{NewFileName}.asset");
                importer.settings.Add(setting);
             
                AssetDatabase.Refresh();
                EditorUtility.SetDirty(importer);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
