package org.jeecg.modules.show.user.service.impl;

import com.alibaba.fastjson.JSON;
import com.alibaba.fastjson.JSONObject;
import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.conditions.update.UpdateWrapper;
import com.baomidou.mybatisplus.core.toolkit.IdWorker;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.apache.commons.collections.CollectionUtils;
import org.apache.commons.lang3.ObjectUtils;
import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.constant.CommonConstant;
import org.jeecg.common.util.DateUtils;
import org.jeecg.common.util.RedisUtil;
import org.jeecg.common.util.SpringContextHolder;
import org.jeecg.common.util.aws.AwsUtil;
import org.jeecg.modules.message.handle.impl.EmailSendMsgHandle;
import org.jeecg.modules.show.area.entity.Area;
import org.jeecg.modules.show.area.mapper.AreaMapper;
import org.jeecg.modules.show.museum.entity.ShowMuseum;
import org.jeecg.modules.show.museum.mapper.ShowMuseumMapper;
import org.jeecg.modules.show.order.entity.ShowOrder;
import org.jeecg.modules.show.order.mapper.ShowOrderMapper;
import org.jeecg.modules.show.perform.entity.ShowPerform;
import org.jeecg.modules.show.perform.mapper.ShowPerformMapper;
import org.jeecg.modules.show.room.entity.ShowRoom;
import org.jeecg.modules.show.room.mapper.ShowRoomMapper;
import org.jeecg.modules.show.user.entity.*;
import org.jeecg.modules.show.user.mapper.ShowUserMapper;
import org.jeecg.modules.show.user.mapper.ShowVisitorMapper;
import org.jeecg.modules.show.user.service.IShowUserService;
import org.jeecg.modules.show.uservip.entity.ShowUserVip;
import org.jeecg.modules.show.uservip.mapper.ShowUserVipMapper;
import org.jeecg.modules.system.util.Base64ImageUtil;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.Date;
import java.util.List;
import java.util.Random;

/**
 * @Description: 用户管理
 * @Author: jeecg-boot
 * @Date: 2021-10-15
 * @Version: V1.0
 */
@Service
public class ShowUserServiceImpl extends ServiceImpl<ShowUserMapper, ShowUser> implements IShowUserService {

    @Autowired
    private ShowUserMapper mapper;
    @Autowired
    private ShowUserVipMapper userVipMapper;
    //    @Autowired
//    private IShowDataService dataService;
//    @Autowired
//    private IShowCommentService commentService;
    @Autowired
    private ShowPerformMapper performMapper;
    @Autowired
    private ShowVisitorMapper visitorMapper;
    @Autowired
    private ShowOrderMapper orderMapper;
    @Autowired
    private ShowMuseumMapper museumMapper;
    @Autowired
    private ShowRoomMapper roomMapper;

