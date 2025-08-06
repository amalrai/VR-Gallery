using System;
using UnityEngine;

namespace Cn.Ktgames.Util.Android
{
    /// <summary>
    /// APK工具
    /// </summary>
    public class ApkUtility
    {
        /// <summary>
        /// 安装失败Apk不存在
        /// </summary>
        public const int InstallFailWithFileNon = 10001;
        /// <summary>
        /// 安装失败用户取消授权
        /// </summary>
        public const int InstallFailWithUserCancelInstallPermission = 10002;
        /// <summary>
        /// 安装结果
        /// </summary>
        public static Action<int, string, string> onInstallResult;

        class ApkUtilityListener : AndroidJavaProxy
        {
            public ApkUtilityListener() : base(ClassName + "$Listener") { }
            void onInstallResult(int code, string path, string message)
            {
                ApkUtility.onInstallResult?.Invoke(code, path, message);
            }
        }
        private const string PackageName = "cn.ktgames.utility.android";
        private const string ClassName = PackageName +".ApkUtility";

        private static AndroidJavaObject activity;
        private static AndroidJavaClass utillityClass; 
        private static AndroidJavaClass UtillityClass
        {
            get
            {
                if(utillityClass==null)
                {
                    utillityClass = new AndroidJavaClass(ClassName);
                }
                return utillityClass;
            }
        }
        /// <summary>
        /// 初始化，如果想接收安装回调结果先调用此接口
        /// </summary>
        public static void Init()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            UtillityClass.CallStatic("Init",new ApkUtilityListener());
#endif
        }

        /// <summary>
        /// 安装Apk
        /// </summary>
        /// <param name="filePath"></param>
        public static void InstallApk(string filePath)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            UtillityClass.CallStatic("InstallApk", GetActivity(), filePath);
#endif
        }
        /// <summary>
        /// 设置权限弹窗信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="applyButton"></param>
        /// <param name="cancelButton"></param>
        public static void SetApkPermissionInfo(string title, string applyButton, string cancelButton)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            UtillityClass.CallStatic("SetApkPermissionInfo", title, applyButton, cancelButton);
#endif
        }
        /// <summary>
        /// 获取是否有安装权限
        /// </summary>
        public static bool GetInstallPermission()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return UtillityClass.CallStatic<bool>("GetInstallPermission", GetActivity());
#else
            return false;
#endif
        }
        /// <summary>
        /// 获取Activity
        /// </summary>
        /// <returns></returns>
        private static AndroidJavaObject GetActivity()
        {
            if (activity==null)
            {
                var UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                activity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            }
            return activity;
        }
    }
}
