package org.jeecg.modules.show.uservip.mapper;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.jeecg.modules.show.uservip.entity.ShowUserVip;

/**
 * @Description: 会员续费管理
 * @Author: jeecg-boot
 * @Date: 2023-04-02
 * @Version: V1.0
 */
public interface ShowUserVipMapper extends BaseMapper<ShowUserVip> {
    @Select("select * from show_user_vip where show_user_id = #{showUserId} and show_room_id = #{showRoomId} limit 1")
    ShowUserVip selectUserVip(@Param("showUserId") String showUserId, @Param("showRoomId") String showRoomId);
}
