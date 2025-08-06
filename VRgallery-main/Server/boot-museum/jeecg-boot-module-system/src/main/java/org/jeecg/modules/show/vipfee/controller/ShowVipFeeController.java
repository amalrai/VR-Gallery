package org.jeecg.modules.show.vipfee.controller;

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
import org.jeecg.common.util.oConvertUtils;
import org.jeecg.modules.show.room.entity.ShowRoom;
import org.jeecg.modules.show.vipfee.entity.ShowVipFee;
import org.jeecg.modules.show.vipfee.service.IShowVipFeeService;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import lombok.extern.slf4j.Slf4j;

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
 * @Description: 付费管理
 * @Author: jeecg-boot
 * @Date:   2023-04-02
 * @Version: V1.0
 */
@Api(tags="付费管理")
@RestController
@RequestMapping("/vipfee/showVipFee")
@Slf4j
public class ShowVipFeeController extends JeecgController<ShowVipFee, IShowVipFeeService> {
	@Autowired
	private IShowVipFeeService showVipFeeService;

	/**
	 * 分页列表查询
	 *
	 * @param showVipFee
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "付费管理-分页列表查询")
	@ApiOperation(value="付费管理-分页列表查询", notes="付费管理-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowVipFee showVipFee,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowVipFee> queryWrapper = QueryGenerator.initQueryWrapper(showVipFee, req.getParameterMap());
		queryWrapper.eq("del_flag", 0);
		LoginUser sysUser = (LoginUser) SecurityUtils.getSubject().getPrincipal();
		if(StringUtils.isNotBlank(sysUser.getShowMuseumId())){
			queryWrapper.eq("show_museum_id", sysUser.getShowMuseumId());
		}
		Page<ShowVipFee> page = new Page<ShowVipFee>(pageNo, pageSize);
		IPage<ShowVipFee> pageList = showVipFeeService.page(page, queryWrapper);
		return Result.OK(pageList).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
	}

	/**
	 *   添加
	 *
	 * @param showVipFee
	 * @return
	 */
	@AutoLog(value = "付费管理-添加")
	@ApiOperation(value="付费管理-添加", notes="付费管理-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowVipFee showVipFee) {
		showVipFeeService.save(showVipFee);
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	/**
	 *  编辑
	 *
	 * @param showVipFee
	 * @return
	 */
	@AutoLog(value = "付费管理-编辑")
	@ApiOperation(value="付费管理-编辑", notes="付费管理-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowVipFee showVipFee) {
		showVipFeeService.updateById(showVipFee);
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "付费管理-通过id删除")
	@ApiOperation(value="付费管理-通过id删除", notes="付费管理-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showVipFeeService.removeById(id);
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "付费管理-批量删除")
	@ApiOperation(value="付费管理-批量删除", notes="付费管理-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showVipFeeService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK().success(MessageUtil.getMessage("操作成功！", UserLocalContext.getLocal()));
	}

	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "付费管理-通过id查询")
	@ApiOperation(value="付费管理-通过id查询", notes="付费管理-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowVipFee showVipFee = showVipFeeService.getById(id);
		if(showVipFee==null) {
			return Result.error(MessageUtil.getMessage("未找到对应数据", UserLocalContext.getLocal()));
		}
		return Result.OK(showVipFee).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showVipFee
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowVipFee showVipFee) {
        return super.exportXls(request, showVipFee, ShowVipFee.class, MessageUtil.getMessage("付费管理", UserLocalContext.getLocal()));
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
        return super.importExcel(request, response, ShowVipFee.class);
    }

}
