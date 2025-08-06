using System;
using System.Collections.Generic;

[Serializable]
public class DataJsonClass
{

    // public const string DefUrl = "https://test-vrgallery-space.adhance.jp/boot-museum"; // test server
    public const string DefUrl = "https://vrgallery-space.adhance.jp/boot-museum"; // production server

    public const string AppIDGetAppDataURL = "/api/app/getAppInfo";//根据APPID获取APP信息
    public const string AppIDGetGalleryListURL = "/api/app/getMuseumList";//根据APPID获取展馆列表
    public const string GalleryIDGetAllRoomURL = "/api/app/getRoomList";//根据展馆ID获取房间
    public const string GetRoomAllExhibitsDataURL = "/api/app/getExhibitsList";//获取房间内所有展品信息
    public const string SendRegisterCodeURL = "/api/app/sendRegisterCode";//注册发送验证码
    public const string RegisterURL = "/api/app/register";//注册提交接口
    public const string LoginURL = "/api/app/login";//登录接口
    public const string UpdateUserInfoURL = "/api/app/updateUserInfo";//完善用户信息接口
    public const string GetUserInfoURL = "/api/app/getUserInfo";//获取用户信息
    public const string ResetPwdURL = "/api/app/resetPwd";//使用旧密码修改密码
    public const string SendForgetPwdCodeURL = "/api/app/sendForgetPwdCode";//忘记密码发送验证码
    public const string ForgetPwdURL = "/api/app/forgetPwd";//使用旧密码修改密码
    public const string YoukeLoginURL = "/api/app/visitorLogin";//游客登录接口
    public const string DeleteUserByIdURL = "/api/app/deleteUserById";//注销用户接口

    public const string GetRegionInfoURL = "/api/app/getArea";//15.获取地区信息接口
    public const string UploadUserLoginLog = "/api/app/uploadLoginLog";//16.上传用户登录日志
    public const string UploadUserClickLogs = "/api/app/uploadClickLog";//17.上传用户点击日志

    public enum NetTypeListen
    {
        AppIDGetAppDataURL,//根据APPID获取APP信息
        AppIDGetGalleryListURL,//appid获取展馆列表
        GalleryIDGetAllRoomURL,//根据展馆ID获取房间
        GetRoomAllExhibitsData,//获取房间内所有展品信息
        SendRegisterCodeURL,//注册发送验证码
        RegisterURL,//注册提交接口
        LoginURL,//登录接口
        UpdateUserInfoURL,//完善用户信息接口
        GetUserInfoURL,//获取用户信息
        ResetPwdURL,//使用旧密码修改密码
        SendForgetPwdCodeURL,//忘记密码发送验证码
        ForgetPwdURL,//使用旧密码修改密码
        YoukeLoginURL,//游客登录
        DeleteUserByIdURL,//注销用户接口
        GetRegionInfoURL,//15.获取地区信息接口
        UploadUserLoginLog,//16.上传用户登录日志
        UploadUserClickLogs,//17.上传用户点击日志

        YoukeSendRegisterCodeURL,//游客注册发送验证码
        YoukeRegisterURL,//游客注册提交接口

        AppIDGetDataListURL,//appid获取文件列表，用来展示文件下载大小
        GetRoomAllExhibitsDataSize,//获取房间内所有展品信息大小
    }

