package org.jeecg.common.util;

import java.io.*;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;

/**
 * @author : wangbinyu
 * @since : 2022/8/3
 * description :
 */
public class FileUtils {
    public static boolean DecodeFile(String oldPath, String newPath) throws FileNotFoundException {
        long start = System.currentTimeMillis();
        File oldFile = new File(oldPath);
        File newFile = new File(newPath);
        int bufSize = 1048576;
        FileOutputStream fos = new FileOutputStream(newFile);
        FileChannel fcin = (new RandomAccessFile(oldFile, "r")).getChannel();
        ByteBuffer rBuffer = ByteBuffer.allocate(bufSize);

        try {
            int len;
            for(byte[] bs = new byte[bufSize]; (len = fcin.read(rBuffer)) != -1; fos.write(bs, 0, len)) {
                rBuffer.rewind();
                rBuffer.get(bs);
                rBuffer.clear();
                if (len > 3) {
                    bs[3] = (byte)(~bs[3]);
                }
            }

            fcin.close();
            fos.close();
            long end = System.currentTimeMillis();
            System.out.println("传统IO读取数据,指定缓冲区大小，总共耗时：" + (end - start) + "ms");
            return true;
        } catch (IOException var14) {
            var14.printStackTrace();
            return false;
        }
    }
    public static void EncodeByte(byte[] bs){
        if(bs.length < 3){
            return;
        }
        int bufSize = 1048576;
        //计算需要处理的次数
        int count = bs.length/bufSize + 1;
        for (int i = 0; i < count; i++) {
            bs[i*bufSize + 3] = (byte)(~bs[i*bufSize + 3]);
        }
    }
    public static InputStream EncodeFileByIs(InputStream inputStream){
        try {
            byte[] bs = new byte[inputStream.available()];
            byte[] buffer = new byte[1024];
            int lastPos = 0;
            for (int len; (len = inputStream.read(buffer)) != -1;){
                System.arraycopy(buffer, 0, bs, lastPos, len);
                lastPos+= len;
            }
            EncodeByte(bs);
            return new ByteArrayInputStream(bs);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return null;
    }
    public static boolean EncodeFile(String oldPath, String newPath) throws FileNotFoundException {
        long start = System.currentTimeMillis();
        File oldFile = new File(oldPath);
        File newFile = new File(newPath);
        int bufSize = 1048576;
        FileOutputStream fos = new FileOutputStream(newFile);
        FileChannel fcin = (new RandomAccessFile(oldFile, "r")).getChannel();
        ByteBuffer rBuffer = ByteBuffer.allocate(bufSize);

        try {
            int len;
            for(byte[] bs = new byte[bufSize]; (len = fcin.read(rBuffer)) != -1; fos.write(bs, 0, len)) {
                rBuffer.rewind();
                rBuffer.get(bs);
                rBuffer.clear();
                if (len > 3) {
                    bs[3] = (byte)(~bs[3]);
                }
            }

            fcin.close();
            fos.close();
            long end = System.currentTimeMillis();
            System.out.println("传统IO读取数据,指定缓冲区大小，总共耗时：" + (end - start) + "ms");
            return true;
        } catch (IOException var14) {
            var14.printStackTrace();
            return false;
        }
    }

    public static void main(String[] args) throws IOException {
//        EncodeFile("C:\\Users\\baby\\Desktop\\AS_03129原图.jpg","C:\\Users\\baby\\Desktop\\AS_03129原图新加密.jpg");
        DecodeFile("C:\\Users\\baby\\Desktop\\AS_03129 (1).jpg","C:\\Users\\baby\\Desktop\\AS_03129 (2).jpg");
//        File oldFile = new File("C:\\Users\\baby\\Desktop\\AS_03129原图.jpg");
//        File newFile = new File("C:\\Users\\baby\\Desktop\\AS_03129111111.jpg");
//        File encodeFile = new File("C:\\Users\\baby\\Desktop\\AS_03129.jpg");
//        InputStream oldIs = new FileInputStream(oldFile);
//        InputStream newIs = new FileInputStream(newFile);
//        InputStream encodeIs = new FileInputStream(encodeFile);
//        byte[] oldByte = new byte[oldIs.available()];
//        byte[] newByte = new byte[newIs.available()];
//        byte[] encodeByte = new byte[encodeIs.available()];
//        oldIs.read(oldByte);
//        newIs.read(newByte);
//        encodeIs.read(encodeByte);
//        for (int i = 0; i < oldByte.length; i++) {
//            if(oldByte[i] != newByte[i]){
//                System.out.println(i);
//                System.out.println(oldByte[i]);
//                System.out.println(newByte[i]);
//            }
//        }
//        oldIs.close();
//        newIs.close();
//        encodeIs.close();
    }
}
