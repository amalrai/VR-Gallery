package org.jeecg.modules.show.exhibits.service.impl;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.conditions.update.LambdaUpdateWrapper;
import com.baomidou.mybatisplus.core.conditions.update.UpdateWrapper;
import com.baomidou.mybatisplus.core.toolkit.IdWorker;
import org.apache.commons.lang.ObjectUtils;
import org.apache.commons.lang.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.constant.CommonConstant;
import org.jeecg.modules.show.exhibits.entity.ShowExhibits;
import org.jeecg.modules.show.exhibits.mapper.ShowExhibitsMapper;
import org.jeecg.modules.show.exhibits.service.IShowExhibitsService;
import org.jeecg.modules.show.exhibits.util.ExhibitsUtil;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.springframework.transaction.annotation.Transactional;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * @Description: 展品管理
 * @Author: jeecg-boot
 * @Date:   2022-06-15
 * @Version: V1.0
 */
@Service
public class ShowExhibitsServiceImpl extends ServiceImpl<ShowExhibitsMapper, ShowExhibits> implements IShowExhibitsService {

    @Autowired
    private ExhibitsUtil exhibitsUtil;
    @Autowired
    private ShowExhibitsMapper mapper;
    @Override
    public Result<?> batchAddExhibits(ShowExhibits exhibits) {
        if(exhibits.getCount() == null || exhibits.getCount() < 0){
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        List<ShowExhibits> list = new ArrayList<>();
        for (int i = 0; i < exhibits.getCount(); i++) {
            ShowExhibits data = new ShowExhibits();
            data.setId(String.valueOf(IdWorker.getId()));
            data.setShowRoomId(exhibits.getShowRoomId());
            data.setExhibitsNo(exhibitsUtil.getExhibitsNo(exhibits.getShowRoomId()));
            //默认禁用
            data.setStatus(1);
            //默认免费
            data.setAuth(0);
            data.setVersion(0);
            list.add(data);
        }
        this.saveBatch(list);
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> editExhibits(ShowExhibits exhibits) {
        ShowExhibits oldExhibits = this.baseMapper.selectById(exhibits.getId());
        this.compareAndSetExhibits(oldExhibits, exhibits);
        this.update(this.forceUpdate(exhibits).eq(ShowExhibits::getId, exhibits.getId()));
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    @Transactional(rollbackFor = Exception.class)
    public Result<?> replaceExhibits(String exhibitsId, String replaceExhibitsId) {
        ShowExhibits exhibits = this.baseMapper.selectById(exhibitsId);
        ShowExhibits replaceExhibits = this.baseMapper.selectById(replaceExhibitsId);
        if(exhibits == null || replaceExhibits == null){
            return Result.error(MessageUtil.getMessage("替换失败，记录已不存在", UserLocalContext.getLocal()));
        }
        //交换ID
        this.update(this.forceUpdate(exhibits).eq(ShowExhibits::getId, replaceExhibitsId));
        this.update(this.forceUpdate(replaceExhibits).eq(ShowExhibits::getId, exhibitsId));
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> batchUpdateStatus(List<String> idList, Integer status) {
        this.update(new UpdateWrapper<ShowExhibits>().lambda()
                .set(ShowExhibits::getStatus, status)
                .setSql("version=version+1")
                .in(ShowExhibits::getId, idList));
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> batchUpdateAuth(List<String> idList, Integer auth) {
        this.update(new UpdateWrapper<ShowExhibits>().lambda()
                .set(ShowExhibits::getAuth, auth)
                .setSql("version=version+1")
                .in(ShowExhibits::getId, idList));
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> batchClear(List<String> idList) {
        ShowExhibits exhibits = new ShowExhibits();
        exhibits.setStatus(1);
        exhibits.setAuth(0);
        this.update(this.forceUpdate(exhibits).in(ShowExhibits::getId, idList));
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> clear(String id) {
        ShowExhibits exhibits = new ShowExhibits();
        exhibits.setStatus(1);
        exhibits.setAuth(0);
        this.update(this.forceUpdate(exhibits).eq(ShowExhibits::getId, id));
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<List<ShowExhibits>> getExhibitsList(String roomId) {
        Result<List<ShowExhibits>> result = Result.OK();
        if(StringUtils.isEmpty(roomId)){
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        List<ShowExhibits> exhibitsList = mapper.selectList(new QueryWrapper<ShowExhibits>().lambda()
                .eq(ShowExhibits::getShowRoomId, roomId)
                .orderByAsc(ShowExhibits::getExhibitsNo));
        result.setResult(exhibitsList);
        return result;
    }

    private void compareAndSetExhibits(ShowExhibits oldExhibits, ShowExhibits exhibits){
        if(StringUtils.isNotBlank(exhibits.getName())){
            exhibits.setName(exhibits.getName().trim());
        }
        if(ObjectUtils.notEqual(oldExhibits.getWebLink(), exhibits.getWebLink())){
            if(StringUtils.isEmpty(exhibits.getWebLink())){
                exhibits.setWebLinkTime(null);
            }else{
                exhibits.setWebLinkTime(new Date());
            }
        }
        if(ObjectUtils.notEqual(oldExhibits.getVideoLink(), exhibits.getVideoLink())){
            if(StringUtils.isEmpty(exhibits.getVideoLink())){
                exhibits.setVideoLinkTime(null);
            }else{
                exhibits.setVideoLinkTime(new Date());
            }
        }
        if(StringUtils.isBlank(exhibits.getMainGraphUrl())){
            exhibits.setMainGraphEncodeUrl(null);
        }
        if(StringUtils.isBlank(exhibits.getVideoUrl())){
            exhibits.setVideoEncodeUrl(null);
        }
        if(StringUtils.isBlank(exhibits.getIntroductionImageUrl())){
            exhibits.setIntroductionImageEncodeUrl(null);
        }
        if(StringUtils.isBlank(exhibits.getAnimationThumbnailUrl())){
            exhibits.setAnimationThumbnailEncodeUrl(null);
        }
        if(StringUtils.isBlank(exhibits.getFrameAnimationUrl())){
            exhibits.setFrameAnimationEncodeUrl(null);
        }
    }
    private LambdaUpdateWrapper<ShowExhibits> forceUpdate(ShowExhibits exhibits){
        return new UpdateWrapper<ShowExhibits>().lambda()
                .set(ShowExhibits::getName, exhibits.getName())
                .set(ShowExhibits::getStatus, exhibits.getStatus())
                .set(ShowExhibits::getAuth, exhibits.getAuth())
                .set(ShowExhibits::getText, exhibits.getText())
                .set(ShowExhibits::getMainGraphUrl, exhibits.getMainGraphUrl())
                .set(ShowExhibits::getMainGraphName, exhibits.getMainGraphName())
                .set(ShowExhibits::getMainGraphSize, exhibits.getMainGraphSize())
                .set(ShowExhibits::getMainGraphTime, exhibits.getMainGraphTime())
                .set(ShowExhibits::getIntroductionImageUrl, exhibits.getIntroductionImageUrl())
                .set(ShowExhibits::getIntroductionImageName, exhibits.getIntroductionImageName())
                .set(ShowExhibits::getIntroductionImageSize, exhibits.getIntroductionImageSize())
                .set(ShowExhibits::getIntroductionImageTime, exhibits.getIntroductionImageTime())
                .set(ShowExhibits::getVideoUrl, exhibits.getVideoUrl())
                .set(ShowExhibits::getVideoName, exhibits.getVideoName())
                .set(ShowExhibits::getVideoSize, exhibits.getVideoSize())
                .set(ShowExhibits::getVideoTime, exhibits.getVideoTime())
                .set(ShowExhibits::getVoiceUrl, exhibits.getVoiceUrl())
                .set(ShowExhibits::getVoiceName, exhibits.getVoiceName())
                .set(ShowExhibits::getVoiceSize, exhibits.getVoiceSize())
                .set(ShowExhibits::getVoiceTime, exhibits.getVoiceTime())
                .set(ShowExhibits::getVideoLink, exhibits.getVideoLink())
                .set(ShowExhibits::getVideoLinkTime, exhibits.getVideoLinkTime())
                .set(ShowExhibits::getWebLink, exhibits.getWebLink())
                .set(ShowExhibits::getWebLinkTime, exhibits.getWebLinkTime())
                .set(ShowExhibits::getAnimationThumbnailUrl, exhibits.getAnimationThumbnailUrl())
                .set(ShowExhibits::getAnimationThumbnailName, exhibits.getAnimationThumbnailName())
                .set(ShowExhibits::getAnimationThumbnailSize, exhibits.getAnimationThumbnailSize())
                .set(ShowExhibits::getAnimationThumbnailTime, exhibits.getAnimationThumbnailTime())
                .set(ShowExhibits::getFrameAnimationUrl, exhibits.getFrameAnimationUrl())
                .set(ShowExhibits::getFrameAnimationName, exhibits.getFrameAnimationName())
                .set(ShowExhibits::getFrameAnimationSize, exhibits.getFrameAnimationSize())
                .set(ShowExhibits::getFrameAnimationTime, exhibits.getFrameAnimationTime())
                .set(ShowExhibits::getMainGraphEncodeUrl, exhibits.getMainGraphEncodeUrl())
                .set(ShowExhibits::getIntroductionImageEncodeUrl, exhibits.getIntroductionImageEncodeUrl())
                .set(ShowExhibits::getVideoEncodeUrl, exhibits.getVideoEncodeUrl())
                .set(ShowExhibits::getAnimationThumbnailEncodeUrl, exhibits.getAnimationThumbnailEncodeUrl())
                .set(ShowExhibits::getFrameAnimationEncodeUrl, exhibits.getFrameAnimationEncodeUrl())
                .setSql("version=version+1");
    }
}
