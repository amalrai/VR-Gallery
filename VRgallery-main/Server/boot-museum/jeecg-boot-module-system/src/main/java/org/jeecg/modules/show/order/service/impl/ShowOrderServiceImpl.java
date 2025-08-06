package org.jeecg.modules.show.order.service.impl;

import com.alibaba.fastjson.JSONObject;
import com.baomidou.mybatisplus.core.toolkit.IdWorker;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import lombok.extern.slf4j.Slf4j;
import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.util.DateUtils;
import org.jeecg.modules.show.museum.entity.ShowMuseum;
import org.jeecg.modules.show.museum.mapper.ShowMuseumMapper;
import org.jeecg.modules.show.order.entity.ShowOrder;
import org.jeecg.modules.show.order.mapper.ShowOrderMapper;
import org.jeecg.modules.show.order.service.IShowOrderService;
import org.jeecg.modules.show.room.entity.ShowRoom;
import org.jeecg.modules.show.room.mapper.ShowRoomMapper;
import org.jeecg.modules.show.user.entity.ShowUser;
import org.jeecg.modules.show.user.mapper.ShowUserMapper;
import org.jeecg.modules.show.uservip.entity.ShowUserVip;
import org.jeecg.modules.show.uservip.mapper.ShowUserVipMapper;
import org.jeecg.modules.show.vipfee.entity.ShowVipFee;
import org.jeecg.modules.show.vipfee.mapper.ShowVipFeeMapper;
import org.jeecg.modules.system.util.MessageUtil;
import org.jeecg.modules.system.util.PayUtil;
import org.jeecgframework.core.util.ApplicationContextUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.math.BigDecimal;
import java.util.Date;
import java.util.List;
import java.util.Map;

/**
 * @Description: 订单管理
 * @Author: jeecg-boot
 * @Date: 2023-04-02
 * @Version: V1.0
 */
@Slf4j
@Service
public class ShowOrderServiceImpl extends ServiceImpl<ShowOrderMapper, ShowOrder> implements IShowOrderService {

    @Autowired
    private ShowOrderMapper mapper;
    @Autowired
    private ShowVipFeeMapper vipFeeMapper;
    @Autowired
    private ShowUserMapper userMapper;
    @Autowired
    private ShowUserVipMapper userVipMapper;
    @Autowired
    private ShowMuseumMapper museumMapper;
    @Autowired
    private ShowRoomMapper roomMapper;
    @Autowired
    private PayUtil payUtil;

    @Override
    public BigDecimal getTotalFee(Map<String, String[]> parameterMap) {
        String endValue = null, beginValue = null, showMuseumId = null, showUserEmail = null;

        if (parameterMap != null && parameterMap.containsKey("payTime_begin")) {
            beginValue = parameterMap.get("payTime_begin")[0].trim();

        }
        if (parameterMap != null && parameterMap.containsKey("payTime_end")) {
            endValue = parameterMap.get("payTime_end")[0].trim();
        }
        if (parameterMap != null && parameterMap.containsKey("showMuseumId")) {
            showMuseumId = parameterMap.get("showMuseumId")[0].trim();
        }
        if (parameterMap != null && parameterMap.containsKey("showUserEmail")) {
            showUserEmail = parameterMap.get("showUserEmail")[0];
        }
        return mapper.getTotalFee(beginValue, endValue, showMuseumId, showUserEmail);
    }

