package org.jeecg.modules.show.log.mapper;



import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.jeecg.modules.show.log.entity.ShowUserClickLog;
import com.baomidou.mybatisplus.core.mapper.BaseMapper;

/**
 * @Description: show_user_click_log
 * @Author: jeecg-boot
 * @Date:   2023-03-26
 * @Version: V1.0
 */
public interface ShowUserClickLogMapper extends BaseMapper<ShowUserClickLog> {
    @Select("select * from show_user_click_log where  show_user_login_log_id = #{loginLogId} and show_exhibits_id = #{exhibitsId} order by create_time asc limit 1")
    ShowUserClickLog queryByLoginId(@Param("loginLogId") String loginLogId, @Param("exhibitsId") String exhibitsId);
}
