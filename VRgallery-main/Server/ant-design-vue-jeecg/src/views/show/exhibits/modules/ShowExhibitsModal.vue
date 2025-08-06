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
    <show-exhibits-form ref="realForm" @ok="submitCallback" :disabled="disableSubmit"></show-exhibits-form>
  </j-modal>
</template>

<script>

  import ShowExhibitsForm from './ShowExhibitsForm'
  export default {
    name: 'ShowExhibitsModal',
    components: {
      ShowExhibitsForm
    },
    data () {
      return {
        title:'',
        width:1400,
        visible: false,
        disableSubmit: false
      }
    },
    methods: {
      add () {
        this.visible=true
        this.$nextTick(()=>{
          this.$refs.realForm.add();
        })
      },
      edit (record) {
        this.visible=true
        this.$nextTick(()=>{
          this.$refs.realForm.edit(record);
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