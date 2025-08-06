<template>
  <div class="uploadImgBody" v-if="Refresh">
    <!--展示已上传图片部分-->
    <imgView v-if="!showFileList" class="imgViewDiv" :imageList="fileList" @deleteImg="deleteImg"></imgView>
    <!--上传图片部分-->
    <el-upload
      v-if="isEdit"
      class="upload-image"
      ref="upload"
      :action="action"
      :headers="headers"
      :multiple="multiple"
      :data="data"
      :name="name"
      :with-credentials="cookieOK"
      :show-file-list="showFileList"
      :drag="drag"
      :accept="accept"
      :list-type="listType"
      :auto-upload="autoUpload"
      :file-list="fileList"
      :disabled="is_disabled"

      :on-preview="handlePreview"
      :on-remove="handleRemove"
      :on-success="handleSuccess"
      :on-error="handleError"
      :on-progress="handleProgress"
      :on-exceed="handleExceed"
      :on-change="onChange"

      :before-upload="beforeUpload"
      :before-remove="beforeRemove"
      :http-request="httpRequest"
    >
      <div slot="default" class="uploadImgDiv">
        <i class="el-icon-plus"></i>
        <!--提示信息-->
        <div slot="tip" class="el-upload__tip" v-if="tip_text!=''">{{tip_text}}</div>
      </div>

      <!--弹框展示上传以后的图片-->
      <div slot="file" slot-scope="{file}">
        <img
          class="el-upload-list__item-thumbnail"
          :src="file.url" alt=""
        >
        <span class="el-upload-list__item-actions">
              <span
                class="el-upload-list__item-preview"
                @click="handlePictureCardPreview(file)"
              >
                <i class="el-icon-zoom-in"></i>
              </span>
              <span
                v-if="!disabled"
                class="el-upload-list__item-delete"
                @click="handleRemove(file)"
              >
                <i class="el-icon-delete"></i>
              </span>
            </span>

      </div>

    </el-upload>
    <el-dialog :visible.sync="dialogVisible">
      <img width="100%" :src="dialogImageUrl" alt="">
    </el-dialog>
  </div>

</template>

