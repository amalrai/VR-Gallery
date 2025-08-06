package org.jeecg.modules.show.vipfee.service.impl;

import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONObject;
import com.baomidou.mybatisplus.core.conditions.query.LambdaQueryWrapper;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.conditions.update.UpdateWrapper;
import com.baomidou.mybatisplus.extension.toolkit.SqlHelper;
import org.apache.commons.collections4.CollectionUtils;
import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.constant.CommonConstant;
import org.jeecg.modules.show.user.entity.ShowUserVo2;
import org.jeecg.modules.show.vipfee.entity.ShowVipFee;
import org.jeecg.modules.show.vipfee.entity.ShowVipFeeVo;
import org.jeecg.modules.show.vipfee.entity.ShowVipFeeVo2;
import org.jeecg.modules.show.vipfee.mapper.ShowVipFeeMapper;
import org.jeecg.modules.show.vipfee.service.IShowVipFeeService;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.springframework.transaction.annotation.Transactional;

import java.io.Serializable;
import java.util.Collection;
import java.util.List;

/**
 * @Description: 付费管理
 * @Author: jeecg-boot
 * @Date:   2023-04-02
 * @Version: V1.0
 */
@Service
public class ShowVipFeeServiceImpl extends ServiceImpl<ShowVipFeeMapper, ShowVipFee> implements IShowVipFeeService {
    @Autowired
    private ShowVipFeeMapper mapper;

    @Override
    @Transactional(rollbackFor = Exception.class)
    public boolean save(ShowVipFee entity) {
        entity.setDelFlag(0);
        if(entity.getStatus() == null){
            entity.setStatus(0);
        }
        // 刷新房间免费付费状态
        if(entity.getStatus() == 1){

        }
        return SqlHelper.retBool(mapper.insert(entity));
    }

    @Override
    public boolean removeById(Serializable id) {
        ShowVipFee entity = new ShowVipFee();
        entity.setId((String) id);
        entity.setDelFlag(1);
        mapper.updateById(entity);
        return true;
    }

    @Override
    public boolean removeByIds(Collection<? extends Serializable> idList) {
        mapper.update(null, new UpdateWrapper<ShowVipFee>().lambda()
                .set(ShowVipFee::getDelFlag, 1)
                .in(ShowVipFee::getId, idList));
        return true;
    }

    @Override
    public Result<?> queryByMuseumId(String museumId) {
        if(StringUtils.isEmpty(museumId)){
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        List<ShowVipFee> vipFeeList = mapper.selectList(new QueryWrapper<ShowVipFee>().lambda()
                .eq(ShowVipFee::getShowMuseumId, museumId)
                .eq(ShowVipFee::getDelFlag, 0)
                .eq(ShowVipFee::getStatus, 1)
                .orderByAsc(ShowVipFee::getTimeLimit));
        for (ShowVipFee vipFee : vipFeeList) {
            vipFee.setDiscountCode("");
        }
        List<ShowVipFeeVo> data = JSONObject.parseArray(JSON.toJSONString(vipFeeList), ShowVipFeeVo.class);
        return Result.OK(data).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
    }
    @Override
    public Result<?> queryByMuseumIdV2(String platform, String museumId, String roomId) {
        if(StringUtils.isEmpty(platform) || StringUtils.isEmpty(museumId) || StringUtils.isEmpty(roomId)){
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        List<ShowVipFee> vipFeeList = mapper.selectList(new QueryWrapper<ShowVipFee>().lambda()
                .eq(ShowVipFee::getShowMuseumId, museumId)
                .eq(ShowVipFee::getShowRoomId, roomId)
                .eq(ShowVipFee::getPlatform, platform)
                .eq(ShowVipFee::getDelFlag, 0)
                .eq(ShowVipFee::getStatus, 1)
                .orderByAsc(ShowVipFee::getTimeLimit));
        List<ShowVipFeeVo2> data = null;
        if(CollectionUtils.isNotEmpty(vipFeeList)){
            for (ShowVipFee vipFee : vipFeeList) {
                vipFee.setDiscountCode("");
            }
            data = JSONObject.parseArray(JSON.toJSONString(vipFeeList), ShowVipFeeVo2.class);
        }
        return Result.OK(data).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
    }
}
