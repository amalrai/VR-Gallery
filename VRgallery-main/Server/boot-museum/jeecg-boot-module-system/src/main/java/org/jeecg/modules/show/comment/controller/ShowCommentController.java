package org.jeecg.modules.show.comment.controller;

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
import org.jeecg.modules.show.comment.entity.ShowComment;
import org.jeecg.modules.show.comment.service.IShowCommentService;

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
 * @Description: 留言管理
 * @Author: jeecg-boot
 * @Date:   2021-10-16
 * @Version: V1.0
 */
@Api(tags="留言管理")
@RestController
@RequestMapping("/comment/showComment")
@Slf4j
public class ShowCommentController extends JeecgController<ShowComment, IShowCommentService> {
	@Autowired
	private IShowCommentService showCommentService;
	
	/**
	 * 分页列表查询
	 *
	 * @param showComment
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "留言管理-分页列表查询")
	@ApiOperation(value="留言管理-分页列表查询", notes="留言管理-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowComment showComment,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowComment> queryWrapper = QueryGenerator.initQueryWrapper(showComment, req.getParameterMap());
		Page<ShowComment> page = new Page<ShowComment>(pageNo, pageSize);
		IPage<ShowComment> pageList = showCommentService.page(page, queryWrapper);
		return Result.OK(pageList);
	}
	
	/**
	 *   添加
	 *
	 * @param showComment
	 * @return
	 */
	@AutoLog(value = "留言管理-添加")
	@ApiOperation(value="留言管理-添加", notes="留言管理-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowComment showComment) {
		showCommentService.save(showComment);
		return Result.OK("添加成功！");
	}
	
	/**
	 *  编辑
	 *
	 * @param showComment
	 * @return
	 */
	@AutoLog(value = "留言管理-编辑")
	@ApiOperation(value="留言管理-编辑", notes="留言管理-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowComment showComment) {
		showCommentService.updateById(showComment);
		return Result.OK("编辑成功!");
	}
	
	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "留言管理-通过id删除")
	@ApiOperation(value="留言管理-通过id删除", notes="留言管理-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showCommentService.removeById(id);
		return Result.OK("删除成功!");
	}
	
	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "留言管理-批量删除")
	@ApiOperation(value="留言管理-批量删除", notes="留言管理-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showCommentService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK("批量删除成功!");
	}
	
	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "留言管理-通过id查询")
	@ApiOperation(value="留言管理-通过id查询", notes="留言管理-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowComment showComment = showCommentService.getById(id);
		if(showComment==null) {
			return Result.error("未找到对应数据");
		}
		return Result.OK(showComment);
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showComment
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowComment showComment) {
        return super.exportXls(request, showComment, ShowComment.class, "留言管理");
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
        return super.importExcel(request, response, ShowComment.class);
    }

}
