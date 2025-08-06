package org.jeecg.modules.show.perform.controller;

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
import org.jeecg.modules.show.perform.entity.ShowPerform;
import org.jeecg.modules.show.perform.service.IShowPerformService;

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
 * @Description: 演出管理
 * @Author: jeecg-boot
 * @Date:   2021-10-16
 * @Version: V1.0
 */
@Api(tags="演出管理")
@RestController
@RequestMapping("/perform/showPerform")
@Slf4j
public class ShowPerformController extends JeecgController<ShowPerform, IShowPerformService> {
	@Autowired
	private IShowPerformService showPerformService;

	/**
	 * 分页列表查询
	 *
	 * @param showPerform
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "演出管理-分页列表查询")
	@ApiOperation(value="演出管理-分页列表查询", notes="演出管理-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowPerform showPerform,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowPerform> queryWrapper = QueryGenerator.initQueryWrapper(showPerform, req.getParameterMap());
		Page<ShowPerform> page = new Page<ShowPerform>(pageNo, pageSize);
		IPage<ShowPerform> pageList = showPerformService.page(page, queryWrapper);
		return Result.OK(pageList);
	}

	/**
	 *   添加
	 *
	 * @param showPerform
	 * @return
	 */
	@AutoLog(value = "演出管理-添加")
	@ApiOperation(value="演出管理-添加", notes="演出管理-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowPerform showPerform) {
		return showPerformService.doAdd(showPerform);
	}

	/**
	 *  编辑
	 *
	 * @param showPerform
	 * @return
	 */
	@AutoLog(value = "演出管理-编辑")
	@ApiOperation(value="演出管理-编辑", notes="演出管理-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowPerform showPerform) {
		return showPerformService.doUpdate(showPerform);
	}

	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "演出管理-通过id删除")
	@ApiOperation(value="演出管理-通过id删除", notes="演出管理-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showPerformService.removeById(id);
		return Result.OK("删除成功!");
	}

	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "演出管理-批量删除")
	@ApiOperation(value="演出管理-批量删除", notes="演出管理-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showPerformService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK("批量删除成功!");
	}

	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "演出管理-通过id查询")
	@ApiOperation(value="演出管理-通过id查询", notes="演出管理-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowPerform showPerform = showPerformService.getById(id);
		if(showPerform==null) {
			return Result.error("未找到对应数据");
		}
		return Result.OK(showPerform);
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showPerform
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowPerform showPerform) {
        return super.exportXls(request, showPerform, ShowPerform.class, "演出管理");
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
        return super.importExcel(request, response, ShowPerform.class);
    }

}
