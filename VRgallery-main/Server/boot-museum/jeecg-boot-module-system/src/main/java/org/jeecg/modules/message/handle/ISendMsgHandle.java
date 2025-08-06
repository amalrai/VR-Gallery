package org.jeecg.modules.message.handle;

public interface ISendMsgHandle {

	boolean SendMsg(String es_receiver, String es_title, String es_content);
}
