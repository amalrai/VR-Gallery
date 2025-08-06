package org.jeecg.modules.show.order.controller;

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
import org.jeecg.modules.show.order.entity.ShowOrder;
import org.jeecg.modules.show.order.service.IShowOrderService;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import lombok.extern.slf4j.Slf4j;

import org.jeecg.modules.show.user.entity.ShowUser;
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
 * @Description: 订单管理
 * @Author: jeecg-boot
 * @Date:   2023-04-02
 * @Version: V1.0
 */
@Api(tags="订单管理")
@RestController
@RequestMapping("/order/showOrder")
@Slf4j
public class ShowOrderController extends JeecgController<ShowOrder, IShowOrderService> {
	@Autowired
	private IShowOrderService showOrderService;

	/**
	 * 分页列表查询
	 *
	 * @param showOrder
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "订单管理-分页列表查询")
	@ApiOperation(value="订单管理-分页列表查询", notes="订单管理-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowOrder showOrder,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowOrder> queryWrapper = QueryGenerator.initQueryWrapper(showOrder, req.getParameterMap());
		LoginUser sysUser = (LoginUser) SecurityUtils.getSubject().getPrincipal();
		if(StringUtils.isNotBlank(sysUser.getShowMuseumId())){
			queryWrapper.eq("show_museum_id", sysUser.getShowMuseumId());
		}
		Page<ShowOrder> page = new Page<ShowOrder>(pageNo, pageSize);
		IPage<ShowOrder> pageList = showOrderService.page(page, queryWrapper);
		return Result.OK(pageList).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
	}
	 @AutoLog(value = "订单管理-交易总金额查询")
	 @ApiOperation(value="订单管理-交易总金额查询", notes="订单管理-交易总金额查询")
	 @GetMapping(value = "/getTotalFee")
	 public Result<?> getTotalFee(HttpServletRequest req) {
		 return Result.OK(showOrderService.getTotalFee(req.getParameterMap())).success(MessageUtil.getMessage("查询成功！", UserLocalContext.getLocal()));
	 }

	/**
	 *   添加
	 *
	 * @param showOrder
	 * @return
	 */
	@AutoLog(value = "订单管理-添加")
	@ApiOperation(value="订单管理-添加", notes="订单管理-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowOrder showOrder) {
		showOrderService.save(showOrder);
		return Result.OK("添加成功！");
	}

	/**
	 *  编辑
	 *
	 * @param showOrder
	 * @return
	 */
	@AutoLog(value = "订单管理-编辑")
	@ApiOperation(value="订单管理-编辑", notes="订单管理-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowOrder showOrder) {
		showOrderService.updateById(showOrder);
		return Result.OK("编辑成功!");
	}

	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "订单管理-通过id删除")
	@ApiOperation(value="订单管理-通过id删除", notes="订单管理-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showOrderService.removeById(id);
		return Result.OK("删除成功!");
	}

	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "订单管理-批量删除")
	@ApiOperation(value="订单管理-批量删除", notes="订单管理-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showOrderService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK("批量删除成功!");
	}

	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "订单管理-通过id查询")
	@ApiOperation(value="订单管理-通过id查询", notes="订单管理-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowOrder showOrder = showOrderService.getById(id);
		if(showOrder==null) {
			return Result.error("未找到对应数据");
		}
		return Result.OK(showOrder);
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showOrder
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowOrder showOrder) throws NoSuchFieldException, IllegalAccessException {
		return super.exportXls(request, showOrder, ExcelLangUtils.chooseLang(ShowOrder.class, UserLocalContext.getLocal()), MessageUtil.getMessage("订单管理", UserLocalContext.getLocal()));
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
        return super.importExcel(request, response, ShowOrder.class);
    }

}
