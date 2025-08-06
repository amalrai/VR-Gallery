package org.jeecg.modules.show.comment.service.impl;

import com.baomidou.mybatisplus.core.conditions.query.QueryWrapper;
import com.baomidou.mybatisplus.core.conditions.update.UpdateWrapper;
import org.apache.commons.lang3.StringUtils;
import org.jeecg.common.api.vo.Result;
import org.jeecg.modules.show.comment.entity.ShowComment;
import org.jeecg.modules.show.comment.mapper.ShowCommentMapper;
import org.jeecg.modules.show.comment.service.IShowCommentService;
import org.jeecg.modules.show.user.entity.ShowUser;
import org.jeecg.modules.show.user.service.IShowUserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.baomidou.mybatisplus.extension.service.impl.ServiceImpl;

import java.util.Date;
import java.util.List;

/**
 * @Description: 留言管理
 * @Author: jeecg-boot
 * @Date:   2021-10-16
 * @Version: V1.0
 */
@Service
public class ShowCommentServiceImpl extends ServiceImpl<ShowCommentMapper, ShowComment> implements IShowCommentService {

    @Autowired
    private ShowCommentMapper mapper;
    @Autowired
    private IShowUserService userService;
    @Override
    public Result<String> updateUserInfo(ShowUser user) {
        mapper.update(null, new UpdateWrapper<ShowComment>().lambda()
                .set(ShowComment::getShowUserPhone, user.getPhone())
                .set(ShowComment::getShowUserEmail, user.getEmail())
                .set(ShowComment::getShowUserSex, user.getSex())
                .set(ShowComment::getShowUserNickName, user.getNickName())
                .eq(ShowComment::getShowUserId, user.getId()));
        return Result.OK();
    }

    @Override
    public Result<List<ShowComment>> getCommentList() {
        List<ShowComment> list = mapper.selectList(new QueryWrapper<ShowComment>().lambda().orderByDesc(ShowComment::getAddTime));
        return Result.OK(list);
    }

    @Override
    public Result<?> submitComment(String userId, String content) {
        if(StringUtils.isEmpty(userId) || StringUtils.isEmpty(content)){
            return Result.error("param error");
        }
        ShowUser user = userService.getById(userId);
        if(user == null){
            return Result.error("user not exist");
        }
        ShowComment comment = new ShowComment();
        comment.setAddTime(new Date());
        comment.setContent(content);
        comment.setShowUserId(user.getId());
        comment.setShowUserNickName(user.getNickName());
        comment.setShowUserSex(user.getSex());
        comment.setShowUserPhone(user.getPhone());
        comment.setShowUserEmail(user.getEmail());
        mapper.insert(comment);
        return Result.OK();
    }
}
