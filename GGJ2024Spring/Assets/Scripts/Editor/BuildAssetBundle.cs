using UnityEngine;
using UnityEditor;
using System.IO;
namespace YSFramework
{
    public class BuildAssetBundle : MonoBehaviour
    {
        [MenuItem("Tools/打AB包")]
        public static void BuildAB()
        {
            string dir = Application.streamingAssetsPath;    //定义AB包路径：工程目录下的StreamingAssets
            if (Directory.Exists(dir) == false)//如果不存在文件夹，那么新建一个
            {
                Directory.CreateDirectory(dir);
            }
            BuildPipeline.BuildAssetBundles(dir,
                BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
            AssetDatabase.Refresh();
            Debug.Log("打包完成");
        }
    }
}