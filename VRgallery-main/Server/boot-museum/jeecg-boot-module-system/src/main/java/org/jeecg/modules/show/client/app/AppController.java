package org.jeecg.modules.show.client.app;

import com.alibaba.fastjson.JSONObject;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.extern.slf4j.Slf4j;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.aspect.annotation.AutoLog;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.modules.show.app.entity.ShowApp;
import org.jeecg.modules.show.app.service.IShowAppService;
import org.jeecg.modules.show.area.entity.Area;
import org.jeecg.modules.show.area.service.IAreaService;
import org.jeecg.modules.show.comment.service.IShowCommentService;
import org.jeecg.modules.show.exhibits.entity.ShowExhibits;
import org.jeecg.modules.show.exhibits.service.IShowExhibitsService;
import org.jeecg.modules.show.log.service.IShowUserClickLogService;
import org.jeecg.modules.show.log.service.IShowUserLoginLogService;
import org.jeecg.modules.show.museum.entity.ShowMuseum;
import org.jeecg.modules.show.museum.service.IShowMuseumService;
import org.jeecg.modules.show.order.service.IShowOrderService;
import org.jeecg.modules.show.perform.service.IShowPerformService;
import org.jeecg.modules.show.room.entity.ShowRoom;
import org.jeecg.modules.show.room.entity.ShowRoomVo;
import org.jeecg.modules.show.room.entity.ShowRoomVo2;
import org.jeecg.modules.show.room.service.IShowRoomService;
import org.jeecg.modules.show.user.entity.ShowUser;
import org.jeecg.modules.show.user.entity.ShowUserVo;
import org.jeecg.modules.show.user.entity.ShowUserVo2;
import org.jeecg.modules.show.user.service.IShowUserService;
import org.jeecg.modules.show.vipfee.service.IShowVipFeeService;
import org.jeecg.modules.system.util.HttpRequestUtil;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import javax.servlet.http.HttpServletRequest;
import java.math.BigDecimal;
import java.util.List;

/**
 * @Description: 留言管理
 * @Author: jeecg-boot
 * @Date: 2021-10-16
 * @Version: V1.0
 */
@Api(tags = "APP接口")
@RestController
@RequestMapping("/api/app/")
@Slf4j
public class AppController {

    @Autowired
    private IShowUserService userService;
    @Autowired
    private IShowAppService appService;
    @Autowired
    private IShowPerformService performService;
    @Autowired
    private IShowCommentService commentService;
    @Autowired
    private IShowRoomService roomService;
    @Autowired
    private IShowMuseumService museumService;
    @Autowired
    private IShowExhibitsService exhibitsService;
    @Autowired
    private IAreaService areaService;
    @Autowired
    private IShowUserLoginLogService loginLogService;
    @Autowired
    private IShowUserClickLogService clickLogService;
    @Autowired
    private IShowVipFeeService vipFeeService;
    @Autowired
    private IShowOrderService orderService;

    @AutoLog(value = "APP接口-游客登录接口")
    @ApiOperation(value = "APP接口-游客登录接口", notes = "APP接口-游客登录接口")
    @RequestMapping(value = "/visitorLogin")
    public Result<?> visitorLogin(HttpServletRequest req) {
        log.error("asdasasd");
        return userService.visitorLogin();
    }

    @AutoLog(value = "APP接口-用户注销接口")
    @ApiOperation(value = "APP接口-用户注销接口", notes = "APP接口-用户注销接口")
    @PostMapping(value = "/deleteUserById")
    public Result<?> deleteUserById(@RequestParam(name = "id") String id,
                                    HttpServletRequest req) {
        return userService.deleteUserById(id);
    }

    @AutoLog(value = "APP接口-发送注册验证码")
    @ApiOperation(value = "APP接口-发送注册验证码", notes = "APP接口-发送注册验证码")
    @PostMapping(value = "/sendRegisterCode")
    public Result<?> sendRegisterCode(@RequestParam(name = "appId") String appId,
                                      @RequestParam(name = "email") String email,
                                      HttpServletRequest req) {

        return userService.sendRegisterCode(appId, email);
    }

