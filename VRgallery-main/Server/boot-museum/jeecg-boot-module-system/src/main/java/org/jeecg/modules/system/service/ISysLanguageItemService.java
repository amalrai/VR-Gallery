package org.jeecg.modules.system.service;

import com.baomidou.mybatisplus.extension.service.IService;
import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.system.entity.SysLanguageItem;

import java.util.Map;

public interface ISysLanguageItemService extends IService<SysLanguageItem> {

	/**
	 * 根据语言编码获取信息
	 * @param languageCode
	 */
	Result<Map<String, String>> getDataByKey(String languageCode, String userName);

}