    @Override
    public Result<?> creatOrder(String vipFeeId, String discountCode, String museumId, String userId) {
        if (StringUtils.isEmpty(vipFeeId)
                || StringUtils.isEmpty(museumId)
                || StringUtils.isEmpty(userId)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        //1. 查询用户信息
        ShowUser user = userMapper.selectById(userId);
        if (user == null) {
            return Result.error(MessageUtil.getMessage("用户不存在", UserLocalContext.getLocal()));
        }
        if (user.getIsAuth() == null || user.getIsAuth() != 1) {
            return Result.error(MessageUtil.getMessage("会员权限已被关闭，请联系管理员", UserLocalContext.getLocal()));
        }
        //2. 查询付费方式信息
        ShowVipFee vipFee = vipFeeMapper.selectById(vipFeeId);
        if (vipFee == null) {
            return Result.error(MessageUtil.getMessage("付费方式不存在", UserLocalContext.getLocal()));
        }
        //3. 查询展馆信息 用做支付时显示商品名称
        ShowMuseum museum = museumMapper.selectById(museumId);
        if (museum == null) {
            return Result.error(MessageUtil.getMessage("展馆不存在", UserLocalContext.getLocal()));
        }
        //4.校验折扣码和展馆信息
        if (!StringUtils.equals(vipFee.getShowMuseumId(), museumId)) {
            return Result.error(MessageUtil.getMessage("付费方式错误", UserLocalContext.getLocal()) + "(museum)");
        }
        if(vipFee.getStatus() == null || vipFee.getStatus() != 1){
            return Result.error(MessageUtil.getMessage("当前付费方式不可用", UserLocalContext.getLocal()));
        }
        Integer renewTime = vipFee.getTimeLimit();
        if (renewTime == null || renewTime <= 0) {
            return Result.error(MessageUtil.getMessage("付费方式错误", UserLocalContext.getLocal()) + "(time)");
        }
        String productName = museum.getName();
        BigDecimal orderFee = vipFee.getVipFee();
        int isDiscount = 0;
        BigDecimal discountFee = BigDecimal.ZERO;
        BigDecimal afterDiscountFee;
        // 用户输入折扣码，且当前付费方式折扣码不为空
        if (StringUtils.isNotBlank(vipFee.getDiscountCode()) && StringUtils.isNotBlank(discountCode)) {
            if (vipFee.getDiscountCode().equals(discountCode)) {
                // 计算折扣金额
                isDiscount = 1;
                discountFee = vipFee.getDiscountFee();
            } else {
                return Result.error(MessageUtil.getMessage("折扣码不存在", UserLocalContext.getLocal()));
            }
        }
        afterDiscountFee = orderFee.subtract(discountFee);
        String orderId = String.valueOf(IdWorker.getId());
        JSONObject payResult = payUtil.creatOrder(orderId, productName, afterDiscountFee);
        if (payResult == null || StringUtils.isBlank(payResult.getString("LinkUrl"))) {
            log.error("订单创建异常 err:{}", payResult);
            return Result.error(MessageUtil.getMessage("订单创建异常", UserLocalContext.getLocal()));
        }
        Date now = new Date();
        ShowOrder order = new ShowOrder();
        order.setId(orderId);
        order.setOrderTime(now);
        order.setRenewTime(vipFee.getTimeLimit());
        order.setProductId(vipFee.getId());
        order.setProductName(productName);
        order.setShowMuseumId(museum.getId());
        order.setShowUserId(user.getId());
        order.setShowUserEmail(user.getEmail());
        order.setOrderFee(orderFee);
        order.setDiscountFee(discountFee);
        order.setIsDiscount(isDiscount);
        order.setDiscountCode(discountCode);
        order.setAfterDiscountFee(afterDiscountFee);
        order.setStatus(0);
        order.setCreateTime(now);
        order.setUpdateTime(now);
        mapper.insert(order);
        JSONObject result = new JSONObject();
        result.put("orderId", orderId);
        result.put("orderUrl", payResult.getString("LinkUrl"));
        return Result.OK(result).success("订单创建成功");
    }

    @Override
    public Result<?> notifyOrder(JSONObject jsonObject) {
        String orderId = jsonObject.getString("OrderID");
        String status = jsonObject.getString("Status");
        BigDecimal amount = jsonObject.getBigDecimal("Amount");
        if (!"AUTH".equalsIgnoreCase(status)) {
            log.info("非支付成功消息，不处理");
            return Result.OK();
        }
        try {
            ShowOrder order = mapper.selectById(orderId);
            if (order == null) {
                throw new Exception("order not exist!");
            }

            Date now = new Date();
            ShowOrder updateOrder = new ShowOrder();
            updateOrder.setId(orderId);
            updateOrder.setStatus(1);
            updateOrder.setPayFee(amount);
            updateOrder.setPayTime(now);

            Date expirationTime;
            ShowUserVip userVip = userVipMapper.selectUserVip(order.getShowUserId(), order.getShowRoomId());
            if(userVip == null){
                userVip = new ShowUserVip();
                expirationTime = DateUtils.getDate(now.getTime() + (order.getRenewTime() * 86400000L));
                userVip.setCreateTime(now);
            }else{
                if(userVip.getExpirationTime().getTime() < now.getTime()){
                    expirationTime = DateUtils.getDate(now.getTime() + (order.getRenewTime() * 86400000L));
                }else{
                    expirationTime = DateUtils.getDate(userVip.getExpirationTime().getTime() + (order.getRenewTime() * 86400000L));
                }
            }
            updateOrder.setExpirationTime(expirationTime);
            userVip.setShowUserId(order.getShowUserId());
            userVip.setShowMuseumId(order.getShowMuseumId());
            userVip.setShowRoomId(order.getShowRoomId());
            userVip.setRenewalTime(now);
            userVip.setShowOrderId(orderId);
            userVip.setExpirationTime(expirationTime);
            userVip.setUpdateTime(now);
            ShowOrderServiceImpl service = ApplicationContextUtil.getContext().getBean(ShowOrderServiceImpl.class);
            service.updateData(updateOrder, userVip);
        } catch (Exception e) {
            log.error("处理支付结果异常", e);
            return Result.error(e.getMessage());
        }
        return Result.OK();
    }

    @Override
    public Result<?> getOrderInfo(String orderId) {
        if (StringUtils.isBlank(orderId)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        ShowOrder order = mapper.selectById(orderId);
        if(order == null){
            return Result.error(MessageUtil.getMessage("订单不存在", UserLocalContext.getLocal()));
        }
        return Result.OK(order).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> getOrderList(String userId, String museumId, String roomId, Integer pageNo, Integer pageSize) {
        if (StringUtils.isBlank(userId)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        List<ShowOrder> orderList = mapper.getOrderList(userId, museumId, roomId, pageNo, pageSize);
        return Result.OK(orderList).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> checkDiscountCode(String vipFeeId, String discountCode) {
        if (StringUtils.isEmpty(vipFeeId)
                || StringUtils.isEmpty(discountCode)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        ShowVipFee vipFee = vipFeeMapper.selectById(vipFeeId);
        if (vipFee == null) {
            return Result.error(MessageUtil.getMessage("付费方式不存在", UserLocalContext.getLocal()));
        }
        if (StringUtils.isBlank(vipFee.getDiscountCode())) {
            return Result.error(MessageUtil.getMessage("折扣码不存在", UserLocalContext.getLocal()));
        }
        if (!vipFee.getDiscountCode().equals(discountCode)) {
            return Result.error(MessageUtil.getMessage("折扣码不存在", UserLocalContext.getLocal()));
        }
        return Result.OK().success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
    }

    @Transactional(rollbackFor = Exception.class)
    public void updateData(ShowOrder updateOrder, ShowUserVip userVip) {
        if(userVip.getId()!= null){
            userVipMapper.updateById(userVip);
        }else{
            userVip.setId(String.valueOf(IdWorker.getId()));
            userVipMapper.insert(userVip);
        }
        mapper.updateById(updateOrder);
    }

    @Override
    public Result<?> creatOrderV2(String vipFeeId, String discountCode, String platform, String museumId, String roomId, String userId) {
        if (StringUtils.isEmpty(vipFeeId)
                || StringUtils.isEmpty(platform)
                || StringUtils.isEmpty(museumId)
                || StringUtils.isEmpty(roomId)
                || StringUtils.isEmpty(userId)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        //1. 查询用户信息
        ShowUser user = userMapper.selectById(userId);
        if (user == null) {
            return Result.error(MessageUtil.getMessage("用户不存在", UserLocalContext.getLocal()));
        }
        if (user.getIsAuth() == null || user.getIsAuth() != 1) {
            return Result.error(MessageUtil.getMessage("会员权限已被关闭，请联系管理员", UserLocalContext.getLocal()));
        }
        //2. 查询付费方式信息
        ShowVipFee vipFee = vipFeeMapper.selectById(vipFeeId);
        if (vipFee == null) {
            return Result.error(MessageUtil.getMessage("付费方式不存在", UserLocalContext.getLocal()));
        }
        //3. 查询展馆信息 用做支付时显示商品名称
        ShowMuseum museum = museumMapper.selectById(museumId);
        if (museum == null) {
            return Result.error(MessageUtil.getMessage("展馆不存在", UserLocalContext.getLocal()));
        }
        ShowRoom room = roomMapper.selectById(roomId);
        if (room == null) {
            return Result.error(MessageUtil.getMessage("展馆不存在", UserLocalContext.getLocal()));
        }
        //4.校验折扣码和展馆信息
        if (!StringUtils.equals(vipFee.getShowMuseumId(), museumId)) {
            return Result.error(MessageUtil.getMessage("付费方式错误", UserLocalContext.getLocal()) + "(museum)");
        }
        if (!StringUtils.equals(vipFee.getShowRoomId(), roomId)) {
            return Result.error(MessageUtil.getMessage("付费方式错误", UserLocalContext.getLocal()) + "(room)");
        }
        if (!StringUtils.equals(vipFee.getPlatform(), platform)) {
            return Result.error(MessageUtil.getMessage("付费方式错误", UserLocalContext.getLocal()) + "(platform)");
        }
        if(vipFee.getStatus() == null || vipFee.getStatus() != 1){
            return Result.error(MessageUtil.getMessage("当前付费方式不可用", UserLocalContext.getLocal()));
        }
        Integer renewTime = vipFee.getTimeLimit();
        if (renewTime == null || renewTime <= 0) {
            return Result.error(MessageUtil.getMessage("付费方式错误", UserLocalContext.getLocal()) + "(time)");
        }
        String productName = museum.getName();
        BigDecimal orderFee = vipFee.getVipFee();
        int isDiscount = 0;
        BigDecimal discountFee = BigDecimal.ZERO;
        BigDecimal afterDiscountFee;
        // 用户输入折扣码，且当前付费方式折扣码不为空
        if (StringUtils.isNotBlank(vipFee.getDiscountCode()) && StringUtils.isNotBlank(discountCode)) {
            if (vipFee.getDiscountCode().equals(discountCode)) {
                // 计算折扣金额
                isDiscount = 1;
                discountFee = vipFee.getDiscountFee();
            } else {
                return Result.error(MessageUtil.getMessage("折扣码不存在", UserLocalContext.getLocal()));
            }
        }
        afterDiscountFee = orderFee.subtract(discountFee);
        String orderId = String.valueOf(IdWorker.getId());
        String linkUrl = "";
        if("GMO".equals(vipFee.getPlatform())){
            JSONObject payResult = payUtil.creatOrder(orderId, productName, afterDiscountFee);
            if (payResult == null || StringUtils.isBlank(payResult.getString("LinkUrl"))) {
                log.error("订单创建异常 err:{}", payResult);
                return Result.error(MessageUtil.getMessage("订单创建异常", UserLocalContext.getLocal()));
            }else{
                linkUrl = payResult.getString("LinkUrl");
            }
        }
        Date now = new Date();
        ShowOrder order = new ShowOrder();
        order.setId(orderId);
        order.setPlatform(vipFee.getPlatform());
        order.setOrderType(vipFee.getOrderType());
        order.setOrderTime(now);
        order.setRenewTime(vipFee.getTimeLimit());
        order.setProductId(vipFee.getId());
        order.setProductName(productName);
        order.setShowMuseumId(museum.getId());
        order.setShowRoomId(vipFee.getShowRoomId());
        order.setShowUserId(user.getId());
        order.setShowUserEmail(user.getEmail());
        order.setOrderFee(orderFee);
        order.setDiscountFee(discountFee);
        order.setIsDiscount(isDiscount);
        order.setDiscountCode(discountCode);
        order.setAfterDiscountFee(afterDiscountFee);
        order.setStatus(0);
        order.setCreateTime(now);
        order.setUpdateTime(now);
        mapper.insert(order);
        JSONObject result = new JSONObject();
        result.put("orderId", orderId);
        result.put("orderUrl", linkUrl);
        return Result.OK(result).success("订单创建成功");
    }

    @Override
    public Result<?> notifyOrderByApp(String orderId, BigDecimal amount) {
        try {
            ShowOrder order = mapper.selectById(orderId);
            if (order == null) {
                throw new Exception("order not exist!");
            }
            Date now = new Date();
            ShowOrder updateOrder = new ShowOrder();
            updateOrder.setId(orderId);
            updateOrder.setStatus(1);
            if(amount == null){
                amount = order.getOrderFee();
            }
            updateOrder.setPayFee(amount);
            updateOrder.setPayTime(now);

            Date expirationTime;
            ShowUserVip userVip = userVipMapper.selectUserVip(order.getShowUserId(), order.getShowRoomId());
            if(userVip == null){
                userVip = new ShowUserVip();
                expirationTime = DateUtils.getDate(now.getTime() + (order.getRenewTime() * 86400000L));
                userVip.setCreateTime(now);
            }else{
                if(userVip.getExpirationTime().getTime() < now.getTime()){
                    expirationTime = DateUtils.getDate(now.getTime() + (order.getRenewTime() * 86400000L));
                }else{
                    expirationTime = DateUtils.getDate(userVip.getExpirationTime().getTime() + (order.getRenewTime() * 86400000L));
                }
            }
            updateOrder.setExpirationTime(expirationTime);
            userVip.setShowUserId(order.getShowUserId());
            userVip.setShowMuseumId(order.getShowMuseumId());
            userVip.setShowRoomId(order.getShowRoomId());
            userVip.setRenewalTime(now);
            userVip.setShowOrderId(orderId);
            userVip.setExpirationTime(expirationTime);
            userVip.setUpdateTime(now);
            ShowOrderServiceImpl service = ApplicationContextUtil.getContext().getBean(ShowOrderServiceImpl.class);
            service.updateData(updateOrder, userVip);
        } catch (Exception e) {
            log.error("处理支付结果异常", e);
            return Result.error(e.getMessage());
        }
        return Result.OK();
    }
}
