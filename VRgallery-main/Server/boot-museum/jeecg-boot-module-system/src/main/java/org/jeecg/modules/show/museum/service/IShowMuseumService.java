package org.jeecg.modules.show.museum.service;

import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.museum.entity.ShowMuseum;
import com.baomidou.mybatisplus.extension.service.IService;

import java.util.List;

/**
 * @Description: show_museum
 * @Author: jeecg-boot
 * @Date:   2022-10-24
 * @Version: V1.0
 */
public interface IShowMuseumService extends IService<ShowMuseum> {
    Result<List<ShowMuseum>> getMuseumList(String appId);
}
