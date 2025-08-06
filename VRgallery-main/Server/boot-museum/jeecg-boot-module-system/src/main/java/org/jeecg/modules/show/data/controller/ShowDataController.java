package org.jeecg.modules.show.data.controller;

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
import org.jeecg.modules.show.data.entity.ShowData;
import org.jeecg.modules.show.data.service.IShowDataService;

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
 * @Description: 数据管理
 * @Author: jeecg-boot
 * @Date:   2021-10-16
 * @Version: V1.0
 */
@Api(tags="数据管理")
@RestController
@RequestMapping("/data/showData")
@Slf4j
public class ShowDataController extends JeecgController<ShowData, IShowDataService> {
	@Autowired
	private IShowDataService showDataService;
	
	/**
	 * 分页列表查询
	 *
	 * @param showData
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "数据管理-分页列表查询")
	@ApiOperation(value="数据管理-分页列表查询", notes="数据管理-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowData showData,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowData> queryWrapper = QueryGenerator.initQueryWrapper(showData, req.getParameterMap());
		Page<ShowData> page = new Page<ShowData>(pageNo, pageSize);
		IPage<ShowData> pageList = showDataService.page(page, queryWrapper);
		return Result.OK(pageList);
	}
	
	/**
	 *   添加
	 *
	 * @param showData
	 * @return
	 */
	@AutoLog(value = "数据管理-添加")
	@ApiOperation(value="数据管理-添加", notes="数据管理-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowData showData) {
		showDataService.save(showData);
		return Result.OK("添加成功！");
	}
	
	/**
	 *  编辑
	 *
	 * @param showData
	 * @return
	 */
	@AutoLog(value = "数据管理-编辑")
	@ApiOperation(value="数据管理-编辑", notes="数据管理-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowData showData) {
		showDataService.updateById(showData);
		return Result.OK("编辑成功!");
	}
	
	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "数据管理-通过id删除")
	@ApiOperation(value="数据管理-通过id删除", notes="数据管理-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showDataService.removeById(id);
		return Result.OK("删除成功!");
	}
	
	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "数据管理-批量删除")
	@ApiOperation(value="数据管理-批量删除", notes="数据管理-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showDataService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK("批量删除成功!");
	}
	
	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "数据管理-通过id查询")
	@ApiOperation(value="数据管理-通过id查询", notes="数据管理-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowData showData = showDataService.getById(id);
		if(showData==null) {
			return Result.error("未找到对应数据");
		}
		return Result.OK(showData);
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showData
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowData showData) {
        return super.exportXls(request, showData, ShowData.class, "数据管理");
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
        return super.importExcel(request, response, ShowData.class);
    }

}
