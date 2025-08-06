package org.jeecg.modules.show.log.service.impl;

import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.modules.show.exhibits.entity.ShowExhibits;
import org.jeecg.modules.show.exhibits.mapper.ShowExhibitsMapper;
import org.jeecg.modules.show.log.entity.ShowUserClickLog;
import org.jeecg.modules.show.log.mapper.ShowUserClickLogMapper;
import org.jeecg.modules.show.log.service.IShowUserClickLogService;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;

import java.util.Date;

/**
 * @Description: show_user_click_log
 * @Author: jeecg-boot
 * @Date:   2023-03-26
 * @Version: V1.0
 */
@Service
public class ShowUserClickLogServiceImpl extends ServiceImpl<ShowUserClickLogMapper, ShowUserClickLog> implements IShowUserClickLogService {
    @Autowired
    private ShowUserClickLogMapper clickLogMapper;
    @Autowired
    private ShowExhibitsMapper exhibitsMapper;
    @Override
    public Result<?> uploadClickLog(String loginLogId, String exhibitsId, Integer introductionCount, Integer linkCount, Integer videoCount, Integer voiceCount, Integer shopCount) {
        if(StringUtils.isBlank(loginLogId) || StringUtils.isBlank(exhibitsId)){
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        ShowUserClickLog clickLog = clickLogMapper.queryByLoginId(loginLogId, exhibitsId);
        if(clickLog != null){
            clickLog.setIntroductionCount(ifNullReturnZero(clickLog.getIntroductionCount()) + ifNullReturnZero(introductionCount));
            clickLog.setLinkCount(ifNullReturnZero(clickLog.getLinkCount()) + ifNullReturnZero(linkCount));
            clickLog.setVideoCount(ifNullReturnZero(clickLog.getVideoCount()) + ifNullReturnZero(videoCount));
            clickLog.setVoiceCount(ifNullReturnZero(clickLog.getVoiceCount()) + ifNullReturnZero(voiceCount));
            clickLog.setShopCount(ifNullReturnZero(clickLog.getShopCount()) + ifNullReturnZero(shopCount));
            clickLog.setUpdateTime(new Date());
            clickLogMapper.updateById(clickLog);
        }else{
            ShowExhibits exhibits = exhibitsMapper.selectById(exhibitsId);
            if(exhibits == null){
                return Result.error(MessageUtil.getMessage("展品不存在", UserLocalContext.getLocal()));
            }
            clickLog = new ShowUserClickLog();
            clickLog.setShowUserLoginLogId(loginLogId);
            clickLog.setShowExhibitsId(exhibitsId);
            clickLog.setShowExhibitsNo(exhibits.getExhibitsNo());
            clickLog.setIntroductionCount(ifNullReturnZero(introductionCount));
            clickLog.setLinkCount(ifNullReturnZero(linkCount));
            clickLog.setVideoCount(ifNullReturnZero(videoCount));
            clickLog.setVoiceCount(ifNullReturnZero(voiceCount));
            clickLog.setShopCount(ifNullReturnZero(shopCount));
            clickLog.setCreateTime(new Date());
            clickLogMapper.insert(clickLog);
        }
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }
    private Integer ifNullReturnZero(Integer temp){
        if(temp == null){
            return 0;
        }
        return temp;
    }
}
