package org.jeecg.modules.system.util;

import com.alibaba.fastjson.JSONObject;
import com.amazonaws.services.dynamodbv2.xspec.B;
import lombok.extern.slf4j.Slf4j;
import org.jeecg.config.pay.GMOConfig;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

import javax.net.ssl.HttpsURLConnection;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.InputStreamReader;
import java.io.OutputStreamWriter;
import java.math.BigDecimal;
import java.net.HttpURLConnection;
import java.net.URL;
import java.nio.charset.StandardCharsets;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

/**
 * @author : wangbinyu
 * @since : 2022/8/29
 * description :
 */
@Slf4j
@Component
public class PayUtil {

    @Autowired
    private GMOConfig gmoConfig;

    public JSONObject creatOrder(String orderNo, String productName, BigDecimal fee){
        // リクエスト先URL
        JSONObject param = new JSONObject();
        JSONObject transaction = new JSONObject();
        JSONObject getUrlParam = new JSONObject();
        param.put("configid", gmoConfig.getConfigId());
        transaction.put("OrderID", orderNo);
        transaction.put("Amount", fee.stripTrailingZeros().toPlainString());
        transaction.put("Tax", "0");
        transaction.put("Overview", productName);
        List<String> payMethods = new ArrayList<>();
        payMethods.add("credit");
        transaction.put("PayMethods", payMethods);
        transaction.put("RetUrl", "https://vr-gallery-j.com/complete.html");
        getUrlParam.put("ShopID", gmoConfig.getShopId());
        getUrlParam.put("ShopPass", gmoConfig.getShopPass());
        getUrlParam.put("GuideMailSendFlag", "0");
        getUrlParam.put("ThanksMailSendFlag", "0");
        getUrlParam.put("CustomerName", "SampleCustomerName");
        getUrlParam.put("TemplateNo", "0");
        param.put("transaction", transaction);
        param.put("geturlparam", getUrlParam);
        JSONObject credit = new JSONObject();
        credit.put("Method", "1");
//        credit.put("TdTenantName", "JingYiTest");
        param.put("credit", credit);
        HttpURLConnection con = null;
        try {
            URL url = new URL(gmoConfig.getLinkUrl());

            // リクエストコネクションの設定
            con = (HttpURLConnection) url.openConnection();
            con.setDoInput(true);
            con.setDoOutput(true);
            con.setRequestMethod("POST");
            con.setRequestProperty("Content-Type", "application/json");

            // リクエスト送信
            try (BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(con.getOutputStream(), StandardCharsets.UTF_8))) {
                bw.write(param.toJSONString());
                bw.flush();
            }

            // レスポンスチェック
            StringBuilder responseSb = new StringBuilder();
            InputStreamReader isr = null;
            if (con.getResponseCode() == HttpsURLConnection.HTTP_OK) {
                // レスポンスの取得
                isr = new InputStreamReader(con.getInputStream(), StandardCharsets.UTF_8);
            } else {
                // エラーレスポンスの取得
                isr = new InputStreamReader(con.getErrorStream(), StandardCharsets.UTF_8);
            }

            BufferedReader br = new BufferedReader(isr);
            String line;
            while ((line = br.readLine()) != null) {
                responseSb.append(line);
                responseSb.append("\r\n");
            }
            return JSONObject.parseObject(responseSb.toString());
        } catch (Exception e) {
            log.error("调用GMO网关支付异常", e);
        } finally {
            if (con != null) {
                con.disconnect();
            }
        }
        return null;
    }
    public static void main(String[] args) {
        String orderNo = "20230516005";
        String productName="test";
        BigDecimal fee = new BigDecimal("20");
        GMOConfig gmoConfig = new GMOConfig();
        gmoConfig.setConfigId("mst2000031442");
        gmoConfig.setLinkUrl("https://pt01.mul-pay.jp/payment/GetLinkplusUrlPayment.json");
        gmoConfig.setShopId("9200004874534");
        gmoConfig.setShopPass("shxsa7cd");
        // リクエスト先URL
        JSONObject param = new JSONObject();
        JSONObject transaction = new JSONObject();
        JSONObject getUrlParam = new JSONObject();
        param.put("configid", gmoConfig.getConfigId());
        transaction.put("OrderID", orderNo);
        transaction.put("Amount", fee.stripTrailingZeros().toPlainString());
//        transaction.put("Amount", "2");
        transaction.put("Tax", "0");
        transaction.put("Overview", productName);
        List<String> payMethods = new ArrayList<>();
        payMethods.add("credit");
        transaction.put("PayMethods", payMethods);
//        transaction.put("RetUrl", "http://4j8za9.natappfree.cc/boot-museum/api/app/notifyOrder");
        getUrlParam.put("ShopID", gmoConfig.getShopId());
        getUrlParam.put("ShopPass", gmoConfig.getShopPass());
        getUrlParam.put("GuideMailSendFlag", "0");
        getUrlParam.put("ThanksMailSendFlag", "0");
        getUrlParam.put("CustomerName", "SampleCustomerName");
        getUrlParam.put("TemplateNo", "0");
        param.put("transaction", transaction);
        param.put("geturlparam", getUrlParam);
        JSONObject credit = new JSONObject();
        credit.put("Method", "1");
        credit.put("TdTenantName", "JingYiTest");
        param.put("credit", credit);
        HttpURLConnection con = null;
        try {
            URL url = new URL(gmoConfig.getLinkUrl());

            // リクエストコネクションの設定
            con = (HttpURLConnection) url.openConnection();
            con.setDoInput(true);
            con.setDoOutput(true);
            con.setRequestMethod("POST");
            con.setRequestProperty("Content-Type", "application/json");

            // リクエスト送信
            try (BufferedWriter bw = new BufferedWriter(new OutputStreamWriter(con.getOutputStream(), StandardCharsets.UTF_8))) {
                bw.write(param.toJSONString());
                bw.flush();
            }

            // レスポンスチェック
            StringBuilder responseSb = new StringBuilder();
            InputStreamReader isr = null;
            if (con.getResponseCode() == HttpsURLConnection.HTTP_OK) {
                // レスポンスの取得
                isr = new InputStreamReader(con.getInputStream(), StandardCharsets.UTF_8);
            } else {
                // エラーレスポンスの取得
                isr = new InputStreamReader(con.getErrorStream(), StandardCharsets.UTF_8);
            }

            BufferedReader br = new BufferedReader(isr);
            String line;
            while ((line = br.readLine()) != null) {
                responseSb.append(line);
                responseSb.append("\r\n");
            }
            System.out.println(responseSb.toString());
        } catch (Exception e) {
            log.error("调用GMO网关支付异常", e);
        } finally {
            if (con != null) {
                con.disconnect();
            }
        }
    }
}
