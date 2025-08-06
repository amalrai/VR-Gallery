package org.jeecg.modules.show.perform.service.impl;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.perform.entity.ShowPerform;
import org.jeecg.modules.show.perform.mapper.ShowPerformMapper;
import org.jeecg.modules.show.perform.service.IShowPerformService;
import org.jeecg.modules.show.user.entity.ShowUser;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;

import java.util.List;

/**
 * @Description: 演出管理
 * @Author: jeecg-boot
 * @Date:   2021-10-16
 * @Version: V1.0
 */
@Service
public class ShowPerformServiceImpl extends ServiceImpl<ShowPerformMapper, ShowPerform> implements IShowPerformService {

    @Autowired
    private ShowPerformMapper mapper;

    @Override
    public Result<?> doAdd(ShowPerform entity) {
        entity.setState(0);
        entity.setViewCount(0);
        entity.setViewTime(0);
        mapper.insert(entity);
        return Result.OK("保存成功");
    }

    @Override
    public Result<?> doUpdate(ShowPerform entity) {
        entity.setState(null);
        entity.setViewCount(null);
        entity.setViewTime(null);
        mapper.updateById(entity);
        return Result.OK("保存成功");
    }

    @Override
    public Result<List<ShowPerform>> getPerformList() {
        List<ShowPerform> list = mapper.selectList(new QueryWrapper<ShowPerform>().lambda().orderByDesc(ShowPerform::getStartTime));
        return Result.OK(list);
    }
}
