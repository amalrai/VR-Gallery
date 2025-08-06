package org.jeecg.modules.system.util;

import org.apache.commons.lang3.ClassUtils;
import org.apache.commons.lang3.StringUtils;
import org.jeecgframework.poi.excel.annotation.Excel;
import org.springframework.beans.BeanUtils;

import java.lang.reflect.Field;
import java.lang.reflect.InvocationHandler;
import java.lang.reflect.Proxy;
import java.util.Map;

/**
 * @author : wangbinyu
 * @since : 2022/7/4
 * description :
 */
public class ExcelLangUtils {

    private ExcelLangUtils() {
    }

    public static Class chooseLang(Class<?> pojoClass, String lang)
            throws NoSuchFieldException, IllegalAccessException {
        //获取实体类中所有字段
        Field[] fields = pojoClass.getDeclaredFields();

        for (Field field : fields) {
            // 获取字段上的注解
            Excel anoExcel = field.getAnnotation(Excel.class);
            if (anoExcel != null) {
                // 获取代理处理器
                InvocationHandler invocationHandler = Proxy.getInvocationHandler(anoExcel);
                // 获取私有 memberValues 属性
                Field f = invocationHandler.getClass().getDeclaredField("memberValues");
                f.setAccessible(true);
                // 获取实例的属性map
                Map<String, Object> memberValues = (Map<String, Object>) f.get(invocationHandler);
                // 获取属性值
                String excelValue = (String) memberValues.get("name");

                if (StringUtils.isNotBlank(excelValue)) {
                    memberValues.put("name", MessageUtil.getMessage(excelValue, lang));
                }
                //处理枚举值
                try {
                    String[] excelReplaceValue = (String[]) memberValues.get("replace");
                    if (excelReplaceValue != null && excelReplaceValue.length > 0) {
                        String[] excelReplaceValueNew = new String[excelReplaceValue.length];
                        for (int i = 0; i < excelReplaceValue.length; i++) {
                            String text = excelReplaceValue[i].split("_")[0];
                            String value = excelReplaceValue[i].split("_")[1];
                            excelReplaceValueNew[i] = MessageUtil.getMessage(text, lang) + "_" + value;
                        }
                        memberValues.put("replace", excelReplaceValueNew);
                    }
                }catch (Exception e){
                    e.printStackTrace();
                }
            }
        }
        return pojoClass;
    }
}
