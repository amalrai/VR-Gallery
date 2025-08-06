package org.jeecg.modules.show.user.controller;

import java.util.Arrays;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;
import java.io.IOException;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.commons.lang3.StringUtils;
import org.apache.shiro.SecurityUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.system.query.QueryGenerator;
import org.jeecg.common.system.vo.LoginUser;
import org.jeecg.common.util.SpringContextHolder;
import org.jeecg.common.util.oConvertUtils;
import org.jeecg.modules.show.museum.entity.ShowMuseum;
import org.jeecg.modules.show.museum.mapper.ShowMuseumMapper;
import org.jeecg.modules.show.user.entity.ShowUser;
import org.jeecg.modules.show.user.service.IShowUserService;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import lombok.extern.slf4j.Slf4j;

import org.jeecg.modules.system.util.ExcelLangUtils;
import org.jeecg.modules.system.util.MessageUtil;
import org.jeecgframework.poi.excel.ExcelImportUtil;
import org.jeecgframework.poi.excel.def.NormalExcelConstants;
import org.jeecgframework.poi.excel.entity.ExportParams;
import org.jeecgframework.poi.excel.entity.ImportParams;
import org.jeecgframework.poi.excel.view.JeecgEntityExcelView;
import org.jeecg.common.system.base.controller.JeecgController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.multipart.MultipartHttpServletRequest;
import org.springframework.web.servlet.ModelAndView;
import com.alibaba.fastjson.JSON;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.jeecg.common.aspect.annotation.AutoLog;

 /**
 * @Description: 用户管理
 * @Author: jeecg-boot
 * @Date:   2021-10-15
 * @Version: V1.0
 */
@Api(tags="用户管理")
@RestController
@RequestMapping("/user/showUser")
@Slf4j
public class ShowUserController extends JeecgController<ShowUser, IShowUserService> {
	@Autowired
	private IShowUserService showUserService;

	/**
	 * 分页列表查询
	 *
	 * @param showUser
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "用户管理-分页列表查询")
	@ApiOperation(value="用户管理-分页列表查询", notes="用户管理-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowUser showUser,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowUser> queryWrapper = QueryGenerator.initQueryWrapper(showUser, req.getParameterMap());
		LoginUser sysUser = (LoginUser) SecurityUtils.getSubject().getPrincipal();
		if(StringUtils.isNotBlank(sysUser.getShowMuseumId())){
			ShowMuseumMapper mapper = SpringContextHolder.getBean(ShowMuseumMapper.class);
			ShowMuseum museum = mapper.selectById(sysUser.getShowMuseumId());
			queryWrapper.eq("show_app_id", museum.getShowAppId());
		}
		Page<ShowUser> page = new Page<ShowUser>(pageNo, pageSize);
		IPage<ShowUser> pageList = showUserService.page(page, queryWrapper);
		return Result.OK(pageList).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
	}

	/**
	 *   添加
	 *
	 * @param showUser
	 * @return
	 */
	@AutoLog(value = "用户管理-添加")
	@ApiOperation(value="用户管理-添加", notes="用户管理-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowUser showUser) {
		showUserService.save(showUser);
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	/**
	 *  编辑
	 *
	 * @param showUser
	 * @return
	 */
	@AutoLog(value = "用户管理-编辑")
	@ApiOperation(value="用户管理-编辑", notes="用户管理-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowUser showUser) {
		showUserService.updateById(showUser);
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "用户管理-通过id删除")
	@ApiOperation(value="用户管理-通过id删除", notes="用户管理-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showUserService.removeById(id);
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "用户管理-批量删除")
	@ApiOperation(value="用户管理-批量删除", notes="用户管理-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showUserService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	 @AutoLog(value = "用户管理-批量开启")
	 @ApiOperation(value="用户管理-批量开启", notes="用户管理-批量开启")
	 @PostMapping(value = "/batchOpen")
	 public Result<?> batchOpen(@RequestParam(name="ids",required=true) String ids) {
		 return showUserService.updateAuthByIds(Arrays.asList(ids.split(",")), 1);
	 }
	 @AutoLog(value = "用户管理-批量关闭")
	 @ApiOperation(value="用户管理-批量关闭", notes="用户管理-批量关闭")
	 @PostMapping(value = "/batchClose")
	 public Result<?> batchClose(@RequestParam(name="ids",required=true) String ids) {
		 return showUserService.updateAuthByIds(Arrays.asList(ids.split(",")), 0);
	 }
	 @AutoLog(value = "用户管理-批量启用")
	 @ApiOperation(value="用户管理-批量启用", notes="用户管理-批量启用")
	 @PostMapping(value = "/batchEnable")
	 public Result<?> batchEnable(@RequestParam(name="ids",required=true) String ids) {
		 return showUserService.updateStateByIds(Arrays.asList(ids.split(",")), 1);
	 }
	 @AutoLog(value = "用户管理-批量禁用 ")
	 @ApiOperation(value="用户管理-批量禁用", notes="用户管理-批量禁用")
	 @PostMapping(value = "/batchDisable")
	 public Result<?> batchDisable(@RequestParam(name="ids",required=true) String ids) {
		 return showUserService.updateStateByIds(Arrays.asList(ids.split(",")), 0);
	 }

	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "用户管理-通过id查询")
	@ApiOperation(value="用户管理-通过id查询", notes="用户管理-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowUser showUser = showUserService.getById(id);
		if(showUser==null) {
			return Result.error(MessageUtil.getMessage("未找到对应数据", UserLocalContext.getLocal()));
		}
		return Result.OK(showUser).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
	}


	 @AutoLog(value = "用户管理-查询游客登录次数")
	 @ApiOperation(value="用户管理-查询游客登录次数", notes="用户管理-查询游客登录次数")
	 @GetMapping(value = "/queryVisitorCount")
	 public Result<?> queryVisitorCount() {
		 Integer count = showUserService.queryVisitorCount();
		 return Result.OK(count).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
	 }

    /**
    * 导出excel
    *
    * @param request
    * @param showUser
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowUser showUser) throws NoSuchFieldException, IllegalAccessException {
        return super.exportXls(request, showUser, ExcelLangUtils.chooseLang(ShowUser.class, UserLocalContext.getLocal()), MessageUtil.getMessage("用户管理", UserLocalContext.getLocal()));
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
        return super.importExcel(request, response, ShowUser.class);
    }

}
