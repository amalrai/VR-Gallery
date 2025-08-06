<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>

          <a-col :span="20">
            <span>{{$t('游客累计登录次数：')}}</span><span>{{count}}</span>
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
    name: 'ShowVisitorForm',
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
        count: 0,
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
          loadVisitorCount: "/user/showUser/queryVisitorCount"
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
      load () {
        var that = this;
        that.confirmLoading = true;
        getAction(this.url.loadVisitorCount,"").then((res) => {
          if (res.success) {
            that.count = res.result;
          }else{
            that.$emit('close');
            that.visible = false;
            that.$message.warning(res.message);
          }
        }).finally(() => {
          that.confirmLoading = false;
        })
      }
    }
  }
</script>