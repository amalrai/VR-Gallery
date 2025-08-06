package org.jeecg.modules.show.museum.service.impl;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.apache.commons.lang.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.constant.CommonConstant;
import org.jeecg.modules.show.museum.entity.ShowMuseum;
import org.jeecg.modules.show.museum.mapper.ShowMuseumMapper;
import org.jeecg.modules.show.museum.service.IShowMuseumService;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * @Description: show_museum
 * @Author: jeecg-boot
 * @Date: 2022-10-24
 * @Version: V1.0
 */
@Service
public class ShowMuseumServiceImpl extends ServiceImpl<ShowMuseumMapper, ShowMuseum> implements IShowMuseumService {

    @Autowired
    private ShowMuseumMapper mapper;

    @Override
    public Result<List<ShowMuseum>> getMuseumList(String appId) {
        Result<List<ShowMuseum>> result = Result.OK();
        if (StringUtils.isEmpty(appId)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        List<ShowMuseum> list = mapper.selectList(new QueryWrapper<ShowMuseum>().lambda()
                .eq(ShowMuseum::getShowAppId, appId)
                .orderByAsc(ShowMuseum::getId));
        result.setResult(list);
        return result;
    }
}
