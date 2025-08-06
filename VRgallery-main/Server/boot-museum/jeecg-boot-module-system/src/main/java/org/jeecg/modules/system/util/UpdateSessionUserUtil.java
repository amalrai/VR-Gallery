package org.jeecg.modules.system.util;

import org.apache.shiro.SecurityUtils;
import org.apache.shiro.subject.PrincipalCollection;
import org.apache.shiro.subject.SimplePrincipalCollection;
import org.apache.shiro.subject.Subject;
import org.apache.shiro.subject.support.DefaultSubjectContext;
import org.jeecg.common.system.vo.LoginUser;


/**
 * @author : wangbinyu
 * @since : 2022/6/28
 * description :
 */
public class UpdateSessionUserUtil {
    public static void setUser(LoginUser loginUserInfo) {
        Subject subject = SecurityUtils.getSubject();
        PrincipalCollection principals = subject.getPrincipals();
        //realName认证信息的key，对应的value就是认证的user对象
        if (principals != null) {
            String realName = principals.getRealmNames().iterator().next();
            //创建一个PrincipalCollection对象，userInfo是更新后的user对象
            PrincipalCollection newPrincipalCollection = new SimplePrincipalCollection(loginUserInfo, realName);
            //调用subject的runAs方法，把新的PrincipalCollection放到session里面
            subject.getSession().setAttribute(DefaultSubjectContext.PRINCIPALS_SESSION_KEY, newPrincipalCollection);
        }
    }
}
