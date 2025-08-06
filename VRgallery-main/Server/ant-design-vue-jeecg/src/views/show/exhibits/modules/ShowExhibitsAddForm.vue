<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="24">
            <a-form-model-item :label="$t('新建数量')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="count">
              <a-input-number v-model="model.count" :placeholder="$t('新建数量')" style="width: 100%" />
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
        roomId:'',
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
            count :[{required: true, message: this.$t('请输入新建数量!')},
              {validator: this.validateCount}],
        },
        url: {
          add: "/exhibits/showExhibits/add",
          edit: "/exhibits/showExhibits/edit",
          queryById: "/exhibits/showExhibits/queryById"
        },
      }
    },
    computed: {
      formDisabled(){
        return this.disabled
      },
    },
    created () {

    },
    methods: {
      add(roomId) {
        this.roomId = roomId;
        this.model.showRoomId = roomId;
        this.visible = true;
      },
      validateCount(rule,value,callback){
        if (!value || new RegExp(/^([1-9]{1})([0-9]{0,2})$/).test(value)){
          if(value=='0' || value > 100){
            callback(this.$t("新建数量输入不正确!"));
          }
          callback();
        }else{
          callback(this.$t("新建数量输入不正确!"));
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