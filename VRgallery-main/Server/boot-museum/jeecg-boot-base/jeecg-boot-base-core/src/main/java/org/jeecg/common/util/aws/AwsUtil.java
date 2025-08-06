package org.jeecg.common.util.aws;

import com.alibaba.fastjson.JSON;
import com.amazonaws.services.s3.AmazonS3;
import com.amazonaws.services.s3.model.CannedAccessControlList;
import com.amazonaws.services.s3.model.GeneratePresignedUrlRequest;
import com.amazonaws.services.s3.model.ObjectMetadata;
import com.amazonaws.services.s3.model.PutObjectRequest;
import lombok.extern.slf4j.Slf4j;
import org.jeecg.common.api.vo.CommonFileVo;
import org.jeecg.common.util.CommonUtils;
import org.jeecg.common.util.DateUtils;
import org.jeecg.common.util.FileUtils;
import org.jeecg.common.util.filter.FileTypeFilter;
import org.jeecg.common.util.filter.StrAttackFilter;
import org.springframework.web.multipart.MultipartFile;

import java.io.*;
import java.math.BigDecimal;

@Slf4j
public class AwsUtil {
    private static AmazonS3 s3;

    private static String accessKeyId;
    private static String accessKeySecret;
    private static String bucketName;
    private static String staticDomain;


    public AwsUtil() {
    }

    public static boolean uploadToS3(File tempFile, String remotePath, String remoteFileName) {
        try {
            String bucketPath = bucketName + remotePath;
            s3.putObject((new PutObjectRequest(bucketPath, remoteFileName, tempFile)).withCannedAcl(CannedAccessControlList.PublicRead));
            new GeneratePresignedUrlRequest(bucketName, remoteFileName);
            return true;
        } catch (Exception var5) {
            var5.printStackTrace();
            return false;
        }
    }

    public static String uploadToS3ByInput(InputStream input, String key) {
        try {
            ObjectMetadata objectMetadata = new ObjectMetadata();
            objectMetadata.setContentLength(input.available());
            s3.putObject((new PutObjectRequest(bucketName, key, input, objectMetadata)).withCannedAcl(CannedAccessControlList.PublicRead));
            new GeneratePresignedUrlRequest(bucketName, key);
            return key;
        } catch (Exception var5) {
            var5.printStackTrace();
            return null;
        }
    }

    public static void main(String[] args) throws FileNotFoundException {
        String path = "https://awsjingyi.s3.cn-northwest-1.amazonaws.com.cn/";
        File file = new File("C:\\Users\\baby\\Desktop\\新建 文本文档 (2).txt");
        InputStream input = new FileInputStream(file);
        System.out.println(path + uploadToS3ByInput(input, "/ia/test/test.txt"));
    }

    public static String upload(MultipartFile file, String fileDir, boolean isEncode) {
        String FILE_URL;
        String encodeFileUrl = null;
        InputStream encodeFileInput = null;
        try {
            String fileSize = new BigDecimal(file.getSize()).divide(new BigDecimal("1048576"), 2, BigDecimal.ROUND_CEILING) + "M";
            StringBuilder fileUrl = new StringBuilder();

            // 获取文件名
            String orgName = file.getOriginalFilename();
            if ("" == orgName) {
                orgName = file.getName();
            }
            FileTypeFilter.fileTypeFilter(file);
            //update-end-author:liusq date:20210809 for: 过滤上传文件类型
            orgName = CommonUtils.getFileName(orgName);
            String fileName = orgName.indexOf(".") == -1
                    ? orgName + "_" + System.currentTimeMillis()
                    : orgName.substring(0, orgName.lastIndexOf(".")) + "_" + System.currentTimeMillis() + orgName.substring(orgName.lastIndexOf("."));
            if (!fileDir.endsWith("/")) {
                fileDir = fileDir.concat("/");
            }
            fileDir = StrAttackFilter.filter(fileDir);
            fileUrl.append(fileDir).append(fileName);
            FILE_URL = staticDomain + fileUrl;
            String result = uploadToS3ByInput(file.getInputStream(), fileUrl.toString());

            // 设置权限(公开读)
//            ossClient.setBucketAcl(newBucket, CannedAccessControlList.PublicRead);
            if (result != null) {
                log.info("------AWS文件上传成功------" + fileUrl);
            }
            //构造加密文件
            if(isEncode){
                StringBuilder fileEncodeUrl = new StringBuilder();
                //文件地址
                fileEncodeUrl.append(fileDir).append("encode/").append(fileName);
                byte[] encodeFile = file.getBytes();
                FileUtils.EncodeByte(encodeFile);
                encodeFileInput = new ByteArrayInputStream(encodeFile);

                String resultEncode = uploadToS3ByInput(encodeFileInput, fileEncodeUrl.toString());
                if(resultEncode != null){
                    log.info("------AWS加密文件上传成功------" + fileEncodeUrl);
                    encodeFileUrl = staticDomain + fileEncodeUrl;
                }
            }
            CommonFileVo commonFileVo = new CommonFileVo();
            commonFileVo.setName(orgName);
            commonFileVo.setSize(fileSize);
            commonFileVo.setUrl(FILE_URL);
            commonFileVo.setEncodeUrl(encodeFileUrl);
            commonFileVo.setTime(DateUtils.getDataString(DateUtils.datetimeFormat.get()));
            return JSON.toJSONString(commonFileVo);
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        } finally {
            if(encodeFileInput != null){
                try {
                    encodeFileInput.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    public static String getAccessKeyId() {
        return accessKeyId;
    }

    public static void setAccessKeyId(String accessKeyId) {
        AwsUtil.accessKeyId = accessKeyId;
    }

    public static String getAccessKeySecret() {
        return accessKeySecret;
    }

    public static void setAccessKeySecret(String accessKeySecret) {
        AwsUtil.accessKeySecret = accessKeySecret;
    }

    public static String getBucketName() {
        return bucketName;
    }

    public static void setBucketName(String bucketName) {
        AwsUtil.bucketName = bucketName;
    }

    public static String getStaticDomain() {
        return staticDomain;
    }

    public static void setStaticDomain(String staticDomain) {
        AwsUtil.staticDomain = staticDomain;
    }

    public static void setS3(AmazonS3 s3) {
        AwsUtil.s3 = s3;
    }

    public static AmazonS3 getS3() {
        return AwsUtil.s3;
    }
}
