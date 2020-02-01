using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    public class CreateAssetBundles
    {
        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAllAssetBundles()
        {
            Caching.ClearCache();
            Debug.Log("building");
            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/AssetBundles/Android", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
            //BuildPipeline.BuildAssetBundles("Assets/StreamingAssets/AssetBundles/iOS", BuildAssetBundleOptions.None, BuildTarget.iOS);
        }
    }
}