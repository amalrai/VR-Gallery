using System.IO;
using UnityEditor;
using UnityEngine;

public class CreateAssetbundles
{
    [MenuItem("AssetsBundle/Build Window AssetBundles")]

    static void BuildAllWindowAssetBundles()//进行打包
    {
        string dir = Application.dataPath+"/AssetsBundle/Window";
        Debug.Log(dir);
        //判断该目录是否存在
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);//在工程下创建AssetBundles目录
        }
        //参数一为打包到哪个路径，参数二压缩选项  参数三 平台的目标
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);
    }


    [MenuItem("AssetsBundle/Build Android AssetBundles")]

    static void BuildAllAndroidAssetBundles()//进行打包
    {
        string dir = Application.dataPath + "/AssetsBundle/Android";
        Debug.Log(dir);
        //判断该目录是否存在
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);//在工程下创建AssetBundles目录
        }
        //参数一为打包到哪个路径，参数二压缩选项  参数三 平台的目标
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
    }

    [MenuItem("AssetsBundle/Build IOS AssetBundles")]

    static void BuildAllIOSAssetBundles()//进行打包
    {
        string dir = Application.dataPath + "/AssetsBundle/IOS";
        Debug.Log(dir);
        //判断该目录是否存在
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);//在工程下创建AssetBundles目录
        }
        //参数一为打包到哪个路径，参数二压缩选项  参数三 平台的目标
        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);
    }
}
