package org.jeecg.modules.show.user.mapper;

import com.baomidou.mybatisplus.core.mapper.BaseMapper;
import org.apache.ibatis.annotations.Select;
import org.apache.ibatis.annotations.Update;
import org.jeecg.modules.show.user.entity.ShowVisitor;

public interface ShowVisitorMapper extends BaseMapper<ShowVisitor> {
    @Update("update show_visitor set login_count=login_count+1")
    int updateLoginCount();

    @Select("select login_count from show_visitor limit 1")
    Integer queryVisitorCount();
}
