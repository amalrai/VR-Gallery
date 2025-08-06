package org.jeecg.modules.show.log.controller;

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
import org.jeecg.common.system.query.QueryGenerator;
import org.jeecg.common.system.vo.LoginUser;
import org.jeecg.common.util.oConvertUtils;
import org.jeecg.modules.show.log.entity.ShowUserLoginLog;
import org.jeecg.modules.show.log.service.IShowUserLoginLogService;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import lombok.extern.slf4j.Slf4j;

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
 * @Description: 登录日志
 * @Author: jeecg-boot
 * @Date:   2023-03-26
 * @Version: V1.0
 */
@Api(tags="登录日志")
@RestController
@RequestMapping("/log/showUserLoginLog")
@Slf4j
public class ShowUserLoginLogController extends JeecgController<ShowUserLoginLog, IShowUserLoginLogService> {
	@Autowired
	private IShowUserLoginLogService showUserLoginLogService;

	/**
	 * 分页列表查询
	 *
	 * @param showUserLoginLog
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "登录日志-分页列表查询")
	@ApiOperation(value="登录日志-分页列表查询", notes="登录日志-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowUserLoginLog showUserLoginLog,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowUserLoginLog> queryWrapper = QueryGenerator.initQueryWrapper(showUserLoginLog, req.getParameterMap());
		LoginUser sysUser = (LoginUser) SecurityUtils.getSubject().getPrincipal();
		if(StringUtils.isNotBlank(sysUser.getShowMuseumId())){
			queryWrapper.eq("show_museum_id", sysUser.getShowMuseumId());
		}
		Page<ShowUserLoginLog> page = new Page<ShowUserLoginLog>(pageNo, pageSize);
		IPage<ShowUserLoginLog> pageList = showUserLoginLogService.page(page, queryWrapper);
		return Result.OK(pageList);
	}

	/**
	 *   添加
	 *
	 * @param showUserLoginLog
	 * @return
	 */
	@AutoLog(value = "登录日志-添加")
	@ApiOperation(value="登录日志-添加", notes="登录日志-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowUserLoginLog showUserLoginLog) {
		showUserLoginLogService.save(showUserLoginLog);
		return Result.OK("添加成功！");
	}

	/**
	 *  编辑
	 *
	 * @param showUserLoginLog
	 * @return
	 */
	@AutoLog(value = "登录日志-编辑")
	@ApiOperation(value="登录日志-编辑", notes="登录日志-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowUserLoginLog showUserLoginLog) {
		showUserLoginLogService.updateById(showUserLoginLog);
		return Result.OK("编辑成功!");
	}

	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "登录日志-通过id删除")
	@ApiOperation(value="登录日志-通过id删除", notes="登录日志-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showUserLoginLogService.removeById(id);
		return Result.OK("删除成功!");
	}

	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "登录日志-批量删除")
	@ApiOperation(value="登录日志-批量删除", notes="登录日志-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showUserLoginLogService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK("批量删除成功!");
	}

	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "登录日志-通过id查询")
	@ApiOperation(value="登录日志-通过id查询", notes="登录日志-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowUserLoginLog showUserLoginLog = showUserLoginLogService.getById(id);
		if(showUserLoginLog==null) {
			return Result.error("未找到对应数据");
		}
		return Result.OK(showUserLoginLog);
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showUserLoginLog
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowUserLoginLog showUserLoginLog) {
        return super.exportXls(request, showUserLoginLog, ShowUserLoginLog.class, "登录日志");
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
        return super.importExcel(request, response, ShowUserLoginLog.class);
    }

}
