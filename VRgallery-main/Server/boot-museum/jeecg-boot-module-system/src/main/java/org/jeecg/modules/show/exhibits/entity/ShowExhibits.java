package org.jeecg.modules.show.exhibits.entity;

import java.io.Serializable;
import java.io.UnsupportedEncodingException;
import java.util.Date;
import java.math.BigDecimal;

import com.alibaba.fastjson.annotation.JSONField;
import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableField;
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
 * @Description: 展品管理
 * @Author: jeecg-boot
 * @Date:   2022-06-15
 * @Version: V1.0
 */
@Data
@TableName("show_exhibits")
@Accessors(chain = true)
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="show_exhibits对象", description="展品管理")
public class ShowExhibits implements Serializable {
    private static final long serialVersionUID = 1L;

	/**id*/
	@TableId(type = IdType.ASSIGN_ID)
    @ApiModelProperty(value = "id")
    private java.lang.String id;
	/**所属房间*/
	@Excel(name = "所属房间", width = 15)
    @ApiModelProperty(value = "所属房间")
    private java.lang.String showRoomId;
	/**编号*/
	@Excel(name = "编号", width = 15)
    @ApiModelProperty(value = "编号")
    private java.lang.Integer exhibitsNo;
	/**展品名*/
	@Excel(name = "展品名", width = 15)
    @ApiModelProperty(value = "展品名")
    private java.lang.String name;
	/**主图名称*/
	@Excel(name = "主图名称", width = 15)
    @ApiModelProperty(value = "主图名称")
    private java.lang.String mainGraphName;
	/**主图大小*/
	@Excel(name = "主图大小", width = 15)
    @ApiModelProperty(value = "主图大小")
    private java.lang.String mainGraphSize;
	/**主图地址*/
	@Excel(name = "主图地址", width = 15)
    @ApiModelProperty(value = "主图地址")
    private java.lang.String mainGraphUrl;
	/**主图上传时间*/
	@Excel(name = "主图上传时间", width = 15, format = "yyyy-MM-dd")
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "主图上传时间")
    private java.util.Date mainGraphTime;
	/**介绍图名称*/
	@Excel(name = "介绍图名称", width = 15)
    @ApiModelProperty(value = "介绍图名称")
    private java.lang.String introductionImageName;
	/**介绍图大小*/
	@Excel(name = "介绍图大小", width = 15)
    @ApiModelProperty(value = "介绍图大小")
    private java.lang.String introductionImageSize;
	/**介绍图地址*/
	@Excel(name = "介绍图地址", width = 15)
    @ApiModelProperty(value = "介绍图地址")
    private java.lang.String introductionImageUrl;
	/**介绍上传时间*/
	@Excel(name = "介绍上传时间", width = 15, format = "yyyy-MM-dd")
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "介绍上传时间")
    private java.util.Date introductionImageTime;
	/**视频名称*/
	@Excel(name = "视频名称", width = 15)
    @ApiModelProperty(value = "视频名称")
    private java.lang.String videoName;
	/**视频大小*/
	@Excel(name = "视频大小", width = 15)
    @ApiModelProperty(value = "视频大小")
    private java.lang.String videoSize;
	/**视频地址*/
	@Excel(name = "视频地址", width = 15)
    @ApiModelProperty(value = "视频地址")
    private java.lang.String videoUrl;
	/**视频上传时间*/
	@Excel(name = "视频上传时间", width = 15, format = "yyyy-MM-dd")
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "视频上传时间")
    private java.util.Date videoTime;
	/**视频外链*/
	@Excel(name = "视频外链", width = 15)
    @ApiModelProperty(value = "视频外链")
    private java.lang.String videoLink;
	/**视频外链更新时间*/
	@Excel(name = "视频外链更新时间", width = 15, format = "yyyy-MM-dd")
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "视频外链更新时间")
    private java.util.Date videoLinkTime;
	/**网页外链*/
	@Excel(name = "网页外链", width = 15)
    @ApiModelProperty(value = "网页外链")
    private java.lang.String webLink;
	/**网页外链更新时间*/
	@Excel(name = "网页外链更新时间", width = 15, format = "yyyy-MM-dd")
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "网页外链更新时间")
    private java.util.Date webLinkTime;
	/**动画缩略图名称*/
	@Excel(name = "动画缩略图名称", width = 15)
    @ApiModelProperty(value = "动画缩略图名称")
    private java.lang.String animationThumbnailName;
	/**动画缩略图大小*/
	@Excel(name = "动画缩略图大小", width = 15)
    @ApiModelProperty(value = "动画缩略图大小")
    private java.lang.String animationThumbnailSize;
	/**动画缩略图地址*/
	@Excel(name = "动画缩略图地址", width = 15)
    @ApiModelProperty(value = "动画缩略图地址")
    private java.lang.String animationThumbnailUrl;
	/**动画缩略图上传时间*/
	@Excel(name = "动画缩略图上传时间", width = 15, format = "yyyy-MM-dd")
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "动画缩略图上传时间")
    private java.util.Date animationThumbnailTime;
	/**帧动画名称*/
	@Excel(name = "帧动画名称", width = 15)
    @ApiModelProperty(value = "帧动画名称")
    private java.lang.String frameAnimationName;
	/**帧动画大小*/
	@Excel(name = "帧动画大小", width = 15)
    @ApiModelProperty(value = "帧动画大小")
    private java.lang.String frameAnimationSize;
	/**帧动画地址*/
	@Excel(name = "帧动画地址", width = 15)
    @ApiModelProperty(value = "帧动画地址")
    private java.lang.String frameAnimationUrl;
	/**帧动画上传时间*/
	@Excel(name = "帧动画上传时间", width = 15, format = "yyyy-MM-dd")
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "帧动画上传时间")
    private java.util.Date frameAnimationTime;
	/**音频名称*/
	@Excel(name = "音频名称", width = 15)
    @ApiModelProperty(value = "音频名称")
    private java.lang.String voiceName;
	/**音频大小*/
	@Excel(name = "音频大小", width = 15)
    @ApiModelProperty(value = "音频大小")
    private java.lang.String voiceSize;
	/**音频地址*/
	@Excel(name = "音频地址", width = 15)
    @ApiModelProperty(value = "音频地址")
    private java.lang.String voiceUrl;
	/**音频上传时间*/
	@Excel(name = "音频上传时间", width = 15, format = "yyyy-MM-dd")
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "音频上传时间")
    private java.util.Date voiceTime;
	/**文字简介*/
	@Excel(name = "文字简介", width = 15)
    @ApiModelProperty(value = "文字简介")
    private java.lang.String text;
	/**状态 0启用1禁用*/
	@Excel(name = "状态 0启用1禁用", width = 15)
    @ApiModelProperty(value = "状态 0启用1禁用")
    private java.lang.Integer status;
	/**权限 0免费1VIP*/
	@Excel(name = "权限 0免费1VIP", width = 15)
    @ApiModelProperty(value = "权限 0免费1VIP")
    private java.lang.Integer auth;

    @Excel(name = "版本号", width = 15)
    @ApiModelProperty(value = "版本号")
    private java.lang.Integer version;

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

    @ApiModelProperty(value = "主图加密地址")
    private java.lang.String mainGraphEncodeUrl;

    @ApiModelProperty(value = "介绍图加密地址")
    private java.lang.String introductionImageEncodeUrl;

    @ApiModelProperty(value = "视频加密地址")
    private java.lang.String videoEncodeUrl;

    @ApiModelProperty(value = "动画缩略图加密地址")
    private java.lang.String animationThumbnailEncodeUrl;

    @ApiModelProperty(value = "帧动画加密地址")
    private java.lang.String frameAnimationEncodeUrl;

    @TableField(exist = false)
    private Integer count;

}
