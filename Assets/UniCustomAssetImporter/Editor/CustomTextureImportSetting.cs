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

using System;
using UnityEditor;
using UnityEngine;

namespace Shibuya24.Tools.Editor
{
    public enum TextureImporterPlatform
    {
        iOS,
        Android,
        WebGL,
    }

    public enum MaxTextureSize
    {
        _32 = 32,
        _64 = 64,
        _128 = 128,
        _256 = 256,
        _512 = 512,
        _1024 = 1024,
        _2048 = 2048,
        _4096 = 4096,
        _8192 = 8192,
    }
    
    public class CustomTextureImportSetting : ScriptableObject
    {
        public bool isEnabled = true;
        [TextArea(2, 10)]
        public string comment;
        [Space(10)]
        public DefaultAsset folder;
        public string regex = "";
        public TextureImporterCompression textureCompression;
        public TextureImporterType textureType = TextureImporterType.Sprite;
        public FilterMode filterMode = FilterMode.Bilinear;
        public TextureWrapMode wrapMode = TextureWrapMode.Clamp;
        [Header("Each Platform Settings")]
        public TexturePlatformSettings[] platformSettings;
    }

    [Serializable]
    public class TexturePlatformSettings
    {
        public TextureImporterPlatform platform;
        public bool overridden = true;
        public MaxTextureSize maxTextureSize = MaxTextureSize._2048;
        public TextureImporterCompression textureCompression = TextureImporterCompression.Compressed;
        public TextureImporterFormat format = TextureImporterFormat.ASTC_6x6;
        public int textureQuality = 50;

        public TextureImporterPlatformSettings ToTextureImporterPlatformSettings()
        {
            return new TextureImporterPlatformSettings
            {
                name = platform.ToString(),
                overridden = overridden,
                maxTextureSize = (int)maxTextureSize,
                textureCompression = textureCompression,
                format = format,
                compressionQuality = textureQuality
            };
        }
    }
}
