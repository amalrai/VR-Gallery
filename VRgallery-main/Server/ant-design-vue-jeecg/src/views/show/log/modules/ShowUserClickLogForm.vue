<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="24">
            <a-form-model-item label="登录日志ID" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showUserLoginLogId">
              <a-input-number v-model="model.showUserLoginLogId" placeholder="请输入登录日志ID" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="展品ID" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showExhibitsId">
              <a-input-number v-model="model.showExhibitsId" placeholder="请输入展品ID" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="展品编号" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showExhibitsNo">
              <a-input-number v-model="model.showExhibitsNo" placeholder="请输入展品编号" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="简介点击次数" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="introductionCount">
              <a-input-number v-model="model.introductionCount" placeholder="请输入简介点击次数" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="链接点击次数" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="linkCount">
              <a-input-number v-model="model.linkCount" placeholder="请输入链接点击次数" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="视频点击次数" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="videoCount">
              <a-input-number v-model="model.videoCount" placeholder="请输入视频点击次数" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="音频点击次数" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="voiceCount">
              <a-input-number v-model="model.voiceCount" placeholder="请输入音频点击次数" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="商店点击次数" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="shopCount">
              <a-input-number v-model="model.shopCount" placeholder="请输入商店点击次数" style="width: 100%" />
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
    name: 'ShowUserClickLogForm',
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
        model:{
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
          add: "/log/showUserClickLog/add",
          edit: "/log/showUserClickLog/edit",
          queryById: "/log/showUserClickLog/queryById"
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
      add () {
        this.edit(this.modelDefault);
      },
      edit (record) {
        this.model = Object.assign({}, record);
        this.visible = true;
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