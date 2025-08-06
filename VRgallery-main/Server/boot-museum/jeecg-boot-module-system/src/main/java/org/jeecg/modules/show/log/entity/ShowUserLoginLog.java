package org.jeecg.modules.show.log.entity;

import java.io.Serializable;
import java.io.UnsupportedEncodingException;
import java.util.Date;
import java.math.BigDecimal;

import com.alibaba.fastjson.annotation.JSONField;
import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import com.fasterxml.jackson.annotation.JsonFormat;
import org.springframework.format.annotation.DateTimeFormat;
import org.jeecgframework.poi.excel.annotation.Excel;
import org.jeecg.common.aspect.annotation.Dict;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;

/**
 * @Description: 登录日志
 * @Author: jeecg-boot
 * @Date:   2023-03-26
 * @Version: V1.0
 */
@Data
@TableName("show_user_login_log")
@Accessors(chain = true)
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="show_user_login_log对象", description="登录日志")
public class ShowUserLoginLog implements Serializable {
    private static final long serialVersionUID = 1L;

	/**ID*/
	@TableId(type = IdType.ASSIGN_ID)
    @ApiModelProperty(value = "ID")
    private java.lang.String id;
	/**用户ID*/
	@Excel(name = "用户ID", width = 15)
    @ApiModelProperty(value = "用户ID")
    private java.lang.String showUserId;
	/**用户昵称*/
	@Excel(name = "用户昵称", width = 15)
    @ApiModelProperty(value = "用户昵称")
    private java.lang.String showUserNickName;
	/**用户姓名*/
	@Excel(name = "用户姓名", width = 15)
    @ApiModelProperty(value = "用户姓名")
    private java.lang.String showUserRealName;
	/**用户账号*/
	@Excel(name = "用户账号", width = 15)
    @ApiModelProperty(value = "用户账号")
    private java.lang.String showUserEmail;
    /**所属app*/
    @Excel(name = "所属app", width = 15)
    @Dict(dictTable ="show_app",dicText = "name",dicCode = "id")
    private String showAppId;
	/**登录展馆ID*/
	@Excel(name = "登录展馆ID", width = 15)
    @ApiModelProperty(value = "登录展馆ID")
    @Dict(dictTable ="show_museum",dicText = "name",dicCode = "id")
    private java.lang.String showMuseumId;
	/**登录房间ID*/
	@Excel(name = "登录房间ID", width = 15)
    @Dict(dictTable ="show_room",dicText = "name",dicCode = "id")
    private java.lang.String showRoomId;
	/**登录ip*/
	@Excel(name = "登录ip", width = 15)
    @ApiModelProperty(value = "登录ip")
    private java.lang.String loginIp;
	/**登录时间*/
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "登录时间")
    private java.util.Date loginTime;
	/**创建人*/
    @ApiModelProperty(value = "创建人")
    private java.lang.String createBy;
	/**创建时间*/
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "创建时间")
    private java.util.Date createTime;
	/**更新人*/
    @ApiModelProperty(value = "更新人")
    private java.lang.String updateBy;
	/**更新时间*/
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "更新时间")
    private java.util.Date updateTime;
}
