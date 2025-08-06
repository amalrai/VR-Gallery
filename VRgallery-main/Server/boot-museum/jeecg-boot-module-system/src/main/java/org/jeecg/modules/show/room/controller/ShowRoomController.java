package org.jeecg.modules.show.room.controller;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.extern.slf4j.Slf4j;
import org.apache.commons.lang3.StringUtils;
import org.apache.shiro.SecurityUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.aspect.annotation.AutoLog;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.config.mqtoken.UserTokenContext;
import org.jeecg.common.system.base.controller.JeecgController;
import org.jeecg.common.system.query.QueryGenerator;
import org.jeecg.common.system.vo.LoginUser;
import org.jeecg.modules.show.room.entity.ShowRoom;
import org.jeecg.modules.show.room.service.IShowRoomService;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.ModelAndView;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.util.Arrays;

/**
 * @Description: show_room
 * @Author: jeecg-boot
 * @Date: 2022-06-15
 * @Version: V1.0
 */
@Api(tags = "show_room")
@RestController
@RequestMapping("/room/showRoom")
@Slf4j
public class ShowRoomController extends JeecgController<ShowRoom, IShowRoomService> {
    @Autowired
    private IShowRoomService showRoomService;
    @Autowired
    public RedisTemplate redisTemplate;

    /**
     * 分页列表查询
     *
     * @param showRoom
     * @param pageNo
     * @param pageSize
     * @param req
     * @return
     */
    @AutoLog(value = "show_room-分页列表查询")
    @ApiOperation(value = "show_room-分页列表查询", notes = "show_room-分页列表查询")
    @GetMapping(value = "/list")
    public Result<?> queryPageList(ShowRoom showRoom,
                                   @RequestParam(name = "pageNo", defaultValue = "1") Integer pageNo,
                                   @RequestParam(name = "pageSize", defaultValue = "10") Integer pageSize,
                                   HttpServletRequest req) {
        QueryWrapper<ShowRoom> queryWrapper = QueryGenerator.initQueryWrapper(showRoom, req.getParameterMap());
        queryWrapper.eq("del_flag", 0);
        LoginUser sysUser = (LoginUser)SecurityUtils.getSubject().getPrincipal();
        if(StringUtils.isNotBlank(sysUser.getShowMuseumId())){
            queryWrapper.eq("show_museum_id", sysUser.getShowMuseumId());
        }
        Page<ShowRoom> page = new Page<ShowRoom>(pageNo, pageSize);
        IPage<ShowRoom> pageList = showRoomService.page(page, queryWrapper);
        return Result.OK(pageList).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
    }

    /**
     * 添加
     *
     * @param showRoom
     * @return
     */
    @AutoLog(value = "show_room-添加")
    @ApiOperation(value = "show_room-添加", notes = "show_room-添加")
    @PostMapping(value = "/add")
    public Result<?> add(@RequestBody ShowRoom showRoom) {
        showRoomService.save(showRoom);
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    /**
     * 编辑
     *
     * @param showRoom
     * @return
     */
    @AutoLog(value = "show_room-编辑")
    @ApiOperation(value = "show_room-编辑", notes = "show_room-编辑")
    @PutMapping(value = "/edit")
    public Result<?> edit(@RequestBody ShowRoom showRoom) {
        showRoomService.updateById(showRoom);
        deleteRedis(showRoom.getId());
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    /**
     * 通过id删除
     *
     * @param id
     * @return
     */
    @AutoLog(value = "show_room-通过id删除")
    @ApiOperation(value = "show_room-通过id删除", notes = "show_room-通过id删除")
    @DeleteMapping(value = "/delete")
    public Result<?> delete(@RequestParam(name = "id", required = true) String id) {
        showRoomService.removeById(id);
        deleteRedis(id);
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    /**
     * 批量删除
     *
     * @param ids
     * @return
     */
    @AutoLog(value = "show_room-批量删除")
    @ApiOperation(value = "show_room-批量删除", notes = "show_room-批量删除")
    @DeleteMapping(value = "/deleteBatch")
    public Result<?> deleteBatch(@RequestParam(name = "ids", required = true) String ids) {
        this.showRoomService.removeByIds(Arrays.asList(ids.split(",")));
        for (String id : ids.split(",")) {
            deleteRedis(id);
        }
        return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
    }

    /**
     * 通过id查询
     *
     * @param id
     * @return
     */
    @AutoLog(value = "show_room-通过id查询")
    @ApiOperation(value = "show_room-通过id查询", notes = "show_room-通过id查询")
    @GetMapping(value = "/queryById")
    public Result<?> queryById(@RequestParam(name = "id", required = true) String id) {
        ShowRoom showRoom = showRoomService.getById(id);
        if (showRoom == null) {
            return Result.error(MessageUtil.getMessage("未找到对应数据", UserLocalContext.getLocal()));
        }
        return Result.OK(showRoom).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
    }

    /**
     * 导出excel
     *
     * @param request
     * @param showRoom
     */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowRoom showRoom) {
        return super.exportXls(request, showRoom, ShowRoom.class, "show_room");
    }

    /**
     * 通过excel导入数据
     *
     * @param request
     * @param response
     * @return
     */
    @RequestMapping(value = "/importExcel", method = RequestMethod.POST)
    public Result<?> importExcel(HttpServletRequest request, HttpServletResponse response) {
        return super.importExcel(request, response, ShowRoom.class);
    }

    private void deleteRedis(String id){
        String keyString = String.format("sys:cache:dictTable::SimpleKey [show_room,name,id,%s]", id);
        redisTemplate.delete(keyString);
    }
}
