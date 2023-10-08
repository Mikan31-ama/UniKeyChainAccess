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

using UnityEngine;
#if UNITY_IPHONE || UNITY_STANDALONE_OSX
using System.Runtime.InteropServices;
#endif

namespace Plugin.iOS
{
    /// <summary>
    /// Key chain
    /// </summary>
    public class KeyChain
	{
        private const string _KEY_FORMAT = "{0}-{1}";
        public static string productUniqueId = null;
#if UNITY_IPHONE || UNITY_STANDALONE_OSX
	
		[DllImport("__Internal")]
		private static extern string Load(string dataKey);

		/// <summary>
        /// Load
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
		public static string BindLoad(string dataKey)
		{
            if (string.IsNullOrEmpty(productUniqueId))
            {
                Debug.LogError("productUniqueId is null or empty");
                return null;
            }
            var loadData = Load(_GetUniqueKey(dataKey));
            Debug.Log($"Load data : {loadData} / data key : {_GetUniqueKey(dataKey)}");
            return loadData;
		}

        [DllImport("__Internal")]
        private static extern bool Save(string dataKey, string saveData);

		/// <summary>
        /// Save
        /// </summary>
        /// <param name="dataKey"></param>
        /// <param name="saveData"></param>
        /// <returns></returns>
		public static bool BindSave(string dataKey, string saveData)
		{
            if (string.IsNullOrEmpty(productUniqueId))
            {
                Debug.LogError("productUniqueId is null or empty");
                return false;
            }
            var isResult = Save(_GetUniqueKey(dataKey), saveData);
            Debug.Log($"Save data resul;t : {isResult} / data key : {_GetUniqueKey(dataKey)} / data : {saveData}");
            return isResult;
		}

        [DllImport("__Internal")]
        private static extern bool Delete(string dataKey);

		/// <summary>
        /// Delete
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
		public static bool BindDelete(string dataKey)
		{
            if (string.IsNullOrEmpty(productUniqueId))
            {
                Debug.LogError("productUniqueId is null or empty");
                return false;
            }
            var isResult = Delete(_GetUniqueKey(dataKey));
            Debug.Log($"Delete data result : {isResult} / data key : {_GetUniqueKey(dataKey)}");
            return isResult;
		}

        /// <summary>
        /// Get Unique Key
        /// </summary>
        /// <param name="dataKey"></param>
        /// <returns></returns>
        private static string _GetUniqueKey(string dataKey)
        {
            return string.Format(_KEY_FORMAT, dataKey, productUniqueId);
        }
	
#endif
	}
}
