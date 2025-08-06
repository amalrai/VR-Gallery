<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail" >
        <a-tabs defaultActiveKey="1" >
          <a-tab-pane tab="BaseInfo" key="1">
            <div>
              <a-form-model ref="checkForm" :model="model" :rules="validatorRules" slot="detail" >
              <a-col :span="24">
                <a-form-model-item :label="$t('展品名')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="name">
                  <a-textarea :rows="5" v-model="model.name" :placeholder="$t('请输入展品名')" required />
                </a-form-model-item>
              </a-col>
              <a-col :span="24">
                <a-form-model-item :label="$t('状态')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="status">
                  <a-select :placeholder="$t('请选择')" v-model="model.status" >
                    <a-select-option value="0" >{{$t('启用')}}</a-select-option>
                    <a-select-option value="1">{{$t('禁用')}}</a-select-option>
                  </a-select>
                </a-form-model-item>
              </a-col>
              <a-col :span="24">
                <a-form-model-item :label="$t('权限')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="auth">
                  <a-select :placeholder="$t('请选择')" v-model="model.auth" >
                    <a-select-option value="0" >{{ $t('免费') }}</a-select-option>
                    <a-select-option value="1">{{ $t('VIP') }}</a-select-option>
                  </a-select>
                </a-form-model-item>
              </a-col>
              <a-col :span="24">
                <a-form-model-item :label="$t('文字简介')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="text">
                  <a-textarea :rows="5" v-model="model.text" :placeholder="$t('请输入文字简介')"  />
                </a-form-model-item>
              </a-col>
              </a-form-model>
            </div>
          </a-tab-pane>
          <a-tab-pane tab="MainGraph" key="2">
            <div>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('主图')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="mainGraphUrl">
                    <j-image-upload-new :text="$t('点击上传')" :number="1" :isMultiple="false" :isSquare="true" :limitWidth="1000" :limitHeight="1000" :limitSize="500"
                                        isEncodeParam="1" bizPath="ia/exhibits/mainGraphUrl/" v-model="model.mainGraphUrl" :afterUpload="handleMainGraphUrl"></j-image-upload-new>
                  </a-form-model-item>
                </a-col>
                <a-input v-model="model.mainGraphEncodeUrl" v-show="false"></a-input>
                <a-col :span="6">
                  1.{{$t('支持jpg')}}<br>
                  2.{{$t('分辨率不可以超过1000x1000')}}<br>
                  3.{{$t('分辨率比例为1:1，如500×500,900×900')}}<br>
                  4.{{$t('单张图片不得超过500kb')}}
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="mainGraphName">
                    <a-input v-model="model.mainGraphName"></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="mainGraphSize">
                    <a-input v-model="model.mainGraphSize"  disabled></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('上传时间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="mainGraphTime" >
                    <j-date date-format="YYYY-MM-DD HH:mm:ss"  v-model="model.mainGraphTime"  style="width: 100%" disabled/>
                  </a-form-model-item>
                </a-col>
              </a-row>
            </div>
          </a-tab-pane>
          <a-tab-pane tab="Introduction" key="3">
            <div>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('介绍图')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="introductionImageUrl">
                    <j-image-upload-new :text="$t('点击上传')" :number="1" :isMultiple="false" :limitWidth="1000" :limitHeight="1000" :limitSize="500"
                                        isEncodeParam="1" bizPath="ia/exhibits/introductionImageUrl/" v-model="model.introductionImageUrl" :afterUpload="handleIntroductionImageUrl"></j-image-upload-new>
                  </a-form-model-item>
                  <a-input v-model="model.introductionImageEncodeUrl" v-show="false"></a-input>
                </a-col>
                <a-col :span="4">
                  1.{{$t('支持jpg')}}<br>
                  2.{{$t('分辨率不可以超过1000x1000')}}<br>
                  3.{{$t('单张图片不得超过500kb')}}
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="introductionImageName">
                    <a-input v-model="model.introductionImageName"></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="introductionImageSize">
                    <a-input v-model="model.introductionImageSize" disabled></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('上传时间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="introductionImageTime" >
                    <j-date date-format="YYYY-MM-DD HH:mm:ss" placeholder="" v-model="model.introductionImageTime"  style="width: 100%" disabled/>
                  </a-form-model-item>
                </a-col>
              </a-row>
            </div>
          </a-tab-pane>
          <a-tab-pane tab="Video" key="4">
            <div>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('视频')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="videoUrl">
                    <j-upload-new :text="$t('点击上传')" :number="1" :multiple="false" :limitSize="30720" :limitWidth="1920" :limitHeight="1080" :accept="videoAccept"
                                  bizPath="ia/exhibits/videoUrl/" v-model="model.videoUrl" :afterUpload="handleVideoUrl"></j-upload-new>
                  </a-form-model-item>
                </a-col>
                <a-input v-model="model.videoEncodeUrl" v-show="false"></a-input>
                <a-col :span="4">
                  1.{{$t('支持MP4格式')}}<br>
                  2.{{$t('大小不超过30MB')}}<br>
                  3.{{$t('分辨率不可以超过1920x1080')}}
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="videoName">
                    <a-input v-model="model.videoName"></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="videoSize">
                    <a-input v-model="model.videoSize" placeholder=""  disabled></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('上传时间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="videoTime" >
                    <j-date date-format="YYYY-MM-DD HH:mm:ss" placeholder="" v-model="model.videoTime"  style="width: 100%" disabled/>
                  </a-form-model-item>
                </a-col>
              </a-row>
            </div>
          </a-tab-pane>
          <a-tab-pane tab="Voice" key="5">
            <div>
              <a-row>
                <a-col :span="12">
                    <a-form-model-item :label="$t('音频')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="voiceUrl">
                    <j-upload-new :text="$t('点击上传')" :number="1" :multiple="false" :limitSize="20480" :accept="voiceAccept" bizPath="ia/exhibits/voiceUrl/" v-model="model.voiceUrl" :afterUpload="handleVoiceUrl"></j-upload-new>
                  </a-form-model-item>
                </a-col>
                <a-col :span="4">
                1.{{$t('支持MP3格式')}}<br>
                2.{{$t('大小不超过20MB')}}
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="voiceName">
                    <a-input v-model="model.voiceName" placeholder=""></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="voiceSize">
                    <a-input v-model="model.voiceSize"  disabled></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('上传时间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="voiceTime" >
                    <j-date date-format="YYYY-MM-DD HH:mm:ss" v-model="model.voiceTime"  style="width: 100%" disabled/>
                  </a-form-model-item>
                </a-col>
              </a-row>
            </div>
          </a-tab-pane>
          <a-tab-pane tab="Link" key="6">
            <div>
              <!--<a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('视频外链')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="videoLink">
                    <a-input v-model="model.videoLink"></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('更新时间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="videoLinkTime">
                    <j-date date-format="YYYY-MM-DD HH:mm:ss"  placeholder="" v-model="model.videoLinkTime"  style="width: 100%" disabled/>
                  </a-form-model-item>
                </a-col>
              </a-row>-->
              <a-row v-for="(value,index) in webLinkObj" :key="index">
                <a-col :span="8">
                  <a-form-model-item :label="$t('链接标题')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="webLink">
                    <a-input v-model="value.title" ></a-input>
                  </a-form-model-item>
                </a-col>
                <a-col :span="12">
                  <a-form-model-item :label="$t('链接地址')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="webLink">
                    <a-input v-model="value.url" ></a-input>
                  </a-form-model-item>
                </a-col>
                <a-col :span="4">
                  <a-button style="border-radius: 50%;width: 40px;height: 40px;" @click="addWebLink(index)">
                    <a-icon type="plus" style="width: 40px;position: absolute;left: 0;top: 12px;"/>
                  </a-button>
                  <a-button style="border-radius: 50%;width: 40px;height: 40px;margin-left: 10px;" @click="minusWebLink(index)">
                    <a-icon type="minus" style="width: 40px;position: absolute;left: 0;top: 12px;"/>
                  </a-button>
                </a-col>

              </a-row>
              <a-row>
                <a-col :span="8">
                  <a-form-model-item :label="$t('更新时间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="webLinkTime">
                    <j-date date-format="YYYY-MM-DD HH:mm:ss"  placeholder="" v-model="model.webLinkTime"  style="width: 100%" disabled/>
                  </a-form-model-item>
                </a-col>
              </a-row>
            </div>
          </a-tab-pane>
          <a-tab-pane tab="AnimationThumbnail" key="7">
            <div>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('缩略图')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="animationThumbnailUrl">
                    <j-image-upload-new :text="$t('点击上传')" :number="1" :isMultiple="false" :limitWidth="1000" :limitHeight="1000" :limitSize="200"
                                    isEncodeParam="1" bizPath="ia/exhibits/animationThumbnailUrl/" v-model="model.animationThumbnailUrl" :afterUpload="handleAnimationThumbnailUrl"></j-image-upload-new>
                  </a-form-model-item>
                  <a-input v-model="model.animationThumbnailEncodeUrl" v-show="false"></a-input>
                </a-col>
                <a-col :span="4">
                  1.{{$t('支持jpg')}}<br>
                  2.{{$t('分辨率不可以超过1000x1000')}}<br>
                  3.{{$t('单张图片不得超过200kb，推荐单张50KB')}}
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="animationThumbnailName">
                    <a-input v-model="model.animationThumbnailName" placeholder=""></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="animationThumbnailSize">
                    <a-input v-model="model.animationThumbnailSize" placeholder=""  disabled></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('上传时间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="animationThumbnailTime" >
                    <j-date date-format="YYYY-MM-DD HH:mm:ss" placeholder="" v-model="model.animationThumbnailTime"  style="width: 100%" disabled/>
                  </a-form-model-item>
                </a-col>
              </a-row>
            </div>
          </a-tab-pane>
          <a-tab-pane tab="FrameAnimation" key="8">
            <div>
              <a-row>
                <a-col :span="24">
                  <a-input v-model="model.frameAnimationEncodeUrl" v-show="false"></a-input>
                  <a-form-model-item :label="$t('帧动画')" :labelCol="labelColNew" :wrapperCol="wrapperColNew" prop="frameAnimationUrl">
                    <div v-show="!showImage">
                      <j-upload-new :text="$t('点击上传')" :number="1" :isMultiple="false" :accept="zipAccept" ref="zipForm" isEncodeParam="1" action="/exhibits/showExhibits/uploadFrameAnimation" bizPath="ia/exhibits/frameAnimationUrl/" v-model="frameAnimationUrlZip" :afterUpload="handleFrameAnimationUrl"></j-upload-new>
                      <span>
                          1.{{$t('支持zip')}}<br>
                          2.{{$t('分辨率不可以超过1000x1000')}}<br>
                          3.{{$t('总数不可以超过50张')}}<br>
                          4.{{$t('单张图片不得超过200kb，推荐单张50KB')}}
                      </span>
                    </div>
                    <div v-show="showImage">
                      <j-image-upload v-model="model.frameAnimationUrl" :value="model.frameAnimationUrl" disabled></j-image-upload>
                      <a-button @click="clearFrameAnimationUrl" type="primary" >{{ $t('清空') }}</a-button>
                    </div>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="frameAnimationName">
                    <a-input v-model="model.frameAnimationName" placeholder=""></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="frameAnimationSize">
                    <a-input v-model="model.frameAnimationSize" placeholder=""  disabled></a-input>
                  </a-form-model-item>
                </a-col>
              </a-row>
              <a-row>
                <a-col :span="12">
                  <a-form-model-item :label="$t('上传时间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="frameAnimationTime" >
                    <j-date date-format="YYYY-MM-DD HH:mm:ss" placeholder="" v-model="model.frameAnimationTime"  style="width: 100%" disabled/>
                  </a-form-model-item>
                </a-col>
              </a-row>
            </div>
          </a-tab-pane>
        </a-tabs>


      </a-form-model>
    </j-form-container>
  </a-spin>
</template>

<script>
let context = null;
import { httpAction, getAction } from '@/api/manage'
import { validateDuplicateValue } from '@/utils/util'
export default {
  name: 'ShowExhibitsForm',
  components: {
  },
  props: {
    //表单禁用
    disabled: {
      type: Boolean,
      default: false,
      required: false
    }
  },
  data () {
    return {
      showImage: false,
      videoAccept:"video/mp4",
      voiceAccept:"audio/mpeg",
      zipAccept:"application/x-zip-compressed,application/zip,application/x-zip",
      model:{
      },
      webLinkObj:[
        {
          "title":"",
          "url":""
        }
      ],
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 },
      },
      labelColNew: {
        xs: { span: 24 },
        sm: { span: 2 },
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 16 },
      },
      wrapperColLink: {
        xs: { span: 20 },
        sm: { span: 16 },
      },
      wrapperColNew: {
        xs: { span: 24 },
        sm: { span: 20 },
      },
      confirmLoading: false,
      validatorRules: {
        // name :[
        //   { max: 30, message: this.$t('超过30个字符，请重新输入'), trigger: 'change' }],
      },
      url: {
        add: "/exhibits/showExhibits/add",
        edit: "/exhibits/showExhibits/edit",
        queryById: "/exhibits/showExhibits/queryById"
      },
      frameAnimationUrlZip:"",
    }
  },
  computed: {
    formDisabled(){
      return this.disabled
    },
  },
  created () {
    context = this;
    //备份model原始值
    this.modelDefault = JSON.parse(JSON.stringify(this.model));
  },
  methods: {
    edit (record) {
      const that = this;
      that.confirmLoading = true;
      getAction(this.url.queryById,{id:record.id}).then((res) => {
        if (res.success) {
          record = res.result;
          that.model = Object.assign({}, record);
          that.model.status = String(record.status);
          that.model.auth = String(record.auth);
          that.visible = true;
          if(that.model.frameAnimationUrl && that.model.frameAnimationUrl!=null && that.model.frameAnimationUrl.length > 0){
            that.showImage = true;
          }
          if(record.webLink && record.webLink.length > 0){
            that.webLinkObj = JSON.parse(record.webLink);
          }
        }else{
          that.$emit('close');
          that.visible = false;
          that.$message.warning(res.message);
        }
      }).finally(() => {
        that.confirmLoading = false;
      })
    },
    handleMainGraphUrl:function (response){
      console.log("handleMainGraphUrl", response);
      if(response.success && response.result != null){
        this.model.mainGraphEncodeUrl = response.result.encodeUrl;
        this.model.mainGraphName = response.result.name;
        this.model.mainGraphSize = response.result.size;
        this.model.mainGraphTime = response.result.time;
      }
    },
    handleIntroductionImageUrl:function (response){
      console.log("handleIntroductionImageUrl", response);
      if(response.success && response.result != null){
        this.model.introductionImageEncodeUrl = response.result.encodeUrl;
        this.model.introductionImageName = response.result.name;
        this.model.introductionImageSize = response.result.size;
        this.model.introductionImageTime = response.result.time;
      }
    },
    handleVideoUrl:function (response){
      console.log("handleVideoUrl", response);
      if(response.success && response.result != null){
        this.model.videoEncodeUrl = response.result.encodeUrl;
        this.model.videoName = response.result.name;
        this.model.videoSize = response.result.size;
        this.model.videoTime = response.result.time;
      }
    },
    handleVoiceUrl:function (response){
      console.log("handleVoiceUrl", response);
      if(response.success && response.result != null){
        this.model.voiceName = response.result.name;
        this.model.voiceSize = response.result.size;
        this.model.voiceTime = response.result.time;
      }
    },
    handleAnimationThumbnailUrl:function (response){
      console.log("handleAnimationThumbnailUrl", response);
      if(response.success && response.result != null){
        this.model.animationThumbnailEncodeUrl = response.result.encodeUrl;
        this.model.animationThumbnailName = response.result.name;
        this.model.animationThumbnailSize = response.result.size;
        this.model.animationThumbnailTime = response.result.time;
      }
    },
    handleFrameAnimationUrl:function (response){
      console.log("handleFrameAnimationUrl", response);
      if(response.success && response.result != null){
        this.model.frameAnimationEncodeUrl = response.result.encodeUrl;
        this.model.frameAnimationUrl = response.result.url;
        this.model.frameAnimationName = response.result.name;
        this.model.frameAnimationSize = response.result.size;
        this.model.frameAnimationTime = response.result.time;
        this.showImage = true;
        this.frameAnimationUrlZip = "";
      }
    },
    clearFrameAnimationUrl:function (){
      this.frameAnimationUrlZip = "";
      this.$refs.zipForm.initFileList("");
      this.$refs.zipForm.handlePathChange();
      this.model.frameAnimationUrl = "";
      this.model.frameAnimationEncodeUrl = "";
      this.showImage = false;
    },
    addWebLink(index){
      if(this.webLinkObj.length >= 10){
        return false;
      }
      var emptyObj = {"title":"","url":""};
      this.webLinkObj.splice(index+1, 0, emptyObj);
    },
    minusWebLink(index){
      if(this.webLinkObj.length <= 1){
        return false;
      }
      this.webLinkObj.splice(index, 1);
    },
    submitForm () {
      const that = this;
      // 触发表单验证
      this.$refs.form.validate(valid => {
        if (valid) {
          this.$refs.checkForm.validate(checkValid => {
            if (checkValid) {
              //人工校验
              if(this.model.name == null || this.model.name === ''){
                this.$message.error(this.$t('请输入') + this.$t('展品名'));
                return false;
              }
              if(this.model.mainGraphUrl !=null && this.model.mainGraphUrl !='' && (this.model.mainGraphName == null || this.model.mainGraphName === '')){
                this.$message.error(this.$t('请输入') + this.$t('主图') +" "+ this.$t('名称'));
                return false;
              }
              if(this.model.introductionImageUrl !=null && this.model.introductionImageUrl !='' && (this.model.introductionImageName == null || this.model.introductionImageName === '')){
                this.$message.error(this.$t('请输入') + this.$t('介绍图') +" "+ this.$t('名称'));
                return false;
              }
              if(this.model.videoUrl !=null && this.model.videoUrl !='' && (this.model.videoName == null || this.model.videoName === '')){
                this.$message.error(this.$t('请输入') + this.$t('视频') +" "+ this.$t('名称'));
                return false;
              }
              if(this.model.animationThumbnailUrl !=null && this.model.animationThumbnailUrl !='' && (this.model.animationThumbnailName == null || this.model.animationThumbnailName === '')){
                this.$message.error(this.$t('请输入') + this.$t('缩略图')  +" "+ this.$t('名称'));
                return false;
              }
              if(this.model.voiceUrl !=null && this.model.voiceUrl !='' && (this.model.voiceName == null || this.model.voiceName === '')){
                this.$message.error(this.$t('请输入') + this.$t('音频') +" "+ this.$t('名称'));
                return false;
              }
              //处理webLink
              if(this.webLinkObj.length == 0){
                this.model.webLink = "";
              }else if(this.webLinkObj.length == 1 && this.webLinkObj[0].title == "" && this.webLinkObj[0].url == ""){
                this.model.webLink = "";
              }else{
                this.model.webLink = JSON.stringify(this.webLinkObj);
              }
              that.confirmLoading = true;
              let httpurl = '';
              let method = '';
              if(!this.model.id){
                httpurl+=this.url.add;
                method = 'post';
              }else{
                httpurl+=this.url.edit;
                method = 'put';
              }
              httpAction(httpurl,this.model,method).then((res)=>{
                if(res.success){
                  that.$message.success(res.message);
                  that.$emit('ok');
                }else{
                  that.$message.warning(res.message);
                }
              }).finally(() => {
                that.confirmLoading = false;
              })
            }
          })
        }
      })
    },
  }
}
</script>