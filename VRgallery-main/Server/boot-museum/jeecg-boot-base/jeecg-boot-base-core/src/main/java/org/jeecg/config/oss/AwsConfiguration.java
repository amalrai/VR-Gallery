package org.jeecg.config.oss;

import com.amazonaws.auth.BasicAWSCredentials;
import com.amazonaws.regions.Region;
import com.amazonaws.regions.Regions;
import com.amazonaws.services.s3.AmazonS3Client;
import org.jeecg.common.util.aws.AwsUtil;
import org.jeecg.common.util.oss.OssBootUtil;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

/**
 * 云存储 配置
 */
@Configuration
public class AwsConfiguration {

    @Value("${jeecg.aws.accessKey}")
    private String accessKey;
    @Value("${jeecg.aws.secretKey}")
    private String accessKeySecret;
    @Value("${jeecg.aws.bucketName}")
    private String bucketName;
    @Value("${jeecg.aws.staticDomain}")
    private String staticDomain;
    @Value("${jeecg.aws.region}")
    private String region;

    @Bean
    public void initAwConfiguration() {
        AwsUtil.setAccessKeyId(accessKey);
        AwsUtil.setAccessKeySecret(accessKeySecret);
        AwsUtil.setBucketName(bucketName);
        AwsUtil.setStaticDomain(staticDomain);
        AwsUtil.setS3(new AmazonS3Client(new BasicAWSCredentials(accessKey, accessKeySecret)));
        AwsUtil.getS3().setRegion(Region.getRegion(Regions.fromName(region)));
    }
}
