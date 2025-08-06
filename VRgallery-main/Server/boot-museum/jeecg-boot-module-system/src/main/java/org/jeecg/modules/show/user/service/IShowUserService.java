package org.jeecg.modules.show.user.service;

import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.user.entity.ShowUser;
import com.baomidou.mybatisplus.extension.service.IService;
import org.jeecg.modules.show.user.entity.ShowUserVo;
import org.jeecg.modules.show.user.entity.ShowUserVo2;

import java.util.List;

/**
 * @Description: 用户管理
 * @Author: jeecg-boot
 * @Date:   2021-10-15
 * @Version: V1.0
 */
public interface IShowUserService extends IService<ShowUser> {

    Result<?> sendRegisterCode(String appId, String email);

    Result<?> register(String appId, String email, String pwd, String code, String appName, Integer sex, Integer years, Long areaId);

    Result<ShowUserVo> login(String appId, String email, String pwd);
    Result<ShowUserVo2> loginV2(String appId, String email, String pwd);

    Result<?> sendForgetPwdCode(String appId, String email);

    Result<?> forgetPwd(String appId, String email, String newPwd, String code);

    Result<?> resetPwd(String appId, String email, String newPwd, String oldPwd);

    Result<?> updateUserInfo(String id, String nickName, String realName, String avatar, Integer sex, Integer age, String phone, Integer years, Long areaId, String detailAddress);

    Result<?> submitViewTime(String userId, String dataId, Integer viewTime);

    Result<?> updateAuthByIds(List<String> asList, int i);

    Result<?> updateStateByIds(List<String> asList, int i);

    Result<?> startViewPerform(String userId, String performId);

    Result<ShowUserVo> getUserInfo(String id);
    Result<ShowUserVo2> getUserInfoV2(String id);


    Result<?> visitorLogin();

    Result<?> deleteUserById(String id);

    Integer queryVisitorCount();

    Result<?> checkAuth(String userId, String museumId);

    Result<?> getPayManagementInfo(String userId, String museumId);

    Result<?> checkAuthV2(String userId, String museumId, String roomId);

    Result<?> getPayManagementInfoV2(String userId, String museumId, String roomId);
}
