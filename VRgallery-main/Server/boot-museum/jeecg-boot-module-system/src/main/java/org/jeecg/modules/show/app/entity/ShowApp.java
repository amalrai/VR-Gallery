package org.jeecg.modules.show.app.entity;

import java.io.Serializable;

import com.alibaba.fastjson.annotation.JSONField;
import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import com.fasterxml.jackson.annotation.JsonFormat;
import org.springframework.format.annotation.DateTimeFormat;
import org.jeecgframework.poi.excel.annotation.Excel;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;

/**
 * @Description: show_app
 * @Author: jeecg-boot
 * @Date:   2022-10-24
 * @Version: V1.0
 */
@Data
@TableName("show_app")
@Accessors(chain = true)
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="show_app对象", description="show_app")
public class ShowApp implements Serializable {
    private static final long serialVersionUID = 1L;

	/**id*/
	@TableId(type = IdType.ASSIGN_ID)
    @ApiModelProperty(value = "id")
    private java.lang.String id;
	/**APP名称*/
	@Excel(name = "APP名称", width = 15)
    @ApiModelProperty(value = "APP名称")
    private java.lang.String name;
	/**APP描述*/
	@Excel(name = "APP描述", width = 15)
    @ApiModelProperty(value = "APP描述")
    private java.lang.String description;
    /**音频名*/
    @Excel(name = "资源名", width = 15)
    @ApiModelProperty(value = "资源名")
    private java.lang.String resourceName;
    /**音频大小*/
    @Excel(name = "音频大小", width = 15)
    @ApiModelProperty(value = "资源大小")
    private java.lang.String resourceSize;
    /**音频地址*/
    @Excel(name = "音频地址", width = 15)
    @ApiModelProperty(value = "资源地址")
    private java.lang.String resourceUrl;
    /**音频名*/
    @Excel(name = "音频名", width = 15)
    @ApiModelProperty(value = "资源名")
    private java.lang.String resourceAndroidName;
    /**音频大小*/
    @Excel(name = "音频大小", width = 15)
    @ApiModelProperty(value = "资源大小")
    private java.lang.String resourceAndroidSize;
    /**音频地址*/
    @Excel(name = "音频地址", width = 15)
    @ApiModelProperty(value = "资源地址")
    private java.lang.String resourceAndroidUrl;
    /**音频名*/
    @Excel(name = "音频名", width = 15)
    @ApiModelProperty(value = "资源名")
    private java.lang.String resourceIosName;
    /**音频大小*/
    @Excel(name = "音频大小", width = 15)
    @ApiModelProperty(value = "资源大小")
    private java.lang.String resourceIosSize;
    /**音频地址*/
    @Excel(name = "音频地址", width = 15)
    @ApiModelProperty(value = "资源地址")
    private java.lang.String resourceIosUrl;

    @Excel(name = "win安装包地址", width = 15)
    @ApiModelProperty(value = "win安装包地址")
    private java.lang.String appUrl;
    @Excel(name = "win安装包商店地址", width = 15)
    @ApiModelProperty(value = "win安装包商店地址")
    private java.lang.String appStoreUrl;
    @Excel(name = "win安装包名称", width = 15)
    @ApiModelProperty(value = "win安装包名称")
    private java.lang.String appName;
    @Excel(name = "win安装包版本", width = 15)
    @ApiModelProperty(value = "win安装包版本")
    private java.lang.String appVersion;
    @Excel(name = "win安装包地址", width = 15)
    @ApiModelProperty(value = "win安装包地址")
    private java.lang.String appAndroidUrl;
    @Excel(name = "win安装包商店地址", width = 15)
    @ApiModelProperty(value = "win安装包商店地址")
    private java.lang.String appAndroidStoreUrl;
    @Excel(name = "win安装包名称", width = 15)
    @ApiModelProperty(value = "win安装包名称")
    private java.lang.String appAndroidName;
    @Excel(name = "win安装包版本", width = 15)
    @ApiModelProperty(value = "win安装包版本")
    private java.lang.String appAndroidVersion;
    @Excel(name = "win安装包地址", width = 15)
    @ApiModelProperty(value = "win安装包地址")
    private java.lang.String appIosUrl;
    @Excel(name = "win安装包商店地址", width = 15)
    @ApiModelProperty(value = "win安装包商店地址")
    private java.lang.String appIosStoreUrl;
    @Excel(name = "win安装包名称", width = 15)
    @ApiModelProperty(value = "win安装包名称")
    private java.lang.String appIosName;
    @Excel(name = "win安装包版本", width = 15)
    @ApiModelProperty(value = "win安装包版本")
    private java.lang.String appIosVersion;
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
