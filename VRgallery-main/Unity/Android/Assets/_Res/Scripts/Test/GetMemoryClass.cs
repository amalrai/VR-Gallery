using System.Collections;
using System.Text;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GetMemoryClass : MonoBehaviour
{
    private long avaliableMb;
    public Text ui;

    void Start()
    {
        //获取当前系统
        //SystemInfo.operatingSystem;
    }

    void Update()
    {
        #region 检测内存是否溢出

        GetMemoryStatus();

        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_INFO
    {
        public uint dwLength;

        public uint dwMemoryLoad;

        //系统内存总量
        public ulong dwTotalPhys;

        //系统可用内存
        public ulong dwAvailPhys;
        public ulong dwTotalPageFile;
        public ulong dwAvailPageFile;
        public ulong dwTotalVirtual;
        public ulong dwAvailVirtual;
    }

    [DllImport("kernel32")]
    public static extern void GlobalMemoryStatus(ref MEMORY_INFO meminfo);
    //  [DllImport("User32")]
    //  public static extern void GetWindowThreadProgressld (IntPtr hwnd,out int id);

    private void GetMemoryStatus()
    {
        MEMORY_INFO MemInfo;
        MemInfo = new MEMORY_INFO();
        GlobalMemoryStatus(ref MemInfo);

        avaliableMb = Convert.ToInt64(MemInfo.dwAvailPhys.ToString()) / 1024 / 1024;
        ui.text = "FreeMemory:" + Convert.ToString(avaliableMb) + " MB";
        print("FreeMemory:" + Convert.ToString(avaliableMb) + " MB");
        if (avaliableMb < 200)
        {
            Debug.Log("内存不足！");
            //弹出内存警告
        }
        else
        {
            Debug.Log("可以使用");
            //自动取消内存警告
            Debug.Log(Environment.WorkingSet.ToString());

        }
    }
}