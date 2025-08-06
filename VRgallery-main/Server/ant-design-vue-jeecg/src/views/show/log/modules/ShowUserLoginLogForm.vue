<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="24">
            <a-form-model-item label="用户ID" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showUserId">
              <a-input v-model="model.showUserId" placeholder="请输入用户ID"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="用户昵称" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showUserNickName">
              <a-input v-model="model.showUserNickName" placeholder="请输入用户昵称"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="用户姓名" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showUserRealName">
              <a-input v-model="model.showUserRealName" placeholder="请输入用户姓名"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="用户账号" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showUserEmail">
              <a-input v-model="model.showUserEmail" placeholder="请输入用户账号"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="登录展馆ID" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showMuseumId">
              <a-input v-model="model.showMuseumId" placeholder="请输入登录展馆ID"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="登录房间ID" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showRoomId">
              <a-input v-model="model.showRoomId" placeholder="请输入登录房间ID"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="登录ip" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="loginIp">
              <a-input v-model="model.loginIp" placeholder="请输入登录ip"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="登录时间" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="loginTime">
              <j-date placeholder="请选择登录时间" v-model="model.loginTime"  style="width: 100%" />
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
    name: 'ShowUserLoginLogForm',
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
          add: "/log/showUserLoginLog/add",
          edit: "/log/showUserLoginLog/edit",
          queryById: "/log/showUserLoginLog/queryById"
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