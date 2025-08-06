package org.jeecg.modules.show.order.mapper;

import java.math.BigDecimal;
import java.util.List;

import org.apache.ibatis.annotations.Param;
import org.apache.ibatis.annotations.Select;
import org.jeecg.modules.show.order.entity.ShowOrder;
import com.baomidou.mybatisplus.core.mapper.BaseMapper;

/**
 * @Description: 订单管理
 * @Author: jeecg-boot
 * @Date:   2023-04-02
 * @Version: V1.0
 */
public interface ShowOrderMapper extends BaseMapper<ShowOrder> {

    @Select({"<script>",
            " select ifnull(sum(pay_fee),0) from show_order where status = 1 ",
            "<when test='showMuseumId != null'>",
            "  and show_museum_id = #{showMuseumId} ",
            "</when> ",
            "<when test='showUserEmail != null'>",
            "  and show_user_email = #{showUserEmail} ",
            "</when> ",
            "<when test='beginTime != null'>",
            "  and pay_time &gt;= #{beginTime} ",
            "</when> ",
            "<when test='endTime != null'>",
            "  and pay_time &lt;= #{endTime} ",
            "</when> ",
            "</script>"})
    BigDecimal getTotalFee(@Param("beginTime")String beginTime, @Param("endTime")String endTime, @Param("showMuseumId")String showMuseumId, @Param("showUserEmail")String showUserEmail);

    @Select({"<script>",
            "select * from show_order where show_user_id = #{showUserId}",
            "<when test='showMuseumId != null'>",
            "  and show_museum_id = #{showMuseumId} ",
            "</when> ",
            "<when test='showRoomId != null'>",
            "  and show_room_id = #{showRoomId} ",
            "</when> ",
            " order by order_time desc",
            "<when test='pageNo != null and pageSize != null'>",
            "  limit  #{pageNo},#{pageSize}",
            "</when> ",
            "</script>"})
    List<ShowOrder> getOrderList(@Param("showUserId")String showUserId, @Param("showMuseumId")String showMuseumId, @Param("showRoomId")String showRoomId, @Param("pageNo")Integer pageNo, @Param("pageSize")Integer pageSize);
}
