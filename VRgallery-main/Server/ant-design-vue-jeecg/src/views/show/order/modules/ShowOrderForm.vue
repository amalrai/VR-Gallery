<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="24">
            <a-form-model-item label="订单号" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="orderId">
              <a-input v-model="model.orderId" placeholder=""  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="下单时间" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="orderTime">
              <j-date placeholder="请选择下单时间" v-model="model.orderTime"  style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="付费时间" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="payTime">
              <j-date placeholder="请选择付费时间" v-model="model.payTime"  style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="所属展馆" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showMuseumId">
              <a-input v-model="model.showMuseumId" placeholder="请输入所属展馆"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="订单金额" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="orderFee">
              <a-input-number v-model="model.orderFee" placeholder="请输入订单金额" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="打折金额" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="discountFee">
              <a-input-number v-model="model.discountFee" placeholder="请输入打折金额" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="是否使用折扣码 0否 1是" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="isDiscount">
              <a-input-number v-model="model.isDiscount" placeholder="请输入是否使用折扣码 0否 1是" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="折扣码" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="discountCode">
              <a-input v-model="model.discountCode" placeholder="请输入折扣码"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="打折后金额" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="afterDiscountFee">
              <a-input-number v-model="model.afterDiscountFee" placeholder="请输入打折后金额" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="实际支付金额" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="payFee">
              <a-input-number v-model="model.payFee" placeholder="请输入实际支付金额" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="购买人" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showUserId">
              <a-input v-model="model.showUserId" placeholder="请输入购买人"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="账号" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showUserEmail">
              <a-input v-model="model.showUserEmail" placeholder="请输入账号"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="状态 0未支付 1已支付" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="status">
              <a-input-number v-model="model.status" placeholder="请输入状态 0未支付 1已支付" style="width: 100%" />
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
    name: 'ShowOrderForm',
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
          add: "/order/showOrder/add",
          edit: "/order/showOrder/edit",
          queryById: "/order/showOrder/queryById"
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