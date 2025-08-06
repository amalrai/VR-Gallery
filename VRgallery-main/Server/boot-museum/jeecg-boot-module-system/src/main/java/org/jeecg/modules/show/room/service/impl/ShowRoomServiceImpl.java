package org.jeecg.modules.show.room.service.impl;

import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONObject;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.conditions.update.UpdateWrapper;
import com.baomidou.mybatisplus.core.toolkit.IdWorker;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.apache.commons.lang.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.constant.CommonConstant;
import org.jeecg.modules.show.exhibits.entity.ShowExhibits;
import org.jeecg.modules.show.exhibits.service.IShowExhibitsService;
import org.jeecg.modules.show.exhibits.util.ExhibitsUtil;
import org.jeecg.modules.show.room.entity.ShowRoom;
import org.jeecg.modules.show.room.entity.ShowRoomVo;
import org.jeecg.modules.show.room.entity.ShowRoomVo2;
import org.jeecg.modules.show.room.mapper.ShowRoomMapper;
import org.jeecg.modules.show.room.service.IShowRoomService;
import org.jeecg.modules.show.vipfee.entity.ShowVipFeeVo2;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.Collection;
import java.util.List;

/**
 * @Description: show_room
 * @Author: jeecg-boot
 * @Date: 2022-06-15
 * @Version: V1.0
 */
@Service
public class ShowRoomServiceImpl extends ServiceImpl<ShowRoomMapper, ShowRoom> implements IShowRoomService {
    @Autowired
    private ShowRoomMapper mapper;
    @Autowired
    private IShowExhibitsService showExhibitsService;
    @Autowired
    private ExhibitsUtil exhibitsUtil;

    @Transactional(rollbackFor = Exception.class)
    @Override
    public boolean save(ShowRoom entity) {
        entity.setId(String.valueOf(IdWorker.getId()));
        entity.setDelFlag(0);
        // 新增房间默认免费
        entity.setFreeFlag(1);
        mapper.insert(entity);
        //创建房间
        List<ShowExhibits> list = new ArrayList<>();
        for (int i = 0; i < 50; i++) {
            ShowExhibits data = new ShowExhibits();
            data.setId(String.valueOf(IdWorker.getId()));
            data.setShowRoomId(entity.getId());
            data.setExhibitsNo(exhibitsUtil.getExhibitsNo(entity.getId()));
            data.setName(data.getExhibitsNo().toString());
            //默认禁用
            data.setStatus(1);
            //默认免费
            data.setAuth(0);
            data.setVersion(0);
            list.add(data);
        }
        showExhibitsService.saveBatch(list);
        return true;
    }

    @Override
    public boolean removeById(Serializable id) {
        ShowRoom entity = new ShowRoom();
        entity.setId((String) id);
        entity.setDelFlag(1);
        mapper.updateById(entity);
        return true;
    }

    @Override
    public boolean removeByIds(Collection<? extends Serializable> idList) {
        mapper.update(null, new UpdateWrapper<ShowRoom>().lambda()
                .set(ShowRoom::getDelFlag, 1)
                .in(ShowRoom::getId, idList));
        return true;
    }

    @Override
    public Result<List<ShowRoomVo>> getRoomList(String museumId) {
        Result<List<ShowRoomVo>> result = Result.OK();
        if (StringUtils.isEmpty(museumId)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        List<ShowRoom> list = mapper.selectList(new QueryWrapper<ShowRoom>().lambda()
                .eq(ShowRoom::getShowMuseumId, museumId)
                .eq(ShowRoom::getDelFlag, 0)
                .orderByAsc(ShowRoom::getRoomNo));
        List<ShowRoomVo> data = JSONObject.parseArray(JSON.toJSONString(list), ShowRoomVo.class);
        result.setResult(data);
        return result;
    }

    @Override
    public Result<List<ShowRoomVo2>> getRoomListV2(String museumId) {
        Result<List<ShowRoomVo2>> result = Result.OK();
        if (StringUtils.isEmpty(museumId)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        List<ShowRoom> list = mapper.selectList(new QueryWrapper<ShowRoom>().lambda()
                .eq(ShowRoom::getShowMuseumId, museumId)
                .eq(ShowRoom::getDelFlag, 0)
                .orderByAsc(ShowRoom::getRoomNo));
        List<ShowRoomVo2> data = JSONObject.parseArray(JSON.toJSONString(list), ShowRoomVo2.class);
        result.setResult(data);
        return result;
    }
}
