package org.jeecg.modules.system.util;

import javax.servlet.http.HttpServletRequest;

/**
 * @author : wangbinyu
 * @since : 2023/3/26
 * description :
 */
public class HttpRequestUtil {
    public static String getRemoteHost(HttpServletRequest request){
        String ip = request.getHeader("x-forwarded-for");
        if(ip == null || ip.length() == 0 || "unknown".equalsIgnoreCase(ip)){
            ip = request.getHeader("proxy-client-ip");
        }
        if(ip == null || ip.length() == 0 || "unknown".equalsIgnoreCase(ip)){
            ip = request.getHeader("wl-proxy-client-ip");
        }
        if(ip == null || ip.length() == 0 || "unknown".equalsIgnoreCase(ip)){
            ip = request.getRemoteAddr();
        }
        if(ip.startsWith("\\")){
            ip = ip.substring(1);
        }
        return ip.equals("0:0:0:0:0:0:0:1")?"127.0.0.1":ip;
    }
}
