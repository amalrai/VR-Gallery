package org.jeecg.modules.system.util;

import org.jeecg.common.util.aws.AwsUtil;
import sun.misc.BASE64Decoder;

import java.io.*;

/**
 * @author : wangbinyu
 * @since : 2022/7/3
 * description :
 */
public class Base64ImageUtil {

    /**
     * 将图片Base64编码转换成img图片文件
     *
     * @param imgBase64 图片Base64编码
     * @param imgPath   图片生成路径
     * @return
     */
    public static boolean getImgBase64ToImgFileAndUpload(String imgBase64, String imgPath) {
        boolean flag = true;
        InputStream inputStream = null;
        try {
            // 解密处理数据
            byte[] bytes = new BASE64Decoder().decodeBuffer(imgBase64);
            for (int i = 0; i < bytes.length; ++i) {
                if (bytes[i] < 0) {
                    bytes[i] += 256;
                }
            }
            inputStream = new ByteArrayInputStream(bytes);
            if(AwsUtil.uploadToS3ByInput(inputStream, imgPath) == null){
                return false;
            }
        } catch (Exception e) {
            e.printStackTrace();
            flag = false;
        } finally {
            if (inputStream != null) {
                try {
                    // 关闭outputStream流
                    inputStream.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
        return flag;
    }
}
