package org.jeecg.modules.show.log.entity;

import java.io.Serializable;
import java.io.UnsupportedEncodingException;
import java.util.Date;
import java.math.BigDecimal;
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
 * @Description: show_user_click_log
 * @Author: jeecg-boot
 * @Date:   2023-03-26
 * @Version: V1.0
 */
@Data
@TableName("show_user_click_log")
@Accessors(chain = true)
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="show_user_click_log对象", description="show_user_click_log")
public class ShowUserClickLog implements Serializable {
    private static final long serialVersionUID = 1L;

	/**ID*/
	@TableId(type = IdType.ASSIGN_ID)
    @ApiModelProperty(value = "ID")
    private java.lang.String id;
	/**登录日志ID*/
	@Excel(name = "登录日志ID", width = 15)
    @ApiModelProperty(value = "登录日志ID")
    private java.lang.String showUserLoginLogId;
	/**展品ID*/
	@Excel(name = "展品ID", width = 15)
    @ApiModelProperty(value = "展品ID")
    private java.lang.String showExhibitsId;
	/**展品编号*/
	@Excel(name = "展品编号", width = 15)
    @ApiModelProperty(value = "展品编号")
    private java.lang.Integer showExhibitsNo;
	/**简介点击次数*/
	@Excel(name = "简介点击次数", width = 15)
    @ApiModelProperty(value = "简介点击次数")
    private java.lang.Integer introductionCount;
	/**链接点击次数*/
	@Excel(name = "链接点击次数", width = 15)
    @ApiModelProperty(value = "链接点击次数")
    private java.lang.Integer linkCount;
	/**视频点击次数*/
	@Excel(name = "视频点击次数", width = 15)
    @ApiModelProperty(value = "视频点击次数")
    private java.lang.Integer videoCount;
	/**音频点击次数*/
	@Excel(name = "音频点击次数", width = 15)
    @ApiModelProperty(value = "音频点击次数")
    private java.lang.Integer voiceCount;
	/**商店点击次数*/
	@Excel(name = "商店点击次数", width = 15)
    @ApiModelProperty(value = "商店点击次数")
    private java.lang.Integer shopCount;
	/**创建人*/
    @ApiModelProperty(value = "创建人")
    private java.lang.String createBy;
	/**创建时间*/
	@JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd")
    @DateTimeFormat(pattern="yyyy-MM-dd")
    @ApiModelProperty(value = "创建时间")
    private java.util.Date createTime;
	/**更新人*/
    @ApiModelProperty(value = "更新人")
    private java.lang.String updateBy;
	/**更新时间*/
	@JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd")
    @DateTimeFormat(pattern="yyyy-MM-dd")
    @ApiModelProperty(value = "更新时间")
    private java.util.Date updateTime;
}
