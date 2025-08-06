package org.jeecg.modules.show.area.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import io.swagger.annotations.ApiModel;
import io.swagger.annotations.ApiModelProperty;
import lombok.Data;
import lombok.EqualsAndHashCode;
import lombok.experimental.Accessors;
import org.jeecgframework.poi.excel.annotation.Excel;

import java.io.Serializable;

/**
 * @author : wangbinyu
 * @since : 2023/3/23
 * description :
 */
@Data
@TableName("area")
@Accessors(chain = true)
@EqualsAndHashCode(callSuper = false)
@ApiModel(value="area对象", description="地区")
public class Area implements Serializable {
    /**id*/
    @TableId(type = IdType.ASSIGN_ID)
    @ApiModelProperty(value = "id")
    private java.lang.String id;
    /**地区名称*/
    @Excel(name = "地区名称", width = 15)
    @ApiModelProperty(value = "地区名称")
    private java.lang.String name;
}
