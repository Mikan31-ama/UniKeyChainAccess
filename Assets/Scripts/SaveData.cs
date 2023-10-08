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

using System.IO;
using System.Text;
using UnityEngine;
using Plugin.iOS;

/// <summary>
/// SaveData class
/// </summary>
public class SaveData
{
    /// <summary>
    /// Initialize
    /// </summary>
    public static void Initialize()
    {
#if UNITY_IPHONE || UNITY_STANDALONE_OSX
        // 他プロジェクトと競合しないように一意な文字列にすること
        // Make it a unique string to avoid conflicts with other projects
        KeyChain.productUniqueId = "productUniqueId";
#endif
    }

    /// <summary>
    /// Save
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static bool Save<T>(string key, T data)
    {
        string fileName = key;
        string result = JsonUtility.ToJson(data);
#if UNITY_EDITOR
        string outputPath = Path.Combine(_GetPath(), fileName);
        try
        {
            using (var writer = new StreamWriter(outputPath, false, Encoding.UTF8))
            {
                writer.Write(result);
                writer.Flush();
            }
        }
        catch (IOException e) 
        {
            Debug.LogError(e.Message);
            return false;
        }
        return true;
#elif UNITY_IPHONE || UNITY_STANDALONE_OSX
        return KeyChain.BindSave(key, result);
#endif
    }

    /// <summary>
    /// Load
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T Load<T>(string key) where T : class
    {
        string fileName = key;
        string data = null;
#if UNITY_EDITOR
        string outputPath = Path.Combine(_GetPath(), fileName);
        if (!File.Exists(outputPath))
        {
            return null;
        }
        using (var reader = new StreamReader(outputPath, Encoding.UTF8))
        {
            data = reader.ReadToEnd();
        }
#elif UNITY_IPHONE || UNITY_STANDALONE_OSX
        data = KeyChain.BindLoad(key);
#endif
        return (string.IsNullOrEmpty(data)) ? null : JsonUtility.FromJson<T>(data);
    }

    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool Delete(string key)
    {
        string fileName = key;
#if UNITY_EDITOR
        string outputPath = Path.Combine(_GetPath(), fileName);
        try
        {
            File.Delete(outputPath);
        }
        catch (IOException e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        return true;
#elif UNITY_IPHONE || UNITY_STANDALONE_OSX
        return KeyChain.BindDelete(key);
#endif
    }

    /// <summary>
    /// Get Path
    /// </summary>
    /// <returns></returns>
    private static string _GetPath()
    {
        return Application.persistentDataPath;
    }
}