    @AutoLog(value = "APP接口-注册提交接口")
    @ApiOperation(value = "APP接口-注册提交接口", notes = "APP接口-注册提交接口")
    @PostMapping(value = "/register")
    public Result<?> register(@RequestParam(name = "appId") String appId,
                              @RequestParam(name = "email") String email,
                              @RequestParam(name = "pwd") String pwd,
                              @RequestParam(name = "code") String code,
                              @RequestParam(name = "appName") String appName,
                              @RequestParam(name = "sex", required = false) Integer sex,
                              @RequestParam(name = "years", required = false) Integer years,
                              @RequestParam(name = "areaId", required = false) Long areaId,
                              HttpServletRequest req) {
        return userService.register(appId, email, pwd, code, appName, sex, years, areaId);
    }

    @AutoLog(value = "APP接口-登陆接口")
    @ApiOperation(value = "APP接口-登陆接口", notes = "APP接口-登陆接口")
    @PostMapping(value = "/login")
    public Result<ShowUserVo> login(@RequestParam(name = "appId") String appId,
                                    @RequestParam(name = "email") String email,
                                    @RequestParam(name = "pwd") String pwd,
                                    HttpServletRequest req) {

        return userService.login(appId, email, pwd);
    }

    @AutoLog(value = "APP接口-登陆接口")
    @ApiOperation(value = "APP接口-登陆接口", notes = "APP接口-登陆接口")
    @PostMapping(value = "/v2/login")
    public Result<ShowUserVo2> loginV2(@RequestParam(name = "appId") String appId,
                                    @RequestParam(name = "email") String email,
                                    @RequestParam(name = "pwd") String pwd,
                                    HttpServletRequest req) {

        return userService.loginV2(appId, email, pwd);
    }


    @AutoLog(value = "APP接口-忘记密码获取验证码")
    @ApiOperation(value = "APP接口-忘记密码获取验证码", notes = "APP接口-忘记密码获取验证码")
    @PostMapping(value = "/sendForgetPwdCode")
    public Result<?> sendForgetPwdCode(@RequestParam(name = "appId") String appId,
                                       @RequestParam(name = "email") String email,
                                       HttpServletRequest req) {

        return userService.sendForgetPwdCode(appId, email);
    }

    @AutoLog(value = "APP接口-忘记密码修改密码")
    @ApiOperation(value = "APP接口-忘记密码修改密码", notes = "APP接口-忘记密码修改密码")
    @PostMapping(value = "/forgetPwd")
    public Result<?> forgetPwd(@RequestParam(name = "appId") String appId,
                               @RequestParam(name = "email") String email,
                               @RequestParam(name = "newPwd") String newPwd,
                               @RequestParam(name = "code") String code,
                               HttpServletRequest req) {
        return userService.forgetPwd(appId, email, newPwd, code);
    }

    @AutoLog(value = "APP接口-使用旧密码修改密码")
    @ApiOperation(value = "APP接口-使用旧密码修改密码", notes = "APP接口-使用旧密码修改密码")
    @PostMapping(value = "/resetPwd")
    public Result<?> resetPwd(@RequestParam(name = "appId") String appId,
                              @RequestParam(name = "email") String email,
                              @RequestParam(name = "oldPwd") String oldPwd,
                              @RequestParam(name = "newPwd") String newPwd,
                              HttpServletRequest req) {
        return userService.resetPwd(appId, email, newPwd, oldPwd);
    }

