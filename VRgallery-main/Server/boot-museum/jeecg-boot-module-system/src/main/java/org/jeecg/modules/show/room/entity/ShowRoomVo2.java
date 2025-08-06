package org.jeecg.modules.show.room.entity;

import com.alibaba.fastjson.annotation.JSONField;
import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import com.fasterxml.jackson.annotation.JsonFormat;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;
import org.jeecg.common.aspect.annotation.Dict;
import org.jeecgframework.poi.excel.annotation.Excel;
import org.springframework.format.annotation.DateTimeFormat;

import java.io.Serializable;
import java.util.Date;

/**
 * @Description: show_room
 * @Author: jeecg-boot
 * @Date:   2022-06-15
 * @Version: V1.0
 */
@Data
@TableName("show_room")
@Accessors(chain = true)
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="show_room对象", description="show_room")
public class ShowRoomVo2 implements Serializable {
    private static final long serialVersionUID = 1L;

	/**id*/
	@TableId(type = IdType.ASSIGN_ID)
    @ApiModelProperty(value = "id")
    private String id;
	/**房间*/
	@Excel(name = "房间", width = 15)
    @ApiModelProperty(value = "房间")
    private String name;
	/**房间编号*/
	@Excel(name = "房间编号", width = 15)
    @ApiModelProperty(value = "房间编号")
    private Integer roomNo;
	/**说明*/
	@Excel(name = "说明", width = 15)
    @ApiModelProperty(value = "说明")
    private String description;
	/**音频名*/
	@Excel(name = "音频名", width = 15)
    @ApiModelProperty(value = "音频名")
    private String musicName;
	/**音频大小*/
	@Excel(name = "音频大小", width = 15)
    @ApiModelProperty(value = "音频大小")
    private String musicSize;
	/**音频地址*/
	@Excel(name = "音频地址", width = 15)
    @ApiModelProperty(value = "音频地址")
    private String musicUrl;
    /**音频名*/
    @Excel(name = "资源名", width = 15)
    @ApiModelProperty(value = "资源名")
    private String resourceName;
    /**音频大小*/
    @Excel(name = "音频大小", width = 15)
    @ApiModelProperty(value = "资源大小")
    private String resourceSize;
    /**音频地址*/
    @Excel(name = "音频地址", width = 15)
    @ApiModelProperty(value = "资源地址")
    private String resourceUrl;
    /**音频名*/
    @Excel(name = "音频名", width = 15)
    @ApiModelProperty(value = "资源名")
    private String resourceAndroidName;
    /**音频大小*/
    @Excel(name = "音频大小", width = 15)
    @ApiModelProperty(value = "资源大小")
    private String resourceAndroidSize;
    /**音频地址*/
    @Excel(name = "音频地址", width = 15)
    @ApiModelProperty(value = "资源地址")
    private String resourceAndroidUrl;
    /**音频名*/
    @Excel(name = "音频名", width = 15)
    @ApiModelProperty(value = "资源名")
    private String resourceIosName;
    /**音频大小*/
    @Excel(name = "音频大小", width = 15)
    @ApiModelProperty(value = "资源大小")
    private String resourceIosSize;
    /**音频地址*/
    @Excel(name = "音频地址", width = 15)
    @ApiModelProperty(value = "资源地址")
    private String resourceIosUrl;
    /**所属展馆*/
    @Excel(name = "所属展馆", width = 15)
    @Dict(dictTable ="show_museum",dicText = "name",dicCode = "id")
    private String showMuseumId;
	/**删除标识*/
	@Excel(name = "删除标识", width = 15)
    @ApiModelProperty(value = "删除标识")
    private Integer delFlag;
	/**创建人*/
    @ApiModelProperty(value = "创建人")
    private String createBy;
	/**创建时间*/
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "创建时间")
    private Date createTime;
	/**更新人*/
    @ApiModelProperty(value = "更新人")
    private String updateBy;
	/**更新时间*/
    @JsonFormat(timezone = "GMT+8",pattern = "yyyy-MM-dd HH:mm:ss")
    @JSONField(format="yyyy-MM-dd HH:mm:ss")
    @DateTimeFormat(pattern="yyyy-MM-dd HH:mm:ss")
    @ApiModelProperty(value = "更新时间")
    private Date updateTime;

    /**场景资源名*/
    @Excel(name = "场景资源名", width = 15)
    @ApiModelProperty(value = "场景资源名")
    private String resourceSceneName;
    /**场景资源大小*/
    @Excel(name = "场景资源大小", width = 15)
    @ApiModelProperty(value = "场景资源大小")
    private String resourceSceneSize;
    /**场景资源地址*/
    @Excel(name = "场景资源地址", width = 15)
    @ApiModelProperty(value = "场景资源地址")
    private String resourceSceneUrl;
    /**是否免费*/
    @Excel(name = "是否免费", width = 15)
    @ApiModelProperty(value = "是否免费")
    private Integer freeFlag;
}