<script>
import imgView from './imgView'
//element的上传文件组件
export default {
  props:{
    /**
     * 自动上传参数
     * */
    autoUpload:{ // 是否需要选取完自动上传功能
      type: Boolean,
      default: false
    },
    action:{//上传的地址
      type: String,
      default: ''
    },
    headers: {//设置上传的请求头部
      type:Object,
      default: () => {
        return {}
      }
    },
    data: {//上传时额外带的参数
      type:Object,
      default: () => {
        return {}
      }
    },
    name:{//上传的文件字段名
      type: String,
      default: 'file'
    },
    cookieOK:{//支持发送 cookie 凭证信息
      type: Boolean,
      default: true
    },
    /**
     * 公共参数
     * */
    showFileList:{//是否显示已上传文件列表
      type: Boolean,
      default: false
    },
    drag:{//是否启用拖拽上传
      type: Boolean,
      default: true
    },
    accept:{//接受文件类型-图片上传类型-不同的格式之间以逗号隔开
      type: String,
      // default:'.doc,.docx,application/msword,application/vnd.openxmlformats-officedocument.wordprocessingml.document'
      default: '.jpg,.jpeg,.png'
    },
    listType:{ // 文件列表的类型 - text/picture/picture-card
      type: String,
      default: 'picture-card'
    },
    fileList:{//已上传的文件列表,
      type:Array,
      default: () => {
        // { 默认格式
        //   name: 'food.jpeg',
        //   url: 'https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/thumbnail/360x360/format/webp/quality/100'
        // }
        return [
          {
            name: 'food.jpeg',
            url: 'https://fuss10.elemecdn.com/3/63/4e7f3a15429bfda99bce42a18cdd1jpeg.jpeg?imageMogr2/thumbnail/360x360/format/webp/quality/100'
          }
        ]
      }
    },
    is_disabled:{//是否禁止，true是禁止，false不禁止
      type: Boolean,
      default: false
    },
    multiple:{//是否可以多选
      type: Boolean,
      default: true
    },
    limit:{//最大允许上传个数
      type: Number,
      default: 30
    },

    tip_text:{//提示信息
      type: String,
      default: ''
    },
    /**
     * 手动上传参数
     * */
    needFromUpload:{ // form表单,将上传的file文件通过 formUpload  方法发送出去
      type: Boolean,
      default: false
    },
    isEdit:{ //是否可编辑
      type: Boolean,
      default: true
    },


  },
  watch: {},
  data() {
    return {
      Refresh:true,//强制刷新
      dialogImageUrl: '',
      dialogVisible: false,
      disabled: false,
    }
  },
  created() {
  },
  mounted() {
  },
  methods: {
    /**
     * 上传-默认事件
     * */
    //文件列表移除文件时的钩子
    handleRemove(file, fileList) {
      console.log('当前移除的是:');
      console.log(file);
    },
    //点击文件列表中已上传的文件时的钩子
    handlePreview(file) {
      console.log('当前点击的是'+file);
    },
    //文件上传成功时的钩子
    handleSuccess(response, file, fileList) {
      console.log('文件上传成功', response);
    },
    //文件上传失败时的钩子
    handleError(err, file, fileList) {
      console.log('文件上传失败');
    },
    //文件上传时的钩子
    handleProgress(event, file, fileList) {
      console.log(file);
    },
    //文件超出个数限制时的钩子
    handleExceed(files, fileList) {
      console.log('文件超出个数限制');
    },
    //覆盖默认的上传行为，可以自定义上传的实现
    httpRequest(){

    },
    //删除文件之前的钩子，参数为上传的文件和文件列表，若返回 false 或者返回 Promise 且被 reject，则停止删除。
    beforeRemove(file, fileList) {
      console.log('当前删除的文件'+file);
      this.fileList.forEach((item,index)=>{
        if(item == file){
          this.fileList.splice(index,1)
        }
      })
    },

    /**
     * 文件状态改变时的钩子，添加文件、上传成功和上传失败时都会被调用
     */
    onChange(file, fileList) {
      this.fileList = fileList;
      console.log('当前的选中文件：');
      console.log(fileList);
    },
    /**
     * 上传文件之前的钩子，参数为上传的文件，若返回 false 或者返回 Promise 且被 reject，则停止上传。
     */
    beforeUpload(file) {
      console.log(file);
    },
    /**
     * 上传-自定义事件
     * */
    submitUpload() {
      this.$refs.upload.submit();
    },
    //清空已上传的文件列表（该方法不支持在 before-upload 中调用）
    clearFiles(){
      this.$refs.upload.clearFiles();
      this.fileList = [];
    },
    //取消上传某个文件
    abortFiles(file){
      this.$refs.upload.abort(file);
    },



    /**
     * 手动上传点击确认
     * */
    submitUploadSD(){
      let arr = this.fileList;
      let str = {
        fileList:arr
      }
      this.$emit('uploadFile',str);
    },

    /**
     * 手动刷新上传组件
     * */
    RefreshUpload(){
      let arr = this.fileList;
      this.Refresh = false;
      this.$nextTick(()=>{
        this.Refresh = true;
      })
    },

    /**
     * 打开查看图片的弹框
     * */
    handlePictureCardPreview(file) {
      this.dialogImageUrl = file.url;
      this.dialogVisible = true;
    },
    //删除当前图片
    deleteImg(item){
      let uid = item.other.uid;
      this.fileList.forEach((item,index)=>{
        if(item.uid == uid){
          this.fileList.splice(index,1)
        }
      })
    }


  },
  components: {
    imgView
  },
  beforeDestroy() {
  }

}
</script>

<style lang='scss' scoped>
.uploadImgBody{
  .imgViewDiv{
    display: inline-block;
    vertical-align: top;
  }
  .upload-image{
    display: inline-block;
  }
}
.upload-image{
  div{
    width:100%;
    height: 100%;
  }
}
</style>