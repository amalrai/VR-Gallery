<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="24">
            <a-form-model-item :label="$t('房间名')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="name">
              <a-input v-model="model.name" :placeholder="$t('请输入房间名')"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('房间编号')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="roomNo">
              <a-input-number v-model="model.roomNo" :placeholder="$t('请输入房间编号')" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('说明')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="description">
              <a-textarea v-model="model.description" :placeholder="$t('请输入说明')"  rows="4"></a-textarea>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('是否免费')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="freeFlag">
              <a-select :placeholder="$t('请选择')" v-model="model.freeFlag" >
                <a-select-option value="0" >{{$t('VIP')}}</a-select-option>
                <a-select-option value="1">{{$t('免费')}}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('展馆')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showMuseumId">
              <j-search-select-tag
                :placeholder="$t('请选择展馆')"
                v-model="model.showMuseumId"
                dict="show_museum,name,id"
                :pageSize="6"
                :async="true"
                :disabled = "canSelectMuseum">
              </j-search-select-tag>
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
        model:{
         },
        canSelectMuseum: false,
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
          name: [
                      { required: true, message: this.$t('请输入房间名') }
                ],
          roomNo: [
                      { required: true, message: this.$t('请输入房间编号') }
                ],
          showMuseumId: [
                      { required: true, message: this.$t('请选择展馆') }
          ]
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
      add () {
        this.modelDefault.showMuseumId = this.$store.getters.userInfo.showMuseumId;
        this.modelDefault.freeFlag = "0";
        this.edit(this.modelDefault);
      },
      edit (record) {
        this.model = Object.assign({}, record);
        if(record.freeFlag){
          this.model.freeFlag = String(record.freeFlag);
        }
        this.visible = true;
        if(this.$store.getters.userInfo.showMuseumId &&this.$store.getters.userInfo.showMuseumId !== ""){
           this.canSelectMuseum = true;
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