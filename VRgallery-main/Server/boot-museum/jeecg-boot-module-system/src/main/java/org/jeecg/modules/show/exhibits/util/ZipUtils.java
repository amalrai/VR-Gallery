package org.jeecg.modules.show.exhibits.util;

import com.alibaba.fastjson.JSON;
import org.jeecg.common.api.vo.CommonFileVo;
import org.jeecg.common.api.vo.Result;
import org.jeecg.common.config.mqtoken.UserLocalContext;
import org.jeecg.common.util.DateUtils;
import org.jeecg.common.util.FastImageInfo;
import org.jeecg.common.util.FileUtils;
import org.jeecg.common.util.aws.AwsUtil;
import org.jeecg.common.util.filter.StrAttackFilter;
import org.jeecg.modules.system.util.MessageUtil;
import org.springframework.web.multipart.MultipartFile;

import java.io.*;
import java.math.BigDecimal;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.Enumeration;
import java.util.List;
import java.util.UUID;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;

/**
 * @author : wangbinyu
 * @since : 2022/6/21
 * description :
 */
public class ZipUtils {
    private static final String accept = "jpg";
    private static final int limitWidth = 1000;
    private static final int limitHeight = 1000;
    private static final int limitNumber = 50;
    private static final int limitSize = 200 * 1024;
    /**
     * 解压
     * @param zipPath zip 文件夹路径
     * @param targetPath 解压路径
     */
    public static void unzip(String zipPath,String targetPath){
        File pathFile = new File(targetPath);
        if(!pathFile.exists()){
            pathFile.mkdirs();
        }

        try{
            //指定编码
            try(ZipFile zipFile = new ZipFile(zipPath, Charset.forName("gbk"))) {
                //遍历里面的文件及文件夹
                Enumeration entries = zipFile.entries();
                while (entries.hasMoreElements()) {
                    ZipEntry entry = (ZipEntry) entries.nextElement();
                    String zipEntryName = entry.getName();


                    try (InputStream in = zipFile.getInputStream(entry)) {
                        String outpath = (targetPath + File.separator + zipEntryName);
                        //判断路径是否存在，不存在则创建文件路径
                        File file = new File(outpath.substring(0, outpath.lastIndexOf(File.separator)));
                        if (!file.exists()) {
                            file.mkdirs();
                        }
                        //判断文件全路径是否为文件夹
                        if (new File(outpath).isDirectory()) {
                            continue;
                        }


                        try (OutputStream out = new FileOutputStream(outpath)) {
                            byte[] buffer = new byte[1024];
                            int len;
                            while ((len = in.read(buffer)) > 0) {
                                out.write(buffer, 0, len);
                            }
                        }
                    }
                }
            }
        }catch ( Exception e){
            e.printStackTrace();
        }
    }
    /**
     * 解压
     * @param zipPath zip 文件夹路径
     * @param targetPath 解压路径
     */
    public static Result<CommonFileVo> unzipAndUpload(MultipartFile inFile, String fileDir, boolean isEncode){
        Result<CommonFileVo> result = new Result<>();
        StringBuilder fileUrls = new StringBuilder();
        StringBuilder fileEncodeUrls = new StringBuilder();
        String fileSize = new BigDecimal(inFile.getSize()).divide(new BigDecimal("1048576"), 2, BigDecimal.ROUND_CEILING) + "M";
        File tmpFile = null;
        ZipFile zipFile;
        InputStream fileStream = null;
        Path path = Paths.get(System.getProperty("java.io.tmpdir"), inFile.getName()+ UUID.randomUUID().toString()+"zip");
        try{
            //1.文件保存到本地
            tmpFile = path.toFile();
            fileStream = inFile.getInputStream();
            org.apache.commons.io.FileUtils.copyInputStreamToFile(fileStream, tmpFile);
            //2.从本地解析ZipFile对象
            zipFile = new ZipFile(tmpFile, Charset.forName("GBK"));
            Enumeration entries = zipFile.entries();
            List<ZipEntry> zipEntryList = new ArrayList<>();
            //遍历里面的文件及文件夹 check文件名
            int lastLength = 0;


            //上传图片格式不正确，请重新上传压缩包”
            //“分辨率超过1000x1000，请重新上传压缩包”
            //“图片容量超过200k，请重新上传压缩包”
            //“超过50张，请重新上传压缩包
            while (entries.hasMoreElements()) {
                ZipEntry entry = (ZipEntry) entries.nextElement();
                String zipEntryName = entry.getName();
                String fileExt = zipEntryName.substring(zipEntryName.lastIndexOf(".") + 1);
                if(!accept.equalsIgnoreCase(fileExt)){
                    throw new Exception(MessageUtil.getMessage("上传图片格式不正确，请重新上传压缩包！", UserLocalContext.getLocal()));
                }
                if(entry.getSize() > limitSize){
                    throw new Exception(MessageUtil.getMessage("图片容量超过200k，请重新上传压缩包！", UserLocalContext.getLocal()));
                }
                FastImageInfo imageInfo = new FastImageInfo(zipFile.getInputStream(entry));
                if(imageInfo.getWidth() > limitWidth || imageInfo.getHeight() > limitHeight){
                    throw new Exception(MessageUtil.getMessage("分辨率超过1000x1000，请重新上传压缩包！", UserLocalContext.getLocal()));
                }
                if(lastLength != 0 && lastLength != zipEntryName.length()){
                    throw new Exception(MessageUtil.getMessage("文件名长度不一致请修改！", UserLocalContext.getLocal()));
                }
                lastLength = zipEntryName.length();
                zipEntryList.add(entry);
            }
            if(zipEntryList.size() > limitNumber){
                throw new Exception(MessageUtil.getMessage("超过50张，请重新上传压缩包！", UserLocalContext.getLocal()));
            }
            if (!fileDir.endsWith("/")) {
                fileDir = fileDir.concat("/");
            }
            fileDir = fileDir + System.currentTimeMillis() + "/";
            fileDir = StrAttackFilter.filter(fileDir);

            for (ZipEntry entry : zipEntryList) {
                String fileName = entry.getName();
                String filePath = fileDir + fileName;
                fileUrls.append(",").append(AwsUtil.getStaticDomain()).append(filePath);
                InputStream zipInput = zipFile.getInputStream(entry);
                if(AwsUtil.uploadToS3ByInput(zipInput, filePath) == null){
                    throw new Exception(MessageUtil.getMessage("上传失败", UserLocalContext.getLocal()));
                }
                if(isEncode){
                    InputStream encodeInput = FileUtils.EncodeFileByIs(zipFile.getInputStream(entry));
                    if(encodeInput == null){
                        throw new Exception(MessageUtil.getMessage("上传失败", UserLocalContext.getLocal()));
                    }
                    String encodeFilePath = fileDir +"encode/"+ fileName;
                    fileEncodeUrls.append(",").append(AwsUtil.getStaticDomain()).append(encodeFilePath);
                    if(AwsUtil.uploadToS3ByInput(encodeInput, encodeFilePath) == null){
                        throw new Exception(MessageUtil.getMessage("上传失败", UserLocalContext.getLocal()));
                    }
                }
            }
            CommonFileVo commonFileVo = new CommonFileVo();
            commonFileVo.setName(inFile.getOriginalFilename());
            commonFileVo.setSize(fileSize);
            commonFileVo.setUrl(fileUrls.substring(1));
            if(fileEncodeUrls.length()>0){
                commonFileVo.setEncodeUrl(fileEncodeUrls.substring(1));
            }
            commonFileVo.setTime(DateUtils.getDataString(DateUtils.datetimeFormat.get()));
            result.setResult(commonFileVo);
            result.setSuccess(true);
            result.setMessage(commonFileVo.getUrl());
        }catch (Exception e){
            e.printStackTrace();
            result.setSuccess(false);
            result.setMessage(MessageUtil.getMessage("上传失败", UserLocalContext.getLocal()) +"!"+ e.getMessage());
        }finally {
            try {
                if(fileStream != null){
                    fileStream.close();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
            org.apache.commons.io.FileUtils.deleteQuietly(tmpFile);
        }
        return result;
    }
    public static void main(String[] args) {
        unzip("C:\\Users\\baby\\Desktop\\test.zip", "C:\\Users\\baby\\Desktop\\");
    }
}
