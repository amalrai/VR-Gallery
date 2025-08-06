package org.jeecg.modules.show.client.app.result;

import com.baomidou.mybatisplus.annotation.TableName;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;
import org.jeecg.modules.show.user.entity.ShowUser;

/**
 * @author : wangbinyu
 * @since : 2021/10/16
 * description :
 */
@Data
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="登陆返回对象", description="登陆返回对象")
public class LoginResult {
    private static final long serialVersionUID = 1L;

    @ApiModelProperty(value = "数据管理的ID，提交观看时间时使用")
    private java.lang.String id;

    @ApiModelProperty(value = "用户信息")
    private ShowUser user;

}
