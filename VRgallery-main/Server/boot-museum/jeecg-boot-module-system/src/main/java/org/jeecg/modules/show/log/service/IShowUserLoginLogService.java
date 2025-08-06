package org.jeecg.modules.show.log.service;

import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.log.entity.ShowUserLoginLog;
import com.baomidou.mybatisplus.extension.service.IService;

/**
 * @Description: 登录日志
 * @Author: jeecg-boot
 * @Date:   2023-03-26
 * @Version: V1.0
 */
public interface IShowUserLoginLogService extends IService<ShowUserLoginLog> {

    Result<?> uploadLoginLog(String userId, String roomId, String museumId, String loginIp);
}
