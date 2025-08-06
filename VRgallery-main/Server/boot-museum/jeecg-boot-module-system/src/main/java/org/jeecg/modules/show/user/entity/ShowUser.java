package org.jeecg.modules.show.user.entity;

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
 * @Description: 用户管理
 * @Author: jeecg-boot
 * @Date:   2021-10-15
 * @Version: V1.0
 */
@Data
@TableName("show_user")
@Accessors(chain = true)
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="show_user对象", description="用户管理")
public class ShowUser implements Serializable {
    private static final long serialVersionUID = 1L;

	/**id*/
	@TableId(type = IdType.ASSIGN_ID)
    @ApiModelProperty(value = "id")
    private java.lang.String id;
    /**所属app*/
    @Excel(name = "所属app", width = 15)
    @Dict(dictTable ="show_app",dicText = "name",dicCode = "id")
    private String showAppId;
    /**所属app*/
//    @Excel(name = "所属APP", width = 15)
    @ApiModelProperty(value = "所属APP")
    private java.lang.String appName;
    /**昵称*/
    @Excel(name = "昵称", width = 15)
    @ApiModelProperty(value = "昵称")
    private java.lang.String nickName;

    /**名称*/
    @Excel(name = "姓名", width = 15)
    @ApiModelProperty(value = "姓名")
    private java.lang.String realName;

    /**性别*/
    @Excel(name = "性别", width = 15, replace = {"男性_0","女性_1","其他_2"})
    @ApiModelProperty(value = "性别")
    private java.lang.Integer sex;

    /**年龄*/
    @Excel(name = "年龄", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "年龄")
    private java.lang.Integer age;

    /**年代*/
    @Excel(name = "年代", width = 15, replace = {"10代_1","20代_2","30代_3","40代_4","50代_5","60代_6","70代～_7"})
    @ApiModelProperty(value = "年代")
    private java.lang.Integer years;

    /**头像*/
    @Excel(name = "头像", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "头像")
    private java.lang.String avatar;


	/**手机号*/
	@Excel(name = "手机号", width = 15)
    @ApiModelProperty(value = "手机号")
    private java.lang.String phone;

    /**手机号*/
    @Excel(name = "密码", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "密码")
    private java.lang.String pwd;

	/**邮箱*/
	@Excel(name = "邮箱", width = 20)
    @ApiModelProperty(value = "邮箱")
    private java.lang.String email;

    /**状态*/
    @Excel(name = "状态", width = 15, replace = {"禁用_0","启用_1"})
    @ApiModelProperty(value = "状态")
    private java.lang.Integer state;

    /**会员权限*/
    @Excel(name = "会员权限", width = 15, replace = {"关闭_0","开启_1"})
    @ApiModelProperty(value = "会员权限")
    private java.lang.Integer isAuth;

    /**收货地址*/
    @Excel(name = "详细地址", width = 60)
    @ApiModelProperty(value = "详细地址(全)")
    private java.lang.String address;

    @ApiModelProperty(value = "地区ID")
    private java.lang.Long areaId;

    @ApiModelProperty(value = "地区名称")
    private java.lang.String areaName;

    @ApiModelProperty(value = "详细地址")
    private java.lang.String detailAddress;

	/**注册时间*/
	@Excel(name = "注册时间", width = 30, format = "yyyy-MM-dd HH:mm:ss")
	@JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "注册时间")
    private java.util.Date registerTime;

	/**会员类型*/
	@Excel(name = "会员类型", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "会员类型")
    private java.lang.Integer type;
	/**激活时间*/
	@Excel(name = "激活时间", width = 15, format = "yyyy-MM-dd HH:mm:ss", isColumnHidden = true)
	@JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "激活时间")
    private java.util.Date activateTime;
//	/**观看次数*/
//	@Excel(name = "观看次数", width = 15)
//    @ApiModelProperty(value = "观看次数")
//    private java.lang.Integer viewCount;
//	/**观看总时长(min)*/
//	@Excel(name = "观看总时长(min)", width = 15)
//    @ApiModelProperty(value = "观看总时长(min)")
//    private java.lang.Integer viewTime;
//	/**留言数量*/
//	@Excel(name = "留言数量", width = 15)
//    @ApiModelProperty(value = "留言数量")
//    private java.lang.Integer commentCount;
//	/**在线时长(min)*/
//	@Excel(name = "在线时长(min)", width = 15)
//    @ApiModelProperty(value = "在线时长(min)")
//    private java.lang.Integer liveTime;

    /**是否首次登录 0首次 1非首次*/
    @Excel(name = "是否首次登录", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "是否首次登录 0首次 1非首次")
    private java.lang.Integer firstLogin;

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
