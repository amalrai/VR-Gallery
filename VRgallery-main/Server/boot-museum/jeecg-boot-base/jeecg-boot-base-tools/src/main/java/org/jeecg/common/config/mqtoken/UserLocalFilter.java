package org.jeecg.common.config.mqtoken;


import javax.servlet.*;
import javax.servlet.annotation.WebFilter;
import javax.servlet.http.HttpServletRequest;
import java.io.IOException;

/**
 * 用户token上下文
 * @author zyf
 */
@WebFilter("/*")
public class UserLocalFilter implements Filter {

    private static String LOCAL = "local";

    @Override
    public void init(FilterConfig filterConfig) throws ServletException {

    }

    @Override
    public void doFilter(ServletRequest servletRequest, ServletResponse servletResponse, FilterChain filterChain) throws IOException, ServletException {
        this.initUserLocal((HttpServletRequest) servletRequest);
        filterChain.doFilter(servletRequest, servletResponse);
    }

    private void initUserLocal(HttpServletRequest request) {
        String local = request.getHeader(LOCAL);
        if (local!=null) {
            try {
                //将token放入上下文中
                UserLocalContext.setLocal(local);
            } catch (Exception e) {

            }
        }
    }

    @Override
    public void destroy() {

    }
}
