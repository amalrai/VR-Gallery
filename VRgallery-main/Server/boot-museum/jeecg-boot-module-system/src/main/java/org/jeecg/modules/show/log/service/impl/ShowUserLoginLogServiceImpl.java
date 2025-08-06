package org.jeecg.modules.show.log.service.impl;

import com.baomidou.mybatisplus.core.toolkit.IdWorker;
import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.modules.show.log.entity.ShowUserLoginLog;
import org.jeecg.modules.show.log.mapper.ShowUserLoginLogMapper;
import org.jeecg.modules.show.log.service.IShowUserLoginLogService;
import org.jeecg.modules.show.user.entity.ShowUser;
import org.jeecg.modules.show.user.mapper.ShowUserMapper;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;

import java.util.Date;

/**
 * @Description: 登录日志
 * @Author: jeecg-boot
 * @Date:   2023-03-26
 * @Version: V1.0
 */
@Service
public class ShowUserLoginLogServiceImpl extends ServiceImpl<ShowUserLoginLogMapper, ShowUserLoginLog> implements IShowUserLoginLogService {

    @Autowired
    private ShowUserMapper userMapper;
    @Autowired
    private ShowUserLoginLogMapper loginLogMapper;

    @Override
    public Result<?> uploadLoginLog(String userId, String roomId, String museumId, String loginIp) {
        if(StringUtils.isBlank(userId) || StringUtils.isBlank(roomId) || StringUtils.isBlank(museumId)){
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        ShowUser user = userMapper.selectById(userId);
        if(user == null){
            return Result.error(MessageUtil.getMessage("用户不存在", UserLocalContext.getLocal()));
        }
        String loginId = String.valueOf(IdWorker.getId());
        Date loginTime = new Date();
        ShowUserLoginLog userLoginLog = new ShowUserLoginLog();
        userLoginLog.setId(loginId);
        userLoginLog.setShowUserId(user.getId());
        userLoginLog.setShowUserEmail(user.getEmail());
        userLoginLog.setShowUserNickName(user.getNickName());
        userLoginLog.setShowUserRealName(user.getRealName());
        userLoginLog.setShowMuseumId(museumId);
        userLoginLog.setShowRoomId(roomId);
        userLoginLog.setShowAppId(user.getShowAppId());
        userLoginLog.setLoginIp(loginIp);
        userLoginLog.setLoginTime(loginTime);
        loginLogMapper.insert(userLoginLog);
        return Result.OK(loginId).success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }
}
