package org.jeecg.modules.show.order.service;

import com.alibaba.fastjson.JSONObject;
import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.order.entity.ShowOrder;
import com.baomidou.mybatisplus.extension.service.IService;

import java.math.BigDecimal;
import java.util.Map;

/**
 * @Description: 订单管理
 * @Author: jeecg-boot
 * @Date:   2023-04-02
 * @Version: V1.0
 */
public interface IShowOrderService extends IService<ShowOrder> {

    BigDecimal getTotalFee(Map<String, String[]> parameterMap);

    Result<?> creatOrder(String vipFeeId, String discountCode, String museumId, String userId);

    Result<?> notifyOrder(JSONObject jsonObject);

    Result<?> getOrderInfo(String orderId);

    Result<?> getOrderList(String userId, String museumId, String roomId, Integer pageNo, Integer pageSize);

    Result<?> checkDiscountCode(String vipFeeId, String discountCode);

    Result<?> creatOrderV2(String vipFeeId, String discountCode, String platform, String museumId, String roomId, String userId);

    Result<?> notifyOrderByApp(String orderId, BigDecimal amount);
}
