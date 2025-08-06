<template>
  <a-spin :spinning="confirmLoading">
    <j-form-container :disabled="formDisabled">
      <a-form-model ref="form" :model="model" :rules="validatorRules" slot="detail">
        <a-row>
          <a-col :span="24">
            <a-form-model-item :label="$t('平台')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="platform">
              <a-select :placeholder="$t('请选择')" v-model="model.platform" >
                <a-select-option value="IOS" >{{$t('IOS')}}</a-select-option>
                <a-select-option value="Android">{{$t('Android')}}</a-select-option>
                <a-select-option value="GMO">{{$t('GMO')}}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('订单类型')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="orderType">
              <a-select :placeholder="$t('请选择')" v-model="model.orderType" >
                <a-select-option value="Consumable" >{{$t('Consumable')}}</a-select-option>
                <a-select-option value="non-Consumable">{{$t('non-Consumable')}}</a-select-option>
                <a-select-option value="Subscription">{{$t('Subscription')}}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('会员费用(JPN)')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="vipFee">
              <a-input-number v-model="model.vipFee" :placeholder="$t('请输入会员费用(JPN)')" style="width: 100%" @change="computeFee"/>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('会员时限(days)')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="timeLimit">
              <a-input-number v-model="model.timeLimit" :placeholder="$t('请输入会员时限(days)')" style="width: 100%" />
            </a-form-model-item>
          </a-col>
          <a-col :span="24" :hidden = "model.platform!=='GMO'">
            <a-form-model-item :label="$t('折扣码')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="discountCode">
              <a-input v-model="model.discountCode" :placeholder="$t('请输入折扣码')"  ></a-input>
            </a-form-model-item>
          </a-col>
          <a-col :span="24" :hidden = "model.platform!=='GMO'">
            <a-form-model-item :label="$t('打折价钱')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="discountFee" >
              <a-input-number v-model="model.discountFee" :placeholder="$t('请输入打折价钱')" style="width: 100%" @change="computeFee"/>
            </a-form-model-item>
          </a-col>
          <a-col :span="24" :hidden = "model.platform!=='GMO'">
            <a-form-model-item :label="$t('打折后价钱')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="afterDiscountFee">
              <a-input-number v-model="model.afterDiscountFee" placeholder="" style="width: 100%" readonly/>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('状态')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="status">
              <a-select :placeholder="$t('请选择')" v-model="model.status" >
                <a-select-option value="0" >{{$t('禁用')}}</a-select-option>
                <a-select-option value="1">{{$t('启用')}}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('所属展馆')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showMuseumId">
              <j-search-select-tag
                ref="selectTag"
                :placeholder="$t('请选择所属展馆')"
                v-model="model.showMuseumId"
                dict="show_museum,name,id"
                :pageSize="6"
                :async="true"
                :disabled = "canSelectMuseum">
              </j-search-select-tag>
            </a-form-model-item>
          </a-col>
          <a-col :span="24">
            <a-form-model-item :label="$t('所属房间')" :labelCol="labelCol" :wrapperCol="wrapperCol" prop="showRoomId">
              <j-search-select-tag
                ref="selectTag"
                :placeholder="$t('请选择所属房间')"
                v-model="model.showRoomId"
                :dict="'show_room,name,id,show_museum_id = ' + model.showMuseumId"
                :pageSize="6"
                :async="false">
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
    name: 'ShowVipFeeForm',
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
        canSelectMuseum: false,
        model:{
          status:0
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
          platform :[{required: true, message: this.$t('请选择平台!')}],
          orderType :[{required: true, message: this.$t('请选择订单类型!')}],
          vipFee :[
            {required: true, message: this.$t('请输入会员费用!')},
            {required: true,pattern:/^([1-9][0-9]*)$/,message: this.$t('金额不正确!')}
          ],
          timeLimit :[
            {required: true, message: this.$t('请输入会员时限!')},
            {required: true,pattern:/^([1-9][0-9]*)$/,message: this.$t('请输入正确的会员时限!')}
          ],
          showMuseumId :[{required: true, message: this.$t('请选择所属展馆!')}],
          showRoomId :[{required: true, message: this.$t('请选择所属房间!')}],
          discountCode :[
            { max: 50, message: this.$t('超过50个字符，请重新输入'), trigger: 'change' },
            {validator: this.validateDiscountCode,trigger: 'change'}
          ],
          discountFee :[
            {required: false,pattern:/^([1-9][0-9]*)$/,message: this.$t('金额不正确!')},
            {validator: this.validateDiscountFee,trigger: 'change'}
          ],
        },
        url: {
          add: "/vipfee/showVipFee/add",
          edit: "/vipfee/showVipFee/edit",
          queryById: "/vipfee/showVipFee/queryById"
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
        this.modelDefault.showRoomId = null;
        this.edit(this.modelDefault);
      },
      edit (record) {
        this.$nextTick(() => {
          console.log(this.$refs.selectTag);
          this.$refs.selectTag.initDictData();
          if(this.$store.getters.userInfo.showMuseumId &&this.$store.getters.userInfo.showMuseumId !== ""){
            this.canSelectMuseum = true;
          }
        })
        this.model = Object.assign({}, record);
        this.model.status = String(record.status);
        this.visible = true;
      },
      computeFee(){
        if(this.model.vipFee && this.model.discountFee){
          this.model.afterDiscountFee = this.model.vipFee - this.model.discountFee;
        }
      },
      validateDiscountCode (rule, value, callback) {
        const discountCode =this.model.discountCode;
        const discountFee =this.model.discountFee;
        if (discountFee && discountFee > 0) {
          if(!discountCode){
            callback(this.$t('请输入折扣码！'));
          }
        }
        callback();
      },
      validateDiscountFee (rule, value, callback) {
        const vipFee =this.model.vipFee;
        const discountCode =this.model.discountCode;
        const discountFee =this.model.discountFee;
        if (discountCode && discountCode.length > 0) {
          if(discountFee){
            if(discountFee <= 0){
              callback(this.$t('请输入正确的折扣费用！'));
            }else{
              if(vipFee && vipFee <= discountFee){
                callback(this.$t('请输入正确的折扣费用！'));
              }
            }
          }else{
            callback(this.$t('请输入折扣费用！'));
          }
        }
        callback();
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