    //==================================================根据APPID获取APP信息============================================
    [System.Serializable]
    public class SendAppID
    {
        public string appId;//APPID
    }
    [System.Serializable]
    public class BackGetAppData
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public AppData result;//返回数据对象
    }
    [System.Serializable]
    public class AppData
    {
        public string id;//APPID
        public string name;//APP名称
        public string description;//APP描述
        public string appAndroidName;//安卓安装包名称
        public string appAndroidStoreUrl;//安卓商店地址
        public string appAndroidUrl;//安卓安装包地址
        public string appAndroidVersion;//安卓安装包版本

        public string appIosName;//苹果安装包名称
        public string appIosStoreUrl;//苹果商店地址
        public string appIosUrl;//苹果安装包地址
        public string appIosVersion;//苹果安装包版本

        public string appName;//Win安装包名称
        public string appStoreUrl;//Win商店地址
        public string appUrl;//Win安装包地址
        public string appVersion;//Win安装包版本

        public string resourceUrl;//Win资源地址
        public string resourceName;//Win资源名称
        public string resourceSize;//Win资源大小

        public string resourceAndroidUrl;//安卓资源地址
        public string resourceAndroidName;//安卓资源名称
        public string resourceAndroidSize;//安卓资源大小

        public string resourceIosUrl;//苹果资源地址
        public string resourceIosName;//苹果资源名称
        public string resourceIosSize;//苹果资源大小

        public string createBy;//创建人ID
        public string createTime;//创建时间 yyyy-MM-dd HH:mm:ss
        public string updateBy;//更新人ID
        public string updateTime;//修改时间 yyyy-MM-dd HH:mm:ss
    }


    //==================================================根据APPID获取展馆列表============================================
    [System.Serializable]
    public class SendAppsID
    {
        public string museumId;//展馆ID
    }
    [System.Serializable]
    public class BackGalleryAllExhibitsData
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public List<AllGalleryResult> result;//返回数据对象
    }
    [System.Serializable]
    public class AllGalleryResult
    {
        public string id;//展馆ID
        public string name;//展馆名称
        public string contactName;//联系人
        public string contactMobile;//联系人电话
        public string showAppId;//所属APPID
        public string createBy;//创建人ID
        public string createTime;//创建时间 yyyy-MM-dd HH:mm:ss
        public string updateBy;//更新人ID
        public string updateTime;//修改时间 yyyy-MM-dd HH:mm:ss
    }

    //==================================================根据展馆ID获取房间============================================

    [System.Serializable]
    public class SendMuseumID
    {
        public string appId;//APP唯一标识
    }
    [System.Serializable]
    public class BackGetAllRoom
    {
        public int code;//返回代码
        public string message;
        public bool success;
        public Int64 timestamp;
        public List<AllRoomResult> result;
    }

    [System.Serializable]
    public class AllRoomResult
    {
        public string id;//房间ID
        public string name;//房间名称
        public int roomNo;//房间编号
        public string description;//房间说明
        public string musicUrl;//房间背景音乐地址
        public string musicName;//房间背景音乐名称
        public string musicSize;//房间背景音乐大小
        public string resourceAndroidUrl;//资源地址
        public string resourceAndroidName;//资源名称
        public string resourceAndroidSize;//资源大小
        public string resourceIosUrl;//ios资源地址
        public string resourceIosName;//ios资源名称
        public string resourceIosSize;//ios资源大小
        public string resourceUrl;//资源地址
        public string resourceName;//资源名称
        public string resourceSize;//资源大小
        public int delFlag;//删除标识
        public string createBy;//创建人ID
        public string createTime;//创建时间 yyyy-MM-dd HH:mm:ss
        public string updateBy;//更新人ID
        public string updateTime;//修改时间 yyyy-MM-dd HH:mm:ss
        public string showMuseumId;//所属展馆ID
    }
    //==================================================获取房间内所有展品信息============================================
    [System.Serializable]
    public class SendRoomID
    {
        public string roomId;//房屋id
    }
    [System.Serializable]
    public class BackRoomAllExhibitsData
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public List<RoomAllExhibitsData> result;//返回数据对象
    }

    [System.Serializable]
    public class RoomAllExhibitsData
    {
        public string id;//房间ID
        public string showRoomId;//所属房间ID
        public int exhibitsNo;//展品编号
        public string name;//展品名
        public int status;//状态 0启用1禁用
        public int auth;//权限 0免费1VIP
        public string text;//文字简介
        public string mainGraphUrl;//主图地址
        public string mainGraphEncodeUrl;//主图加密地址
        public string mainGraphName;//主图名称
        public string mainGraphSize;//主图大小
        public string mainGraphTime;//主图上传时间yyyy-MM-dd HH:mm:ss
        public string introductionImageUrl;//介绍图地址
        public string introductionImageEncodeUrl;//介绍图加密地址
        public string introductionImageName;//介绍图名称
        public string introductionImageSize;//介绍图大小
        public string introductionImageTime;//介绍图上传时间yyyy-MM-dd HH:mm:ss
        public string videoUrl;//视频地址
        public string videoEncodeUrl;//视频加密地址
        public string videoName;//视频名称
        public string videoSize;//视频大小
        public string videoTime;//视频上传时间yyyy-MM-dd HH:mm:ss
        public string voiceUrl;//音频地址
        public string voiceName;//音频名称
        public string voiceSize;//音频大小
        public string voiceTime;//音频上传时间yyyy-MM-dd HH:mm:ss
        public string animationThumbnailUrl;//动画缩略图地址
        public string animationThumbnailEncodeUrl;//动画缩略图加密地址
        public string animationThumbnailName;//动画缩略图名称
        public string animationThumbnailSize;//动画缩略图大小
        public string animationThumbnailTime;//动画缩略图上传时间yyyy-MM-dd HH:mm:ss
        public string frameAnimationUrl;//帧动画地址（多个，隔开）
        public string frameAnimationEncodeUrl;//帧动画加密地址（多个，隔开）
        public string frameAnimationName;//帧动画名称
        public string frameAnimationSize;//帧动画大小
        public string frameAnimationTime;//帧动画上传时间yyyy-MM-dd HH:mm:ss
        public string videoLink;//视频外链
        public string videoLinkTime;//视频外链更新时间yyyy-MM-dd HH:mm:ss
        public string webLink;//网页外链
        public string webLinkTime;//网页外链更新时间yyyy-MM-dd HH:mm:ss
        public int version;//版本号（每当数据修改时累加1，但不代表图片或视频资源一定有变动，实际是否触发下载还需要判断url地址）
        public string createBy;//创建人ID
        public string createTime;//创建时间 yyyy-MM-dd HH:mm:ss
        public string updateBy;//更新人ID
        public string updateTime;//修改时间 yyyy-MM-dd HH:mm:ss
        public int count;

    }

    //==================================================注册发送验证码============================================
    [System.Serializable]
    public class SendEmail
    {
        public string appId;//APP唯一标识
        public string email;//邮箱
    }
    [System.Serializable]
    public class BackSendDataResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public List<object> result;//返回数据对象
    }

    //==================================================注册提交接口============================================
    [System.Serializable]
    public class RegisterData
    {
        public string appId;//APP唯一标识
        public string appName;//所属APP
        public string code;//验证码
        public string email;//邮箱
        public string pwd;//密码
        public int sex;//性别
        public int years;//年代
        public int areaId;//地区ID
    }
    [System.Serializable]
    public class BackRegisterDataResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public string result;//返回数据对象
    }

    //==================================================登录接口============================================
    [System.Serializable]
    public class LoginData
    {
        public string appId;//APP唯一标识
        public string email;//邮箱
        public string pwd;//密码
    }
    [System.Serializable]
    public class BackLoginDataResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public LoginUserDataResultNew result;//返回数据对象
    }
    [System.Serializable]
    public class LoginUserDataResultNew
    {
        public string activateTime;//激活时间
        public string address;//收货地址
        public int age;//年龄
        public string appName;//所属APP
        public string avatar;//头像url
        public string email;//邮箱
        public int firstLogin;//是否第一次登录0首次 1非首次
        public string id;//用户ID
        public int isAuth;//会员权限0关闭 1开启
        public string nickName;//昵称
        public string phone;//手机号
        public string pwd;//密码
        public string realName;//姓名
        public string registerTime;//注册时间
        public int sex;//性别
        public int state;//状态0禁用 1启用
        public int type;//会员类型
        public string createBy;//创建人ID
        public string createTime;//创建时间 yyyy-MM-dd HH:mm:ss
        public string updateBy;//更新人ID
        public string updateTime;//修改时间 yyyy-MM-dd HH:mm:ss
        public string showAppId;//APP唯一标识 当前账号对应的app
        public int years;//年代 APP自行枚举对照"10代_1","20代_2","30代_3","40代_4","50代_5","60代_6","70代～_7"
        public int areaId;//地区ID
        public string areaName;//地区名称
        public string detailAddress;//详细地址
    }
    //==================================================完善用户信息接口============================================
    [System.Serializable]
    public class UserInfo
    {
        public string id;//用户ID
        public int age;//年龄
        public string avatar;//头像base64编码
        public string nickName;//昵称
        public string phone;//手机号
        public string realName;//姓名
        public int sex;//性别
        public int years;//年代 APP自行枚举对照"10代_1","20代_2","30代_3","40代_4","50代_5","60代_6","70代～_7
        public int areaId;//地区ID
        public string detailAddress;//详细地址
    }
    [System.Serializable]
    public class BackUpdateUserInfoResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public object result;//返回数据对象
    }
    //==================================================获取用户信息============================================
    [System.Serializable]
    public class GetUser
    {
        public int id;//用户ID
    }
    [System.Serializable]
    public class BackGetUserInfoResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public GetUserDataResult result;//返回数据对象
    }
    [System.Serializable]
    public class GetUserDataResult
    {
        public string activateTime;//激活时间
        public string address;//收货地址
        public int age;//年龄
        public string appName;//所属APP
        public string avatar;//头像url
        public string email;//邮箱
        public int firstLogin;//是否第一次登录0首次 1非首次
        public string id;//用户ID
        public int isAuth;//会员权限0关闭 1开启
        public string nickName;//昵称
        public string phone;//手机号
        public string pwd;//密码
        public string realName;//姓名
        public string registerTime;//注册时间
        public int sex;//性别
        public int state;//状态0禁用 1启用
        public int type;//会员类型
        public string createBy;//创建人ID
        public string createTime;//创建时间 yyyy-MM-dd HH:mm:ss
        public string updateBy;//更新人ID
        public string updateTime;//修改时间 yyyy-MM-dd HH:mm:ss
    }

    //==================================================使用旧密码修改密码============================================
    [System.Serializable]
    public class ResetPwdData
    {
        public string appId;//APP唯一标识
        public string email;//邮箱
        public string newPwd;//新密码
        public string oldPwd;//旧密码

    }
    [System.Serializable]
    public class BackResetPwdResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public object result;//返回数据对象
    }
    //==================================================忘记密码发送验证码============================================
    [System.Serializable]
    public class SendForgetPwdCode
    {
        public string appId;//APP唯一标识
        public string email;//邮箱

    }
    [System.Serializable]
    public class BackSendForgetPwdCodeResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public object result;//返回数据对象
    }
    //==================================================使用旧密码修改密码============================================
    [System.Serializable]
    public class ResetPwdDataCode
    {
        public string appId;//APP唯一标识
        public string email;//邮箱
        public string newPwd;//新密码
        public string code;//验证码
    }
    [System.Serializable]
    public class BackResetPwdDataCodeResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public object result;//返回数据对象
    }
    //==================================================游客登录接口============================================
    [System.Serializable]
    public class BackYoukeCodeResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public object result;//返回数据对象
    }
    //==================================================注销用户接口============================================
    [System.Serializable]
    public class SendUserID
    {
        public string id;//唯一标识
    }
    [System.Serializable]
    public class BackDeleteUserByIdCodeResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public object result;//返回数据对象
    }
    //==================================================获取地区信息接口============================================
    [System.Serializable]
    public class BackRegionInfoByIdCodeResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public List<GetRegionInfoResult> result;//返回数据对象
    }
    [System.Serializable]
    public class GetRegionInfoResult
    {
        public string id;//地区ID
        public string name;//地区名称
    }
    //==================================================上传用户登录日志============================================
    [System.Serializable]
    public class SendUserLoginInfo
    {
        public string userId;//用户ID
        public string roomId;//房间ID
        public string museumId;//展馆ID
    }
    [System.Serializable]
    public class BackResultByUserLoginInfoCodeResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public object result;//返回数据对象
    }
    //==================================================上传用户点击日志============================================
    [System.Serializable]
    public class SendUserClickInfo
    {
        public string loginLogId;//登录日志ID
        public string exhibitsId;//展品ID
        public string introductionCount;//简介点击次数
        public string linkCount;//链接点击次数
        public string videoCount;//视频点击次数
        public string voiceCount;//音频点击次数
        public string shopCount;//商店点击次数
    }
    [System.Serializable]
    public class BackResultByUserClickInfoCodeResult
    {
        public int code;//返回码 200代表成功其余失败
        public string message;//返回信息
        public bool success;//成功标识 true代表成功
        public Int64 timestamp;//时间戳
        public object result;//返回数据对象
    }
}
