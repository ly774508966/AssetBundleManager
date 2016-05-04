using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MAssets
{
    public class AssetBundleManager : MonoBehaviour
    {
        public static string ServerUrl = "";
        private static List<AssetBundleOperation.Load> _InProgressOperation = new List<AssetBundleOperation.Load>();

        public static AssetBundleOperation.LoadAsset LoadAssetAsync(string assetBundleName, string assetName, System.Type type)
        {
            AssetBundleOperation.LoadAsset operation = null;
            operation = new AssetBundleOperation.LoadAssetFull(assetBundleName, assetName, type);
            _InProgressOperation.Add(operation);
            return operation;
        }
    }
}
