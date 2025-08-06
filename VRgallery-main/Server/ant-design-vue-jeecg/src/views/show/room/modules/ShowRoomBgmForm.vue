<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="12">
            <a-form-model-item :label="$t('音频')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="musicUrl">
              <j-upload-new :text="$t('点击上传')" :number="1" :multiple="false" :limitSize="20480" :accept="voiceAccept" bizPath="ia/room/musicUrl/" v-model="model.musicUrl" :afterUpload="handleMusicUrl"></j-upload-new>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :span="12">
            <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="musicName">
              <a-input v-model="model.musicName" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :span="12">
            <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="musicSize">
              <a-input v-model="model.musicSize" placeholder=""  disabled></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
    </j-form-container>
  </a-spin>
</template>

<script>

  import { httpAction, getAction } from '@/api/manage'
  import { validateDuplicateValue } from '@/utils/util'

  export default {
    name: 'ShowRoomForm',
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
        voiceAccept:"audio/mpeg",
        model:{
          id:"",
          musicUrl:"",
          musicName:"",
          musicSize:"",
         },
        labelCol: {
          xs: { span: 24 },
          sm: { span: 5 },
        },
        wrapperCol: {
          xs: { span: 24 },
          sm: { span: 16 },
        },
        confirmLoading: false,
        validatorRules: {
        },
        url: {
          add: "/room/showRoom/add",
          edit: "/room/showRoom/edit",
          queryById: "/room/showRoom/queryById"
        }
      }
    },
    computed: {
      formDisabled(){
        return this.disabled
      },
    },
    created () {
       //备份model原始值
      this.modelDefault = JSON.parse(JSON.stringify(this.model));
    },
    methods: {
      edit (roomId) {
        var that = this;
        that.confirmLoading = true;
        getAction(this.url.queryById,{id:roomId}).then((res) => {
          if (res.success) {
            that.model.id = res.result.id;
            that.model.musicUrl = res.result.musicUrl;
            that.model.musicName = res.result.musicName;
            that.model.musicSize = res.result.musicSize;
            that.visible = true;
          }else{
            that.$emit('close');
            that.visible = false;
            that.$message.warning(res.message);
          }
        }).finally(() => {
          that.confirmLoading = false;
        })
      },
      handleMusicUrl:function (response){
        console.log("handleMusicUrl", response);
        if(response.success && response.result != null){
          this.model.musicName = response.result.name;
          this.model.musicSize = response.result.size;
        }
      },
      submitForm () {
        const that = this;
        // 触发表单验证
        this.$refs.form.validate(valid => {
          if (valid) {
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
      },
    }
  }
</script>