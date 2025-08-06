package org.jeecg.modules.show.museum.controller;

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
import org.jeecg.modules.show.museum.entity.ShowMuseum;
import org.jeecg.modules.show.museum.service.IShowMuseumService;

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
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.multipart.MultipartHttpServletRequest;
import org.springframework.web.servlet.ModelAndView;
import com.alibaba.fastjson.JSON;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.jeecg.common.aspect.annotation.AutoLog;

 /**
 * @Description: show_museum
 * @Author: jeecg-boot
 * @Date:   2022-10-24
 * @Version: V1.0
 */
@Api(tags="show_museum")
@RestController
@RequestMapping("/museum/showMuseum")
@Slf4j
public class ShowMuseumController extends JeecgController<ShowMuseum, IShowMuseumService> {
	@Autowired
	private IShowMuseumService showMuseumService;
	@Autowired
	public RedisTemplate redisTemplate;

	/**
	 * 分页列表查询
	 *
	 * @param showMuseum
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "show_museum-分页列表查询")
	@ApiOperation(value="show_museum-分页列表查询", notes="show_museum-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowMuseum showMuseum,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowMuseum> queryWrapper = QueryGenerator.initQueryWrapper(showMuseum, req.getParameterMap());
		Page<ShowMuseum> page = new Page<ShowMuseum>(pageNo, pageSize);
		IPage<ShowMuseum> pageList = showMuseumService.page(page, queryWrapper);
		return Result.OK(pageList);
	}

	/**
	 *   添加
	 *
	 * @param showMuseum
	 * @return
	 */
	@AutoLog(value = "show_museum-添加")
	@ApiOperation(value="show_museum-添加", notes="show_museum-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowMuseum showMuseum) {
		showMuseumService.save(showMuseum);
		return Result.OK("添加成功！");
	}

	/**
	 *  编辑
	 *
	 * @param showMuseum
	 * @return
	 */
	@AutoLog(value = "show_museum-编辑")
	@ApiOperation(value="show_museum-编辑", notes="show_museum-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowMuseum showMuseum) {
		showMuseumService.updateById(showMuseum);
		deleteRedis(showMuseum.getId());
		return Result.OK("编辑成功!");
	}

	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "show_museum-通过id删除")
	@ApiOperation(value="show_museum-通过id删除", notes="show_museum-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showMuseumService.removeById(id);
		deleteRedis(id);
		return Result.OK("删除成功!");
	}

	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "show_museum-批量删除")
	@ApiOperation(value="show_museum-批量删除", notes="show_museum-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showMuseumService.removeByIds(Arrays.asList(ids.split(",")));
		for (String id : ids.split(",")) {
			deleteRedis(id);
		}
		return Result.OK("批量删除成功!");
	}

	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "show_museum-通过id查询")
	@ApiOperation(value="show_museum-通过id查询", notes="show_museum-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowMuseum showMuseum = showMuseumService.getById(id);
		if(showMuseum==null) {
			return Result.error("未找到对应数据");
		}
		return Result.OK(showMuseum);
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showMuseum
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowMuseum showMuseum) {
        return super.exportXls(request, showMuseum, ShowMuseum.class, "show_museum");
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
        return super.importExcel(request, response, ShowMuseum.class);
    }

	 private void deleteRedis(String id){
		 String keyString = String.format("sys:cache:dictTable::SimpleKey [show_museum,name,id,%s]", id);
		 redisTemplate.delete(keyString);
	 }

}
