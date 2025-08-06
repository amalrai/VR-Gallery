<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="24">
            <a-form-model-item label="演出名称" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="name" >
              <a-input v-model="model.name" placeholder="请输入演出名称" :disabled="formDisabled"></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24" >
            <a-form-model-item label="演出状态" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="state" v-show="!this.data.isAddMode" >
              <j-dict-select-tag type="select" v-model="model.state" dictCode="performState" placeholder="请选择演出状态" disabled/>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="演出开始时间" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="startTime">
              <j-date placeholder="请选择演出开始时间" v-model="model.startTime"  style="width: 100%" showTime date-format="YYYY-MM-DD HH:mm:ss" :disabled="formDisabled"/>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="演出结束时间" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="endTime">
              <j-date placeholder="请选择演出结束时间" v-model="model.endTime"  style="width: 100%" showTime date-format="YYYY-MM-DD HH:mm:ss" :disabled="formDisabled"/>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="回放开始时间" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="replayStartTime">
              <j-date placeholder="请选择回放开始时间" v-model="model.replayStartTime"  style="width: 100%" :disabled="formDisabled"/>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="回放结束时间" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="replayEndTime">
              <j-date placeholder="请选择回放结束时间" v-model="model.replayEndTime"  style="width: 100%" :disabled="formDisabled"/>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="观看人次" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="viewCount" v-show="!this.data.isAddMode" >
              <a-input-number v-model="model.viewCount" placeholder="请输入观看人次" style="width: 100%" disabled/>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item label="观看时长(min)" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="viewTime" v-show="!this.data.isAddMode" >
              <a-input-number v-model="model.viewTime" placeholder="请输入观看时长(min)" style="width: 100%" disabled/>
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
    name: 'ShowPerformForm',
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
          add: "/perform/showPerform/add",
          edit: "/perform/showPerform/edit",
          queryById: "/perform/showPerform/queryById"
        },
        data:{
          isAddMode: true, // 是否新增页面
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
        this.data.isAddMode = true;
      },
      edit (record) {
        this.model = Object.assign({}, record);
        this.visible = true;
        this.data.isAddMode = false;
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