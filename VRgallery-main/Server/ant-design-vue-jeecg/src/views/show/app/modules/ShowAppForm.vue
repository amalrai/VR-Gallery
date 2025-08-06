<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="8">
            <a-form-model-item :label="$t('APPID')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="id">
              <a-input v-model="model.id" :placeholder="$t('请输入APPID')"  disabled></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="$t('APP名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="name">
              <a-input v-model="model.name" :placeholder="$t('请输入APP名称')"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="$t('APP描述')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="description">
              <a-input v-model="model.description" :placeholder="$t('请输入APP描述')"  ></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
<!--        <a-row>-->
<!--          <a-col :span="8">-->
<!--            <a-form-model-item :label="'Android '+$t('资源包')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceAndroidUrl">-->
<!--              <j-upload-new :text="$t('点击上传')" :limitSize="102400" :number="1" :multiple="false" bizPath="ia/app/resourceAndroidUrl/" v-model="model.resourceAndroidUrl" :afterUpload="handleResourceAndroidUrl"></j-upload-new>-->
<!--            </a-form-model-item>-->
<!--          </a-col>-->
<!--          <a-col :span="8">-->
<!--            <a-form-model-item :label="'ios '+$t('资源包')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceIosUrl">-->
<!--              <j-upload-new :text="$t('点击上传')" :limitSize="102400" :number="1" :multiple="false" bizPath="ia/app/resourceIosUrl/" v-model="model.resourceIosUrl" :afterUpload="handleResourceIosUrl"></j-upload-new>-->
<!--            </a-form-model-item>-->
<!--          </a-col>-->
<!--          <a-col :span="8">-->
<!--            <a-form-model-item :label="'win '+$t('资源包')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceUrl">-->
<!--              <j-upload-new :text="$t('点击上传')" :limitSize="102400" :number="1" :multiple="false" bizPath="ia/app/resourceUrl/" v-model="model.resourceUrl" :afterUpload="handleResourceUrl"></j-upload-new>-->
<!--            </a-form-model-item>-->
<!--          </a-col>-->
<!--        </a-row>-->
<!--        <a-row>-->
<!--          <a-col :span="8">-->
<!--            <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceAndroidName">-->
<!--              <a-input v-model="model.resourceAndroidName" placeholder=""></a-input>-->
<!--            </a-form-model-item>-->
<!--          </a-col>-->
<!--          <a-col :span="8">-->
<!--            <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceIosName">-->
<!--              <a-input v-model="model.resourceIosName" placeholder=""></a-input>-->
<!--            </a-form-model-item>-->
<!--          </a-col>-->
<!--          <a-col :span="8">-->
<!--            <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceName">-->
<!--              <a-input v-model="model.resourceName" placeholder=""></a-input>-->
<!--            </a-form-model-item>-->
<!--          </a-col>-->
<!--        </a-row>-->
<!--        <a-row>-->
<!--          <a-col :span="8">-->
<!--            <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceAndroidSize">-->
<!--              <a-input v-model="model.resourceAndroidSize" placeholder=""  disabled></a-input>-->
<!--            </a-form-model-item>-->
<!--          </a-col>-->
<!--          <a-col :span="8">-->
<!--            <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceIosSize">-->
<!--              <a-input v-model="model.resourceIosSize" placeholder=""  disabled></a-input>-->
<!--            </a-form-model-item>-->
<!--          </a-col>-->
<!--          <a-col :span="8">-->
<!--            <a-form-model-item :label="$t('大小')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="resourceSize">-->
<!--              <a-input v-model="model.resourceSize" placeholder=""  disabled></a-input>-->
<!--            </a-form-model-item>-->
<!--          </a-col>-->
<!--        </a-row>-->
        <a-row>
          <a-col :span="8">
            <a-form-model-item :label="'Android '+$t('安装包')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appAndroidUrl">
              <j-upload-new :text="$t('点击上传')" :limitSize="102400" :number="1" :multiple="false" bizPath="ia/app/appAndroidUrl/" v-model="model.appAndroidUrl" :afterUpload="handleAppAndroidUrl"></j-upload-new>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="'ios '+$t('安装包')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appIosUrl">
              <j-upload-new :text="$t('点击上传')" :limitSize="102400" :number="1" :multiple="false" bizPath="ia/app/appIosUrl/" v-model="model.appIosUrl" :afterUpload="handleAppIosUrl"></j-upload-new>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="'win '+$t('安装包')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appUrl">
              <j-upload-new :text="$t('点击上传')" :limitSize="102400" :number="1" :multiple="false" bizPath="ia/app/appUrl/" v-model="model.appUrl" :afterUpload="handleAppUrl"></j-upload-new>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :span="8">
            <a-form-model-item :label="$t('商店地址')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appAndroidStoreUrl">
              <a-input v-model="model.appAndroidStoreUrl" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="$t('商店地址')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appIosStoreUrl">
              <a-input v-model="model.appIosStoreUrl" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="$t('商店地址')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appStoreUrl">
              <a-input v-model="model.appStoreUrl" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :span="8">
            <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appAndroidName">
              <a-input v-model="model.appAndroidName" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appIosName">
              <a-input v-model="model.appIosName" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="$t('名称')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appName">
              <a-input v-model="model.appName" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row>
          <a-col :span="8">
            <a-form-model-item :label="$t('版本号')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appAndroidVersion">
              <a-input v-model="model.appAndroidVersion" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="$t('版本号')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appIosVersion">
              <a-input v-model="model.appIosVersion" placeholder=""></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item :label="$t('版本号')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="appVersion">
              <a-input v-model="model.appVersion" placeholder=""></a-input>
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
    name: 'ShowAppForm',
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
          add: "/show/showApp/add",
          edit: "/show/showApp/edit",
          queryById: "/show/showApp/queryById"
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
        if(!record.id){
          this.model = Object.assign({}, record);
          that.visible = true;
          return false;
        }
        var that = this;
        that.confirmLoading = true;
        getAction(this.url.queryById,{id:record.id}).then((res) => {
          if (res.success) {
            that.model = Object.assign({}, res.result);
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
      handleResourceUrl:function (response){
        console.log("handleResourceUrl", response);
        if(response.success && response.result != null){
          this.model.resourceName = response.result.name;
          this.model.resourceSize = response.result.size;
        }
      },
      handleResourceAndroidUrl:function (response){
        console.log("handleResourceAndroidUrl", response);
        if(response.success && response.result != null){
          this.model.resourceAndroidName = response.result.name;
          this.model.resourceAndroidSize = response.result.size;
        }
      },
      handleResourceIosUrl:function (response){
        console.log("handleResourceIosUrl", response);
        if(response.success && response.result != null){
          this.model.resourceIosName = response.result.name;
          this.model.resourceIosSize = response.result.size;
        }
      },
      handleAppUrl:function (response){
        console.log("handleAppUrl", response);
        if(response.success && response.result != null){
          this.model.appName = response.result.name;
        }
      },
      handleAppAndroidUrl:function (response){
        console.log("handleAppAndroidUrl", response);
        if(response.success && response.result != null){
          this.model.appAndroidName = response.result.name;
        }
      },
      handleAppIosUrl:function (response){
        console.log("handleAppIosUrl", response);
        if(response.success && response.result != null){
          this.model.appIosName = response.result.name;
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