    @Override
    public Result<?> sendRegisterCode(String appId, String email) {
        if (StringUtils.isEmpty(email) || StringUtils.isEmpty(appId)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        int count = mapper.selectCount(new QueryWrapper<ShowUser>().lambda()
                .eq(ShowUser::getEmail, email)
                .eq(ShowUser::getShowAppId, appId));
        if (count > 0) {
            return Result.error(MessageUtil.getMessage("邮箱已被注册", UserLocalContext.getLocal()));
        }
        //发送登录验证码
        String code = String.format("%04d", new Random().nextInt(9999));

        //存入redis
        String redisKey = "user:code:register:" + appId + ":" + email;
        RedisUtil redisUtil = SpringContextHolder.getBean(RedisUtil.class);
        if (!redisUtil.set(redisKey, code, 900)) {
            return Result.error(MessageUtil.getMessage("验证码发送失败", UserLocalContext.getLocal()));
        }
        EmailSendMsgHandle sendMsgHandle = new EmailSendMsgHandle();
        StringBuilder messageContent = new StringBuilder();
        messageContent.append(MessageUtil.getMessage("register.email.content1", UserLocalContext.getLocal()))
                .append(code).append("\n")
                .append(MessageUtil.getMessage("register.email.content2", UserLocalContext.getLocal()));
        boolean result = sendMsgHandle.SendMsg(email,
                MessageUtil.getMessage("register.email.title", UserLocalContext.getLocal()),
                messageContent.toString());
        if (!result) {
            redisUtil.del(redisKey);
            return Result.error(MessageUtil.getMessage("验证码发送失败", UserLocalContext.getLocal()));
        }
        return Result.OK().success(MessageUtil.getMessage("验证码发送成功", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> register(String appId, String email, String pwd, String code, String appName, Integer sex, Integer years, Long areaId) {
        if (StringUtils.isEmpty(email) || StringUtils.isEmpty(pwd) || StringUtils.isEmpty(code) || StringUtils.isEmpty(appId)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        List<ShowUser> users = mapper.selectList(new QueryWrapper<ShowUser>().lambda()
                .eq(ShowUser::getEmail, email)
                .eq(ShowUser::getShowAppId, appId));
        if (!CollectionUtils.isEmpty(users)) {
            return Result.error(MessageUtil.getMessage("邮箱已被注册", UserLocalContext.getLocal()));
        }
        //校验验证码
        String redisKey = "user:code:register:" + appId + ":" + email;
        RedisUtil redisUtil = SpringContextHolder.getBean(RedisUtil.class);
        String oldCode = (String) redisUtil.get(redisKey);
        if (!code.equalsIgnoreCase(oldCode)) {
            return Result.error(MessageUtil.getMessage("验证码无效", UserLocalContext.getLocal()));
        }
        ShowUser user = new ShowUser();
        String uid = String.valueOf(IdWorker.getId());
        user.setId(uid);
        user.setEmail(email);
        user.setPwd(pwd);
        user.setShowAppId(appId);
        user.setAppName(appName);
        user.setState(1);
        user.setIsAuth(1);
        user.setType(1);
        user.setSex(sex);
        user.setYears(years);
        if (areaId != null) {
            user.setAreaId(areaId);
            AreaMapper areaMapper = SpringContextHolder.getBean(AreaMapper.class);
            Area area = areaMapper.selectById(areaId);
            if (area == null) {
                return Result.error(MessageUtil.getMessage("地区错误", UserLocalContext.getLocal()));
            }
            user.setAreaName(area.getName());
            user.setAddress(area.getName());
        }
//        user.setViewCount(0);
//        user.setViewTime(0);
//        user.setCommentCount(0);
//        user.setLiveTime(0);
        user.setRegisterTime(new Date());
        user.setFirstLogin(0);
        if (mapper.insert(user) > 0) {
            return Result.OK(uid).success(MessageUtil.getMessage("注册成功", UserLocalContext.getLocal()));
        } else {
            return Result.error("error");
        }
    }

    @Override
    public Result<ShowUserVo> login(String appId, String email, String pwd) {
        Result<ShowUserVo> result = Result.OK();
        if (StringUtils.isEmpty(email) || StringUtils.isEmpty(pwd) || StringUtils.isEmpty(appId)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        List<ShowUser> users = mapper.selectList(new QueryWrapper<ShowUser>().lambda()
                .eq(ShowUser::getEmail, email)
                .eq(ShowUser::getShowAppId, appId));
        if (CollectionUtils.isEmpty(users)) {
            result.setCode(CommonConstant.SC_JEECG_NO_AUTHZ);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("账号错误或未注册", UserLocalContext.getLocal()));
            return result;
        }
        ShowUser user = users.get(0);
        if (ObjectUtils.notEqual(pwd, user.getPwd())) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("密码错误", UserLocalContext.getLocal()));
            return result;
        }
        user.setPwd(null);
        result.setResult(JSONObject.parseObject(JSON.toJSONString(user), ShowUserVo.class));
        result.setMessage(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
//        Result<String> ret = dataService.saveViewTime(null, user, 0);
//        if(ret.isSuccess()){
//            if(user.getFirstLogin() == null){
//                user.setFirstLogin(0);
//            }
//            if(user.getFirstLogin() == 0){
//                ShowUser updateUser = new ShowUser();
//                updateUser.setFirstLogin(1);
//                updateUser.setId(user.getId());
//                mapper.updateById(updateUser);
//            }
//            loginResult.setId(ret.getResult());
//            loginResult.setUser(user);
//        }
        return result;
    }
    @Override
    public Result<ShowUserVo2> loginV2(String appId, String email, String pwd) {
        Result<ShowUserVo2> result = Result.OK();
        if (StringUtils.isEmpty(email) || StringUtils.isEmpty(pwd) || StringUtils.isEmpty(appId)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        List<ShowUser> users = mapper.selectList(new QueryWrapper<ShowUser>().lambda()
                .eq(ShowUser::getEmail, email)
                .eq(ShowUser::getShowAppId, appId));
        if (CollectionUtils.isEmpty(users)) {
            result.setCode(CommonConstant.SC_JEECG_NO_AUTHZ);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("账号错误或未注册", UserLocalContext.getLocal()));
            return result;
        }
        ShowUser user = users.get(0);
        if (ObjectUtils.notEqual(pwd, user.getPwd())) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("密码错误", UserLocalContext.getLocal()));
            return result;
        }
        user.setPwd(null);
        result.setResult(JSONObject.parseObject(JSON.toJSONString(user), ShowUserVo2.class));
        result.setMessage(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
        return result;
    }

    @Override
    public Result<?> sendForgetPwdCode(String appId, String email) {
        if (StringUtils.isEmpty(email) || StringUtils.isEmpty(appId)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        int count = mapper.selectCount(new QueryWrapper<ShowUser>().lambda()
                .eq(ShowUser::getEmail, email)
                .eq(ShowUser::getShowAppId, appId));
        if (count == 0) {
            return Result.error(MessageUtil.getMessage("邮箱未注册", UserLocalContext.getLocal()));
        }
        //发送登录验证码
        String code = String.format("%04d", new Random().nextInt(9999));

        //存入redis
        String redisKey = "user:code:forgetPwd:" + appId + ":" + email;
        RedisUtil redisUtil = SpringContextHolder.getBean(RedisUtil.class);
        if (!redisUtil.set(redisKey, code, 900)) {
            return Result.error(MessageUtil.getMessage("验证码发送失败", UserLocalContext.getLocal()));
        }
        EmailSendMsgHandle sendMsgHandle = new EmailSendMsgHandle();
        StringBuilder messageContent = new StringBuilder();
        messageContent.append(MessageUtil.getMessage("register.email.content1", UserLocalContext.getLocal()))
                .append(code).append("\n")
                .append(MessageUtil.getMessage("register.email.content2", UserLocalContext.getLocal()));
        boolean result = sendMsgHandle.SendMsg(email,
                MessageUtil.getMessage("register.email.title", UserLocalContext.getLocal()),
                messageContent.toString());
        if (!result) {
            redisUtil.del(redisKey);
            return Result.error(MessageUtil.getMessage("验证码发送失败", UserLocalContext.getLocal()));
        }
        return Result.OK().success(MessageUtil.getMessage("验证码发送成功", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> forgetPwd(String appId, String email, String newPwd, String code) {
        if (StringUtils.isEmpty(email) || StringUtils.isEmpty(newPwd) || StringUtils.isEmpty(code) || StringUtils.isEmpty(appId)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        List<ShowUser> users = mapper.selectList(new QueryWrapper<ShowUser>().lambda()
                .eq(ShowUser::getEmail, email)
                .eq(ShowUser::getShowAppId, appId));
        if (CollectionUtils.isEmpty(users)) {
            return Result.error(MessageUtil.getMessage("邮箱未注册", UserLocalContext.getLocal()));
        }
        //校验验证码
        String redisKey = "user:code:forgetPwd:" + appId + ":" + email;
        RedisUtil redisUtil = SpringContextHolder.getBean(RedisUtil.class);
        String oldCode = (String) redisUtil.get(redisKey);
        if (!code.equalsIgnoreCase(oldCode)) {
            return Result.error(MessageUtil.getMessage("验证码无效", UserLocalContext.getLocal()));
        }
        ShowUser user = users.get(0);
        ShowUser updateUser = new ShowUser();
        updateUser.setPwd(newPwd);
        updateUser.setId(user.getId());
        mapper.updateById(updateUser);
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> resetPwd(String appId, String email, String newPwd, String oldPwd) {
        if (StringUtils.isEmpty(email) || StringUtils.isEmpty(newPwd) || StringUtils.isEmpty(oldPwd) || StringUtils.isEmpty(appId)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        List<ShowUser> users = mapper.selectList(new QueryWrapper<ShowUser>().lambda()
                .eq(ShowUser::getEmail, email)
                .eq(ShowUser::getShowAppId, appId));
        if (CollectionUtils.isEmpty(users)) {
            return Result.error(MessageUtil.getMessage("邮箱未注册", UserLocalContext.getLocal()));
        }
        ShowUser user = users.get(0);
        if (!StringUtils.equals(user.getPwd(), oldPwd)) {
            return Result.error(MessageUtil.getMessage("密码错误", UserLocalContext.getLocal()));
        }
        ShowUser updateUser = new ShowUser();
        updateUser.setPwd(newPwd);
        updateUser.setId(user.getId());
        mapper.updateById(updateUser);
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> updateUserInfo(String id, String nickName, String realName, String avatar, Integer sex, Integer age, String phone, Integer years, Long areaId, String detailAddress) {
        if (StringUtils.isEmpty(id)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        ShowUser user = mapper.selectById(id);
        if (user == null) {
            return Result.error(MessageUtil.getMessage("用户不存在", UserLocalContext.getLocal()));
        }
        ShowUser updateUser = new ShowUser();
        updateUser.setNickName(nickName);
        updateUser.setRealName(realName);
        if (StringUtils.isNotBlank(avatar)) {
            String fileKey = null;
            try {
                String dataPrix = "";
                String data = "";
                String[] d = avatar.split("base64,");
                if (d.length == 2) {
                    dataPrix = d[0];
                    data = d[1];
                } else {
                    return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
                }

                String suffix;
                if ("data:image/jpeg;".equalsIgnoreCase(dataPrix)) {
                    suffix = ".jpg";
                } else if ("data:image/x-icon;".equalsIgnoreCase(dataPrix)) {
                    suffix = ".ico";
                } else if ("data:image/gif;".equalsIgnoreCase(dataPrix)) {
                    suffix = ".gif";
                } else if ("data:image/png;".equalsIgnoreCase(dataPrix)) {
                    suffix = ".png";
                } else {
                    return Result.error(MessageUtil.getMessage("头像上传失败", UserLocalContext.getLocal()));
                }
                fileKey = "ia/user/avatar/" + id + "/" + System.currentTimeMillis() + suffix;
                if (!Base64ImageUtil.getImgBase64ToImgFileAndUpload(data, fileKey)) {
                    return Result.error(MessageUtil.getMessage("头像上传失败", UserLocalContext.getLocal()));
                }
            } catch (Exception e) {
                log.error("头像上传失败", e);
                return Result.error(MessageUtil.getMessage("头像上传失败", UserLocalContext.getLocal()));
            }
            updateUser.setAvatar(AwsUtil.getStaticDomain() + fileKey);
        }
        updateUser.setSex(sex);
        updateUser.setAge(age);
        updateUser.setPhone(phone);
        updateUser.setYears(years);
        if (areaId != null) {
            updateUser.setAreaId(areaId);
            AreaMapper areaMapper = SpringContextHolder.getBean(AreaMapper.class);
            Area area = areaMapper.selectById(areaId);
            if (area == null) {
                return Result.error(MessageUtil.getMessage("地区错误", UserLocalContext.getLocal()));
            }
            updateUser.setAreaName(area.getName());
            updateUser.setDetailAddress(detailAddress);
            updateUser.setAddress(area.getName() + detailAddress);
        }
        updateUser.setFirstLogin(1);
        updateUser.setId(id);
        mapper.updateById(updateUser);
        //dataService.updateUserInfo(user);
        //commentService.updateUserInfo(user);
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<ShowUserVo> getUserInfo(String id) {
        Result<ShowUserVo> result = Result.OK();
        if (StringUtils.isEmpty(id)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        ShowUser user = mapper.selectById(id);
        if (user == null) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("用户不存在", UserLocalContext.getLocal()));
            return result;
        }
        user.setPwd(null);
        result.setMessage(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
        result.setResult(JSONObject.parseObject(JSON.toJSONString(user), ShowUserVo.class));
        return result;
    }
    @Override
    public Result<ShowUserVo2> getUserInfoV2(String id) {
        Result<ShowUserVo2> result = Result.OK();
        if (StringUtils.isEmpty(id)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        ShowUser user = mapper.selectById(id);
        if (user == null) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("用户不存在", UserLocalContext.getLocal()));
            return result;
        }
        user.setPwd(null);
        result.setMessage(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
        result.setResult(JSONObject.parseObject(JSON.toJSONString(user), ShowUserVo2.class));
        return result;
    }

    @Override
    public Result<?> visitorLogin() {

        visitorMapper.updateLoginCount();
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> deleteUserById(String id) {
        if (StringUtils.isEmpty(id)) {
            return Result.error(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
        }
        ShowUser user = mapper.selectById(id);
        if (user == null) {
            return Result.error(MessageUtil.getMessage("用户不存在", UserLocalContext.getLocal()));
        }
        mapper.deleteById(id);
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Integer queryVisitorCount() {
        Integer count = visitorMapper.queryVisitorCount();
        return count != null ? count : 0;
    }

    @Override
    public Result<?> checkAuth(String userId, String museumId) {
        Result<String> result = new Result<>();
        if (StringUtils.isEmpty(userId) || StringUtils.isEmpty(museumId)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        //1. 检查用户是否存在
        ShowUser user = mapper.selectById(userId);
        if (user == null) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("用户不存在", UserLocalContext.getLocal()));
            return result;
        }
        //2. 判断用户会员权限是否开启
        if (user.getIsAuth() == null || user.getIsAuth() != 1) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("会员权限已被关闭，请联系管理员", UserLocalContext.getLocal()));
            return result;
        }
        //3. 权限开启判断用户当前展馆是否到期
        ShowUserVip userVip = userVipMapper.selectUserVip(userId, museumId);
        Date now = new Date();
        if (userVip == null || userVip.getExpirationTime() == null || now.getTime() > userVip.getExpirationTime().getTime()) {
            result.setCode(CommonConstant.FAIL_NO_AUTH);
            result.setSuccess(true);
            result.setMessage(MessageUtil.getMessage("会员权限已到期", UserLocalContext.getLocal()));
            if (userVip != null && userVip.getExpirationTime() != null) {
                result.setResult(DateUtils.date2Str(userVip.getExpirationTime(), DateUtils.datetimeFormat.get()));
            }
            return result;
        }
        result.setCode(CommonConstant.SC_OK_200);
        result.setSuccess(true);
        result.setMessage("success");
        result.setResult(DateUtils.date2Str(userVip.getExpirationTime(), DateUtils.datetimeFormat.get()));
        return result;
    }

    @Override
    public Result<?> getPayManagementInfo(String userId, String museumId) {
        Result<String> result = new Result<>();
        if (StringUtils.isEmpty(userId) || StringUtils.isEmpty(museumId)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        ShowMuseum museum = museumMapper.selectById(museumId);
        if (museum == null) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        ShowUserVip userVip = userVipMapper.selectUserVip(userId, museumId);
        if (userVip == null || userVip.getShowOrderId() == null) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        ShowOrder order = orderMapper.selectById(userVip.getShowOrderId());
        if (order == null) {
            return Result.error(MessageUtil.getMessage("订单不存在", UserLocalContext.getLocal()));
        }
        ShowUserPayManagementVO userPayManagementVO = new ShowUserPayManagementVO();
        userPayManagementVO.setId(order.getId());
        userPayManagementVO.setShowMuseumName(museum.getName());
        userPayManagementVO.setExpirationTime(userVip.getExpirationTime());
        userPayManagementVO.setPayFee(order.getPayFee());
        userPayManagementVO.setPayTime(order.getPayTime());
        userPayManagementVO.setIsDiscount(order.getIsDiscount());
        userPayManagementVO.setDiscountCode(order.getDiscountCode());
        return Result.OK(userPayManagementVO).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
    }
    @Override
    public Result<?> getPayManagementInfoV2(String userId, String museumId, String roomId) {
        Result<String> result = new Result<>();
        if (StringUtils.isEmpty(userId) || StringUtils.isEmpty(museumId) || StringUtils.isEmpty(roomId)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        ShowMuseum museum = museumMapper.selectById(museumId);
        if (museum == null) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        ShowRoom room = roomMapper.selectById(roomId);
        if (room == null) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        ShowUserVip userVip = userVipMapper.selectUserVip(userId, roomId);
        if (userVip == null || userVip.getShowOrderId() == null) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        ShowOrder order = orderMapper.selectById(userVip.getShowOrderId());
        if (order == null) {
            return Result.error(MessageUtil.getMessage("订单不存在", UserLocalContext.getLocal()));
        }
        ShowUserPayManagementVo2 userPayManagementVO = new ShowUserPayManagementVo2();
        userPayManagementVO.setId(order.getId());
        userPayManagementVO.setShowMuseumName(museum.getName());
        userPayManagementVO.setShowRoomName(room.getName());
        userPayManagementVO.setExpirationTime(userVip.getExpirationTime());
        userPayManagementVO.setPayFee(order.getPayFee());
        userPayManagementVO.setPayTime(order.getPayTime());
        userPayManagementVO.setIsDiscount(order.getIsDiscount());
        userPayManagementVO.setDiscountCode(order.getDiscountCode());
        return Result.OK(userPayManagementVO).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> checkAuthV2(String userId, String museumId, String roomId) {
        Result<String> result = new Result<>();
        if (StringUtils.isEmpty(userId) || StringUtils.isEmpty(museumId) || StringUtils.isEmpty(roomId)) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("参数错误", UserLocalContext.getLocal()));
            return result;
        }
        //1. 检查用户是否存在
        ShowUser user = mapper.selectById(userId);
        if (user == null) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("用户不存在", UserLocalContext.getLocal()));
            return result;
        }
        //2. 判断用户会员权限是否开启
        if (user.getIsAuth() == null || user.getIsAuth() != 1) {
            result.setCode(CommonConstant.SC_INTERNAL_SERVER_ERROR_500);
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("会员权限已被关闭，请联系管理员", UserLocalContext.getLocal()));
            return result;
        }
        //3. 权限开启判断用户当前展馆是否到期
        ShowUserVip userVip = userVipMapper.selectUserVip(userId, roomId);
        Date now = new Date();
        if (userVip == null || userVip.getExpirationTime() == null || now.getTime() > userVip.getExpirationTime().getTime()) {
            result.setCode(CommonConstant.FAIL_NO_AUTH);
            result.setSuccess(true);
            result.setMessage(MessageUtil.getMessage("会员权限已到期", UserLocalContext.getLocal()));
            if (userVip != null && userVip.getExpirationTime() != null) {
                result.setResult(DateUtils.date2Str(userVip.getExpirationTime(), DateUtils.datetimeFormat.get()));
            }
            return result;
        }
        result.setCode(CommonConstant.SC_OK_200);
        result.setSuccess(true);
        result.setMessage("success");
        result.setResult(DateUtils.date2Str(userVip.getExpirationTime(), DateUtils.datetimeFormat.get()));
        return result;
    }

    @Override
    public Result<?> updateAuthByIds(List<String> asList, int i) {
        if (CollectionUtils.isEmpty(asList)) {
            return Result.error(MessageUtil.getMessage("请选择一条记录！", UserLocalContext.getLocal()));
        }
        mapper.update(null, new UpdateWrapper<ShowUser>().lambda()
                .set(ShowUser::getIsAuth, i)
                .in(ShowUser::getId, asList));
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> updateStateByIds(List<String> asList, int i) {
        if (CollectionUtils.isEmpty(asList)) {
            return Result.error(MessageUtil.getMessage("请选择一条记录！", UserLocalContext.getLocal()));
        }
        mapper.update(null, new UpdateWrapper<ShowUser>().lambda()
                .set(ShowUser::getState, i)
                .in(ShowUser::getId, asList));
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    @Transactional(rollbackFor = Exception.class)
    public Result<?> startViewPerform(String userId, String performId) {
        if (StringUtils.isEmpty(userId) || StringUtils.isEmpty(performId)) {
            return Result.error("param error");
        }
        mapper.update(null, new UpdateWrapper<ShowUser>().lambda().setSql("view_count=view_count+1").eq(ShowUser::getId, userId));
        performMapper.update(null, new UpdateWrapper<ShowPerform>().lambda().setSql("view_count=view_count+1").eq(ShowPerform::getId, performId));
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @Override
    public Result<?> submitViewTime(String userId, String dataId, Integer viewTime) {
        if (StringUtils.isEmpty(userId) || StringUtils.isEmpty(dataId) || viewTime == null) {
            return Result.error("param error");
        }
        ShowUser user = mapper.selectById(userId);
        if (user == null) {
            return Result.error("user not exist");
        }
        //dataService.saveViewTime(dataId, user, viewTime);
        return Result.OK();
    }
}
