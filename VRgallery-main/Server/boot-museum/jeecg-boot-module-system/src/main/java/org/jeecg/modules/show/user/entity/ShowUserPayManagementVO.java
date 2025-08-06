package org.jeecg.modules.show.user.entity;

import com.alibaba.fastjson.annotation.JSONField;
import com.fasterxml.jackson.annotation.JsonFormat;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;
import lombok.EqualsAndHashCode;
import org.springframework.format.annotation.DateTimeFormat;

import java.io.Serializable;

/**
 * @author : wangbinyu
 * @since : 2023/4/25
 * description :
 */
@Data
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="付款管理对象", description="付款管理")
public class ShowUserPayManagementVO implements Serializable {
    private static final long serialVersionUID = 1L;
    @ApiModelProperty(value = "订单ID")
    private java.lang.String id;
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "购买日")
    private java.util.Date payTime;
    @ApiModelProperty(value = "展馆名称")
    private java.lang.String showMuseumName;
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "到期时间")
    private java.util.Date expirationTime;
    @ApiModelProperty(value = "付费价钱")
    private java.math.BigDecimal payFee;
    @ApiModelProperty(value = "折扣码是否使用")
    private java.lang.Integer isDiscount;
    @ApiModelProperty(value = "折扣码")
    private java.lang.String discountCode;
}
