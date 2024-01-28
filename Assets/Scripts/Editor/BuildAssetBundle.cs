using UnityEngine;
using UnityEditor;
using System.IO;
namespace YSFramework
{
    public class BuildAssetBundle : MonoBehaviour
    {
        [MenuItem("Tools/��AB��")]
        public static void BuildAB()
        {
            string dir = Application.streamingAssetsPath;    //����AB��·��������Ŀ¼�µ�StreamingAssets
            if (Directory.Exists(dir) == false)//����������ļ��У���ô�½�һ��
            {
                Directory.CreateDirectory(dir);
            }
            BuildPipeline.BuildAssetBundles(dir,
                BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
            AssetDatabase.Refresh();
            Debug.Log("������");
        }
    }
}