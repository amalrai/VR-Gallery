package org.jeecg.config.pay;

import lombok.Data;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Configuration;

/**
 * @author : wangbinyu
 * @since : 2023/4/6
 * description :
 */
@Configuration
@Data
public class GMOConfig {
    @Value("${api.gmo.url:}")
    private String linkUrl;
    @Value("${api.gmo.configId:}")
    private String configId;
    @Value("${api.gmo.shopId:}")
    private String shopId;
    @Value("${api.gmo.shopPass:}")
    private String shopPass;
}
