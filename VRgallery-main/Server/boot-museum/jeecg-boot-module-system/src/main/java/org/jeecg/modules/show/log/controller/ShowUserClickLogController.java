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
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.system.query.QueryGenerator;
import org.jeecg.common.util.oConvertUtils;
import org.jeecg.modules.show.log.entity.ShowUserClickLog;
import org.jeecg.modules.show.log.service.IShowUserClickLogService;

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
 * @Description: show_user_click_log
 * @Author: jeecg-boot
 * @Date:   2023-03-26
 * @Version: V1.0
 */
@Api(tags="show_user_click_log")
@RestController
@RequestMapping("/log/showUserClickLog")
@Slf4j
public class ShowUserClickLogController extends JeecgController<ShowUserClickLog, IShowUserClickLogService> {
	@Autowired
	private IShowUserClickLogService showUserClickLogService;
	
	/**
	 * 分页列表查询
	 *
	 * @param showUserClickLog
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "show_user_click_log-分页列表查询")
	@ApiOperation(value="show_user_click_log-分页列表查询", notes="show_user_click_log-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowUserClickLog showUserClickLog,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowUserClickLog> queryWrapper = QueryGenerator.initQueryWrapper(showUserClickLog, req.getParameterMap());
		Page<ShowUserClickLog> page = new Page<ShowUserClickLog>(pageNo, pageSize);
		IPage<ShowUserClickLog> pageList = showUserClickLogService.page(page, queryWrapper);
		return Result.OK(pageList);
	}
	
	/**
	 *   添加
	 *
	 * @param showUserClickLog
	 * @return
	 */
	@AutoLog(value = "show_user_click_log-添加")
	@ApiOperation(value="show_user_click_log-添加", notes="show_user_click_log-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowUserClickLog showUserClickLog) {
		showUserClickLogService.save(showUserClickLog);
		return Result.OK("添加成功！");
	}
	
	/**
	 *  编辑
	 *
	 * @param showUserClickLog
	 * @return
	 */
	@AutoLog(value = "show_user_click_log-编辑")
	@ApiOperation(value="show_user_click_log-编辑", notes="show_user_click_log-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowUserClickLog showUserClickLog) {
		showUserClickLogService.updateById(showUserClickLog);
		return Result.OK("编辑成功!");
	}
	
	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "show_user_click_log-通过id删除")
	@ApiOperation(value="show_user_click_log-通过id删除", notes="show_user_click_log-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showUserClickLogService.removeById(id);
		return Result.OK("删除成功!");
	}
	
	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "show_user_click_log-批量删除")
	@ApiOperation(value="show_user_click_log-批量删除", notes="show_user_click_log-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showUserClickLogService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK("批量删除成功!");
	}
	
	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "show_user_click_log-通过id查询")
	@ApiOperation(value="show_user_click_log-通过id查询", notes="show_user_click_log-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowUserClickLog showUserClickLog = showUserClickLogService.getById(id);
		if(showUserClickLog==null) {
			return Result.error("未找到对应数据");
		}
		return Result.OK(showUserClickLog);
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showUserClickLog
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowUserClickLog showUserClickLog) {
        return super.exportXls(request, showUserClickLog, ShowUserClickLog.class, "show_user_click_log");
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
        return super.importExcel(request, response, ShowUserClickLog.class);
    }

}
