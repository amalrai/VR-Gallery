package org.jeecg.modules.show.comment.service;

import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.comment.entity.ShowComment;
import com.baomidou.mybatisplus.extension.service.IService;
import org.jeecg.modules.show.user.entity.ShowUser;

import java.util.List;

/**
 * @Description: 留言管理
 * @Author: jeecg-boot
 * @Date:   2021-10-16
 * @Version: V1.0
 */
public interface IShowCommentService extends IService<ShowComment> {
    Result<String> updateUserInfo(ShowUser user);

    Result<List<ShowComment>> getCommentList();

    Result<?> submitComment(String userId, String content);
}
