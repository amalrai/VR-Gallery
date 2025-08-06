package org.jeecg.modules.show.data.service.impl;

import com.baomidou.mybatisplus.core.conditions.update.UpdateWrapper;
import com.baomidou.mybatisplus.core.toolkit.IdWorker;
import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.data.entity.ShowData;
import org.jeecg.modules.show.data.mapper.ShowDataMapper;
import org.jeecg.modules.show.data.service.IShowDataService;
import org.jeecg.modules.show.user.entity.ShowUser;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;

import java.util.Date;

/**
 * @Description: 数据管理
 * @Author: jeecg-boot
 * @Date:   2021-10-16
 * @Version: V1.0
 */
@Service
public class ShowDataServiceImpl extends ServiceImpl<ShowDataMapper, ShowData> implements IShowDataService {

    @Autowired
    private ShowDataMapper mapper;

    @Override
    public Result<String> saveViewTime(String id, ShowUser user, int viewTime) {
        ShowData data = new ShowData();
        data.setViewTime(viewTime);
        if(StringUtils.isEmpty(id)){
            id = String.valueOf(IdWorker.getId(data));
            data.setId(id);
            data.setStartTime(new Date());
            data.setShowUserId(user.getId());
            data.setShowUserNickName(user.getNickName());
            data.setShowUserSex(user.getSex());
            data.setShowUserPhone(user.getPhone());
            data.setShowUserEmail(user.getEmail());
            data.setShowUserRegisterTime(user.getRegisterTime());
            mapper.insert(data);
        }else{
            data.setId(id);
            data.setEndTime(new Date());
            mapper.updateById(data);
        }
        return Result.OK(id);
    }

    @Override
    public Result<String> updateUserInfo(ShowUser user) {
        mapper.update(null, new UpdateWrapper<ShowData>().lambda()
                .set(ShowData::getShowUserPhone, user.getPhone())
                .set(ShowData::getShowUserEmail, user.getEmail())
                .set(ShowData::getShowUserSex, user.getSex())
                .set(ShowData::getShowUserNickName, user.getNickName())
                .set(ShowData::getShowUserRegisterTime, user.getRegisterTime())
                .eq(ShowData::getShowUserId, user.getId()));
        return Result.OK();
    }
}
