<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="20">
            <a-form-model-item :label="$t('头像')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="avatar">
              <j-image-upload :text="$t('点击上传')" v-model="model.avatar"></j-image-upload>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('所属APP')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showAppId">
<!--              <a-input v-model="model.appName"  disabled></a-input>-->
              <j-dict-select-tag v-model="model.showAppId" placeholder="请选择所属APP" dictCode="show_app,name,id" disabled/>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('昵称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="nickName">
              <a-input v-model="model.nickName"  disabled></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('姓名')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="realName">
              <a-input v-model="model.realName"  disabled></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('性别')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="sex" >
              <a-select  v-model="model.sex" disabled>
                <a-select-option value="0" >{{$t('男')}}</a-select-option>
                <a-select-option value="1">{{$t('女')}}</a-select-option>
                <a-select-option value="2">{{$t('其他')}}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('年代')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="years">
              <a-select  v-model="model.years" disabled>
                <a-select-option value="1" >{{$t('10代')}}</a-select-option>
                <a-select-option value="2">{{$t('20代')}}</a-select-option>
                <a-select-option value="3">{{$t('30代')}}</a-select-option>
                <a-select-option value="4" >{{$t('40代')}}</a-select-option>
                <a-select-option value="5">{{$t('50代')}}</a-select-option>
                <a-select-option value="6">{{$t('60代')}}</a-select-option>
                <a-select-option value="7">{{$t('70代～')}}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('手机号')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="phone">
              <a-input v-model="model.phone"  disabled ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('邮箱')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="email">
              <a-input v-model="model.email"  disabled ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('注册时间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="registerTime">
              <j-date  v-model="model.registerTime"  style="width: 100%" disabled/>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('状态')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="state">
              <a-select  v-model="model.state" disabled>
                <a-select-option value="0" >{{$t('禁用')}}</a-select-option>
                <a-select-option value="1">{{$t('启用')}}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('会员权限')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="isAuth">
              <a-select v-model="model.isAuth" disabled>
                <a-select-option value="0" >{{$t('关闭')}}</a-select-option>
                <a-select-option value="1">{{$t('开启')}}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="20">
            <a-form-model-item :label="$t('详细地址')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="address">
              <a-input v-model="model.address"  disabled ></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
    </j-form-container>
  </a-spin>
</template>

<script>

  import { httpAction, getAction } from '@api/manage'
  import { validateDuplicateValue } from '@/utils/util'

  export default {
    name: 'ShowUserForm',
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
          add: "/user/showUser/add",
          edit: "/user/showUser/edit",
          queryById: "/user/showUser/queryById"
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
        this.model.sex = String(record.sex);
        this.model.state = String(record.state);
        this.model.years = String(record.years);
        this.model.isAuth = String(record.isAuth);
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