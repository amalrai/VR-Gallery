package org.jeecg.modules.system.util;

import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.util.RedisUtil;
import org.jeecg.common.util.SpringContextHolder;
import org.jeecg.modules.system.service.ISysLanguageItemService;

import java.util.Map;

/**
 * @author : wangbinyu
 * @since : 2022/6/27
 * description :多语言消息处理类
 */
public class MessageUtil {
    private static final String LANG_KEY = "i18n:";
    public static String getMessage(String key, String local){
        RedisUtil redisUtil = SpringContextHolder.getBean(RedisUtil.class);
        String val = null;
        if(redisUtil.hasKey(LANG_KEY + local)){
            val = (String) redisUtil.hget(LANG_KEY + local, key);
        }
        if(StringUtils.isEmpty(val)){
            //loadFromDB
            ISysLanguageItemService service = SpringContextHolder.getBean(ISysLanguageItemService.class);
            Result<Map<String, String>> result = service.getDataByKey(local, null);
            if(result.isSuccess()){
                Map<String, String> data = result.getResult();
                if(data != null){
                    for (String s : data.keySet()) {
                        redisUtil.hset(LANG_KEY + local, s, data.get(s));
                    }
                    if(data.containsKey(key)){
                        return data.get(key);
                    }
                }
            }
            val = key;
        }
        return val;
    }
}
