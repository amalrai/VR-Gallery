package org.jeecg.modules.system.service.impl;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;
import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.system.api.ISysBaseAPI;
import org.jeecg.common.system.vo.LoginUser;
import org.jeecg.modules.system.entity.SysLanguageItem;
import org.jeecg.modules.system.mapper.SysLanguageItemMapper;
import org.jeecg.modules.system.service.ISysLanguageItemService;
import org.jeecg.modules.system.util.UpdateSessionUserUtil;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@Service
public class SysLanguageItemServiceImpl extends ServiceImpl<SysLanguageItemMapper, SysLanguageItem> implements ISysLanguageItemService {

    @Autowired
    private ISysBaseAPI sysBaseAPI;

    @Override
    public Result<Map<String, String>> getDataByKey(String languageCode, String userName) {
        Result<Map<String, String>> result = new Result<>();
        result.setSuccess(true);
        result.setMessage("success");
        List<SysLanguageItem> data = this.getBaseMapper().selectList(new QueryWrapper<SysLanguageItem>().lambda().eq(SysLanguageItem::getSysLanguageCode, languageCode));
        //将data转为map
        Map<String, String> dataMap = data.stream().collect(Collectors.toMap(SysLanguageItem::getLangKey, SysLanguageItem::getLangValue));
        result.setResult(dataMap);
        //判断是否存在登录用户，存在则更新用户语言
        if (StringUtils.isNotEmpty(userName)) {
            //根据userName查询用户信息
            LoginUser sysUser = sysBaseAPI.getUserByName(userName);
            if (sysUser != null) {
                sysUser.setLocal(languageCode);
                UpdateSessionUserUtil.setUser(sysUser);
            }
        }
        return result;
    }
}
