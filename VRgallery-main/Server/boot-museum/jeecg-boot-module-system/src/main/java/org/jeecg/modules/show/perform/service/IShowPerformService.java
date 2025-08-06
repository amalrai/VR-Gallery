package org.jeecg.modules.show.perform.service;

import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.perform.entity.ShowPerform;
import com.baomidou.mybatisplus.extension.service.IService;

import java.util.List;

/**
 * @Description: 演出管理
 * @Author: jeecg-boot
 * @Date:   2021-10-16
 * @Version: V1.0
 */
public interface IShowPerformService extends IService<ShowPerform> {

    Result<?> doAdd(ShowPerform showPerform);

    Result<?> doUpdate(ShowPerform showPerform);

    Result<List<ShowPerform>> getPerformList();
}
