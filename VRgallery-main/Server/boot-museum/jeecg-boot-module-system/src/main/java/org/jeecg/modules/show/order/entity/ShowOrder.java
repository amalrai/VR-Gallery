package org.jeecg.modules.show.order.entity;

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
 * @Description: 订单管理
 * @Author: jeecg-boot
 * @Date:   2023-04-02
 * @Version: V1.0
 */
@Data
@TableName("show_order")
@Accessors(chain = true)
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="show_order对象", description="订单管理")
public class ShowOrder implements Serializable {
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
	/**下单时间*/
    @Excel(name = "下单时间", width = 15, format = "yyyy-MM-dd HH:mm:ss", isColumnHidden = true)
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "下单时间")
    private java.util.Date orderTime;
	/**付费时间*/
    @Excel(name = "付费时间", width = 15, format = "yyyy-MM-dd HH:mm:ss")
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "付费时间")
    private java.util.Date payTime;
    /**到期时间*/
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "到期时间")
    private java.util.Date expirationTime;
	/**所属展馆*/
    @Excel(name = "展馆", width = 15)
    @Dict(dictTable ="show_museum",dicText = "name",dicCode = "id")
    @ApiModelProperty(value = "展馆")
    private java.lang.String showMuseumId;
    /**所属房间*/
    @Excel(name = "房间", width = 15)
    @Dict(dictTable ="show_room",dicText = "name",dicCode = "id")
    @ApiModelProperty(value = "房间")
    private java.lang.String showRoomId;
    /**账号*/
    @Excel(name = "账号", width = 15)
    @ApiModelProperty(value = "账号")
    private java.lang.String showUserEmail;
	/**订单金额*/
	@Excel(name = "订单金额", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "订单金额")
    private java.math.BigDecimal orderFee;
	/**打折金额*/
	@Excel(name = "打折金额", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "打折金额")
    private java.math.BigDecimal discountFee;
	/**是否使用折扣码 0否 1是*/
	@Excel(name = "折扣码是否使用", width = 15, replace = {"否_0","是_1"})
    @ApiModelProperty(value = "折扣码是否使用")
    private java.lang.Integer isDiscount;
	/**折扣码*/
	@Excel(name = "折扣码", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "折扣码")
    private java.lang.String discountCode;
	/**打折后金额*/
	@Excel(name = "打折后金额", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "打折后金额")
    private java.math.BigDecimal afterDiscountFee;
	/**实际支付金额*/
	@Excel(name = "付费价钱", width = 15)
    @ApiModelProperty(value = "付费价钱")
    private java.math.BigDecimal payFee;
	/**购买人*/
	@Excel(name = "购买人", width = 15, isColumnHidden = true)
    @ApiModelProperty(value = "购买人")
    private java.lang.String showUserId;
    /**续费时长（天）*/
    @ApiModelProperty(value = "续费时长")
    private java.lang.Integer renewTime;
    /**产品ID（付费方式ID）*/
    @ApiModelProperty(value = "产品ID（付费方式ID）")
    private java.lang.String productId;
    /**产品名称*/
    @ApiModelProperty(value = "产品名称")
    private java.lang.String productName;
	/**状态 0未支付 1已支付*/
	@Excel(name = "状态", width = 15, replace = {"未支付_0","已支付_1"})
    @ApiModelProperty(value = "状态")
    private java.lang.Integer status;
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
