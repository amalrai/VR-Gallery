package org.jeecg.modules.show.data.service;

import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.data.entity.ShowData;
import com.baomidou.mybatisplus.extension.service.IService;
import org.jeecg.modules.show.user.entity.ShowUser;

/**
 * @Description: 数据管理
 * @Author: jeecg-boot
 * @Date:   2021-10-16
 * @Version: V1.0
 */
public interface IShowDataService extends IService<ShowData> {
    Result<String> saveViewTime(String id, ShowUser user, int viewTime);
    Result<String> updateUserInfo(ShowUser user);
}
