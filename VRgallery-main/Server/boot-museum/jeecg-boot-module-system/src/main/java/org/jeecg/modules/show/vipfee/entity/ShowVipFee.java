package org.jeecg.modules.show.vipfee.entity;

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
 * @Description: 付费管理
 * @Author: jeecg-boot
 * @Date:   2023-04-02
 * @Version: V1.0
 */
@Data
@TableName("show_vip_fee")
@Accessors(chain = true)
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="show_vip_fee对象", description="付费管理")
public class ShowVipFee implements Serializable {
    private static final long serialVersionUID = 1L;

	/**ID*/
	@TableId(type = IdType.ASSIGN_ID)
    @ApiModelProperty(value = "ID")
    private java.lang.String id;
    /**平台*/
    @Excel(name = "平台", width = 15)
    @ApiModelProperty(value = "平台")
    private java.lang.String platform;
    /**订单类型*/
    @Excel(name = "订单类型", width = 15)
    @ApiModelProperty(value = "订单类型")
    private java.lang.String orderType;
	/**会员费用(JPN)*/
	@Excel(name = "会员费用(JPN)", width = 15)
    @ApiModelProperty(value = "会员费用(JPN)")
    private java.math.BigDecimal vipFee;
	/**会员时限(days)*/
	@Excel(name = "会员时限(days)", width = 15)
    @ApiModelProperty(value = "会员时限(days)")
    private java.lang.Integer timeLimit;
	/**折扣码*/
	@Excel(name = "折扣码", width = 15)
    @ApiModelProperty(value = "折扣码")
    private java.lang.String discountCode;
	/**打折价钱*/
	@Excel(name = "打折价钱", width = 15)
    @ApiModelProperty(value = "打折价钱")
    private java.math.BigDecimal discountFee;
	/**打折后价钱*/
	@Excel(name = "打折后价钱", width = 15)
    @ApiModelProperty(value = "打折后价钱")
    private java.math.BigDecimal afterDiscountFee;
	/**状态*/
    @Excel(name = "状态", width = 15, replace = {"禁用_0","启用_1"})
    @ApiModelProperty(value = "状态")
    private java.lang.Integer status;
	/**所属展馆*/
	@Excel(name = "所属展馆", width = 15)
    @Dict(dictTable ="show_museum",dicText = "name",dicCode = "id")
    @ApiModelProperty(value = "所属展馆")
    private java.lang.String showMuseumId;
    /**所属房间*/
    @Excel(name = "所属房间", width = 15)
    @Dict(dictTable ="show_room",dicText = "name",dicCode = "id")
    @ApiModelProperty(value = "所属房间")
    private java.lang.String showRoomId;
	/**删除标识*/
	@Excel(name = "删除标识", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "删除标识")
    private java.lang.Integer delFlag;
	/**创建人*/
    @ApiModelProperty(value = "创建人")
    private java.lang.String createBy;
	/**创建时间*/
    @Excel(name = "创建时间", width = 15, format = "yyyy-MM-dd HH:mm:ss", isColumnHidden = true)
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "创建时间")
    private java.util.Date createTime;
	/**更新人*/
    @ApiModelProperty(value = "更新人")
    private java.lang.String updateBy;
	/**更新时间*/
    @Excel(name = "更新时间", width = 15, format = "yyyy-MM-dd HH:mm:ss", isColumnHidden = true)
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "更新时间")
    private java.util.Date updateTime;
}