    @AutoLog(value = "APP接口-完善用户信息接口")
    @ApiOperation(value = "APP接口-完善用户信息接口", notes = "APP接口-根据用户ID完善用户信息接口")
    @PostMapping(value = "/updateUserInfo")
    public Result<?> updateUserInfo(@RequestParam(name = "id") String id,
                                    @RequestParam(name = "nickName", required = false) String nickName,
                                    @RequestParam(name = "realName", required = false) String realName,
                                    @RequestParam(name = "avatar", required = false) String avatar,
                                    @RequestParam(name = "sex", required = false) Integer sex,
                                    @RequestParam(name = "age", required = false) Integer age,
                                    @RequestParam(name = "phone", required = false) String phone,
                                    @RequestParam(name = "years", required = false) Integer years,
                                    @RequestParam(name = "areaId", required = false) Long areaId,
                                    @RequestParam(name = "detailAddress", required = false) String detailAddress,
                                    HttpServletRequest req) {

        return userService.updateUserInfo(id, nickName, realName, avatar, sex, age, phone, years, areaId, detailAddress);
    }

    @AutoLog(value = "APP接口-获取用户信息")
    @ApiOperation(value = "APP接口-获取用户信息", notes = "APP接口-获取用户信息")
    @PostMapping(value = "/getUserInfo")
    public Result<ShowUserVo> getUserInfo(@RequestParam(name = "id") String id,
                                        HttpServletRequest req) {

        return userService.getUserInfo(id);
    }

    @AutoLog(value = "APP接口-获取用户信息")
    @ApiOperation(value = "APP接口-获取用户信息", notes = "APP接口-获取用户信息")
    @PostMapping(value = "/v2/getUserInfo")
    public Result<ShowUserVo2> getUserInfoV2(@RequestParam(name = "id") String id,
                                             HttpServletRequest req) {

        return userService.getUserInfoV2(id);
    }

    @AutoLog(value = "APP接口-获取展馆列表")
    @ApiOperation(value = "APP接口-获取展馆列表", notes = "APP接口-获取展馆列表")
    @PostMapping(value = "/getMuseumList")
    public Result<List<ShowMuseum>> getMuseumList(@RequestParam(name = "appId") String appId, HttpServletRequest req) {
        return museumService.getMuseumList(appId);
    }

    @AutoLog(value = "APP接口-获取房间列表")
    @ApiOperation(value = "APP接口-获取房间列表", notes = "APP接口-获取房间列表")
    @PostMapping(value = "/getRoomList")
    public Result<List<ShowRoomVo>> getRoomList(@RequestParam(name = "museumId") String museumId, HttpServletRequest req) {
        return roomService.getRoomList(museumId);
    }

    @AutoLog(value = "APP接口-获取房间内所有展品信息")
    @ApiOperation(value = "APP接口-获取房间内所有展品信息", notes = "APP接口-获取房间内所有展品信息")
    @PostMapping(value = "/getExhibitsList")
    public Result<List<ShowExhibits>> getExhibitsList(@RequestParam(name = "roomId") String roomId, HttpServletRequest req) {
        return exhibitsService.getExhibitsList(roomId);
    }

