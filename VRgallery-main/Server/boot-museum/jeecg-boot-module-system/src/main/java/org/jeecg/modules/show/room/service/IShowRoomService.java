package org.jeecg.modules.show.room.service;

import com.baomidou.mybatisplus.extension.service.IService;
import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.room.entity.ShowRoom;
import org.jeecg.modules.show.room.entity.ShowRoomVo;
import org.jeecg.modules.show.room.entity.ShowRoomVo2;

import java.util.List;

/**
 * @Description: show_room
 * @Author: jeecg-boot
 * @Date: 2022-06-15
 * @Version: V1.0
 */
public interface IShowRoomService extends IService<ShowRoom> {

    Result<List<ShowRoomVo>> getRoomList(String museumId);

    Result<List<ShowRoomVo2>> getRoomListV2(String museumId);
}
