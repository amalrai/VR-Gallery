package org.jeecg.modules.show.vipfee.service;

import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.vipfee.entity.ShowVipFee;
import com.baomidou.mybatisplus.extension.service.IService;

/**
 * @Description: 付费管理
 * @Author: jeecg-boot
 * @Date:   2023-04-02
 * @Version: V1.0
 */
public interface IShowVipFeeService extends IService<ShowVipFee> {

    Result<?> queryByMuseumId(String museumId);

    Result<?> queryByMuseumIdV2(String platform, String museumId, String roomId);
}