    @AutoLog(value = "APP接口-获取APP信息")
    @ApiOperation(value = "APP接口-获取APP信息", notes = "APP接口-获取APP信息")
    @PostMapping(value = "/getAppInfo")
    public Result<ShowApp> getAppInfo(@RequestParam(name = "appId") String appId,
                                      HttpServletRequest req) {
        ShowApp showApp = appService.getById(appId);
        return Result.OK(showApp).success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    @AutoLog(value = "APP接口-获取地区信息")
    @ApiOperation(value = "APP接口-获取地区信息", notes = "APP接口-获取地区信息")
    @PostMapping(value = "/getArea")
    public Result<List<Area>> getArea(HttpServletRequest req) {
        return Result.OK(areaService.list());
    }

    @AutoLog(value = "APP接口-上传登录日志")
    @ApiOperation(value = "APP接口-上传登录日志", notes = "APP接口-上传登录日志")
    @PostMapping(value = "/uploadLoginLog")
    public Result<?> uploadLoginLog(@RequestParam(name = "userId") String userId,
                                    @RequestParam(name = "roomId") String roomId,
                                    @RequestParam(name = "museumId") String museumId,
                                    HttpServletRequest req) {
        String loginIp = HttpRequestUtil.getRemoteHost(req);
        return loginLogService.uploadLoginLog(userId, roomId, museumId, loginIp);
    }

    @AutoLog(value = "APP接口-上传点击日志")
    @ApiOperation(value = "APP接口-上传点击日志", notes = "APP接口-上传点击日志")
    @PostMapping(value = "/uploadClickLog")
    public Result<?> uploadClickLog(@RequestParam(name = "loginLogId") String loginLogId,
                                    @RequestParam(name = "exhibitsId") String exhibitsId,
                                    @RequestParam(name = "introductionCount", required = false) Integer introductionCount,
                                    @RequestParam(name = "linkCount", required = false) Integer linkCount,
                                    @RequestParam(name = "videoCount", required = false) Integer videoCount,
                                    @RequestParam(name = "voiceCount", required = false) Integer voiceCount,
                                    @RequestParam(name = "shopCount", required = false) Integer shopCount,
                                    HttpServletRequest req) {
        return clickLogService.uploadClickLog(loginLogId, exhibitsId, introductionCount, linkCount, videoCount, voiceCount, shopCount);
    }

    @AutoLog(value = "APP接口-用户鉴权接口")
    @ApiOperation(value = "APP接口-用户鉴权接口", notes = "APP接口-用户鉴权接口")
    @PostMapping(value = "/checkAuth")
    public Result<?> checkAuth(@RequestParam(name = "userId") String userId,
                               @RequestParam(name = "museumId") String museumId,
                               HttpServletRequest req) {
        return userService.checkAuth(userId, museumId);
    }

    @AutoLog(value = "APP接口-查询当前展馆付费模式")
    @ApiOperation(value = "APP接口-查询当前展馆付费模式", notes = "APP接口-查询当前展馆付费模式")
    @PostMapping(value = "/queryPayMode")
    public Result<?> queryPayMode(@RequestParam(name = "museumId") String museumId,
                                  HttpServletRequest req) {
        return vipFeeService.queryByMuseumId(museumId);
    }

    @AutoLog(value = "APP接口-创建订单")
    @ApiOperation(value = "APP接口-创建订单", notes = "APP接口-创建订单")
    @PostMapping(value = "/creatOrder")
    public Result<?> creatOrder(@RequestParam(name = "vipFeeId") String vipFeeId,
                                @RequestParam(name = "discountCode") String discountCode,
                                @RequestParam(name = "museumId") String museumId,
                                @RequestParam(name = "userId") String userId,
                                HttpServletRequest req) {
        return orderService.creatOrder(vipFeeId, discountCode, museumId, userId);
    }

    @AutoLog(value = "APP接口-接收订单支付结果")
    @ApiOperation(value = "APP接口-接收订单支付结果", notes = "APP接口-接收订单支付结果")
    @PostMapping(value = "/notifyOrder")
    public Integer notifyOrder(@RequestParam(name = "OrderID") String orderId,
                               @RequestParam(name = "Status") String status,
                               @RequestParam(name = "Amount") String amount,
                               @RequestParam(name = "PayTimes") Long payTimes,
                               @RequestParam(name = "ErrCode") Long errCode,
                               @RequestParam(name = "ErrInfo") Long errInfo,
                               HttpServletRequest req) {
        JSONObject jsonObject = new JSONObject();
        for (String key : req.getParameterMap().keySet()) {
            jsonObject.put(key, req.getParameter(key));
        }
        log.info("接收支付结果：{}", jsonObject.toJSONString());
        return orderService.notifyOrder(jsonObject).isSuccess() ? 0 : 1;
    }

    @AutoLog(value = "APP接口-根据订单ID获取订单详情")
    @ApiOperation(value = "APP接口-根据订单ID获取订单详情", notes = "APP接口-根据订单ID获取订单详情")
    @PostMapping(value = "/getOrderInfo")
    public Result<?> getOrderInfo(@RequestParam(name = "orderId") String orderId,
                                  HttpServletRequest req) {

        return orderService.getOrderInfo(orderId);
    }

    @AutoLog(value = "APP接口-分页获取订单列表")
    @ApiOperation(value = "APP接口-分页获取订单列表", notes = "APP接口-分页获取订单列表")
    @PostMapping(value = "/getOrderList")
    public Result<?> getOrderList(@RequestParam(name = "userId") String userId,
                                  @RequestParam(name = "museumId") String museumId,
                                  @RequestParam(name = "pageNo") Integer pageNo,
                                  @RequestParam(name = "pageSize") Integer pageSize,
                                  HttpServletRequest req) {
        return orderService.getOrderList(userId, museumId, null, pageNo, pageSize);
    }

    @AutoLog(value = "APP接口-获取付款管理界面信息")
    @ApiOperation(value = "APP接口-获取付款管理界面信息", notes = "APP接口-获取付款管理界面信息")
    @PostMapping(value = "/getPayManagementInfo")
    public Result<?> getPayManagementInfo(@RequestParam(name = "userId") String userId,
                                          @RequestParam(name = "museumId") String museumId,
                                          HttpServletRequest req) {

        return userService.getPayManagementInfo(userId, museumId);
    }
    //**********************V2*********************/
    @AutoLog(value = "APP接口-获取房间列表V2")
    @ApiOperation(value = "APP接口-获取房间列表V2", notes = "APP接口-获取房间列表V2")
    @PostMapping(value = "/v2/getRoomList")
    public Result<List<ShowRoomVo2>> getRoomListV2(@RequestParam(name = "museumId") String museumId, HttpServletRequest req) {
        return roomService.getRoomListV2(museumId);
    }

    @AutoLog(value = "APP接口-查询当前展馆付费模式V2")
    @ApiOperation(value = "APP接口-查询当前展馆付费模式V2", notes = "APP接口-查询当前展馆付费模式V2")
    @PostMapping(value = "/v2/queryPayMode")
    public Result<?> queryPayModeV2(@RequestParam(name = "platform") String platform,
                                    @RequestParam(name = "museumId") String museumId,
                                    @RequestParam(name = "roomId") String roomId,
                                    HttpServletRequest req) {
        return vipFeeService.queryByMuseumIdV2(platform, museumId, roomId);
    }

    @AutoLog(value = "APP接口-创建订单V2")
    @ApiOperation(value = "APP接口-创建订单V2", notes = "APP接口-创建订单V2")
    @PostMapping(value = "/v2/creatOrder")
    public Result<?> creatOrderV2(@RequestParam(name = "vipFeeId") String vipFeeId,
                                  @RequestParam(name = "discountCode") String discountCode,
                                  @RequestParam(name = "platform") String platform,
                                  @RequestParam(name = "museumId") String museumId,
                                  @RequestParam(name = "roomId") String roomId,
                                  @RequestParam(name = "userId") String userId,
                                  HttpServletRequest req) {
        return orderService.creatOrderV2(vipFeeId, discountCode, platform, museumId, roomId, userId);
    }

    @AutoLog(value = "APP接口-接收订单支付结果V2")
    @ApiOperation(value = "APP接口-接收订单支付结果V2", notes = "APP接口-接收订单支付结果V2")
    @PostMapping(value = "/notifyOrderByApp")
    public Result<?> notifyOrderByApp(@RequestParam(name = "orderId") String orderId,
                                      @RequestParam(name = "amount", required = false) BigDecimal amount,
                                    HttpServletRequest req) {
        log.info("接收App支付结果：{}", orderId);
        return orderService.notifyOrderByApp(orderId, amount);
    }

    @AutoLog(value = "APP接口-用户鉴权接口V2")
    @ApiOperation(value = "APP接口-用户鉴权接口V2", notes = "APP接口-用户鉴权接口V2")
    @PostMapping(value = "/v2/checkAuth")
    public Result<?> checkAuthV2(@RequestParam(name = "userId") String userId,
                               @RequestParam(name = "museumId") String museumId,
                                 @RequestParam(name = "roomId") String roomId,
                               HttpServletRequest req) {
        return userService.checkAuthV2(userId, museumId, roomId);
    }
    @AutoLog(value = "APP接口-分页获取订单列表")
    @ApiOperation(value = "APP接口-分页获取订单列表", notes = "APP接口-分页获取订单列表")
    @PostMapping(value = "/v2/getOrderList")
    public Result<?> getOrderListV2(@RequestParam(name = "userId") String userId,
                                  @RequestParam(name = "museumId") String museumId,
                                    @RequestParam(name = "roomId") String roomId,
                                  @RequestParam(name = "pageNo") Integer pageNo,
                                  @RequestParam(name = "pageSize") Integer pageSize,
                                  HttpServletRequest req) {
        return orderService.getOrderList(userId, museumId, roomId, pageNo, pageSize);
    }

    @AutoLog(value = "APP接口-获取付款管理界面信息V2")
    @ApiOperation(value = "APP接口-获取付款管理界面信息V2", notes = "APP接口-获取付款管理界面信息V2")
    @PostMapping(value = "/v2/getPayManagementInfo")
    public Result<?> getPayManagementInfoV2(@RequestParam(name = "userId") String userId,
                                          @RequestParam(name = "museumId") String museumId,
                                            @RequestParam(name = "roomId") String roomId,
                                          HttpServletRequest req) {

        return userService.getPayManagementInfoV2(userId, museumId, roomId);
    }




//    @AutoLog(value = "APP接口-获取演出信息")
//    @ApiOperation(value="APP接口-获取演出信息", notes="APP接口-获取演出信息")
//    @PostMapping(value = "/getPerformList")
//    public Result<List<ShowPerform>> getPerformList(HttpServletRequest req) {
//        return performService.getPerformList();
//    }

//    @AutoLog(value = "APP接口-获取所有留言信息")
//    @ApiOperation(value="APP接口-获取所有留言信息", notes="APP接口-获取所有留言信息")
//    @PostMapping(value = "/getCommentList")
//    public Result<List<ShowComment>> getCommentList(HttpServletRequest req) {
//        return commentService.getCommentList();
//    }
//
//    @AutoLog(value = "APP接口-提交留言信息")
//    @ApiOperation(value="APP接口-提交留言信息", notes="APP接口-提交留言信息")
//    @PostMapping(value = "/submitComment")
//    public Result<?> submitComment(@RequestParam(name="userId") String userId,
//                                   @RequestParam(name="content") String content,
//                                   HttpServletRequest req) {
//        return commentService.submitComment(userId, content);
//    }
//
//    @AutoLog(value = "APP接口-定时提交观看时长")
//    @ApiOperation(value="APP接口-定时提交观看时长", notes="APP接口-定时提交观看时长")
//    @PostMapping(value = "/submitViewTime")
//    public Result<?> submitViewTime(@RequestParam(name="userId") String userId,
//                                    @RequestParam(name="dataId") String dataId,
//                                    @RequestParam(name="viewTime") Integer viewTime,
//                                    HttpServletRequest req) {
//        return userService.submitViewTime(userId, dataId, viewTime);
//    }
//    @AutoLog(value = "APP接口-用户观看演出接口")
//    @ApiOperation(value="APP接口-用户观看演出接口", notes="APP接口-用户观看演出接口")
//    @PostMapping(value = "/startViewPerform")
//    public Result<?> startViewPerform(@RequestParam(name="userId") String userId,
//                                      @RequestParam(name="performId") String performId,
//                                      HttpServletRequest req) {
//
//        return userService.startViewPerform(userId, performId);
//    }
}
