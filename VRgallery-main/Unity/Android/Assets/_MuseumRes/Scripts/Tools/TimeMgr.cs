using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMgr : UnitySingleton<TimeMgr> {


    public delegate void Interval();
    public Dictionary<Interval, float> mDicinterval = new Dictionary<Interval, float>();

    public void AddInterval(Interval interval,float time)
    {
        if (null != interval)
        mDicinterval[interval] = Time.time + time;
    }

    public void RemoveInterval(Interval interval)
    {
         if (null != interval)
         {
             if (mDicinterval.ContainsKey(interval))
             {
                 mDicinterval.Remove(interval);
             }
         }
    }


    void Update()
    {
        if(mDicinterval.Count > 0)
        {
            List<Interval> remove = new List<Interval>();
            foreach(KeyValuePair<Interval,float> KeyValue in mDicinterval)
            {
                if (KeyValue.Value <= Time.time)
                {
                    remove.Add(KeyValue.Key);
                }
            }
            for (int i = 0; i < remove.Count;i++ )
            {
                remove[i]();
                mDicinterval.Remove(remove[i]);
            }
        }

    }

}
