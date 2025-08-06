package org.jeecg.modules.show.exhibits.mapper;

import java.util.List;

import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.jeecg.modules.show.exhibits.entity.ShowExhibits;
import com.baomidou.mybatisplus.core.mapper.BaseMapper;

/**
 * @Description: 展品管理
 * @Author: jeecg-boot
 * @Date:   2022-06-15
 * @Version: V1.0
 */
public interface ShowExhibitsMapper extends BaseMapper<ShowExhibits> {
    @Select("select max(exhibits_no) from show_exhibits where show_room_id = #{roomId}")
    Integer getMaxNo(@Param("roomId") String roomId);
}
