package org.jeecg.modules.show.exhibits.util;

import org.jeecg.common.util.RedisUtil;
import org.jeecg.modules.show.exhibits.mapper.ShowExhibitsMapper;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

/**
 * @author : wangbinyu
 * @since : 2022/6/19
 * description :
 */
@Component
public class ExhibitsUtil {

    @Autowired
    private RedisUtil redisUtil;
    @Autowired
    private ShowExhibitsMapper showExhibitsMapper;

    public Integer getExhibitsNo(String roomId) {
        String seq = "seq:exhibits:" + roomId;
        Boolean isExist = redisUtil.hHasKey("sequences", seq);
        if (!isExist) {
            Integer lastId = showExhibitsMapper.getMaxNo(roomId);
            if (lastId != null && lastId > 0) {
                redisUtil.hputIfAbsent("sequences", seq, lastId.toString());
            }
        }
        return Long.valueOf(redisUtil.hincr("sequences", seq, 1L)).intValue();
    }
}
