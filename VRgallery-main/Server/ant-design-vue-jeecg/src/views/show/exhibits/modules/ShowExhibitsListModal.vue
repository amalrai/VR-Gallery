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
    <show-exhibits-list-form ref="realForm" @ok="submitCallback" :disabled="disableSubmit"></show-exhibits-list-form>
  </j-modal>
</template>

<script>

  import ShowExhibitsListForm from './ShowExhibitsListForm'
  export default {
    name: 'ShowExhibitsListModal',
    components: {
      ShowExhibitsListForm
    },
    data () {
      return {
        title:'',
        width:1000,
        visible: false,
        disableSubmit: false
      }
    },
    methods: {
      open (record) {
        this.visible=true
        this.$nextTick(()=>{
          this.$refs.realForm.open(record);
        })
      },
      close () {
        this.$emit('close');
        this.visible = false;
      },
      handleOk () {
        this.$refs.realForm.doReplace();
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