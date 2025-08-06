package org.jeecg.modules.show.exhibits.service;

import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.exhibits.entity.ShowExhibits;
import com.baomidou.mybatisplus.extension.service.IService;

import java.util.List;

/**
 * @Description: 展品管理
 * @Author: jeecg-boot
 * @Date:   2022-06-15
 * @Version: V1.0
 */
public interface IShowExhibitsService extends IService<ShowExhibits> {
    /**
     * 根据count创建空的展品位
     * @param exhibits
     */
    Result<?> batchAddExhibits(ShowExhibits exhibits);

    Result<?> editExhibits(ShowExhibits exhibits);

    Result<?> replaceExhibits(String exhibitsId, String replaceExhibitsId);

    Result<?> batchUpdateStatus(List<String> idList, Integer status);

    Result<?> batchUpdateAuth(List<String> idList, Integer auth);

    Result<?> batchClear(List<String> idList);

    Result<?> clear(String id);

    Result<List<ShowExhibits>> getExhibitsList(String roomId);
}
