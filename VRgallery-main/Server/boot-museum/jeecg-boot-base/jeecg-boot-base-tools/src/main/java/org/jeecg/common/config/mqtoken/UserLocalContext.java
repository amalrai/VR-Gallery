package org.jeecg.common.config.mqtoken;


/**
 * 用户token上下文
 *
 * @author zyf
 */
public class UserLocalContext {

    private static ThreadLocal<String> userLocal = new ThreadLocal<String>();

    public UserLocalContext() {
    }

    public static String getLocal() {
        return userLocal.get();
    }

    public static void setLocal(String local) {
        userLocal.set(local);
    }
}
