package org.jeecg.modules.show.exhibits.controller;

import java.util.Arrays;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.system.query.QueryGenerator;
import org.jeecg.modules.show.exhibits.util.ZipUtils;
import org.jeecg.modules.show.exhibits.entity.ShowExhibits;
import org.jeecg.modules.show.exhibits.service.IShowExhibitsService;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import lombok.extern.slf4j.Slf4j;

import org.jeecg.modules.system.util.MessageUtil;
import org.jeecg.common.system.base.controller.JeecgController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.multipart.MultipartFile;
import org.springframework.web.multipart.MultipartHttpServletRequest;
import org.springframework.web.servlet.ModelAndView;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.jeecg.common.aspect.annotation.AutoLog;

 /**
 * @Description: 展品管理
 * @Author: jeecg-boot
 * @Date:   2022-06-15
 * @Version: V1.0
 */
@Api(tags="展品管理")
@RestController
@RequestMapping("/exhibits/showExhibits")
@Slf4j
public class ShowExhibitsController extends JeecgController<ShowExhibits, IShowExhibitsService> {
	@Autowired
	private IShowExhibitsService showExhibitsService;

	/**
	 * 分页列表查询
	 *
	 * @param showExhibits
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "展品管理-分页列表查询")
	@ApiOperation(value="展品管理-分页列表查询", notes="展品管理-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowExhibits showExhibits,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowExhibits> queryWrapper = QueryGenerator.initQueryWrapper(showExhibits, req.getParameterMap());
		Page<ShowExhibits> page = new Page<ShowExhibits>(pageNo, pageSize);
		IPage<ShowExhibits> pageList = showExhibitsService.page(page, queryWrapper);
		return Result.OK(pageList).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
	}

	/**
	 *   添加
	 *
	 * @param showExhibits
	 * @return
	 */
	@AutoLog(value = "展品管理-添加")
	@ApiOperation(value="展品管理-添加", notes="展品管理-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowExhibits showExhibits) {
		return showExhibitsService.batchAddExhibits(showExhibits);
	}

	/**
	 *  编辑
	 *
	 * @param showExhibits
	 * @return
	 */
	@AutoLog(value = "展品管理-编辑")
	@ApiOperation(value="展品管理-编辑", notes="展品管理-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowExhibits showExhibits) {
		return  showExhibitsService.editExhibits(showExhibits);
	}

	 /**
	  *  交换
	  *
	  * @param showExhibits
	  * @return
	  */
	 @AutoLog(value = "展品管理-替换")
	 @ApiOperation(value="展品管理-替换", notes="展品管理-替换")
	 @DeleteMapping(value = "/replace")
	 public Result<?> replace(@RequestParam(name="exhibitsId",required=true)String exhibitsId,
							  @RequestParam(name="replaceExhibitsId",required=true)String replaceExhibitsId) {
		 return  showExhibitsService.replaceExhibits(exhibitsId, replaceExhibitsId);
	 }

	 /**
	  *  批量修改状态
	  *
	  * @param ids
	  * @return
	  */
	 @AutoLog(value = "展品管理-批量修改状态")
	 @ApiOperation(value="展品管理-批量修改状态", notes="展品管理-批量修改状态")
	 @DeleteMapping(value = "/batchUpdateStatus")
	 public Result<?> batchUpdateStatus(@RequestParam(name="ids",required=true) String ids, @RequestParam(name="status",required=true)Integer status) {
		 return showExhibitsService.batchUpdateStatus(Arrays.asList(ids.split(",")), status);
	 }

	 /**
	  *  批量修改权限
	  *
	  * @param ids
	  * @return
	  */
	 @AutoLog(value = "展品管理-批量修改权限")
	 @ApiOperation(value="展品管理-批量修改权限", notes="展品管理-批量修改权限")
	 @DeleteMapping(value = "/batchUpdateAuth")
	 public Result<?> batchUpdateAuth(@RequestParam(name="ids",required=true) String ids, Integer auth) {
		 return showExhibitsService.batchUpdateAuth(Arrays.asList(ids.split(",")), auth);
	 }

	 /**
	  *  批量清空
	  *
	  * @param ids
	  * @return
	  */
	 @AutoLog(value = "展品管理-批量清空")
	 @ApiOperation(value="展品管理-批量清空", notes="展品管理-批量清空")
	 @DeleteMapping(value = "/batchClear")
	 public Result<?> batchClear(@RequestParam(name="ids",required=true) String ids, Integer auth) {
		 return showExhibitsService.batchClear(Arrays.asList(ids.split(",")));
	 }

	 /**
	  *   通过id清空
	  *
	  * @param id
	  * @return
	  */
	 @AutoLog(value = "展品管理-通过id清空")
	 @ApiOperation(value="展品管理-通过id清空", notes="展品管理-通过id清空")
	 @DeleteMapping(value = "/clear")
	 public Result<?> clear(@RequestParam(name="id",required=true) String id) {
		 return showExhibitsService.clear(id);
	 }

	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "展品管理-通过id删除")
	@ApiOperation(value="展品管理-通过id删除", notes="展品管理-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showExhibitsService.removeById(id);
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "展品管理-批量删除")
	@ApiOperation(value="展品管理-批量删除", notes="展品管理-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showExhibitsService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "展品管理-通过id查询")
	@ApiOperation(value="展品管理-通过id查询", notes="展品管理-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowExhibits showExhibits = showExhibitsService.getById(id);
		if(showExhibits==null) {
			return Result.error(MessageUtil.getMessage("未找到对应数据", UserLocalContext.getLocal()));
		}
		return Result.OK(showExhibits).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showExhibits
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowExhibits showExhibits) {
        return super.exportXls(request, showExhibits, ShowExhibits.class, "展品管理");
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
        return super.importExcel(request, response, ShowExhibits.class);
    }

	 @PostMapping(value = "/uploadFrameAnimation")
	 public Result<?> uploadFrameAnimation(HttpServletRequest request, HttpServletResponse response) {
		 String bizPath = request.getParameter("biz");
		 boolean isEncode = false;
		 String isEncodeStr = request.getParameter("isEncode");
		 if("1".equals(isEncodeStr)){
			 isEncode = true;
		 }
		 MultipartHttpServletRequest multipartRequest = (MultipartHttpServletRequest) request;
		 MultipartFile file = multipartRequest.getFile("file");// 获取上传文件对象
		 if(file == null){
		 	return Result.error("请选择文件！");
		 }
		 return ZipUtils.unzipAndUpload(file, bizPath, isEncode);
	 }

}
