package org.jeecg.modules.show.log.service;

import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.log.entity.ShowUserClickLog;
import com.baomidou.mybatisplus.extension.service.IService;

/**
 * @Description: show_user_click_log
 * @Author: jeecg-boot
 * @Date:   2023-03-26
 * @Version: V1.0
 */
public interface IShowUserClickLogService extends IService<ShowUserClickLog> {

    Result<?> uploadClickLog(String loginLogId, String exhibitsId, Integer introductionCount, Integer linkCount, Integer videoCount, Integer voiceCount, Integer shopCount);
}
