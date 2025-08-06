package org.jeecg.common.system.util;

import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.util.RedisUtil;
import org.jeecg.common.util.SpringContextHolder;

/**
 * @author : wangbinyu
 * @since : 2022/6/27
 * description :多语言消息处理类
 */
public class MessageCacheUtil {
    private static final String LANG_KEY = "i18n:";

    public static String getMessage(String key, String local) {
        RedisUtil redisUtil = SpringContextHolder.getBean(RedisUtil.class);
        String val = null;
        if (redisUtil.hasKey(LANG_KEY + local)) {
            val = (String) redisUtil.hget(LANG_KEY + local, key);
        }
        if (StringUtils.isEmpty(val)) {
            val = key;
        }
        return val;
    }
}
