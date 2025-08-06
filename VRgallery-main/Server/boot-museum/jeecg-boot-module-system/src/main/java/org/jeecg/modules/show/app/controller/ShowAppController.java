package org.jeecg.modules.show.app.controller;

import java.util.Arrays;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.system.query.QueryGenerator;
import org.jeecg.modules.show.app.entity.ShowApp;
import org.jeecg.modules.show.app.service.IShowAppService;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.metadata.IPage;
import com.baomidou.mybatisplus.extension.plugins.pagination.Page;
import lombok.extern.slf4j.Slf4j;

import org.jeecg.common.system.base.controller.JeecgController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.redis.core.RedisTemplate;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.servlet.ModelAndView;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import org.jeecg.common.aspect.annotation.AutoLog;

 /**
 * @Description: show_app
 * @Author: jeecg-boot
 * @Date:   2022-10-24
 * @Version: V1.0
 */
@Api(tags="show_app")
@RestController
@RequestMapping("/show/showApp")
@Slf4j
public class ShowAppController extends JeecgController<ShowApp, IShowAppService> {
	@Autowired
	private IShowAppService showAppService;
	@Autowired
	public RedisTemplate redisTemplate;

	/**
	 * 分页列表查询
	 *
	 * @param showApp
	 * @param pageNo
	 * @param pageSize
	 * @param req
	 * @return
	 */
	@AutoLog(value = "show_app-分页列表查询")
	@GetMapping(value = "/list")
	public Result<?> queryPageList(ShowApp showApp,
								   @RequestParam(name="pageNo", defaultValue="1") Integer pageNo,
								   @RequestParam(name="pageSize", defaultValue="10") Integer pageSize,
								   HttpServletRequest req) {
		QueryWrapper<ShowApp> queryWrapper = QueryGenerator.initQueryWrapper(showApp, req.getParameterMap());
		Page<ShowApp> page = new Page<ShowApp>(pageNo, pageSize);
		IPage<ShowApp> pageList = showAppService.page(page, queryWrapper);
		return Result.OK(pageList);
	}

	/**
	 *   添加
	 *
	 * @param showApp
	 * @return
	 */
	@AutoLog(value = "show_app-添加")
	@PostMapping(value = "/add")
	public Result<?> add(@RequestBody ShowApp showApp) {
		showAppService.save(showApp);
		return Result.OK("添加成功！");
	}

	/**
	 *  编辑
	 *
	 * @param showApp
	 * @return
	 */
	@AutoLog(value = "show_app-编辑")
	@PutMapping(value = "/edit")
	public Result<?> edit(@RequestBody ShowApp showApp) {
		showAppService.updateById(showApp);
		String keyString = String.format("sys:cache:dictTable::SimpleKey [show_app,name,id,%s]", showApp.getId());
		redisTemplate.delete(keyString);
		return Result.OK("编辑成功!");
	}

	/**
	 *   通过id删除
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "show_app-通过id删除")
	@DeleteMapping(value = "/delete")
	public Result<?> delete(@RequestParam(name="id",required=true) String id) {
		showAppService.removeById(id);
		String keyString = String.format("sys:cache:dictTable::SimpleKey [show_app,name,id,%s]", id);
		redisTemplate.delete(keyString);
		return Result.OK("删除成功!");
	}

	/**
	 *  批量删除
	 *
	 * @param ids
	 * @return
	 */
	@AutoLog(value = "show_app-批量删除")
	@DeleteMapping(value = "/deleteBatch")
	public Result<?> deleteBatch(@RequestParam(name="ids",required=true) String ids) {
		this.showAppService.removeByIds(Arrays.asList(ids.split(",")));
		return Result.OK("批量删除成功!");
	}

	/**
	 * 通过id查询
	 *
	 * @param id
	 * @return
	 */
	@AutoLog(value = "show_app-通过id查询")
	@GetMapping(value = "/queryById")
	public Result<?> queryById(@RequestParam(name="id",required=true) String id) {
		ShowApp showApp = showAppService.getById(id);
		if(showApp==null) {
			return Result.error("未找到对应数据");
		}
		return Result.OK(showApp);
	}

    /**
    * 导出excel
    *
    * @param request
    * @param showApp
    */
    @RequestMapping(value = "/exportXls")
    public ModelAndView exportXls(HttpServletRequest request, ShowApp showApp) {
        return super.exportXls(request, showApp, ShowApp.class, "show_app");
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
        return super.importExcel(request, response, ShowApp.class);
    }

}
