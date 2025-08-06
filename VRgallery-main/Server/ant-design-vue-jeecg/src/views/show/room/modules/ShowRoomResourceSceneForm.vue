<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="12">
            <a-form-model-item :label="$t('场景包')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceSceneUrl">
              <j-upload-new :text="$t('点击上传')" :limitSize="102400" :number="1" :multiple="false" bizPath="ia/room/resourceSceneUrl/" v-model="model.resourceSceneUrl" :afterUpload="handleResourceSceneUrl"></j-upload-new>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :span="12">
            <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceSceneName">
              <a-input v-model="model.resourceSceneName" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :span="12">
            <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceSceneSize">
              <a-input v-model="model.resourceSceneSize" placeholder=""  disabled></a-input>
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
          resourceSceneUrl:"",
          resourceSceneName:"",
          resourceSceneSize:"",
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
            that.model.resourceSceneUrl = res.result.resourceSceneUrl;
            that.model.resourceSceneName = res.result.resourceSceneName;
            that.model.resourceSceneSize = res.result.resourceSceneSize;
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
      handleResourceSceneUrl:function (response){
        console.log("handleResourceSceneUrl", response);
        if(response.success && response.result != null){
          this.model.resourceSceneName = response.result.name;
          this.model.resourceSceneSize = response.result.size;
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