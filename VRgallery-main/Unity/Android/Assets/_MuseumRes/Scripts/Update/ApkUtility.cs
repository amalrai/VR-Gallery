using System;
using UnityEngine;

namespace Cn.Ktgames.Util.Android
{
    /// <summary>
    /// APK����
    /// </summary>
    public class ApkUtility
    {
        /// <summary>
        /// ��װʧ��Apk������
        /// </summary>
        public const int InstallFailWithFileNon = 10001;
        /// <summary>
        /// ��װʧ���û�ȡ����Ȩ
        /// </summary>
        public const int InstallFailWithUserCancelInstallPermission = 10002;
        /// <summary>
        /// ��װ���
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
        /// ��ʼ�����������հ�װ�ص�����ȵ��ô˽ӿ�
        /// </summary>
        public static void Init()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            UtillityClass.CallStatic("Init",new ApkUtilityListener());
#endif
        }

        /// <summary>
        /// ��װApk
        /// </summary>
        /// <param name="filePath"></param>
        public static void InstallApk(string filePath)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            UtillityClass.CallStatic("InstallApk", GetActivity(), filePath);
#endif
        }
        /// <summary>
        /// ����Ȩ�޵�����Ϣ
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
        /// ��ȡ�Ƿ��а�װȨ��
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
        /// ��ȡActivity
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
