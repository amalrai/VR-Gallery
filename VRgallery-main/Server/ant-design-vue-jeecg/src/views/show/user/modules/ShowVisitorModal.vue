<template>
  <j-modal
    :title="title"
    :width="width"
    :visible="visible"
    switchFullscreen
    @ok="handleOk"
    :okButtonProps="{ class:{'jee-hidden': disableSubmit} }"
    @cancel="handleCancel"
    :cancelText="$t('关闭')"
    :okText="$t('确定')">
    <show-visitor-form ref="realForm" @ok="submitCallback" :disabled="disableSubmit"></show-visitor-form>
  </j-modal>
</template>

<script>

  import ShowVisitorForm from './ShowVisitorForm'
  export default {
    name: 'ShowVisitorModal',
    components: {
      ShowVisitorForm
    },
    data () {
      return {
        title:'',
        width:800,
        visible: false,
        disableSubmit: false
      }
    },
    methods: {
      open () {
        this.visible=true
        this.$nextTick(()=>{
          this.$refs.realForm.load();
        })
      },
      close () {
        this.$emit('close');
        this.visible = false;
      },
      handleOk () {
        this.$refs.realForm.submitForm();
      },
      submitCallback(){
        this.$emit('ok');
        this.visible = false;
      },
      handleCancel () {
        this.close()
      }
    }
  }
</script>